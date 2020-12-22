
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.QTI.Xsd.QTI22_Final

Namespace QTI.Model.QTI22

    Public Class TestDocumentSet

        Public Property AssessmentTestType As AssessmentTestType
        Public Property NumberOfItemsInTest As Integer = 0
        Public Property Test As AssessmentTest2
        Public Property WeightMaxScore As Decimal = 0

        Public Sub New(test As AssessmentTest2)
            _Test = test
            Initialise()
        End Sub
        Protected Overridable Sub Initialise()
            _NumberOfItemsInTest = _Test.GetAllItemReferencesInTest.Count
        End Sub

    End Class
End NameSpace