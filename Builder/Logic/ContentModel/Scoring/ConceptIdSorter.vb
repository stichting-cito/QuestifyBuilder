Namespace ContentModel.Scoring
    public Class ConceptIdSorter : Implements IComparer(Of String)

        Const catchAll As String = "[*]"

        Public Function Compare(x As String, y As String) As Integer Implements IComparer(Of String).Compare
            Dim subParamIdX = DefaultStringOperations.GetSubParameterId(x)
            Dim subParamIdY = DefaultStringOperations.GetSubParameterId(y)

            Dim answerCatX = DefaultStringOperations.GetAnswerCategoryId(x)
            Dim answerCatY = DefaultStringOperations.GetAnswerCategoryId(y)


            Dim compAnswerCategoy = CompareAnswerCategory(answerCatX, answerCatY)
            Dim compSubParam = System.String.Compare(subParamIdX, subParamIdY, System.StringComparison.Ordinal)


            If (String.IsNullOrWhiteSpace(answerCatX) Xor String.IsNullOrWhiteSpace(answerCatY)) Then
                If (String.IsNullOrWhiteSpace(answerCatX)) Then Return -1
                Return 1
            End If

            If (answerCatX = catchAll Xor answerCatY = catchAll) Then
                If (answerCatX = catchAll) Then Return 1
                Return -1
            End If

            Return If(compSubParam = 0, compAnswerCategoy, compSubParam)
        End Function


        Private Function CompareAnswerCategory(answerCatX As String, answerCatY As String) As Integer
            If (answerCatX = catchAll AndAlso answerCatY = catchAll) Then Return 0
            If (answerCatX = catchAll AndAlso answerCatY <> catchAll) Then Return 1
            If (answerCatX <> catchAll AndAlso answerCatY = catchAll) Then Return -1
            Return System.String.Compare(answerCatX, answerCatY, System.StringComparison.Ordinal)
        End Function

    End Class
End Namespace