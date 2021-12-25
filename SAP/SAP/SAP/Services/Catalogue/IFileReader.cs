using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using SAP.ViewModels.Catalogue.Admin;

namespace SAP.Services.Catalogue
{
    public interface IFileReader
    {
        List<CreateItemViewModel> UpdateCatalogue(String filepath);
    }
}
