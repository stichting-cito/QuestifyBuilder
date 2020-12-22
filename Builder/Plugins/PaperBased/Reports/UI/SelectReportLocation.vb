Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.IO
Imports Questify.Builder.Configuration

Public Class SelectReportLocation
    Implements INotifyPropertyChanged

    Private _OverWriteFileName As Boolean

    Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged


    Public Sub New()
        InitializeComponent()
        _OverWriteFileName = False
        Me.DataBindings.Add(New Binding("OverWriteFileName", Me.OptionValidatorWordExportBindingSource, "OverwriteExisting",
                                                             False, DataSourceUpdateMode.OnPropertyChanged))

    End Sub



    Private Sub BrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseButton.Click
        ErrorProvider.Clear
        If Not String.IsNullOrEmpty(FileNameTextBox.Text) Then
            SaveFileDialog.FileName = IO.Path.GetFileName(FileNameTextBox.Text)
        End If
        If SaveFileDialog.ShowDialog = DialogResult.OK Then
            FileNameTextBox.Text = SaveFileDialog.FileName
            ReportSettings.WordReport = Path.GetDirectoryName(FileNameTextBox.Text)
            Me.ValidateChildren()

            OverWriteFileName = IO.File.Exists(SaveFileDialog.FileName)

        End If
        Me.ValidateChildren()
    End Sub

    Private Sub NotyfyProperty(ByVal propName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propName))
    End Sub



    <Bindable(True, BindingDirection.TwoWay)>
    Public Property OverWriteFileName() As Boolean
        Get
            Return _OverWriteFileName
        End Get
        Set(ByVal value As Boolean)
            _OverWriteFileName = value
            NotyfyProperty("OverWriteFileName")
        End Set
    End Property


End Class
