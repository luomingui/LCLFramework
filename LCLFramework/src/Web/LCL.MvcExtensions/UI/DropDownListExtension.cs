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
    public static class DropDownListExtension
    {



        //public static IHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression, object htmlAttribute)
        //{
        //    var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

        //    var enumType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;

        //    var enumValues = Enum.GetValues(enumType).Cast<object>();

        //    var items = from enumValue in enumValues
        //                select new SelectListItem
        //                {
        //                    Text = enumValue.ToString(),
        //                    Value = ((int)enumValue).ToString(),
        //                    Selected = enumValue.Equals(metadata.Model)
        //                };
        //    return html.DropDownListFor(expression, items, string.Empty, htmlAttribute);
        //}



    }


   

}
