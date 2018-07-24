using System;
using FM.BLL;
using FM.Models;
using FM.Models.Responses;
using NUnit.Framework;

namespace FM.Tests
{
    [TestFixture]
    public class TaxTest
    {
        [Test]
        [TestCase("New York", "NY", 8.0)]//true
        public void TaxGetTest(string state, string abb, decimal taxi)
        {

            Tax tax = new Tax
            {
                StateName = state,
                StateAbbreviation = abb,
                TaxRate = taxi
            };

            TaxManager manager = TaxFactory.Create();
            TaxResponse response = manager.TaxRate(state);

            Assert.AreEqual(response.Tax.StateName, state);
            Assert.AreEqual(response.Tax.StateAbbreviation, abb);
            Assert.AreEqual(response.Tax.TaxRate, taxi);

        }
    }
}
