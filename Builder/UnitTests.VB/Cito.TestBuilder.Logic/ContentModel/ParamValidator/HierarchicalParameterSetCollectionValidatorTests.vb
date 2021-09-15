
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports System.Xml.Serialization

<TestClass>
Public Class HierarchicalParameterSetCollectionValidatorTests

    <TestMethod(), TestCategory("Logic")>
    Public Sub ValidateEmpty_returns_True()
        'Arrange
        Dim parameterSetCol As New ParameterSetCollection
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub ValidatesPrmSetColl_WithEmptyPrmColl_returns_True()
        'Arrange
        Dim parameterSetCol As New ParameterSetCollection
        parameterSetCol.Add(New ParameterCollection())
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub XhtmlWith_NoData_AndRequired_IsNotValid()
        'Arrange
        Dim parameterSetCol = getParmsetWithColl(simple_Req_NoData_XhtmlPrm)
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub XhtmlWith_Data_AndRequired_IsValid()
        'Arrange
        Dim parameterSetCol = getParmsetWithColl(simple_Req_Data_XhtmlPrm)
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub XhtmlWith_NoData_AndRequired_ButNotVisible_IsValid()
        'Arrange
        Dim parameterSetCol = getParmsetWithColl(simple_ReqNotVis_NoData_XhtmlPrm)
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub XhtmlWith_Data_AndRequired_ButNotVisible_IsValid()
        'Arrange
        Dim parameterSetCol = getParmsetWithColl(simple_ReqNotVis_Data_XhtmlPrm)
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub VisibleCollection_And_ReqXhtml_noData_IsNotValid()
        'Arrange
        Dim parameterSetCol = getParmsetWithColl(collectionVisible_XhtmlReq_NoData)
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsFalse(result)
    End Sub


    <TestMethod(), TestCategory("Logic")>
    Public Sub VisibleCollection_And_ReqXhtml_Data_IsValid()
        'Arrange
        Dim parameterSetCol = getParmsetWithColl(collectionVisible_XhtmlReq_Data)
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub HiddenCollection_And_ReqXhtml_noData_IsValid()
        'Arrange
        'The fact that the XhtmlParam is required is nice, but since the parent parameter (the collection) is not visible
        'this will validate true.
        Dim parameterSetCol = getParmsetWithColl(collectionHidden_XhtmlReq_NoData)
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

#Region "Data"


    Private simple_Req_NoData_XhtmlPrm As XElement = <ArrayOfParameterCollection>
                                                         <ParameterCollection id="entireItem">
                                                             <xhtmlparameter name="xhtmlA">
                                                                 <designersetting key="required">true</designersetting>
                                                             </xhtmlparameter>
                                                         </ParameterCollection>
                                                     </ArrayOfParameterCollection>

    Private simple_Req_Data_XhtmlPrm As XElement = <ArrayOfParameterCollection>
                                                       <ParameterCollection id="entireItem">
                                                           <xhtmlparameter name="xhtmlA">
                                                               <designersetting key="required">true</designersetting>
                                                               some data
                                                           </xhtmlparameter>
                                                       </ParameterCollection>
                                                   </ArrayOfParameterCollection>


    Private simple_ReqNotVis_NoData_XhtmlPrm As XElement = <ArrayOfParameterCollection>
                                                               <ParameterCollection id="entireItem">
                                                                   <xhtmlparameter name="xhtmlA">
                                                                       <designersetting key="visible">false</designersetting>
                                                                       <designersetting key="required">true</designersetting>
                                                                   </xhtmlparameter>
                                                               </ParameterCollection>
                                                           </ArrayOfParameterCollection>

    Private simple_ReqNotVis_Data_XhtmlPrm As XElement = <ArrayOfParameterCollection>
                                                             <ParameterCollection id="entireItem">
                                                                 <xhtmlparameter name="xhtmlA">
                                                                     <designersetting key="visible">false</designersetting>
                                                                     <designersetting key="required">true</designersetting>
                                                               some data
                                                           </xhtmlparameter>
                                                             </ParameterCollection>
                                                         </ArrayOfParameterCollection>

    Private collectionVisible_XhtmlReq_NoData As XElement = <ArrayOfParameterCollection>
                                                                <ParameterCollection id="entireItem">
                                                                    <collectionparameter name="multiChoice">

                                                                        <definition>
                                                                            <xhtmlparameter name="abc">
                                                                                <designersetting key="required">true</designersetting>
                                                                            </xhtmlparameter>
                                                                        </definition>

                                                                        <subparameterset id="1">
                                                                            <xhtmlparameter name="abc">
                                                                                <designersetting key="required">true</designersetting>
                                                                            </xhtmlparameter>
                                                                        </subparameterset>

                                                                    </collectionparameter>
                                                                </ParameterCollection>
                                                            </ArrayOfParameterCollection>

    Private collectionVisible_XhtmlReq_Data As XElement = <ArrayOfParameterCollection>
                                                              <ParameterCollection id="entireItem">
                                                                  <collectionparameter name="multiChoice">

                                                                      <definition>
                                                                          <xhtmlparameter name="abc">
                                                                              <designersetting key="required">true</designersetting>
                                                                          </xhtmlparameter>
                                                                      </definition>

                                                                      <subparameterset id="1">
                                                                          <xhtmlparameter name="abc">
                                                                              <designersetting key="required">true</designersetting>
                                                                              data
                                                                          </xhtmlparameter>
                                                                      </subparameterset>

                                                                  </collectionparameter>
                                                              </ParameterCollection>
                                                          </ArrayOfParameterCollection>

    Private collectionHidden_XhtmlReq_NoData As XElement = <ArrayOfParameterCollection>
                                                               <ParameterCollection id="entireItem">
                                                                   <collectionparameter name="multiChoice">
                                                                       <designersetting key="visible">false</designersetting>

                                                                       <definition>
                                                                           <xhtmlparameter name="abc">
                                                                               <designersetting key="required">true</designersetting>
                                                                           </xhtmlparameter>
                                                                       </definition>

                                                                       <subparameterset id="1">
                                                                           <xhtmlparameter name="abc">
                                                                               <designersetting key="required">true</designersetting>
                                                                           </xhtmlparameter>
                                                                       </subparameterset>

                                                                   </collectionparameter>
                                                               </ParameterCollection>
                                                           </ArrayOfParameterCollection>

    Public Function XmlDeserializeFromXelementWithDesignerSetting(Of T)(ByVal xelement As XElement) As T
        Using stringreader As New IO.StringReader(xelement.ToString())

            Dim xml_overrides As New XmlAttributeOverrides
            Dim xml_attrbts As New XmlAttributes()
            xml_attrbts.XmlIgnore = False
            xml_attrbts.XmlElements.Add(New XmlElementAttribute("designersetting"))
            xml_overrides.Add(GetType(ParameterBase), "DesignerSettings", xml_attrbts)
            Dim ser As New XmlSerializer(GetType(T), xml_overrides)


            Dim obj As Object = ser.Deserialize(stringreader)
            stringreader.Close()
            stringreader.Dispose()
            Return DirectCast(obj, T)
        End Using
    End Function

    Private Function getParmsetWithColl(paramSetWithColl As XElement) As ParameterSetCollection
        Dim ret = XmlDeserializeFromXelementWithDesignerSetting(Of ParameterSetCollection)(paramSetWithColl)
        Return ret
    End Function

#End Region

End Class
