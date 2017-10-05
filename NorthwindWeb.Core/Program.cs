using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text;

namespace NorthwindWeb.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            //initialize the databse
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<Context.NorthwindDatabase>();
                    //test data
                    Context.NorthwindTestDatabaseInitializer.InsertNorthwindTestData(context);

                    //add views
                    //todo ensure that this is a thing
                    var sqlFiles = Directory.GetFiles(Startup._hostingEnvironment.ContentRootPath.ToString() + "\\App_Data\\SQL", "*.sql").OrderBy(x => x);

                    foreach (string file in sqlFiles)
                    {
                        context.Database.ExecuteSqlCommand(File.ReadAllText(file));
                    }
                }
                catch (Exception ex)
                {
                    //todo make it to log via log4net
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(null, ex, "An error occurred while seeding the database.", null);
                }
            }

            host.Run();
        }

        /// <summary>
        /// In case there are all Views in one sql file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <remarks>
        ///var sqlCommands = Directory
        ///                    .EnumerateFiles(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "*.sql")
        ///                    .OrderBy(x => x)
        ///                    .SelectMany(ReadAllCommands);
        ///
        ///foreach (string command in sqlCommands)
        ///{
        ///    context.Database.ExecuteSqlCommand(command);
        ///}
        /// </remarks>
        static IEnumerable<string> ReadAllCommands(string path)
        {
            StringBuilder sb = null;
            foreach (string line in File.ReadLines(path))
            {
                if (string.Equals(line, "GO", StringComparison.OrdinalIgnoreCase))
                {
                    if (null != sb && 0 != sb.Length)
                    {
                        string item = sb.ToString();
                        if (!string.IsNullOrWhiteSpace(item)) yield return item;
                        sb = null;
                    }
                }
                else
                {
                    if (null == sb) sb = new StringBuilder();
                    sb.AppendLine(line);
                }
            }
            if (null != sb && 0 != sb.Length)
            {
                string item = sb.ToString();
                if (!string.IsNullOrWhiteSpace(item)) yield return item;
            }
        }
    }
}
