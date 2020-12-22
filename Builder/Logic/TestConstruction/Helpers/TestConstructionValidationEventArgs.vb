Imports Questify.Builder.Logic.Chain

Namespace TestConstruction.Helpers
    Public Class TestConstructionValidationEventArgs
        Inherits EventArgs

        Private _resolution As ResolutionEnum = ResolutionEnum.Abort
        Private ReadOnly _resolutionsAvailable As ResolutionEnum = ResolutionEnum.Abort
        Private ReadOnly _underlyingException As ChainHandlerException = Nothing

        Public Sub New()
        End Sub

        Public Sub New(ByVal underlyingException As ChainHandlerException)
            _underlyingException = underlyingException
        End Sub

        Public Sub New(ByVal resolutionsAvailable As resolutionEnum, ByVal underlyingException As ChainHandlerException)
            Me.New(underlyingException)
            _resolutionsAvailable = resolutionsAvailable
        End Sub


        Public Property Resolution() As ResolutionEnum
            Get
                Return _resolution
            End Get
            Set
                If Not (_resolutionsAvailable And value) = value Then
                    Throw New InvalidOperationException("Value for 'TestConstructionEventsArg.Resolution' is not valid. This value is marked as unavailable using property 'ResolutionsAvailable'.")
                End If

                _resolution = value
            End Set
        End Property

        Public ReadOnly Property ResolutionsAvailable() As ResolutionEnum
            Get
                Return _resolutionsAvailable
            End Get
        End Property

        Public ReadOnly Property UnderlyingException() As ChainHandlerException
            Get
                Return _underlyingException
            End Get
        End Property


        <Flags()>
        Public Enum ResolutionEnum
            RetryFix = 1
            RetryIgnore = 2
            Abort = 4
            All = (RetryFix + RetryIgnore + Abort)
        End Enum

    End Class
End Namespace