
<Serializable> _
Public MustInherit Class TestPackageComponentViewBase
    Inherits TestPackageComponentBase


    Private _parent As TestPackageComponentBase



    Protected Sub New()
    End Sub

    Protected Sub New(componentModel As TestPackageComponent)
        MyBase.New(componentModel)

        AddDynamicPropertiesFromModel(componentModel)
    End Sub



    Public ReadOnly Property TestPackageComponentModel As TestPackageComponent
        Get
            Return DirectCast(Me.NodeModel, TestPackageComponent)
        End Get
    End Property



    Public Property Parent As TestPackageComponentBase
        Get
            Return _parent
        End Get
        Set
            _parent = value

            If value IsNot Nothing Then
                Me.TestPackageComponentModel.Parent = value.NodeModel
            Else
                Me.TestPackageComponentModel.Parent = Nothing
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property ValidationEntityIdentifier As String
        Get
            Return Me.Title
        End Get
    End Property



    Public Overridable Sub AddDynamicPropertiesFromModel(testComponentModel As TestPackageComponent)
        Me.NodeModel = testComponentModel

        Me.TestPackageComponentModel.AddDynamicPropertyIfNotExists("settingsCollection", GetType(SettingsCollection2), Nothing, New SettingsCollection2())
    End Sub


End Class