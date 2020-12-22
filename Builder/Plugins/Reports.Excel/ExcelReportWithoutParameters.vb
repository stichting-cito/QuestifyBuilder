
Imports System.Linq
Imports Questify.Builder.Logic.Service.Model.Entities

Public Class ExcelReportWithoutParameters
    Inherits ExcelReport



    Public Overrides ReadOnly Property Name() As String
        Get
            If MyBase.Collection.Any Then
                Select Case MyBase.Collection.FirstOrDefault.GetType
                    Case GetType(AssessmentTestResourceDto)
                        Return My.Resources.ReportDescriptionTestWithoutParameter
                    Case GetType(ItemResourceDto)
                        Return My.Resources.ReportNameItemWithoutParameters
                End Select
            End If
            Return String.Empty
        End Get
    End Property

    Public overrides ReadOnly Property Description() As String
        Get
            Dim first = MyBase.Collection.FirstOrDefault
            If first IsNot Nothing Then
                Select Case first.GetType
                    Case GetType(AssessmentTestResourceDto)
                        Return My.Resources.ReportDescriptionTestWithoutParameter
                    Case GetType(ItemResourceDto)
                        Return My.Resources.ReportDescriptionItemWithoutParameter
                End Select
            End If
            Return String.Empty
        End Get
    End Property

    Public Overrides ReadOnly Property IncludeItemParameters As Boolean
        Get
            Return False
        End Get
    End Property


End Class
