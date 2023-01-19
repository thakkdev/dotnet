using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTest
{
    public class Pattern
    {
        /// <summary>
        /// Given a random k between 1 to 100
        /// identify lows within an array of low/num/high int values
        /// the lows identified as 3 lows form a triangle with 2 sdies eqaul
        /// And less the  the high-k values for all lows
        /// Each array is in order
        /// num is a random value between low and high
        /// If 2 sides not equal for any then return empty
        /// if lows are not less the high-k then return empty
        /// 
        /// ex i[0] = low; i[1] = mew; i[2] = high
        /// //minimum three aeeay objects or count of 3s in the list
        /// [1,2,3][2,3,4][3,4,5]
        /// Lows = 1,2,3 2 sides not equal then return empty
        /// 
        /// [1,2,6][1,5,9][3,4,5] and k=1
        /// Lows = 1,1,3 => 2 sides are equal
        /// Triangle low = 6-1=5 > 1 ; 9-1=8>1; 5-1=4>3
        /// Return  Array - {[1,2,6][1,5,9][3,4,5]}
        /// 
        /// </summary>
        public void solution()
        {




        }
    }
}
