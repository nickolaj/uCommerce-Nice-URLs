namespace uCommerceNiceUrls.Core.Shared.Interfaces.Bases
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Represents a repository factory.
	/// </summary>
	/// <typeparam name="TRepository">The repository on which this factory should handle.</typeparam>
	public interface IRepositoryFactory<TRepository>
		where TRepository : class, IRepository
	{
		/// <summary>
		/// Gets the repository, handled by this factory.
		/// </summary>
		TRepository Repository { get; }
	}
}
