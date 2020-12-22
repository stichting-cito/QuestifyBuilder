
using System;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Model.Entities;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.Service
{
    [TestClass]
    public class ReflectionHelperTests
    {
        [TestMethod]
        public void GetPropertyValueString_PropertyNotFound_ReturnsNull()
        {
            var item = new ItemResourceDto();

            var result = ReflectionHelper.GetPropertyValueString(item, "propName");

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetPropertyValueString_PropertyValueGevuld_ReturnsValue()
        {
            var item = new ItemResourceDto
            {
                ItemId = "1234"
            };

            var result = ReflectionHelper.GetPropertyValueString(item, "ItemId");

            Assert.AreEqual(item.ItemId, result);
        }

        [TestMethod]
        public void GetPropertyValueString_PropertyValueGevuldMetDatum_ReturnsValue()
        {
            var item = new ItemResourceDto
            {
                CreationDate = new DateTime(2017, 5, 8, 10, 12, 23)
            };

            var orgCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
            var result = ReflectionHelper.GetPropertyValueString(item, "creationDate");
            Thread.CurrentThread.CurrentCulture = orgCulture;

            Assert.AreEqual("5-8-2017 10:12", result);
        }

        [TestMethod]
        public void GetPropertyValueDecimal_PropertyNotFound_ReturnsNull()
        {
            var item = new ItemResourceDto();

            var result = ReflectionHelper.GetPropertyValueDecimal(item, "propName");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPropertyValueDecimal_ObjectIsNull_ReturnsNull()
        {
            var result = ReflectionHelper.GetPropertyValueDecimal(null, "responseCount");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPropertyValueDecimal_PropertyValueIsNull_ReturnsNull()
        {
            var item = new ItemResourceDto
            {
                ResponseCount = null
            };

            var result = ReflectionHelper.GetPropertyValueDecimal(item, "responseCount");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPropertyValueDecimal_PropertyValueIsGevuld_ReturnsPropertyValue()
        {
            var item = new ItemResourceDto
            {
                MaxScore = 1.23m
            };

            var result = ReflectionHelper.GetPropertyValueDecimal(item, "MaxScore");

            Assert.AreEqual(item.MaxScore, result);
        }
    }
}
