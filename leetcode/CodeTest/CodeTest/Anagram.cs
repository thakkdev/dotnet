using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTest
{
    public class Anagram
    {

        public void Solution(string s , string t)
        {
            
            if(s.Length != t.Length)
            {
                Console.WriteLine("False");
                return;
            }
            List<char> schars = s.ToList();
            schars.Sort();

            List<char> tchars = t.ToList();
            tchars.Sort();

            for(int i=0; i< s.Length; i++)
            {
                if(schars[i] != tchars[i])
                {
                    Console.WriteLine("False");
                    return;
                }
            }

            Console.WriteLine("True");


        }
    }
}
