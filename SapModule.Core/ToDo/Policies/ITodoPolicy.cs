using Abp.Domain.Services;
using SapModule.Core.ToDo.Entities;

namespace SapModule.Core.ToDo.Policies
{
    public interface ITodoPolicy : IDomainService
    {
        void CheckTodoCreation(Todo input);
    }
}
