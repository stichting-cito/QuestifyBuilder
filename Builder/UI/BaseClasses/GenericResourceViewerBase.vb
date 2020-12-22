Imports Cito.Tester.Common
Imports Cito.Tester.ContentModel
Imports Questify.Builder.Model.ContentModel.EntityClasses


Public Class GenericResourceViewerBase


    Public Event ResourceNeeded As EventHandler(Of ResourceNeededEventArgs)



    Private _BindingContext As BindingContext
    Private _resourceData As GenericResourceEntity
    Private _resourceManager As ResourceManagerBase



    Public Overridable ReadOnly Property HasChangesToPropagate As Boolean
        Get
            Return False
        End Get
    End Property

    Public Property ResourceManager() As ResourceManagerBase
        Get
            Return _resourceManager
        End Get
        Set(value As ResourceManagerBase)
            If (value IsNot Nothing) Then
                _resourceManager = value
            End If
        End Set
    End Property

    Public Property DataSource() As GenericResourceEntity
        Get
            Return _resourceData
        End Get
        Set(ByVal value As GenericResourceEntity)
            _resourceData = value

            DataBind()
        End Set
    End Property

    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")> _
    Public Overrides Property BindingContext() As System.Windows.Forms.BindingContext
        Get
            If Me._BindingContext IsNot Nothing Then
                Return Me._BindingContext
            Else
                If Parent IsNot Nothing Then
                    Return Parent.BindingContext
                Else
                    Return Nothing
                End If
            End If
        End Get
        Set(ByVal value As System.Windows.Forms.BindingContext)
            Me._BindingContext = value
            Me.OnBindingContextChanged(New System.EventArgs())
        End Set
    End Property

    Protected ReadOnly Property IsResourceDataAvailable() As Boolean
        Get
            Return Me.DataSource IsNot Nothing AndAlso Me.DataSource.ResourceData IsNot Nothing AndAlso Me.DataSource.ResourceData.BinData IsNot Nothing
        End Get
    End Property





    Public Overridable Sub PreSave()
    End Sub



    <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters")> _
    Protected Overridable Sub DataBind()
        Throw New NotImplementedException("Override this methode to use is.")
    End Sub

    Protected Overridable Sub OnResourceNeeded(ByVal e As ResourceNeededEventArgs)
        If e Is Nothing Then
            Throw New ArgumentNullException("e")
        Else
            RaiseEvent ResourceNeeded(Me, e)
        End If

    End Sub



    Private Sub GenericResourceViewerBase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
