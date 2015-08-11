

namespace LCL.Tools.Data
{
    public class Generate2000Sql : GenerateSql
    {
        public Generate2000Sql( )
        {
            this.sql_tablesInfos = @"SELECT TOP 100 PERCENT --a.id, 
      CASE WHEN a.colorder = 1 THEN d.name ELSE '' END AS 表名, 
      CASE WHEN a.colorder = 1 THEN isnull(f.value, '') ELSE '' END AS 表说明, 
      a.colorder AS 字段序号, a.name AS 字段名, CASE WHEN COLUMNPROPERTY(a.id, 
      a.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识, 
      CASE WHEN EXISTS
          (SELECT 1
         FROM dbo.sysindexes si INNER JOIN
               dbo.sysindexkeys sik ON si.id = sik.id AND si.indid = sik.indid INNER JOIN
               dbo.syscolumns sc ON sc.id = sik.id AND sc.colid = sik.colid INNER JOIN
               dbo.sysobjects so ON so.name = si.name AND so.xtype = 'PK'
         WHERE sc.id = a.id AND sc.colid = a.colid) THEN '√' ELSE '' END AS 主键, 
      b.name AS 类型, a.length AS 长度, COLUMNPROPERTY(a.id, a.name, 'PRECISION') 
      AS 精度, ISNULL(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) AS 小数位数, 
      CASE WHEN a.isnullable = 1 THEN '√' ELSE '' END AS 允许空, ISNULL(e.text, '') 
      AS 默认值, ISNULL(g.[value], '') AS 字段说明, d.crdate AS 创建时间, 
      CASE WHEN a.colorder = 1 THEN d.refdate ELSE NULL END AS 更改时间
FROM dbo.syscolumns a LEFT OUTER JOIN
      dbo.systypes b ON a.xtype = b.xusertype INNER JOIN
      dbo.sysobjects d ON a.id = d.id AND d.xtype = 'U' AND 
      d.status >= 0 LEFT OUTER JOIN
      dbo.syscomments e ON a.cdefault = e.id LEFT OUTER JOIN
      dbo.sysproperties g ON a.id = g.id AND a.colid = g.smallid AND 
      g.name = 'MS_Description' LEFT OUTER JOIN
      dbo.sysproperties f ON d.id = f.
id AND f.smallid = 0 AND 
      f.name = 'MS_Description'
 where d.name<>'dtproperties' ";
        }
    }
}
