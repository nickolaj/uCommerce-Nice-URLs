using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uCommerceNiceUrls.Core.Entities;
using UCommerce.EntitiesV2;

namespace uCommerceNiceUrls.Core.Extensions
{
    public static class uCommerceExtensions
    {

        public static ProductCatalog GetuCommerceCatalog(this NiceUrl_Catalog catalog)
        {
            return ProductCatalog.Get(catalog.uCommerceCatalogID);
        }
        public static Category GetuCommerceCategory(this NiceUrl_Category category)
        {
            return Category.Get(category.uCommerceCategoryID);
        }
        public static Product GetuCommerceProduct(this NiceUrl_Product product)
        {
            return Product.Get(product.uCommerceProductID);
        }


    }
}
