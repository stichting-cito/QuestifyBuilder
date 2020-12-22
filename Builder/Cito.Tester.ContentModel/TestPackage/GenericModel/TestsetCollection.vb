
Imports Cito.Tester.Common


<Serializable> _
Public Class TestsetCollection
    Inherits List(Of TestSet)



    Public Sub New(parent As TestPackageNode)
        MyBase.New()
        _parent = parent
    End Sub

    Private _parent As TestPackageNode

    Public Overloads Sub Add(testset As TestSet)
        ReflectionHelper.CheckIsNotNothing(testset, "Testset component")
        MyBase.Add(testset)
        testset.Parent = _parent
    End Sub
End Class
