Public Interface ITransformable(Of Out TScoreParameter As {ScoringParameter}) : Inherits ITransformable

    Function Transform() As TScoreParameter

End Interface

Public Interface ITransformable

    ReadOnly Property CanTransform As Boolean
    ReadOnly Property IsTransformed As Boolean

End Interface
