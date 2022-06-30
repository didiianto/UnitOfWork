using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitOfWork.Models;

namespace UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Registration> RegistrationRepository { get; }
        Task SaveAsync(); 
    }
}
