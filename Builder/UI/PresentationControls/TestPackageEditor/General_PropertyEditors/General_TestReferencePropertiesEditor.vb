Imports Cito.Tester.ContentModel
Imports Questify.Builder.Logic

Public Class General_TestReferencePropertiesEditor
    Implements ITestReferenceEditorPropertyEditor


    Private _testReferenceModel As TestReference
    Private _testReference As GeneralTestReference



    Public Sub New()
        InitializeComponent()

    End Sub



    Public Property CurrentDataSource() As TestReference Implements ITestReferenceEditorPropertyEditor.CurrentDataSource
        Get
            Return _testReferenceModel
        End Get
        Set(ByVal value As TestReference)
            _testReferenceModel = value

            If value IsNot Nothing Then
                _testReference = New GeneralTestReference(value)
                ControlBindingSource.DataSource = _testReference

            Else
                _testReferenceModel = Nothing
                ControlBindingSource.DataSource = Nothing
            End If
        End Set
    End Property

    Public Overrides ReadOnly Property FrameTitle() As String
        Get
            Return My.Resources.General_TestReferencePropertiesEditor_FrameTitle
        End Get
    End Property

    Public Overrides ReadOnly Property HasFieldsToFillByUser() As Boolean
        Get
            Return True
        End Get
    End Property





End Class