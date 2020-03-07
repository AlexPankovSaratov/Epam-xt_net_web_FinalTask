using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Comment
    {
        public int ID { get; set; }
        public string Author { get; set; }
        public int PhotoID { get; set; }
        public string TextComment { get; set; }
    }
}
