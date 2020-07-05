Public Class clsVacuum_axis
    Inherits clsControl

    Public Property Vacuum() As Byte()

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

        If Vacuum Is Nothing Then Exit Sub

        Dim myBrush As SolidBrush = New SolidBrush(Color.White)
        Me.Graphics.FillRectangle(myBrush, 0, 0, Panel.Width, Panel.Height)

        Dim Pen As Pen = New Pen(Color.Black)
        Dim Font As New Font("Verdana", 7)
        Dim x1 As Single
        Dim y1 As Single
        Dim x2 As Single
        Dim y2 As Single
        Dim fontOffset = (Me.Panel.Height / MapSize) / 2 + Font.Size
        For index As Integer = 0 To Me.Vacuum.Length - 1
            x1 = 0
            y1 = Me.Panel.Height - index * Me.Height - 1
            x2 = Me.Panel.Width
            y2 = Me.Panel.Height - index * Me.Height - 1

            Me.Graphics.DrawLine(Pen, x1, y1, x2, y2)
            Me.Graphics.DrawString(Vacuum(index), Font, Brushes.Black, 0, y1 - fontOffset)
        Next

        MyBase.Draw()

    End Sub


End Class
