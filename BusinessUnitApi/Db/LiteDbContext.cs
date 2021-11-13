using BusinessUnitApi.Controllers;
using BusinessUnitApi.Model;
using LiteDB;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessUnitApi.Db
{
    public class LiteDbContext
    {
        public LiteDatabase Database { get; }
        private ILiteCollection<BusinessUnit> BUList => Database.GetCollection<BusinessUnit>("businessunits");
        public LiteDbContext(IOptions<LiteDbConfig> configs)
        {
            try
            {
                var db = new LiteDatabase(configs.Value.DatabasePath);
                if (db != null)
                    Database = db;
            }
            catch (Exception ex)
            {
                throw new Exception("Can find or create LiteDb database.", ex);
            }
        }
        public async Task InsertBusinessUnit(BusinessUnit BUToAdd)
        {
            await Validator.ValidateBusinessUnit(BUToAdd);
            BUList.Insert(BUToAdd);
        }
        public IEnumerable<BusinessUnit> GetAllBusinessUnits()
        {
            return BUList.FindAll().ToList();
        }
        public void DeleteBusinessUnit(int id)
        {
            BUList.DeleteMany(i => i.Id == id);
        }

    }
}
