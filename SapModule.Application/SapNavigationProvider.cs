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
                    L("Sap")
                    )
                    .AddItem(new MenuItemDefinition("Projects", L("Projects")))


                ));
        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpModuleZeroConsts.LocalizationSourceName);
        }
    }
}
