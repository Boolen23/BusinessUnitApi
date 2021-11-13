using Dadata;
using Dadata.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessUnitApi.Model
{
    public class DaData
    {
        public static string ApiKey { get; set; }
        public static async Task<SuggestResponse<Party>>Load(string Inn, string Kpp)
        {
            var api = new SuggestClientAsync(ApiKey);

            FindPartyRequest request = new FindPartyRequest(query: Inn, kpp: Kpp);
            return await api.FindParty(request);
            
        }
    }
}
