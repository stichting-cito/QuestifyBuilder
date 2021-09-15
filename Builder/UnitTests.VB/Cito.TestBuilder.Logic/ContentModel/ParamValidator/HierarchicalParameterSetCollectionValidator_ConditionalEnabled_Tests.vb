
Imports Questify.Builder.Logic.ContentModel
Imports Cito.Tester.ContentModel
Imports System.Xml.Linq
Imports System.Xml.Serialization

<TestClass>
Public Class HierarchicalParameterSetCollectionValidator_ConditionalEnabled_Tests

	<TestMethod(), TestCategory("Logic")>
	Public Sub XHtmlIsEnabled_NoData_IsNotValid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(simple_Req_NoData_XhtmlPrm)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsFalse(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
	Public Sub XHtmlIsEnabled_NoData_CaseInsensitivity_IsStillNotValid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(simple_Req_NoData_XhtmlPrm_CaseTest)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsFalse(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
	Public Sub XHtmlIsEnabled_Data_IsValid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(simple_Req_Data_XhtmlPrm)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsTrue(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
	Public Sub XHtmlIsDisabled_NoData_IsValid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(simple_Req_NoData_XhtmlPrm_NotEnabled)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsTrue(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
	Public Sub XHtmlIsDisabled_NoData_CaseInsensitivity_IsValid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(simple_Req_NoData_XhtmlPrm_CaseTest_NotEnabled)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsTrue(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
    Public Sub AreaParam_ConditionalDisabled_ResourceParamNotVisible_IsValid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(AreaParam_ConditionalDisabled_ResourceParamNotVisible)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsTrue(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
    Public Sub AreaParam_ConditionalEnabled_ResourceParamVisible_HasNoData_IsInvalid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(AreaParam_ConditionalEnabled_ResourceParamVisible)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsFalse(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
 Public Sub AreaParam_ConditionalEnabled_ResourceParamVisible_NotRequired_HasNoData_IsValid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(AreaParam_ConditionalEnabled_ResourceParamVisible_NotRequired)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsTrue(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
    Public Sub AreaParam_ConditionalEnabled_WithOrValue_ResourceParamVisible_Required_HasNoData_IsInvalid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(AreaParam_ConditionalEnabled_WithOrValue_ResourceParamVisible_Required)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub AreaParam_ConditionalEnabled_WithOrValue_ResourceParamVisible_Required_HasData_ButNoShapes_IsInvalid()
        'Arrange
        Dim parameterSetCol = getParmsetWithColl(AreaParam_ConditionalEnabled_WithOrValue_ResourceParamVisible_Required_NoShapes)
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsFalse(result)
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub AreaParam_ConditionalEnabled_WithOrValue_ResourceParamVisible_Required_HasData_AndShapes_IsValid()
        'Arrange
        Dim parameterSetCol = getParmsetWithColl(AreaParam_ConditionalEnabled_WithOrValue_ResourceParamVisible_Required_WithShapes)
        Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
        
        'Act
        Dim result = validator.Validate()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

	<TestMethod(), TestCategory("Logic")>
    Public Sub AreaParam_ConditionalEnabled_WithNotValue_ResourceParamVisible_Required_HasNoData_IsInvalid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(AreaParam_ConditionalEnabled_WithNotValue_ResourceParamVisible_Required)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsFalse(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
    Public Sub AreaParam_ConditionalEnabled_WithNotValue_ResourceParamNotVisible_Required_HasNoData_IsValid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(AreaParam_ConditionalEnabled_WithNotValue_ResourceParamNotVisible)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsTrue(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
    Public Sub AreaParam_ConditionalEnabled_WithEmptyValue_ResourceParamVisible_Required_HasNoData_IsInvalid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(AreaParam_ConditionalEnabled_WithEmptyValue_ResourceParamVisible_Required)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsFalse(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
    Public Sub AreaParam_ConditionalEnabled_WithNotEmpty_ResourceParamVisible_Required_HasNoData_IsInvalid()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(AreaParam_ConditionalEnabled_WithNotEmpty_ResourceParamVisible_Required)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		Dim result = validator.Validate()
		
	    'Assert
		Assert.IsFalse(result)
	End Sub

	<TestMethod(), TestCategory("Logic")>
	<ExpectedException(GetType(ArgumentException))>
	Public Sub Error_NoParameterDefined_ExpectsException()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(Error_NoParameterDefined)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		validator.Validate() 'Exception here
		
	    'Assert
		'Expects Exception
	End Sub


	<TestMethod(), TestCategory("Logic")>
	<ExpectedException(GetType(ArgumentException))>
	Public Sub Error_NoValueDefined_ExpectsException()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(Error_NoValueDefined)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		validator.Validate() 'Exception here
		
	    'Assert
		'Expects Exception
	End Sub

	<TestMethod(), TestCategory("Logic")>
<ExpectedException(GetType(KeyNotFoundException))>
	Public Sub Error_OtherParamNotYetDefined_ExpectsException()
		'Arrange
		Dim parameterSetCol = getParmsetWithColl(Error_OtherParamNotYetDefined)
		Dim validator As New HierarchicalParameterSetCollectionValidator(parameterSetCol)
		
	    'Act
		validator.Validate() 'Exception here
		
	    'Assert
		'Expects Exception
	End Sub


#Region "Data"

	Private simple_Req_NoData_XhtmlPrm As XElement = <ArrayOfParameterCollection>
														 <ParameterCollection id="entireItem">
															 <booleanparameter name="A">true</booleanparameter>
															 <xhtmlparameter name="B">
																 <designersetting key="required">true</designersetting>
																 <designersetting key="conditionalEnabled">true</designersetting>
																 <designersetting key="conditionalEnabledSwitchParameter">A</designersetting>
																 <designersetting key="conditionalEnabledWhenValue">true</designersetting>
															 </xhtmlparameter>
														 </ParameterCollection>
													 </ArrayOfParameterCollection>

	Private simple_Req_Data_XhtmlPrm As XElement = <ArrayOfParameterCollection>
													   <ParameterCollection id="entireItem">
														   <booleanparameter name="A">true</booleanparameter>
														   <xhtmlparameter name="B">
															   <designersetting key="required">true</designersetting>
															   <designersetting key="conditionalEnabled">true</designersetting>
															   <designersetting key="conditionalEnabledSwitchParameter">A</designersetting>
															   <designersetting key="conditionalEnabledWhenValue">true</designersetting>
                                                               Some data
                                                           </xhtmlparameter>
													   </ParameterCollection>
												   </ArrayOfParameterCollection>

	Private simple_Req_NoData_XhtmlPrm_CaseTest As XElement = <ArrayOfParameterCollection>
																  <ParameterCollection id="entireItem">
																	  <booleanparameter name="A">true</booleanparameter>
																	  <xhtmlparameter name="B">
																		  <designersetting key="required">true</designersetting>
																		  <designersetting key="conditionalEnabled">true</designersetting>
																		  <designersetting key="conditionalEnabledSwitchParameter">A</designersetting>
																		  <designersetting key="conditionalEnabledWhenValue">tRuE</designersetting>
																	  </xhtmlparameter>
																  </ParameterCollection>
															  </ArrayOfParameterCollection>

	Private simple_Req_NoData_XhtmlPrm_NotEnabled As XElement = <ArrayOfParameterCollection>
																	<ParameterCollection id="entireItem">
																		<booleanparameter name="A">true</booleanparameter>
																		<xhtmlparameter name="B">
																			<designersetting key="required">true</designersetting>
																			<designersetting key="conditionalEnabled">true</designersetting>
																			<designersetting key="conditionalEnabledSwitchParameter">A</designersetting>
																			<designersetting key="conditionalEnabledWhenValue">false</designersetting>
																		</xhtmlparameter>
																	</ParameterCollection>
																</ArrayOfParameterCollection>

	Private simple_Req_NoData_XhtmlPrm_CaseTest_NotEnabled As XElement = <ArrayOfParameterCollection>
																			 <ParameterCollection id="entireItem">
																				 <booleanparameter name="A">false</booleanparameter>
																				 <xhtmlparameter name="B">
																					 <designersetting key="required">true</designersetting>
																					 <designersetting key="conditionalEnabled">true</designersetting>
																					 <designersetting key="conditionalEnabledSwitchParameter">A</designersetting>
																					 <designersetting key="conditionalEnabledWhenValue">tRuE</designersetting>
																				 </xhtmlparameter>
																			 </ParameterCollection>
																		 </ArrayOfParameterCollection>

	Private Error_NoParameterDefined As XElement = <ArrayOfParameterCollection>
													   <ParameterCollection id="entireItem">
														   <xhtmlparameter name="B">
															   <designersetting key="required">true</designersetting>
															   <designersetting key="conditionalEnabled">true</designersetting>
															   <designersetting key="conditionalEnabledSwitchParameter"></designersetting>
															   <designersetting key="conditionalEnabledWhenValue">false</designersetting>
														   </xhtmlparameter>
													   </ParameterCollection>
												   </ArrayOfParameterCollection>

	Private Error_NoValueDefined As XElement = <ArrayOfParameterCollection>
												   <ParameterCollection id="entireItem">
													   <xhtmlparameter name="B">
														   <designersetting key="required">true</designersetting>
														   <designersetting key="conditionalEnabled">true</designersetting>
														   <designersetting key="conditionalEnabledSwitchParameter">A</designersetting>
														   <designersetting key="conditionalEnabledWhenValue"></designersetting>
													   </xhtmlparameter>
												   </ParameterCollection>
											   </ArrayOfParameterCollection>

	Private Error_OtherParamNotYetDefined As XElement = <ArrayOfParameterCollection>
															<ParameterCollection id="entireItem">
																<xhtmlparameter name="B">
																	<designersetting key="required">true</designersetting>
																	<designersetting key="conditionalEnabled">true</designersetting>
																	<designersetting key="conditionalEnabledSwitchParameter">A</designersetting>
																	<designersetting key="conditionalEnabledWhenValue">true</designersetting>
																</xhtmlparameter>
																<booleanparameter name="A">false</booleanparameter>
															</ParameterCollection>
														</ArrayOfParameterCollection>

	Private AreaParam_ConditionalDisabled_ResourceParamNotVisible As XElement = <ArrayOfParameterCollection>
																					<ParameterCollection id="entireItem">
																						<listedparameter name="alternativesEditor">
																							<designersetting key="visible">true</designersetting>mc</listedparameter>
																						<areaparameter name="area">
																							<designersetting key="conditionalEnabled">true</designersetting>
																							<designersetting key="conditionalEnabledSwitchParameter">alternativesEditor</designersetting>
																							<designersetting key="conditionalEnabledWhenValue">hotspotDrawing</designersetting>
																							<definition>
																								<resourceparameter name="clickableImage">
																									<designersetting key="required">true</designersetting>
																								</resourceparameter>
																							</definition>
																						</areaparameter>
																					</ParameterCollection>
																				</ArrayOfParameterCollection>

	Private AreaParam_ConditionalEnabled_ResourceParamVisible As XElement = <ArrayOfParameterCollection>
																				<ParameterCollection id="entireItem">
																					<listedparameter name="alternativesEditor">
																						<designersetting key="visible">true</designersetting>hotspotDrawing</listedparameter>
																					<areaparameter name="area">
																						<designersetting key="conditionalEnabled">true</designersetting>
																						<designersetting key="conditionalEnabledSwitchParameter">alternativesEditor</designersetting>
																						<designersetting key="conditionalEnabledWhenValue">hotspotDrawing</designersetting>
																						<definition>
																							<resourceparameter name="clickableImage">
																								<designersetting key="required">true</designersetting>
																							</resourceparameter>
																						</definition>
																						<subparameterset id="1">
																							<resourceparameter name="clickableImage">
																								<designersetting key="required">true</designersetting>
																							</resourceparameter>
																						</subparameterset>
																					</areaparameter>
																				</ParameterCollection>
																			</ArrayOfParameterCollection>

	Private AreaParam_ConditionalEnabled_ResourceParamVisible_NotRequired As XElement = <ArrayOfParameterCollection>
																							<ParameterCollection id="entireItem">
																								<listedparameter name="alternativesEditor">
																									<designersetting key="visible">true</designersetting>hotspotDrawing</listedparameter>
																								<areaparameter name="area">
																									<designersetting key="conditionalEnabled">true</designersetting>
																									<designersetting key="conditionalEnabledSwitchParameter">alternativesEditor</designersetting>
																									<designersetting key="conditionalEnabledWhenValue">hotspotDrawing</designersetting>
																									<definition>
																										<resourceparameter name="clickableImage">
																										</resourceparameter>
																									</definition>
																									<subparameterset id="1">
																										<resourceparameter name="clickableImage">
																										</resourceparameter>
																									</subparameterset>
																								</areaparameter>
																							</ParameterCollection>
																						</ArrayOfParameterCollection>

    Private AreaParam_ConditionalEnabled_WithOrValue_ResourceParamVisible_Required As XElement = <ArrayOfParameterCollection>
                                                                                                     <ParameterCollection id="entireItem">
                                                                                                         <listedparameter name="alternativesEditor">
                                                                                                             <designersetting key="visible">true</designersetting>hotspotDrawing</listedparameter>
                                                                                                         <areaparameter name="area">
                                                                                                             <designersetting key="conditionalEnabled">true</designersetting>
                                                                                                             <designersetting key="conditionalEnabledSwitchParameter">alternativesEditor</designersetting>
                                                                                                             <designersetting key="conditionalEnabledWhenValue">mc|hotspotDrawing</designersetting>
                                                                                                             <definition>
                                                                                                                 <resourceparameter name="clickableImage">
                                                                                                                     <designersetting key="required">true</designersetting>
                                                                                                                 </resourceparameter>
                                                                                                             </definition>
                                                                                                             <subparameterset id="1">
                                                                                                                 <resourceparameter name="clickableImage">
                                                                                                                     <designersetting key="required">true</designersetting>
                                                                                                                 </resourceparameter>
                                                                                                             </subparameterset>
                                                                                                         </areaparameter>
                                                                                                     </ParameterCollection>
                                                                                                 </ArrayOfParameterCollection>

    Private AreaParam_ConditionalEnabled_WithOrValue_ResourceParamVisible_Required_NoShapes As XElement = <ArrayOfParameterCollection>
                                                                                                              <ParameterCollection id="entireItem">
                                                                                                                  <listedparameter name="alternativesEditor">
                                                                                                                      <designersetting key="visible">true</designersetting>hotspotDrawing</listedparameter>
                                                                                                                  <areaparameter name="area">
                                                                                                                      <designersetting key="conditionalEnabled">true</designersetting>
                                                                                                                      <designersetting key="conditionalEnabledSwitchParameter">alternativesEditor</designersetting>
                                                                                                                      <designersetting key="conditionalEnabledWhenValue">mc|hotspotDrawing</designersetting>
                                                                                                                      <designersetting key="required">true</designersetting>
                                                                                                                      <definition>
                                                                                                                          <resourceparameter name="clickableImage">
                                                                                                                              <designersetting key="required">true</designersetting>
                                                                                                                          </resourceparameter>
                                                                                                                      </definition>
                                                                                                                      <subparameterset id="1">
                                                                                                                          <resourceparameter name="clickableImage">UK.jpg
                                                                                                                                <designersetting key="required">true</designersetting>
                                                                                                                          </resourceparameter>
                                                                                                                      </subparameterset>
                                                                                                                      <Shapes/>
                                                                                                                  </areaparameter>
                                                                                                              </ParameterCollection>
                                                                                                          </ArrayOfParameterCollection>

    Private AreaParam_ConditionalEnabled_WithOrValue_ResourceParamVisible_Required_WithShapes As XElement = <ArrayOfParameterCollection>
                                                                                                                <ParameterCollection id="entireItem">
                                                                                                                    <listedparameter name="alternativesEditor">
                                                                                                                        <designersetting key="visible">true</designersetting>hotspotDrawing</listedparameter>
                                                                                                                    <areaparameter name="area">
                                                                                                                        <designersetting key="conditionalEnabled">true</designersetting>
                                                                                                                        <designersetting key="conditionalEnabledSwitchParameter">alternativesEditor</designersetting>
                                                                                                                        <designersetting key="conditionalEnabledWhenValue">mc|hotspotDrawing</designersetting>
                                                                                                                        <designersetting key="required">true</designersetting>
                                                                                                                        <definition>
                                                                                                                            <resourceparameter name="clickableImage">
                                                                                                                                <designersetting key="required">true</designersetting>
                                                                                                                            </resourceparameter>
                                                                                                                        </definition>
                                                                                                                        <subparameterset id="1">
                                                                                                                            <resourceparameter name="clickableImage">UK.jpg</resourceparameter>
                                                                                                                        </subparameterset>
                                                                                                                        <Shapes>
                                                                                                                            <Circle id="A" label="A" radius="69">
                                                                                                                                <AnchorPoint>
                                                                                                                                    <X>89</X>
                                                                                                                                    <Y>129</Y>
                                                                                                                                </AnchorPoint>
                                                                                                                            </Circle>
                                                                                                                        </Shapes>
                                                                                                                    </areaparameter>
                                                                                                                </ParameterCollection>
                                                                                                            </ArrayOfParameterCollection>

	Private AreaParam_ConditionalEnabled_WithNotValue_ResourceParamVisible_Required As XElement = <ArrayOfParameterCollection>
																									  <ParameterCollection id="entireItem">
																										  <listedparameter name="alternativesEditor">
																											  <designersetting key="visible">true</designersetting>hotspotDrawing</listedparameter>
																										  <areaparameter name="area">
																											  <designersetting key="conditionalEnabled">true</designersetting>
																											  <designersetting key="conditionalEnabledSwitchParameter">alternativesEditor</designersetting>
																											  <designersetting key="conditionalEnabledWhenValue">!mc</designersetting>
																											  <definition>
																												  <resourceparameter name="clickableImage">
																													  <designersetting key="required">true</designersetting>
																												  </resourceparameter>
																											  </definition>
																											  <subparameterset id="1">
																												  <resourceparameter name="clickableImage">
																													  <designersetting key="required">true</designersetting>
																												  </resourceparameter>
																											  </subparameterset>
																										  </areaparameter>
																									  </ParameterCollection>
																								  </ArrayOfParameterCollection>

	Private AreaParam_ConditionalEnabled_WithNotValue_ResourceParamNotVisible As XElement = <ArrayOfParameterCollection>
																								<ParameterCollection id="entireItem">
																									<listedparameter name="alternativesEditor">
																										<designersetting key="visible">true</designersetting>mc</listedparameter>
																									<areaparameter name="area">
																										<designersetting key="conditionalEnabled">true</designersetting>
																										<designersetting key="conditionalEnabledSwitchParameter">alternativesEditor</designersetting>
																										<designersetting key="conditionalEnabledWhenValue">!mc</designersetting>
																										<definition>
																											<resourceparameter name="clickableImage">
																												<designersetting key="required">true</designersetting>
																											</resourceparameter>
																										</definition>
																										<subparameterset id="1">
																											<resourceparameter name="clickableImage">
																												<designersetting key="required">true</designersetting>
																											</resourceparameter>
																										</subparameterset>
																									</areaparameter>
																								</ParameterCollection>
																							</ArrayOfParameterCollection>

	Private AreaParam_ConditionalEnabled_WithEmptyValue_ResourceParamVisible_Required As XElement = <ArrayOfParameterCollection>
																										<ParameterCollection id="entireItem">
																											<listedparameter name="alternativesEditor">
																												<designersetting key="visible">true</designersetting></listedparameter>
																											<areaparameter name="area">
																												<designersetting key="conditionalEnabled">true</designersetting>
																												<designersetting key="conditionalEnabledSwitchParameter">alternativesEditor</designersetting>
																												<designersetting key="conditionalEnabledWhenValue">(EMPTY)</designersetting>
																												<definition>
																													<resourceparameter name="clickableImage">
																														<designersetting key="required">true</designersetting>
																													</resourceparameter>
																												</definition>
																												<subparameterset id="1">
																													<resourceparameter name="clickableImage">
																														<designersetting key="required">true</designersetting>
																													</resourceparameter>
																												</subparameterset>
																											</areaparameter>
																										</ParameterCollection>
																									</ArrayOfParameterCollection>

	Private AreaParam_ConditionalEnabled_WithNotEmpty_ResourceParamVisible_Required As XElement = <ArrayOfParameterCollection>
																									  <ParameterCollection id="entireItem">
																										  <listedparameter name="alternativesEditor">
																											  <designersetting key="visible">true</designersetting>hotspotDrawing</listedparameter>
																										  <areaparameter name="area">
																											  <designersetting key="conditionalEnabled">true</designersetting>
																											  <designersetting key="conditionalEnabledSwitchParameter">alternativesEditor</designersetting>
																											  <designersetting key="conditionalEnabledWhenValue">!(EMPTY)</designersetting>
																											  <definition>
																												  <resourceparameter name="clickableImage">
																													  <designersetting key="required">true</designersetting>
																												  </resourceparameter>
																											  </definition>
																											  <subparameterset id="1">
																												  <resourceparameter name="clickableImage">
																													  <designersetting key="required">true</designersetting>
																												  </resourceparameter>
																											  </subparameterset>
																										  </areaparameter>
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
