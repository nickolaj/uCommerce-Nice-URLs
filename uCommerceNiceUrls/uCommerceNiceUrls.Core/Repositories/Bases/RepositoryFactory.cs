namespace uCommerceNiceUrls.Core.Repositories.Bases
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using uCommerceNiceUrls.Core.Shared.Interfaces.Bases;

	/// <summary>
	/// Repository factory, initializes a repository, and sets it up.
	/// </summary>
	/// <typeparam name="TRepository">Type of repository to initialize</typeparam>
	public class RepositoryFactory<TRepository> : IRepositoryFactory<TRepository>
		where TRepository : class, IRepository
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RepositoryFactory{TRepository}" /> class.
		/// Initializes the applied repository.
		/// </summary>
		/// <param name="unitOfWork">Unit of work, to utilize in the repository.</param>
		/// <param name="repository">The repository to initialize.</param>
		public RepositoryFactory(IUnitOfWork unitOfWork, TRepository repository)
		{
			((IRepository)repository).UnitOfWork = unitOfWork;

			this.Repository = repository;
		}

		#region IRepositoryFactory<TRepository> Members

		/// <summary>
		/// Gets the repository, maintained by this factory.
		/// </summary>
		public TRepository Repository
		{
			get;
			private set;
		}

		#endregion
	}
}
