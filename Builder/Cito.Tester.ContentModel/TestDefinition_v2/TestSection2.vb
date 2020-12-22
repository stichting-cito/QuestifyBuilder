
Imports System.Xml.Serialization
Imports Cito.Tester.Common

Imports Cito.Tester.ContentModel.Datasources


<Serializable,
XmlRoot(ElementName:="TestSection2")>
Public Class TestSection2
    Inherits TestComponent2


    Private _components As TestComponentCollection2
    Private _containsPickableComponents As Boolean
    Private _containsPickedComponents As Boolean
    Private _pickedComponents As Integer

    Private _sectionType As enumSectionType = enumSectionType.normal

    Private _itemDataSource As String

    Private _itemDataSourceBehaviour As DataSourceBehaviourEnum

    Private _itemWeightForVariantTests As Double = 1.0




    Public Sub New()
        MyBase.New()

        Me._components = New TestComponentCollection2(Me)
        Me._containsPickableComponents = True
    End Sub



    <XmlAttribute("itemDataSource")>
    Public Property ItemDataSource As String
        Get
            Return _itemDataSource
        End Get
        Set
            _itemDataSource = value
        End Set
    End Property

    <XmlAttribute("itemDataSourceBehaviour")>
    Public Property ItemDataSourceBehaviour As DataSourceBehaviourEnum
        Get
            Return _itemDataSourceBehaviour
        End Get
        Set
            _itemDataSourceBehaviour = value
        End Set
    End Property

    <XmlAttribute("itemWeightForVariantTests")>
    Public Property ItemWeightForVariantTests As Double
        Get
            Return _itemWeightForVariantTests
        End Get
        Set
            _itemWeightForVariantTests = value
        End Set
    End Property

    <XmlArray("Components"),
XmlArrayItem("Section", GetType(TestSection2)),
XmlArrayItem("ItemReference", GetType(ItemReference2))>
    Public ReadOnly Property Components As TestComponentCollection2
        Get
            Return Me._components
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property IsPickable As Boolean
        Get
            If Not _containsPickableComponents Then Return False

            If Me.State = ComponentState.Pickable Then
                For Each c As TestComponent2 In Me._components
                    If c.IsPickable Then Return True
                Next
            End If

            _containsPickableComponents = False
            Return False
        End Get
    End Property

    <XmlIgnore>
    Public Overrides ReadOnly Property MaxScore As Double
        Get
            Dim cummulativeMaxScore As Double = 0
            For Each component As TestComponent2 In Me.Components
                If TypeOf component Is ItemReference2 Then
                    Dim itemref As ItemReference2 = DirectCast(component, ItemReference2)
                    If itemref.Active = True AndAlso itemref.ItemFunctionalType = ItemFunctionalType.Regular Then
                        cummulativeMaxScore += itemref.MaxScore
                    End If
                Else
                    cummulativeMaxScore += component.MaxScore
                End If
            Next

            Return cummulativeMaxScore
        End Get
    End Property

    <XmlIgnore>
    Public Property PickedComponents As Integer
        Get
            Return _pickedComponents
        End Get
        Set
            _pickedComponents = value
        End Set
    End Property

    <XmlAttribute("SectionType")>
    Public Property SectionType As enumSectionType
        Get
            Return _sectionType
        End Get
        Set
            _sectionType = value
        End Set
    End Property






    Public Function ContainsItem(identifier As String) As Boolean
        For Each child As AssessmentTestNode In Me.Components
            If TypeOf child Is TestSection2 Then
                Return DirectCast(child, TestSection2).ContainsItem(identifier)
            Else
                Return Me.Components.Contains(identifier)
            End If
        Next
    End Function

    Public Function ContainsPickedComponents() As Boolean
        If _containsPickedComponents Then Return True

        For Each c As TestComponent2 In Me._components
            If TypeOf c Is ItemReference2 Then
                If DirectCast(c, ItemReference2).State = ComponentState.Picked Then
                    _containsPickedComponents = True
                    Return True
                End If
            ElseIf TypeOf c Is TestSection2 Then
                If DirectCast(c, TestSection2).ContainsPickedComponents() = True Then
                    _containsPickedComponents = True
                    Return True
                End If
            End If
        Next

        Return False
    End Function


    Public Function FindFirstItemReferenceInSection() As ItemReference2
        Return FindFirstItemReferenceInSection(Me)
    End Function



    Public Shared Function FindFirstItemReferenceInSection(section As TestSection2) As ItemReference2
        Dim result As ItemReference2 = Nothing

        For Each comp As AssessmentTestNode In section.Components
            If TypeOf comp Is ItemReference2 Then
                Dim item As ItemReference2 = DirectCast(comp, ItemReference2)
                Return item
            Else
                Dim innerSection As TestSection2 = DirectCast(comp, TestSection2)
                result = FindFirstItemReferenceInSection(innerSection)
                If result IsNot Nothing Then Return result
            End If
        Next

        Return result
    End Function


    Public Function GetAllItemReferencesInSection(includeChildren As Boolean) As List(Of ItemReference2)
        Dim returnValue As New List(Of ItemReference2)

        For Each comp As TestComponent2 In Me.Components
            If TypeOf comp Is ItemReference2 Then
                returnValue.Add(DirectCast(comp, ItemReference2))
            Else
                If includeChildren Then
                    Dim innerSection As TestSection2 = DirectCast(comp, TestSection2)
                    returnValue.AddRange(innerSection.GetAllItemReferencesInSection(True))
                End If
            End If
        Next

        Return returnValue
    End Function


    Public Function GetAllSectionsInSection() As List(Of TestSection2)
        Dim returnValue As New List(Of TestSection2)

        For Each comp As TestComponent2 In Me.Components
            If TypeOf comp Is TestSection2 Then
                returnValue.Add(DirectCast(comp, TestSection2))
                returnValue.AddRange(DirectCast(comp, TestSection2).GetAllSectionsInSection())
            End If
        Next

        Return returnValue
    End Function


    Public Function FindNodeByIdentifier(identifier As String) As AssessmentTestNode
        Dim result As AssessmentTestNode = Nothing

        If Me.Identifier = identifier Then
            result = Me
        Else
            For Each component As TestComponent2 In Me.Components
                If TypeOf component Is TestSection2 Then
                    result = DirectCast(component, TestSection2).FindNodeByIdentifier(identifier)
                    If result IsNot Nothing Then
                        Return result
                    End If
                End If
            Next
        End If

        Return result
    End Function


End Class