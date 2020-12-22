Imports System.ComponentModel

Public Class ExcelImportOptionsDataEntity
    Inherits ImportOptionsDataEntityBase
    Implements IDataErrorInfo

    Public Property ExcelUrl() As String
        Get
            Return Url
        End Get
        Set
            Url = value
        End Set
    End Property

End Class
