using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCL
{
    public class PagedResult<TModel>
    {
        public const int DefaultPageSize = 10;
        public int TotalCount { get; set; }
        private int totalPages;
        public int TotalPages
        {
            get { return totalPages; }
            set { totalPages = value; }
        }
        public int PageSize { get; set; }
        public int CurrentPageNum { get; set; }
        public int PageCount { get; set; }
        public bool HasPreviousPage
        {
            get
            {
                return CurrentPageNum >= 1;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return CurrentPageNum <= PageCount;
            }
        }
        public List<TModel> PagedModels { get; set; }
        public List<int> PageSizeList { get; private set; }
        /// <summary> 
        /// 从数据库获取分页后的数据，然后设置到PagedListViewModel。 
        /// </summary> 
        /// <param name="totalCount">总数。</param> 
        /// <param name="pageSize">每页大小。</param> 
        /// <param name="currentPageNum">当前页序号。</param> 
        /// <param name="pagedModels">当前页对象列表。</param> 
        public PagedResult(int? totalCount, int? totalPages, int? pageSize, int? pageNumber, List<TModel> pagedModels)
        {
            TotalCount = totalCount.Value;
            PageSize = pageSize.Value;
            TotalPages = totalPages.Value;
            PageSizeList = new List<int>();
            ResetPageSizeList(totalCount.Value);
            UpdatePageBySpecifiedPagedModels(totalCount.Value, pageSize.Value, pageNumber.Value, pagedModels);
        }
        /// <summary> 
        /// 从数据库获取分页后的数据，然后设置到PagedListViewModel。 
        /// </summary> 
        /// <param name="totalCount">总数。</param> 
        /// <param name="pageSize">每页大小。</param> 
        /// <param name="currentPageNum">当前页序号。</param> 
        /// <param name="pagedModels">当前页对象列表。</param> 
        public PagedResult(int totalCount, int currentPageNum, int pageSize, List<TModel> pagedModels)
        {
            TotalCount = totalCount;
            TotalPages = (totalCount + pageSize - 1) / pageSize;
            PageSize = pageSize;
            PageSizeList = new List<int>();
            ResetPageSizeList(totalCount);
            UpdatePageBySpecifiedPagedModels(totalCount, pageSize, currentPageNum, pagedModels);
        }
        /// <summary> 
        /// 获取所有实例，由PagedListViewModel进行分页。 
        /// </summary> 
        /// <param name="pageSize">每页大小。</param> 
        /// <param name="currentPageNum">当前页序号。</param> 
        /// <param name="allModels">所有实例。</param> 
        public PagedResult(int currentPageNum, int pageSize, List<TModel> allModels)
        {
            PageSize = pageSize;
            TotalPages = (allModels.Count + pageSize - 1) / pageSize;
            PageSizeList = new List<int>();
            ResetPageSizeList(allModels.Count);
            UpdatePageByAllModels(pageSize, currentPageNum, allModels);
        }
        private void ResetPageSizeList(int totalCount)
        {
            PageSizeList.Clear();

            AddPageSize(DefaultPageSize);

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
        public void UpdatePageBySpecifiedPagedModels(int totalCount, int pageSize, int currentPageNum, List<TModel> pagedModels)
        {
            if (!PageSizeList.Contains(pageSize))
            {
                PageSizeList.Insert(0, pageSize);
            }

            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPageNum = currentPageNum;

            PageCount = TotalCount % PageSize == 0 ? TotalCount / PageSize : TotalCount / PageSize + 1;

            PagedModels = pagedModels;
        }
        public void UpdatePageByAllModels(int pageSize, int currentPageNum, List<TModel> allModels)
        {
            if (!PageSizeList.Contains(pageSize))
            {
                PageSizeList.Insert(0, pageSize);
            }

            TotalCount = allModels.Count;
            PageSize = pageSize;
            CurrentPageNum = currentPageNum;

            PageCount = TotalCount % PageSize == 0 ? TotalCount / PageSize : TotalCount / PageSize + 1;

            if (CurrentPageNum <= 1)
            {
                CurrentPageNum = 1;
            }
            if (CurrentPageNum >= PageCount)
            {
                CurrentPageNum = PageCount;
            }

            PagedModels = allModels.Skip((CurrentPageNum - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
