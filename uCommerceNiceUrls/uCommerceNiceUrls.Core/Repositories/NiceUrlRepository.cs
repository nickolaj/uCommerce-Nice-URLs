using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Collections;
using uCommerceNiceUrls.Core.Entities;
using uCommerceNiceUrls.Core.Extensions;
using uCommerceNiceUrls.Core.Shared.Interfaces;
using UCommerce.EntitiesV2;

namespace uCommerceNiceUrls.Core.Repositories
{


    public class NiceUrlRepository : Bases.RepositoryBase<NiceUrl_Url>, INiceUrlRepository
    {

        private IDbSet<NiceUrl_Url> _niceUrlSet;
        public IDbSet<NiceUrl_Url> NiceUrlSet { get { return _niceUrlSet ?? (_niceUrlSet = this.UnitOfWork.Context.Set<NiceUrl_Url>()); }}

        private IDbSet<NiceUrl_LanguageName> _languageNamesSet;
        public IDbSet<NiceUrl_LanguageName> LanguageNamesSet { get { return _languageNamesSet ?? (_languageNamesSet = this.UnitOfWork.Context.Set<NiceUrl_LanguageName>()); } }

        private IDbSet<NiceUrl_Catalog> _niceUrlCatalogsSet;
        public IDbSet<NiceUrl_Catalog> NiceUrlCatalogsSet { get { return _niceUrlCatalogsSet ?? (_niceUrlCatalogsSet = this.UnitOfWork.Context.Set<NiceUrl_Catalog>()); } }

        private IDbSet<NiceUrl_Category> _niceUrlCategoriesSet;
        public IDbSet<NiceUrl_Category> NiceUrlCategoriesSet { get { return _niceUrlCategoriesSet ?? (_niceUrlCategoriesSet = this.UnitOfWork.Context.Set<NiceUrl_Category>()); } }

        private IDbSet<NiceUrl_Product> _niceUrlProductsSet;
        public IDbSet<NiceUrl_Product> NiceUrlProductsSet { get { return _niceUrlProductsSet ?? (_niceUrlProductsSet = this.UnitOfWork.Context.Set<NiceUrl_Product>()); } }



        public IList<NiceUrl_LanguageName> GetUrl(int catalogID, int categoryID, int productID, string cultureCode)
        {
            var prodUrl =
                this.NiceUrlProductsSet.FirstOrDefault(
                    x =>
                    x.uCommerceProductID == productID && x.NiceUrl_Categories.Any(c=> c.uCommerceCategoryID == categoryID && c.NiceUrl_Catalog.uCommerceCatalogID == catalogID));
            var prodLangName = prodUrl != null ? prodUrl.NiceUrl_LanguageName.FirstOrDefault(x=> x.CultureCode == cultureCode) : null;

            var langNames = GetUrl(catalogID, categoryID, cultureCode);
            langNames.Add(prodLangName);
            return langNames;

        }

        public IList<NiceUrl_LanguageName> GetUrl(int catalogID, int categoryID, string cultureCode)
        {
            var niceUrlCategory =
                this.NiceUrlCategoriesSet.FirstOrDefault(x => x.uCommerceCategoryID == categoryID && x.NiceUrl_Catalog.uCommerceCatalogID == catalogID);
            
            var categoryLangName= niceUrlCategory != null && niceUrlCategory.NiceUrl_LanguageName.Any()
                       ? niceUrlCategory.NiceUrl_LanguageName.FirstOrDefault(x => x.CultureCode == cultureCode)
                       : null;
            var catalogLangName = GetUrl(catalogID, cultureCode);

            return new List<NiceUrl_LanguageName> { catalogLangName, categoryLangName };
        }

