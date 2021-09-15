Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Text.RegularExpressions
Imports Cito.Tester.ContentModel
Imports DocumentFormat.OpenXml
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports DocumentFormat.OpenXml.Drawing
Imports DocumentFormat.OpenXml.Drawing.Wordprocessing
Imports Cito.Tester.Common
Imports System.Drawing
Imports NotesFor.HtmlToOpenXml
Imports Questify.Builder.Logic.HelperClasses
Imports Questify.Builder.Logic.Service.HelperFunctions
Imports Questify.Builder.Packaging
Imports JustificationValues = DocumentFormat.OpenXml.Math.JustificationValues
Imports NonVisualDrawingProperties = DocumentFormat.OpenXml.Drawing.Pictures.NonVisualDrawingProperties
Imports Outline = DocumentFormat.OpenXml.Drawing.Outline
Imports Picture = DocumentFormat.OpenXml.Drawing.Pictures.Picture
Imports NonVisualPictureProperties = DocumentFormat.OpenXml.Drawing.Pictures.NonVisualPictureProperties
Imports NonVisualPictureDrawingProperties = DocumentFormat.OpenXml.Drawing.Pictures.NonVisualPictureDrawingProperties
Imports BlipFill = DocumentFormat.OpenXml.Drawing.Pictures.BlipFill
Imports ShapeProperties = DocumentFormat.OpenXml.Drawing.Pictures.ShapeProperties
Imports Questify.Builder.Logic

