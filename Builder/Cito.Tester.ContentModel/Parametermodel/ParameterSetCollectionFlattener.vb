Imports System.Linq

Friend Class ParameterSetCollectionFlattener

    Private ReadOnly _coll As ParameterSetCollection

    Sub New(paramSetColl As ParameterSetCollection)
        _coll = paramSetColl
    End Sub

    Public Iterator Function Flatten(Optional includeSpecialPrmsFromScoringPrms As Boolean = False) As IEnumerable(Of ParameterBase)
        If _coll Is Nothing Then Return
        For Each prmColl As ParameterCollection In _coll
            For Each e In Flatten(prmColl, includeSpecialPrmsFromScoringPrms)
                Yield e
            Next
        Next

    End Function

    Public Function GetParentCollection(parameter As ParameterBase) As ParameterCollection
        If _coll Is Nothing Then Return Nothing
        Dim returnValue As ParameterCollection = Nothing
        For Each prmColl As ParameterCollection In _coll
            GetParentCollectionRecursive(prmColl, returnValue, parameter)
            If returnValue IsNot Nothing Then Return returnValue
        Next
        Return Nothing
    End Function

    Private Sub GetParentCollectionRecursive(collection As ParameterCollection, ByRef returnCollection As ParameterCollection, parameter As ParameterBase)
        If collection.InnerParameters.Contains(parameter) Then
            returnCollection = collection
            Return
        End If
        For Each colSet In collection.InnerParameters.OfType(Of CollectionParameter)
            If colSet.Value IsNot Nothing Then
                For Each col In colSet.Value
                    GetParentCollectionRecursive(col, returnCollection, parameter)
                    If returnCollection IsNot Nothing Then return
                Next
            End If
        Next
    End Sub

    Public Iterator Function FlattenDefinition(Optional includeDynamicParameters As Boolean = True) As IEnumerable(Of ParameterBase)
        If _coll Is Nothing Then Return
        Dim collToCheck As List(Of ParameterCollection)
        If Not includeDynamicParameters Then
            collToCheck = _coll.Where(Function(c) c.IsDynamicCollection = False).ToList
        Else
            collToCheck = _coll.ToList
        End If
        For Each prmColl As ParameterCollection In collToCheck
            For Each e In FlattenDefinition(prmColl)
                Yield e
            Next
        Next
    End Function

    Public Iterator Function FlattenDefinitionName(Optional includeCollectionName As Boolean = False) As IEnumerable(Of String)
        If _coll Is Nothing Then Return
        Dim collToCheck As List(Of ParameterCollection)
        collToCheck = _coll.Where(Function(c) c.IsDynamicCollection = False).ToList
        For Each prmColl As ParameterCollection In collToCheck
            For Each e In FlattenDefinition(prmColl)
                If includeCollectionName Then
                    Yield $"{prmColl.Id}-{e.Name}"
                Else
                    Yield e.Name
                End If
            Next
        Next
    End Function

    Private Iterator Function Flatten(prmColl As ParameterCollection, includeSpecialPrmsFromScoringPrms As Boolean) As IEnumerable(Of ParameterBase)
        For Each p As ParameterBase In prmColl.InnerParameters
            Yield p
            If TypeOf p Is CollectionParameter Then
                Dim collPrm = DirectCast(p, CollectionParameter)
                If (collPrm.Value IsNot Nothing) Then
                    For Each p2 In collPrm.Value.FlattenParameters()
                        Yield p2
                    Next
                End If
                If includeSpecialPrmsFromScoringPrms AndAlso TypeOf p Is ScoringParameter Then
                    Dim xtraParameters = DirectCast(p, ScoringParameter).GetParametersWithDesignerSettings
                    If xtraParameters IsNot Nothing AndAlso xtraParameters.Count > 0 Then
                        For Each p3 In xtraParameters
                            Yield p3
                        Next
                    End If
                End If
            End If
        Next
    End Function

    Private Iterator Function FlattenDefinition(prmColl As ParameterCollection) As IEnumerable(Of ParameterBase)
        For Each p As ParameterBase In prmColl.InnerParameters
            Yield p
            If TypeOf p Is CollectionParameter Then
                Dim collPrm = DirectCast(p, CollectionParameter)
                If (collPrm.Value IsNot Nothing AndAlso collPrm.BluePrint IsNot Nothing) Then
                    For Each p2 In collPrm.BluePrint.InnerParameters
                        Yield p2
                    Next
                End If
            End If
        Next
    End Function

End Class