        public NiceUrl_LanguageName GetUrl(int catalogID, string cultureCode)
        {
            var catUrl = this.NiceUrlCatalogsSet.FirstOrDefault(x => x.uCommerceCatalogID == catalogID);
            return catUrl != null && catUrl.NiceUrl_LanguageName.Any()
                       ? catUrl.NiceUrl_LanguageName.FirstOrDefault(x => x.CultureCode == cultureCode)
                       : null;
        }
        public NiceUrl_Product CreateOrUpdateProduct(int ucommerceProductId,IEnumerable<NiceUrl_Category> categories , IEnumerable<string> cultureCodes )
        {
            var prod = Product.Get(ucommerceProductId);
            
            var langnames = new List<NiceUrl_LanguageName> {};
                langnames.AddRange(cultureCodes.Select(cultureCode => new NiceUrl_LanguageName()
                                                                          {
                                                                              CultureCode = cultureCode, NiceUrl = prod.GetNiceUrl(cultureCode)
                                                                          }));
            var existing = GetAllProducts().FirstOrDefault(p => p.uCommerceProductID == ucommerceProductId);
            if(existing == null)
            {
                
                var newEntity = new NiceUrl_Product()
                               {
                                   NiceUrl_Categories = categories as ICollection<NiceUrl_Category>,
                                   NiceUrl_LanguageName = langnames,
                                   uCommerceProductID = ucommerceProductId
                               };
                Insert(newEntity);
                return newEntity;
            }
            else
            {
                existing.NiceUrl_LanguageName = langnames;
                existing.NiceUrl_Categories = categories as ICollection<NiceUrl_Category>;
                Update(existing);
                return existing;

            }

        }
        
        public void Insert(NiceUrl_Catalog catalog)
        {
            this.NiceUrlCatalogsSet.Add(catalog);
            this.UnitOfWork.Context.SaveChanges();
        }

        public void Insert(NiceUrl_Catalog catalog, NiceUrl_Category category)
        {
            catalog.NiceUrl_Category.Add(category);
            this.Update(catalog);
        }

        public void Insert(NiceUrl_Category category, NiceUrl_Product product)
        {
            category.NiceUrl_Products.Add(product);
            this.Update(category);
        }

        public void Update(NiceUrl_Catalog catalog)
        {
            this.NiceUrlCatalogsSet.Attach(catalog);
            this.UnitOfWork.Context.Entry<NiceUrl_Catalog>(catalog).State = EntityState.Modified;
            this.UnitOfWork.Context.SaveChanges();
            
        }

        public void Update(NiceUrl_Category category)
        {
            this.NiceUrlCategoriesSet.Attach(category);
            this.UnitOfWork.Context.Entry<NiceUrl_Category>(category).State = EntityState.Modified;
            this.UnitOfWork.Context.SaveChanges();
        }

        public void Update(NiceUrl_Product product)
        {
            this.NiceUrlProductsSet.Attach(product);
            this.UnitOfWork.Context.Entry<NiceUrl_Product>(product).State = EntityState.Modified;
            this.UnitOfWork.Context.SaveChanges();
        }

        public void Delete(NiceUrl_Catalog catalog)
        {
            this.NiceUrlCatalogsSet.Remove(catalog);
            this.UnitOfWork.Context.SaveChanges();
        }

        public void Delete(NiceUrl_Category category)
        {
            this.NiceUrlCategoriesSet.Remove(category);
            this.UnitOfWork.Context.SaveChanges();
        }

        public void Delete(NiceUrl_Product product)
        {
            this.NiceUrlProductsSet.Remove(product);
            this.UnitOfWork.Context.SaveChanges();
        }

        public IQueryable<NiceUrl_Catalog> GetAllCatalogs()
        {
            return this.NiceUrlCatalogsSet;
        }

        public NiceUrl_Catalog GetCatalogByuCommerceId(int id)
        {
            return this.NiceUrlCatalogsSet.FirstOrDefault(x => x.uCommerceCatalogID == id);
        }
        public NiceUrl_Catalog GetCatalog(int id)
        {
            return this.NiceUrlCatalogsSet.FirstOrDefault(x => x.ID == id);
        }

        public NiceUrl_Catalog GetCatalog(string url, string cultureCode)
        {
            return this.NiceUrlCatalogsSet.FirstOrDefault(x => x.NiceUrl_LanguageName.Any(y => y.NiceUrl == url && y.CultureCode == cultureCode));
        }

        
        public IQueryable<NiceUrl_Category> GetAllCategories()
        {
            return this.NiceUrlCategoriesSet;
        }

