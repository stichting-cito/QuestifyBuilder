Imports System.ComponentModel
Imports System.Diagnostics.CodeAnalysis
Imports System.Text

<SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix"), _
    Serializable> _
Public MustInherit Class ValidatingEntityCollectionBase(Of TValidatingContentModelEntity As IDataErrorInfo)
    Inherits List(Of TValidatingContentModelEntity)
    Implements IDataErrorInfo
    Implements IValidatingEntity

    Protected Sub New()
        MyBase.New()
    End Sub

    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Dim errorBuilder As New StringBuilder()

            For Each dataErrorEntity As IDataErrorInfo In Me
                errorBuilder.Append(dataErrorEntity.Error)
            Next

            Return errorBuilder.ToString()
        End Get
    End Property

    Public ReadOnly Property ErrorItem(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            Return String.Empty
        End Get
    End Property


    Public Function GetValidationErrors(includeChildren As Boolean) As ValidationValueCollection Implements IValidatingEntity.GetValidationErrors
        Dim returnValue As New ValidationValueCollection()

        If includeChildren Then
            For Each dataErrorEntity As IDataErrorInfo In Me
                If TypeOf dataErrorEntity Is IValidatingEntity Then
                    returnValue.AddRange(DirectCast(dataErrorEntity, IValidatingEntity).GetValidationErrors(True))
                Else
                    Throw New Exception("Expected each entity in this collection implements IValidatingEntity!")
                End If
            Next
        End If

        Return returnValue
    End Function
End Class
