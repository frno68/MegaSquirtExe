Public Class clsMSCommand
    Private MSCommandByte As Byte()
    Public Sub New()

    End Sub
    Public ReadOnly Property T() As Byte()
        Get
            MSCommandByte = New Byte() {Asc("T")}
            Return MSCommandByte
        End Get
    End Property
    Public ReadOnly Property A() As Byte()
        Get
            MSCommandByte = New Byte() {Asc("A")}
            Return MSCommandByte
        End Get
    End Property
    Public ReadOnly Property GetRealTimeValues() As Byte()
        Get
            MSCommandByte = New Byte() {Asc("R")}
            Return MSCommandByte
        End Get
    End Property

    Public ReadOnly Property GetStaticValues() As Byte()
        Get
            MSCommandByte = New Byte() {Asc("P"), 1, Asc("V")}
            Return MSCommandByte
        End Get
    End Property

End Class
