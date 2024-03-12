using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csgo2_demo_s3
{
    internal class Utils
    {
        public static string FormatDemoFileName()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static string ArchiveDemo(string demoPath,string demoName)
        {
            var demoStorePath = $"{demoPath}.zip";
            using (FileStream zipToOpen = new FileStream(demoStorePath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry = archive.CreateEntryFromFile(demoPath, demoName);
                }
            }
            return demoStorePath;
        }
    }
}
