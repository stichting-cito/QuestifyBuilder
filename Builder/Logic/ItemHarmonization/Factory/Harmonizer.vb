Imports Questify.Builder.Model.ContentModel.EntityClasses
Imports Cito.Tester.ContentModel
Imports System.Collections.Concurrent
Imports Questify.Builder.Logic.Enums
Imports Questify.Builder.Logic.ItemHarmonization.Implementation
Imports Questify.Builder.Logic.ItemHarmonization.Implementation.Base
Imports Questify.Builder.Logic.ItemHarmonization.Interface
Imports Questify.Builder.Logic.Service.Interfaces

Namespace ItemHarmonization.Factory
    Public Class Harmonizer
        Implements IHarmonizer
        Implements IDisposable

        Private ReadOnly _specificImplementations As New List(Of IHarmonize)()

        Public Sub New(options As HarmonizeOptions)
            ResetImplementations(options)
        End Sub

        Private Sub ResetImplementations(options As HarmonizeOptions)
            _specificImplementations.Clear()
            Select Case options
                Case HarmonizeOptions.Base
                    _specificImplementations.Add(New BaseTemplateHarmonization())
                    Exit Select
                Case HarmonizeOptions.Inline
                    _specificImplementations.Add(New InlineElementHarmonization())
                    Exit Select
                Case HarmonizeOptions.All
                    _specificImplementations.Add(New BaseTemplateHarmonization())
                    _specificImplementations.Add(New InlineElementHarmonization())
                    Exit Select
                Case Else
                    Throw New NotImplementedException()
            End Select
        End Sub

        Public Function Harmonize(item As ItemResourceEntity) As Boolean Implements IHarmonizer.Harmonize
            Dim isFixed As Boolean = False
            For Each h As IHarmonize In _specificImplementations
                If h.Harmonize(item) Then
                    isFixed = True
                End If
            Next
            Return isFixed
        End Function

        Public Function Harmonize(item As ItemResourceEntity, template As String) As Boolean Implements IHarmonizer.Harmonize
            Dim isFixed As Boolean = False
            For Each h As IHarmonize In _specificImplementations
                If h.Harmonize(item.GetAssessmentItem(), item, template) Then
                    isFixed = True
                End If
            Next
            Return isFixed
        End Function

        Public Function Harmonize(item As ItemResourceEntity, template As String, harmonizeOption As HarmonizeOptions) As Boolean
            ResetImplementations(harmonizeOption)
            Return Harmonize(item, template)
        End Function

        Public Function Harmonize(templates As IEnumerable(Of String), item As ItemResourceEntity, harmonizeOption As HarmonizeOptions) As Boolean
            ResetImplementations(harmonizeOption)
            Return Harmonize(templates, item)
        End Function

        Public Function Harmonize(templates As IEnumerable(Of String), item As ItemResourceEntity) As Boolean Implements IHarmonizer.Harmonize
            Dim isFixed As Boolean = False
            For Each h As IHarmonize In _specificImplementations
                If h.Harmonize(templates, item) Then
                    isFixed = True
                End If
            Next
            Return isFixed
        End Function

        Public Function Harmonize(parametersetCollections As ConcurrentDictionary(Of String, ParameterSetCollection), item As ItemResourceEntity) As Boolean Implements IHarmonizer.Harmonize
            Dim isFixed As Boolean = False
            For Each h As IHarmonize In _specificImplementations
                If h.Harmonize(parametersetCollections, item) Then
                    isFixed = True
                End If
            Next
            Return isFixed
        End Function

        Private disposedValue As Boolean

        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    _specificImplementations.ForEach(Sub(i)
                                                         If TypeOf i Is BaseHarmonization Then
                                                             DirectCast(i, BaseHarmonization).Dispose()
                                                         End If
                                                     End Sub)
                    _specificImplementations.Clear()
                End If
            End If
            disposedValue = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
        End Sub
    End Class
End Namespace
