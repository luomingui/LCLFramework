using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace LCL.Repositories.XML
{
    /// <summary>
    /// Represents the Entity Framework repository context.
    /// XML结构为Attribute
    /// </summary>
    public class XMLRepositoryContext : RepositoryContext, IXMLRepositoryContext
    {
        #region Private Fields
        static object lockObj = new object();
        private readonly XDocument xmlContext;
        private readonly object sync = new object();
        #endregion

        #region Ctor
        public XMLRepositoryContext()
        {
            this.xmlContext = new XDocument();
        }
        public XMLRepositoryContext(XDocument efContext)
        {
            this.xmlContext = efContext;
        }
        #endregion



        #region IXMLRepositoryContext Members
        public XDocument Context
        {
            get { return this.xmlContext; }
        }
        string _xmlfilesavepath = "";
        public string XmlFileSavePath
        {
            get { return _xmlfilesavepath; }
        }

        public IQueryable<TAggregateRoot> GetModel<TAggregateRoot>() where TAggregateRoot : class, IAggregateRoot
        {
            IEnumerable<XElement> list = this.xmlContext.Root.Elements(typeof(TAggregateRoot).Name);
            IList<TAggregateRoot> returnList = new List<TAggregateRoot>();
            foreach (var item in list)
            {
                TAggregateRoot entity = null;
                foreach (var member in typeof(TAggregateRoot).GetType()
                                             .GetProperties()
                                             .Where(i => i.PropertyType.IsValueType
                                                 || i.PropertyType == typeof(String)))//只找简单类型的属性
                {
                    if (item.Attribute(member.Name) != null)
                        member.SetValue(entity, Convert.ChangeType(item.Attribute(member.Name).Value, member.PropertyType), null);//动态转换为指定类型
                }
                returnList.Add(entity);
            }
            return returnList.AsQueryable();
        }


        #endregion

        #region IRepositoryContext Members
        public override void RegisterNew(object obj)
        {
            if (obj == null)
                throw new ArgumentException("The database entity can not be null.");

            this._xmlfilesavepath = AppDomain.CurrentDomain.BaseDirectory + "/" + obj.GetType().Name + ".xml";

            XElement db = new XElement(obj.GetType().Name);
            foreach (var member in obj.GetType()
                                       .GetProperties()
                                       .Where(i => i.PropertyType.IsValueType
                                           || i.PropertyType == typeof(String)))
            {
                db.Add(new XAttribute(member.Name, member.GetValue(obj, null) ?? string.Empty));
            }
            this.xmlContext.Add(db);
            lock (lockObj)
            {
                this.xmlContext.Save(this.XmlFileSavePath);
            }
            Committed = false;
        }
        public override void RegisterModified(object item)
        {
            if (item == null)
                throw new ArgumentException("The database entity can not be null.");

            this._xmlfilesavepath = AppDomain.CurrentDomain.BaseDirectory + "/" + item.GetType().Name + ".xml";

            IEntity ar = (IEntity)item;

            XElement xe = (from db in this.xmlContext.Root.Elements(item.GetType().Name)
                           where db.Attribute("Id").Value == ar.ID.ToString()
                           select db).Single();
            try
            {
                foreach (var member in item.GetType()
                                           .GetProperties()
                                           .Where(i => i.PropertyType.IsValueType
                                               || i.PropertyType == typeof(String)))
                {
                    xe.SetAttributeValue(member.Name, member.GetValue(item, null) ?? string.Empty);
                }
                lock (lockObj)
                {
                    this.xmlContext.Save(this.XmlFileSavePath);
                }
            }
            catch
            {
                throw;
            }
            Committed = false;
        }
        public override void RegisterDeleted(object item)
        {
            if (item == null)
                throw new ArgumentException("The database entity can not be null.");

            this._xmlfilesavepath = AppDomain.CurrentDomain.BaseDirectory + "/" + item.GetType().Name + ".xml";
            IEntity ar = (IEntity)item;

            XElement xe = (from db in this.xmlContext.Root.Elements(typeof(object).Name)
                           where db.Attribute("Id").Value == ar.ID.ToString()
                           select db).Single() as XElement;
            xe.Remove();
            lock (lockObj)
            {
                this.xmlContext.Save(this.XmlFileSavePath);
            }
            Committed = false;
        }
        #endregion

        #region IUnitOfWork Members
        public override bool DistributedTransactionSupported
        {
            get { return true; }
        }
        public override void Commit()
        {
            if (!Committed)
            {
                lock (sync)
                {
                    this.xmlContext.Save(this.XmlFileSavePath);
                }
                Committed = true;
            }
        }
        public override void Rollback()
        {
            Committed = false;
        }

        #endregion



    }
}
