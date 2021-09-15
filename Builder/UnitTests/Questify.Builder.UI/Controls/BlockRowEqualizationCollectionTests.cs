

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Questify.Builder.UI.Wpf.Controls;

namespace Questify.Builder.UnitTests.Questify.Builder.UI.Controls
{
    [TestClass]
    public class BlockRowEqualizationCollectionTests
    {
        [TestMethod]
        public void DoesNotBehaveDifferentWhenMaximumIsEqualToCollection()
        {
            //Arrange
            var strList = new List<String>(new []{"1","2","3"});
            var test = new BlockRowEqualizationCollection(strList, strList.Count);            
            
            //Act
            var result = new List<String>();
            foreach(var x in test)
            {
                result.Add(x.ToString());
            }
            
            //Assert
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void OnlyReturn_2_of_3()
        {
            //Arrange
            var strList = new List<String>(new[] { "1", "2", "3" });
            var test = new BlockRowEqualizationCollection(strList, 2);

            //Act
            var result = new List<String>();
            foreach (var x in test)
            {
                result.Add(x.ToString());
            }

            //Assert
            Assert.AreEqual(2, result.Count);
        }



        [TestMethod]
        public void Return_4_of_3_SoOneEmptyValue()
        {
            //Arrange
            var strList = new List<String>(new[] { "1", "2", "3" });
            var test = new BlockRowEqualizationCollection(strList, 4);

            //Act
            var result = new List<String>();
            foreach (var x in test)
            {
                result.Add( (x == null)? string.Empty: x.ToString());
            }

            //Assert
            Assert.AreEqual(4, result.Count);
        }
    }
}
