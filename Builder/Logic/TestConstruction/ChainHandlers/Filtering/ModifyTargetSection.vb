Imports Cito.Tester.ContentModel.Datasources
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.Chain
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Filtering
    Public Class ModifyTargetSection
        Inherits ChainHandlerBase(Of TestConstructionRequest)

        Private ReadOnly _items As IEnumerable(Of ResourceRef)
        Private ReadOnly _target As TestSection2
        Private ReadOnly _datasourceToSet As String

        Public Sub New(ByVal items As IEnumerable(Of ResourceRef), ByVal existingSection As TestSection2)
            _items = items
            _target = existingSection
        End Sub

        Public Sub New(ByVal items As IEnumerable(Of ResourceRef), ByVal existingSection As TestSection2, ByVal setDataSource As String)
            _items = items
            _target = existingSection
            _datasourceToSet = setDataSource
        End Sub

        Public Sub New(ByVal items As IEnumerable(Of ResourceRef), ByVal newSectionName As String, ByVal parentSection As TestSection2)
            _items = items
            _target = New TestSection2() With {.Identifier = newSectionName, .Title = newSectionName, .Parent = parentSection}
        End Sub

        Public Sub New(ByVal items As IEnumerable(Of ResourceRef), ByVal newSectionName As String, ByVal parentSection As TestSection2, ByVal datasource As String)
            Me.New(items, newSectionName, parentSection)
            _target.ItemDataSource = datasource
        End Sub


        Public Overrides Function ProcessRequest(requestData As TestConstructionRequest) As ChainHandlerResult
            For Each e As ResourceRef In _items
                If Not (requestData.OverridenTarget.ContainsKey(e)) Then
                    requestData.OverridenTarget.Add(e, _target)
                Else
                    requestData.OverridenTarget(e) = _target
                End If
            Next

            If Not String.IsNullOrEmpty(_datasourceToSet) Then
                Debug.Assert(String.IsNullOrEmpty(_target.ItemDataSource), "WHoa this should have been empty.")
                _target.ItemDataSource = _datasourceToSet
            End If

            Return ChainHandlerResult.RequestHandled
        End Function

    End Class
End Namespace