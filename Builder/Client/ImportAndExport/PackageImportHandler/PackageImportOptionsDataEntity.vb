Imports System.ComponentModel

Public Class PackageImportOptionsDataEntity
    Inherits ImportOptionsDataEntityBase
    Implements IDataErrorInfo

    Public Property PackageUrl() As String
        Get
            Return Url
        End Get
        Set
            Url = Value
        End Set
    End Property

    Public Property ImportToRoot As Boolean = False
End Class