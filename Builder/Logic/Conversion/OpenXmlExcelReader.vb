
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Spreadsheet

Namespace Conversion

    Public Class OpenXmlExcelReader
        Private ReadOnly _customPropertiesDefinition As New Dictionary(Of String, String)
        Private ReadOnly _customPropertyValues As New Dictionary(Of String, Dictionary(Of String, String))
        Private ReadOnly _errorMessages As New List(Of String)
        Private _resourceCodeColumnName As String
        Private _knownColumnHeaders As List(Of String)

        ''' <summary>
        ''' Gets the custom property definitions.
        ''' </summary>
        Public ReadOnly Property CustomPropertyDefinitions As List(Of String)
            Get
                Dim newList As New List(Of String)
                For Each customProperty As String In _customPropertiesDefinition.Values
                    newList.Add(customProperty)
                Next
                Return newList
            End Get
        End Property

        ''' <summary>
        ''' Gets the custom property values.
        ''' </summary>
        Public ReadOnly Property CustomPropertyValues As Dictionary(Of String, Dictionary(Of String, String))
            Get
                Return _customPropertyValues
            End Get
        End Property

        ''' <summary>
        ''' Read the Excel document
        ''' </summary>
        Public Function ReadExcelDocument(filename As String, knownColumnHeaders As List(Of String), resourceCodeColumnName As String) As List(Of String)
            Dim fileStream As FileStream = Nothing
            Try
                Try
                    ' Try to open the file with FileShare.Read. 
                    fileStream = New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read)
                    Return ReadExcelDocument(fileStream, knownColumnHeaders, resourceCodeColumnName)
                Catch ex As Exception
                    ' Unable to open in FileShare.Read mode, trying FileShare.ReadWrite next.
                End Try
                Try
                    ' If the file is open in a program like Word or Excel, FileShare.ReadWrite will be required to open it. 
                    ' If this fails as well, then we can't open the file.
                    fileStream = New FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                    Return ReadExcelDocument(fileStream, knownColumnHeaders, resourceCodeColumnName)
                Catch ex As Exception
                    _errorMessages.Add(ex.Message)
                End Try
            Finally
                If fileStream IsNot Nothing Then
                    fileStream.Close()
                    fileStream.Dispose()
                End If
            End Try

            Return _errorMessages
        End Function

        ''' <summary>
        ''' Read the Excel document
        ''' </summary>
        Public Function ReadExcelDocument(stream As Stream, knownColumnHeaders As List(Of String), resourceCodeColumnName As String) As List(Of String)
            Try
                _resourceCodeColumnName = resourceCodeColumnName
                _knownColumnHeaders = knownColumnHeaders

                OpenExcelDocument(stream)
            Catch ex As Exception
                _errorMessages.Add(ex.Message)
            End Try
            Return _errorMessages
        End Function

        ''' <summary>
        ''' Opens the excel document.
        ''' </summary>
        ''' <param name="stream">Stream of the file.</param>
        Private Sub OpenExcelDocument(stream As Stream)
            Using document As SpreadsheetDocument = SpreadsheetDocument.Open(stream, False)
                Dim sheets As IEnumerable(Of Sheet) = document.WorkbookPart.Workbook.Descendants(Of Sheet)()
                If sheets.Count() = 0 Then
                    ' The specified worksheet does not exist.
                    Exit Sub
                End If
                'we'll work with the first workbook
                Dim worksheetPart As WorksheetPart = CType(document.WorkbookPart.GetPartById(sheets.First().Id), WorksheetPart)
                Dim worksheet As Worksheet = worksheetPart.Worksheet

                Dim rowIndex As Integer = 0
                Dim codeColumnReference As String = String.Empty
                For Each row As Row In worksheet.Descendants(Of Row)()
                    Dim code As String = String.Empty
                    Dim cellIndex As Integer = 0
                    Dim itemCustomPropertyValues As New Dictionary(Of String, String)
                    For Each cell As Cell In row
                        Dim cellValue As String = String.Empty
                        If cell.DataType = "s" Then
                            cellValue = GetSharedStringItemById(document.WorkbookPart, CType(cell.CellValue.Text, Integer))
                        ElseIf cell.CellValue IsNot Nothing AndAlso cell.CellValue.Text IsNot Nothing Then
                            cellValue = cell.CellValue.Text
                        ElseIf cell.DataType = "inlineStr" Then
                            cellValue = cell.InlineString.InnerText
                        End If
                        If rowIndex = 0 Then
                            'first row should be the caption
                            If Not String.IsNullOrEmpty(cellValue) Then
                                'we cannot count the rows because empty rows are skipped so we should use the reference
                                If Not _knownColumnHeaders.Contains(cellValue.ToLower) AndAlso Not _resourceCodeColumnName.Equals(cellValue.ToLower) Then
                                    _customPropertiesDefinition.Add(OnlyAlphaNumericChars(cell.CellReference), cellValue)
                                End If
                                If _resourceCodeColumnName.Equals(cellValue.ToLower) Then
                                    codeColumnReference = OnlyAlphaNumericChars(cell.CellReference)
                                End If
                            End If
                        Else
                            If OnlyAlphaNumericChars(cell.CellReference) = codeColumnReference Then
                                code = cellValue
                            Else
                                If _customPropertiesDefinition.ContainsKey(OnlyAlphaNumericChars(cell.CellReference)) Then
                                    Dim propertyName As String = _customPropertiesDefinition(OnlyAlphaNumericChars(cell.CellReference))
                                    If Not _knownColumnHeaders.Contains(propertyName.ToLower) Then
                                        itemCustomPropertyValues.Add(propertyName, cellValue)
                                    End If
                                End If
                            End If
                        End If
                        cellIndex += 1
                    Next
                    If rowIndex = 0 AndAlso String.IsNullOrEmpty(codeColumnReference) Then
                        _errorMessages.Add(My.Resources.CodeNotFound)
                        Exit Sub
                    ElseIf Not rowIndex = 0 Then
                        If Not String.IsNullOrEmpty(code) Then
                            If Not _customPropertyValues.ContainsKey(code) Then
                                _customPropertyValues.Add(code, itemCustomPropertyValues)
                            Else
                                'we'll take the last one
                                _customPropertyValues(code) = itemCustomPropertyValues
                            End If
                        End If
                    End If
                    rowIndex += 1
                Next
                document.Close()
            End Using
        End Sub

        ''' <summary>
        ''' Gets the shared string item by id.
        ''' </summary>
        ''' <param name="workbookPart">The workbook part.</param>
        ''' <param name="id">The id.</param><returns></returns>
        Public Function GetSharedStringItemById(workbookPart As WorkbookPart, id As Integer) As String
            Return workbookPart.SharedStringTablePart.SharedStringTable.Elements(Of SharedStringItem)().ElementAt(id).Text.Text
        End Function

        Public Function OnlyAlphaNumericChars(OrigString As String) As String
            Dim rgx As Regex = New Regex("[^a-zA-Z]")
            Return rgx.Replace(OrigString, String.Empty)
        End Function

    End Class
End Namespace