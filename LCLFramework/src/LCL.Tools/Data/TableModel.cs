using System.Collections.Generic;

namespace LCL.Tools
{
    public class DataBaseModel
    {
        public string DatabaseName { get; set; }
        public List<TableModel> Tables { get; set; }
    }
    public class TableModel
    {
        public string TableName { get; set; }
        public string TableNameRemark { get; set; }
        public string TablePK { get; set; }
        public bool IsTree { get; set; }
        public List<TableColumn> Columns { get; set; }
    }
    public class TableColumn
    {
        public TableColumn()
        {
            PK = false;
            Identifying = false;
            Isnullable = false;
            MaxLength = 50;
        }
        public bool PK { get; set; }
        //标识
        public bool Identifying { get; set; }
        //允许空
        public bool Isnullable { get; set; }
        public int MaxLength { get; set; }
        public string ColumnName { get; set; }
        public string ColumnType { get; set; }
        //字段说明
        public string ColumnRemark { get; set; }
        public string DefaultValue { get; set; }
        public string ColumnNameRemark
        {
            get { return ColumnRemark + "[" + ColumnName + "]"; }
        }
    }
    public class DBAModel
    {

        public void Load()
        {

        }
    }
}
