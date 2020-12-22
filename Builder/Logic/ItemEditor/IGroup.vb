Imports System.ComponentModel

Namespace ItemEditor
    Public Interface IGroup
        Inherits INotifyPropertyChanged
        ReadOnly Property Name() As String
        Property HasVisibleParameters() As Boolean
        Property IsVisible() As Boolean
        ReadOnly Property Parameters() As IEnumerable(Of IParameterEvaluator)
    End Interface
End Namespace
