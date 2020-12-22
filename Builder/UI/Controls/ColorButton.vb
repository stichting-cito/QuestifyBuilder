Imports System.ComponentModel
Imports Questify.Builder.UI.Controls.Logic


Namespace Controls
    Public Class ColorButton
        Inherits Button

        Private isPressed As Boolean
        Private acceptClick As Boolean
        Private m_selectedColor As Color
        Private colorSelector As ColorSelector
        Private dropDown As ToolStripDropDown
        Private lastCloseTime As DateTime
        Private lastOpenColor As Color

        Public Event SelectedColorChanged As EventHandler

        Public Sub New()
            MyBase.Text = ""
            m_selectedColor = Color.Black

            PrepareColorSelector()
        End Sub

        <Category("Appearance")> _
        <DefaultValue(GetType(Color), "Black")> _
        Public Property SelectedColor() As Color
            Get
                Return m_selectedColor
            End Get
            Set(value As Color)
                If value <> m_selectedColor Then
                    m_selectedColor = value
                    Invalidate()
                    OnSelectedColorChanged()
                End If
            End Set
        End Property


        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property Text() As String
            Get
                Return MyBase.Text
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property TextAlign() As System.Drawing.ContentAlignment
            Get
                Return MyBase.TextAlign
            End Get
            Set(value As System.Drawing.ContentAlignment)
                MyBase.TextAlign = value
            End Set
        End Property

        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property Font() As Font
            Get
                Return MyBase.Font
            End Get
            Set(value As Font)
                MyBase.Font = value
            End Set
        End Property

        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Overrides Property ForeColor() As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(value As Color)
                MyBase.ForeColor = value
            End Set
        End Property

        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property Image() As Image
            Get
                Return MyBase.Image
            End Get
            Set(value As Image)
                MyBase.Image = value
            End Set
        End Property
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property ImageAlign() As System.Drawing.ContentAlignment
            Get
                Return MyBase.ImageAlign
            End Get
            Set(value As System.Drawing.ContentAlignment)
                MyBase.ImageAlign = value
            End Set
        End Property
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property ImageIndex() As Integer
            Get
                Return MyBase.ImageIndex
            End Get
            Set(value As Integer)
                MyBase.ImageIndex = value
            End Set
        End Property
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property ImageKey() As String
            Get
                Return MyBase.ImageKey
            End Get
            Set(value As String)
                MyBase.ImageKey = value
            End Set
        End Property
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property ImageList() As ImageList
            Get
                Return MyBase.ImageList
            End Get
            Set(value As ImageList)
                MyBase.ImageList = value
            End Set
        End Property
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property RightToLeft() As RightToLeft
            Get
                Return MyBase.RightToLeft
            End Get
            Set(value As RightToLeft)
                MyBase.RightToLeft = value
            End Set
        End Property
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property TextImageRelation() As TextImageRelation
            Get
                Return MyBase.TextImageRelation
            End Get
            Set(value As TextImageRelation)
                MyBase.TextImageRelation = value
            End Set
        End Property
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property UseMnemonic() As Boolean
            Get
                Return MyBase.UseMnemonic
            End Get
            Set(value As Boolean)
                MyBase.UseMnemonic = value
            End Set
        End Property
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property AutoEllipsis() As Boolean
            Get
                Return MyBase.AutoEllipsis
            End Get
            Set(value As Boolean)
                MyBase.AutoEllipsis = value
            End Set
        End Property
        <Browsable(False)> _
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
        Public Shadows Property UseCompatibleTextRendering() As Boolean
            Get
                Return MyBase.UseCompatibleTextRendering
            End Get
            Set(value As Boolean)
                MyBase.UseCompatibleTextRendering = value
            End Set
        End Property



        Protected Overrides Sub OnFontChanged(e As EventArgs)
            MyBase.OnFontChanged(e)
            colorSelector.Font = Font
        End Sub

        Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
            MyBase.OnPaint(pevent)

            Dim colorRect As Rectangle = ClientRectangle

            If isPressed AndAlso Not Application.RenderWithVisualStyles Then
                colorRect.X += 1
                colorRect.Y += 1
            End If
            colorRect.X += 8
            colorRect.Y += 5
            colorRect.Width -= 16
            colorRect.Height -= 10
            Using b As Brush = New SolidBrush(m_selectedColor)
                pevent.Graphics.FillRectangle(b, colorRect)
            End Using
            Using p As New Pen(SystemColors.ControlDark)
                colorRect.Width -= 1
                colorRect.Height -= 1
                pevent.Graphics.DrawRectangle(p, colorRect)
            End Using
        End Sub

        Protected Overrides Sub OnMouseDown(mevent As MouseEventArgs)
            MyBase.OnMouseDown(mevent)

            If mevent.Button = MouseButtons.Left Then
                isPressed = True
                Invalidate()

                acceptClick = (DateTime.UtcNow - lastCloseTime).TotalMilliseconds > 100
            End If
        End Sub

        Protected Overrides Sub OnMouseUp(mevent As MouseEventArgs)
            If mevent.Button = MouseButtons.Left Then
                isPressed = False
                Invalidate()
            End If

            MyBase.OnMouseUp(mevent)
        End Sub

        Protected Overrides Sub OnClick(e As EventArgs)
            MyBase.OnClick(e)


            If acceptClick Then
                If ColorMath.RgbToHsl(SystemColors.Control).L > 215 Then
                    colorSelector.BackColor = SystemColors.Control
                Else
                    colorSelector.BackColor = ColorMath.Blend(SystemColors.Control, SystemColors.Window, 0.6)
                End If

                colorSelector.SelectedColor = m_selectedColor
                lastOpenColor = m_selectedColor
                dropDown.Show(Me, New Point(0, Height))
            End If
        End Sub

        Private Sub colorSelector_SelectedColorChanged(sender As Object, e As EventArgs)
            SelectedColor = colorSelector.SelectedColor
        End Sub

        Private Sub colorSelector_CloseRequested(sender As Object, e As EventArgs)
            If dropDown IsNot Nothing AndAlso Not dropDown.IsDisposed AndAlso dropDown.Visible Then
                dropDown.Close()
            End If
        End Sub

        Private Sub colorSelector_CancelRequested(sender As Object, e As EventArgs)
            If dropDown IsNot Nothing AndAlso Not dropDown.IsDisposed AndAlso dropDown.Visible Then
                SelectedColor = lastOpenColor
                dropDown.Close()
            End If
        End Sub

        Private Sub dropDown_Closed(sender As Object, e As ToolStripDropDownClosedEventArgs)
            lastCloseTime = DateTime.UtcNow
        End Sub

        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing Then
                If dropDown IsNot Nothing Then
                    dropDown.Dispose()
                    dropDown = Nothing
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub PrepareColorSelector()
            colorSelector = New ColorSelector()
            colorSelector.Padding = New Padding(6)
            colorSelector.Font = Font
            AddHandler colorSelector.SelectedColorChanged, AddressOf colorSelector_SelectedColorChanged
            AddHandler colorSelector.CloseRequested, AddressOf colorSelector_CloseRequested
            AddHandler colorSelector.CancelRequested, AddressOf colorSelector_CancelRequested

            Dim controlHost As New ToolStripControlHost(colorSelector)
            controlHost.Margin = New Padding(1)
            dropDown = New ToolStripDropDown()
            dropDown.Padding = Padding.Empty
            dropDown.Items.Add(controlHost)
            AddHandler dropDown.Closed, AddressOf dropDown_Closed
        End Sub

        Protected Sub OnSelectedColorChanged()
            RaiseEvent SelectedColorChanged(Me, EventArgs.Empty)
        End Sub
    End Class

End Namespace