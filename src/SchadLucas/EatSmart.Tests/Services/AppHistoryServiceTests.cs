using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace SchadLucas.EatSmart.Tests.Services
{
    [TestClass]
    public class AppHistoryServiceTests
    {
        public int TestProperty => 123;

        [TestMethod]
        public void xd()
        {
            OnPropertyChanging(() => TestProperty);
        }

        private void OnPropertyChanging<TProperty>(Expression<Func<TProperty>> p)
        {
            var e = (MemberExpression)p.Body;
            Assert.AreEqual(nameof(TestProperty), e.Member.Name);
        }
    }
}