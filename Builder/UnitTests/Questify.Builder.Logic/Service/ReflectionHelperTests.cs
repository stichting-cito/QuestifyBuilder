
using System;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.Logic.Service.HelperFunctions;
using Questify.Builder.Logic.Service.Model.Entities;

namespace Questify.Builder.UnitTests.Questify.Builder.Logic.Service
{
    /// <summary>
    /// Unit tests for the class ReflectionHelper
    /// </summary>
    [TestClass]
    public class ReflectionHelperTests
    {
        [TestMethod]
        public void GetPropertyValueString_PropertyNotFound_ReturnsNull()
        {
            //Arrange
            var item = new ItemResourceDto();

            //Act
            var result = ReflectionHelper.GetPropertyValueString(item, "propName");

            //Assert
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetPropertyValueString_PropertyValueGevuld_ReturnsValue()
        {
            //Arrange
            var item = new ItemResourceDto
            {
                ItemId = "1234"
            };

            //Act
            var result = ReflectionHelper.GetPropertyValueString(item, "ItemId");

            //Assert
            Assert.AreEqual(item.ItemId, result);
        }

        [TestMethod]
        public void GetPropertyValueString_PropertyValueGevuldMetDatum_ReturnsValue()
        {
            //Arrange
            var item = new ItemResourceDto
            {
                CreationDate = new DateTime(2017, 5, 8, 10, 12, 23)
            };

            //Act
            var orgCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");
            var result = ReflectionHelper.GetPropertyValueString(item, "creationDate");
            Thread.CurrentThread.CurrentCulture = orgCulture;

            //Assert
            Assert.AreEqual("5-8-2017 10:12", result);
        }

        [TestMethod]
        public void GetPropertyValueDecimal_PropertyNotFound_ReturnsNull()
        {
            //Arrange
            var item = new ItemResourceDto();

            //Act
            var result = ReflectionHelper.GetPropertyValueDecimal(item, "propName");

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPropertyValueDecimal_ObjectIsNull_ReturnsNull()
        {
            //Act
            var result = ReflectionHelper.GetPropertyValueDecimal(null, "responseCount");

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPropertyValueDecimal_PropertyValueIsNull_ReturnsNull()
        {
            //Arrange
            var item = new ItemResourceDto
            {
                ResponseCount = null
            };

            //Act
            var result = ReflectionHelper.GetPropertyValueDecimal(item, "responseCount");

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetPropertyValueDecimal_PropertyValueIsGevuld_ReturnsPropertyValue()
        {
            //Arrange
            var item = new ItemResourceDto
            {
                MaxScore = 1.23m
            };

            //Act
            var result = ReflectionHelper.GetPropertyValueDecimal(item, "MaxScore");

            //Assert
            Assert.AreEqual(item.MaxScore, result);
        }
    }
}
