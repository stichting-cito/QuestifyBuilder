Public Class EditImagePropertiesDialog

    Private _currentImageWidth As Integer
    Private _currentImageHeight As Integer
    Private _currentImageSizePercentage As Double

    Private _origImageWidth As Integer
    Private _origImageHeight As Integer

    Public WriteOnly Property OrigImageWidth() As Integer
        Set(ByVal value As Integer)
            _origImageWidth = value
        End Set
    End Property

    Public WriteOnly Property OrigImageHeight() As Integer
        Set(ByVal value As Integer)
            _origImageHeight = value
        End Set
    End Property

    Private Property ImageSizePercentage() As Double
        Get
            Return _currentImageSizePercentage
        End Get
        Set(ByVal value As Double)
            _currentImageSizePercentage = value
            NumericUpDownPerc.Value = Convert.ToDecimal(Math.Round(_currentImageSizePercentage, 1))
        End Set
    End Property

    Public Property ImageWidth() As Integer
        Get
            Return Convert.ToInt32(NumericUpDownWidth.Value)
        End Get
        Set(ByVal value As Integer)
            _currentImageWidth = value
            NumericUpDownWidth.Value = _currentImageWidth
            LabelWidthInCm.Text = String.Format("({0} cm)", PixelsToCm(_currentImageWidth, True))
        End Set
    End Property

    Public Property ImageHeight() As Integer
        Get
            Return Convert.ToInt32(NumericUpDownHeight.Value)
        End Get
        Set(ByVal value As Integer)
            _currentImageHeight = value
            NumericUpDownHeight.Value = _currentImageHeight
            LabelHeightInCm.Text = String.Format("({0} cm)", PixelsToCm(_currentImageHeight, False))
        End Set
    End Property

    Public Property AlternativeText() As String
        Get
            Return TextBoxAlternativeText.Text
        End Get
        Set(ByVal value As String)
            TextBoxAlternativeText.Text = value
        End Set
    End Property

    Public Property ApplyBorder() As Boolean
        Get
            Return CheckBoxApplyBorder.Checked
        End Get
        Set(ByVal value As Boolean)
            CheckBoxApplyBorder.Checked = value
        End Set
    End Property

    Private Function PixelsToCm(ByVal sizeInPixels As Integer, ByVal inXDirection As Boolean) As Double
        Dim SizeInCm As Double

        Const MillimetersPerInch As Single = 25.4
        Dim g As Graphics = Me.CreateGraphics()
        If inXDirection Then
            SizeInCm = Math.Round((sizeInPixels / g.DpiX * MillimetersPerInch) / 10, 2)
        Else
            SizeInCm = Math.Round((sizeInPixels / g.DpiY * MillimetersPerInch) / 10, 2)
        End If
        g.Dispose()

        Return SizeInCm
    End Function

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DerivePercentageAndWidthAndHeight(ByVal changedPercentage As Double, ByVal changedWidth As Integer, ByVal changedHeight As Integer)
        If _origImageWidth <= 0 OrElse _origImageHeight <= 0 Then Exit Sub

        Dim newWidth As Integer = 0
        Dim newHeight As Integer = 0
        Dim newPercentage As Double = 0
        Dim AnyChange As Boolean = False

        If changedPercentage > -1 Then
            If _currentImageSizePercentage <> changedPercentage Then
                AnyChange = True
                newPercentage = changedPercentage
                newWidth = Convert.ToInt32(_origImageWidth * newPercentage / 100)
                newHeight = Convert.ToInt32(_origImageHeight * newPercentage / 100)
            End If
        ElseIf changedWidth > -1 Then
            If _currentImageWidth <> changedWidth Then
                AnyChange = True
                newPercentage = (changedWidth / (_origImageWidth / 100))
                newWidth = changedWidth
                newHeight = Convert.ToInt32(_origImageHeight * newPercentage / 100)
            End If
        ElseIf changedHeight > -1 Then
            If _currentImageHeight <> changedHeight Then
                AnyChange = True
                newPercentage = Convert.ToInt32(changedHeight / (_origImageHeight / 100))
                newWidth = Convert.ToInt32(_origImageWidth * newPercentage / 100)
                newHeight = changedHeight
            End If
        End If

        If AnyChange Then
            ImageSizePercentage = newPercentage
            ImageWidth = newWidth
            ImageHeight = newHeight

            Dim newRatio As Double = Math.Round(newWidth / newHeight, 2)
            displayCurrentRatio(newRatio)
        End If
    End Sub

    Private Sub displayCurrentRatio(ByVal ratio As Double)
        LabelCurrent.Text = ratio.ToString()
        If ratio <> Math.Round(_origImageWidth / _origImageHeight, 2) Then
            LabelCurrent.ForeColor = Color.Red
        Else : LabelCurrent.ForeColor = Color.Black
        End If
    End Sub

    Private Sub EditImagePropertiesDialog_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dim percentage As Double = ImageWidth / (_origImageWidth / 100)

        ImageSizePercentage = percentage

        LabelOrg.Text = Math.Round(_origImageWidth / _origImageHeight, 2).ToString()
        displayCurrentRatio(Math.Round(_currentImageWidth / _currentImageHeight, 2))
    End Sub

    Private Sub ButtonReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonReset.Click
        ImageWidth = _origImageWidth
        ImageHeight = _origImageHeight
        ImageSizePercentage = 100
    End Sub

    Private Sub NumericUpDownPerc_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDownPerc.Validated
        Dim NewPercentage As Double = 0
        Try
            NewPercentage = Convert.ToDecimal(NumericUpDownPerc.Value)
        Catch fe As FormatException
            Return
        End Try
        DerivePercentageAndWidthAndHeight(NewPercentage, -1, -1)
    End Sub

    Private Sub NumericUpDownHeight_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDownHeight.Validated
        Dim newHeight As Integer = 0
        Try
            newHeight = Convert.ToInt32(NumericUpDownHeight.Value)
        Catch fe As FormatException
            Return
        End Try
        DerivePercentageAndWidthAndHeight(-1, -1, newHeight)
    End Sub

    Private Sub NumericUpDownWidth_Validated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDownWidth.Validated
        Dim newWidth As Integer = 0
        Try
            newWidth = Convert.ToInt32(NumericUpDownWidth.Value)
        Catch fe As FormatException
            Return
        End Try
        DerivePercentageAndWidthAndHeight(-1, newWidth, -1)
    End Sub

    Private Sub NumericUpDownHeight_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDownHeight.ValueChanged
        Dim newHeight As Integer = 0
        Try
            newHeight = Convert.ToInt32(NumericUpDownHeight.Value)
        Catch fe As FormatException
            Return
        End Try
        DerivePercentageAndWidthAndHeight(-1, -1, newHeight)
    End Sub

    Private Sub NumericUpDownWidth_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDownWidth.ValueChanged
        Dim newWidth As Integer = 0
        Try
            newWidth = Convert.ToInt32(NumericUpDownWidth.Value)
        Catch fe As FormatException
            Return
        End Try
        DerivePercentageAndWidthAndHeight(-1, newWidth, -1)
    End Sub

    Private Sub NumericUpDownPerc_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDownPerc.ValueChanged
        Dim NewPercentage As Double = 0
        Try
            NewPercentage = Convert.ToDecimal(NumericUpDownPerc.Value)
        Catch fe As FormatException
            Return
        End Try
        DerivePercentageAndWidthAndHeight(NewPercentage, -1, -1)
    End Sub
End Class
