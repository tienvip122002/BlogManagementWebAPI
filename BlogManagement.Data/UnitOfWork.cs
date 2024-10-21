using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
	{
		BlogManagementWebAPIContext _blogManagementWebAPIContext;

		Repository<UserToken> _repositoryUserToken;
		Repository<User> _repositoryUser;
		Repository<Category> _repositoryCategory;


		private bool disposedValue;

		public UnitOfWork(BlogManagementWebAPIContext managementWebAPIContext)
		{
			_blogManagementWebAPIContext = managementWebAPIContext;
		}

		public Repository<UserToken> RepositoryUserToken => _repositoryUserToken ??= new Repository<UserToken>(_blogManagementWebAPIContext);
		public Repository<User> RepositoryUser => _repositoryUser ??= new Repository<User>(_blogManagementWebAPIContext);
		public Repository<Category> RepositoryCategory => _repositoryCategory ??= new Repository<Category>(_blogManagementWebAPIContext);

		public async Task CommitAsync()
		{
			await _blogManagementWebAPIContext.SaveChangesAsync();
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
					_blogManagementWebAPIContext.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~UnitOfWork()
		// {
		//     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		//     Dispose(disposing: false);
		// }

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
