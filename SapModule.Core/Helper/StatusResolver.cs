using Abp.Localization;
using SapModule.Core.Helper.Enums;
using System;

namespace SapModule.Core.Helper
{
    public class StatusResolver : IStatusResolver
    {
        public string SourceName => SapConsts.LocalizationSourceName;
        private readonly ILocalizationManager _localizationManager;

        public StatusResolver(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }

        public string GetStatusName(StatusTypes.Status status)
        {
            switch (status)
            {
                case StatusTypes.Status.Success:
                    return _localizationManager.GetString(SapConsts.LocalizationSourceName, "Success");
                case StatusTypes.Status.Failed:
                    return _localizationManager.GetString(SapConsts.LocalizationSourceName, "Failed");
                case StatusTypes.Status.InProcess:
                    return _localizationManager.GetString(SapConsts.LocalizationSourceName, "InProcess");
                case StatusTypes.Status.Critical:
                    return _localizationManager.GetString(SapConsts.LocalizationSourceName, "Critical");
                case StatusTypes.Status.Paused:
                    return _localizationManager.GetString(SapConsts.LocalizationSourceName, "Paused");
                case StatusTypes.Status.OffTime:
                    return _localizationManager.GetString(SapConsts.LocalizationSourceName, "OffTime");
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        public int ResolveStatusToInt(StatusTypes.Status status)
        {
            return (int)status;
        }

        public StatusTypes.Status ResolveIntToStatus(int status)
        {
            return (StatusTypes.Status)status;
        }

        public StatusTypes.Status ResolveStatusCodeFromClient(string statusCode)
        {
            switch (statusCode)
            {
                case "Success":
                    return StatusTypes.Status.Success;
                case "Failed":
                    return StatusTypes.Status.Failed;
                case "Critical":
                    return StatusTypes.Status.Critical;
                case "Paused":
                    return StatusTypes.Status.Paused;
                case "InProcess":
                    return StatusTypes.Status.InProcess;
                default:
                    return StatusTypes.Status.InProcess;
            }
        }
    }
}
