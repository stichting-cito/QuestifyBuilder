Option Infer On
Option Strict Off

Imports System.Linq
Imports Questify.Builder.Logic.Chain
Imports Cito.Tester.ContentModel
Imports Cito.Tester.Common
Imports Questify.Builder.Logic.TestConstruction.Requests

Namespace TestConstruction.ChainHandlers.Processing
    Public Class AddNonExistingSectionsToAssessment
        Inherits ChainHandlerBase(Of TestConstructionRequest)

        Private ReadOnly _assessmentTest As AssessmentTest2
        Private ReadOnly _defaultTarget As TestSection2



        Public Sub New(ByVal assessmentTest As AssessmentTest2, ByVal defaultTarget As TestSection2)
            _assessmentTest = assessmentTest
            _defaultTarget = defaultTarget
        End Sub

        Public Overrides Function ProcessRequest(requestData As TestConstructionRequest) As ChainHandlerResult

            If (requestData.OverridenTarget.Count > 0) Then
                Dim allSections As IEnumerable(Of TestSection2)
                Dim sectionsToAdd As IEnumerable(Of TestSection2)

                allSections = _assessmentTest.GetAllSectionsInTest()
                sectionsToAdd = GetAllSectionsToCreate(allSections, requestData.OverridenTarget.Values.Distinct(New TestSection2Comparer()))

                _defaultTarget.Components.AddRange(sectionsToAdd.Cast(Of TestComponent2)())
            End If
            Return ChainHandlerResult.RequestHandled
        End Function

        Private Function GetAllSectionsToCreate(ByVal allSections As IEnumerable(Of TestSection2), ByVal possibleSectionsToCreate As IEnumerable(Of TestSection2)) As IEnumerable(Of TestSection2)
            Return SetOperations.Difference(possibleSectionsToCreate, allSections, New TestSection2Comparer)
        End Function


        Private Class TestSection2Comparer
            Implements IEqualityComparer(Of TestSection2)

            Public Function Equals1(x As TestSection2, y As TestSection2) As Boolean Implements IEqualityComparer(Of TestSection2).Equals
                Return x.Identifier = y.Identifier
            End Function

            Public Function GetHashCode1(obj As TestSection2) As Integer Implements IEqualityComparer(Of TestSection2).GetHashCode
                Return obj.Identifier.GetHashCode
            End Function
        End Class

    End Class

End Namespace