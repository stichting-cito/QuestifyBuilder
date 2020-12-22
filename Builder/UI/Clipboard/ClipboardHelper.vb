Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.HelperClasses

Public Class ClipboardHelper

    Private Shared _clipboardWrapperToTextSb As Text.StringBuilder

    Private Shared Function ConvertClipboardWrapperToText(ByVal dataForClipBoard As ComponentCollectionClipboardWrapperBase) As String
        _ClipboardWrapperToTextSB = New Text.StringBuilder
        dataForClipBoard.TraverseComponents(AddressOf RecurseComponentsCallBack, TraversalMethod.DepthFirst)

        Return _ClipboardWrapperToTextSB.ToString
    End Function

    Private Shared Sub RecurseComponentsCallBack(ByVal node As TestNodeBase, ByRef level As Integer)
        Dim textToAdd As String = ""
        Dim tempTestNode As TestNodeBase = node
        If tempTestNode.GetType Is GetType(AssessmentTestNode) Then
            Dim testNode As AssessmentTestNode = DirectCast(tempTestNode, AssessmentTestNode)
            If TypeOf testNode Is ItemReference2 Then
                Dim itemRef As ItemReference2 = DirectCast(testNode, ItemReference2)
                textToAdd = String.Format("ItemRef to [{0}] (Id=[{1}])", itemRef.SourceName, itemRef.Identifier)

            ElseIf TypeOf testNode Is TestSection2 Then
                Dim section As TestSection2 = DirectCast(testNode, TestSection2)
                textToAdd = String.Format("Section [{0}] (Id=[{1}], {2} components)", section.Title, section.Identifier, section.Components.Count)

            ElseIf TypeOf testNode Is TestPart2 Then
                Dim part As TestPart2 = DirectCast(testNode, TestPart2)
                textToAdd = String.Format("TestPart [{0}]  (Id=[{1}], {2} Sections)", part.Title, part.Identifier, part.Sections.Count)
            Else
                Throw New NotSupportedException()
            End If
        ElseIf tempTestNode.GetType Is GetType(TestPackageNode) Then
            Dim testPackageNode As TestPackageNode = DirectCast(tempTestNode, TestPackageNode)
            If TypeOf testPackageNode Is TestReference Then
                Dim testRef As TestReference = DirectCast(testPackageNode, TestReference)
                textToAdd = String.Format("TestRef to [{0}] (Id=[{1}])", testRef.SourceName, testRef.Identifier)

            ElseIf TypeOf testPackageNode Is TestSet Then
                Dim testset As TestSet = DirectCast(testPackageNode, TestSet)
                textToAdd = String.Format("Tetset [{0}] (Id=[{1}], {2} components)", testset.Title, testset.Identifier, testset.Components.Count)
            Else
                Throw New NotSupportedException()
            End If
        End If

        _ClipboardWrapperToTextSB.Append(CChar(" "), Level * 2)
        _ClipboardWrapperToTextSB.AppendLine(textToAdd)
    End Sub


    Public Shared Function GetData() As ComponentCollectionClipboardWrapperBase
        Dim objectToReturn As ComponentCollectionClipboardWrapperBase = Nothing
        Dim dataObj As IDataObject = Clipboard.GetDataObject
        Dim format As String = GetType(TestComponentCollectionClipboardWrapper).FullName

        If dataObj.GetDataPresent(format) Then
            Dim ms As IO.MemoryStream = CType(dataObj.GetData(format), IO.MemoryStream)

            objectToReturn = BinarySerializationHelper.BinaryDeserializeFromStream(Of ComponentCollectionClipboardWrapperBase)(ms)
        End If

        Return objectToReturn
    End Function

    Public Shared Sub SetData(ByVal dataForClipBoard As ComponentCollectionClipboardWrapperBase, ByVal orphanTestComponents As Boolean)
        If dataForClipBoard.Components.Count > 0 Then
            Dim format As DataFormats.Format = DataFormats.GetFormat(dataForClipBoard.GetType.FullName)

            Dim dataObj As IDataObject = New DataObject

            If orphanTestComponents Then
                dataForClipBoard.OrphanTestComponents()
            End If

            Dim ms As New IO.MemoryStream
            BinarySerializationHelper.BinarySerializeToStream(Of ComponentCollectionClipboardWrapperBase)(ms, dataForClipBoard)

            dataObj.SetData(format.Name, False, ms)
            dataObj.SetData(DataFormats.Text, True, ConvertClipboardWrapperToText(dataForClipBoard))
            Clipboard.SetDataObject(dataObj, False)
        Else
            Clipboard.Clear()
        End If
    End Sub

    Public Enum TraversalMethod
        BreadthFirst
        DepthFirst
    End Enum

End Class