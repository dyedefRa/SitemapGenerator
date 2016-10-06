using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitemapGenerator.Helpers
{
    public class CrawlHelper
    {
        internal static List<string> GetAllUrlsForSitemap(string url)
        {
            var xxx = string.Empty;
            try
            {
                var tmpList = new List<string>(); var finalList = new List<string>();
                var result = GetAllUrls(url);
                result = result.Distinct().ToList();
                foreach(var weburl in result)
                {

                    if(weburl.ToLower().StartsWith(url.ToLower()) && !weburl.ToLower().Equals(url.ToLower()))
                    {
                        var tmpResult = GetAllUrls(weburl);
                        if(tmpResult != null && tmpResult.Count() > 0)
                        {
                            tmpResult = tmpResult.Distinct().ToList();
                            foreach (var subUrl in tmpResult)
                            {
                                tmpList.Add(subUrl);
                            }
                        }
                        
                       
                    }
                    

                    finalList.Add(weburl);
                }

                if (tmpList != null && tmpList.Count() > 0)
                {
                    tmpList = tmpList.Distinct().ToList();
                    foreach (var tmpUrls in tmpList)
                    {
                        if (tmpUrls.ToLower().StartsWith(url.ToLower()) && !tmpUrls.ToLower().Equals(url.ToLower()))
                        {
                            xxx = tmpUrls;
                            var finalResult = GetAllUrls(tmpUrls);
                            if(finalResult != null && finalResult.Count() > 0)
                            {
                                finalResult = finalResult.Distinct().ToList();
                            }
                            foreach (var subUrl in finalResult)
                            {
                                finalList.Add(subUrl);
                            }
                        }
                       
                    }
                }
              
                
                if(finalList != null && finalList.Count() > 0)
                {
                    finalList = finalList.Distinct().ToList();
                }
              

                return finalList;

            }
            catch(Exception ex)
            {
                
            }

            return null;
        }

        internal static List<string> GetAllUrls(string url)
        {
            try
            {
                var result =  new List<string>();
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument doc = new HtmlDocument();
                doc = hw.Load(url);
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    string hrefValue = link.GetAttributeValue("href", string.Empty);
                    if (String.IsNullOrEmpty(hrefValue) || hrefValue.ToLower().EndsWith("search/+") || hrefValue.ToLower().EndsWith("search/") || hrefValue.Contains(".js") || hrefValue.Contains(".png") || hrefValue.Contains(".jpg") || hrefValue.Contains(".css") || hrefValue.StartsWith("#") || hrefValue.StartsWith("https://www.facebook.com/sharer") || hrefValue.StartsWith("https://twitter.com/intent") || hrefValue.StartsWith("https://plus.google.com/share") || hrefValue.Contains("mailto") || hrefValue.Contains("tel"))
                    {

                    }
                    else
                    {
                        result.Add(hrefValue);
                    }

                }

                return result;
            }
            catch(Exception ex)
            {

            }

            return null;
        }


    }
}
