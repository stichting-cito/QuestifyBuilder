Imports Cito.Tester.Common

Public Class MethodTransaction
    Inherits TransactionBase


    Private _methodName As String



    Public Sub New(section As TestSectionViewBase, methodName As String)
        MyBase.New(section)
        If section IsNot Nothing Then
            If Not String.IsNullOrEmpty(methodName) Then
                Me.Id = section.Identifier
                _methodName = methodName
            Else
                Throw New TestViewerException(My.Resources.Error_MethodTransaction_ParametersNotSet)
            End If
        Else
            Throw New TestViewerException(My.Resources.Error_MethodTransaction_ParametersNotSet)
        End If
    End Sub

    Public Sub New()
        MyBase.New()
    End Sub



    Public Property MethodName As String
        Get
            Return _methodName
        End Get
        Set
            _methodName = value
        End Set
    End Property


End Class