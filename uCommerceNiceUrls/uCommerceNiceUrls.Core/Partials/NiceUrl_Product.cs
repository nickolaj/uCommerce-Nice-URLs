using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using uCommerceNiceUrls.Core.Extensions;
using uCommerceNiceUrls.Core.Helpers;

namespace uCommerceNiceUrls.Core.Entities
{
    public partial class NiceUrl_Product
    {
        //public NiceUrl_Product(UCommerce.EntitiesV2.Product product, UCommerce.EntitiesV2.Category category, string cultureCode) : this()
        //{
        //    this.uCommerceProductID = product.Id;
        //    var langName = new NiceUrl_LanguageName()
        //                       {
        //                           CultureCode = cultureCode,
        //                           NiceUrl = product.GetNiceUrl(cultureCode)
        //                       };
        //    this.NiceUrl_LanguageName.Add(langName);
        //    this.NiceUrl_Categories.Add(URLHelper.GetNiceUrlObject(category));
            
        //}
        //public NiceUrl_Product(UCommerce.EntitiesV2.Product product, IEnumerable<UCommerce.EntitiesV2.Category> categories, string cultureCode)
        //    : this()
        //{
        //    this.uCommerceProductID = product.Id;
        //    var langName = new NiceUrl_LanguageName()
        //    {
        //        CultureCode = cultureCode,
        //        NiceUrl = product.GetNiceUrl(cultureCode)
        //    };
        //    this.NiceUrl_LanguageName.Add(langName);
        //    this.NiceUrl_Categories = categories.Select(x => x.GetNiceUrlObject()) as ICollection<NiceUrl_Category>;

        //}
    }
}
