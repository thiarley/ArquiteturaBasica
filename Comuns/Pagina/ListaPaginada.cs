using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArquiteturaBasica.Pagina
{
    public class ListaPaginada<T> : List<T>, IListaPaginada<T>
    {
        public ListaPaginada(IList<T> source, int pageIndex, int pageSize, int total)
        {
            this.TotalCount = total;

            if (pageSize > 0)
                this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
            {
                TotalPages++;
            }

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;

            this.AddRange(source);
        }


        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }
    }
}
