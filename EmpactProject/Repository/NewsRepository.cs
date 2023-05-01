using EmpactProject.Model;
using EmpactProject.Services;
using System;
using System.Globalization;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace EmpactProject.Repository
{
    public class NewsRepository : INewsRepository
    {
        private const string _url = "https://rss.nytimes.com/services/xml/rss/nyt/World.xml";
        public List<News> NewsList { get; set; }

        public NewsRepository()
        {
           NewsList = new List<News>();
           SerializeXML(); 
        }

        public async Task SerializeXML()
        {
            using (HttpClient client = new HttpClient())
            {
                using (Stream stream = await client.GetStreamAsync(_url))
                {
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        reader.ReadToFollowing("item");
                        do
                        {
                            XmlReader newsReader = reader.ReadSubtree();
                            News newsItem = new();

                            while (newsReader.Read())
                            {
                                if (newsReader.NodeType == XmlNodeType.Element)
                                {
                                    switch (newsReader.Name)
                                    {
                                        case "title":
                                            newsItem.Title = newsReader.ReadElementContentAsString();
                                            break;
                                        case "description":
                                            newsItem.Description = newsReader.ReadElementContentAsString();
                                            break;
                                        case "link":
                                            newsItem.Link = newsReader.ReadElementContentAsString();
                                            break;
                                        case "pubDate":
                                            string pubDateString = newsReader.ReadElementContentAsString();
                                            newsItem.PublicationDate = DateTime.ParseExact(
                                                pubDateString,
                                                "ddd, dd MMM yyyy HH:mm:ss zzz",
                                                CultureInfo.InvariantCulture);
                                            break;
                                    }
                                }
                            }

                            NewsList.Add(newsItem);
                        } while (reader.ReadToFollowing("item"));
                    }
                }
            }
        }


    }
}

