//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Mvc;

//namespace LCL.MvcExtensions
//{
	/// <summary>
	/// Versions: V 1.0 版
	/// Author: 罗敏贵
	/// E-mail: minguiluo@163.com
	/// Blogs： http://www.cnblogs.com/luomingui
	/// CreateDate: 2014-9-25  星期四 14:04:37
	/// 
	/// <summary>

//    /// <summary>
//    /// http://www.cnblogs.com/024hi/archive/2010/12/22/1913691.html
//    /// </summary>
//    public class ChartResult : ActionResult
//    {
//        private readonly Chart _chart;
//        private readonly string _format;

//        public ChartResult(Chart chart, string format = "png")
//        {
//            if (chart == null)
//                throw new ArgumentNullException("chart");

//            _chart = chart;
//            _format = format;

//            if (string.IsNullOrEmpty(_format))
//                _format = "png";
//        }

//        public Chart Chart
//        {
//            get { return _chart; }
//        }

//        public string Format
//        {
//            get { return _format; }
//        }

//        public override void ExecuteResult(ControllerContext context)
//        {
//            _chart.Write(_format);
//        }
//    }
//}
