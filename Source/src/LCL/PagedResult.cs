using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL
{
    public class PagedResult<T> : ICollection<T>
    {
        #region Ctor
        public PagedResult()
        {
            this._data = new List<T>();
        }
        public PagedResult(int? pageSize, int? pageNumber, IList<T> data)
        {
            this.pageSize = pageSize;
            this.pageNumber = pageNumber;
            this._data = data;

            TotalPages = (data.Count + pageSize - 1) / pageSize;
            PageSizeList = new List<int>();
            ResetPageSizeList(data.Count);
            UpdatePageByAllModels(pageSize.Value, pageNumber.Value, data);

        }
        /// <summary>
        /// 初始化一个新的实例
        /// </summary>
        /// <param name="totalRecords">记录总数</param>
        /// <param name="totalPages">总页数</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="data">当前页对象列表</param>
        public PagedResult(int? totalRecords, int? totalPages, int? pageSize, int? pageNumber, IList<T> data)
        {
            this.totalRecords = totalRecords;
            this.totalPages = totalPages;
            this.pageSize = pageSize;
            this.pageNumber = pageNumber;
            this._data = data;

            PageSizeList = new List<int>();
            ResetPageSizeList(totalRecords.Value);
            UpdatePageBySpecifiedPagedModels(totalRecords.Value, pageSize.Value, pageNumber.Value, data);
        }
        #endregion

        #region MyRegion
        private void ResetPageSizeList(int totalCount)
        {
           
            AddPageSize(PageSize.Value);
            if (totalCount > 10)
            {
                AddPageSize(10);
            }
            if (totalCount > 20)
            {
                AddPageSize(20);
            }
            if (totalCount > 50)
            {
                AddPageSize(50);
            }
            if (totalCount > 100)
            {
                AddPageSize(100);
            }
            AddPageSize(totalCount);
        }
        private void AddPageSize(int pageSize)
        {
            if (!PageSizeList.Contains(pageSize))
            {
                PageSizeList.Add(pageSize);
            }
        }
        public void UpdatePageBySpecifiedPagedModels(int totalCount, int pageSize, int currentPageNum, IList<T> data)
        {
            if (!PageSizeList.Contains(pageSize))
            {
                PageSizeList.Insert(0, pageSize);
            }

            TotalRecords = totalCount;
            PageSize = pageSize;

            pageNumber = currentPageNum;
            totalPages = TotalRecords % PageSize == 0 ? TotalRecords / PageSize : TotalRecords / PageSize + 1;
            this._data=data;
        }
        public void UpdatePageByAllModels(int pageSize, int currentPageNum, IList<T> data)
        {
            if (!PageSizeList.Contains(pageSize))
            {
                PageSizeList.Insert(0, pageSize);
            }
            TotalRecords = data.Count;
            PageSize = pageSize;
            pageNumber = currentPageNum;
            totalPages = TotalRecords % PageSize == 0 ? TotalRecords / PageSize : TotalRecords / PageSize + 1;
            if (pageNumber <= 1)
            {
                pageNumber = 1;
            }
            if (pageNumber >= totalPages)
            {
                pageNumber = totalPages;
            }

            this._data = data.Skip((pageNumber.Value - 1) * PageSize.Value).Take(PageSize.Value).ToList();
        }
        #endregion

        #region Public Properties
        public List<int> PageSizeList { get; private set; }
        private int? totalRecords;
        public int? TotalRecords
        {
            get { return totalRecords; }
            set { totalRecords = value; }
        }

        private int? totalPages;
        public int? TotalPages
        {
            get { return totalPages; }
            set { totalPages = value; }
        }

        private int? pageSize;
        public int? PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int? pageNumber;
        public int? PageNumber
        {
            get { return pageNumber; }
            set { pageNumber = value; }
        }

        private IList<T> _data;
        public IList<T> Data
        {
            get { return _data; }
        }
        #endregion

        #region ICollection<T> Members
        public void Add(T item)
        {
            _data.Add(item);
        }
        public void Clear()
        {
            _data.Clear();
        }
        public bool Contains(T item)
        {
            return _data.Contains(item);
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            _data.CopyTo(array, arrayIndex);
        }
        public int Count
        {
            get { return _data.Count; }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }
        public bool Remove(T item)
        {
            return _data.Remove(item);
        }
        #endregion

        #region IEnumerable<T> Members
        public IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        #endregion
    }
}
