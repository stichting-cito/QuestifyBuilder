

Namespace Commanding

    Public Class ContextParameterBinding
        Inherits ParameterBinding


        Private _contextObject As Object = Nothing



        Public Sub New(contextObject As Object)
            If contextObject Is Nothing Then
                Throw New ArgumentNullException("contextObjext")
            End If
            _contextObject = contextObject
        End Sub



        Public ReadOnly Property ContextObject() As Object
            Get
                Return _contextObject
            End Get
        End Property



        Public Overrides Function GetCommandParameter(source As Object) As Object
            Return _contextObject
        End Function


    End Class

End Namespace

