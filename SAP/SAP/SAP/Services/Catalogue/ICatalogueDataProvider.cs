using SAP.ViewModels.Catalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Services.Catalogue
{
    public interface ICatalogueDataProvider
    {
        Task<CatalogueViewModel> GetCatalogueAsync(Guid? id = null);

        Task<ItemViewModel> GetItemAsync(Guid id);
    }
}
