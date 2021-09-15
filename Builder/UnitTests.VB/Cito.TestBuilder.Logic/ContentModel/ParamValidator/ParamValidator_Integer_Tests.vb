
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass()> Public Class ParamValidator_Integer_Tests : Inherits baseParamValidator

    <TestMethod(), TestCategory("Logic")>
    Public Sub DefaultTest()
        'Arrange
        Dim prm = GetIntegerParam(<IntegerParameter name="itemQuestionArea">
                                  </IntegerParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RangeFrom_valid()
        'Arrange
        Dim prm = GetIntegerParam(<IntegerParameter name="itemQuestionArea">
                                      <designersetting key="rangeFrom">1</designersetting>2
                                       </IntegerParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RangeFrom_Invalid()
        'Arrange
        Dim prm = GetIntegerParam(<IntegerParameter name="itemQuestionArea">
                                      <designersetting key="rangeFrom">3</designersetting>
                                           2
                                       </IntegerParameter>)
       
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RangeTo_valid()
        'Arrange
        Dim prm = GetIntegerParam(<IntegerParameter name="itemQuestionArea">
                                      <designersetting key="rangeTo">10</designersetting>5
                                       </IntegerParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RangeTo_Invalid()
        'Arrange
        Dim prm = GetIntegerParam(<IntegerParameter name="itemQuestionArea">
                                      <designersetting key="rangeTo">10</designersetting>15
                                       </IntegerParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RangeFromTo_valid()
        'Arrange
        Dim prm = GetIntegerParam(<IntegerParameter name="itemQuestionArea">
                                      <designersetting key="rangeFrom">1</designersetting>
                                      <designersetting key="rangeTo">10</designersetting>5
                                       </IntegerParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub RangeFromTo_Invalid()
        'Arrange
        Dim prm = GetIntegerParam(<IntegerParameter name="itemQuestionArea">
                                      <designersetting key="rangeFrom">1</designersetting>
                                      <designersetting key="rangeTo">10</designersetting>15
                                       </IntegerParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    Private Function GetIntegerParam(x As XElement) As IntegerParameter
        Dim ret = XmlDeserializeFromXelementWithDesignerSetting(Of IntegerParameter)(x)
        Return ret
    End Function

End Class