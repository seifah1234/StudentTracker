using StudentTracker.DAL.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await appDbContext.Database.BeginTransactionAsync(cancellationToken);
        }


        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await appDbContext.Database.CommitTransactionAsync(cancellationToken);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await appDbContext.Database.RollbackTransactionAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
