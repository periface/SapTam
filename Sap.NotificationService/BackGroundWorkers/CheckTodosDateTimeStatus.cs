using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using SapModule.Core.Helper.Enums;
using SapModule.Core.ToDo.Entities;
using System;
using System.Linq;

namespace Sap.BackGroundWorkers.BackGroundWorkers
{
    public class CheckTodosDateTimeStatus : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<Todo> _todoRepository;
        private readonly IRepository<HistoryStatus> _historyRepository;
        public CheckTodosDateTimeStatus(AbpTimer timer, IRepository<Todo> todoRepository, IRepository<HistoryStatus> historyRepository) : base(timer)
        {
            _todoRepository = todoRepository;
            _historyRepository = historyRepository;
            Timer.Period = 60000;
        }
        [UnitOfWork]
        protected override void DoWork()
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var today = DateTime.Now;
                var allUnFinishedTodos = _todoRepository.GetAll();
                var found = (from todo in allUnFinishedTodos
                             where todo.HistoryStatuses.OrderByDescending(a => a.CreationTime).FirstOrDefault()
                                   != null &&
                                   todo.HistoryStatuses.OrderByDescending(a => a.CreationTime).FirstOrDefault().StatusType !=
                                   (int)StatusTypes.Status.Success
                             select todo).ToList();
                foreach (var allUnFinishedTodo in found)
                {
                    if (allUnFinishedTodo.DueDate.HasValue)
                    {
                        if (allUnFinishedTodo.DueDate < today)
                        {
                            //Time is over
                            allUnFinishedTodo.SetStatus(StatusTypes.Status.OffTime);
                            //Send mail to responsible



                        }
                        if (allUnFinishedTodo.DueDate == today)
                        {
                            //Is critical
                            allUnFinishedTodo.SetStatus(StatusTypes.Status.Critical);
                            //Send mail to responsible
                        }
                    }
                }
                CurrentUnitOfWork.SaveChanges();

            }

        }
    }
}
