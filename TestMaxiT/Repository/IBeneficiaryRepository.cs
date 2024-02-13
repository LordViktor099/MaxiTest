using Microsoft.AspNetCore.Mvc;

namespace TestMaxiT.Repository
{
    public interface IBeneficiaryRepository<T> where T : class 
    {
        public Task<ActionResult<T>> Get(int id);
        public Task<ActionResult<T>> Create(T entity, int employeeId);
        public Task<ActionResult> Update(int id, T entity);
        public Task<ActionResult> Delete(int id);
    }
}
