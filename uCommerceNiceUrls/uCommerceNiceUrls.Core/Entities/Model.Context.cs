﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace uCommerceNiceUrls.Core.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BaseContainer : DbContext, uCommerceNiceUrls.Core.Shared.Interfaces.Bases.IDbContext
    {
        public BaseContainer()
            : base("name=BaseContainer")
        {
        }
    	public new IDbSet<TEntity> Set<TEntity>() 
    		where TEntity : class
    	{
    		return base.Set<TEntity>();
    	}
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<NiceUrl_LanguageName> NiceUrl_LanguageName { get; set; }
        public DbSet<NiceUrl_Url> NiceUrl_Url { get; set; }
        public DbSet<uCommerce_Category> uCommerce_Category { get; set; }
        public DbSet<uCommerce_Product> uCommerce_Product { get; set; }
        public DbSet<uCommerce_ProductCatalog> uCommerce_ProductCatalog { get; set; }
    }
}
