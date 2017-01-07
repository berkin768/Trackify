using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Trackify
{
    public class Program
    {
        public static string ConnectionString;
        public static void Main(string[] args)
        {
            //var projConf = new ConfigurationBuilder().AddJsonFile("project.json").Build();
            //ConnectionString = projConf["ConnectionString"];
            ConnectionString = "server=DESKTOP-BII7C2E;database=Trackify;Trusted_Connection=true;";
            Console.WriteLine("CONNECTION STRIN" + Program.ConnectionString);
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
