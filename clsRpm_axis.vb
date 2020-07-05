Public Class clsRpm_axis
    Inherits clsControl

    Public Property Rpm() As Byte()

    Public Shadows Property Panel() As Panel
        Get
            Return MyBase.Panel
        End Get
        Set(ByVal value As Panel)
            MyBase.Panel = value

            Me.Height = Me.Panel.Height / MapSize
            Me.Width = Me.Panel.Width / MapSize
            Me.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit
        End Set
    End Property

    Public Shadows Sub Draw()

        If Rpm Is Nothing Then Exit Sub

        Dim myBrush As SolidBrush = New SolidBrush(Color.White)
        Me.Graphics.FillRectangle(myBrush, 0, 0, Panel.Width, Panel.Height)

        Dim Pen As Pen = New Pen(Color.Black)
        Dim Font As New Font("Verdana", 7)
        Dim x1 As Single
        Dim y1 As Single
        Dim x2 As Single
        Dim y2 As Single
        Dim fontOffset = Me.Width / 2 - Font.Size
        Dim RPMValue As Integer = 0
        For index As Integer = 0 To Rpm.Length - 1
            x1 = index * Me.Width
            y1 = 0
            x2 = index * Me.Width
            y2 = Me.Panel.Height

            Me.Graphics.DrawLine(Pen, x1, y1, x2, y2)

            Dim drawFormat As New System.Drawing.StringFormat
            drawFormat.FormatFlags = StringFormatFlags.DirectionVertical
            RPMValue = Me.Rpm(index) * 100
            Me.Graphics.DrawString(RPMValue, Font, Brushes.Black, x1 + fontOffset, 0, drawFormat)
        Next

        MyBase.Draw()

    End Sub

End Class
