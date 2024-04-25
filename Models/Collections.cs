using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemKolekcjonerstwa.Models
{
    public class Collections
    {
        public string CollectionType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 

        public int Count { get; set; }
        public object Id { get; internal set; }
    }
}
