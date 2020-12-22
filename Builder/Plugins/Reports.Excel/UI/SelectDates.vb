
Imports System.Windows.Forms

Public Class SelectDates

    Dim _optionValidator As OptionValidatorResourceExposureLog

    Public Sub New(optionValidator As OptionValidatorResourceExposureLog)

        InitializeComponent()

        _optionValidator = optionValidator

    End Sub


    Private Sub FromDatePicker_ValueChanged(sender As Object, e As EventArgs) Handles FromDatePicker.ValueChanged
        _optionValidator.FromDate = DirectCast(sender, DateTimePicker).Value
        ValidateDates(DirectCast(sender, Control))
    End Sub

    Private Sub ToDatePicker_ValueChanged(sender As Object, e As EventArgs) Handles ToDatePicker.ValueChanged
        _optionValidator.ToDate = DirectCast(sender, DateTimePicker).Value
        ValidateDates(DirectCast(sender, Control))

    End Sub

    Private Sub ValidateDates(picker As Control)
        ErrorProvider1.Clear()
        If Not _optionValidator.ToDate >= _optionValidator.FromDate Then
            ErrorProvider1.SetError(picker, My.Resources.OptionValidatorResourceExposureLog_The__date_from__is_not_earlier_than__date_to_)
        End If
    End Sub

    Private Sub SelectDates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.FromDateLabel.Text = My.Resources.SelectDates_FromDateLabelText
        Me.ToDateLabel.Text = My.Resources.SelectDates_ToDateLabelText
    End Sub
End Class
