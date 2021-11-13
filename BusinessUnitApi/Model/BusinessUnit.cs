using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessUnitApi.Model
{
    public class BusinessUnit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// DaDataMember
        /// </summary>
        public string FullName { get; set; }
        public BusinessUnitType Type { get; set; }
        public string TypeString
        {
            get { return this.Type.ToString(); }
            set
            {
                BusinessUnitType tp;
                this.Type = Enum.TryParse(value, true, out tp) ? tp : BusinessUnitType.EMPTY;
            }
        }
        public string Inn { get; set; }
        public string Kpp { get; set; }
    }
}
