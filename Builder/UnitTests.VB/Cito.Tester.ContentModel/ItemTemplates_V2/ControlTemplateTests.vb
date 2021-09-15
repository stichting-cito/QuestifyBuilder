
Imports System.Xml.Linq
Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel

<TestClass>
Public Class ControlTemplateTests

    <TestMethod()> <TestCategory("ContentModel")>
    Public Sub GetEnabledTargets_TemplateHas2Targets_1Enabled_Expects1()
        'Arrange
        Dim controlTemplate = SerializeHelper.XmlDeserializeFromString(Of ControlTemplate)(template.ToString())
      
        'Act
        Dim result = controlTemplate.GetEnabledTargetNames()
        
        'Assert
        Assert.AreEqual(2, controlTemplate.Targets.Count)
        Assert.AreEqual(1, result.Length)
    End Sub

#Region "Data"

    ReadOnly template As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="2">
                                        <Description></Description>
                                        <Targets>
                                            <Target xsi:type="ControlTemplateTarget" enabled="true" name="testPlayer">
                                                <Template>
                                                    <![CDATA[	]]>
                                                </Template>
                                            </Target>
                                        </Targets>
                                        <Targets>
                                            <Target xsi:type="ControlTemplateTarget" enabled="false" name="ces">
                                                <Template>
                                                    <![CDATA[	]]>
                                                </Template>
                                            </Target>
                                        </Targets>
                                        <SharedFunctions/>
                                        <SharedParameterSet>
                                            <booleanparameter name="bool1"/>
                                        </SharedParameterSet>
                                    </Template>
                                
#End Region

End Class
