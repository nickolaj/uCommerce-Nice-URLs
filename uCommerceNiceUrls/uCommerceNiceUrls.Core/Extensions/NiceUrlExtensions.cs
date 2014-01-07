using System;
using uCommerceNiceUrls.Core.Entities;
using UCommerce.EntitiesV2;

namespace uCommerceNiceUrls.Core.Extensions
{
    public static class NiceUrlExtensions
    {
        public static string GetNiceUrl(this Product product, string cultureCode)
        {
            return Helpers.URLHelper.GetNiceUrlName(product, cultureCode);
        }
        public static NiceUrl_Product GetNiceUrlObject(this Product product)
        {
            throw new NotImplementedException();
            //return Helpers.URLHelper.GetOrCreateNiceUrlObject(product);
        }
        public static NiceUrl_Category GetNiceUrlObject(this Category category)
        {
            return Helpers.URLHelper.GetOrCreateNiceUrlObject(category);
        }
        public static NiceUrl_Catalog GetNiceUrlObject(this ProductCatalog catalog)
        {
            return Helpers.URLHelper.GetOrCreateNiceUrlObject(catalog);
        }
        public static string GetNiceUrl(this Category category, string cultureCode)
        {
            return Helpers.URLHelper.GetNiceUrlName(category, cultureCode);
        }
        public static string GetNiceUrl(this ProductCatalog catalog, string cultureCode)
        {
            return Helpers.URLHelper.GetNiceUrlName(catalog, cultureCode);
        }

    }
}
