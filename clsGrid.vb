Public Class clsGrid
    Inherits clsControl

    Private Property Pen As New Pen(Color.Blue, 6)
    Private Property Framepen As New Pen(Color.Black, 1)
    Private Property DotBrush As New SolidBrush(Color.Blue)

    Public Shadows Property Panel() As Panel
        Get
            Return MyBase.Panel
        End Get
        Set(value As Panel)
            MyBase.Panel = value

            Me.Height = MyBase.Panel.Height / MapSize
            Me.Width = MyBase.Panel.Width / MapSize
        End Set
    End Property

    Public Property WhatToDisplayInGrid() As WhatToDisplayInGrid
    Public Property Map() As Byte(,)
    Public Property Lambda() As Byte(,)
    Public Property SelectedCell() As clsCellPosition
    Public Property CurrentRpm() As Integer
    Public Property Rpm() As Byte()
    Public Property CurrentVacuum() As Byte
    Public Property Vacuum() As Byte()

    Public Shadows Sub Draw()

        If Map Is Nothing Or Lambda Is Nothing Then Exit Sub

        For row As Integer = 0 To MapSize - 1
            For col As Integer = 0 To MapSize - 1

                Me.MapBrush = New SolidBrush(retrieveColorForPosition(row, col, Map))
                Me.Lambdabrush = New SolidBrush(retrieveColorForPosition(row, col, Lambda))

                fillCell(row, col)
                frameCell(row, col)

                If col < MapSize - 1 And row < MapSize - 1 Then
                    If CurrentRpm >= Rpm(col) And CurrentRpm < Rpm(col + 1) And _
                    CurrentVacuum >= Vacuum(row) And CurrentVacuum < Vacuum(row + 1) Then
                        'This is the cell where the engine gets its values from right now
                        setDot(row, col)
                    End If
                End If

                If _SelectedCell.Row = row And _SelectedCell.Col = col Then
                    selectCell(row, col)
                End If

            Next
        Next

        MyBase.Draw()

    End Sub

    Private Sub fillCell(ByVal p_row As Integer, ByVal p_col As Integer)

        'Positioner angiven nerifrån enligt x- och y-axlar dvs {0,0} nere till vänster
        Select Case _WhatToDisplayInGrid
            Case Module1.WhatToDisplayInGrid.Lambda
                x = p_col * Me.Width
                y = Me.Panel.Height - p_row * Me.Height - Me.Height - 1
                Me.Graphics.FillRectangle(Me.Lambdabrush, x, y, Me.Width, Me.Height)
            Case Module1.WhatToDisplayInGrid.Map
                x = p_col * Me.Width
                y = Me.Panel.Height - p_row * Me.Height - Me.Height - 1
                Me.Graphics.FillRectangle(Me.MapBrush, x, y, Me.Width, Me.Height)
            Case Module1.WhatToDisplayInGrid.Map_Lambda
                Dim myPoints(2) As PointF
                'Rita triangel för Mapens värde i denna punkt
                'Denna triangel hamnar uppe till vänster
                'Pos 1: (0,0)
                x = p_col * Me.Width
                y = Me.Panel.Height - p_row * Me.Height - 1
                Dim myPoint1 As New PointF(x, y)
                myPoints(0) = myPoint1

                'Pos 2: (0,1)
                x = p_col * Me.Width
                y = Me.Panel.Height - p_row * Me.Height - Me.Height - 1
                Dim myPoint2 As New PointF(x, y)
                myPoints(1) = myPoint2

                'Pos 3: (1,1)
                x = p_col * Me.Width + Me.Width
                y = Me.Panel.Height - p_row * Me.Height - Me.Height - 1
                Dim myPoint3 As New PointF(x, y)
                myPoints(2) = myPoint3
                Me.Graphics.FillPolygon(Me.MapBrush, myPoints)

                'Rita triangel för Lambdavärdet i denna punkt
                'Denna triangel hamnar nere till höger
                'Pos 1: (0,0)
                x = p_col * Me.Width
                y = Me.Panel.Height - p_row * Me.Height - 1
                myPoint1 = New PointF(x, y)
                myPoints(0) = myPoint1

                'Pos 2: (1,0)
                x = p_col * Me.Width + Me.Width
                y = Me.Panel.Height - p_row * Me.Height - 1
                myPoint2 = New PointF(x, y)
                myPoints(1) = myPoint2

                'Pos 3: (1,1)
                x = p_col * Me.Width + Me.Width
                y = Me.Panel.Height - p_row * Me.Height - Me.Height - 1
                myPoint3 = New PointF(x, y)
                myPoints(2) = myPoint3

                Me.Graphics.FillPolygon(Me.Lambdabrush, myPoints)
        End Select

    End Sub

    Private Sub setDot(ByVal row As Integer, ByVal col As Integer)

        'Ram runt klickad ruta
        x = col * Me.Width + (Me.Width / 2 - 5)
        y = Me.Panel.Height - row * Me.Height - Me.Height + (Me.Height / 2 - 5)
        Me.Graphics.FillEllipse(_DotBrush, x, y, 20, 20)

    End Sub

    Private Sub selectCell(ByVal row As Integer, ByVal col As Integer)

        'Ram runt klickad ruta
        MyBase.x = col * Me.Width + Me.Pen.Width / 2
        y = Me.Panel.Height - row * Me.Height - Me.Height + Me.Pen.Width / 2 - 1
        Me.Graphics.DrawRectangle(Me.Pen, x, y, Me.Width - Me.Pen.Width, Me.Height - Me.Pen.Width)

    End Sub

    Private Sub frameCell(ByVal row As Integer, ByVal col As Integer)

        x = col * Me.Width
        y = Me.Panel.Height - row * Me.Height - Me.Height - 1
        Me.Graphics.DrawRectangle(Me.Framepen, x, y, Me.Width, Me.Height)

    End Sub

End Class
