using Abp.Application.Navigation;
using Abp.Localization;
using Cinotam.AbpModuleZero;

namespace SapModule.Application
{
    public class SapNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.Menus.Add("Sap", new MenuDefinition("Sap", L("Sap"))
                .AddItem(
                new MenuItemDefinition(
                    "SapSystem",
                    L("Sap"),
                     icon: "fa fa-lg fa-fw fa-tiles",
                    customData: new { ActionName = "ProjectsList", ControllerName = "Projects" }
                    )
                    .AddItem(
                    new MenuItemDefinition(
                        "Projects",
                        L("Projects"),
                        customData: new { ActionName = "ProjectsList", ControllerName = "Projects" }
                    ))


                ));
        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpModuleZeroConsts.LocalizationSourceName);
        }
    }
}
