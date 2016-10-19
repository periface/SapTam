using Abp.Domain.Repositories;
using Sap.ReportingService.CommonReports.Dtos;
using Sap.ReportingService.ContractDtos;
using SapModule.Core.Helper.Enums;
using SapModule.Core.Projects.Entities;
using SapModule.Core.ToDo.Entities;
using SapModule.Core.ToDoLists.Entities;
using System.Linq;

namespace Sap.ReportingService.CommonReports
{

    public class CommonReportsService : ICommonReportsService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ToDoList> _todoListRepository;
        private readonly IRepository<Todo> _todoRepository;
        public CommonReportsService(IRepository<Project> projectRepository, IRepository<ToDoList> todoListRepository, IRepository<Todo> todoRepository)
        {
            _projectRepository = projectRepository;
            _todoListRepository = todoListRepository;
            _todoRepository = todoRepository;
        }

        public LineGenericChartData GetTodosInProjectReport(int projectId)
        {
            var report = new AllTodoReports("Tareas", "ChartJs");
            var project = _projectRepository.Get(projectId);
            report.AddLabel(project.Name);


            var todos = _todoRepository.GetAllList(a => a.TodoList.Project.Id == projectId);

            var todosInProcess = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.InProcess) != null).ToList();
            if (todosInProcess.Any())
            {
                report.DataSets.Add(DataSet.CreateDataSet("En proceso", todosInProcess.Count()));
            }
            var todosFinished = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.Success) != null).ToList();

            if (todosFinished.Any())
            {
                report.DataSets.Add(DataSet.CreateDataSet("Finalizados", todosFinished.Count()));
            }


            var todosPaused = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.Paused) != null).ToList();
            if (todosPaused.Any())
            {
                report.DataSets.Add(DataSet.CreateDataSet("Pausados", todosPaused.Count()));

            }

            var todosFailed = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.Failed) != null).ToList();
            if (todosFailed.Any())
            {
                report.DataSets.Add(DataSet.CreateDataSet("Fallados", todosFailed.Count()));

            }
            var todosOffTime = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.OffTime) != null).ToList();

            if (todosOffTime.Any())
            {
                report.DataSets.Add(DataSet.CreateDataSet("Fuera de Tiempo", todosOffTime.Count()));

            }
            var todosCriticalTime = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.Critical) != null).ToList();

            if (todosCriticalTime.Any())
            {
                report.DataSets.Add(DataSet.CreateDataSet("Critico", todosCriticalTime.Count()));
            }
            return report;
        }

        public LineGenericChartData GetTodoListData(int projectId)
        {
            var report = new AllTodoReports("Listas de Tareas", "ChartJs");
            var todoLists = _todoListRepository.GetAllList(a => a.Project.Id == projectId).ToList();

            var inProcessDataSet = DataSet.CreateDataSet("En Proceso");
            var finishedDataSet = DataSet.CreateDataSet("Terminados");
            var pausedDataSet = DataSet.CreateDataSet("Pausados");
            var failedDataSet = DataSet.CreateDataSet("Fallidos");
            var offTimeDataSet = DataSet.CreateDataSet("Fuera de tiempo");
            var criticalTimeDataSet = DataSet.CreateDataSet("Critico");
            foreach (var todoList in todoLists)
            {
                var todos = _todoRepository.GetAllList(a => a.TodoList.Id == todoList.Id).ToList();
                if (todos.Any())
                {
                    var name = todoList.Name;
                    if (todoList.Name.Length > 25)
                    {
                        name = todoList.Name.Substring(0, 24);
                    }
                    report.AddLabel(name);

                    var todosInProcess = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.InProcess) != null).ToList();

                    inProcessDataSet.AddData(todosInProcess.Count);

                    var todosFinished = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.Success) != null).ToList();

                    finishedDataSet.AddData(todosFinished.Count);

                    var todosPaused = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.Paused) != null).ToList();
                    pausedDataSet.AddData(todosPaused.Count);

                    var todosFailed = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.Failed) != null).ToList();

                    failedDataSet.AddData(todosFailed.Count);

                    var todosOffTime = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.OffTime) != null).ToList();

                    offTimeDataSet.AddData(todosOffTime.Count);

                    var todosCriticalTime = todos.Where(a => a.HistoryStatuses.OrderByDescending(s => s.CreationTime).FirstOrDefault(s => s.StatusType == (int)StatusTypes.Status.Critical) != null).ToList();

                    criticalTimeDataSet.AddData(todosCriticalTime.Count);

                }

            }
            report.AddDataSet(inProcessDataSet);
            report.AddDataSet(finishedDataSet);
            report.AddDataSet(offTimeDataSet);
            report.AddDataSet(failedDataSet);
            report.AddDataSet(pausedDataSet);
            report.AddDataSet(criticalTimeDataSet);
            return report;
        }
    }
}
