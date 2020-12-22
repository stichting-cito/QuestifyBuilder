Imports Cito.Tester.ContentModel

Public Class GenericTestPackageModelPlugin
    Implements ITestPackageModelPlugin

    Public ReadOnly Property Name As String Implements ITestPackageModelPlugin.Name
        Get
            Return GenericTestModelPlugin.PLUGIN_NAME
        End Get
    End Property

    Public Function GetTestSet(testSetModel As TestSet) As TestSetViewBase Implements ITestPackageModelPlugin.GetTestSet
        Return New GeneralTestSet(testSetModel)
    End Function

    Public Function IsSupportedTestPackage(package As Type) As Boolean Implements ITestPackageModelPlugin.IsSupportedTestPackage
        If package Is GetType(GeneralTestPackage) OrElse
                package Is GetType(GeneralTestSet) OrElse
                package Is GetType(GeneralTestReference) Then
            Return True
        End If

        Return False
    End Function

    Public Function IsSupportedView(view As String) As Boolean Implements ITestPackageModelPlugin.IsSupportedView
        Return view = Name
    End Function

    Public Function ConstructTestReferenceView(model As TestReference) As TestPackageComponentViewBase Implements ITestPackageModelPlugin.ConstructTestReferenceView
        Return New GeneralTestReference(model)
    End Function

    Public Function ConstructTestSetView(model As TestSet) As TestSetViewBase Implements ITestPackageModelPlugin.ConstructTestSetView
        Dim returnValue As New GeneralTestSet(model)

        For Each component As TestPackageComponent In model.Components
            If TypeOf component Is TestReference Then
                returnValue.Components.Add(ConstructTestReferenceView(DirectCast(component, TestReference)))
            Else
                Throw New NotSupportedException(
                    $"Type '{component.GetType().FullName}' not supported in component collection")
            End If
        Next

        Return returnValue
    End Function

    Public Function CreateView(model As TestPackage) As TestPackageViewBase Implements ITestPackageModelPlugin.CreateView
        Dim returnValue As New GeneralTestPackage(model)

        For Each testset As TestSet In model.TestSets
            returnValue.TestSets.Add(ConstructTestSetView(testset))
        Next

        If Not model.IncludedViews.Contains(Name) Then
            model.IncludedViews.Add(Name)
        End If

        If Not model.IncludedViews.Contains(GenericTestModelPlugin.PLUGIN_NAME) Then
            model.IncludedViews.Add(GenericTestModelPlugin.PLUGIN_NAME)
        End If

        Return returnValue
    End Function
End Class
