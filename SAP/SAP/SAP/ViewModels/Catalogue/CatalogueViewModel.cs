using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.ViewModels.Catalogue
{
    public class CatalogueViewModel
    {
        public List<CategoryViewModel> RootCategories { get; set; }

        public List<ItemViewModel> Items { get; set; }
    }
}
