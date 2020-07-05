Public Class clsCurrent
    Inherits clsControl

    Private Property Pen As Pen = New Pen(Color.Black, 1)
    Private Property MapHeight As Single
    Private Property LambdaHeight As Single

    Public Shadows Property Panel() As Panel
        Get
            Return MyBase.Panel
        End Get
        Set(ByVal value As Panel)
            MyBase.Panel = value

            'Me.Width = MyBase.Panel.Width / 2
            Me.Width = MyBase.Panel.Width
            Me.Height = -1 'not important in this context
        End Set
    End Property
    Public Property Map() As Byte(,)
    Public Property Lambda() As Byte(,)
    Public Property SelectedCell() As clsCellPosition = New clsCellPosition

    Public Shadows Sub Draw()

        If Me.Map Is Nothing Or Me.Lambda Is Nothing Then Exit Sub

        'Clear the area from earlier bars
        Me.Graphics.FillRectangle(New SolidBrush(System.Drawing.ColorTranslator.FromOle(RGB(180, 180, 180))), 0, 0, Me.Panel.Width, Me.Panel.Height)

        Me.MapHeight = CInt((Me.Panel.Height) * (Me.Map(SelectedCell.Row, SelectedCell.Col) / 255)) 'Procentuell höjd och kompensation så att ramen syns
        'Me.LambdaHeight = CInt((Me.Panel.Height) * (Me.Lambda(SelectedCell.Row, SelectedCell.Col) / 255)) 'Procentuell höjd och kompensation så att ramen syns

        Me.MapBrush = New SolidBrush(retrieveColorForPosition(SelectedCell.Row, SelectedCell.Col, Me.Map))
        'Me.Lambdabrush = New SolidBrush(retrieveColorForPosition(SelectedCell.Row, SelectedCell.Col, Me.Lambda))

        x = 0
        y = Me.Panel.Height - Me.MapHeight - 2
        Me.Graphics.FillRectangle(Me.MapBrush, x, y, Me.Width, Me.MapHeight)
        Me.Graphics.DrawRectangle(Me.Pen, x, y, Me.Width - Me.Pen.Width, Me.MapHeight - Me.Pen.Width)

        'x = CInt(Me.Panel.Width / 2)
        'y = Me.Panel.Height - Me.LambdaHeight - 2
        'Me.Graphics.FillRectangle(Me.Lambdabrush, x, y, Me.Width, Me.LambdaHeight)
        'Me.Graphics.DrawRectangle(Me.Pen, x, y, Me.Width, Me.LambdaHeight)

        MyBase.Draw()

    End Sub

End Class
