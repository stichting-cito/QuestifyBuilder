
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass()> Public Class ParamValidator_ResourceParam_Tests : Inherits baseParamValidator

    <TestMethod(), TestCategory("Logic")>
    Public Sub DefaultTest()
        Dim prm = GetTextParam(<ResourceParameter name="itemQuestionArea">
                               </ResourceParameter>)

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ValueRequired_NoValue_IsInvalid()
        Dim prm = GetTextParam(<ResourceParameter name="itemQuestionArea">
                                   <designersetting key="required">true</designersetting>
                               </ResourceParameter>)

        Dim result = prm.IsValid()

        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ValueRequired_Value_Isvalid()
        Dim prm = GetTextParam(<ResourceParameter name="itemQuestionArea">
                                   <designersetting key="required">true</designersetting>picture.jpg
                               </ResourceParameter>)

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    Private Function GetTextParam(x As XElement) As ResourceParameter
        Dim ret = XmlDeserializeFromXelementWithDesignerSetting(Of ResourceParameter)(x)
        Return ret
    End Function

End Class