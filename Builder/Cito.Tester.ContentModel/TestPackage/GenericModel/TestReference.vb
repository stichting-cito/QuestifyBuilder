Imports System.Diagnostics.CodeAnalysis
Imports System.Xml.Serialization
Imports Cito.Tester.Common


<Serializable, _
 XmlRoot(ElementName:="TestReference")> _
Public Class TestReference
    Inherits TestPackageComponent

    Private _sourceName As String



    <SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")> _
    Public Sub New()
        MyBase.New()

        Me.SourceName = String.Empty
    End Sub

    Public Sub New(test As AssessmentTest2)
        Me.New()
        If test IsNot Nothing Then
            Me._sourceName = test.Identifier
        Else
            Throw New ContentModelException(My.Resources.Error_ItemReference_Constructor_ParametersNotSet)
        End If
    End Sub




    <XmlAttribute("Src")> _
    Public Property SourceName As String
        Get
            Return Me._sourceName
        End Get
        Set
            Me._sourceName = value
        End Set
    End Property



End Class

