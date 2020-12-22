Imports System.Xml.Serialization


<Serializable>
Public MustInherit Class TestComponentViewBase
    Inherits TestComponentBase


    Private _parent As TestComponentBase




    Protected Sub New()
    End Sub

    Protected Sub New(componentModel As TestComponent2)
        MyBase.New(componentModel)

        AddDynamicPropertiesFromModel(componentModel)
    End Sub



    Public ReadOnly Property TestComponentModel As TestComponent2
        Get
            Return DirectCast(Me.NodeModel, TestComponent2)
        End Get
    End Property




    Public ReadOnly Property MaxScore As Double
        Get
            Return Me.TestComponentModel.MaxScore
        End Get
    End Property

    <XmlIgnore>
    Public Property Parent As TestComponentBase
        Get
            Return _parent
        End Get
        Set
            _parent = Value

            If Value IsNot Nothing Then
                Me.TestComponentModel.Parent = Value.NodeModel
            Else
                Me.TestComponentModel.Parent = Nothing
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Title
        End Get
    End Property



    Friend Overridable Sub AddDynamicPropertiesFromModel(testComponentModel As TestComponent2)
        Me.NodeModel = testComponentModel

        Me.TestComponentModel.AddDynamicPropertyIfNotExists("settingsCollection", GetType(SettingsCollection2), Nothing, New SettingsCollection2())
    End Sub


End Class