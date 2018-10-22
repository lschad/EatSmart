using JetBrains.Annotations;
using SchadLucas.EatSmart.Services;

namespace SchadLucas.EatSmart.Data.Models
{
    [UsedImplicitly]
    public class SummaryModel : Model, ISummaryModel
    {
        public string[] Names => new[] { "adam", "berta", "kasper", "michel", "toprak" };

        public SummaryModel(IDbInMemoryFactoryService contextFactory) : base(contextFactory)
        {
        }
    }
}