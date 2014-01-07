namespace uCommerceNiceUrls.Core.Shared.Interfaces.Bases
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Represents a DbContext.
	/// </summary>
	public interface IDbContext : IDisposable
	{
		/// <summary>
		/// Gets a System.Data.Entity.Infrastructure.DbEntityEntry&gt;TEntity&lt; object for
		/// the given entity providing access to information about the entity and the
		/// ability to perform actions on the entity.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="entity">The entity.</param>
		/// <returns>An entry for the entity.</returns>
		DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

		/// <summary>
		///  Returns a DbSet instance for access to entities of the given type in the
		///  context, the ObjectStateManager, and the underlying store.
		/// </summary>
		/// <typeparam name="TEntity">The type entity for which a set should be returned.</typeparam>
		/// <returns>A set for the given entity type.</returns>
		/// <remarks>See the DbSet class for more details.</remarks>
		IDbSet<TEntity> Set<TEntity>() where TEntity : class;

		/// <summary>
		/// Saves all changes made in this context to the underlying database.
		/// </summary>
		/// <returns>The number of objects written to the underlying database.</returns>
		/// <exception cref="System.InvalidOperationException">Thrown if the context has been disposed.</exception>
		int SaveChanges();
	}
}
