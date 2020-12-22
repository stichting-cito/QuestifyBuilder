
Imports Questify.Builder.Logic.Chain

Namespace QTI.ChainHandlers

    Public Class ProgressHandler(Of ProcessHandlerType)
        Inherits ChainHandlerBase(Of ProcessHandlerType)

        Public Delegate Sub ProgressDelegate(message As String, numberOfSteps As Nullable(Of Integer))

        Private ReadOnly _progressDelegate As ProgressDelegate
        Private ReadOnly _message As String
        Private ReadOnly _numberOfSteps As Nullable(Of Integer)

        Public Overrides Function ProcessRequest(requestData As ProcessHandlerType) As ChainHandlerResult
            _progressDelegate.Invoke(_message, _numberOfSteps)
            Return ChainHandlerResult.RequestHandled
        End Function

        Public Sub New(message As String, progressDelegate As ProgressDelegate)
            _message = message
            _progressDelegate = progressDelegate
        End Sub

        Public Sub New(numberOfSteps As Integer, progressDelegate As ProgressDelegate)
            _numberOfSteps = numberOfSteps
            _progressDelegate = progressDelegate
        End Sub

    End Class
End NameSpace