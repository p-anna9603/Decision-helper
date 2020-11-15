using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionSupport
{
    public class Index
    {
        int product;
        int worker;
        int robot;

        public Index(int prod, int rob, int work)
        {
            product = prod;
            robot = rob;
            worker = work;
        }

        public int Product { get => product; set => product = value; }
        public int Worker { get => worker; set => worker = value; }
        public int Robot { get => robot; set => robot = value; }
    }
}
