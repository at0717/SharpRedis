using System.Collections;
using System.Collections.Generic;

namespace SharpRedis
{
    public class PageList<T>: IEnumerable<T>
    {
        public IEnumerable<T> Data { get; private set; }
        public long Total { get; private set; }
        public int PageSize { get; private set; }
        public long PageIndex { get; private set; }

        public PageList(IEnumerable<T> data, long total, int pageSize, long pageIndex)
        {
            Data = data;
            Total = total;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}