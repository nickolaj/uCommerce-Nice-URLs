using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using uCommerceNiceUrls.Core.Entities;
using uCommerceNiceUrls.Core.Extensions;
using uCommerceNiceUrls.Core.Shared.Interfaces;
using uCommerceNiceUrls.Core.Shared.Interfaces.Bases;
using Ninject;
using Ninject.Web.Common;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;
using umbraco.cms.businesslogic.language;

namespace uCommerceNiceUrls.Core.Helpers
{
    public static class URLHelper
    {
        private static readonly char[] separator = "/".ToCharArray();
       
        public static void SetContextByUrl(string url)
        {
            //url = CleanUrl(url);
            var splittedUrl = SplitUrl(url);
            var repo = CreateRepository();

            NiceUrl_Catalog niceUrlCatalog;
            NiceUrl_Category niceUrlCategory;
            NiceUrl_Product niceUrlProduct;

            UrlDecode(splittedUrl,out niceUrlCatalog, out niceUrlCategory,out niceUrlProduct);
            SetuCommerceContext(niceUrlCatalog,niceUrlCategory,niceUrlProduct);
        }
        private static void SetuCommerceContext(NiceUrl_Catalog catalog, NiceUrl_Category category, NiceUrl_Product product)
        {
            if(catalog != null)
                SiteContext.Current.CatalogContext.CurrentCatalog = catalog.GetuCommerceCatalog();
            if(category != null)
            SiteContext.Current.CatalogContext.CurrentCategory = category.GetuCommerceCategory();
            if(product != null)
            SiteContext.Current.CatalogContext.CurrentProduct = product.GetuCommerceProduct();
        }
        private static string[] SplitUrl(string url)
        {
            return url.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }
        private static string CleanUrl(string url)
        {
            var uri = new Uri(url);
            return uri.AbsolutePath;
        }
        public static void UrlDecode(string[] splittedUrl, out NiceUrl_Catalog urlCatalog, out  NiceUrl_Category urlCategory, out NiceUrl_Product urlProduct)
        {
            var repo = CreateRepository();
            var cultureCode = GetCultureCode();


            urlCatalog = repo.GetCatalog(splittedUrl[0], cultureCode);
            urlCategory = null;
            urlProduct = null; 

            for (int i = 1; i < splittedUrl.Length; i++)
            {
                var innerCat = repo.GetCategory(splittedUrl[i], cultureCode,urlCatalog);
                if (innerCat != null)
                {
                    urlCategory = innerCat;
                }
                else if(urlCategory != null)
                {
                    var innerProduct = repo.GetProduct(splittedUrl[i], cultureCode, urlCategory);
                    if (innerProduct != null)
                    {
                        urlProduct = innerProduct;
                    }
                }
            }


        }
        private static INiceUrlRepository CreateRepository()
        {
             var bootStrapper = new Bootstrapper();
            var kernel = bootStrapper.Kernel;

            return ((IRepositoryFactory<INiceUrlRepository>)kernel.GetService(typeof(IRepositoryFactory<INiceUrlRepository>))).Repository;
        }
        private static string GetCultureCode()
        {
            return Thread.CurrentThread.CurrentUICulture.Name;
        }
        public static string GetNiceUrlName(ProductCatalog catalog, string cultureCode)
        {
            return catalog.Name;
        }
        public static string GetNiceUrlName(Category category, string cultureCode)
        {
            return category.Name;
        }
        public static string GetNiceUrlName(Product product, string cultureCode)
        {
            return product.Name;
        }
        public static bool IsUniqueUrl(Product product, Category category, string newName, string cultureCode)
        {
            var repo = CreateRepository();
            return repo.IsUniqueProductUrl(product.GetNiceUrl(cultureCode), cultureCode, category.GetNiceUrlObject(),
                                           product.Id);
        }
        public static bool IsUniqueUrl(Category category, ProductCatalog catalog, string newName, string cultureCode)
        {
            var repo = CreateRepository();
            return repo.GetCategory(newName, cultureCode, repo.GetCatalogByuCommerceId(catalog.Id)) == null;
        }
        public static bool IsUniqueUrl(ProductCatalog catalog, string newName, string cultureCode)
        {
            var repo = CreateRepository();
            return repo.GetCatalog(newName, cultureCode) == null;
        }
        public static IEnumerable<string> CultureCodeList()
        {
            return Language.GetAllAsList().Select(x => x.CultureAlias);
            
        }
        public static NiceUrl_Product GetOrCreateNiceUrlObject(Product product,IEnumerable<string> cultureCodes )
        {
            var repo = CreateRepository();
            return repo.CreateOrUpdateProduct(product.Id,product.GetCategories().Select(x=> x.GetNiceUrlObject()),cultureCodes);

        }
        public static NiceUrl_Category GetOrCreateNiceUrlObject(Category category)
        {
            var repo = CreateRepository();
            return category != null ? repo.GetCategoryByuCommerceId(category.Id) : null;
        }
        public static NiceUrl_Catalog GetOrCreateNiceUrlObject(ProductCatalog catalog)
        {
            var repo = CreateRepository();
            return catalog != null ? repo.GetCatalogByuCommerceId(catalog.Id) : null;
        }

    }
}
