using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTM.Domain.Enums;

namespace UTM.Domain.Models
{
    public class BookOut
    {
        public Guid Id { get; set; }
        public DateTime LastChangedAt { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<string> Authors { get; set; } = new();

        public List<Genre> Genres { get; set; } = new();
    }
}
