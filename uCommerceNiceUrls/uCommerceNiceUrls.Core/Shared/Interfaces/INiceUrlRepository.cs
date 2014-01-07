using System.Collections.Generic;
using System.Linq;
using uCommerceNiceUrls.Core.Entities;

namespace uCommerceNiceUrls.Core.Shared.Interfaces
{
    public interface INiceUrlRepository : Bases.IRepository<NiceUrl_Url>
    {
        IList<NiceUrl_LanguageName> GetUrl(int catalogID, int categoryID, int productID, string cultureCode);
        IList<NiceUrl_LanguageName> GetUrl(int catalogID, int categoryID, string cultureCode);
        NiceUrl_LanguageName GetUrl(int catalogID, string cultureCode);
        void Insert(NiceUrl_Catalog catalog);
        void Insert(NiceUrl_Catalog catalog, NiceUrl_Category category);
        void Insert(NiceUrl_Category category, NiceUrl_Product product);
        void Update(NiceUrl_Catalog catalog);
        void Update(NiceUrl_Category category);
        void Update(NiceUrl_Product product);
        void Delete(NiceUrl_Catalog catalog);
        void Delete(NiceUrl_Category category);
        void Delete(NiceUrl_Product product);
        IQueryable<NiceUrl_Catalog> GetAllCatalogs();
        NiceUrl_Catalog GetCatalogByuCommerceId(int id);
        NiceUrl_Catalog GetCatalog(int id);
        NiceUrl_Catalog GetCatalog(string url, string cultureCode);
        IQueryable<NiceUrl_Category> GetAllCategories();
        NiceUrl_Category GetCategory(int id);
        NiceUrl_Category GetCategoryByuCommerceId(int uCommerceCategoryId);
        NiceUrl_Category GetCategory(string categoryURL, string cultureCode, NiceUrl_Catalog catalog);
        NiceUrl_Category GetCategoryByPath(int catalogID, string categoryUrl, string cultureCode);
        NiceUrl_Category GetCategoryByPath(NiceUrl_Catalog catalog, string categoryUrl, string cultureCode);
        IQueryable<NiceUrl_Product> GetAllProducts();
        NiceUrl_Product GetProduct(int id);
        NiceUrl_Product GetProductByuCommerceId(int id);
        NiceUrl_Product GetProduct(string productURL, string cultureCode, NiceUrl_Category category);
        bool IsUniqueProductUrl(string productUrl, string cultureCode, NiceUrl_Category category, int ucommerceproductId);
        NiceUrl_Product GetProductByPath(int categoryID, string productURL, string cultureCode);
        NiceUrl_Product GetProductByPath(NiceUrl_Category categoryURL, string productURL, string cultureCode);
        NiceUrl_Product CreateOrUpdateProduct(int ucommerceProductId, IEnumerable<NiceUrl_Category> categories,
                              IEnumerable<string> cultureCodes);
    }
}