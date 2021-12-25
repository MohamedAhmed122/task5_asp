using AutoMapper;
using SAP.Data.Models;
using SAP.Data.Models.Catalogue;
using SAP.ViewModels.Cart;
using SAP.ViewModels.Catalogue;
using SAP.ViewModels.Catalogue.Admin;
using SAP.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // From db to view entities
            CreateMap<Item, ItemViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Attributes, AttributesViewModel>();
            CreateMap<ItemToCart, CartItemViewModel>();
            CreateMap<Cart, CartViewModel>().ForMember(x => x.Items, opt => opt.MapFrom(c => c.ItemToCarts));
            CreateMap<ItemToOrder, OrderItemViewModel>();
            CreateMap<Order, OrderViewModel>().ForMember(x => x.OrderItems, opt => opt.MapFrom(o => o.ItemToOrders));

            // From view to db entities
            CreateMap<CreateCategoryViewModel, Category>();
            CreateMap<CreateItemViewModel, Item>();
            CreateMap<AttributesViewModel, Attributes>();
        }
    }
}
