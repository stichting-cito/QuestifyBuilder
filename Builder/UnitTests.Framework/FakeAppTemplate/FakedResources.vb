Namespace FakeAppTemplate
    Public Module FakedResources

        Public ImageLayoutTemplate As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                     <Description>Dit is een test omschrijving voor ItemLayoutTemplate</Description>
                                                     <Targets>
                                                         <Target xsi:type="ItemLayoutTemplateTarget" enabled="true" name="ces">
                                                             <Description></Description>
                                                             <Template><![CDATA[
				                                                <html xmlns:cito="http://www.cito.nl/citotester">
					                                                <cito:control id="entireItem" type="InlineImage">
					                                                </cito:control>
				                                                </html>]]>
                                                             </Template>
                                                         </Target>
                                                     </Targets>
                                                 </Template>

        Public ImageControlTemplate As XElement = <Template xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" definitionVersion="1">
                                                      <Description></Description>
                                                      <Targets>
                                                          <Target xsi:type="ControlTemplateTarget" enabled="true" name="ces">
                                                              <Description></Description>
                                                              <Template>
                                                                  <![CDATA[<img id="" src="resource://package/<%#source%>"  alt="" isinlineelement="true" />]]>
                                                              </Template>
                                                          </Target>
                                                      </Targets>
                                                      <SharedFunctions>
                                                          <![CDATA[ ]]>
                                                      </SharedFunctions>
                                                      <SharedParameterSet>
                                                          <resourceparameter name="source">
                                                              <designersetting key="label">Afbeelding bestand</designersetting>
                                                              <designersetting key="description">Selecteer de afbeelding</designersetting>
                                                              <designersetting key="required">true</designersetting>
                                                              <designersetting key="filter">image</designersetting>
                                                              <designersetting key="group">Afbeelding</designersetting>
                                                              <designersetting key="sortkey">0</designersetting>
                                                              <designersetting key="editbuttonVisible">false</designersetting>
                                                              <designersetting key="deletebuttonVisible">false</designersetting>
                                                          </resourceparameter>

                                                          <booleanparameter name="editSize">
                                                              <designersetting key="defaultvalue">False</designersetting>
                                                              <designersetting key="visible">false</designersetting>
                                                              <designersetting key="required">false</designersetting>
                                                          </booleanparameter>

                                                      </SharedParameterSet>
                                                  </Template>

    End Module
End NameSpace