using Microsoft.AspNetCore.Mvc;

namespace TestMaxiT.Repository
{
    public interface IEmployeeRepository<T> where T : class
    {
        public Task<ActionResult<T>> Get(int id);
        public Task<ActionResult<T>> Create(T entity);
        public Task<ActionResult<T>> Update(int id, T entity);
        public Task<ActionResult> Delete(int id);
    }
}
