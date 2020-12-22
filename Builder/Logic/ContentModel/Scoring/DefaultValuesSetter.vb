Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    Public Class DefaultValuesSetter(Of T)
        Inherits DefaultValuesSetter

        Private ReadOnly _manipulator As IScoreManipulator
        Private ReadOnly _findingManipulator As IFindingManipulator

        Public Sub New(manipulator As IScoreManipulator, findingManipulator As IFindingManipulator)
            _manipulator = manipulator
            _findingManipulator = findingManipulator
        End Sub

        Public Overrides Sub Execute(key As String, factSetNumber As Integer)

            _manipulator.SetFactSetTarget(factSetNumber)

            If TypeOf _manipulator Is MultiResponseScoringManipulator Then
                Dim manipulator = DirectCast(_manipulator, IChoiceScoringManipulator)
                manipulator.SetKeyWithDefaultValue(key)
            Else
                Dim factId = _manipulator.GetFactIdForKey(key)
                _findingManipulator.SetFactSetTarget(factSetNumber)
                _findingManipulator.FindOrCreateFact(factId)
            End If

        End Sub

    End Class

    Public MustInherit Class DefaultValuesSetter

        Public Shared Function Create(scoringParameter As ScoringParameter, solution As Solution) As DefaultValuesSetter

            Dim findingManipulator = ScoringParameterFactory.GetKeyManipulator(solution, scoringParameter)
            Dim scoreManipulator = ScoringParameterFactory.GetKeyScoreBaseManipulator(scoringParameter, solution)
            Dim typeToInstantiate As Type = Nothing

            If scoreManipulator.GetType().BaseType.IsGenericType AndAlso scoreManipulator.GetType().GetInterfaces().Any(Function(t) t.IsInterface AndAlso t.IsGenericType) Then
                Dim keyType = scoreManipulator.GetType().GetInterfaces().First(Function(t) t.IsInterface AndAlso t.IsGenericType).GetGenericArguments().First()
                Dim basetypeToConstruct = GetType(DefaultValuesSetter(Of ))
                typeToInstantiate = basetypeToConstruct.MakeGenericType(keyType)
            Else
                typeToInstantiate = GetType(DefaultValueSetterChoice)
            End If

            Return CType(Activator.CreateInstance(typeToInstantiate, scoreManipulator, findingManipulator), DefaultValuesSetter)
        End Function

        Public MustOverride Sub Execute(key As String, factSetNumber As Integer)

    End Class

    Public Class DefaultValueSetterChoice
        Inherits DefaultValuesSetter

        Private ReadOnly _manipulator As IScoreManipulator
        Private ReadOnly _findingManipulator As IFindingManipulator

        Public Sub New(manipulator As IScoreManipulator, findingManipulator As IFindingManipulator)
            _manipulator = manipulator
            _findingManipulator = findingManipulator
        End Sub

        Public Overrides Sub Execute(key As String, factSetNumber As Integer)
            _manipulator.SetFactSetTarget(factSetNumber)
        End Sub
    End Class

End Namespace