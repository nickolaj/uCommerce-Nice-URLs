namespace uCommerceNiceUrls.Core.Repositories.Bases
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using uCommerceNiceUrls.Core.Shared.Interfaces.Bases;

	/// <summary>
	/// Contains and maintains the current DbContext and current database connection.
	/// </summary>
	public class UnitOfWork : IUnitOfWork
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnitOfWork" /> class, and opens the database connection.
		/// </summary>
		public UnitOfWork()
		{
			// TODO: Implement below:
			this.Context = new Entities.BaseContainer();
			//throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the DbContext of this unit of work.
		/// </summary>
		public IDbContext Context { get; private set; }

		#region IDisposable Members

		/// <summary>
		/// Disposes the current DbContext and closes the connection.
		/// </summary>
		public void Dispose()
		{
			this.Context.Dispose();
		}

		#endregion
	}
}
