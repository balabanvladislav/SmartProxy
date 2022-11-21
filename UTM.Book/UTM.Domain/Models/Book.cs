using UTM.Domain.Enums;
using UTM.Repository.MongoDB.Models;

namespace UTM.Domain.Models
{
    public class Book : MongoDocument
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<string> Authors { get; set; } = new();

        public List<Genre> Genres { get; set; } = new();
    }
}
