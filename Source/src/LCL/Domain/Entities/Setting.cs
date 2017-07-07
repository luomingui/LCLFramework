
using LCL.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCL.Domain.Entities
{
    public partial class Setting : AggregateRoot
    {
        public Setting() { }

        public Setting(string name, string value, int storeId = 0)
        {
            this.ID = Guid.NewGuid();
            this.Name = name;
            this.Value = value;
            this.StoreId = storeId;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public int StoreId { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
    public partial  class SecuritySettings : ISettings
    {
        public bool ForceSslForAllPages { get; set; }
        public string EncryptionKey { get; set; }
        public List<string> AdminAreaAllowedIpAddresses { get; set; }
    }
}
