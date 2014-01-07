This folder should contain the edmx-file, and various code generating templates.

To setup edmx correctly, right click on the white background of the editor and click on the "Add Code Generation Item..."

Select the DbContext-item and give it a name, lets call it "Model".

Open the new Model.Context.tt, and find the line looking like: 
<#=Accessibility.ForType(container)#> partial class <#=Code.Escape(container)#> : DbContext

Change it to:
<#=Accessibility.ForType(container)#> partial class <#=Code.Escape(container)#> : DbContext, uCommerceNiceUrls.Core.Shared.Interfaces.Bases.IDbContext

Add the following to the class, right before the line "protected override void OnModelCreating(DbModelBuilder modelBuilder)":

	public new IDbSet<TEntity> Set<TEntity>() 
		where TEntity : class
	{
		return base.Set<TEntity>();
	}