Public Class OpenXmlGenerator


    Private Enum SupportedSourceFiles
        text = 1
        xhtml = 2
        docx = 3
        rtf = 4
        mht = 5
        html = 6
    End Enum

    Private Enum CustomXmlElementType
        xhtmlElement = 1
        xhtmlElementToOpenXml = 2
        imageElement = 3
        wordElement = 4
        wordContent = 5
    End Enum

    Private Enum ContentControlBlock
        ContentBlock = 1
        AspectReferenceBlock = 2
        ItemPropertiesBlock = 3
    End Enum

    Private Enum TestProperties
        test_itemcount = 1
        test_maxscore = 2
        test_version = 3
    End Enum

    Private Enum DocFields
        num_pages = 1
    End Enum


    Private Const PropertiesToOtherBookletsPrefix As String = "_"

    Private Const IgnoreParameterName As String = "ignore"
    Private Const AspectreferencesetCollection As String = "aspectreferencesetcollection"
    Private Const ItemContentName As String = "item_content"
    Private Const ItemTypeName As String = "ItemType"

    Private Const ContentProperty As String = "content"
    Private Const ItemTypeProperty As String = "itemtype|type"
    Private Const TitleProperty As String = "title|titel"
    Private Const CodeProperty As String = "code|id|identifier"
    Private Const ItemTemplateProperty As String = "template|layouttemplatesourcename"
    Private Const MaxScoreProperty As String = "maxscore|max-score|max_score"
    Private Const DescriptionProperty As String = "description|opmerking|opmerkingen"
    Private Const KeyProperty As String = "sleutel|key|solution"
    Private Const SequenceNumberProperty As String = "sequencenumber|volgnr|volgnummer"
    Private Const SequenceNumberDotProperty As String = "sequencenumberdot|volgnrpunt|volgnummerpunt"

    Private Const TestPrefix As String = "test_"
    Private Const ItemPrefix As String = "item_"
    Private Const AspectPrefix As String = "aspect_"
    Private Const AspectReferencePrefix As String = "aspectreference_"

    Private Const RequiredSuffix As String = "_required"
    Private Const LoopSuffix As String = "_loop"
    Private Const UniqueSuffix As String = "_unique"

    Private Const DefaultTableWidthPortait As Integer = 9500
    Private Const DefaultTableWidthLandscape As Integer = 0



    Private _currentBookletName As String = String.Empty
    Private _itemSelector As WordItemSelectorManager
    Private _resourceManager As ResourceManagerBase
    Private _testContext As WordAssessmentTest
    Private _contextIdentifier As Nullable(Of Integer)
    Private _bookletCollection As Dictionary(Of String, WordprocessingDocument)
    Private _styleSheetsToReference As Dictionary(Of String, String)

    Private _totalNumberOfItemsInTest As Integer = 0
    Private _listOfWordFragmentsAdded As List(Of String)
    Private _regexPackageUrl As String = "(resource://package|file:///)([a-zA-Z0-9\~\!\@\#\$\%\^\&amp;\*\(\)_\ \-\=\+\\\/\?\.\:\;\'\,]*)?"
    Private _regexClass As String = "class=""UserSR([a-zA-Z0-9]*)?"""
    Private _regexBetweenTags As String = "<{0}\b[^>]*>(.*?)</{0}>"
    Private _altChunkIdCounter As Integer

    Private _contentBlockDictionary As Dictionary(Of String, OpenXmlCompositeElement)
    Private _aspectDictionary As Dictionary(Of String, Aspect)
    Private _mathMlEditorPlugin As IMathMlEditorPlugin

    Private _itemCounter As Integer = 0
    Private _regularItemCounter As Integer = 0

    Private _showItemCounter As Boolean
    Private _maxScoreTest As Double = 0.0
    Private _versionNumberTest As String
    Private _htmlConverter As HtmlConverter
    Friend WithEvents StyleHelper As WordStyleHelper

    Private _tempFolder As String = TempStorageHelper.GetTempStoragePath

    Private _cachedStyleSheetsToReference As Dictionary(Of String, Dictionary(Of String, String))

    Private _pageOrientationLandScape As Boolean = False


    Private Property PackageUri() As Uri

    Public Event HandlerProgress(sender As Object, e As ProgressEventArgs)
    Public Event WordDocGenerated(sender As Object, e As EventArgs)


    Public Function OpenDocument(path As String) As WordprocessingDocument
        Dim wordDoc As WordprocessingDocument = WordprocessingDocument.Open(path, True)

        If wordDoc.DocumentType = WordprocessingDocumentType.Template Then
            wordDoc.ChangeDocumentType(WordprocessingDocumentType.Document)

            Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
            mainPart.DocumentSettingsPart.AddExternalRelationship("http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate", New Uri(path, UriKind.Absolute))
        End If

        Return wordDoc
    End Function


    Private Function GetResourcePathFromString(xmlHtmlString As String) As MatchCollection
        Dim urlRegex As New Regex(_regexPackageUrl, RegexOptions.IgnoreCase)
        Return urlRegex.Matches(xmlHtmlString.Trim)
    End Function

    Private Sub AddResourceToMhtFile(resourcePath As String, mhtFile As MhtFile)
        Dim resourceName As String = IO.Path.GetFileName(resourcePath)
        Dim eventArgs As New ResourceNeededEventArgs(resourceName, AddressOf StreamConverters.ConvertStreamToByteArray)

        ResourceNeeded(Me, eventArgs)

        If eventArgs.BinaryResource IsNot Nothing AndAlso eventArgs.BinaryResource.ResourceObject IsNot Nothing Then
            mhtFile.AppendMhtBinaryFile(eventArgs.BinaryResource, resourcePath)
        End If
    End Sub

    Private Sub AddElementToDocument(ByRef mainPart As MainDocumentPart, ByRef openxmlElement As OpenXmlElement)
        mainPart.Document.Body.AppendChild(openxmlElement)
    End Sub

    Private Sub AddElementBeforeFirstParagraphInContentControl(ByRef paragraphToAddAfter As Wordprocessing.Paragraph, ByRef openXmlElementToAdd As OpenXmlElement)
        paragraphToAddAfter.InsertBeforeSelf(openXmlElementToAdd)
    End Sub

    Private Sub SaveAndDisposeWordObjects(ByRef wordDoc As WordprocessingDocument)
        wordDoc.MainDocumentPart.Document.Save()
        wordDoc.Dispose()
    End Sub

    Private Sub RemoveBlocksThatShouldBeIgnored(ByRef element As Body, removeCustomXmlForOtherBooklets As Boolean)
        Dim res As IEnumerable(Of CustomXmlElement)

        res = From cr In element.Descendants(Of CustomXmlElement)()
              Where String.Equals(cr.Element, IgnoreParameterName, StringComparison.OrdinalIgnoreCase)
              Select cr

        For Each customXmlelement As CustomXmlElement In res.ToList
            customXmlelement.Parent.RemoveChild(customXmlelement)
        Next

        If removeCustomXmlForOtherBooklets Then
            Dim resOtherBooklets As IEnumerable(Of CustomXmlElement) =
                    From cr In element.Descendants(Of CustomXmlElement)()
                    Where (cr.Element.Value.StartsWith(PropertiesToOtherBookletsPrefix))
                    Select cr

            For Each customXmlelement As CustomXmlElement In resOtherBooklets.ToList
                customXmlelement.Parent.RemoveChild(customXmlelement)
            Next
        End If
    End Sub

    Private Sub AddParagraphToEmptyTableCell(ByRef element As Body)
        Dim res As IEnumerable(Of Wordprocessing.TableCell)
        Dim lastChild As OpenXmlElement

        res = From cr In element.Descendants(Of Wordprocessing.TableCell)()
              Select cr

        For Each ceCell As Wordprocessing.TableCell In res.ToList
            lastChild = ceCell.LastChild

            If lastChild Is Nothing OrElse lastChild.GetType().ToString() <> GetType(Wordprocessing.Paragraph).ToString() Then
                ceCell.AppendChild(New Wordprocessing.Paragraph())
            End If
        Next
    End Sub

    Private Sub RemoveBookmarkFromBlock(ByRef stdBlock As SdtElement)
        Dim resBegin = From cr In stdBlock.Descendants(Of BookmarkStart)()
                       Select cr

        If resBegin IsNot Nothing AndAlso resBegin.ToList IsNot Nothing Then
            For Each bookmarkStart As BookmarkStart In resBegin.ToList
                bookmarkStart.Remove()
            Next
        End If

        Dim resEnd = From cr In stdBlock.Descendants(Of BookmarkEnd)()
                     Select cr

        If resEnd IsNot Nothing AndAlso resEnd.ToList IsNot Nothing Then
            For Each bookmarkEnd As BookmarkEnd In resEnd.ToList
                bookmarkEnd.Remove()
            Next
        End If
    End Sub

    Private Sub AddDataToCustomBlock(ByRef wordDocument As WordprocessingDocument, ByRef customBlock As SdtElement, ByRef item As AssessmentItem, ByRef body As Body, aspect As Aspect)
        If customBlock IsNot Nothing Then

            Dim newItemBlock As SdtElement = DirectCast(customBlock.Clone, SdtElement)

            Dim contentControlsInTemplate As List(Of SdtElement) = GetAllCustomBlocksInContentControlWithoutLoop(newItemBlock)

            Dim areRequiredParametersFilled As Boolean = True
            LoopThroughContentControls(contentControlsInTemplate, item, body, areRequiredParametersFilled, wordDocument, Nothing, aspect)

            Dim childElementIndex As Integer = 0
            RemoveBookmarkFromBlock(newItemBlock)

            Dim contentBlock As OpenXmlCompositeElement = GetContentBlockFromSdtElement(newItemBlock)
            Dim numberOfChildElements As Integer = 0

            If Not areRequiredParametersFilled OrElse (newItemBlock Is Nothing) OrElse (contentBlock Is Nothing) Then
                Return
            End If

            numberOfChildElements = contentBlock.ChildElements.Count

            For Each openXmlElement As OpenXmlElement In contentBlock.ChildElements
                If (childElementIndex = (numberOfChildElements - 1)) AndAlso openXmlElement.GetType Is GetType(Wordprocessing.Paragraph) AndAlso Not ParagraphContainsText(CType(openXmlElement, Wordprocessing.Paragraph)) Then
                ElseIf customBlock.GetType Is GetType(SdtContentRun) AndAlso customBlock.Parent IsNot Nothing Then
                    customBlock.Parent.InsertBeforeSelf(DirectCast(openXmlElement.Clone, OpenXmlElement))
                Else
                    customBlock.InsertBeforeSelf(DirectCast(openXmlElement.Clone, OpenXmlElement))
                End If

                childElementIndex += 1
            Next

        End If
    End Sub

    Private Function GetBodyFromItem(item As AssessmentItem, ByRef wordDoc As WordprocessingDocument, isPreview As Boolean) As Body
        Dim parseTemplateString As String = SetupItem(item)
        Dim newBody As New Body(parseTemplateString)

        RemoveBlocksThatShouldBeIgnored(newBody, Not isPreview)

        _styleSheetsToReference = GetReferencedStylesheets(item)

        For Each customXmlElementType As CustomXmlElementType In [Enum].GetValues(GetType(CustomXmlElementType))
            ResolveCustomXmlElement(CType(newBody, OpenXmlElement), wordDoc.MainDocumentPart, customXmlElementType, False)
        Next

        Return newBody
    End Function

    Private Function CustomXmlBlockHasCustomElementType(customXmlBlock As OpenXmlElement) As Boolean
        If customXmlBlock IsNot Nothing AndAlso TypeOf customXmlBlock Is CustomXmlBlock Then
            Dim customXmlBlockElementType As String = DirectCast(customXmlBlock, CustomXmlBlock).Element

            Return Not [Enum].GetValues(GetType(CustomXmlElementType)).Cast(Of CustomXmlElementType)().ToList().Select(
                Function(v) v.ToString()).Contains(customXmlBlockElementType, StringComparer.InvariantCultureIgnoreCase)
        End If

        Return False
    End Function

    Private Sub LoopThroughContentControls(
                                           ByRef contentControlsInTemplate As List(Of SdtElement),
                                           ByRef item As AssessmentItem,
                                           ByRef body As Body,
                                           ByRef areRequiredParametersFilled As Boolean,
                                           ByRef wordDocument As WordprocessingDocument,
                                           index As Nullable(Of Integer), aspect As Aspect)

        For Each contentControl As SdtElement In contentControlsInTemplate
            item = ProcessContentControl(contentControl, item, body, wordDocument, areRequiredParametersFilled, index, aspect)
        Next
    End Sub

    Private Function ProcessContentControl(
                                           contentControl As SdtElement,
                                           item As AssessmentItem,
                                           ByRef body As Body,
                                           ByRef wordDocument As WordprocessingDocument,
                                           ByRef areRequiredParametersFilled As Boolean,
                                           index As Integer?,
                                           aspect As Aspect) As AssessmentItem

        Dim contentcontrolName As String = GetCustomBlockName(contentControl).ToLower.Trim
        Dim isRequired As Boolean = False
        Dim shouldBeUnique As Boolean = False
        Dim isCustomCustomXmlBlock As Boolean = False

        If contentcontrolName.Contains(UniqueSuffix) Then
            shouldBeUnique = True
            contentcontrolName = contentcontrolName.Replace(UniqueSuffix, String.Empty)
        End If

        If contentcontrolName.Contains(RequiredSuffix) Then
            isRequired = True
            contentcontrolName = contentcontrolName.Replace(RequiredSuffix, String.Empty)
        End If

        contentcontrolName = contentcontrolName.Replace(LoopSuffix, String.Empty)

        Dim contentBlock As OpenXmlCompositeElement = GetContentBlockFromSdtElement(DirectCast(contentControl.Clone, SdtElement))
        Dim firstRun As Wordprocessing.Run = GetFirstRunFromContentBlock(contentBlock)
        Dim firstParagraph As Wordprocessing.Paragraph = GetFirstParagraphFromContentBlock(contentBlock, contentControl)

        If contentcontrolName.Contains(ItemPrefix) _
   OrElse contentcontrolName.Contains(AspectReferencePrefix) _
   OrElse contentcontrolName.Contains(AspectPrefix) _
   OrElse [Enum].IsDefined(GetType(TestProperties), contentcontrolName) _
   OrElse [Enum].IsDefined(GetType(DocFields), contentcontrolName) _
   OrElse contentcontrolName.Contains(TestPrefix) _
    Then
            AddPropertiesToDocument(item, contentcontrolName, body, wordDocument, isRequired, areRequiredParametersFilled, firstParagraph, index, aspect, firstRun)
        ElseIf contentcontrolName = AspectreferencesetCollection AndAlso item IsNot Nothing Then
            item = ProcessAspectReferences(contentControl, contentBlock, item, body, wordDocument, isRequired, areRequiredParametersFilled)
        Else
            contentcontrolName = ProcessOther(contentcontrolName, body, wordDocument, isRequired, areRequiredParametersFilled, firstParagraph, shouldBeUnique, isCustomCustomXmlBlock)
        End If
        Dim childElementIndex As Integer = 0

        For Each openXmlElement As OpenXmlElement In contentBlock.ChildElements
            If Not isCustomCustomXmlBlock _
               AndAlso Not contentcontrolName.Contains(ItemContentName) _
               AndAlso contentControl.Parent.GetType Is GetType(Wordprocessing.Paragraph) _
               AndAlso childElementIndex = 0 _
                Then
                For Each openXmlElementInFirstElement As OpenXmlElement In openXmlElement.ChildElements
                    contentControl.InsertBeforeSelf(openXmlElementInFirstElement.CloneNode(True))
                Next
            ElseIf (contentcontrolName.Contains(ItemContentName) OrElse isCustomCustomXmlBlock) AndAlso contentControl.GetType Is GetType(SdtRun) Then
                contentControl.Parent.InsertBeforeSelf(openXmlElement.CloneNode(True))
            Else
                contentControl.InsertBeforeSelf(openXmlElement.CloneNode(True))
            End If

            childElementIndex = +1
        Next

        contentControl.Remove()
        Return item
    End Function

    Private Function ProcessAspectReferences(
                                             contentControl As SdtElement,
                                             contentBlock As OpenXmlCompositeElement,
                                             item As AssessmentItem,
                                             ByRef body As Body,
                                             ByRef wordDocument As WordprocessingDocument,
                                             isRequired As Boolean,
                                             ByRef areRequiredParametersFilled As Boolean) As AssessmentItem

        If contentControl.GetType IsNot GetType(SdtBlock) Then
            Return item
        End If
        If item.Solution IsNot Nothing _
           AndAlso item.Solution.AspectReferenceSetCollection IsNot Nothing _
           AndAlso Not item.Solution.AspectReferenceSetCollection.Count = 0 _
            Then
            contentBlock.RemoveAllChildren()
            For aspectReferenceIndex As Integer = 0 To item.Solution.AspectReferenceSetCollection.Item(0).Items.Count - 1
                Dim newaspectReferenceBlock As SdtElement = DirectCast(contentControl.Clone, SdtElement)
                Dim aspectReferenceContentControlsInTemplate As List(Of SdtElement) = GetAllCustomBlocksInContentControl(newaspectReferenceBlock)

                RemoveBookmarkFromBlock(newaspectReferenceBlock)
                LoopThroughContentControls(aspectReferenceContentControlsInTemplate, item, body, areRequiredParametersFilled, wordDocument, aspectReferenceIndex, Nothing)

                Dim aspectContentBlock As OpenXmlCompositeElement = GetContentBlockFromSdtElement(DirectCast(newaspectReferenceBlock, SdtElement))
                Dim childElementIndexAspectReference As Integer = 0

                For Each openxmlElementInAspectReferenceBlock As OpenXmlElement In aspectContentBlock.ChildElements
                    If childElementIndexAspectReference <> (aspectContentBlock.ChildElements.Count - 1) _
                       OrElse (openxmlElementInAspectReferenceBlock.GetType IsNot GetType(Wordprocessing.Paragraph)) Then
                        contentBlock.AppendChild(openxmlElementInAspectReferenceBlock.CloneNode(True))
                    End If
                    childElementIndexAspectReference += 1
                Next
            Next
        ElseIf isRequired Then
            areRequiredParametersFilled = False
        End If
        Return item
    End Function

    Private Function ProcessOther(
                                  contentcontrolName As String,
                                  ByRef body As Body,
                                  wordDocument As WordprocessingDocument,
                                  isRequired As Boolean,
                                  ByRef areRequiredParametersFilled As Boolean,
                                  firstParagraph As Wordprocessing.Paragraph,
                                  shouldBeUnique As Boolean,
                                  ByRef isCustomCustomXmlBlock As Boolean) As String

        Dim customXmlBlock As OpenXmlElement = FindCustomXml(contentcontrolName, body)

        If customXmlBlock Is Nothing Then
            If Not contentcontrolName.StartsWith(PropertiesToOtherBookletsPrefix) Then
                contentcontrolName = String.Concat(PropertiesToOtherBookletsPrefix, contentcontrolName)
            End If

            customXmlBlock = FindCustomXml(contentcontrolName, body)
        End If

        isCustomCustomXmlBlock = CustomXmlBlockHasCustomElementType(customXmlBlock)

        If customXmlBlock IsNot Nothing AndAlso ElementContainsCustomXmlElementsThatShouldbeResolved(customXmlBlock) Then
            Dim xmlElement As OpenXmlElement = DirectCast(customXmlBlock.Clone, OpenXmlElement)
            Dim parameterIsFilled As Boolean = True

            For Each customXmlElementType As CustomXmlElementType In [Enum].GetValues(GetType(CustomXmlElementType))
                Dim customXmlTypeIsFilled As Boolean = ResolveCustomXmlElement(xmlElement, wordDocument.MainDocumentPart, customXmlElementType, shouldBeUnique)

                If isRequired AndAlso parameterIsFilled Then
                    parameterIsFilled = customXmlTypeIsFilled
                End If
            Next

            If parameterIsFilled Then
                For Each elementInsideCustomBlock As OpenXmlElement In xmlElement.ChildElements
                    Dim elementToAdd As OpenXmlElement = DirectCast(elementInsideCustomBlock.Clone(), OpenXmlElement)

                    If Not isCustomCustomXmlBlock Then
                        elementToAdd = New Wordprocessing.Paragraph(elementToAdd)
                    End If

                    firstParagraph.InsertBeforeSelf(DirectCast(elementInsideCustomBlock.Clone, OpenXmlElement))
                Next

                firstParagraph.Remove()
            ElseIf isRequired Then
                areRequiredParametersFilled = False
            End If
        ElseIf isRequired Then
            areRequiredParametersFilled = False
        End If
        Return contentcontrolName
    End Function


    Private Sub AddPropertiesToDocument(
                                           item As AssessmentItem,
                                           contentControlName As String,
                                           ByRef body As Body,
                                           ByRef wordDocument As WordprocessingDocument,
                                           propertyIsRequired As Boolean,
                                           ByRef areRequiredParametersFilled As Boolean,
                                           ByRef firstParagraph As Wordprocessing.Paragraph,
                                           index As Nullable(Of Integer),
                                           aspect As Aspect,
                                           newRun As Wordprocessing.Run)

        Dim isAdded As Boolean = False
        If item IsNot Nothing Then
            If contentControlName.Contains(ItemPrefix) Then
                Dim contentControlNameWithoutItem As String = contentControlName.Replace(ItemPrefix, String.Empty)

                Select Case True
                    Case ContentProperty.Split(CChar("|")).Contains(contentControlNameWithoutItem)
                        Dim newBodyToParseTemplate As Body = DirectCast(body.Clone, Body)

                        AddParsedTemplateToParagraph(newBodyToParseTemplate, wordDocument, firstParagraph)
                        isAdded = True
                    Case CodeProperty.Split(CChar("|")).Contains(contentControlNameWithoutItem)
                        newRun.AppendChild(New Wordprocessing.Text(item.Identifier.ToString))
                    Case MaxScoreProperty.Split(CChar("|")).Contains(contentControlNameWithoutItem)
                        If item.Solution IsNot Nothing _
                           AndAlso Not String.IsNullOrEmpty(item.Solution.MaxSolutionTranslatedScore.ToString) _
                           AndAlso Not item.Solution.MaxSolutionTranslatedScore = 0 _
                            Then
                            Dim maxScore As Decimal = GetItemMaxScore(item, item.Solution.MaxSolutionTranslatedScore.Value)
                            newRun.AppendChild(New Wordprocessing.Text(maxScore.ToString()))
                        ElseIf propertyIsRequired Then
                            areRequiredParametersFilled = False
                        End If
                    Case SequenceNumberDotProperty.Split(CChar("|")).Contains(contentControlNameWithoutItem)
                        If _showItemCounter Then
                            newRun.AppendChild(New Wordprocessing.Text(_regularItemCounter.ToString & "."))
                        End If
                    Case SequenceNumberProperty.Split(CChar("|")).Contains(contentControlNameWithoutItem)
                        If _showItemCounter Then
                            newRun.AppendChild(New Wordprocessing.Text(_regularItemCounter.ToString))
                        End If
                    Case KeyProperty.Split(CChar("|")).Contains(contentControlNameWithoutItem)
                        If item.Solution IsNot Nothing AndAlso item.Solution.Findings IsNot Nothing AndAlso Not String.IsNullOrEmpty(item.Solution.Findings.ToString) Then
                            newRun.AppendChild(New Wordprocessing.Text(item.Solution.Findings.ToString()))
                        ElseIf propertyIsRequired Then
                            areRequiredParametersFilled = False
                        End If
                    Case ItemTemplateProperty.Split(CChar("|")).Contains(contentControlNameWithoutItem)
                        If String.IsNullOrEmpty(item.LayoutTemplateSourceName.ToString) AndAlso propertyIsRequired Then
                            areRequiredParametersFilled = False
                        End If
                        newRun.AppendChild(New Wordprocessing.Text(item.LayoutTemplateSourceName.ToString))
                    Case TitleProperty.Split(CChar("|")).Contains(contentControlNameWithoutItem)
                        If String.IsNullOrEmpty(item.Title.ToString) AndAlso propertyIsRequired Then
                            areRequiredParametersFilled = False
                        End If
                        newRun.AppendChild(New Wordprocessing.Text(item.Title.ToString))
                    Case ItemTypeProperty.Split(CChar("|")).Contains(contentControlNameWithoutItem)
                        Dim itemType As String = GetMetaData(item.LayoutTemplateSourceName, ItemTypeName)
                        newRun.AppendChild(New Wordprocessing.Text(itemType))
                    Case Else
                        Dim customPropertyValue As String = GetMetaData(item.Identifier, contentControlNameWithoutItem)

                        If Not String.IsNullOrEmpty(customPropertyValue) Then
                            newRun.AppendChild(New Wordprocessing.Text(customPropertyValue))
                        ElseIf item.Item(contentControlName.Replace(ItemPrefix, String.Empty)) IsNot Nothing _
                               AndAlso Not String.IsNullOrEmpty(item.Item(contentControlNameWithoutItem)) _
                            Then
                            newRun.AppendChild(New Wordprocessing.Text(item.Item(contentControlNameWithoutItem).ToString))
                        End If
                End Select
            End If

            If contentControlName.Contains(AspectReferencePrefix) _
               AndAlso index.HasValue _
               AndAlso item.Solution IsNot Nothing _
               AndAlso item.Solution.AspectReferenceSetCollection IsNot Nothing _
               AndAlso Not item.Solution.AspectReferenceSetCollection.Count = 0 _
               AndAlso item.Solution.AspectReferenceSetCollection(0).Items.Count > index _
                Then
                If _aspectDictionary Is Nothing Then
                    _aspectDictionary = New Dictionary(Of String, Aspect)
                End If

                Dim aspectSourceName As String = item.Solution.AspectReferenceSetCollection(0).Items(index.Value).SourceName

                If Not _aspectDictionary.ContainsKey(aspectSourceName) Then
                    Dim eventArgs As New ResourceNeededEventArgs(aspectSourceName, GetType(Aspect))

                    ResourceNeeded(Me, eventArgs)

                    If eventArgs.BinaryResource IsNot Nothing AndAlso eventArgs.BinaryResource.ResourceObject IsNot Nothing Then
                        _aspectDictionary.Add(aspectSourceName, DirectCast(eventArgs.BinaryResource.ResourceObject, Aspect))
                    End If
                End If

                Dim contentControlNameWithoutAspectReferencePrefix As String = contentControlName.Replace(AspectReferencePrefix, String.Empty)

                Select Case True
                    Case CodeProperty.Split(CChar("|")).Contains(contentControlNameWithoutAspectReferencePrefix)
                        newRun.AppendChild(New Wordprocessing.Text(item.Solution.AspectReferenceSetCollection(0).Items(index.Value).SourceName))
                    Case DescriptionProperty.Split(CChar("|")).Contains(contentControlNameWithoutAspectReferencePrefix)
                        Dim xHtmlString As String = item.Solution.AspectReferenceSetCollection(0).Items(index.Value).Description

                        If String.IsNullOrEmpty(xHtmlString) Then
                            xHtmlString = _aspectDictionary(aspectSourceName).Description
                        End If

                        If Not String.IsNullOrEmpty(xHtmlString) Then
                            Dim fakeBody As New Body(String.Format("<w:body xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main""><w:tbl><w:tr><w:tc><w:p><w:customXml w:element=""xhtmlElementToOpenXml"">{0}</w:customXml></w:p></w:tc></w:tr></w:tbl></w:body>", xHtmlString))

                            ResolveCustomXmlElement(CType(fakeBody, OpenXmlElement), wordDocument.MainDocumentPart, CustomXmlElementType.xhtmlElementToOpenXml, False)

                            Dim firstTableCell As Wordprocessing.TableCell = fakeBody.Descendants(Of Wordprocessing.TableCell).FirstOrDefault
                            For Each OpenXmlElement As OpenXmlElement In firstTableCell.ChildElements
                                firstParagraph.InsertBeforeSelf(OpenXmlElement.CloneNode(True))
                            Next

                            isAdded = True
                        End If
                    Case MaxScoreProperty.Split(CChar("|")).Contains(contentControlNameWithoutAspectReferencePrefix)
                        Dim maxScore As Decimal = GetItemMaxScore(item, item.Solution.AspectReferenceSetCollection(0).Items(index.Value).MaxScore)
                        newRun.AppendChild(New Wordprocessing.Text(maxScore.ToString))
                    Case TitleProperty.Split("|"c).Contains(contentControlNameWithoutAspectReferencePrefix)
                        newRun.AppendChild(New Wordprocessing.Text(_aspectDictionary(aspectSourceName).Title))
                    Case Else
                        If item.Solution.AspectReferenceSetCollection(0).Items(index.Value).Item(contentControlNameWithoutAspectReferencePrefix) IsNot Nothing _
                           AndAlso Not String.IsNullOrEmpty(item.Solution.AspectReferenceSetCollection(0).Items(index.Value).Item(contentControlNameWithoutAspectReferencePrefix)) _
                            Then
                            newRun.AppendChild(New Wordprocessing.Text(item.Solution.AspectReferenceSetCollection(0).Items(index.Value).Item(contentControlNameWithoutAspectReferencePrefix).ToString))
                        End If
                End Select
            End If
        ElseIf aspect IsNot Nothing Then
            Dim contentControlNameWithoutAspectPrefix As String = contentControlName.Replace(AspectPrefix, String.Empty)

            Select Case True
                Case CodeProperty.Split(CChar("|")).Contains(contentControlNameWithoutAspectPrefix)
                    newRun.AppendChild(New Wordprocessing.Text(aspect.Identifier.ToString))
                Case TitleProperty.Split(CChar("|")).Contains(contentControlNameWithoutAspectPrefix)
                    newRun.AppendChild(New Wordprocessing.Text(aspect.Title.ToString))
                Case MaxScoreProperty.Split(CChar("|")).Contains(contentControlNameWithoutAspectPrefix)
                    Dim maxScore = GetItemMaxScore(item, aspect.MaxScore)
                    newRun.AppendChild(New Wordprocessing.Text(maxScore.ToString))
                Case DescriptionProperty.Split(CChar("|")).Contains(contentControlNameWithoutAspectPrefix)
                    Dim xHtmlString As String = aspect.Description

                    If Not String.IsNullOrEmpty(xHtmlString) Then
                        Dim mhtFile As MhtFile = CreateMhtFile(xHtmlString, True)

                        Dim altChunk As New AltChunk()
                        altChunk.Id = AddAlternativeFormatImportPartToDocument(mhtFile.FinalizeMht,
                                                                               AlternativeFormatImportPartType.Mht, wordDocument.MainDocumentPart,
                                                                               Encoding.ASCII, _altChunkIdCounter)
                        firstParagraph.AppendChild(New Wordprocessing.Run(altChunk))
                        isAdded = True
                    End If
                Case Else
                    If aspect.Item(contentControlNameWithoutAspectPrefix) IsNot Nothing _
                       AndAlso Not String.IsNullOrEmpty(aspect.Item(contentControlNameWithoutAspectPrefix)) _
                        Then
                        newRun.AppendChild(New Wordprocessing.Text(aspect.Item(contentControlNameWithoutAspectPrefix).ToString))
                    End If
            End Select
        ElseIf [Enum].IsDefined(GetType(TestProperties), contentControlName) Then
            Dim testproperty As TestProperties = DirectCast([Enum].Parse(GetType(TestProperties), contentControlName), TestProperties)

            Select Case testproperty
                Case TestProperties.test_itemcount
                    newRun.AppendChild(New Wordprocessing.Text(_regularItemCounter.ToString))
                Case TestProperties.test_maxscore
                    newRun.AppendChild(New Wordprocessing.Text(_maxScoreTest.ToString))
                Case TestProperties.test_version
                    newRun.AppendChild(New Wordprocessing.Text(_versionNumberTest))
            End Select
        ElseIf [Enum].IsDefined(GetType(DocFields), contentControlName) Then
            Dim docfield As DocFields = DirectCast([Enum].Parse(GetType(DocFields), contentControlName), DocFields)

            Select Case docfield
                Case DocFields.num_pages
                    Dim fldchrBegin As New FieldChar
                    fldchrBegin.FieldCharType = FieldCharValues.Begin
                    fldchrBegin.Dirty = True

                    Dim fld As New FieldCode(" NUMPAGES \\* MERGEFORMAT ")
                    Dim fldchrEnd As New FieldChar

                    fldchrEnd.FieldCharType = FieldCharValues.End
                    firstParagraph.AppendChild(New Wordprocessing.Run(fldchrBegin))
                    firstParagraph.AppendChild(New Wordprocessing.Run(fld))
                    firstParagraph.AppendChild(New Wordprocessing.Run(fldchrEnd))
            End Select
        ElseIf contentControlName.Contains(TestPrefix) Then
            Dim contentControlNameWithoutTest As String = contentControlName.Replace(TestPrefix, String.Empty)

            Dim customPropertyValue As String = GetMetaData(_testContext.Identifier, contentControlNameWithoutTest)
            If Not String.IsNullOrEmpty(customPropertyValue) Then
                newRun.AppendChild(New Wordprocessing.Text(customPropertyValue))
            End If
        End If

        If Not isAdded Then
            If newRun Is Nothing Then
                newRun = New Wordprocessing.Run(New Wordprocessing.Text(String.Empty))
                If propertyIsRequired Then
                    areRequiredParametersFilled = False
                End If
            End If

            firstParagraph.AppendChild(newRun)
        End If
    End Sub

    Private Function GetVersionNumber(version As Integer) As String
        Return String.Format("{0}.{1}", version \ 10000, version Mod 10000)
    End Function

    Private Function GetItemMaxScore(item As AssessmentItem, solutionMaxScore As Decimal) As Decimal
        Dim itemRef As ItemReferenceViewBase = If(_testContext IsNot Nothing, _testContext.GetItemReferenceByName(item.Identifier), Nothing)

        If itemRef IsNot Nothing Then
            solutionMaxScore = solutionMaxScore * CType(itemRef.Weight, Decimal)
        End If

        Return solutionMaxScore
    End Function

    Private Function GetMetaData(resourceName As String, metadataname As String) As String
        Dim metaDataCollection As MetaDataCollection = TestSessionContext.GetResourceMetaData(resourceName)

        For Each md As MetaData In metaDataCollection
            If md.Name.Equals(metadataname, StringComparison.InvariantCultureIgnoreCase) Then
                Return md.Value
            End If
        Next

        Return String.Empty
    End Function

    Private Sub AddParsedTemplateToParagraph(body As Body, worddocument As WordprocessingDocument, firstParagraphInContentControl As Wordprocessing.Paragraph)
        RemoveBlocksThatShouldBeIgnored(body, True)

        For Each customXmlElementType As CustomXmlElementType In [Enum].GetValues(GetType(CustomXmlElementType))
            ResolveCustomXmlElement(CType(body, OpenXmlElement), worddocument.MainDocumentPart, customXmlElementType, False)
        Next

        For Each openXmlElement As OpenXmlElement In body.ChildElements
            AddElementBeforeFirstParagraphInContentControl(firstParagraphInContentControl, openXmlElement.CloneNode(True))
        Next
    End Sub

    Private Function ParagraphContainsText(ByRef paragraph As Wordprocessing.Paragraph) As Boolean
        Dim returnValue As Boolean = Not paragraph.ChildElements.Count = 0

        If returnValue Then
            Dim res = From sd In paragraph.Descendants(Of Wordprocessing.Text)()
            returnValue = Not res.ToList.Count = 0
        End If

        Return returnValue
    End Function

    Private Function FindCustomBlock(name As String, ByRef body As Body) As SdtElement
        Dim res = From sd In body.Descendants(Of SdtAlias)()
                  Where String.Equals(sd.Val, name, StringComparison.OrdinalIgnoreCase)
                  Select sd

        If res IsNot Nothing _
           AndAlso res.FirstOrDefault IsNot Nothing _
           AndAlso res.FirstOrDefault.Parent IsNot Nothing _
           AndAlso res.FirstOrDefault.Parent.Parent IsNot Nothing _
            Then
            Dim sdtContentBlock = res.FirstOrDefault.Parent.Parent

            Return (TryCast(sdtContentBlock, SdtElement))
        Else
            Return Nothing
        End If
    End Function

    Private Function FindMultipleCustomBlocksByType(startsWithName As String, ByRef wordDoc As WordprocessingDocument) As List(Of SdtElement)
        Dim returnValue As New List(Of SdtElement)

        Dim res = From sd In wordDoc.MainDocumentPart.Document.Body.Descendants(Of SdtAlias)()
                  Where sd.Val.ToString.StartsWith(startsWithName)
                  Select sd

        AddBlocks(res, returnValue)

        For Each header As HeaderPart In wordDoc.MainDocumentPart.HeaderParts
            Dim resHeader = From sd In header.Header.Descendants(Of SdtAlias)()
                            Where sd.Val.ToString.StartsWith(startsWithName)
                            Select sd

            AddBlocks(resHeader, returnValue)
        Next
        For Each footer As FooterPart In wordDoc.MainDocumentPart.FooterParts
            Dim resFooter = From sd In footer.Footer.Descendants(Of SdtAlias)()
                            Where sd.Val.ToString.StartsWith(startsWithName)
                            Select sd

            AddBlocks(resFooter, returnValue)
        Next
        Return returnValue
    End Function

    Private Function FindMultipleCustomBlocks(name As String, ByRef wordDoc As WordprocessingDocument) As List(Of SdtElement)
        Dim returnValue As New List(Of SdtElement)

        Dim res = From sd In wordDoc.MainDocumentPart.Document.Body.Descendants(Of SdtAlias)()
                  Where String.Equals(sd.Val, name, StringComparison.OrdinalIgnoreCase)
                  Select sd
        AddBlocks(res, returnValue)

        For Each header As HeaderPart In wordDoc.MainDocumentPart.HeaderParts
            Dim resHeader = From sd In header.Header.Descendants(Of SdtAlias)()
                            Where String.Equals(sd.Val, name, StringComparison.OrdinalIgnoreCase)
                            Select sd
            AddBlocks(resHeader, returnValue)
        Next

        For Each footer As FooterPart In wordDoc.MainDocumentPart.FooterParts
            Dim resFooter = From sd In footer.Footer.Descendants(Of SdtAlias)()
                            Where String.Equals(sd.Val, name, StringComparison.OrdinalIgnoreCase)
                            Select sd
            AddBlocks(resFooter, returnValue)
        Next

        Return returnValue
    End Function

    Private Sub AddBlocks(res As IEnumerable(Of SdtAlias), returnValue As List(Of SdtElement))

        If res IsNot Nothing _
           AndAlso res.FirstOrDefault IsNot Nothing _
           AndAlso res.FirstOrDefault.Parent IsNot Nothing _
           AndAlso res.FirstOrDefault.Parent.Parent IsNot Nothing _
            Then
            For Each sdtAlias As SdtAlias In res.ToList
                If sdtAlias.Parent IsNot Nothing AndAlso sdtAlias.Parent.Parent IsNot Nothing Then
                    returnValue.Add(DirectCast(sdtAlias.Parent.Parent, SdtElement))
                End If
            Next
        End If
    End Sub


    Private Function FindMultipleCustomXml(name As String, ByRef element As OpenXmlElement) As List(Of CustomXmlElement)
        Dim res = From bm In element.Descendants(Of CustomXmlElement)()
                  Where String.Equals(bm.Element, name, StringComparison.OrdinalIgnoreCase)
                  Select bm
        Return res.ToList
    End Function

    Private Function GetAllCustomBlocksInContentControlWithoutLoop(element As SdtElement) As List(Of SdtElement)
        Dim res = From sd In element.Descendants(Of SdtAlias)()
                  Where Not (sd.Val.ToString.ToLower.Trim.Contains(LoopSuffix)) AndAlso Not _
                                                                                        sd.Val.ToString = GetCustomBlockName(element)
                  Select sd

        If res IsNot Nothing AndAlso res.ToList IsNot Nothing Then
            Dim returnList As New List(Of SdtElement)

            For Each sdtAlias As SdtAlias In res.ToList
                returnList.Add(DirectCast(sdtAlias.Parent.Parent, SdtElement))
            Next

            Return returnList
        Else
            Return Nothing
        End If
    End Function

    Private Function GetAllCustomBlocksInContentControl(element As OpenXmlElement) As List(Of SdtElement)
        Dim res = From sd In element.Descendants(Of SdtElement)()
                  Select sd

        Return res.ToList
    End Function


    Private Function GetCustomBlockName(customBlock As SdtElement) As String
        Dim returnString As String = String.Empty

        If customBlock IsNot Nothing Then
            Dim res = From sd In customBlock.Descendants(Of SdtAlias)()
                      Select sd

            If res.FirstOrDefault IsNot Nothing Then
                returnString = DirectCast(res.FirstOrDefault, SdtAlias).Val
            End If
        End If

        Return returnString
    End Function

    Private Function FindCustomXml(name As String, ByRef openXml As Body) As CustomXmlElement
        Dim res = From bm In openXml.Descendants(Of CustomXmlElement)()
                  Where String.Equals(bm.Element, name, StringComparison.OrdinalIgnoreCase)
                  Select bm

        Dim contentBlock As CustomXmlElement = res.FirstOrDefault

        Return contentBlock
    End Function


    Private Function GetContentBlockFromSdtElement(ByRef sdtElement As SdtElement) As OpenXmlCompositeElement
        Dim res = From bm In sdtElement.Descendants(Of OpenXmlCompositeElement)()
                  Where bm.GetType Is GetType(SdtContentBlock) OrElse bm.GetType Is GetType(SdtContentRun) OrElse bm.GetType Is GetType(SdtContentCell)

        Return res.FirstOrDefault
    End Function

    Private Function GetFirstParagraphFromContentBlock(ByRef contentBlock As OpenXmlCompositeElement, ByRef contentControl As SdtElement) As Wordprocessing.Paragraph
        Dim returnValue As Wordprocessing.Paragraph = Nothing

        If contentBlock Is Nothing Then
            Return Nothing
        End If

        Dim res = From bm In contentBlock.Descendants(Of Wordprocessing.Paragraph)()
        returnValue = res.FirstOrDefault

        If returnValue Is Nothing Then
            contentBlock.RemoveAllChildren()
            returnValue = New Wordprocessing.Paragraph()

            If contentControl.Parent IsNot Nothing AndAlso contentControl.Parent.GetType Is GetType(Wordprocessing.Paragraph) Then
                Dim resParagraphProperties = From ppr In contentControl.Parent.Descendants(Of Wordprocessing.ParagraphProperties)()
                Dim paragraphProperties As Wordprocessing.ParagraphProperties = resParagraphProperties.FirstOrDefault

                If (paragraphProperties IsNot Nothing) Then
                    returnValue.AppendChild(paragraphProperties.CloneNode(True))
                End If
            End If

            contentBlock.AppendChild(returnValue)
        Else
            returnValue.RemoveAllChildren()
        End If


        Return returnValue
    End Function

    Private Function GetFirstRunFromContentBlock(ByRef contentBlock As OpenXmlCompositeElement) As Wordprocessing.Run
        Dim returnValue As Wordprocessing.Run = Nothing

        If contentBlock Is Nothing Then
            Return Nothing
        End If
        Dim res = From bm In contentBlock.Descendants(Of Wordprocessing.Run)()
        returnValue = res.FirstOrDefault

        If returnValue Is Nothing Then
            returnValue = New Wordprocessing.Run()
        Else
            returnValue.RemoveAllChildren(Of Wordprocessing.Text)()
        End If

        Return returnValue
    End Function


    Private Function ResolveCustomXmlElement(
            ByRef element As OpenXmlElement,
            ByRef mainPart As MainDocumentPart,
            ByRef customXmlType As CustomXmlElementType,
            ByRef shouldBeUnique As Boolean,
            Optional forInlineChoiceAlternative As Boolean = False) As Boolean

        Dim isElementFilled As Boolean = True

        Dim customElementList As List(Of CustomXmlElement) = FindMultipleCustomXml(String.Concat(customXmlType.ToString), element)
        For Each customElement As CustomXmlElement In customElementList

            Select Case customXmlType
                Case CustomXmlElementType.imageElement
                    isElementFilled = AddImagesToDocument(customElement, mainPart)
                Case CustomXmlElementType.xhtmlElement
                    Dim asOpenXmlElement = DirectCast(customElement, OpenXmlElement)
                    isElementFilled = ProcessXhtml(asOpenXmlElement, _altChunkIdCounter, mainPart)
                Case CustomXmlElementType.xhtmlElementToOpenXml
                    isElementFilled = ProcessXhtmlToOpenXml(customElement, forInlineChoiceAlternative, mainPart)
                Case CustomXmlElementType.wordElement
                    isElementFilled = AddWordFragmentToDocument(customElement, mainPart, shouldBeUnique)
                Case CustomXmlElementType.wordContent
                    isElementFilled = AddWordContentToDocument(customElement, shouldBeUnique)
            End Select
        Next

        Return isElementFilled
    End Function

    Private Function ElementContainsCustomXmlElementsThatShouldbeResolved(ByRef element As OpenXmlElement) As Boolean
        Dim res = From bm In element.Descendants(Of CustomXmlElement)()
                  Where [Enum].GetValues(GetType(CustomXmlElementType)).Cast(Of CustomXmlElementType)().ToList().Select(Function(v) v.ToString()).Contains(bm.Element, StringComparer.InvariantCultureIgnoreCase)
                  Select bm

        Return Not res.Count = 0
    End Function


    Private Function ProcessXhtml(ByRef customElement As OpenXmlElement, ByRef altChunkIdCounter As Integer, ByRef mainPart As MainDocumentPart) As Boolean
        Dim xhtmlString As String = customElement.InnerXml

        If Not String.IsNullOrEmpty(xhtmlString) Then
            Dim mhtFile As MhtFile = CreateMhtFile(xhtmlString, True)
            Dim altChunk As New AltChunk()
            altChunk.Id = AddAlternativeFormatImportPartToDocument(mhtFile.FinalizeMht,
                                                                   AlternativeFormatImportPartType.Mht, mainPart,
                                                                   Encoding.ASCII, altChunkIdCounter)
            customElement.Parent.ReplaceChild(New Wordprocessing.Run(altChunk), customElement)

            Return True
        Else
            customElement.Remove()

            Return False
        End If
    End Function

    Private Function ReplaceSpecialXmlCharacters(source As String) As String
        Dim xmlDoc As New XmlDocument()

        Dim result As String = source.Replace(vbNewLine, String.Empty)

        result = result.Replace(vbLf, String.Empty)

        Try
            xmlDoc.LoadXml(result)
            Return source

        Catch xmlEx As XmlException
            Dim partWhereTheErrorIs As String = result.Substring(0, xmlEx.LinePosition - 1)
            Dim positionOfCharacterToReplace As Integer = partWhereTheErrorIs.LastIndexOf("&")
            Dim escapedChar As String = String.Empty

            If positionOfCharacterToReplace <> -1 AndAlso positionOfCharacterToReplace = xmlEx.LinePosition - 2 Then
                escapedChar = "&amp;"
            Else
                positionOfCharacterToReplace = partWhereTheErrorIs.LastIndexOf("<")
                escapedChar = "&lt;"
            End If

            Dim partBeforeIllegalCharacter As String = result.Substring(0, positionOfCharacterToReplace)
            Dim partAfterIllegalCharacter As String = result.Substring(positionOfCharacterToReplace + 1)

            result = partBeforeIllegalCharacter & escapedChar & partAfterIllegalCharacter
            result = ReplaceSpecialXmlCharacters(result)
        End Try

        Return result
    End Function

    Friend Function ProcessXhtmlToOpenXml(ByRef customElement As CustomXmlElement, forInlineChoiceAlternative As Boolean, mainPart As MainDocumentPart) As Boolean
        Dim xhtmlString As String = ReplaceSpecialXmlCharacters(customElement.OuterXml)
        xhtmlString = SetPreserveWhiteSpaces(xhtmlString)

        If (MathMLHelper.HtmlContainsMathML(xhtmlString)) Then
            UpdateMathML(xhtmlString)
        End If

        Dim imghelper = New ImageHelper(_resourceManager)

        imghelper.DownloadImages(xhtmlString)
        RemoveTtsStyles(xhtmlString)
        UpdateStyles(xhtmlString)
        ConvertDivToParagraph(xhtmlString)
        CleanupHtmlBeforeConversion(xhtmlString)

        StyleHelper.ExtractStyles(xhtmlString)
        Dim openXmlCompositeElementList As IList(Of OpenXmlCompositeElement) = _htmlConverter.Parse(xhtmlString)
        _htmlConverter.RefreshStyles()

        Dim firstParagraphProcessed As Boolean = False

        CheckOpenXmlComposites(customElement, openXmlCompositeElementList, firstParagraphProcessed)

        AddAttributesToLastParagraph(customElement)

        customElement.Remove()

        Return True
    End Function

    Private Sub CheckOpenXmlComposites(
            customElement As CustomXmlElement,
            openXmlCompositeElementList As IList(Of OpenXmlCompositeElement),
            firstParagraphProcessed As Boolean)

        For Each openXmlCompositeElement As OpenXmlCompositeElement In openXmlCompositeElementList
            CheckOpenXmlCompositeElement(openXmlCompositeElement, firstParagraphProcessed, customElement)
        Next
    End Sub

    Private Sub CheckOpenXmlCompositeElement(openXmlCompositeElement As OpenXmlCompositeElement, ByRef firstParagraphProcessed As Boolean, customElement As CustomXmlElement)

        Select Case openXmlCompositeElement.GetType().ToString()
            Case GetType(Wordprocessing.Paragraph).ToString()

                CheckIndentation(openXmlCompositeElement)

                If Not firstParagraphProcessed Then
                    For Each openxmlElement As OpenXmlElement In openXmlCompositeElement.ChildElements
                        customElement.Parent.AppendChild(openxmlElement.CloneNode(True))
                    Next

                    firstParagraphProcessed = True
                Else
                    customElement.Parent.Parent.AppendChild(openXmlCompositeElement)
                End If

            Case GetType(Wordprocessing.Table).ToString()

                RemoveParagraphsFromTable(openXmlCompositeElement)
                RemoveSpacingFromCells(openXmlCompositeElement)
                CorrectTableCellStyles(CType(openXmlCompositeElement, Wordprocessing.Table))

                If Not firstParagraphProcessed Then
                    customElement.Parent.AppendChild(New Wordprocessing.Run(openXmlCompositeElement.CloneNode(False)))

                    For Each openxmlElement As OpenXmlElement In openXmlCompositeElement
                        customElement.Parent.LastChild.FirstChild.AppendChild(openxmlElement.CloneNode(True))
                    Next

                    firstParagraphProcessed = True
                Else
                    customElement.Parent.Parent.AppendChild(openXmlCompositeElement.CloneNode(False))

                    For Each openxmlElement As OpenXmlElement In openXmlCompositeElement
                        customElement.Parent.Parent.LastChild.AppendChild(openxmlElement.CloneNode(True))
                    Next

                    If customElement.Parent.Parent.GetType().ToString() = GetType(Wordprocessing.TableCell).ToString() Then
                        If Not customElement.Parent.Parent.LastChild.GetType().ToString() = GetType(Wordprocessing.Paragraph).ToString() Then
                            customElement.Parent.Parent.AppendChild(New Wordprocessing.Paragraph)
                        End If
                    End If
                End If
        End Select
    End Sub

    Private Sub CorrectTableCellStyles(table As Wordprocessing.Table)
        For Each tableCell As Wordprocessing.TableCell In table.Descendants(Of Wordprocessing.TableCell)
            For Each styleName As String In tableCell.TableCellProperties?.ChildElements.OfType(Of RunStyle).Select(Function(rs) rs.Val.Value)
                If Not String.IsNullOrEmpty(styleName) AndAlso StyleHelper.StylesList.ContainsKey(styleName) Then
                    Dim properties = StyleHelper.StylesList(styleName)
                    For Each pr In properties
                        tableCell.TableCellProperties.Append(pr.CloneNode(True))
                    Next

                    Exit For
                End If
            Next
        Next
    End Sub

    Private Sub AddAttributesToLastParagraph(customElement As CustomXmlElement)
        If Not customElement.Parent.Parent.LastChild.GetType().ToString().Equals(GetType(Wordprocessing.Paragraph).ToString()) Then
            Return
        End If

        Dim prop As Wordprocessing.ParagraphProperties = customElement.Parent.Parent.LastChild.GetFirstChild(Of Wordprocessing.ParagraphProperties)()

        If prop IsNot Nothing Then
            Return
        End If

        Dim spacing As SpacingBetweenLines = New SpacingBetweenLines With {.After = "0"}

        If (customElement.Parent.Parent.LastChild.HasChildren) Then
            customElement.Parent.Parent.LastChild.FirstChild.InsertBeforeSelf(New Wordprocessing.ParagraphProperties).Append(spacing)
        Else
            customElement.Parent.Parent.LastChild.AppendChild(spacing)
        End If
    End Sub

    Private Sub CleanupHtmlBeforeConversion(ByRef xHtmlString As String)
        xHtmlString = xHtmlString.Replace("color:#;", ";")
    End Sub

    Private Sub RemoveSpacingFromCells(ByRef table As OpenXmlCompositeElement)
        For childElementOfTable As Integer = table.ChildElements.Count - 1 To 0 Step -1
            If Not (TypeOf table.ChildElements(childElementOfTable) Is Wordprocessing.TableRow) Then
                Continue For
            End If

            For childElementOfRow As Integer = table.ChildElements(childElementOfTable).ChildElements.Count - 1 To 0 Step -1
                If Not (TypeOf table.ChildElements(childElementOfTable).ChildElements(childElementOfRow) Is Wordprocessing.TableCell) Then
                    Continue For
                End If

                For childElementOfTableCell As Integer = table.ChildElements(childElementOfTable).ChildElements(childElementOfRow).ChildElements.Count - 1 To 0 Step -1
                    Dim paragraphOrTable As OpenXmlElement = table.ChildElements(childElementOfTable).ChildElements(childElementOfRow).ChildElements(childElementOfTableCell)

                    Select Case paragraphOrTable.GetType().Name.ToString()
                        Case "Paragraph"
                            RemoveSpacesFromParagraph(paragraphOrTable)
                        Case ("Table")
                            RemoveSpacingFromCells(DirectCast(table.ChildElements(childElementOfTable).ChildElements(childElementOfRow).ChildElements(childElementOfTableCell), OpenXmlCompositeElement))
                    End Select
                Next
            Next
        Next
    End Sub

    Private Sub RemoveSpacesFromParagraph(paragraphOrTable As OpenXmlElement)

        Dim paragraph As Wordprocessing.Paragraph = DirectCast(paragraphOrTable, Wordprocessing.Paragraph)
        Dim paragraphProperties As New Wordprocessing.ParagraphProperties()
        Dim spacingBetweenLines As New SpacingBetweenLines()

        spacingBetweenLines.After = "0"
        spacingBetweenLines.LineRule = LineSpacingRuleValues.Auto
        paragraphProperties.SpacingBetweenLines = spacingBetweenLines

        If paragraph.ChildElements.Count = 0 Then
            paragraph.AppendChild(Of OpenXmlElement)(paragraphProperties)
        Else
            paragraph.InsertBefore(Of OpenXmlElement)(paragraphProperties, paragraph.ChildElements.First)
        End If
    End Sub

    Private Sub CheckIndentation(element As OpenXmlCompositeElement)
        Dim prop As Wordprocessing.ParagraphProperties = element.GetFirstChild(Of Wordprocessing.ParagraphProperties)()
        If prop Is Nothing Then
            Return
        End If

        Dim numprop As NumberingProperties = prop.GetFirstChild(Of NumberingProperties)()
        If numprop Is Nothing Then
            Return
        End If

        Dim numLvlref As NumberingLevelReference = numprop.GetFirstChild(Of NumberingLevelReference)()
        If numLvlref Is Nothing Then
            Return
        End If

        Dim numLvl As Integer = Convert.ToInt32(numLvlref.Val.ToString())
        Dim ind As Indentation = prop.GetFirstChild(Of Indentation)()

        If ind Is Nothing Then
            Return
        End If

        Dim indLeft As Integer = Convert.ToInt32(ind.Left.Value)

        If indLeft <> ((numLvl + 1) * 357) Then
            ind.Left.Value = ((numLvl + 1) * 357).ToString()
        End If
    End Sub

    Private Function GetMathJustificationValue(paragraphJustification As String) As JustificationValues
        Select Case paragraphJustification.ToLower
            Case "right"
                Return JustificationValues.Right
            Case "center"
                Return JustificationValues.Center
            Case Else
                Return JustificationValues.Left
        End Select
    End Function

    Private Sub RemoveParagraphsFromTable(ByRef table As OpenXmlCompositeElement)

        If Not table.HasChildren Then Return

        If table.GetType().ToString() Is GetType(Wordprocessing.Table).ToString() Then
            For a As Integer = table.ChildElements.Count - 1 To 0 Step -1
                If table.ChildElements(a).GetType().ToString() Is GetType(Wordprocessing.TableRow).ToString() Then
                    For b As Integer = table.ChildElements(a).ChildElements.Count - 1 To 0 Step -1
                        RemoveParagraphsFromCell(table, a, b)
                    Next
                End If
            Next
        End If
    End Sub

    Private Sub RemoveParagraphsFromCell(table As OpenXmlCompositeElement, a As Integer, b As Integer)

        If table.ChildElements(a).ChildElements(b).GetType().ToString() IsNot GetType(Wordprocessing.TableCell).ToString() Then Return

        Dim hasSubTable As Boolean = False
        Dim numberOfParagraphs As Integer = 0
        Dim hasRuns As Boolean = False
        numberOfParagraphs = GetNumberOfParagraphs(table, a, b, numberOfParagraphs, hasSubTable)

        If (numberOfParagraphs <= 1) Then Return

        If hasSubTable = True Then
            RemoveParagraphs(table, a, b, numberOfParagraphs)

            numberOfParagraphs = 0

            For y As Integer = table.ChildElements(a).ChildElements(b).ChildElements.Count - 1 To 0 Step -1
                Select Case (table.ChildElements(a).ChildElements(b).ChildElements(y).GetType().Name.ToString())
                    Case "Paragraph"
                        numberOfParagraphs += 1
                    Case "Table"
                        If numberOfParagraphs = 0 Then
                            table.ChildElements(a).ChildElements(b).AppendChild(New Wordprocessing.Paragraph)
                            Exit For
                        End If
                End Select
            Next
        Else
            RemoveParagraphs(table, a, b, numberOfParagraphs)
        End If

        If Not table.ChildElements(a).ChildElements(b).LastChild.GetType().ToString() Is GetType(Wordprocessing.Paragraph).ToString() Then
            table.ChildElements(a).ChildElements(b).AppendChild(New Wordprocessing.Paragraph)
        End If
    End Sub

    Private Sub RemoveParagraphs(table As OpenXmlCompositeElement, a As Integer, b As Integer, ByRef numberOfParagraphs As Integer)
        Dim hasRuns As Boolean

        For c As Integer = table.ChildElements(a).ChildElements(b).ChildElements.Count - 1 To 0 Step -1
            If table.ChildElements(a).ChildElements(b).ChildElements(c).GetType().ToString() Is GetType(Wordprocessing.Paragraph).ToString() Then
                hasRuns = False

                For d As Integer = table.ChildElements(a).ChildElements(b).ChildElements(c).ChildElements.Count - 1 To 0 Step -1
                    If Not table.ChildElements(a).ChildElements(b).ChildElements(c).ChildElements(d).GetType().ToString() Is GetType(Wordprocessing.ParagraphProperties).ToString() Then
                        hasRuns = True
                        Exit For
                    End If
                Next

                If hasRuns = False Then
                    If numberOfParagraphs > 1 Then
                        table.ChildElements(a).ChildElements(b).RemoveChild(table.ChildElements(a).ChildElements(b).ChildElements(c))
                        numberOfParagraphs -= 1
                    End If
                End If
            End If
        Next
    End Sub

    Private Function GetNumberOfParagraphs(table As OpenXmlCompositeElement, a As Integer, b As Integer, numberOfParagraphs As Integer, ByRef hasSubTable As Boolean) As Integer

        For x As Integer = table.ChildElements(a).ChildElements(b).ChildElements.Count - 1 To 0 Step -1
            Select Case (table.ChildElements(a).ChildElements(b).ChildElements(x).GetType().Name.ToString())
                Case "Paragraph"
                    numberOfParagraphs += 1
                Case "Table"
                    Dim childElement = table.ChildElements(a).ChildElements(b).ChildElements(x)
                    Dim asOpenXmlCompositeElement = DirectCast(childElement, OpenXmlCompositeElement)
                    RemoveParagraphsFromTable(asOpenXmlCompositeElement)
                    hasSubTable = True
            End Select
        Next
        Return numberOfParagraphs
    End Function

    Private Function CreateMhtFile(xhtmlString As String, isXhtml As Boolean) As MhtFile
        Dim mhtFile As New MhtFile()
        mhtFile.AppendMhtHeader()

        If isXhtml Then
            mhtFile.AppendMhtTextFile("<html><head><META http-equiv=""Content-Type"" content=""text/html; charset=ANSI""></head><body>" & xhtmlString & "</body></html>")
        Else
            mhtFile.AppendMhtTextFile(xhtmlString)
        End If

        Dim matchCollection As MatchCollection = GetResourcePathFromString(xhtmlString)
        For Each m As Match In matchCollection
            AddResourceToMhtFile(m.Value, mhtFile)
        Next

        mhtFile.AppendMhtBoundary(True)

        Return mhtFile
    End Function

    Private Function CreateAltChunckFormAlternativeFormatType(
        filename As String,
        ByRef mainPart As MainDocumentPart,
        shouldBeUnique As Boolean) As AltChunk

        Dim returnValue As AltChunk = Nothing
        Dim resourceName As String = IO.Path.GetFileName(filename)

        If shouldBeUnique AndAlso (Not shouldBeUnique OrElse (_listOfWordFragmentsAdded IsNot Nothing) AndAlso _listOfWordFragmentsAdded.Contains(String.Format("{0}|{1}", _currentBookletName, resourceName))) Then
            Return Nothing
        End If
        Dim eventArgs As New ResourceNeededEventArgs(resourceName, AddressOf StreamConverters.ConvertStreamToByteArray)
        ResourceNeeded(Me, eventArgs)
        Dim externalFileAsString As String = String.Empty

        If eventArgs.BinaryResource IsNot Nothing AndAlso eventArgs.BinaryResource.ResourceObject IsNot Nothing Then
            Dim fileType As SupportedSourceFiles = GetSupportedFileFromExtension(IO.Path.GetExtension(resourceName))
            Dim importType As AlternativeFormatImportPartType
            Dim encoding As Encoding = Encoding.UTF8

            Select Case fileType
                Case SupportedSourceFiles.rtf
                    importType = AlternativeFormatImportPartType.Rtf
                Case SupportedSourceFiles.html
                    Dim enc As New UTF8Encoding()

                    externalFileAsString = CreateMhtFile(enc.GetString(DirectCast(eventArgs.BinaryResource.ResourceObject, Byte())), False).FinalizeMht
                    importType = AlternativeFormatImportPartType.Mht
                    encoding = Encoding.ASCII
                Case SupportedSourceFiles.xhtml
                    Dim enc As New UTF8Encoding()

                    externalFileAsString = CreateMhtFile(enc.GetString(DirectCast(eventArgs.BinaryResource.ResourceObject, Byte())), False).FinalizeMht
                    importType = AlternativeFormatImportPartType.Mht
                    encoding = Encoding.ASCII
                Case SupportedSourceFiles.mht
                    importType = AlternativeFormatImportPartType.Mht
                    encoding = Encoding.ASCII
                Case SupportedSourceFiles.text
                    importType = AlternativeFormatImportPartType.TextPlain
                Case SupportedSourceFiles.docx
                    importType = AlternativeFormatImportPartType.WordprocessingML
                Case Else
                    Dim mimeType As String = FileHelper.GetMimeFromByteArray(filename, DirectCast(eventArgs.BinaryResource.ResourceObject, Byte()))

                    Select Case mimeType
                        Case "text/html"
                            Dim enc As New UTF8Encoding()
                            externalFileAsString = CreateMhtFile(enc.GetString(DirectCast(eventArgs.BinaryResource.ResourceObject, Byte())), False).FinalizeMht
                            importType = AlternativeFormatImportPartType.Mht
                            encoding = Encoding.ASCII
                        Case "text/plain"
                            importType = AlternativeFormatImportPartType.TextPlain
                        Case "application/x-zip-compressed"
                            importType = AlternativeFormatImportPartType.WordprocessingML
                        Case Else
                            Throw New Exception("Resource parameter type couldn't be resolved.")
                    End Select

            End Select

            Dim altChunk As New AltChunk()
            Dim altId As String = String.Empty

            If String.IsNullOrEmpty(externalFileAsString) Then
                altId = AddAlternativeFormatImportPartToDocument(DirectCast(eventArgs.BinaryResource.ResourceObject, Byte()),
                                                                 importType, mainPart, _altChunkIdCounter)
            Else
                altId = AddAlternativeFormatImportPartToDocument(externalFileAsString,
                                                                 importType, mainPart,
                                                                 encoding, _altChunkIdCounter)
            End If

            altChunk.Id = altId
            returnValue = altChunk

            If _listOfWordFragmentsAdded Is Nothing Then
                _listOfWordFragmentsAdded = New List(Of String)
            End If

            _listOfWordFragmentsAdded.Add(String.Format("{0}|{1}", _currentBookletName, resourceName))
        End If


        Return returnValue
    End Function

    Private Function GetSupportedFileFromExtension(extension As String) As SupportedSourceFiles
        Dim fileType As SupportedSourceFiles

        Select Case extension.ToLower
            Case ".txt", ".text"
                fileType = SupportedSourceFiles.text
            Case ".rt", ".rtf"
                fileType = SupportedSourceFiles.rtf
            Case ".docx"
                fileType = SupportedSourceFiles.docx
            Case ".xhtml", ".xhtm"
                fileType = SupportedSourceFiles.xhtml
            Case ".htm", ".html"
                fileType = SupportedSourceFiles.html
            Case ".mht", ".mhtml"
                fileType = SupportedSourceFiles.mht
            Case Else
                Throw New ApplicationException("source text filetype not supported or file extension not correct.")
        End Select
        Return fileType
    End Function

    Private Function AddImagesToDocument(ByRef customElement As CustomXmlElement, ByRef mainPart As MainDocumentPart) As Boolean
        Dim returnValue As Boolean = False
        Dim content As String = customElement.InnerXml
        Dim matchCollection As MatchCollection = GetResourcePathFromString(content)

        Select Case matchCollection.Count
            Case Is > 1
                Throw New ApplicationException("A customXmlElement can only contain one Image")
            Case Is = 0
                customElement.Remove()
            Case Else
                For Each m As Match In matchCollection
                    returnValue = True

                    Dim filename As String = m.Value
                    Dim imagePartType As ImagePartType = GetImagePartTypeFromFileName(filename)

                    Dim imagePart As ImagePart = mainPart.AddImagePart(imagePartType)
                    Dim resourceName As String = IO.Path.GetFileName(filename)
                    Dim eventArgs As New ResourceNeededEventArgs(resourceName, AddressOf StreamConverters.ConvertStreamToByteArray)
                    ResourceNeeded(Me, eventArgs)
                    Dim imageSize As SizeF

                    If eventArgs.BinaryResource IsNot Nothing AndAlso eventArgs.BinaryResource.ResourceObject IsNot Nothing Then
                        Using stream As Stream = New MemoryStream(DirectCast(eventArgs.BinaryResource.ResourceObject, Byte()))
                            Using image As Image = Image.FromStream(stream)
                                imageSize = image.PhysicalDimension
                            End Using
                            stream.Position = 0
                            imagePart.FeedData(stream)
                        End Using
                    End If

                    ReplaceCustomXmlWithImage(customElement, resourceName, mainPart.GetIdOfPart(imagePart), imageSize)
                Next
        End Select
        Return returnValue
    End Function

    Private Sub AddImageToDocument(ByRef image As Image, mainPart As MainDocumentPart, name As String, addBorder As Boolean, requestedSizeInDoc As System.Drawing.Size)
        If image IsNot Nothing Then
            Dim imagePart As ImagePart = mainPart.AddImagePart(ImagePartType.Jpeg)
            Dim stream As Stream = ImageToStream(image)
            imagePart.FeedData(stream)

            AddImage(requestedSizeInDoc.Width, requestedSizeInDoc.Height, mainPart.Document.Body, name, mainPart.GetIdOfPart(imagePart), addBorder)
        End If
    End Sub

    Private Sub AddImageAtLocation(ByRef image As Image, mainPart As MainDocumentPart, name As String, requestedSizeInDoc As System.Drawing.Size, currentNode As OpenXmlElement)
        Dim imagePart As ImagePart = mainPart.AddImagePart(ImagePartType.Png)
        Dim stream As Stream = ImageToStream(image)
        imagePart.FeedData(stream)

        Dim elementToAdd As OpenXmlElement = AddImagePartToNewElement(requestedSizeInDoc.Width, requestedSizeInDoc.Height, mainPart.GetIdOfPart(imagePart), name, False)

        If currentNode.Parent Is Nothing Then
            currentNode.AppendChild(New Wordprocessing.Run(elementToAdd))
        Else
            currentNode.Parent.InsertBefore(New Wordprocessing.Run(elementToAdd), currentNode)
        End If
    End Sub

    Public Sub AddImageToDocument(ByRef imagePath As String, mainPart As MainDocumentPart, name As String, addBorder As Boolean, requestedSizeInDoc As System.Drawing.Size)
        Using image As Image = Image.FromFile(imagePath)
            AddImageToDocument(image, mainPart, name, addBorder, requestedSizeInDoc)
        End Using
    End Sub

    Private Function ImageToStream(image As Image) As Stream
        Dim stream = New MemoryStream()
        image.Save(stream, image.RawFormat)
        stream.Position = 0
        Return stream
    End Function

    Private Function AddWordContentToDocument(ByRef customElement As CustomXmlElement, shouldBeUnique As Boolean) As Boolean
        Dim isFilled As Boolean = False
        Dim idAttribute = customElement.ExtendedAttributes.FirstOrDefault(Function(a) a.LocalName = "id")
        Dim resourceName As String = If(idAttribute.LocalName = "id", idAttribute.Value, Convert.ToBase64String(SerializeHelper.GetMD5Hash(customElement.InnerText)))

        If Not shouldBeUnique OrElse (_listOfWordFragmentsAdded Is Nothing OrElse Not _listOfWordFragmentsAdded.Contains(String.Format("{0}|{1}", _currentBookletName, resourceName))) Then
            isFilled = customElement IsNot Nothing AndAlso customElement.HasChildren

            If isFilled Then

                Dim content As OpenXmlElement
                If customElement.ChildElements.Count = 1 Then
                    content = customElement.FirstChild.CloneNode(True)
                Else
                    Dim r As New Wordprocessing.Run(customElement.ChildElements)
                    content = New Wordprocessing.Paragraph(r)
                End If

                customElement.Parent.ReplaceChild(content, customElement)

                If _listOfWordFragmentsAdded Is Nothing Then
                    _listOfWordFragmentsAdded = New List(Of String)
                End If

                _listOfWordFragmentsAdded.Add(String.Format("{0}|{1}", _currentBookletName, resourceName))
            End If
        End If

        If Not isFilled Then
            customElement.Remove()
        End If

        Return isFilled
    End Function

    Private Function AddWordFragmentToDocument(ByRef customElement As CustomXmlElement, ByRef mainPart As MainDocumentPart, ByRef shouldBeUnique As Boolean) As Boolean
        Dim content As String = customElement.InnerXml.Trim()
        Dim isFilled As Boolean = False

        If Not String.IsNullOrEmpty(content) Then
            Dim altChunk As AltChunk = CreateAltChunckFormAlternativeFormatType(content, mainPart, shouldBeUnique)

            If altChunk IsNot Nothing Then
                isFilled = True
                customElement.Parent.ReplaceChild(altChunk, customElement)
            End If
        End If

        If Not isFilled Then
            customElement.Remove()
        End If

        Return isFilled
    End Function

    Private Function AddImagePartToNewElement(relationshipId As String, imageSize As SizeF, resourceName As String, addBorder As Boolean) As OpenXmlElement
        Return AddImagePartToNewElement(imageSize.Width, imageSize.Height, relationshipId, resourceName, addBorder)
    End Function

    Private Function AddImagePartToNewElement(width As Single, height As Single, relationshipId As String, resourceName As String, addBorder As Boolean) As OpenXmlElement
        Dim widthInEmUs As Integer = CInt(width * 9525)
        Dim heightInEMUs As Integer = CInt(height * 9525)
        Dim outline As Outline
        Dim effectExtent As EffectExtent

        If addBorder Then
            effectExtent = New EffectExtent() With {.BottomEdge = 28575, .LeftEdge = 19050, .RightEdge = 28575, .TopEdge = 19050}
            outline = New Outline(
                New SolidFill(
                    New SchemeColor With {.Val = SchemeColorValues.Text1}
                    )
                )

        Else
            effectExtent = New EffectExtent() With {.BottomEdge = 0, .LeftEdge = 0, .RightEdge = 0, .TopEdge = 0}
            outline = New Outline(
                New SolidFill()
                )
        End If

        Dim element = New Drawing(
            New Inline(
                New Extent() With {.Cx = widthInEmUs, .Cy = heightInEMUs},
                effectExtent,
                New DocProperties() With {.Id = CType(1UI, UInt32Value), .Name = resourceName},
                New Wordprocessing.NonVisualGraphicFrameDrawingProperties(
                    New GraphicFrameLocks() With {.NoChangeAspect = True}
                    ),
                New Graphic(New GraphicData(
                    New Picture(
                        New NonVisualPictureProperties(
                            New NonVisualDrawingProperties() With {.Id = 0UI, .Name = resourceName},
                            New NonVisualPictureDrawingProperties()
                            ),
                        New BlipFill(
                            New Blip(
                                New BlipExtensionList(
                                    New BlipExtension() With {.Uri = Guid.NewGuid.ToString})
                                ) With {.Embed = relationshipId, .CompressionState = BlipCompressionValues.None},
                            New Stretch(
                                New FillRectangle()
                                )
                            ),
                        New ShapeProperties(
                            New Transform2D(
                                New Offset() With {.X = 0L, .Y = 0L},
                                New Extents() With {.Cx = widthInEmUs, .Cy = heightInEMUs}),
                            New PresetGeometry(
                                New AdjustValueList()
                                ) With {.Preset = ShapeTypeValues.Rectangle},
                            outline
                            )
                        )
                    ) With {.Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"}
                            )
                )
            )

        Return element
    End Function

    Private Sub ReplaceCustomXmlWithImage(ByRef customElement As CustomXmlElement, resourceName As String, relationshipId As String, imageSize As SizeF)
        Dim element As OpenXmlElement = AddImagePartToNewElement(relationshipId, imageSize, resourceName, False)

        customElement.Parent.ReplaceChild(New Wordprocessing.Run(element), customElement)
    End Sub


    Private Sub AddImage(width As Single, height As Single, ByRef element As Body, resourceName As String, relationshipId As String, addBorder As Boolean)
        Dim elementToAdd As OpenXmlElement = AddImagePartToNewElement(width, height, relationshipId, resourceName, addBorder)

        element.AppendChild(New Wordprocessing.Paragraph(New Wordprocessing.Run(elementToAdd)))
    End Sub

    Private Function GetImagePartTypeFromFileName(filename As String) As ImagePartType
        Dim extension As String = IO.Path.GetExtension(filename)

        Select Case extension.ToLower
            Case ".bmp"
                Return ImagePartType.Bmp
            Case ".emf"
                Return ImagePartType.Emf
            Case ".gif"
                Return ImagePartType.Gif
            Case ".icon"
                Return ImagePartType.Icon
            Case ".jpeg", ".jpg"
                Return ImagePartType.Jpeg
            Case ".png"
                Return ImagePartType.Png
            Case ".wmf"
                Return ImagePartType.Wmf
            Case Else
                Throw New ApplicationException(String.Format("File extension:'{0}' not supported", extension))
        End Select
    End Function

    Private Function SetPreserveWhiteSpaces(ByRef xhtmlString As String) As String
        Dim doc As New XmlDocument()
        doc.LoadXml(xhtmlString)

        doc = SetPreserveWhiteSpaceToTag(doc, "table", "default")
        doc = SetPreserveWhiteSpaceToTag(doc, "p", "preserve")
        Return doc.OuterXml
    End Function

    Private Function SetPreserveWhiteSpaceToTag(ByRef doc As XmlDocument, tagName As String, spaceValue As String) As XmlDocument
        For Each node As XmlNode In doc.SelectNodes(String.Format("//*[local-name() = '{0}']", tagName))
            If node.Attributes("xml:space") Is Nothing Then
                Dim attribute As XmlAttribute = doc.CreateAttribute("xml", "space", "http://www.w3.org/1999/xhtml")
                attribute.Value = spaceValue

                node.Attributes.Append(attribute)
            End If
        Next
        Return doc
    End Function

    Private Sub UpdateStyles(ByRef xhtmlString As String)
        If _styleSheetsToReference Is Nothing Then
            Return
        End If

        Dim classRegex As New Regex(_regexClass, RegexOptions.IgnoreCase)
        For Each match As Match In classRegex.Matches(xhtmlString.Trim)
            If match.Value = "class=""UserSROpmerkingNietInAfname""" Then
                xhtmlString = RemoveRemarks(xhtmlString)
            Else
                ReplaceClassWithStyle(xhtmlString, match.Value)
            End If
        Next
    End Sub

    Private Sub ReplaceClassWithStyle(ByRef xhtmlString As String, className As String)
        Dim presentStyle = GetPresentStyle(xhtmlString)
        For Each stylesheet As String In _styleSheetsToReference.Values
            Dim newStyle = GetStyle(stylesheet, className, presentStyle)
            If String.IsNullOrEmpty(newStyle) OrElse newStyle.Equals(presentStyle, StringComparison.InvariantCultureIgnoreCase) Then
                Continue For
            End If
            If String.IsNullOrEmpty(presentStyle) Then
                xhtmlString = xhtmlString.Replace(className, newStyle)
            Else
                xhtmlString = xhtmlString.Replace(className, "")
                xhtmlString = xhtmlString.Replace(presentStyle, newStyle)
            End If
            Exit For
        Next
    End Sub

    Private Function GetPresentStyle(xhtmlString As String) As String
        Dim styleRegEx As New Regex("style=""([^""]*)""", RegexOptions.IgnoreCase)
        Dim matches = styleRegEx.Matches(xhtmlString.Trim)
        If matches.Count > 0 Then
            Return matches.Item(0).Value
        End If
        Return String.Empty
    End Function

    Public Function RemoveRemarks(xhtmlString As String) As String

        Dim doc = New XHtmlDocument()
        doc.LoadXml(xhtmlString)
        Const QUERY As String = "//*[contains(@class, 'UserSROpmerkingNietInAfname')]"
        Dim nodesToRemove = doc.SelectNodes(QUERY)

        For Each nodeToRemove As XmlNode In nodesToRemove
            nodeToRemove.ParentNode.RemoveChild(nodeToRemove)
        Next

        Return doc.DocumentElement.OuterXml
    End Function

    Public Sub RemoveTtsStyles(ByRef xhtmlString As String)

        If String.IsNullOrEmpty(xhtmlString) Then
            Return
        End If

        Dim doc = XDocument.Parse(xhtmlString, LoadOptions.PreserveWhitespace)
        Dim namesp As XNamespace = "http://www.w3.org/1999/xhtml"

        Dim nodesToRemove = doc.Descendants().Where(Function(se) se.Attributes IsNot Nothing AndAlso se.Attributes.Any(Function(a) a.Name = "class" AndAlso Not String.IsNullOrEmpty(a.Value) AndAlso (a.Value.IndexOf("ttsPauze", StringComparison.InvariantCultureIgnoreCase) > -1 OrElse a.Value.IndexOf("ttsFonetisch", StringComparison.InvariantCultureIgnoreCase) > -1))).ToList()
        For Each node As XElement In nodesToRemove
            If node.FirstNode IsNot Nothing Then
                If node.ToString().Contains("<br ") Then
                    node.AddAfterSelf(node.Nodes)
                End If
            End If
            node.Remove()
        Next

        Dim nodesToReplace = doc.Descendants(namesp + "span").Where(Function(se) se.Attributes IsNot Nothing _
                                                                         AndAlso Not se.Attributes.Any(Function(sa) sa.Name = "style" AndAlso Not String.IsNullOrEmpty(sa.Value)) _
                                                                         AndAlso se.Attributes.Any(Function(a) a.Name = "class" _
                                                                                                               AndAlso Not String.IsNullOrEmpty(a.Value) AndAlso a.Value.IndexOf("ttsTonen", StringComparison.InvariantCultureIgnoreCase) > -1)).ToList
        For Each node As XElement In nodesToReplace
            If String.IsNullOrEmpty(StripTtsTonenClassName(node.Attribute("class").Value)) Then
                node.AddAfterSelf(node.Nodes)
                node.Remove()
            End If
        Next

        Dim nodesToUpdate = doc.Descendants(namesp + "span").Where(Function(se) se.Attributes IsNot Nothing _
                                                                        AndAlso se.Attributes.Any(Function(sa) sa.Name = "style" AndAlso Not String.IsNullOrEmpty(sa.Value)) _
                                                                        AndAlso se.Attributes.Any(Function(a) a.Name = "class" _
                                                                                                              AndAlso Not String.IsNullOrEmpty(a.Value) AndAlso a.Value.IndexOf("ttsTonen", StringComparison.InvariantCultureIgnoreCase) > -1)).ToList

        For Each node As XElement In nodesToUpdate
            Dim classes As String = StripTtsTonenClassName(node.Attribute("class").Value)
            If String.IsNullOrEmpty(classes.Trim) Then
                node.Attribute("class").Remove()
            Else
                node.Attribute("class").Value = classes.Trim
            End If
        Next

        Dim nodesToUpdate_NoSpan = doc.Descendants().Where(Function(se) Not se.Name.ToString().Equals("span", StringComparison.InvariantCultureIgnoreCase) _
                                                                AndAlso se.Attributes IsNot Nothing _
                                                                AndAlso se.Attributes.Any(Function(a) a.Name = "class" _
                                                                                                      AndAlso Not String.IsNullOrEmpty(a.Value) AndAlso a.Value.IndexOf("ttsTonen", StringComparison.InvariantCultureIgnoreCase) > -1)).ToList

        For Each node As XElement In nodesToUpdate_NoSpan
            Dim classes As String = StripTtsTonenClassName(node.Attribute("class").Value)
            If String.IsNullOrEmpty(classes.Trim) Then
                node.Attribute("class").Remove()
            Else
                node.Attribute("class").Value = classes.Trim
            End If
        Next

        xhtmlString = doc.Nodes().Aggregate("", Function(c, n) c + n.ToString())
    End Sub

    Private Function StripTtsTonenClassName(classes As String) As String
        Dim result As String = String.Empty
        For Each cls As String In classes.Split(" "c)
            If Not cls.IndexOf("ttsTonen", StringComparison.InvariantCultureIgnoreCase) > -1 Then
                result = String.Concat(result, String.Format(" {0}", cls))
            End If
        Next
        Return result.Trim
    End Function

    Private Sub UpdateMathML(ByRef xhtmlString As String)
        Dim xDoc As XDocument
        Using strReader As New StringReader("<root>" & xhtmlString & "</root>")
            xDoc = XDocument.Load(strReader, LoadOptions.PreserveWhitespace)
        End Using

        Dim mathMlTagsToReplace As New List(Of XElement)
        xDoc.Descendants().Where(Function(d) d.Name.LocalName.Equals("math", StringComparison.InvariantCultureIgnoreCase)).ToList().ForEach(Sub(x)
                                                                                                                                                Dim mathImgTag As XElement =
                                              <img src=<%= String.Format("data:image/png;base64,{0}", Convert.ToBase64String(MathMlToImage(x.OuterXml()))) %> id=<%= Guid.NewGuid().ToString() %> alt=""/>
                                                                                                                                                x.AddAfterSelf(mathImgTag)
                                                                                                                                                mathMlTagsToReplace.Add(x)
                                                                                                                                            End Sub)

        mathMlTagsToReplace.ForEach(Sub(m)
                                        m.Remove()
                                    End Sub)

        xhtmlString = xDoc.Descendants("root").First().InnerXml()
    End Sub

    Private Function GetStyle(cssFile As String, className As String, presentStyle As String) As String
        Dim result = GetStylesForClass(cssFile, className)
        If String.IsNullOrEmpty(result) Then
            Return presentStyle
        End If

        result = FormatStylesForClass(result)
        If String.IsNullOrEmpty(result) Then
            Return presentStyle
        End If

        If String.IsNullOrEmpty(presentStyle) Then
            Return $"style=""{result}"""
        Else
            Return $"{AppendColon(presentStyle)} {result}"""
        End If
    End Function

    Private Function GetStylesForClass(cssFile As String, className As String) As String
        Dim styles As New StringBuilder()

        className = Replace(className, "class=", "")
        className = Replace(className, """", "")

        Dim matches As MatchCollection = Regex.Matches(cssFile, String.Format(".{0}[^{{]+\{{[^}}]+?}}", className))
        For Each match As Match In matches
            styles.Append(Regex.Replace(Regex.Replace(match.Value, "/\*.+?\*/", String.Empty, RegexOptions.Singleline), "\s", String.Empty))
        Next

        Return styles.ToString()
    End Function

    Private Function FormatStylesForClass(styles As String) As String
        Dim startIndex = InStr(styles, "{")
        Dim endIndex = DirectCast(IIf(startIndex > 0, InStr(startIndex, styles, "}"), 0), Integer)
        If startIndex = 0 OrElse endIndex = 0 Then
            Return String.Empty
        Else
            Return Mid(styles, (startIndex + 1), (endIndex - startIndex) - 1)
        End If
    End Function

    Private Function AppendColon(presentStyle As String) As String
        Dim result = presentStyle.Remove(presentStyle.LastIndexOf(""""))
        Return result + ";"
    End Function

    Private Function GetReferencedStylesheets(item As AssessmentItem) As Dictionary(Of String, String)
        Dim styleSheetsToReference As New Dictionary(Of String, String)
        If item Is Nothing Then
            Return styleSheetsToReference
        End If

        If _cachedStyleSheetsToReference IsNot Nothing AndAlso _cachedStyleSheetsToReference.ContainsKey(item.LayoutTemplateSourceName) Then
            Return _cachedStyleSheetsToReference(item.LayoutTemplateSourceName)
        End If
        For Each dependentResourceReference As DependentResource In _resourceManager.GetDependentResourcesForResource(item.LayoutTemplateSourceName)
            If GetMetaData(dependentResourceReference.Name, "MediaType") = "text/css" OrElse DependentResourceIsStylesheet(dependentResourceReference.Name) Then
                Dim binResource As BinaryResource
                Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
                binResource = _resourceManager.GetResource(dependentResourceReference.Name, New ResourceProcessingFunction(AddressOf StreamConverters.ConvertStreamToString), request)
                styleSheetsToReference.Add(dependentResourceReference.Name, DirectCast(binResource.ResourceObject, String))
            End If
        Next
        If _cachedStyleSheetsToReference Is Nothing Then
            _cachedStyleSheetsToReference = New Dictionary(Of String, Dictionary(Of String, String))
        End If
        If Not _cachedStyleSheetsToReference.ContainsKey(item.LayoutTemplateSourceName) Then
            _cachedStyleSheetsToReference.Add(item.LayoutTemplateSourceName, styleSheetsToReference)
        End If
        Return styleSheetsToReference
    End Function

    Private Function DependentResourceIsStylesheet(dependentResourceName As String) As Boolean
        Dim eventArgs As New ResourceNeededEventArgs(dependentResourceName, AddressOf StreamConverters.ConvertStreamToByteArray)
        ResourceNeeded(Me, eventArgs)
        Dim mimeType As String = FileHelper.GetMimeFromByteArray(dependentResourceName, DirectCast(eventArgs.BinaryResource.ResourceObject, Byte()))
        If Not String.IsNullOrEmpty(mimeType) AndAlso mimeType.Equals("text/css", StringComparison.InvariantCultureIgnoreCase) Then
            Return True
        End If
        Return False
    End Function



    Private Sub FillVariables(ByRef wordDoc As WordprocessingDocument)

        Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
        If mainPart.CustomXmlParts IsNot Nothing AndAlso mainPart.CustomXmlParts.Count > 0 Then
            Dim customXmlPart As CustomXmlPart = mainPart.CustomXmlParts(0)
            Using ts As New StreamWriter(customXmlPart.GetStream())
                ts.Write(String.Empty)
                ts.Flush()
            End Using
        End If
    End Sub


    Private Function LoadAssessmentTest() As AssessmentTest2
        Dim assessmentTest As AssessmentTest2 = Nothing
        Dim testCollection As ResourceEntryCollection = _resourceManager.GetResourcesOfType("AssessmentTestResourceEntity")
        If testCollection.Count = 1 Then
            Dim request = New ResourceRequestDTO With {.WithDependencies = True, .WithCustomProperties = True}
            Dim testResource As BinaryResource = _resourceManager.GetResource(testCollection(0).Name, AddressOf StreamConverters.ConvertStreamToString, request)
            assessmentTest = DirectCast(SerializeHelper.XmlDeserializeFromString(DirectCast(testResource.ResourceObject, String), GetType(AssessmentTest2)), AssessmentTest2)
            _versionNumberTest = GetVersionNumber(testCollection(0).Version)
        Else
            Throw New TesterException("Cannot find test resource.")
        End If

        Return assessmentTest
    End Function



    Private Function SetupItem(item As AssessmentItem) As String
        Dim itemLayoutTemplateEventArgs As New ResourceNeededEventArgs(item.LayoutTemplateSourceName,
                                                               New ResourceProcessingFunction(AddressOf StreamConverters.ConvertStreamToString))

        ResourceNeeded(Me, itemLayoutTemplateEventArgs)
        itemLayoutTemplateEventArgs.GetResource(Of String)()
        Dim parser As New ItemLayoutAdapter(item.LayoutTemplateSourceName, item.Parameters, AddressOf ResourceNeeded)
        _contextIdentifier = 1
        Try
            Dim parsedDocument As XHtmlDocument = parser.ParseTemplate(PaperBasedTestPlugin.PLUGIN_NAME, False, _contextIdentifier)
            Return parsedDocument.OuterXml
        Catch e As ControlTemplateException
            Return String.Empty
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Function GetItemByCode(itemCode As String) As AssessmentItem
        Return DirectCast(SerializeHelper.XmlDeserializeFromStream(_resourceManager.GetResource(itemCode).ResourceObject, GetType(AssessmentItem)), AssessmentItem)
    End Function

    Private Sub ProcessItem(PBTItemReference As WordItemReference)
        _maxScoreTest += PBTItemReference.MaxScore

        Dim item As AssessmentItem = GetItemByCode(PBTItemReference.SourceName)
        Dim parseTemplateString As String = SetupItem(item)
        Dim newBody As New Body(parseTemplateString)

        TestSessionContext.CurrentItem = item
        TestSessionContext.CurrentItemIndex = _itemCounter

        _styleSheetsToReference = GetReferencedStylesheets(item)

        _itemCounter += 1
        If PBTItemReference.ItemFunctionalType = ItemFunctionalType.Regular Then
            _regularItemCounter += 1
        End If
        _showItemCounter = (PBTItemReference.ItemFunctionalType = ItemFunctionalType.Regular)

        If _bookletCollection Is Nothing Then
            Return
        End If

        For Each bookletName As String In _bookletCollection.Keys
            _currentBookletName = bookletName
            Dim wordDocument As WordprocessingDocument = _bookletCollection.Item(bookletName)
            InitHtmlConverter(wordDocument.MainDocumentPart)

            For Each contentControlBlock As ContentControlBlock In [Enum].GetValues(GetType(ContentControlBlock))
                Dim contentBlockKey As String = String.Format("{0}|{1}", contentControlBlock.ToString, bookletName)
                Dim contentBlock As OpenXmlCompositeElement = Nothing
                If _contentBlockDictionary IsNot Nothing _
                   AndAlso _contentBlockDictionary.ContainsKey(contentBlockKey) Then
                    contentBlock = _contentBlockDictionary.Item(contentBlockKey)
                Else
                    contentBlock = FindCustomBlock(contentControlBlock.ToString, wordDocument.MainDocumentPart.Document.Body)
                    If contentBlock IsNot Nothing Then
                        If _contentBlockDictionary Is Nothing Then
                            _contentBlockDictionary = New Dictionary(Of String, OpenXmlCompositeElement)
                        End If
                        _contentBlockDictionary.Add(contentBlockKey, contentBlock)
                    End If
                End If
                If contentBlock IsNot Nothing Then
                    AddDataToCustomBlock(wordDocument, DirectCast(contentBlock, SdtElement), item, newBody, Nothing)
                End If
            Next
        Next
    End Sub

    Friend Sub InitHtmlConverter(mainPart As MainDocumentPart)
        If _htmlConverter IsNot Nothing Then
            RemoveHandler _htmlConverter.HtmlStyles.StyleMissing, AddressOf StyleHelper.HtmlStyleMissing
        End If

        If (mainPart.StyleDefinitionsPart Is Nothing) Then
            mainPart.AddNewPart(Of StyleDefinitionsPart)()
            mainPart.StyleDefinitionsPart.Styles = New Styles()
        End If

        _htmlConverter = New HtmlConverter(mainPart)
        StyleHelper = New WordStyleHelper()

        AddHandler _htmlConverter.HtmlStyles.StyleMissing, AddressOf StyleHelper.HtmlStyleMissing
    End Sub

    Private Sub StyleHelperStylesChanged() Handles StyleHelper.StylesChanged
        _htmlConverter?.RefreshStyles()
    End Sub

    Private Sub StartPickingItems()
        _itemSelector = New WordItemSelectorManager(_testContext)
        AddHandler _itemSelector.ResourceNeeded, AddressOf ResourceNeeded
        AddHandler TestSessionContext.ResourceNeeded, AddressOf ResourceNeeded

        For Each testPart As TestPartViewBase In _testContext.TestParts
            _totalNumberOfItemsInTest += _itemSelector.GetItemCountOfTestPart(testPart)
        Next

        Dim endOfTest As Boolean = False
        Dim itemIndex As Integer = 0
        While Not endOfTest
            TestSessionContext.CurrentItem = Nothing
            Dim wordItemReference As WordItemReference = _itemSelector.PickNewItem(_itemCounter)
            Dim e As New ProgressEventArgs(_totalNumberOfItemsInTest)
            e.Message = String.Format(My.Resources.ItemIsAdded, wordItemReference.SourceName)
            e.Value = itemIndex + 1
            RaiseEvent HandlerProgress(Me, e)
            ProcessItem(wordItemReference)
            endOfTest = _itemSelector.IsLastItemInTest
            itemIndex += 1
        End While
        If _bookletCollection IsNot Nothing Then
            For Each wordDocumentName As String In _bookletCollection.Keys
                Dim wordDocument As WordprocessingDocument = _bookletCollection.Item(wordDocumentName)
                InitHtmlConverter(wordDocument.MainDocumentPart)

                Dim aspectContentBlock As OpenXmlCompositeElement = FindCustomBlock("AspectBlock", wordDocument.MainDocumentPart.Document.Body)
                If aspectContentBlock IsNot Nothing Then
                    If _aspectDictionary IsNot Nothing AndAlso Not _aspectDictionary.Count = 0 Then
                        For Each aspect As Aspect In _aspectDictionary.Values
                            AddDataToCustomBlock(wordDocument, DirectCast(aspectContentBlock, SdtBlock), Nothing, Nothing, aspect)
                        Next
                    End If
                    aspectContentBlock.Remove()
                End If
                For Each contentControlBlock As ContentControlBlock In [Enum].GetValues(GetType(ContentControlBlock))
                    Dim contentBlockKey As String = String.Format("{0}|{1}", contentControlBlock.ToString, wordDocumentName)
                    If _contentBlockDictionary IsNot Nothing AndAlso _contentBlockDictionary.ContainsKey(contentBlockKey) Then
                        _contentBlockDictionary.Item(contentBlockKey).Parent.RemoveChild(_contentBlockDictionary.Item(contentBlockKey))
                    End If
                Next
                For Each testproperty As TestProperties In [Enum].GetValues(GetType(TestProperties))
                    Dim testpropertyName As String = testproperty.ToString
                    Dim testpropertyList As List(Of SdtElement) = FindMultipleCustomBlocks(testpropertyName, wordDocument)
                    LoopThroughContentControls(testpropertyList, Nothing, Nothing, False, wordDocument, Nothing, Nothing)
                Next
                For Each docfield As DocFields In [Enum].GetValues(GetType(DocFields))
                    Dim docfieldName As String = docfield.ToString
                    Dim docfieldList As List(Of SdtElement) = FindMultipleCustomBlocks(docfieldName, wordDocument)
                    LoopThroughContentControls(docfieldList, Nothing, Nothing, False, wordDocument, Nothing, Nothing)
                Next

                Dim testcustompropertyList As List(Of SdtElement) = FindMultipleCustomBlocksByType(TestPrefix, wordDocument)
                LoopThroughContentControls(testcustompropertyList, Nothing, Nothing, False, wordDocument, Nothing, Nothing)

                AddParagraphToEmptyTableCell(wordDocument.MainDocumentPart.Document.Body)
                SaveAndDisposeWordObjects(wordDocument)
            Next
        End If
        Select Case _resourceManager.GetType.ToString
            Case GetType(ManifestResourceManager).ToString
                DirectCast(_resourceManager, ManifestResourceManager).Dispose()
        End Select

        RemoveHandler TestSessionContext.ResourceNeeded, AddressOf ResourceNeeded

        RaiseEvent WordDocGenerated(Me, Nothing)
    End Sub

    Private Function AddAlternativeFormatImportPartToDocument(data As String,
                                                              alternativeFormatImportPartType As AlternativeFormatImportPartType,
                                                              ByRef mainPart As MainDocumentPart,
                                                              encoding As Encoding, ByRef altChunkIdCounter As Integer) As String
        Dim altChunkId As String = [String].Format("AltChunkId{0}", altChunkIdCounter)
        Dim chunk As AlternativeFormatImportPart = mainPart.AddAlternativeFormatImportPart(alternativeFormatImportPartType, altChunkId)
        Using chunkStream As Stream = chunk.GetStream(FileMode.Create, FileAccess.Write)
            Using stringWriter As New StreamWriter(chunkStream, encoding)
                stringWriter.Write(data)
            End Using
        End Using
        altChunkIdCounter += 1
        Return altChunkId
    End Function

    Private Function AddAlternativeFormatImportPartToDocument(
            data As Byte(),
            alternativeFormatImportPartType As AlternativeFormatImportPartType,
            ByRef mainPart As MainDocumentPart,
            ByRef altChunkIdCounter As Integer) As String
        Dim altChunkId As String = [String].Format("AltChunkId{0}", altChunkIdCounter)
        Dim chunk As AlternativeFormatImportPart = mainPart.AddAlternativeFormatImportPart(alternativeFormatImportPartType, altChunkId)
        Using chunkStream As Stream = chunk.GetStream(FileMode.Create, FileAccess.Write)
            Using stream As MemoryStream = New MemoryStream(data)
                stream.WriteTo(chunkStream)
            End Using
        End Using
        altChunkIdCounter += 1
        Return altChunkId
    End Function

    Private Sub ConvertDivToParagraph(ByRef xhtmlString As String)
        Dim rx As New Regex(String.Format(_regexBetweenTags, "div"), RegexOptions.Singleline)
        Dim matchCollection As MatchCollection = rx.Matches(xhtmlString)
        For Each m As Match In matchCollection
            Dim sb As New StringBuilder(String.Empty)
            If m.Groups.Count > 0 Then
                sb.Append("<p>")
                sb.Append(m.Groups(m.Groups.Count - 1).ToString().Trim)
                sb.Append("</p>")
            End If
            xhtmlString = xhtmlString.Replace(m.Value, sb.ToString)
        Next
    End Sub



    Public Sub LoadAndProcessTest(resourceManager As ResourceManagerBase, testName As String, printFormList As Dictionary(Of PrintForm, String))
        _resourceManager = resourceManager
        Dim testResource As StreamResource = _resourceManager.GetResource(testName)
        _versionNumberTest = GetVersionNumber(testResource.Version)
        Dim assessmentTestModel As AssessmentTest2 = DirectCast(SerializeHelper.XmlDeserializeFromStream(testResource.ResourceObject, GetType(AssessmentTest2)), AssessmentTest2)
        LoadAndProcessPackage(assessmentTestModel, printFormList)
    End Sub


    Public Function SetupPBTItemForPreview(resourceManager As ResourceManagerBase, item As AssessmentItem) As String
        _resourceManager = resourceManager
        _tempFolder = IO.Path.Combine(TempStorageHelper.GetTempStoragePath, TempStorageHelper.GetValidFolderNameFromString(item.Identifier))

        Dim directoryInfo As New DirectoryInfo(_tempFolder)

        If Not directoryInfo.Exists Then
            directoryInfo.Create()
        End If

        Dim tempDocumentLocation As String = IO.Path.Combine(_tempFolder, String.Format("{0}.{1}", IO.Path.GetRandomFileName, "docx"))
        Dim tempWord As WordprocessingDocument = Nothing

        CreateDocument(tempDocumentLocation, tempWord)
        InitHtmlConverter(tempWord.MainDocumentPart)

        Dim newBody As Body = GetBodyFromItem(item, tempWord, True)

        For Each openXmlElement As OpenXmlElement In newBody.ChildElements
            AddElementToDocument(tempWord.MainDocumentPart, openXmlElement.CloneNode(True))
        Next

        SaveAndDisposeWordObjects(tempWord)

        Return (tempDocumentLocation)
    End Function

    Public Sub AddItemToWordDoc(ByRef wordDoc As WordprocessingDocument, resourceManager As ResourceManagerBase, item As AssessmentItem)
        _resourceManager = resourceManager
        InitHtmlConverter(wordDoc.MainDocumentPart)
        Dim newBody As Body = GetBodyFromItem(item, wordDoc, False)

        For Each openXmlElement As OpenXmlElement In newBody.ChildElements
            AddElementToDocument(wordDoc.MainDocumentPart, openXmlElement.CloneNode(True))
        Next
    End Sub

    Public Sub AddInlineChoiceAlternativesToWordDoc(ByRef wordDoc As WordprocessingDocument, resourceManager As ResourceManagerBase, item As AssessmentItem)
        Dim headerAdded As Boolean = False
        _resourceManager = resourceManager
        InitHtmlConverter(wordDoc.MainDocumentPart)
        For Each scoringPrm As ScoringParameter In item.Parameters.DeepFetchInlineScoringParameters().Where(Function(sp) TypeOf sp Is InlineChoiceScoringParameter)

            If Not headerAdded Then
                Me.AddHeaderTwo(wordDoc, My.Resources.ChoiceAlternatives)
                headerAdded = True
            End If

            Dim icPrm As InlineChoiceScoringParameter = DirectCast(scoringPrm, InlineChoiceScoringParameter)

            Me.AddHeaderThree(wordDoc, icPrm.Label)

            Dim icTable As Wordprocessing.Table = Me.AddTable(wordDoc, GetDefaultTableWidth(_pageOrientationLandScape), "A6A6A6")

            For Each alternative As ParameterCollection In icPrm.Value
                If alternative.InnerParameters.Any(Function(p) TypeOf p Is XHtmlParameter) Then
                    AddInlineChoiceAlternativeRow(wordDoc, icTable, alternative.InnerParameters.First(Function(p) TypeOf p Is XHtmlParameter), alternative.Id)
                ElseIf alternative.InnerParameters.Any(Function(p) TypeOf p Is PlainTextParameter) Then
                    AddInlineChoiceAlternativeRow(wordDoc, icTable, alternative.InnerParameters.First(Function(p) TypeOf p Is PlainTextParameter), alternative.Id)
                End If
            Next
        Next
    End Sub

    Public Sub AddInlineChoiceAlternativeRow(ByRef wordDoc As WordprocessingDocument, ByRef table As Wordprocessing.Table, prm As ParameterBase, alternativeId As String)
        Dim tr As New Wordprocessing.TableRow()
        Dim paragraph As Wordprocessing.Paragraph = CreateParagraphWithoutSpacing()

        Dim run As New Wordprocessing.Run()
        run.Append(New Wordprocessing.Text(alternativeId))
        paragraph.Append(run)

        Dim tc1 As New Wordprocessing.TableCell(paragraph)
        Dim tcProp As New Wordprocessing.TableCellProperties()
        Dim tcWidth As New TableCellWidth()
        tcWidth.Width = "567"
        tcProp.Append(tcWidth)
        tc1.Append(tcProp)
        tr.Append(tc1)

        Dim tc2 As New Wordprocessing.TableCell()
        Dim xHtmlString As String = String.Empty
        If TypeOf prm Is XHtmlParameter Then
            xHtmlString = DirectCast(prm, XHtmlParameter).Value
        ElseIf TypeOf prm Is PlainTextParameter Then
            xHtmlString = DirectCast(prm, PlainTextParameter).Value
        End If
        If Not String.IsNullOrEmpty(xHtmlString) Then
            Dim fakeBody As New Body(String.Format("<w:body xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main""><w:tbl><w:tr><w:tc><w:p><w:customXml w:element=""xhtmlElementToOpenXml"">{0}</w:customXml></w:p></w:tc></w:tr></w:tbl></w:body>", xHtmlString))

            ResolveCustomXmlElement(CType(fakeBody, OpenXmlElement), wordDoc.MainDocumentPart, CustomXmlElementType.xhtmlElementToOpenXml, False, True)

            Dim firstTableCell As Wordprocessing.TableCell = fakeBody.Descendants(Of Wordprocessing.TableCell).FirstOrDefault

            If firstTableCell.ChildElements IsNot Nothing AndAlso firstTableCell.ChildElements.Count > 0 Then
                For Each o As OpenXmlElement In firstTableCell.ChildElements
                    tc2.Append(o.CloneNode(True))
                Next
            End If
        End If
        tr.Append(tc2)

        table.Append(tr)
    End Sub

    Public Sub AddItemToWordDoc(ByRef wordDoc As WordprocessingDocument, resourceManager As ResourceManagerBase, itemCode As String)
        _resourceManager = resourceManager

        Dim item As AssessmentItem = GetItemByCode(itemCode)

        AddItemToWordDoc(wordDoc, resourceManager, item)
    End Sub

    Public Sub AddInlineChoiceAlternativesToWordDoc(ByRef wordDoc As WordprocessingDocument, resourceManager As ResourceManagerBase, itemCode As String)
        _resourceManager = resourceManager

        Dim item As AssessmentItem = GetItemByCode(itemCode)
        _styleSheetsToReference = GetReferencedStylesheets(item)

        AddInlineChoiceAlternativesToWordDoc(wordDoc, resourceManager, item)
    End Sub

    Public Sub AddSolutionToWordDoc(
                                    ByRef wordDoc As WordprocessingDocument,
                                    resourceManager As ResourceManagerBase,
                                    itemCode As String,
                                    itemResourceSolution As String)

        _resourceManager = resourceManager
        Dim item As AssessmentItem = GetItemByCode(itemCode)

        If item.Solution IsNot Nothing Then
            If item.Solution.AspectReferenceSetCollection IsNot Nothing _
               AndAlso item.Solution.AspectReferenceSetCollection.Count > 0 _
                Then
                Dim inlineConverter = New InlineElementConverter

                For Each aspectref As AspectReference In item.Solution.AspectReferenceSetCollection.Item(0).Items
                    Dim aspectResource As Boolean = False
                    InitHtmlConverter(wordDoc.MainDocumentPart)

                    AddText(wordDoc, String.Format(My.Resources.Name, aspectref.SourceName))
                    AddText(wordDoc, String.Format(My.Resources.MaxScore, aspectref.MaxScore))

                    Dim eventArgs As New ResourceNeededEventArgs(aspectref.SourceName, GetType(Aspect))
                    ResourceNeeded(Me, eventArgs)

                    If eventArgs.BinaryResource IsNot Nothing AndAlso eventArgs.BinaryResource.ResourceObject IsNot Nothing Then
                        aspectResource = True
                        Dim aspect = DirectCast(eventArgs.BinaryResource.ResourceObject, Aspect)

                        AddText(wordDoc, String.Format(My.Resources.Title, aspect.Title))
                        AddText(wordDoc, My.Resources.Aspect)

                        If Not String.IsNullOrEmpty(aspect.Description.Trim) Then
                            AddAspectDescription(aspect.Description.Trim, wordDoc, inlineConverter)
                        End If
                    End If

                    If aspectref.Description IsNot Nothing AndAlso Not String.IsNullOrEmpty(aspectref.Description.Trim) Then
                        If Not aspectResource Then
                            AddText(wordDoc, My.Resources.Aspect)
                        End If
                        AddAspectDescription(aspectref.Description.Trim, wordDoc, inlineConverter)
                    End If
                Next
            Else
                If item.Solution.Findings.Count > 0 AndAlso ContainsMathMl(item.Solution.Findings.ToString()) Then
                    If Not AddItemKeys(wordDoc, item.Solution.Findings.ToString()) Then
                        AddText(wordDoc, itemResourceSolution)
                    End If
                Else
                    AddText(wordDoc, itemResourceSolution)
                End If
            End If
        Else
            AddText(wordDoc, "-")
        End If
    End Sub

    Private Sub AddAspectDescription(aspectDescription As String, ByRef wordDoc As WordprocessingDocument, inlineConverter As InlineElementConverter)
        Dim xmlDoc As New XmlDocument()
        xmlDoc.LoadXml(String.Format("<wrapper>{0}</wrapper>", Trim(aspectDescription)))

        Dim nodes As XmlNode() = New List(Of XmlNode)(xmlDoc.DocumentElement.OfType(Of XmlNode)).ToArray

        If Not TemplateHelper.IsXHtmlParameterEmpty(nodes) Then
            Dim description = inlineConverter.ConvertInlineElementAnchorsToHtml(aspectDescription, PaperBasedTestPlugin.PLUGIN_NAME, AddressOf ResourceNeeded)
            Dim fakeBody As OpenXmlElement = CreateFakeBody(description)
            ResolveCustomXmlElement(fakeBody, wordDoc.MainDocumentPart, CustomXmlElementType.xhtmlElementToOpenXml, False)
            Dim firstTableCell As Wordprocessing.TableCell = fakeBody.Descendants(Of Wordprocessing.TableCell).FirstOrDefault

            If firstTableCell.ChildElements IsNot Nothing AndAlso firstTableCell.ChildElements.Count > 0 Then
                For Each o As OpenXmlElement In firstTableCell.ChildElements
                    wordDoc.MainDocumentPart.Document.Body.Append(o.CloneNode(True))
                Next
            End If
        End If
    End Sub

    Private Function ContainsMathMl(findings As String) As Boolean
        Dim regex As New Regex("(<math.*?></math>)|((.*){}()#&(.*))</math>")
        Dim itemKeys = regex.Split(findings).ToList()
        For Each itemKey As String In itemKeys
            If MathMLHelper.IsValidMathMlExpression(itemKey) Then
                Return True
            End If
        Next

        Return False
    End Function
    Private Function CreateFakeBody(description As String) As OpenXmlElement
        Dim result = New Body(String.Format("<w:body xmlns:w=""http://schemas.openxmlformats.org/wordprocessingml/2006/main""><w:tbl><w:tr><w:tc><w:p><w:customXml w:element=""xhtmlElementToOpenXml"">{0}</w:customXml></w:p></w:tc></w:tr></w:tbl></w:body>", description))
        Return CType(result, OpenXmlElement)
    End Function


    Public Sub CreateDocument(path As String, ByRef wordDoc As WordprocessingDocument)
        wordDoc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document)

        Dim mainPart As MainDocumentPart = wordDoc.AddMainDocumentPart()

        mainPart.Document = New Document()
        Dim body As New Body()
        Dim p As New Wordprocessing.Paragraph()
        Dim r As New Wordprocessing.Run()
        Dim t As New Wordprocessing.Text(String.Empty)

        r.Append(t)
        p.Append(r)
        body.Append(p)
        mainPart.Document.Append(body)
    End Sub

    Public Sub AddHeaderOne(ByRef wordDoc As WordprocessingDocument, text As String, noSpacingBefore As Boolean)
        AddHeader(wordDoc, text, noSpacingBefore, "Heading1")
    End Sub

    Public Sub AddHeaderOne(ByRef wordDoc As WordprocessingDocument, text As String)
        AddHeader(wordDoc, text, False, "Heading1")
    End Sub


    Public Sub AddHeaderTwo(ByRef wordDoc As WordprocessingDocument, text As String)
        AddHeader(wordDoc, text, False, "Heading2")
    End Sub

    Public Sub AddHeaderThree(ByRef wordDoc As WordprocessingDocument, text As String)
        AddHeader(wordDoc, text, False, "Heading3")
    End Sub

    Private Sub AddHeader(ByRef wordDoc As WordprocessingDocument, text As String, noSpacingBefore As Boolean, headerVal As String)
        Dim body As Body = wordDoc.MainDocumentPart.Document.Body
        Dim heading As New Wordprocessing.Paragraph()
        Dim headingRun As New Wordprocessing.Run()
        Dim headingText As New Wordprocessing.Text(text)
        Dim headingPPr As New Wordprocessing.ParagraphProperties()

        headingPPr.ParagraphStyleId = New ParagraphStyleId() With {.Val = headerVal}

        If noSpacingBefore Then
            Dim spacing As SpacingBetweenLines = New SpacingBetweenLines() With {.Before = "0"}
            headingPPr.Append(spacing)
        End If

        heading.Append(headingPPr)
        headingRun.Append(headingText)
        heading.Append(headingRun)
        body.Append(heading)
    End Sub

    Public Function AddTable(ByRef wordDoc As WordprocessingDocument, requestedTableWidth As TableWidth, Optional borderColor As String = "") As Wordprocessing.Table
        Dim table As New Wordprocessing.Table()
        Dim tblPr As New Wordprocessing.TableProperties(requestedTableWidth)
        Dim tblBorders As New TableBorders()

        tblBorders.TopBorder = New Wordprocessing.TopBorder()
        tblBorders.TopBorder.Val = New EnumValue(Of BorderValues)(BorderValues.Single)
        tblBorders.BottomBorder = New Wordprocessing.BottomBorder()
        tblBorders.BottomBorder.Val = New EnumValue(Of BorderValues)(BorderValues.Single)
        tblBorders.LeftBorder = New Wordprocessing.LeftBorder()
        tblBorders.LeftBorder.Val = New EnumValue(Of BorderValues)(BorderValues.Single)
        tblBorders.RightBorder = New Wordprocessing.RightBorder()
        tblBorders.RightBorder.Val = New EnumValue(Of BorderValues)(BorderValues.Single)
        tblBorders.InsideHorizontalBorder = New Wordprocessing.InsideHorizontalBorder()
        tblBorders.InsideHorizontalBorder.Val = BorderValues.Single
        tblBorders.InsideVerticalBorder = New Wordprocessing.InsideVerticalBorder()
        tblBorders.InsideVerticalBorder.Val = BorderValues.Single

        If Not String.IsNullOrEmpty(borderColor) Then
            tblBorders.TopBorder.Color = borderColor
            tblBorders.BottomBorder.Color = borderColor
            tblBorders.LeftBorder.Color = borderColor
            tblBorders.RightBorder.Color = borderColor
            tblBorders.InsideHorizontalBorder.Color = borderColor
            tblBorders.InsideVerticalBorder.Color = borderColor
        End If

        tblPr.Append(tblBorders)
        table.Append(tblPr)

        table.Append(New Wordprocessing.TableGrid())
        wordDoc.MainDocumentPart.Document.Body.Append(table)

        Return table
    End Function

    Public Function AddTable(ByRef wordDoc As WordprocessingDocument) As Wordprocessing.Table
        Return AddTable(wordDoc, New TableWidth With {.Width = "10000"})
    End Function

    Public Sub AddRow(ByRef table As Wordprocessing.Table, listOfValues As List(Of String), bold As Boolean)
        Dim tr As New Wordprocessing.TableRow()

        For Each textValue As String In listOfValues
            Dim paragraph As New Wordprocessing.Paragraph
            Dim paragraphStyle As ParagraphStyleId = New ParagraphStyleId With {.Val = "NoSpacing"}
            Dim paragraphProperties As New Wordprocessing.ParagraphProperties
            paragraphProperties.Append(paragraphStyle)
            paragraph.Append(paragraphProperties)

            Dim run As New Wordprocessing.Run()

            If bold Then
                Dim runProperties As New Wordprocessing.RunProperties()

                runProperties.Append(New Bold())
            End If

            run.Append(New Wordprocessing.Text(textValue))
            paragraph.Append(run)

            Dim tc As New Wordprocessing.TableCell(paragraph)
            tr.Append(tc)
        Next
        table.Append(tr)
    End Sub

    Public Sub AddText(ByRef wordDoc As WordprocessingDocument, text As String)
        wordDoc.MainDocumentPart.Document.Body.Append(New Wordprocessing.Paragraph(New Wordprocessing.Run(New Wordprocessing.Text(text))))
    End Sub

    Private Function AddItemKeys(wordDoc As WordprocessingDocument, mathMl As String) As Boolean
        Dim paragraph As New Wordprocessing.Paragraph

        Dim paragraphProperties As New Wordprocessing.ParagraphProperties
        Dim justification As New Wordprocessing.Justification()
        justification.Val = Wordprocessing.JustificationValues.Left
        paragraphProperties.Append(justification)

        Dim regex As New Regex("(<math.*?></math>)|((.*){}()#&(.*))</math>")
        Dim itemKeys As List(Of String)

        Try
            itemKeys = regex.Split(mathMl).ToList()
            For Each itemKey As String In itemKeys
                If MathMLHelper.IsValidMathMlExpression(itemKey) Then

                    Dim imageBytes As Byte() = MathMlToImage(itemKey)
                    If imageBytes IsNot Nothing Then
                        Dim imageHelper = New ImageHelper(_resourceManager)
                        Dim imageLocation = imageHelper.StoreTempImage(imageBytes)

                        Dim image As Image = Image.FromFile(imageLocation)
                        AddImageAtLocation(image, wordDoc.MainDocumentPart, Guid.NewGuid().ToString(), New System.Drawing.Size(image.Width, image.Height), paragraph)

                        Dim text As New Wordprocessing.Text("  ")
                        text.Space = SpaceProcessingModeValues.Preserve
                        paragraph.Append(New Wordprocessing.Run(text))
                    End If
                Else
                    Dim text As New Wordprocessing.Text(itemKey & "  ")
                    text.Space = SpaceProcessingModeValues.Preserve
                    paragraph.Append(New Wordprocessing.Run(text))
                End If
            Next

            paragraph.Append(paragraphProperties)

            wordDoc.MainDocumentPart.Document.Body.Append(paragraph)
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Private Function MathMlToImage(mathMl As String) As Byte()
        If GetMathMlPlugin() Is Nothing Then Return Nothing

        Dim mathImage = GetMathMlPlugin().RenderPng(mathMl)
        Return mathImage
    End Function

    Private Function GetMathMlPlugin() As IMathMlEditorPlugin
        If _mathMlEditorPlugin Is Nothing Then
            _mathMlEditorPlugin = Logic.PluginHelper.MathMlPlugin
        End If
        Return _mathMlEditorPlugin
    End Function

    Public Sub AddRow(ByRef table As Wordprocessing.Table, listOfValues As List(Of String))
        AddRow(table, listOfValues, False)
    End Sub

    Public Sub AddPageBreak(ByRef wordDoc As WordprocessingDocument)
        wordDoc.MainDocumentPart.Document.Body.Append(New Wordprocessing.Paragraph(New Wordprocessing.Run(New Wordprocessing.Break() With {.Type = BreakValues.Page})))
    End Sub

    Public Sub SetPageOrientationLandscape(ByRef wordDoc As WordprocessingDocument)
        Dim sectProp As IEnumerable(Of SectionProperties) = wordDoc.MainDocumentPart.Document.Descendants(Of SectionProperties)()
        If sectProp IsNot Nothing AndAlso sectProp.Count > 0 Then

            For Each sec As SectionProperties In wordDoc.MainDocumentPart.Document.Descendants(Of SectionProperties)()
                Dim pageOrientationChanged As Boolean = False
                Dim pgSz As PageSize = sec.Descendants(Of PageSize).FirstOrDefault()

                If pgSz IsNot Nothing Then
                    If pgSz.Orient Is Nothing Then
                        pageOrientationChanged = True
                        pgSz.Orient = New EnumValue(Of PageOrientationValues)(PageOrientationValues.Landscape)
                    ElseIf Not pgSz.Orient.Value = PageOrientationValues.Landscape Then
                        pageOrientationChanged = True
                        pgSz.Orient.Value = PageOrientationValues.Landscape
                    End If
                End If

                If pageOrientationChanged Then
                    Dim width = pgSz.Width
                    Dim height = pgSz.Height
                    pgSz.Width = height
                    pgSz.Height = width
                End If
            Next
        Else
            wordDoc.MainDocumentPart.Document.Body.Append(
                New SectionProperties(
                    New PageSize() With {.Width = 16838, .Height = 11906, .Orient = PageOrientationValues.Landscape}))
        End If
        _pageOrientationLandScape = True
    End Sub

    Public Function GetDefaultTableWidth(pageOrientationLandscape As Boolean) As TableWidth
        Return New TableWidth() With {.Width = CType(If(pageOrientationLandscape = True, DefaultTableWidthLandscape, DefaultTableWidthPortait), DocumentFormat.OpenXml.StringValue)}
    End Function

    Private Sub ResourceNeeded(sender As Object, e As ResourceNeededEventArgs)
        Dim resource As BinaryResource = Nothing
        Dim request = New ResourceRequestDTO()
        If (e.Command And ResourceNeededCommand.Resource) = ResourceNeededCommand.Resource Then
            If e.TypedResourceType IsNot Nothing Then
                resource = _resourceManager.GetTypedResource(e.ResourceName, e.TypedResourceType, request)
            Else
                resource = _resourceManager.GetResource(e.ResourceName, e.StreamProcessingDelegate, request)
            End If
            e.BinaryResource = resource
        End If
    End Sub

    Private Sub LoadAndProcessPackage(assessmentTestModel As AssessmentTest2, printFormList As Dictionary(Of PrintForm, String))

        For Each printForm As PrintForm In printFormList.Keys
            If _bookletCollection Is Nothing Then
                _bookletCollection = New Dictionary(Of String, WordprocessingDocument)
            End If
            _bookletCollection.Add(printForm.TypeLabel, OpenDocument(printFormList(printForm)))
        Next

        If _bookletCollection IsNot Nothing Then
            For Each wordDoc As WordprocessingDocument In _bookletCollection.Values
                FillVariables(wordDoc)
            Next

            If AssessmentTestv2Factory.ContainsView(assessmentTestModel, PaperBasedTestPlugin.PLUGIN_NAME) Then
                _testContext = AssessmentTestv2Factory.CreateView(Of WordAssessmentTest)(assessmentTestModel)
            Else
                Throw New TesterException("Test doesn't contain a WordAssessmentTest view.")
            End If

            StartPickingItems()
        Else
            Throw New TesterException("No booklets configured.")
        End If
    End Sub

    Private Function CreateParagraphWithoutSpacing() As Wordprocessing.Paragraph
        Dim paragraph As New Wordprocessing.Paragraph
        Dim paragraphStyle As ParagraphStyleId = New ParagraphStyleId With {.Val = "NoSpacing"}
        Dim paragraphProperties As New Wordprocessing.ParagraphProperties
        paragraphProperties.Append(paragraphStyle)
        paragraph.Append(paragraphProperties)
        Return paragraph
    End Function

End Class