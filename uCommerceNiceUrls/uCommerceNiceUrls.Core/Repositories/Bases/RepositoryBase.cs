namespace uCommerceNiceUrls.Core.Repositories.Bases
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.Entity;
	using System.Linq;
	using System.Text;
	using uCommerceNiceUrls.Core.Shared.Interfaces.Bases;

	/// <summary>
	/// Base repository with basic CRUD actions.
	/// </summary>
	/// <typeparam name="T">Type of the entity, on which this repository is based.</typeparam>
	public class RepositoryBase<T> : IRepository<T>
		where T : class
	{
		/// <summary>
		/// Unit of work
		/// </summary>
		private IUnitOfWork unitOfWork = null;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RepositoryBase{T}" /> class, with no unit of work.
		/// </summary>
		protected RepositoryBase()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RepositoryBase{T}" /> class, with the applied unit of work.
		/// </summary>
		/// <param name="unitOfWork">Unit of work, this repository should utilize.</param>
		protected RepositoryBase(IUnitOfWork unitOfWork)
		{
			// Open the connection to the database.
			this.UnitOfWork = unitOfWork;
			this.ObjectSet = this.UnitOfWork.Context.Set<T>();
		}

		/// <summary>
		/// Gets or sets the unit of work, which this repository is using.
		/// </summary>
		public IUnitOfWork UnitOfWork
		{
			get
			{
				return this.unitOfWork;
			}

			set
			{
				this.unitOfWork = value;
				this.ObjectSet = this.UnitOfWork.Context.Set<T>();
			}
		}
		#endregion

		/// <summary>
		/// Gets the current db set for this repository.
		/// </summary>
		protected IDbSet<T> ObjectSet
		{
			get;
			private set;
		}

		#region IRepository<T> Members

		/// <summary>
		/// Gets all items from the current db set.
		/// </summary>
		/// <remarks>This could lead to excessive use outside the repository context. Use it with care!</remarks>
		/// <returns>All items from the db set.</returns>
		public virtual IQueryable<T> GetAll()
		{
			return this.ObjectSet;
		}

		/// <summary>
		/// Gets a single entity, found by the primary key.
		/// </summary>
		/// <param name="key">The primary key value, to locate an entity by.</param>
		/// <returns>The entity, if found, otherwise, null.</returns>
		public virtual T GetByKey(int key)
		{
			return this.ObjectSet.Find(key);
		}

		/// <summary>
		/// Adds a single entity to the db set, and saves it to the database.
		/// </summary>
		/// <param name="entity">Entity to insert.</param>
		/// <returns>The entity again, with updated values.</returns>
		public virtual T Insert(T entity)
		{
			this.ObjectSet.Add(entity);
			this.UnitOfWork.Context.SaveChanges();
			return entity;
		}

		/// <summary>
		/// Updates a single entity, and stores the new data in the database.
		/// </summary>
		/// <param name="entity">Entity to update.</param>
		/// <returns>The entity with updated values.</returns>
		public virtual T Update(T entity)
		{
			this.ObjectSet.Attach(entity);
			this.UnitOfWork.Context.Entry<T>(entity).State = EntityState.Modified;
			this.UnitOfWork.Context.SaveChanges();
			return entity;
		}

		/// <summary>
		/// Removes a single entity from the db set, and the database.
		/// </summary>
		/// <param name="entity">Entity to remove.</param>
		public virtual void Delete(T entity)
		{
			this.ObjectSet.Attach(entity);
			this.ObjectSet.Remove(entity);
			this.UnitOfWork.Context.SaveChanges();
		}

		#endregion
	}
}
