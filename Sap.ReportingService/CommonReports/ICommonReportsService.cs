using Abp.Application.Services;
using Sap.ReportingService.ContractDtos;

namespace Sap.ReportingService.CommonReports
{
    public interface ICommonReportsService : IApplicationService
    {
        LineGenericChartData GetTodosInProjectReport(int projectId);
        LineGenericChartData GetTodoListData(int projectId);
    }
}
