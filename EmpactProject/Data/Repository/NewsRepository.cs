using EmpactProject.Data.Model;
using EmpactProject.Model;
using System.Globalization;
using System.Net;
using System.Xml;
using System.Xml.Serialization;

namespace EmpactProject.Data.Repository
{
    public class NewsRepository : INewsRepository
    {
        private const string _url = "https://rss.nytimes.com/services/xml/rss/nyt/World.xml";
        public List<News> NewsList { get; set; }
        private readonly AppDbContext _dbContext;

        public NewsRepository(AppDbContext appDbContext)
        {
            NewsList = new List<News>();
            SerializeXML();
            _dbContext = appDbContext;
        }

        public void SaveListToDb(List<News> news)
        {
            foreach(News item in news) 
            {
                _dbContext.News.Add(item);
            }
        }
        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
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

