using System;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LCL.MvcExtensions
{
    public static class ExSelectListItem
    {
        public static List<SelectListItem> ToSelectListItem(this Enum valueEnum)
        {
            return (from int value in Enum.GetValues(valueEnum.GetType())
                    select new SelectListItem
                    {
                        Text = Enum.GetName(valueEnum.GetType(), value),
                        Value = value.ToString()
                    }).ToList();
        }

        public static List<SelectListItem> ToSelectListItem(this Enum valueEnum, string selectName)
        {
            return (from int value in Enum.GetValues(valueEnum.GetType())
                    select new SelectListItem
                    {
                        Text = Enum.GetName(valueEnum.GetType(), value),
                        Value = Enum.GetName(valueEnum.GetType(), value),
                        Selected = Enum.GetName(valueEnum.GetType(), value) == selectName ? true : false
                    }).ToList();
        }
    }



}
