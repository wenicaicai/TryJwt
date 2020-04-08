using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Server.Services
{
    public class AsParallelLinQService : IAsParallelLinQService
    {
        //集合元素的数量 ,数量少时，使用asparallel并不会提升性能
        //int[] _array = Enumerable.Range(0, short.MaxValue).ToArray();
        int[] _array = Enumerable.Range(0, 2).ToArray();
        int counter = 0;

        //
        static object locker = new object();
        public string Print()
        {
            StringBuilder strList = new StringBuilder();
            int runTimes = 10000;
            Stopwatch stopwatchI = new Stopwatch();
            stopwatchI.Start();
            for (var i = 0; i < runTimes; i++)
            {
                SumDefault(_array);
            }
            stopwatchI.Stop();
            Stopwatch stopwatchII = new Stopwatch();
            stopwatchII.Start();
            for (var i = 0; i < runTimes; i++)
            {
                SumParallel(_array);
            }
            stopwatchII.Stop();

            strList.Append($"普通加法循环，耗时：{stopwatchI.Elapsed.TotalMilliseconds}");
            strList.Append($"\n AsParalle,发挥多核处理器优势，耗时：{stopwatchII.Elapsed.TotalMilliseconds}");

            return strList.ToString();
        }

        public int SumDefault(int[] array)
        {
            return array.Sum();
        }

        public int SumParallel(int[] array)
        {
            return array.AsParallel().Sum();
        }

        public string ThreadUnsafe()
        {
            StringBuilder strList = new StringBuilder();
            Func<int, bool> func = IsEvenCounter;
            int[] array = Enumerable.Range(0, 1000).ToArray();

            for (int i = 0; i < 10; i++)
            {
                var result = array.AsParallel().Where(func);
                strList.Append(result.Count().ToString() + "\n");
            }
            return strList.ToString();

        }

        public string ThreadSafe()
        {
            StringBuilder strList = new StringBuilder();
            Func<int, bool> func = IsEvenCounterLock;
            int[] array = Enumerable.Range(0, 1000).ToArray();

            for (int i = 0; i < 10; i++)
            {
                var result = array.AsParallel().Where(func);
                strList.Append(result.Count().ToString() + "\n");
            }
            return strList.ToString();
        }

        public string ThreadDefault()
        {
            StringBuilder strList = new StringBuilder();
            Func<int, bool> func = IsEvenCounter;
            int[] array = Enumerable.Range(0, 1000).ToArray();

            for (int i = 0; i < 10; i++)
            {
                var result = array.Where(func);
                strList.Append(result.Count().ToString() + "\n");
            }
            return strList.ToString();
        }

        bool IsEvenCounter(int value)
        {
            return counter++ % 2 == 0;
        }

        bool IsEvenCounterLock(int value)
        {
            lock (locker)
            {
                return counter++ % 2 == 0;
            }
        }

    }
}
