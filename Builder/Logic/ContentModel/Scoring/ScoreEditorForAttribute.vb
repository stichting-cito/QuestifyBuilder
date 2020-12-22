Namespace ContentModel.Scoring

    <AttributeUsage(AttributeTargets.Class, AllowMultiple:=True)>
    NotInheritable Class ScoreEditorForAttribute
        Inherits Attribute

        Sub New(type As Type)
            ForScoreParameter = type
        End Sub

        Public Property ForScoreParameter As Type

    End Class
End Namespace