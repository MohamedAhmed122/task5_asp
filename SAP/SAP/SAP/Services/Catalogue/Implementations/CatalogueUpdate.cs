using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAP.ViewModels.Catalogue.Admin;

namespace SAP.Services.Catalogue.Implementations
{
    public class CatalogueUpdate : ICatalogueUpdate
    {
        private IFileReader _fileReader;

        private void SetUpdateMethod(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public List<CreateItemViewModel> Update(String filepath)
        {
            if (filepath.EndsWith(".txt"))
                SetUpdateMethod(new TXTReader());
            else if (filepath.EndsWith(".json"))
                SetUpdateMethod(new JSONReader());
            return _fileReader.UpdateCatalogue(filepath); 
        }
    }
}   
