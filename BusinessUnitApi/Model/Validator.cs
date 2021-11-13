using Dadata.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessUnitApi.Model
{
    public static class Validator
    {
        public static async Task ValidateBusinessUnit(BusinessUnit bu)
        {
            if (string.IsNullOrEmpty(bu.Name) || string.IsNullOrEmpty(bu.Inn) || bu.Type == BusinessUnitType.EMPTY)
                throw new Exception("Пустое Имя, ИНН или тип контрагента!");

            if (bu.Type == BusinessUnitType.LEGAL && string.IsNullOrEmpty(bu.Kpp))
                throw new Exception("Пустое КПП у юр.лица!");

            if (bu.Type == BusinessUnitType.INDIVIDUAL && !string.IsNullOrEmpty(bu.Kpp))
                throw new Exception("У ИП не может быть КПП!");

            var PartyData = await DaData.Load(bu.Inn, bu.Type == BusinessUnitType.LEGAL ? bu.Kpp : null);
            if (PartyData.suggestions.Count == 0) 
                throw new Exception($"Не найдена информаций по {(bu.Type == BusinessUnitType.LEGAL ? "Юр. лицу" : "ИП")} \"{bu.Name}\"!");

            bu.FullName = PartyData.suggestions[0].data.name.full_with_opf;
        }
    }
}
