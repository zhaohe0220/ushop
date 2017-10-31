using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PageBar
    {
        public static string GetPageBar(string pid, int pageIndex, int pageCount)
        {
            if (pageCount == 1)
            {
                return string.Empty;
            }
            //页面共10页
            int start = pageIndex - 5;//起始位置
            // start = start < 1 ? 1 : start;
            if (start < 1)
            {
                start = 1;
            }
            int end = start + 9;
            if (end > pageCount)
            {
                end = pageCount;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                if (i == pageIndex)
                {
                    sb.Append(i);
                }
                else
                {
                    //sb.Append(string.Format("<a href='?id={0}&pageIndex={1}'>首页</a>",pid, 1));
                sb.Append(string.Format("<a href='?id={0}&pageIndex={1}'>{1}</a>", pid, i));
                }
            }
            return sb.ToString();
        }
    }
}
