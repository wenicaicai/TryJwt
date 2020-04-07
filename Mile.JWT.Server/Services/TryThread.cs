using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JWT.Server.Services
{
    public class TryThread
    {
        public static string TaskI()
        {
            Thread.Sleep(1000);
            return "TaskI sleeps 1s";
        }

        public static string TaskII()
        {
            Thread.Sleep(2000);
            return "TaskII sleeps 2s";
        }
    }

    //Task默认使用线程池
    //Thread比较耗费资源
    //Task 简化了异步编程的方式


    public class ThreadPool
    {
        public async Task<string> GetThreadId()
        {
            StringBuilder strList = new StringBuilder();
            var info = string.Format("线程I，ID:{0}",Thread.CurrentThread.ManagedThreadId);
            strList.Append(info);
            await Task.Delay(500);
            var infoResult = TaskCaller().Result;
            strList.Append(infoResult);
            return strList.ToString();
        }

        private async Task<string> TaskCaller()
        {
            await Task.Delay(1000);
            return string.Format("线程II,Id：{0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
