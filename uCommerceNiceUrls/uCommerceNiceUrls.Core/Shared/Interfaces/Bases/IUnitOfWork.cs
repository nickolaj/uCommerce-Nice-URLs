namespace uCommerceNiceUrls.Core.Shared.Interfaces.Bases
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	/// <summary>
	/// Represents a unit of work.
	/// </summary>
	public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Gets the current context of this unit of work.
		/// </summary>
		IDbContext Context { get; }
	}
}
