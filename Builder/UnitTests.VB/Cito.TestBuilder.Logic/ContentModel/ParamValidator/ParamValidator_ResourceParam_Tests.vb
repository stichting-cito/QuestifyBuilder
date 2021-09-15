
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass()> Public Class ParamValidator_ResourceParam_Tests : Inherits baseParamValidator

    <TestMethod(), TestCategory("Logic")>
    Public Sub DefaultTest()
        'Arrange
        Dim prm = GetTextParam(<ResourceParameter name="itemQuestionArea">
                               </ResourceParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ValueRequired_NoValue_IsInvalid()
        'Arrange
        Dim prm = GetTextParam(<ResourceParameter name="itemQuestionArea">
                                   <designersetting key="required">true</designersetting>
                               </ResourceParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ValueRequired_Value_Isvalid()
        'Arrange
        Dim prm = GetTextParam(<ResourceParameter name="itemQuestionArea">
                                   <designersetting key="required">true</designersetting>picture.jpg
                               </ResourceParameter>)
        
        'Act
        Dim result = prm.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    Private Function GetTextParam(x As XElement) As ResourceParameter
        Dim ret = XmlDeserializeFromXelementWithDesignerSetting(Of ResourceParameter)(x)
        Return ret
    End Function

End Class