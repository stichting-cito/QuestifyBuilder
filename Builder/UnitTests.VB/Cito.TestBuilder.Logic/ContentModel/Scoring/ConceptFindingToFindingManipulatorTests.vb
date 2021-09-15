
Imports Cito.Tester.ContentModel

''' <summary>
''' Test Copy behavior from KeyFinding to ConceptFinding
''' </summary>
<TestClass>
Public Class ConceptFindingToFindingManipulatorTests : Inherits BaseFindingToFindingManipulatorTests(Of ConceptFinding)

    Protected Overrides Function CreateFinding(id As String) As ConceptFinding
        Return New ConceptFinding(id)
    End Function

End Class
