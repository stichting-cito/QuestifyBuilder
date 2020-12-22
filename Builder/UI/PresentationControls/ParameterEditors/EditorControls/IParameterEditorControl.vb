Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Interface IParameterEditorControl
    Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)
    Event EditResource As EventHandler(Of ResourceNameEventArgs)
End Interface
