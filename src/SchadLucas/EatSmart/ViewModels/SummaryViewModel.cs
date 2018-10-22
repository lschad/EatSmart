using SchadLucas.EatSmart.Data.Models;
using JetBrains.Annotations;

namespace SchadLucas.EatSmart.ViewModels
{
    public sealed class SummaryViewModel : ViewModel, ISummaryViewModel
    {
        private ISummaryModel _model;
        private string _test;

        public string Test
        {
            get => _test;
            set => SetField(ref _test, value);
        }

        [UsedImplicitly]
        public SummaryViewModel(ISummaryModel model)
        {
            _model = model;
            Test = "hahahahahah updated";
        }
    }
}