using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SAP.Services.Catalogue;
using SAP.ViewModels.Catalogue;
using SAP.ViewModels.Catalogue.Admin;

namespace SAP.Controllers.Admin
{
    [Authorize]
    [Route("admin/catalogue")]
    public class CatalogueAdminController : Controller
    {
        private ICatalogueAdministration CatalogueAdministration { get; }

        private ICatalogueDataProvider CatalogueDataProvider { get; }

        private ICatalogueUpdate CatalogueUpdate { get; }

        public CatalogueAdminController(ICatalogueAdministration catalogueAdministration,
            ICatalogueDataProvider catalogueDataProvider, ICatalogueUpdate catalogueUpdate)
        {
            CatalogueAdministration = catalogueAdministration;
            CatalogueDataProvider = catalogueDataProvider;
            CatalogueUpdate = catalogueUpdate;
        }

        [Route("")]
        public async Task<ActionResult> Index()
        {
            var catalogue = await CatalogueDataProvider.GetCatalogueAsync();
            return View(catalogue);
        }

        [Route("node/{id}")]
        public async Task<ActionResult> Node(Guid id)
        {
            var catalogue = await CatalogueDataProvider.GetCatalogueAsync(id);
            return View("Index", catalogue);
        }

        [Route("create_category")]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("create_category")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCategory(CreateCategoryViewModel category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                await CatalogueAdministration.CreateCategoryAsync(category);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [Route("create_item")]
        public ActionResult CreateItem()
        {
            return View();
        }

        [HttpPost]
        [Route("create_item")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateItem(CreateItemViewModel item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                await CatalogueAdministration.CreateItemAsync(item);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // Never implement delete of something by GET request!
        [Route("delete_item/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await CatalogueAdministration.DeleteItemAsync(id);
            return RedirectToAction("Index");
        }

        [Route("update_catalogue")]
        public IActionResult UpdateCatalogue()
        {
            return View();
        }

        [Route("update_catalogue/success")]
        public async Task <IActionResult> UpdateCatalogueOperation()
        {
            var list = CatalogueUpdate.Update(@"..\SAP\wwwroot\data.json");
            foreach (var item in list)
               await CatalogueAdministration.CreateItemAsync(item);
            return View();
        }


    }
}