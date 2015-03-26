using LCL.Specifications;
using System;
using System.Linq.Expressions;

namespace LCL.Tests.Common
{
    /// <summary>
    ///  根据时间进行查询
    /// </summary>
    public class DateMaintenanceSpecification : Specification<User>
    {
        string _dateFlg = "";
        DateTime? _fromDate;
        DateTime? _toDate;
        public DateMaintenanceSpecification(DateTime? fromDate, DateTime? toDate, string dateFlg)
        {
            _fromDate = fromDate ?? DateTime.MinValue;
            _toDate = toDate ?? DateTime.MaxValue;
            _dateFlg = dateFlg;
        }
        public override Expression<Func<User, bool>> GetExpression()
        {
            return c => c.AddDate >= _fromDate && c.AddDate <= _toDate;
        }
    }
    /// <summary>
    ///  根据关键字进行查询
    ///  Linq模糊查询 http://www.cnblogs.com/cracker/archive/2011/02/07/1949669.html
    /// </summary>
    public class KeyMaintenanceSpecification : Specification<User>
    {
        string _keyword = "";
        string _dateFlg = "";
        public KeyMaintenanceSpecification(string keyword, string dateFlg)
        {
            _keyword = keyword;
            _dateFlg = dateFlg;
        }
        public override Expression<Func<User, bool>> GetExpression()
        {
            Expression<Func<User, bool>> spec = c => c.Name.IndexOf(_keyword) != -1; ;
            return spec;
        }
    }
}
