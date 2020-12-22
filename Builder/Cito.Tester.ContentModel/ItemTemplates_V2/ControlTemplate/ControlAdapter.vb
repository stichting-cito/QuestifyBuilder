Imports System.IO
Imports System.Text
Imports System.Xml

Imports Cito.Tester.Common
Imports System.Xml.Serialization
Imports Cito.Tester.Common.Logging
Imports Cito.Tester.Common.WeakEventHandler

Public NotInheritable Class ControlAdapter


    Private _controlElement As XmlElement
    Private _controlTemplate As ControlTemplate = Nothing
    Private _itemParameters As ParameterCollection



    Public Sub New(citoControlElement As XmlElement, parameterSet As ParameterCollection)
        MyBase.New()
        _controlElement = citoControlElement
        _itemParameters = parameterSet

        If _itemParameters IsNot Nothing AndAlso _itemParameters.InnerParameters IsNot Nothing Then
            AddHandlerForResourceNeeded(_itemParameters.InnerParameters)
        End If
    End Sub

    Public Sub New()
    End Sub



    Private Event _ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

    Public Event PreParseControlTemplateProgress As EventHandler(Of EventArgs)

    Public Custom Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)

        AddHandler(value As EventHandler(Of ResourceNeededEventArgs))
            AddHandler _ResourceNeeded, value
            PreloadResourcesIfRequired()
        End AddHandler

        RemoveHandler(value As EventHandler(Of ResourceNeededEventArgs))
            RemoveHandler _ResourceNeeded, value
        End RemoveHandler

        RaiseEvent(sender As Object, e As ResourceNeededEventArgs)
            RaiseEvent _ResourceNeeded(sender, e)
        End RaiseEvent
    End Event



    Public Function GetControlAdaptertemplate() As ControlTemplate
        Dim type As String = GetAttributeValue(_controlElement, "type")
        Return GetControlTemplate(type)
    End Function

    Public Function GetDefaultParameterSet(itemLayoutTemplateTargetName As String) As ParameterCollection
        If _controlTemplate Is Nothing Then
            _controlTemplate = Me.GetControlAdaptertemplate
        End If

        Dim returnedParameterset As New ParameterCollection

        returnedParameterset.InnerParameters.AddRange(_controlTemplate.SharedParameterSet.InnerParameters)

        Dim target As ControlTemplateTarget = _controlTemplate.GetEnabledTargetByName(itemLayoutTemplateTargetName)
        If target IsNot Nothing Then
            returnedParameterset.InnerParameters.AddRange(target.ParameterSet.InnerParameters)
        End If

        If returnedParameterset.InnerParameters.Count > 0 Then
            ExtractDesignerSettingsAndAttributeReferences(returnedParameterset)
        End If

        Return returnedParameterset
    End Function

    Public Function ParseControl(target As String, parseWithDebugger As Boolean) As XmlDocument
        Dim type As String = GetControlTemplateType()
        Dim id As String = GetControlTemplateId()

        _controlTemplate = GetControlTemplate(type)

        Log.TraceInformation(TraceCategory.RenderItem, My.Resources.Trace_ControlAdapter_ParseControl_CompileControltemplate)

        Dim controlTemplate As String
        Dim functions As String

        If (_controlTemplate.Targets.Item(target) IsNot Nothing) AndAlso (_controlTemplate.Targets.Item(target).Template IsNot Nothing) Then
            controlTemplate = _controlTemplate.Targets.Item(target).Template.Text
        ElseIf _controlTemplate.Targets.Item(target) Is Nothing Then
            Throw New ControlTemplateException(
                $"ControlTemplate '{id}' of type {type} does not contain Target {target}.")
        Else
            Throw New ControlTemplateException(
                $"ControlTemplate '{id}' of type {type} is missing template for Target {target}.")
        End If

        If _controlTemplate.SharedFunctions IsNot Nothing Then
            functions = _controlTemplate.SharedFunctions.Text
        Else
            Throw New ControlTemplateException($"ControlTemplate '{id}' of type {type} is missing SharedFunctions-tag.")
        End If

        Dim compiler As CompileEngine = CompileControlTemplate(controlTemplate, functions)
        Try
            compiler.CallMethod("Render", New Object() {_itemParameters, parseWithDebugger})

            Dim controlTemplateDoc As New XmlDocument()

            controlTemplateDoc.LoadXml("<cito:controlTemplate xmlns=""http://www.w3.org/1999/xhtml"" xmlns:qh5=""http://www.imsglobal.org/xsd/imsqtiv2p2_html5_v1p0"" xmlns:cito=""http://www.cito.nl/citotester""></cito:controlTemplate>")

            controlTemplateDoc.FirstChild.InnerXml = compiler.OutputText

            Dim nsmgr As XmlNamespaceManager = New XmlNamespaceManager(controlTemplateDoc.NameTable)
            nsmgr.AddNamespace("xhtml", "http://www.w3.org/1999/xhtml")
            nsmgr.AddNamespace("cito", "http://www.cito.nl/citotester")

            ConvertInlineElementAnchorsToHtml(controlTemplateDoc, nsmgr, target)

            Return controlTemplateDoc
        Catch ex As Exception
            Dim message As New StringBuilder

            With message
                .AppendLine("An error occurred while running controltemplate.")
                .AppendLine()
                .AppendFormat("Id : '{0}'", id)
                .AppendLine()
                .AppendFormat("Type : '{0}'", type)
                .AppendLine()
                .AppendFormat("Target : '{0}'", target)
                .AppendLine()
                .AppendLine()
                .Append(GetFullExceptionMessage(ex, ex.Message))
            End With

            Throw New ControlTemplateScriptException(message.ToString, compiler.SourceCode, ex)
        End Try
    End Function

    Private Function GetFullExceptionMessage(ex As Exception, ByRef message As String) As String
        If ex.InnerException IsNot Nothing Then
            Return message & vbNewLine & GetFullExceptionMessage(ex.InnerException, ex.InnerException.Message)
        End If

        Return ex.Message
    End Function

    Private Sub ConvertInlineElementAnchorsToHtml(doc As XmlDocument, nsmgr As XmlNamespaceManager, target As String)
        Const FIRST_LEVEL_INLINE_ELEMENTS_EXPRESSION As String = "//cito:InlineElement[not(ancestor::cito:InlineElement)]"
        Dim controllerNodes As XmlNodeList = doc.SelectNodes(FIRST_LEVEL_INLINE_ELEMENTS_EXPRESSION, nsmgr)

        For Each node As XmlNode In controllerNodes

            Using reader As New StringReader(node.OuterXml)
                Dim inlineElement As InlineElement = DirectCast(SerializeHelper.XmlDeserializeFromReader(reader, GetType(InlineElement)), InlineElement)
                Dim adapter As ItemLayoutAdapter = New ItemLayoutAdapter(inlineElement.LayoutTemplateSourceName, Nothing, AddressOf OnResourceNeeded)
                Dim xHtmlDocument As XHtmlDocument = adapter.ParseTemplate(target, inlineElement.Parameters, False)
                Dim newNodeList As XmlNodeList = xHtmlDocument.SelectNodes("html/*")

                If newNodeList IsNot Nothing AndAlso Not newNodeList.Count = 0 Then
                    For i As Integer = 0 To newNodeList.Count - 1
                        Dim nodeIndex As Integer = (newNodeList.Count - 1) - i
                        Dim importedNode As XmlNode = doc.ImportNode(newNodeList(nodeIndex), True)
                        If Not String.IsNullOrEmpty(node.ParentNode.Prefix) Then importedNode.Prefix = node.ParentNode.Prefix
                        node.ParentNode.InsertAfter(importedNode, node)
                    Next
                    node.ParentNode.RemoveChild(node)
                End If
            End Using
        Next
    End Sub

    Public Sub PreParseControlTemplates(target As String, templateResources As ICollection(Of String))
        Dim currentParsedControlTemplate As String = String.Empty

        Try
            For Each controlTemplateResourceName As String In templateResources
                Log.TraceInformation(TraceCategory.RenderItem, My.Resources.Trace_ControlAdapter_ParseControl_CompileControltemplate)

                currentParsedControlTemplate = controlTemplateResourceName
                Dim template As ControlTemplate = GetControlTemplate(controlTemplateResourceName)

                If template.Targets.HasTarget(target) Then
                    Dim controlTemplate As String = template.Targets.Item(target).Template.Text
                    Dim functions As String = template.SharedFunctions.Text

                    CompileControlTemplate(controlTemplate, functions)
                End If

                OnPreParseControlTemplateProgress(New EventArgs)
            Next
        Catch ex As Exception
            Throw New ControlTemplateException(
                $"Error while pre-parsing control template with name '{currentParsedControlTemplate}'", ex)
        End Try
    End Sub

    Private Shared Function CompileControlTemplate(controlTemplate As String, functions As String) As CompileEngine
        Dim aspParser As New AspV3StyleParser(controlTemplate, functions)
        Dim code As String = aspParser.GetParsedResult()

        Dim compiler As New CompileEngine(code)

        compiler.References.Add("Cito.Tester.Common.dll")
        compiler.References.Add("Cito.Tester.ContentModel.dll")
        compiler.References.Add("System.Xml.dll")
        compiler.References.Add("System.Xml.Linq.dll")
        compiler.References.Add("System.Drawing.dll")

        If aspParser.FoundReferences.Count > 0 Then
            For Each reference As String In aspParser.FoundReferences
                Log.TraceInformation(TraceCategory.RenderItem, "Add reference '{0}' to compiler.", reference)
                compiler.References.Add(reference)
            Next
        End If

        Log.TraceInformation(TraceCategory.RenderItem, "Compile controltemplate.")
        If compiler.Run() Then
            Return compiler
        Else
            Dim err As New StringBuilder()
            err.Append(String.Format(My.Resources.Error_ControlAdapter_ParseControl_CompilingFailed, compiler.CompileErrors.Count, Environment.NewLine))
            For Each compErr As String In compiler.CompileErrors
                err.AppendLine(" - " & compErr)
            Next
            Throw New ControlTemplateScriptException(err.ToString(), code)
        End If
    End Function

    Friend Shared Sub ExtractDesignerSettingsAndAttributeReferences(parameterColl As ParameterCollection)

        If parameterColl IsNot Nothing Then
            For Each parameter As ParameterBase In parameterColl.InnerParameters

                Dim collectionParameter = TryCast(parameter, ICollectionParameter)
                If (collectionParameter IsNot Nothing) Then
                    ExtractDesignerSettingsAndAttributeReferences(collectionParameter.BluePrint)
                End If

                Dim scoringParameter = TryCast(parameter, ScoringParameter)
                If (scoringParameter IsNot Nothing) Then
                    Dim parameters = scoringParameter.GetParametersWithDesignerSettings()
                    If (parameters IsNot Nothing) Then
                        For Each subParameter As ParameterBase In parameters
                            collectionParameter = TryCast(subParameter, ICollectionParameter)
                            If collectionParameter IsNot Nothing Then
                                ExtractDesignerSettingsAndAttributeReferences(collectionParameter.BluePrint)
                            End If
                            ExtractAndAddFromNodes(subParameter)
                        Next
                    End If
                End If

                ExtractAndAddFromNodes(parameter)

            Next
        End If
    End Sub

    Private Shared Sub ExtractAndAddFromNodes(parameter As ParameterBase)
        Dim designerSettingSerializer As New XmlSerializer(GetType(DesignerSetting))
        Dim attributeReferenceSerializer As New XmlSerializer(GetType(AttributeReference))

        If parameter.Nodes IsNot Nothing Then
            For Each node As XmlNode In parameter.Nodes
                If node.Name = "designersetting" Then
                    Dim reader As New XmlNodeReader(node)
                    Dim setting As DesignerSetting = DirectCast(designerSettingSerializer.Deserialize(reader), DesignerSetting)
                    parameter.DesignerSettings.Add(setting)
                    reader.Close()
                ElseIf node.Name = "attributereference" Then
                    Dim reader As New XmlNodeReader(node)
                    Dim reference As AttributeReference = DirectCast(attributeReferenceSerializer.Deserialize(reader), AttributeReference)
                    parameter.AttributeReferences.Add(reference)
                    reader.Close()
                End If

            Next

            parameter.Nodes = Nothing
        End If
    End Sub

    Private Shared Function GetAttributeValue(elem As XmlElement, attribute As String) As String
        Dim value As String = String.Empty
        Try
            value = elem.Attributes(attribute).Value

        Catch ex As Exception
            Throw New ControlTemplateException(String.Format(My.Resources.Error_ControlAdapter_GetAttributeValue_AttributeNotFound, attribute, elem.Name))
        End Try

        Return value
    End Function

    Private Function GetControlTemplate(name As String) As ControlTemplate
        Log.TraceInformation(TraceCategory.RenderItem, "Get controltemplate of type '{0}'.", name)
        Dim evnt As New ResourceNeededEventArgs(name, New ResourceProcessingFunction(AddressOf StreamConverters.ConvertStreamToString))
        Dim templateToReturn As ControlTemplate

        Try
            OnResourceNeeded(Me, evnt)
            Dim tmp As String = evnt.GetResource(Of String)()
            templateToReturn = ControlTemplateFactory.Create(tmp, True)
            Return templateToReturn

        Catch ex As ResourceException
            Throw
        Catch ex As Exception
            Throw New InteractionControlException(String.Format(My.Resources.Error_ControlAdapter_GetControlAdapterTemplate_CannotGetControlTemplate, name))
        End Try
    End Function

    Private Function GetControlTemplateType() As String
        Dim type As String = GetAttributeValue(_controlElement, "type")
        Return type
    End Function

    Private Function GetControlTemplateId() As String
        Dim id As String = GetAttributeValue(_controlElement, "id")
        Return id
    End Function

    Private Sub OnPreParseControlTemplateProgress(e As EventArgs)
        RaiseEvent PreParseControlTemplateProgress(Me, e)
    End Sub

    Private Sub OnResourceNeeded(sender As Object, e As ResourceNeededEventArgs)
        RaiseEvent ResourceNeeded(sender, e)
    End Sub



    Private Sub PreloadResourcesIfRequired()
        If _itemParameters IsNot Nothing AndAlso _itemParameters.InnerParameters IsNot Nothing Then
            PreloadResourcesIfRequired(_itemParameters.InnerParameters)
        End If
    End Sub

    Private Sub PreloadResourcesIfRequired(parameters As ParameterList)
        For Each param As ParameterBase In parameters
            PreloadResourcesIfRequired(param)
        Next
    End Sub

    Private Sub PreloadResourcesIfRequired(parameter As ParameterBase)
        If TypeOf parameter Is ResourceParameter Then
            DirectCast(parameter, ResourceParameter).PreFetchResource()
        ElseIf TypeOf parameter Is CollectionParameter Then
            For Each param As ParameterBase In DirectCast(parameter, CollectionParameter).Value.FlattenParameters
                PreloadResourcesIfRequired(param)
            Next
        End If
    End Sub

    Private Sub AddHandlerForResourceNeeded(parameters As ParameterList)
        For Each param As ParameterBase In parameters
            AddHandlerForResourceNeeded(param)
        Next
    End Sub

    Private Sub AddHandlerForResourceNeeded(parameter As ParameterBase)
        If TypeOf parameter Is ResourceParameter Then
            AddHandler DirectCast(parameter, ResourceParameter).ResourceNeeded, New EventHandler(Of ResourceNeededEventArgs)(AddressOf OnResourceNeeded).MakeWeak(Sub(e) RemoveHandler DirectCast(parameter, ResourceParameter).ResourceNeeded, e)
        ElseIf TypeOf parameter Is CollectionParameter Then
            For Each param As ParameterBase In DirectCast(parameter, CollectionParameter).Value.FlattenParameters
                AddHandlerForResourceNeeded(param)
            Next
        End If
    End Sub


End Class