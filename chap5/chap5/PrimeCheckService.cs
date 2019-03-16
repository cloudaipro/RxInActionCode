using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chap5
{
    class PrimeCheckService
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);
        public virtual async Task<bool> IsPrimeAsync(int number)
        {
            //Console.WriteLine("[[" + number);
            //int delay = random.Next() % 1000;
            //Console.WriteLine(number + " delay: " + delay);
            //await Task.Delay(delay);
            return await Task.Run(async () =>
            {
                int delay = random.Next() % 1000;
                Console.WriteLine(number + " delay: " + delay);
                await Task.Delay(delay);
                for (int j = 2; j <= Math.Sqrt(number); j++)
                {
                    //Console.WriteLine("::" + j);
                    if (number % j == 0)
                    {
                        Console.WriteLine("::FALSE:" + number);
                        return false;
                    }
                }
                Console.WriteLine("**TRUE:" + number);
                return true;
            });
        }
        //public virtual Task<bool> IsPrimeAsync(int number)
        //{
        //    //Console.WriteLine("[[" + number);
        //    return Task.Run(() =>
        //    {
        //        for (int j = 2; j <= Math.Sqrt(number); j++)
        //        {
        //            //Console.WriteLine("::" + j);
        //            if (number % j == 0)
        //                return false;
        //        }
        //        //Console.WriteLine("**" + number);
        //        return true;
        //    });
        //}
    }
}
