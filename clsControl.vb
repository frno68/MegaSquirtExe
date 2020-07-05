Public MustInherit Class clsControl

    Protected Property x As Single
    Protected Property y As Single 'Declaration here for performance reasons
    Protected Property Graphics As Graphics 'Object to draw to
    Protected Property Handle As Graphics 'Create Handle For The Picture Box Graphics
    Protected Property Bitmap As Bitmap 'Image to be placed in the panel and which we are drawing on
    Protected Property MapBrush As SolidBrush
    Protected Property Lambdabrush As SolidBrush
    Protected Property Width As Single
    Protected Property Height As Single

    Private _Panel As Panel
    Public Property Panel() As Panel
        Get
            Return _Panel
        End Get
        Set(ByVal value As Panel)

            _Panel = value

            Me.Handle = _Panel.CreateGraphics 'Create Handle For The Picture Box Graphics
            Me.Handle.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

            Me.Bitmap = New Bitmap(_Panel.Width, _Panel.Height)
            Me.Graphics = Graphics.FromImage(Me.Bitmap)
            Me.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        End Set
    End Property

    Protected Sub Draw()

        Me.Handle.DrawImage(Me.Bitmap, New Point(0, 0))

    End Sub

End Class
