using SitemapGenerator.Helpers;
using SitemapGenerator.Sitemap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the url of website");
            var url = "https://techgeek.nowfloats.com";

            var path = @"C:\Users\Ravindra Naik\Desktop\sitemap.xml";
            var urls = CrawlHelper.GetAllUrlsForSitemap(url);
            var siteMap = new Sitemap.Sitemap();
            foreach(var webUrl in urls)
            {
                siteMap.Add(new SitemapLocation
                {
                    ChangeFrequency = SitemapLocation.eChangeFrequency.daily,
                    Url = webUrl
                });
            }
            
            siteMap.WriteSitemapToFile(path); 
        }
    }
}
