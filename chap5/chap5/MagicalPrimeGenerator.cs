using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chap5
{
    class MagicalPrimeGenerator
    {
        public int startOff = 100000;


        public async Task<IReadOnlyCollection<int>> GenerateAsync(int amount)
        {
            return await Task.Run(() => Generate(amount).ToList().AsReadOnly());

        }

        public IEnumerable<int> Generate(int amount)
        {

            foreach(int prime in GeneratePrime())
            {
                yield return prime;
                if (--amount <= 0)
                    break;
            }

        }

        public IEnumerable<int> GeneratePrime()
        {
            IList<int> primes = new List<int>();
            primes.Add(2);
            yield return 2;
            int curTest = 3;
            while (true)
            {
                int sqrt = (int)Math.Sqrt(curTest);
                bool isPrime = true;
                for (int i = 0; i < primes.Count && primes[i] <= sqrt; i++)
                    if (curTest % primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                if (isPrime)
                {
                    primes.Add(curTest);
                    yield return curTest;
                }
                curTest += 2;
            }

        }

        //public IEnumerable<int> GenerateAsync(int amount)
        //{
        //    amount += startOff;
        //    IList<int> rtnPrimes = new List<int>();
        //    IList<int> primes = new List<int>();
        //    primes.Add(2);

        //    int curTest = 3;
        //    while(primes.Count < amount)
        //    {
        //        int sqrt = (int)Math.Sqrt(curTest);
        //        bool isPrime = true;
        //        for (int i = 0; i < primes.Count && primes[i] <= sqrt; i++)
        //            if (curTest % primes[i] == 0)
        //            {
        //                isPrime = false;
        //                break;
        //            }
        //        if (isPrime)
        //        {
        //            primes.Add(curTest);
        //            if (primes.Count > startOff)
        //            {
        //                rtnPrimes.Add(curTest);
        //                Console.WriteLine("GenerateAsync@" + curTest);
        //                yield return curTest;
        //            }
        //        }
        //        curTest += 2;
        //    } 
        //}
        //public IEnumerable<int> Generate(int amount)
        //{
        //    amount += startOff;
        //    IList<int> rtnPrimes = new List<int>();
        //    IList<int> primes = new List<int>();
        //    primes.Add(2);

        //    int curTest = 3;
        //    while (primes.Count < amount)
        //    {
        //        int sqrt = (int)Math.Sqrt(curTest);
        //        bool isPrime = true;
        //        for (int i = 0; i < primes.Count && primes[i] <= sqrt; i++)
        //            if (curTest % primes[i] == 0)
        //            {
        //                isPrime = false;
        //                break;
        //            }
        //        if (isPrime)
        //        {
        //            primes.Add(curTest);
        //            if (primes.Count > startOff)
        //            {
        //                rtnPrimes.Add(curTest);
        //            }
        //        }
        //        curTest += 2;
        //    }
        //    return rtnPrimes;
        //}
    }
}
