Imports Cito.Tester.ContentModel


Namespace ContentModel

    Friend Class HierarchicalParameterSetCollectionValidator

        Private ReadOnly _prmSetColl As ParameterSetCollection

        Public Sub New(prmSetColl As ParameterSetCollection)
            _prmSetColl = prmSetColl
        End Sub


        Public Function GetErrors() As String
            Dim ret As New List(Of String)

            For Each prmColl In _prmSetColl
                Dim valueBag As New Dictionary(Of String, String)(StringComparison.InvariantCultureIgnoreCase)
                For Each prm In prmColl.InnerParameters
                    If Not prm.IsValid(valueBag) Then
                        Dim errorMessage As String = prm.GetError(valueBag)
                        If Not ret.Contains(errorMessage) Then ret.Add(errorMessage)
                    End If
                    Dim c = TryCast(prm, ICollectionParameter)
                    If (c Is Nothing) Then valueBag.Add(prm.Name, prm.ToString())
                Next
            Next
            Return String.Join(vbNewLine, ret.ToArray)
        End Function

        Public Function Validate() As Boolean
            Dim ret As Boolean = True

            For Each prmColl In _prmSetColl

                Dim valueBag As New Dictionary(Of String, String)(StringComparison.InvariantCultureIgnoreCase)


                For Each prm In prmColl.InnerParameters

                    ret = ret AndAlso prm.IsValid(valueBag)
                    If (Not ret) Then Return ret

                    Dim c = TryCast(prm, ICollectionParameter)
                    If (c Is Nothing) Then valueBag.Add(prm.Name, prm.ToString())

                Next
            Next

            Return ret
        End Function
    End Class
End Namespace
