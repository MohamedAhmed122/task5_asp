using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SAP.Services;
using SAP.Services.Catalogue;

namespace SAP.Controllers
{
    [Route("catalogue")]
    public class CatalogueController : Controller
    {
        ICatalogueDataProvider CatalogueDataProvider { get; }

        ICartService CartService { get; }

        public CatalogueController(ICatalogueDataProvider catalogueDataProvider,
            ICartService cartService)
        {
            CatalogueDataProvider = catalogueDataProvider;
            CartService = cartService;
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

        [Route("item/{id}")]
        public async Task<ActionResult> Item(Guid id)
        {
            var item = await CatalogueDataProvider.GetItemAsync(id);
            item.IsInCart = await CartService.IsItemInCartAsync(id);
            return View(item);
        }
    }
}