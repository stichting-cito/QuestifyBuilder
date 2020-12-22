
Imports System.Xml.Linq
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass()> Public Class ParamValidator_Area_Tests : Inherits baseParamValidator

    <TestMethod(), TestCategory("Logic")>
    Public Sub DefaultTest()
        Dim prm = GetValidAreaParam()

        Dim result = prm.IsValid()

        Assert.IsTrue(result)
    End Sub

    Private Function GetValidAreaParam() As AreaParameter
        Dim ret = XmlDeserializeFromXelementWithDesignerSetting(Of AreaParameter)(validAreaParam)
        Return ret
    End Function

    Private validAreaParam As XElement = <AreaParameter name="itemQuestionArea">
                                             <designersetting key="group">4 Stam</designersetting>
                                             <designersetting key="sortkey">2</designersetting>
                                             <designersetting key="required">true</designersetting>
                                             <designersetting key="subsetidentifiers">Alphabetic</designersetting>
                                             <designersetting key="linkedresourceparametername">clickableImage</designersetting>
                                             <subparameterset id="A">
                                                 <resourceparameter name="clickableImage">UK.jpg</resourceparameter>
                                             </subparameterset>
                                             <definition>
                                                 <resourceparameter name="clickableImage">
                                                     <designersetting key="label">Plaatje met klikbare gebieden</designersetting>
                                                     <designersetting key="description">Selecteer de afbeelding</designersetting>
                                                     <designersetting key="required">true</designersetting>
                                                     <designersetting key="filter">image/png|image/jpeg|image/gif|image/x-png|image/pjpeg</designersetting>
                                                     <designersetting key="group">4 Stam</designersetting>
                                                     <designersetting key="editbuttonVisible">false</designersetting>
                                                     <designersetting key="deletebuttonVisible">false</designersetting>
                                                 </resourceparameter>
                                             </definition>
                                             <Shapes>
                                                 <Circle id="A" label="A" radius="69">
                                                     <AnchorPoint>
                                                         <X>89</X>
                                                         <Y>129</Y>
                                                     </AnchorPoint>
                                                 </Circle>
                                             </Shapes>
                                         </AreaParameter>

End Class