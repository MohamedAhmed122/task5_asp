using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using Newtonsoft.Json;
using SAP.ViewModels.Catalogue.Admin;

namespace SAP.Services.Catalogue.Implementations
{
    class JSONReader : IFileReader
    {

        public List<CreateItemViewModel> UpdateCatalogue(String filepath)
        {
            var itemList = new List<CreateItemViewModel>();
            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                if (json != " ")
                {
                    itemList = JsonConvert.DeserializeObject<List<CreateItemViewModel>>(json);
                }
            }
            
            return itemList;
        }
    }
}
