using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerCodeGenerator
{
    public static class StringExtend
    {
        /// <summary>
        /// 驼峰命名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Hump(this string str)
        {
            return str.ReplaceFirst(str.First().ToString(), str.First().ToLower().ToString());
        }
    }
}
