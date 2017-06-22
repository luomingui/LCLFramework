using LCL.Core;
using LCL.Core.Domain.Entities;
using LCL.Core.Domain.Repositories;
using LCL.Core.Domain.Specifications;
using LCL.Core.Infrastructure;
using LCL.Web.Framework.Models;
using LCL.Web.Framework.Mvc;
using LCL.Web.Framework.Security;
using LCL.Web.Framework.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LCL.Web.Framework.Controllers
{

    public class LController<TAggregateRoot> : LController where TAggregateRoot : class,IAggregateRoot
    {
        #region Ajax CURD
        public virtual LJsonResult AjaxGetBy()
        {
            var modelList = this.Repository<TAggregateRoot>().FindAll().ToList();

            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = modelList;

            return json;
        }
        public virtual LJsonResult EasyUIAjaxGetPage(int? page, int? rows)
        {
            /*
   1、easyui-datagrid 分页接收参数
   page 接受客户端的页码，对应的就是用户选择或输入的pageNumber（按照上图的例子，用户点了下一页，传到服务器端就是2）
   rows 接受客户端的每页记录数，对应的就是pageSize  （用户在下拉列表选择每页显示30条记录，传到服务器就是30）
                */
            if (!page.HasValue) page = 1;
            if (!rows.HasValue) rows =10;
            int pageNumber = page.Value;
            int pageSize = rows.Value;

            var modelList = this.Repository<TAggregateRoot>().FindAll(p => p.Id, SortOrder.Descending, pageNumber, pageSize);

            var gridModel = new EasyUIGridModel<TAggregateRoot>();
            if (modelList != null)
            {
                gridModel.total = modelList.Count;
                gridModel.rows = modelList.Data.ToList<TAggregateRoot>();
            }
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = gridModel;

            return json;
        }
        public virtual LJsonResult AjaxGetByPage(int? page, int? rows)
        {
            if (!page.HasValue) page = 1;
            if (!rows.HasValue) rows = 10;
            int pageNumber = page.Value;
            int pageSize = rows.Value;
            var modelList = this.Repository<TAggregateRoot>().FindAll(p => p.Id, SortOrder.Descending, pageNumber, pageSize);

            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = modelList;

            return json;
        }
        [HttpPost]
        public virtual LJsonResult AjaxAdd(TAggregateRoot model)
        {
            var result = new LResult(true);
            try
            {
                var customerRepository = this.Repository<TAggregateRoot>();
                customerRepository.Add(model);
                customerRepository.Context.Commit();
                customerRepository.Context.Dispose();
            }
            catch (Exception)
            {
                result = new LResult(false);
            }

            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;

            return json;
        }
        [HttpPost]
        public virtual LJsonResult AjaxDelete(TAggregateRoot model)
        {
            var result = new LResult(true);
            try
            {
                var customerRepository = this.Repository<TAggregateRoot>();

                var entity = customerRepository.GetByKey(model.Id);
                if (entity != null)
                    customerRepository.Remove(entity);
                customerRepository.Context.Commit();
                customerRepository.Context.Dispose();
            }
            catch (Exception)
            {
                result = new LResult(false);
            }
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        [HttpPost]
        public virtual LJsonResult AjaxEdit(TAggregateRoot model)
        {
            var result = new LResult(true);
            try
            {
                this.Repository<TAggregateRoot>().Update(model);
            }
            catch (Exception)
            {
                result = new LResult(false);
            }
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        [HttpPost]
        public virtual LJsonResult AjaxDeleteList(IList<int> idList)
        {
            var result = new LResult(true);
            try
            {
                for (int i = 0; i < idList.Count; i++)
                {
                    var entity = this.Repository<TAggregateRoot>().GetByKey(idList[i]);
                    this.Repository<TAggregateRoot>().Remove(entity);
                }
            }
            catch (Exception)
            {
                result = new LResult(false);
            }
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        public virtual LJsonResult AjaxGetByModel(int id)
        {
            var result = new LResult(true);
            try
            {
                var model = this.Repository<TAggregateRoot>().GetByKey(id);
                result.Data = model;
            }
            catch (Exception)
            {
                result = new LResult(false);
            }
            var json = new LJsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            json.Data = result;
            return json;
        }
        #endregion
    }
}
