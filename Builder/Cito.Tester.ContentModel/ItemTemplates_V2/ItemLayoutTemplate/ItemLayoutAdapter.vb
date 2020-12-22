Imports System.Text.RegularExpressions
Imports Cito.Tester.Common
Imports System.Linq
Imports System.Xml
Imports Cito.Tester.Common.Logging

Public Class ItemLayoutAdapter

    Private ReadOnly _itemLayoutTemplate As ItemLayoutTemplate
    Private _parameterSets As ParameterSetCollection

    Private _xmlTemplate As New Dictionary(Of String, XHtmlDocument)

    Public Sub New(itemLayoutTemplate As ItemLayoutTemplate, controlsParameters As ParameterSetCollection)
        _itemLayoutTemplate = itemLayoutTemplate
        _parameterSets = controlsParameters
    End Sub

    Public Sub New(itemLayoutResourceName As String, controlsParameters As ParameterSetCollection, resourceNeededHandler As EventHandler(Of ResourceNeededEventArgs))
        AddHandler ResourceNeeded, resourceNeededHandler
        _itemLayoutTemplate = GetItemLayoutTemplate(itemLayoutResourceName)
        _parameterSets = controlsParameters
    End Sub

    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

    Public ReadOnly Property Template As ItemLayoutTemplate
        Get
            Return _itemLayoutTemplate
        End Get
    End Property



    Public Function CreateParameterSetsFromItemTemplate() As ParameterSetCollection
        Dim returnedParameterSetCollection As New ParameterSetCollection

        For Each target As String In Me._itemLayoutTemplate.GetEnabledTargetNames
            Dim targetParameterSetCollection As ParameterSetCollection = CreateParameterSetsFromItemTemplate(target)

            For Each paramSet As ParameterCollection In targetParameterSetCollection
                If Not returnedParameterSetCollection.Contains(paramSet.Id) Then
                    returnedParameterSetCollection.Add(paramSet)
                Else
                    Dim paramCollection = DirectCast(returnedParameterSetCollection.GetParamCollectionByControlId(paramSet.Id), ParameterCollection)

                    For Each parameter As ParameterBase In paramSet.InnerParameters
                        If Not paramCollection.InnerParameters.Contains(parameter.Name) Then
                            paramCollection.InnerParameters.Add(parameter)
                        End If
                    Next
                End If
            Next
        Next

        Return returnedParameterSetCollection
    End Function

    Public Sub SetParameterSetCollectionForItemTemplate(parameterSetCollection As ParameterSetCollection)
        Me._parameterSets = parameterSetCollection
    End Sub

    Public Function ParseTemplate(target As String, parameters As ParameterSetCollection, debugControlTemplate As Boolean) As XHtmlDocument
        Return ParseTemplate(target, parameters, debugControlTemplate, Nothing)
    End Function

    Public Function ParseTemplate(target As String, parameters As ParameterSetCollection, debugControlTemplate As Boolean, contextIdentifier As Nullable(Of Integer)) As XHtmlDocument
        _parameterSets = parameters
        Return ParseTemplate(target, debugControlTemplate, contextIdentifier)
    End Function

    Public Function ParseTemplate(target As String, debugControlTemplate As Boolean) As XHtmlDocument
        Return ParseTemplate(target, debugControlTemplate, Nothing)
    End Function

    Public Function ParseTemplate(target As String, debugControlTemplate As Boolean, contextIdentifier As Nullable(Of Integer)) As XHtmlDocument
        Log.TraceInformation(TraceCategory.RenderItem, "Parse item template.")
        Log.Indent()

        If debugControlTemplate Then
            Log.TraceInformation(TraceCategory.RenderItem, "Clear cached templates, because debugging is enabled.")
            TemplateCacheManager.ClearCache()
        End If

        Dim targetXHtmlDoc As XHtmlDocument = Me.GetCloneFromUnparsedXmlTemplateForTarget(target)
        Dim citoControlsNodes As XmlNodeList = GetCitoControlsNodes(targetXHtmlDoc)

        Log.TraceInformation(TraceCategory.RenderItem, "Found {0} 'cito control'-tags.", citoControlsNodes.Count)

        For Each placeHolderCitoControl As XmlNode In citoControlsNodes
            If TypeOf placeHolderCitoControl Is XmlElement Then
                Dim citoControlNode = DirectCast(placeHolderCitoControl, XmlElement)
                Dim id As String = GetAttributeValue(citoControlNode, "id")

                Log.TraceInformation(TraceCategory.RenderItem, "Process cito tag '{0}'.", id)
                Log.Indent()

                Dim parameterSet As ParameterCollection = _parameterSets.GetParamCollectionByControlId(id)

                Dim ctlAdapter = New ControlAdapter(citoControlNode, parameterSet)
                AddHandler ctlAdapter.ResourceNeeded, AddressOf ControlAdapter_ResourceNeeded

                AddHandler TestSessionContext.ResourceNeeded, AddressOf ControlAdapter_ResourceNeeded

                Log.TraceInformation(TraceCategory.RenderItem, "Enable controltemplate debugging.", citoControlsNodes.Count)
                Dim controlTemplateOutput As XmlDocument = ctlAdapter.ParseControl(target, debugControlTemplate)

                InjectControlTemplateResult(targetXHtmlDoc, placeHolderCitoControl, controlTemplateOutput)

                RemoveHandler TestSessionContext.ResourceNeeded, AddressOf ControlAdapter_ResourceNeeded
                RemoveHandler ctlAdapter.ResourceNeeded, AddressOf ControlAdapter_ResourceNeeded

                Log.Unindent()
            End If
        Next

        Log.Unindent()

        If contextIdentifier.HasValue Then
            RecursiveAddContextToResourceURL(targetXHtmlDoc.ChildNodes, contextIdentifier.Value)
        End If

        Return targetXHtmlDoc
    End Function

    Public Function GetInlineMediaTemplates() As Dictionary(Of String, String)
        Dim inlineMediaTemplates As New Dictionary(Of String, String)
        Dim inlineMediaTemplateDesignerSettings As String() = {Constants.DESIGNERSETTING_INLINEIMAGETEMPLATE, Constants.DESIGNERSETTING_INLINEAUDIOTEMPLATE, Constants.DESIGNERSETTING_INLINEVIDEOTEMPLATE}
        Dim templateToAdd As String = String.Empty

        For Each inlineMediaTemplateDesignerSetting In inlineMediaTemplateDesignerSettings
            templateToAdd = GetDesignerSettingFromTemplate(inlineMediaTemplateDesignerSetting)
            If Not String.IsNullOrEmpty(templateToAdd) Then
                Dim m As Match = Regex.Match(inlineMediaTemplateDesignerSetting, "inline(?<mediatype>[A-Za-z0-9\-]+)template$", RegexOptions.IgnoreCase)
                inlineMediaTemplates.Add(m.Groups("mediatype").Value.ToLower(), templateToAdd)
            End If
        Next

        Return inlineMediaTemplates
    End Function

    Public Function GetInlineCustomInteractionTemplate() As String
        Return GetDesignerSettingFromTemplate(Constants.DESIGNERSETTING_INLINECITEMPLATE)
    End Function

    Public Function GetPopupTemplate() As String
        Return GetDesignerSettingFromTemplate(Constants.DESIGNERSETTING_POPUPTEMPLATE)
    End Function

    Public Function GetInlineFindingOverride() As String
        Return GetDesignerSettingFromTemplate(Constants.DESIGNERSETTING_INLINEFINDINGOVERRIDE)
    End Function

    Private Function GetDesignerSettingFromTemplate(designerSettingName As String) As String
        Dim result As String = String.Empty

        If (Template IsNot Nothing AndAlso Template.DesignerSettings IsNot Nothing) Then
            If Template.DesignerSettings.GetDesignerSettingByKey(designerSettingName) IsNot Nothing Then
                result = Template.DesignerSettings.GetDesignerSettingByKey(designerSettingName).Value
            End If
        End If
        Return result
    End Function


    Public Function ValidateSolution(Solution As KeyFindingCollection) As Boolean
        Dim interactionInfo As InteractionControllerInfoCollection = Me.GetInteractionInfo()

        Return Solution.ValidateSolution(interactionInfo)
    End Function

    Private Shared Function GetAttributeValue(elem As XmlElement, attribute As String) As String
        Dim value As String
        Try
            value = elem.Attributes(attribute).Value
        Catch ex As Exception
            Throw New ItemTemplateException(String.Format(My.Resources.Error_ItemAdapter_GetAttributeValue_AttributeNotFound, attribute, elem.Name))
        End Try

        Return value
    End Function

    Private Sub ControlAdapter_ResourceNeeded(sender As Object, e As ResourceNeededEventArgs)
        OnResourceNeeded(sender, e)
    End Sub

    Private Function CreateParameterSetsFromItemTemplate(target As String) As ParameterSetCollection

        Dim returnedParameterSetCollection As New ParameterSetCollection
        Log.TraceInformation(TraceCategory.RenderItem, "Parse item template and determine parameters.")
        Log.Indent()

        Dim citoControlsNodes As XmlNodeList = GetCitoControlsNodes(target)
        Log.TraceInformation(TraceCategory.RenderItem, "Found {0} 'cito control'-tags.", citoControlsNodes.Count)

        For Each citoControl As XmlNode In citoControlsNodes
            If TypeOf citoControl Is XmlElement Then
                Dim citoControlElement As XmlElement = DirectCast(citoControl, XmlElement)
                Dim id As String = GetAttributeValue(citoControlElement, "id")

                Log.TraceInformation(TraceCategory.RenderItem, "Process cito tag '{0}'.", id)
                Log.Indent()

                Dim ctlAdapter As New ControlAdapter(citoControlElement, Nothing)

                AddHandler ctlAdapter.ResourceNeeded, AddressOf ControlAdapter_ResourceNeeded

                Dim controlParameterSet As ParameterCollection = ctlAdapter.GetDefaultParameterSet(target)

                OverrideDesignerSettings(controlParameterSet, citoControlElement.ChildNodes)

                controlParameterSet.Id = id

                For Each param As ParameterBase In controlParameterSet.InnerParameters
                    Dim value As String = param.DesignerSettings.GetSettingValueByKey("defaultvalue")
                    If Not String.IsNullOrEmpty(value) AndAlso Not param.SetValue(value) Then
                        Throw New ItemTemplateException(
                            $"Parameter '{param.Name}' tries to set a default value which was not possible.")
                    End If
                Next

                controlParameterSet.OverrideAttributeReferences()

                returnedParameterSetCollection.Add(controlParameterSet)
                Log.Unindent()
            End If
        Next

        Log.Unindent()
        Return returnedParameterSetCollection
    End Function

    Private Function GetCitoControlsNodes(unparsedXmlTemplate As XHtmlDocument) As XmlNodeList

        Dim nsmgr = New XmlNamespaceManager(unparsedXmlTemplate.NameTable)
        nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
        nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")
        Dim citoControlsNodes As XmlNodeList = unparsedXmlTemplate.SelectNodes("//cito:control", nsmgr)
        Return citoControlsNodes
    End Function

    Private Function GetCitoControlsNodes(target As String) As XmlNodeList
        Dim xmlTemplate As XHtmlDocument = GetUnparsedXmlTemplateForTarget(target)
        Return GetCitoControlsNodes(xmlTemplate)
    End Function


    Private Function GetUnparsedXmlTemplateForTarget(target As String) As XHtmlDocument
        If Not _xmlTemplate.ContainsKey(target.ToLower) Then
            If _itemLayoutTemplate.TargetExists(target) Then
                Dim tempXMLTemplate As XHtmlDocument = GetCloneFromUnparsedXmlTemplateForTarget(target)
                _xmlTemplate.Add(target.ToLower, tempXMLTemplate)
            Else
                Throw New ArgumentException($"Nonexisting target ({target})", "target")
            End If
        End If

        Return _xmlTemplate(target.ToLower)
    End Function

    Private Function GetCloneFromUnparsedXmlTemplateForTarget(target As String) As XHtmlDocument
        Dim tempXMLTemplate As New XHtmlDocument

        If _itemLayoutTemplate.TargetExists(target) Then
            Dim targetTemplate As ItemLayoutTemplateTarget = _itemLayoutTemplate.Targets.Item(target)

            Trace.Assert(targetTemplate IsNot Nothing,
                         $"TargetTemplate for target [{target}] in ItemLayoutTemplate should not be nothing")
            tempXMLTemplate.LoadXml(targetTemplate.Template.Text, False)
        Else
            Throw New ArgumentException($"Nonexisting target ({target})", "target")
        End If

        Return tempXMLTemplate
    End Function

    Private Function GetItemLayoutTemplate(name As String) As ItemLayoutTemplate
        Log.TraceInformation(TraceCategory.RenderItem, "Get ItemLayoutTemplate of type '{0}'.", name)

        Dim resourceNeeded As New ResourceNeededEventArgs(name, New ResourceProcessingFunction(AddressOf StreamConverters.ConvertStreamToString))

        Try
            OnResourceNeeded(Me, resourceNeeded)

            Return ItemLayoutTemplateFactory.Create(resourceNeeded.GetResource(Of String)(), True)
        Catch ex As ResourceException
            Throw
        Catch ex As Exception
            Throw New InteractionControlException($"Error while getting ItemLayoutTemplate with name {name}", ex)
        End Try
    End Function

    Public Function GetListOfControlTemplatesAndIds() As Dictionary(Of String, List(Of String))
        Dim result As New Dictionary(Of String, List(Of String))
        For Each targetName As String In Me.Template.GetEnabledTargetNames
            For Each kvp As KeyValuePair(Of String, List(Of String)) In GetListOfControlTemplatesAndIds(targetName)
                If Not result.ContainsKey(kvp.Key) Then result.Add(kvp.Key, kvp.Value)
            Next
        Next
        Return result
    End Function

    Private Function GetListOfControlTemplatesAndIds(target As String) As Dictionary(Of String, List(Of String))
        Dim citoControlsNodes As XmlNodeList = GetCitoControlsNodes(target)
        Dim result As New Dictionary(Of String, List(Of String))

        For Each citoControl As XmlNode In citoControlsNodes
            If TypeOf citoControl Is XmlElement Then
                Dim citoControlElement As XmlElement = DirectCast(citoControl, XmlElement)
                Dim typeAttr As String = GetAttributeValue(citoControlElement, "type")
                Dim ids As New List(Of String)
                If Not result.ContainsKey(typeAttr) Then
                    ids.Add(GetAttributeValue(citoControlElement, "id"))
                    result.Add(typeAttr, ids)
                Else
                    ids = result(typeAttr)
                    ids.Add(GetAttributeValue(citoControlElement, "id"))
                    result(typeAttr) = ids
                End If

            End If
        Next

        Return result
    End Function

    Public Function GetInteractionInfo() As InteractionControllerInfoCollection
        Dim resultToReturn As New InteractionControllerInfoCollection

        For Each targetValue As String In Me.Template.GetEnabledTargetNames
            GetInteractionInfoForTarget(targetValue, resultToReturn)
        Next
        Return resultToReturn
    End Function

    Private Sub GetInteractionInfoForTarget(target As String, ByRef interactionControllerInfoCollection As InteractionControllerInfoCollection)
        Dim parsedXmlTemplate As XHtmlDocument = Me.ParseTemplate(target, False)

        For Each iControllerNode As XmlNode In GetListOfInteractionControllers(parsedXmlTemplate)
            Dim controllerId As String = iControllerNode.Attributes("id").Value
            Dim iControllerInfo As New InteractionControllerInfo(iControllerNode, GetListOfInteractionControls(parsedXmlTemplate, controllerId))

            If Not interactionControllerInfoCollection.Contains(iControllerInfo.Id) Then
                interactionControllerInfoCollection.Add(iControllerInfo)
            Else
                Dim existingController As InteractionControllerInfo = interactionControllerInfoCollection.Item(iControllerInfo.Id)

                Trace.Assert(existingController.InteractionControls.Count = iControllerInfo.InteractionControls.Count,
             $"Not all targets generate the same number of InteractionControls for InteractionController with id=[{iControllerInfo.Id}].")

                If existingController.InteractionControls.Count = iControllerInfo.InteractionControls.Count Then
                    For Each ictrl As InteractionControlInfo In iControllerInfo.InteractionControls
                        Trace.Assert(existingController.InteractionControls.Contains(ictrl.Id), String.Format("Inconsistent target ([{0}]) detected. InteractionControl [{2}] for InteractionController [{1}] doesn't exist in one or more other targets.", target, iControllerInfo.Id, ictrl.Id))
                    Next
                End If
            End If
        Next
    End Sub


    Private Function GetListOfInteractionControllers(parsedXmlTemplate As XHtmlDocument) As XmlNodeList
        Dim nsmgr As XmlNamespaceManager = New XmlNamespaceManager(parsedXmlTemplate.NameTable)
        nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
        nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")

        Dim nodes As XmlNodeList = parsedXmlTemplate.SelectNodes("//cito:interactionController", nsmgr)
        Return nodes
    End Function

    Private Function GetListOfInteractionControls(parsedXmlTemplate As XHtmlDocument, controllerId As String) As XmlNodeList
        Dim nsmgr As XmlNamespaceManager = New XmlNamespaceManager(parsedXmlTemplate.NameTable)
        nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
        nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")

        Dim nodes As XmlNodeList = parsedXmlTemplate.SelectNodes(
    $"//cito:interactionControl[@controller='{controllerId}']", nsmgr)
        Return nodes
    End Function

    Private Sub OnResourceNeeded(sender As Object, e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(sender, e)
    End Sub

    Private Sub OverrideDesignerSettings(parameterColl As ParameterCollection, overrideParameterNodes As XmlNodeList)
        If overrideParameterNodes IsNot Nothing Then
            For Each parameterNode As XmlNode In overrideParameterNodes
                If parameterNode.Name = "parameter" Then
                    Dim name As String = parameterNode.Attributes("name").Value
                    Dim param = parameterColl.TryGetParameterByName(Of ParameterBase)(name)
                    If param IsNot Nothing Then
                        Dim parameter As ParameterBase = DirectCast(parameterColl.GetParameterByName(name), ParameterBase)
                        Dim settingsOverrideList As XmlNodeList = parameterNode.ChildNodes
                        If settingsOverrideList IsNot Nothing Then
                            OverrideDesignerSettings(settingsOverrideList, parameter, name)
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Private Sub OverrideDesignerSettings(settingsOverrideList As XmlNodeList, parameter As ParameterBase, name As String)
        For Each settingOverrideNode As XmlNode In settingsOverrideList
            If settingOverrideNode.Name = "definition" Then
                If TypeOf parameter Is CollectionParameter Then
                    Dim definitionParametersToOverride As XmlNodeList = settingOverrideNode.ChildNodes
                    OverrideDesignerSettings(DirectCast(parameter, CollectionParameter).BluePrint, definitionParametersToOverride)
                Else
                    Throw New ContentModelException($"Item layout template attempts to override the definition of parameter '{name}' while this is not a collectionParameter.")
                End If
            ElseIf settingOverrideNode.Name = "parameter" Then
                If TypeOf parameter Is CollectionParameter Then
                    Dim definitionParametersToOverride As XmlNodeList = settingOverrideNode.SelectNodes(".")
                    OverrideDesignerSettings(DirectCast(parameter, CollectionParameter).BluePrint, definitionParametersToOverride)

                    If TypeOf parameter Is ScoringParameter Then
                        For Each prm As ParameterBase In DirectCast(parameter, ScoringParameter).GetParametersWithDesignerSettings.Where(Function(x) x.Name.Equals(settingOverrideNode.Attributes("name").Value))
                            OverrideDesignerSettings(settingOverrideNode.ChildNodes, prm, name)
                        Next
                    End If
                Else
                    Throw New ContentModelException($"Item layout template attempts to override the definition of parameter '{name}' while this is not a collectionParameter.")
                End If
            ElseIf Not settingOverrideNode.NodeType.Equals(XmlNodeType.SignificantWhitespace) Then
                Dim key As String = settingOverrideNode.Attributes("key").Value
                Dim value As String = settingOverrideNode.InnerText

                Select Case key
                    Case "redirectEnabled"
                        Dim redirectEnabledDesignerSetting As DesignerSetting = parameter.DesignerSettings.GetDesignerSettingByKey("redirectEnabled")
                        If redirectEnabledDesignerSetting Is Nothing Then
                            redirectEnabledDesignerSetting = New DesignerSetting
                            redirectEnabledDesignerSetting.Key = "redirectEnabled"
                            parameter.DesignerSettings.Add(redirectEnabledDesignerSetting)
                        End If
                        redirectEnabledDesignerSetting.Value = value
                    Case "redirectToTargetControlId"
                        Dim redirectToTargetControlIdDesignerSetting As DesignerSetting = parameter.DesignerSettings.GetDesignerSettingByKey("redirectToTargetControlId")
                        If redirectToTargetControlIdDesignerSetting Is Nothing Then
                            redirectToTargetControlIdDesignerSetting = New DesignerSetting
                            redirectToTargetControlIdDesignerSetting.Key = "redirectToTargetControlId"
                            parameter.DesignerSettings.Add(redirectToTargetControlIdDesignerSetting)
                        End If
                        redirectToTargetControlIdDesignerSetting.Value = value
                    Case "redirectToTargetParameterId"
                        Dim redirectToTargetParameterIdDesignerSetting As DesignerSetting = parameter.DesignerSettings.GetDesignerSettingByKey("redirectToTargetParameterId")

                        If redirectToTargetParameterIdDesignerSetting Is Nothing Then
                            redirectToTargetParameterIdDesignerSetting = New DesignerSetting
                            redirectToTargetParameterIdDesignerSetting.Key = "redirectToTargetParameterId"
                            parameter.DesignerSettings.Add(redirectToTargetParameterIdDesignerSetting)
                        End If
                        redirectToTargetParameterIdDesignerSetting.Value = value
                    Case Else
                        Dim setting As DesignerSetting = parameter.DesignerSettings.GetDesignerSettingByKey(key)
                        If key = "list" AndAlso TypeOf (parameter) Is ListedParameter Then
                            If settingOverrideNode.HasChildNodes Then
                                Dim listValues As New List(Of ListValue)
                                For Each listNode As XmlNode In settingOverrideNode.ChildNodes
                                    If listNode.HasChildNodes Then
                                        For Each node As XmlNode In listNode.ChildNodes
                                            If node.Name = "listvalue" Then
                                                Dim listValue As New ListValue
                                                listValue.Key = node.Attributes("key").Value
                                                listValue.DisplayValue = node.InnerText
                                                listValues.Add(listValue)
                                            Else
                                                Throw New ControlTemplateException("Unknown node name: listvalues is expected.")
                                            End If
                                        Next
                                    End If
                                Next
                                setting.ListValue = listValues
                            End If

                        Else
                            If setting IsNot Nothing Then
                                setting.Value = value
                            Else
                                Throw New ContentModelException(
    $"Item layout template tries to override designer setting '{key}' of parameter '{name}' while this setting is not defined in the control template")
                            End If
                        End If
                End Select
            End If
        Next
    End Sub

    Private Sub InjectControlTemplateResult(itemLayoutTemplateToInjectIn As XHtmlDocument, placeHolderCitoControl As XmlNode, ControlTemplateResult As XmlDocument)
        Log.TraceInformation(TraceCategory.RenderItem, My.Resources.Trace_ItemAdapter_ProcessControlTemplateOutput_AddScriptOutput)

        Dim nsmgr As XmlNamespaceManager = New XmlNamespaceManager(itemLayoutTemplateToInjectIn.NameTable)
        nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
        nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")
        Dim controlTemplateNode As XmlNode = ControlTemplateResult.SelectSingleNode("//cito:controlTemplate", nsmgr)

        Trace.Assert(controlTemplateNode IsNot Nothing, String.Format(My.Resources.Error_ItemAdapter_ProcessControlTemplateOutput_NoValidTemplate, placeHolderCitoControl.Name))

        Dim importedControlTemplateResult As XmlNode = itemLayoutTemplateToInjectIn.ImportNode(controlTemplateNode, True)




        Dim insertAfterNode As XmlNode = placeHolderCitoControl
        For Each node As XmlNode In importedControlTemplateResult.ChildNodes
            Dim insertedNode As XmlNode = placeHolderCitoControl.ParentNode.InsertAfter(node.CloneNode(True), insertAfterNode)
            insertAfterNode = insertedNode
        Next

        placeHolderCitoControl.ParentNode.RemoveChild(placeHolderCitoControl)
    End Sub


    Private Sub RecursiveAddContextToResourceURL(parentCollection As XmlNodeList, contextIdentifier As Integer)
        For Each node As XmlNode In parentCollection
            If node.Value IsNot Nothing AndAlso node.Value.IndexOf("resource://package", StringComparison.OrdinalIgnoreCase) > 0 Then
                node.Value = node.Value.ToLower().Replace("resource://package",
                                                          $"resource://package:{contextIdentifier}")
            End If

            If node.Attributes IsNot Nothing Then
                For Each attribute As XmlAttribute In node.Attributes
                    If attribute.Value IsNot Nothing AndAlso attribute.Value.StartsWith("resource://package", StringComparison.OrdinalIgnoreCase) Then
                        Dim newUri As New UriBuilder(attribute.Value)
                        newUri.Port = contextIdentifier
                        attribute.Value = newUri.ToString()
                    End If
                Next
            End If

            If node.ChildNodes.Count > 0 Then
                RecursiveAddContextToResourceURL(node.ChildNodes, contextIdentifier)
            End If
        Next
    End Sub


End Class