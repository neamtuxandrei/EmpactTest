using System.ComponentModel.DataAnnotations;

namespace EmpactProject.Data.Model
{
    public class News
    {
        public int Id { get; set; }
        public string GuidLink { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public DateTime? PublicationDate { get; set; }

        public int PublisherId { get; set; } // foreign key property
        public Publisher Publisher { get; set; } = null!; // navigation reference

        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
