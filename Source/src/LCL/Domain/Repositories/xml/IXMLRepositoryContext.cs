using LCL.Domain.Entities;
using LCL.Domain.Repositories;
using LCL.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LCL.Repositories.XML
{
    public interface IXMLRepositoryContext : IRepositoryContext
    {
        XDocument Context { get; }
        string XmlFileSavePath { get; }
        IQueryable<TEntity> GetModel<TEntity>() where TEntity : class, IEntity;

    }
}
