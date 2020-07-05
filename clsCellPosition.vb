Public Class clsCellPosition

    Dim _Row As Integer
    Dim _Col As Integer

    Public Property Row() As Integer
        Get
            Return _Row
        End Get
        Set(ByVal value As Integer)
            _Row = value
        End Set
    End Property

    Public Property Col() As Integer
        Get
            Return _Col
        End Get
        Set(ByVal value As Integer)
            _Col = value
        End Set
    End Property

    Public Sub New()
        _Row = 0
        _Col = 0
    End Sub
End Class
