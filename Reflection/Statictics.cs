using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    public class Statictics
    {
        public Dictionary<int, string> statictics;
        public Statictics() { 
            this.statictics = new Dictionary<int, string>();
        }
        public void addStatictics(int iter, string metrics)
        {
            this.statictics.Add(iter, metrics);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this.statictics)
            {
                sb.Append($" Step {item.Key} - time {item.Value} \n");
            }
            return sb.ToString();
        }

    }


}
