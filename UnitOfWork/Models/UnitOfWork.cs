using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitOfWork.Models;
using UnitOfWork.Interfaces;

namespace UnitOfWork.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext context;
        public UnitOfWork(ApplicationDBContext _context)
        {
            this.context = _context;
        }

        #region Repo
        private IRepository<Registration> registrationRepository;

        public IRepository<Registration> RegistrationRepository => registrationRepository ?? new Repository<Registration>(context);
        #endregion

        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}