        public NiceUrl_Category GetCategory(int id)
        {
            return this.NiceUrlCategoriesSet.FirstOrDefault(x => x.ID == id);
        }
        public NiceUrl_Category GetCategoryByuCommerceId(int uCommerceCategoryId)
        {
            return this.NiceUrlCategoriesSet.FirstOrDefault(x => x.uCommerceCategoryID == uCommerceCategoryId);
        }

        public NiceUrl_Category GetCategory(string categoryURL, string cultureCode, NiceUrl_Catalog catalog)
        {
            var count =
                this.NiceUrlCategoriesSet.Count(
                    x => x.NiceUrl_LanguageName.Any(y => y.NiceUrl == categoryURL && y.CultureCode == cultureCode));
            return count > 1 ? GetCategoryByPath(catalog,categoryURL,cultureCode) :this.NiceUrlCategoriesSet.FirstOrDefault(x => x.NiceUrl_LanguageName.Any(y => y.NiceUrl == categoryURL && y.CultureCode == cultureCode));
        }

        public NiceUrl_Category GetCategoryByPath(int catalogID, string categoryUrl, string cultureCode)
        {
            return
                this.NiceUrlCatalogsSet.FirstOrDefault(x => x.uCommerceCatalogID == catalogID).NiceUrl_Category.FirstOrDefault(
                    x => x.NiceUrl_LanguageName.Any(z => z.CultureCode == cultureCode && z.NiceUrl == categoryUrl));
        }
        public NiceUrl_Category GetCategoryByPath(NiceUrl_Catalog catalog, string categoryUrl, string cultureCode)
        {
            return this.NiceUrlCategoriesSet.Where(x => x.CatalogID == catalog.ID).FirstOrDefault(
                    x => x.NiceUrl_LanguageName.Any(z => z.CultureCode == cultureCode && z.NiceUrl == categoryUrl));
        }

        public IQueryable<NiceUrl_Product> GetAllProducts()
        {
            return this.NiceUrlProductsSet;
        }

        public NiceUrl_Product GetProduct(int id)
        {
            return this.NiceUrlProductsSet.FirstOrDefault(x => x.ID == id);
        }
        public NiceUrl_Product GetProductByuCommerceId(int id)
        {
            return this.NiceUrlProductsSet.FirstOrDefault(x => x.uCommerceProductID == id);
        }
        


        public NiceUrl_Product GetProduct(string productURL, string cultureCode, NiceUrl_Category category)
        {
            var count = this.NiceUrlProductsSet.Count(x => x.NiceUrl_LanguageName.Any(y => y.NiceUrl == productURL && y.CultureCode == cultureCode));
            return count > 1 ? GetProductByPath(category,productURL,cultureCode) :this.NiceUrlProductsSet.FirstOrDefault(x => x.NiceUrl_LanguageName.Any(y => y.NiceUrl == productURL && y.CultureCode == cultureCode));
        }
        public bool IsUniqueProductUrl(string productUrl, string cultureCode, NiceUrl_Category category, int ucommerceproductId)
        {
            var simpleSearchResult =
                this.NiceUrlProductsSet.Any(
                    x => x.NiceUrl_LanguageName.Any(y => y.NiceUrl == productUrl && y.CultureCode == cultureCode) && x.uCommerceProductID != ucommerceproductId);
            var returnvalue = !simpleSearchResult;
            if(simpleSearchResult)
            {
                returnvalue = !category.NiceUrl_Products.Any(catprod => catprod.NiceUrl_LanguageName.Any(
                    x => x.CultureCode == cultureCode && x.NiceUrl == productUrl));
            }
            return returnvalue;
        }

        public NiceUrl_Product GetProductByPath(int categoryID, string productURL, string cultureCode)
        {
            return this.NiceUrlCategoriesSet.FirstOrDefault(
                x => x.uCommerceCategoryID == categoryID).NiceUrl_Products.FirstOrDefault(y => y.NiceUrl_LanguageName.Any(
                        z => z.CultureCode == cultureCode && z.NiceUrl == productURL));
        }
        public NiceUrl_Product GetProductByPath(NiceUrl_Category categoryURL, string productURL, string cultureCode)
        {
            return categoryURL.NiceUrl_Products.FirstOrDefault
                    (y => y.NiceUrl_LanguageName.Any(z => z.CultureCode == cultureCode && z.NiceUrl == productURL));
        }
    }
}
