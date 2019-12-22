using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam1DZ
{
    public class List
    {
        public static void SWAP<T> (ref T x,ref T y)
        {
            T temp = x;
            x = y;
            y = temp;
        }
    }
}
