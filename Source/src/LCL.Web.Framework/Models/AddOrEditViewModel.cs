using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Web;

namespace LCL
{
    /// <summary>
    /// 视图模型 添加/修改
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class AddOrEditViewModel<TModel> 
    {
        public AddOrEditViewModel()
        {
            CurrentPageNum = 0;
            PageSize = 10;
        }
        public int CurrentPageNum { get; set; }
        public int PageSize { get; set; }
        public bool Added { get; set; }
        public TModel Entity { get; set; }
    } 
} 
