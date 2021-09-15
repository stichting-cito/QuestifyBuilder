﻿
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic.ContentModel

<TestClass>
Public Class BaseParamValidatorTests

    <TestInitialize()> _
    Public Sub MyTestInitialize()
        FailOnAssert.Disable = True 'Disable 
    End Sub
    
    <TestCleanup()> _
    Public Sub MyTestCleanup()
        FailOnAssert.Disable = False
    End Sub

    <TestMethod(), TestCategory("Logic")>
    Public Sub checkIfUnknowParamIsValid_ShouldBe()
        'Arrange
        Dim param = New FakeParameter()
        
        'Act
        Dim result = param.IsValid()
        
        'Assert
        Assert.IsTrue(result)
    End Sub

    Class FakeParameter
        Inherits ParameterBase

        Public Overrides Function EqualsByValue(param As ParameterBase) As Boolean
            Return False
        End Function

        Public Overrides Function SetValue(value As String) As Boolean
            Throw New NotImplementedException()
        End Function
    End Class

End Class
