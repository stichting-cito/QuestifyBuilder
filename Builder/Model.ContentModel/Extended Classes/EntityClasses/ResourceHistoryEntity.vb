Namespace Questify.Builder.Model.ContentModel.EntityClasses

    Partial Public Class ResourceHistoryEntity

        Public ReadOnly Property Version As String
            Get
                Return String.Concat(MajorVersion, ".", MinorVersion)
            End Get
        End Property
    End Class

End Namespace