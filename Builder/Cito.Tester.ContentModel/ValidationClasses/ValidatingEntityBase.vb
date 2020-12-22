
Imports System.Text
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports Cito.Tester.Common

<Serializable> _
Public MustInherit Class ValidatingEntityBase
    Implements IDataErrorInfo
    Implements IValidatingEntity


    <NonSerialized> _
    Private _validationErrors As ValidationValueCollection



    Protected Overridable ReadOnly Property Validator As IEntityValidation
        Get
            Return Nothing
        End Get
    End Property

    Protected Overridable ReadOnly Property ValidatingChilds As ReadOnlyCollection(Of IDataErrorInfo)
        Get
            Return Nothing
        End Get
    End Property



    <XmlIgnore> _
    Public ReadOnly Property IsValid As Boolean
        Get
            Return String.IsNullOrEmpty(Me.Error)
        End Get
    End Property

    <XmlIgnore> _
    Public MustOverride ReadOnly Property ValidationEntityIdentifier As String




    Public Function Validate(fieldName As String) As Boolean
        Dim validatorInstance As IEntityValidation = Me.Validator
        Dim validationError As String

        If validatorInstance IsNot Nothing Then
            _validationErrors.RemoveValidationValue(fieldName, Me)
            validationError = validatorInstance.ValidateEntityFieldValue(Me, fieldName)
            If Not String.IsNullOrEmpty(validationError) Then
                _validationErrors.AddValidationValue(fieldName, Me.Validator.FriendlyEntityName, Me, validationError)
                Return False
            End If
        End If

        Return True
    End Function

    Friend Sub ResetValidation(fieldName As String)
        _validationErrors.RemoveValidationValue(fieldName, Me)
    End Sub




    Public Function GetMD5Hash() As Byte()
        Return SerializeHelper.GetMD5Hash(Me)
    End Function

    Public MustOverride Sub ValidateAllProperties()



    Public Function GetValidationErrors(includeChildren As Boolean) As ValidationValueCollection Implements IValidatingEntity.GetValidationErrors
        Dim returnValue As New ValidationValueCollection()

        returnValue.AddRange(_validationErrors)

        If includeChildren AndAlso Me.ValidatingChilds IsNot Nothing Then
            For Each validatingChild As IDataErrorInfo In Me.ValidatingChilds
                If TypeOf validatingChild Is IValidatingEntity Then
                    returnValue.AddRange(DirectCast(validatingChild, IValidatingEntity).GetValidationErrors(True))
                Else
                    Throw New Exception("Expected each entity in this collection implements IValidatingEntity!")
                End If
            Next
        End If

        Return returnValue
    End Function


    Protected Sub New()
        _validationErrors = New ValidationValueCollection
    End Sub



    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Dim messageBuilder As New StringBuilder

            If _validationErrors.Count > 0 Then
                For Each entry As ValidationValue In _validationErrors
                    If String.IsNullOrEmpty(entry.ValidatingEntity.ValidationEntityIdentifier) Then
                        messageBuilder.AppendFormat(" - {0}. ({1}){2}", entry.Message, entry.FriendlyEntityName, Environment.NewLine)
                    Else
                        messageBuilder.AppendFormat(" - {0} in '{1}' ({2}){3}", entry.Message, entry.ValidatingEntity.ValidationEntityIdentifier, entry.FriendlyEntityName, Environment.NewLine)
                    End If
                Next
            End If

            If Me.ValidatingChilds IsNot Nothing Then
                For Each validatingChild As IDataErrorInfo In Me.ValidatingChilds
                    messageBuilder.Append(validatingChild.Error)
                Next
            End If

            Return messageBuilder.ToString()
        End Get
    End Property


    <XmlIgnore> _
    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            Dim returnValue As String = String.Empty

            Dim validation As ValidationValue = _validationErrors.GetValidationValue(columnName, Me)
            If validation IsNot Nothing Then
                returnValue = validation.Message
            End If

            Return returnValue
        End Get
    End Property


End Class

