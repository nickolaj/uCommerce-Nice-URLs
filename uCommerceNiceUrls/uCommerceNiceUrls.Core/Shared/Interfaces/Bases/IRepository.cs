namespace uCommerceNiceUrls.Core.Shared.Interfaces.Bases
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Represents a generic repository.
	/// </summary>
	/// <typeparam name="T">The on which this interface is build upon.</typeparam>
	public interface IRepository<T> : IRepository
	{
		/// <summary>
		/// Gets all items of the type <see cref="{T}"/>.
		/// </summary>
		/// <remarks>This could lead to excessive use outside the repository context. Use it with care!</remarks>
		/// <returns>All items of the type <see cref="{T}"/>.</returns>
		IQueryable<T> GetAll();

		/// <summary>
		/// Gets a single entity, found by the primary key.
		/// </summary>
		/// <param name="entityKey">The primary key value, to locate an entity by.</param>
		/// <returns>The entity, if found, otherwise, null.</returns>
		T GetByKey(int entityKey);

		/// <summary>
		/// Adds a single entity of the type <see cref="{T}"/> to the repository.
		/// </summary>
		/// <param name="entity">Entity to add.</param>
		/// <returns>The entity.</returns>
		T Insert(T entity);

		/// <summary>
		/// Updates a single entity of the type <see cref="{T}"/> in the repository.
		/// </summary>
		/// <param name="entity">Entity to update.</param>
		/// <returns>The entity of the type <see cref="{T}"/>.</returns>
		T Update(T entity);

		/// <summary>
		/// Removes a single entity of the type <see cref="{T}"/>, from the repository.
		/// </summary>
		/// <param name="entity">Entity to remove.</param>
		void Delete(T entity);
	}

	/// <summary>
	/// Represents a basic repository.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Gets or sets the unit of work, for this repository.
		/// </summary>
		IUnitOfWork UnitOfWork { get; set; }
	}
}
