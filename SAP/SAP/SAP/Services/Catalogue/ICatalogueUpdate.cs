using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAP.ViewModels.Catalogue.Admin;

namespace SAP.Services.Catalogue
{
    public interface ICatalogueUpdate
    {
        List<CreateItemViewModel> Update(String filepath);
    }
}
