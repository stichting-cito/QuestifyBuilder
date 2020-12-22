Imports Cito.Tester.ContentModel
Imports System.Linq

Namespace ContentModel.Scoring

    Friend Class AddInteractionToGroup(Of T)
        Inherits AddInteractionToGroup

        Private ReadOnly _manipulator As IGapScoringManipulator(Of T)

        Public Sub New(manipulator As IGapScoringManipulator(Of T))
            _manipulator = manipulator
        End Sub

        Public Overrides Sub Execute(key As String, factSetNumbers As IEnumerable(Of Integer))

            Dim values As New List(Of T)
            Dim gapValues = _manipulator.GetKeyStatus()(key)
            For Each gapValue In gapValues
                values.Add(gapValue.Value)
            Next

            _manipulator.RemoveKey(key)

            For index As Integer = 0 To factSetNumbers.Count() - 1
                Dim factSetNumber = factSetNumbers(index)
                _manipulator.SetFactSetTarget(factSetNumber)
                If index = 0 Then
                    _manipulator.SetKey(key, values.ToArray())
                Else
                    _manipulator.SetKeyWithDefaultValue(key)
                End If
            Next
        End Sub

    End Class

    Public MustInherit Class AddInteractionToGroup

        Public Shared Function Create(scoringParamater As ScoringParameter, solution As Solution) As AddInteractionToGroup

            Dim scoreManipulator = ScoringParameterFactory.GetKeyScoreBaseManipulator(scoringParamater, solution)

            Dim typeToInstantiate As Type
            If scoreManipulator.GetType().BaseType.IsGenericType AndAlso scoreManipulator.GetType().GetInterfaces().Any(Function(tInterface) tInterface.IsGenericType AndAlso tInterface.GetGenericTypeDefinition = GetType(IGapScoringManipulator(Of ))) Then
                Dim keyType = scoreManipulator.GetType().GetInterfaces().First(Function(t) t.IsInterface AndAlso t.IsGenericType).GetGenericArguments().First()
                Dim basetypeToConstruct = GetType(AddInteractionToGroup(Of ))
                typeToInstantiate = basetypeToConstruct.MakeGenericType(keyType)
            Else
                typeToInstantiate = GetType(AddChoiceInteractionToGroup)
            End If

            Return CType(Activator.CreateInstance(typeToInstantiate, scoreManipulator), AddInteractionToGroup)
        End Function

        Public MustOverride Sub Execute(key As String, factSetNumbers As IEnumerable(Of Integer))

    End Class

    Friend Class AddChoiceInteractionToGroup
        Inherits AddInteractionToGroup

        Private ReadOnly _manipulator As IScoreManipulator

        Public Sub New(manipulator As IScoreManipulator)
            _manipulator = manipulator
        End Sub

        Public Overrides Sub Execute(key As String, factSetNumbers As IEnumerable(Of Integer))

            If TypeOf _manipulator Is IChoiceScoringManipulator Then
                Dim manipulator = DirectCast(_manipulator, IChoiceScoringManipulator)

                Dim selected As String = Nothing
                If manipulator.GetKeyStatus().Any(Function(s) s.Value) Then
                    selected = manipulator.GetKeyStatus().First(Function(s) s.Value).Key
                    manipulator.RemoveKey(selected)
                End If

                For index As Integer = 0 To factSetNumbers.Count() - 1
                    Dim factSetNumber = factSetNumbers(index)
                    manipulator.SetFactSetTarget(factSetNumber)
                    If index = 0 AndAlso selected IsNot Nothing Then
                        manipulator.SetKey(selected)
                    End If
                Next

            ElseIf TypeOf _manipulator Is IChoiceArrayScoringManipulator Then

                Dim manipulator = DirectCast(_manipulator, IChoiceArrayScoringManipulator)
                Dim selected = manipulator.GetKeyStatus(key)
                manipulator.RemoveKey(key)

                For index As Integer = 0 To factSetNumbers.Count() - 1
                    Dim factSetNumber = factSetNumbers(index)
                    manipulator.SetFactSetTarget(factSetNumber)
                    If index = 0 AndAlso Not String.IsNullOrEmpty(selected) Then
                        manipulator.SetKey(key, selected)
                    End If
                Next


            End If


        End Sub

    End Class
End Namespace