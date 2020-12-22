Imports System.Activities
Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.workers.Flow

    Public NotInheritable Class RemoveEmptyValues
        Inherits CodeActivity

        Property KeyValues As InArgument(Of ValueCollection)

        Protected Overrides Sub Execute(ByVal context As CodeActivityContext)
            Dim values = context.GetValue(KeyValues)

            For Each kv As KeyValue In values.Where(Function(v) TypeOf v Is KeyValue).ToList()
                If (IsEmpty(kv)) Then values.Remove(kv)
            Next
        End Sub

        Private Function IsEmpty(value As KeyValue) As Boolean
            If Not value.Values.Any() Then Return True
            Dim valuesToRemove As New List(Of BaseValue)
            value.Values.ForEach(Sub(v)
                                     If Not TypeOf v Is NoValue AndAlso Not TypeOf v Is CatchAllValue Then
                                         If Not v.HasValue() Then valuesToRemove.Add(v)
                                     End If
                                 End Sub)
            valuesToRemove.ForEach(Sub(vtr)
                                       value.Values.Remove(vtr)
                                   End Sub)
            Return Not value.Values.Any()
        End Function

    End Class

End Namespace