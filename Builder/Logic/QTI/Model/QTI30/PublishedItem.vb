Imports System.Xml
Imports Cito.Tester.Common
Imports Enums
Imports Questify.Builder.Logic.QTI.Helpers.QTI30.QtiModelHelpers
Imports Questify.Builder.Logic.QTI.Xsd.QTI30

Namespace QTI.Model.QTI30

    Public Class PublishedItem
        Public Property Qti As XmlDocument
        Public Property ItemMetaDataCollection As MetaDataCollection
        Public Property ItemType As Nullable(Of ItemTypeEnum)
        Public Property MaxScore As Integer
        Public Property HighestWeight As Double
        Public Property DepencencyList As List(Of String)
        Public Property FileList As List(Of FileType)
        Public Property Code As String
        Public Property Title As String

        Public Sub New(itemHelper As ItemHelper, qti As XmlDocument, maxScore As Integer)
            _Code = itemHelper.Code
            _Title = itemHelper.Title
            _ItemMetaDataCollection = itemHelper.MetaDataCollection
            _ItemType = itemHelper.ItemType
            _DepencencyList = itemHelper.Dependencies
            _FileList = GetFileList(itemHelper)
            _Qti = qti
            _MaxScore = maxScore
        End Sub

        Protected Overridable Function GetFileList(itemHelper As ItemHelper) As List(Of FileType)
            Return itemHelper.GetFileList()
        End Function

    End Class
End Namespace