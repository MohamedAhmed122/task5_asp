using SAP.ViewModels.Catalogue;
using SAP.ViewModels.Catalogue.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Services.Catalogue
{
    public interface ICatalogueAdministration
    {
        Task CreateCategoryAsync(CreateCategoryViewModel category);

        Task CreateItemAsync(CreateItemViewModel item);

        Task DeleteItemAsync(Guid id);
    }
}
