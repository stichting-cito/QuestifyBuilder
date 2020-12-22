Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.IO
Imports Questify.Builder.Configuration


Public Class SelectReportLocation
    Implements INotifyPropertyChanged

    Private _overWriteFileName As Boolean
    Private _fileLocationOnly As Boolean

    Public Event PropertyChanged(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged


    Public Sub New(Optional fileLocationOnly As Boolean = False)
        InitializeComponent()
        _overWriteFileName = False
        _fileLocationOnly = fileLocationOnly
        Me.DataBindings.Add(New System.Windows.Forms.Binding("OverWriteFileName", Me.OptionsValidatorBindingSource, "OverwriteExisting",
                                                             False, DataSourceUpdateMode.OnPropertyChanged))

    End Sub



    Private Sub BrowseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BrowseButton.Click
        If _fileLocationOnly Then
            If Not String.IsNullOrEmpty(FileNameTextBox.Text) Then
                FolderBrowserDialog.SelectedPath = Path.GetDirectoryName(FileNameTextBox.Text)
            End If
            If FolderBrowserDialog.ShowDialog(Me) = DialogResult.OK Then
                If Not FolderBrowserDialog.SelectedPath.EndsWith(Path.DirectorySeparatorChar) Then
                    FolderBrowserDialog.SelectedPath = FolderBrowserDialog.SelectedPath + Path.DirectorySeparatorChar
                End If
                FileNameTextBox.Text = FolderBrowserDialog.SelectedPath
                ReportSettings.ExcelReport = Path.GetDirectoryName(FileNameTextBox.Text)
                Me.ValidateChildren()

                OverWriteFileName = True
            End If
        Else
            If Not String.IsNullOrEmpty(FileNameTextBox.Text) Then
                SaveFileDialog.FileName = IO.Path.GetFileName(FileNameTextBox.Text)
            End If
            If SaveFileDialog.ShowDialog = DialogResult.OK Then
                FileNameTextBox.Text = SaveFileDialog.FileName
                ReportSettings.ExcelReport = Path.GetDirectoryName(FileNameTextBox.Text)
                Me.ValidateChildren()

                OverWriteFileName = IO.File.Exists(SaveFileDialog.FileName)
            End If
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
