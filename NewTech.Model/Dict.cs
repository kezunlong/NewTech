using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTech.Model
{
    public class Dict
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int Order { get; set; }
        public string Parent { get; set; }
        public bool Status { get; set; }
    }
}
