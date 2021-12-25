using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAP.Data;
using SAP.ViewModels.Catalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Services.Catalogue.Implementations
{
    public class CatalogueDataProvider : ICatalogueDataProvider
    {
        private IMapper Mapper { get; }

        private ApplicationDbContext DbContext { get; set; }

        public CatalogueDataProvider(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            Mapper = mapper;
            DbContext = applicationDbContext;
        }

        public async Task<CatalogueViewModel> GetCatalogueAsync(Guid? id = null)
        {
            var catalogue = new CatalogueViewModel
            {
                RootCategories = (await DbContext.Categories.Where(x => x.ParentId == null).ToListAsync()).Select(Mapper.Map<CategoryViewModel>).ToList()
            };

            if (id.HasValue)
            {
                var pathToRoot = await GetSelectedNodesPathAsync(id.Value);
                pathToRoot.Reverse();
                await FillChainOfCategoriesWithChild(catalogue.RootCategories.First(x => x.Id == pathToRoot.First()), pathToRoot);
            }

            return catalogue;
        }

        public async Task<ItemViewModel> GetItemAsync(Guid id)
        {
            var item = await DbContext.Items
                .Include(x => x.Attributes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                throw new KeyNotFoundException();
            }

            var result = Mapper.Map<ItemViewModel>(item);

            result.AttributesToSelect = new SelectList(result.Attributes, "Id", "ViewData");
            return result;
        }

        #region Private
        private async Task FillChainOfCategoriesWithChild(CategoryViewModel rootCategory, List<Guid> categoriesToFill)
        {
            rootCategory.IsSelected = true;

            var category = await DbContext.Categories
                .Include(x => x.Childs)
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == rootCategory.Id);

            if (category.Childs.Any())
            {
                rootCategory.Childs = category.Childs.Select(Mapper.Map<CategoryViewModel>).ToList();
            }

            if (category.Items.Any())
            {
                rootCategory.Items = category.Items.Select(Mapper.Map<ItemViewModel>).ToList();
            }

            if (categoriesToFill.Count > 1)
            {
                categoriesToFill.RemoveAt(0);
                await FillChainOfCategoriesWithChild(rootCategory.Childs.First(x => x.Id == categoriesToFill.First()), categoriesToFill);
            }
        }

        private async Task<List<Guid>> GetSelectedNodesPathAsync(Guid id)
        {
            var result = new List<Guid>();
            await FindPathToRootFromCategoryAsync(id, result);
            return result;
        }

        private async Task FindPathToRootFromCategoryAsync(Guid id, ICollection<Guid> categories)
        {
            if (categories == null)
            {
                categories = new List<Guid>();
            }

            categories.Add(id);
            var category = await DbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category.ParentId.HasValue)
            {
                await FindPathToRootFromCategoryAsync(category.ParentId.Value, categories);
            }
        }
        #endregion Private
    }
}
