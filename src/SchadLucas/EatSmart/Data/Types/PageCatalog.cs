using JetBrains.Annotations;

namespace SchadLucas.EatSmart.Data.Types
{
    public interface IPageCatalog
    {
        NavigationElement Foods { get; }

        NavigationElement Recipes { get; }

        NavigationElement Summary { get; }
    }

    [UsedImplicitly]
    public class PageCatalog : IPageCatalog
    {
        public NavigationElement Foods { get; }

        public NavigationElement Recipes { get; }

        public NavigationElement Summary { get; }

        public PageCatalog(NavigationElement foodsFactory, NavigationElement summaryFactory, NavigationElement recipesFactory)
        {
            Foods = foodsFactory;
            Summary = summaryFactory;
            Recipes = recipesFactory;
        }
    }
}