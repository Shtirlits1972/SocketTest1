using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketTest1
{
    public class Wshello
    {
        public  string type { get; set; }
        public  channel [] channels { get; set; }
    }


    public class channel
    {
        public  string name { get; set; }
        public  string[] product_ids { get; set; }
    }
}
