using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchadLucas.EatSmart.Services;
using System.Windows.Controls;

namespace SchadLucas.EatSmart.Tests.Services
{
    public class MyNavigationServiceTestsView : UserControl { }

    [TestClass]
    public class NavigationServiceTests
    {
        private IMessageHubService MessageHub { get; set; }
        private INavigationService SUT { get; set; }

        [TestMethod]
        public void HasNext()
        {
            Assert.IsFalse(SUT.HasNext);
            SUT.NavigateTo<MyNavigationServiceTestsView>();
            SUT.NavigateBackward();
            Assert.IsTrue(SUT.HasNext);
        }

        [TestMethod]
        public void HasPrevious()
        {
            Assert.IsFalse(SUT.HasPrevious);
            SUT.NavigateTo<MyNavigationServiceTestsView>();
            Assert.IsTrue(SUT.HasPrevious);
        }

        [TestInitialize]
        public void Initialize()
        {
            MessageHub = new MessageHubService();
            SUT = new NavigationService(MessageHub, null);
        }

        [TestMethod]
        public void Next()
        {
            SUT.NavigateTo<MyNavigationServiceTestsView>();
            SUT.NavigateBackward();
            Assert.IsNotNull(SUT.Next);
        }
    }
}