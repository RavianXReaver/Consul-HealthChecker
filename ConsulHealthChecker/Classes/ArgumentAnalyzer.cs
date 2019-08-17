using System;
using System.Linq;
using ConsulHealthChecker.Models;

namespace ConsulHealthChecker.Classes
{
    public static class ArgumentAnalyzer
    {
        private static (AnalyzerStatus, ServiceModel) Validator(string[] args)
        {
            var service = new ServiceModel();
            
            foreach (var ser in args)
            {
                var val = ser.Replace("\"", "").Split(";");

                if (val.Length != 3)
                {
                    return (AnalyzerStatus.MissingArgs, null);
                }

                if (!val.Any(vals => val[0].Contains("http://") || val[0].Contains("https://")))
                {
                    return (AnalyzerStatus.InvalidHost, null);
                }

                if (val.Any(vals => string.IsNullOrWhiteSpace(val[1])))
                {
                    return (AnalyzerStatus.InvalidServiceName, null);
                }

                try
                {
                    if (val.Any(vals => Convert.ToInt32(val[2]) <= 0))
                    {
                        return (AnalyzerStatus.InvalidTime, null);
                    }
                    else
                    {
                        service.ConsulServerAddress = val[0];
                        service.ServiceName = val[1];
                        service.Interval = Convert.ToInt32(val[2]);
                        
                        return (AnalyzerStatus.ValidArgs, service);
                    }
                }
                catch (Exception e)
                {
                    return (AnalyzerStatus.InvalidTime, null);
                }
            }

            return (AnalyzerStatus.NoArgs, null);
        }

        /// <summary>
        /// Analyzes the input arguments and returns the valid host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ServiceModel Checker(string[] args)
        {
            var (status, serviceModel) = Validator(args);

            switch (status)
            {
                case AnalyzerStatus.ValidArgs:
                    Console.WriteLine("Accepted arguments");
                    return serviceModel;
                case AnalyzerStatus.MissingArgs:
                    Console.WriteLine("Missing Arguments : ConsulAddress;ServiceName;Inteval Eg: \"http://localhost:8500;TestAPI;50\"");
                    break;
                case AnalyzerStatus.NoArgs:
                    Console.WriteLine("No arguments Entered");
                    break;
                case AnalyzerStatus.InvalidHost:
                    Console.Write("Invalid format for host. Expected host value : \n http(s)://google.com \n http(s)://0.0.0.0:00 \n http(s)://localhost:00");
                    break;
                case AnalyzerStatus.InvalidServiceName:
                    Console.Write("Invalid ServiceName. Expected [A-Z,0-9] Eg: TestAPI123");
                    break;
                case AnalyzerStatus.InvalidTime:
                    Console.Write("Invalid Time. For best results, time should be over 10ms. Eg: 10");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            return null;
        }
    }


    internal enum AnalyzerStatus
    {
        ValidArgs,
        MissingArgs,
        NoArgs,
        InvalidHost,
        InvalidServiceName,
        InvalidTime
    }
}