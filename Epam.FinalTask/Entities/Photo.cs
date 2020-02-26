using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Country { get; set; }
        public byte[] Image { get; set; }
        public int AuthorId { get; set; }
        public HashSet<int> LikeUsersList { get; set; } = new HashSet<int>();
    }
}
