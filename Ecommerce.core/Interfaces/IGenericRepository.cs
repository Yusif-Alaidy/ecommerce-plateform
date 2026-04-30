using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecommerce.core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Get all ================================================================================
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);  
        // ========================================================================================

        // Get one ================================================================================
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        // ========================================================================================

        // Create =================================================================================
        Task AddAsync(T entity);
        // ========================================================================================

        // Update =================================================================================
        Task UpdateAsync(T entity);
        // ========================================================================================

        // DeleteAsync ============================================================================
        Task DeleteAsync(int id);
        // ========================================================================================


    }
}
