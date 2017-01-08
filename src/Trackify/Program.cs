using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Trackify.ELayer;
using Trackify.Log;

namespace Trackify
{
    public class Program
    {
        public static string ConnectionString;
        public static List<User> users = new List<User>();
        public static LogManager lm = new LogManager("trackifyLog.txt");
        public static void Main(string[] args)
        {
            lm.Log(lm.fileName, 0, "", "");
            ConnectionString = "server=GE-602OE;database=Trackify;Trusted_Connection=true;";
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
