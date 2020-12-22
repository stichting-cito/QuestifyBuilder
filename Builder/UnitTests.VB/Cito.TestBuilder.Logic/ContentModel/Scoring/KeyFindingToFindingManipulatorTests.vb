
Imports Cito.Tester.ContentModel

<TestClass>
Public Class KeyFindingToFindingManipulatorTests : Inherits BaseFindingToFindingManipulatorTests(Of KeyFinding)

    Protected Overrides Function CreateFinding(id As String) As KeyFinding
        Return New KeyFinding(id)
    End Function

End Class
