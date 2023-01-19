using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTest
{
    public class LeetTest
    {

        //return count of max subarray - LeetCode 53
        public int MaxSubArray(int[] nums)
        {
            Dictionary<int, int> dicta = new Dictionary<int, int>();

            int maxsum = nums[0];
            int currsum = 0;

            //needs to be contiguous
            foreach (var item in nums)
            {
                if (currsum < 0)
                {
                    currsum = 0;
                }
                currsum = currsum + item;
                maxsum = Math.Max(maxsum, currsum);


            }


            return maxsum;
        }



        /// <summary>
        /// Two sum 2,4,5,7,11, 15 
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int[] TwoSum(int[] numbers, int target)
        {
            int[] ns = new int[2];

            Dictionary<int, int> dicta = new Dictionary<int, int>();
            //add to inext if number does  not add

            for (int i = 0; i < numbers.Length; i++)
            {
                int diff = target - numbers[i];

                if (dicta.ContainsKey(diff))
                {
                    //found the value
                    ns[0] = dicta[diff] + 1;
                    ns[1] = i + 1;
                    break;
                }
                else
                {
                    if (!dicta.ContainsKey(numbers[i]))
                    {
                        dicta.Add(numbers[i], i);

                    }
                }
            }

            return ns;
        }

        /// <summary>
        /// Rob house leet 198
        /// 2,7,9,3,1
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int Rob(int[] nums)
        {
            int val1 = 0;
            int val2 = 0;

            foreach (var item in nums)
            {
                int temp = Math.Max(item + val1, val2);
                val1 = val2;
                val2 = temp;

            }
            return val2;

        }


        public LinkedListNode<int> MergeTwoLists(LinkedListNode<int> list1, LinkedListNode<int> list2)
        {

            LinkedListNode<int> lok = new LinkedListNode<int>(list1.List.Count + list2.List.Count);

            //lok.List.a

            return lok;

        }

        public int MaxProfit(int[] prices)
        {

            int maxprof = 0;
            int r = 0;
            int i = 1;
            List<int> li = prices.ToList() ;
            li.Reverse();


            while (i < li.Count())
            {
                if (li[r] > li[i])
                {
                    int prof = li[r] - li[i];
                    maxprof = Math.Max(prof, maxprof);
                }
                else
                {
                    r=i;
                }

                i = i + 1;
            }


            return maxprof;
        }

        public int ClimbStairs(int n)
        {
            //n = 4;
            int prevval = 1;
            int prevprevval = 0;
            int maxsteps = 0;

            for(int i = 1; i <= n; i++)
            {
                
                maxsteps = prevval + prevprevval;               
                prevprevval = prevval;
                prevval = maxsteps;
                if (prevprevval < 0)
                {
                    prevprevval = 0;
                }
            }

            return maxsteps;

        }

        public bool IsHappy(int n)
        {

            int xnum = 0;
            int sqnum = 0;
            bool hap = true;
            HashSet<int> happy = new HashSet<int>();

            //Mode of n
            while (n > 0)
            {
                sqnum = n;
                int sunsuqare = 0;

                while (sqnum >= 10)
                {
                    xnum = sqnum % 10;
                    sqnum = sqnum / 10;
                    sunsuqare = sunsuqare + (xnum * xnum);
                }

                if (sqnum < 10)
                {
                    sunsuqare = sunsuqare + sqnum * sqnum;
                }

                n = sunsuqare;
                if (happy.Contains(sunsuqare))
                {
                    if(sunsuqare == 1)
                    {
                        hap = true;
                    }
                    else
                    {
                        hap = false;
                    }
               
                    break;
                }
                else
                {
                    happy.Add(sunsuqare);
                }

            }
            
            return hap;
        }




    }
}
