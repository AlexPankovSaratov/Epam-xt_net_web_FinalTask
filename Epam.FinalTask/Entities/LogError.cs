using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LogError
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}
