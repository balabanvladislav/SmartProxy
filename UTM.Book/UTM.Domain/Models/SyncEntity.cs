using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTM.Domain.Models
{
    public class SyncEntity
    {
        public Guid Id { get; set; }
        public DateTime LastChangedAt { get; set; }
        public string JsonData { get; set; } = string.Empty;
        public string SyncType { get; set; } = string.Empty;
        public string ObjectType { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
    }
}
