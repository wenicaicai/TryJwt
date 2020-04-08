using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWT.Server.Services
{
    /// <summary>
    /// 累加
    /// </summary>
    public class AggregateLinQService : IAggregateLinQService
    {
        private int[] _array = Enumerable.Range(2, 64).ToArray();
        int[] array = { 1, 2, 3, 4, 5 };
        public string AggregateSum()
        {
            StringBuilder strList = new StringBuilder();
            var resultI = _array.Aggregate((a, b) => a + b);
            strList.Append("累加，结果："+ resultI.ToString());
            var resultII = _array.Aggregate((a, b) => b * a);
            strList.Append("\n 累乘，结果：" + resultII.ToString());
            return strList.ToString();
        }

    }
}
