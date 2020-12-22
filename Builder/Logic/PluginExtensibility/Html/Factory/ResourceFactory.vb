Imports Questify.Builder.Model.ContentModel.EntityClasses

Namespace PluginExtensibility.Html.Factory

    Public Class ResourceFactory

        Private ReadOnly _bankId As Integer

        Public Sub New(bankId As Integer)
            _bankId = bankId
        End Sub

        Public ReadOnly Property BankId As Integer
            Get
                Return _bankId
            End Get
        End Property


        Public Function CreateGenericResourceAsTemplate() As GenericResourceEntity
            Dim genericResource As New GenericResourceEntity()

            With genericResource
                .ResourceId = Guid.NewGuid()
                .BankId = _bankId
                .Version = "0.1"
                .ResourceData = New ResourceDataEntity()
            End With

            Return genericResource
        End Function


    End Class


End Namespace


