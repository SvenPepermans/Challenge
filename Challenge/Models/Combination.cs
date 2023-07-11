using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Models
{
    public class Combination
    {
        public string CombinationWord { get; set; } = "";
        public List<string> SubWords { get; set; } = new List<string>();
    }
}
