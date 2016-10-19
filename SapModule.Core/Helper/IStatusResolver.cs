using Abp.Domain.Services;
using SapModule.Core.Helper.Enums;

namespace SapModule.Core.Helper
{
    public interface IStatusResolver : IDomainService
    {
        string SourceName { get; }
        string GetStatusName(StatusTypes.Status status);
        int ResolveStatusToInt(StatusTypes.Status status);
        StatusTypes.Status ResolveIntToStatus(int status);
        StatusTypes.Status ResolveStatusCodeFromClient(string statusCode);
    }
}
