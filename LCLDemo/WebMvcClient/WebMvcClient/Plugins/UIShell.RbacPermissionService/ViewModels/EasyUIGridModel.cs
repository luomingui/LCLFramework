using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.MvcExtensions
{
    public class EasyUIGridModel<TModel>
    {
        public int total { get; set; }
        public List<TModel> rows { get; set; }
    }
}
