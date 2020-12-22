Imports System.Xml.Linq
Imports Cito.Tester.ContentModel

Namespace QTI.Interfaces

    Public Interface IResponseProcessingCustomOperators

        Function GetPreProcessingCustomOperator(selectedPreprocessors As SelectedPreprocessorCollection, responseIndex As Integer, baseValue As String, Optional responseSubIndex As Integer = 0) As String

        Function GetDecimalCustomOperator(responseIndex As Integer, Optional responseSubIndex As Integer = 0) As XElement

        Function GetMathMLCustomOperator_Equals(responseIndex As Integer, correctResponse As String, Optional responseSubIndex As Integer = 0) As XElement

        Function GetMathMLCustomOperator_Equals(responseIndex As Integer, correctResponse As String, sequence As String, Optional responseSubIndex As Integer = 0) As XElement

        Function GetMathMLCustomOperator_EqualsSoft(responseIndex As Integer, correctResponse As String, Optional responseSubIndex As Integer = 0) As XElement

        Function GetMathMLCustomOperator_EqualsSoft(responseIndex As Integer, correctResponse As String, sequence As String, Optional responseSubIndex As Integer = 0) As XElement

        Function GetMathMLCustomOperator_Evaluate(responseIndex As Integer, variableName As String, variableValue As String, expectedResult As String, Optional responseSubIndex As Integer = 0, Optional degree As String = "") As XElement

        Function GetMathMLCustomOperator_Equivalent(responseIndex As Integer, correctResponse As String, Optional responseSubIndex As Integer = 0) As XElement

        Function GetMathMLCustomOperator_Equivalent(responseIndex As Integer, correctResponse As String, sequence As String, Optional responseSubIndex As Integer = 0) As XElement

        Function GetMathMLCustomOperator_EvaluateDependency(responseIndex As Integer, correctResponse As String, variables As List(Of Tuple(Of String, Integer, Boolean)), Optional responseSubIndex As Integer = 0, Optional addNullCheck As Boolean = False) As XElement

        Function GetMathMLCustomOperator_EqualEquation(responseIndex As Integer, correctResponse As String, Optional responseSubIndex As Integer = 0) As XElement

        Function GetMathMLCustomOperator_EqualSteps(responseIndex As Integer, Optional responseSubIndex As Integer = 0) As XElement

        Function GetProcessingForVariable(responseIndex As Integer, Optional responseSubIndex As Integer = 0) As XElement

        Function GetProcessingForSpecifiedVariable(variableIdentifier As String, Optional responseSubIndex As Integer = 0) As XElement

        Function GetGeogebraCustomOperator(responseIndex As Integer, correctResponse As String, Optional responseSubIndex As Integer = 0) As XElement

    End Interface
End NameSpace