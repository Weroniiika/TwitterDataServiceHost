using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using TwitterWCFService;
using NLog;

namespace GettingStartedHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8080/TwitterWCFService/");
            Logger logger = LogManager.GetLogger("Service Host");

            ServiceHost host = new ServiceHost(typeof(TwitterService), baseAddress);

            try
            {
                host.AddServiceEndpoint(typeof(ITwitterService), new WSHttpBinding(), "TwitterService");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                host.Description.Behaviors.Add(smb);

                host.Open();
                logger.Info("service host opened");
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                host.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                Console.ReadLine();
                host.Abort();
            }
        }
    }
}
