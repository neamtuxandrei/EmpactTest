using EmpactProject.Model;
using System;
using System.Globalization;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace EmpactProject.Repository
{
    public class NewsRepository : INewsRepository
    {
        private string _url = "https://rss.nytimes.com/services/xml/rss/nyt/World.xml";
        private List<News> _news;

        public NewsRepository()
        {
            _news = new List<News>();
            SerializeXML();
        }

        public List<News> GetNews()
        {
            return _news;
        }

        private void SerializeXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(News), new XmlRootAttribute("item"));
            using (WebClient client = new())
            {
                using (XmlReader reader = XmlReader.Create(client.OpenRead(_url)))
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

                        _news.Add(newsItem);
                    } while (reader.ReadToFollowing("item"));
                }
            }
        }
    }
}

