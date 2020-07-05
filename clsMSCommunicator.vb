Imports System.IO.Ports
Public Class clsMSCommunicator

#Region "Private Vars"

    'Lite viktiga konstanter
    Private _RANDOM As New Random

    Private Property MScommand As clsMSCommand = New clsMSCommand 'Håller kommandobeskrivningarna bara
    Private Property SerialPort As SerialPort

    Private A(21) As Byte  '22 lång byte array för kommandot "A"
    Private T(31) As Byte '32 lång byte array för kommandot "T"
#End Region

#Region "Public interface"

    Public Event DataAvailable(ByVal sender As Object, ByVal e As EventArgs)

    Public Sub New()

        Me.SerialPort = New SerialPort
        Me.SerialPort.BaudRate = 9600
        Me.SerialPort.PortName = "COM1"
        Me.SerialPort.Parity = Parity.None
        Me.SerialPort.DataBits = 8
        Me.SerialPort.StopBits = StopBits.One
        Try
            Me.SerialPort.Open()
            AddHandler _SerialPort.DataReceived, AddressOf SerialPort_DataReceived
        Catch ex As Exception
            'Detta ger applikationen möjlighet att fungera i sk offlineläge
        End Try

    End Sub

    Private _CommandInProgress As Boolean = False
    Public Property CommandInProgress() As Boolean
        Set(ByVal value As Boolean)
            _CommandInProgress = value
        End Set
        Get
            Return _CommandInProgress
        End Get
    End Property

    Private _CurrentCommand As Byte()
    Public ReadOnly Property CurrentCommand() As Byte()
        Get
            Return _CurrentCommand
        End Get
    End Property

    Private _Map(11, 11) As Byte 'Matris för att hålla alla mapvärden kommandot "P" "[1..3]" "V"
    Public ReadOnly Property Map() As Byte(,)
        Get
            Return _Map
        End Get
    End Property

    Private _Lambda(11, 11) As Byte 'Matris för att hålla alla mapvärden kommandot "P" "[1..3]" "V"
    Public ReadOnly Property Lambda() As Byte(,)
        Get
            Return _Lambda
        End Get
    End Property

    Private _LambdaAccumulated(11, 11, 19) As Byte 'Matris för att hålla alla mapvärden för en position ackumulerat
    Private _LambdaAverage(11, 11) As Byte 'Matris för att hålla alla mapvärden kommandot "P" "[1..3]" "V"
    Public ReadOnly Property LambdaAverage() As Byte(,)
        Get
            Return _LambdaAverage
        End Get
    End Property

    Private _Rpm(11) As Byte 'Vektor för att hålla rubrikerna för varje mätpunkt
    Public ReadOnly Property Rpm() As Byte()
        Get
            Return _Rpm
        End Get
    End Property

    Private _Vacuum(11) As Byte 'Vektor för att hålla rubrikerna för varje mätpunkt
    Public ReadOnly Property Vacuum() As Byte()
        Get
            Return _Vacuum
        End Get
    End Property

    Private _CurrentMapValue As Integer = 0
    Public ReadOnly Property CurrentMapValue
        Get
            Return _CurrentMapValue
        End Get
    End Property

    Private _CurrentLambdaValue = 0
    Public ReadOnly Property CurrentLambdaValue As Integer
        Get
            Return _CurrentLambdaValue
        End Get
    End Property

    Private _CurrentRPMValue As Integer = 0
    Public ReadOnly Property CurrentRPMValue As Integer
        Get
            Return _CurrentRPMValue
        End Get
    End Property

    Private _RealTimeValues(38) As Byte 'Array för kommandot "R"
    Public ReadOnly Property RealTimeValues() As Byte()
        Get
            Return _RealTimeValues
        End Get
    End Property

    Public Sub Send(ByVal arrCommand As Byte())

        If CommandInProgress Then Exit Sub 'Nåt annat kommando väntar på sitt svar. Ge fan

        CommandInProgress = True
        _CurrentCommand = arrCommand
        If _SerialPort.IsOpen Then
            If Not _SerialPort.IsOpen Then
                _SerialPort.Open()
            End If
            _SerialPort.DiscardInBuffer()
            _SerialPort.Write(arrCommand, 0, arrCommand.Length)
        Else
            'Klassen används nu i offline mode. Då behöver vi inte försöka skicka nåt kommando neråt
            'Vi kastar ett event som talar om för anroparen att det finns data att hämta på en gång
            fillArraysAndMatrixesWithRandomData()
            RaiseEvent DataAvailable(Me, EventArgs.Empty)
        End If

    End Sub

#End Region

    Private Sub fillArraysAndMatrixesWithRandomData()

        For row As Integer = 0 To _Lambda.GetLength(0) - 1
            For col As Integer = 0 To _Lambda.GetLength(1) - 1
                Me.Lambda(row, col) = _RANDOM.Next(0, 255)
                Me.LambdaAverage(row, col) = CalculateLambdaAverage(row, col)
            Next
        Next

        Me._CurrentMapValue = _RANDOM.Next(0, 255)
        Me._CurrentLambdaValue = _RANDOM.Next(0, 255)
        Me._CurrentRPMValue = _RANDOM.Next(0, 255)


    End Sub

    Private Sub SerialPort_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)

        Select Case True

            Case AreEqual(CurrentCommand, Me.MScommand.A)

                If Me.SerialPort.BytesToRead < A.Length Then Exit Sub
                Me.SerialPort.Read(A, 0, A.Length)

            Case AreEqual(CurrentCommand, Me.MScommand.T)

                If Me.SerialPort.BytesToRead < T.Length Then Exit Sub
                Me.SerialPort.Read(T, 0, T.Length)

            Case AreEqual(CurrentCommand, Me.MScommand.GetRealTimeValues)

                If Me.SerialPort.BytesToRead < Me.RealTimeValues.Length Then Exit Sub
                Me.SerialPort.Read(Me.RealTimeValues, 0, Me.RealTimeValues.Length)

                If Not Me.Rpm Is Nothing And Not Me.Vacuum Is Nothing Then 'Vi har inte hämtat nåt än ifall denna e tom

                    Me._CurrentMapValue = Me.RealTimeValues(4)
                    Me._CurrentRPMValue = Me.RealTimeValues(13)
                    Me._CurrentLambdaValue = Me.RealTimeValues(9)

                    For row As Integer = 0 To MapSize - 1
                        For col As Integer = 0 To MapSize - 1
                            If col < MapSize - 1 And row < MapSize - 1 Then
                                If Me.CurrentRPMValue >= Me.Rpm(col) And Me.CurrentRPMValue < Me.Rpm(col + 1) And _
                                 Me.CurrentMapValue >= Me.Vacuum(row) And Me.CurrentMapValue < Me.Vacuum(row + 1) Then
                                    'Lambda för denna position spänner över ett visst intervall, därav ifsatserna
                                    _Lambda(row, col) = Me.RealTimeValues(9)
                                    _LambdaAverage(row, col) = CalculateLambdaAverage(row, col)
                                End If
                            End If
                        Next
                    Next
                End If

            Case AreEqual(Me.CurrentCommand, Me.MScommand.GetStaticValues)

                Dim myMatrixSize As Integer = Me.Map.GetLength(0) * Me.Map.GetLength(1)
                If Me.SerialPort.BytesToRead < myMatrixSize Then Exit Sub
                Dim myByte(0) As Byte
                'Mapvärdena ligger på de 0-143 första positionerna
                For i As Integer = 0 To 11
                    For j As Integer = 0 To 11
                        Me.SerialPort.Read(myByte, 0, 1)
                        Me.Map(i, j) = myByte(0)
                    Next
                Next
                'Nu har vi läst fram och ska ta position 144
                'Lite odefinerat data fram till plats 157 ... fortsätt läsa fram till 157
                For i = 144 To 157
                    'Läs bort de första byten som är ointressanta
                    Me.SerialPort.Read(myByte, 0, 1)
                Next
                'X-axeln RPM 158 To 169
                For i As Integer = 0 To 11
                    Me.SerialPort.Read(myByte, 0, 1)
                    Me.Rpm(i) = myByte(0)
                Next
                'Y-axeln Vacuum 170 To 181
                For i As Integer = 0 To 11
                    Me.SerialPort.Read(myByte, 0, 1)
                    Me.Vacuum(i) = myByte(0)
                Next

        End Select

        RaiseEvent DataAvailable(Me, EventArgs.Empty)

    End Sub

    Private Function CalculateLambdaAverage(p_Row As Integer, p_Col As Integer) As Single

        'Dags att ackumulera lambdavärdena och ha lite logik för att undvika lagring av spikvärden
        Dim LambdaAccumulatedTotal As Double = 0
        Dim LambdaAccumulatedAverage As Double = 0
        'Dim LambdaAccumulatedIndex As Integer
        Dim positionForLowestValue = 0
        Dim positionForHighestValue = 0

        'Flytta alla värden ett snäpp framåt i arrayen
        For i = _LambdaAccumulated.GetUpperBound(2) To 1 Step -1
            _LambdaAccumulated(p_Row, p_Col, i) = _LambdaAccumulated(p_Row, p_Col, i - 1)
        Next
        'Stoppa in det nya värdet först i listan
        _LambdaAccumulated(p_Row, p_Col, 0) = Me.Lambda(p_Row, p_Col)

        For LambdaAccumulatedIndex = 0 To _LambdaAccumulated.GetUpperBound(2)
            LambdaAccumulatedTotal += _LambdaAccumulated(p_Row, p_Col, LambdaAccumulatedIndex)
        Next

        'Snittvärdet för alla lambdavärden i denna position
        Return LambdaAccumulatedTotal / (_LambdaAccumulated.GetUpperBound(2) + 1)

        ''Här försöker vi räkna ut snittet för den här positionens sparade lambdavärden
        'For LambdaAccumulatedIndex = 0 To _LambdaAccumulated.GetUpperBound(2)
        '    If _LambdaAccumulated(p_Row, p_Col, LambdaAccumulatedIndex) = 0 Then
        '        'Vi har inte fyllt hela den här arrayen så därför stoppar vi bara in värdet
        '        _LambdaAccumulated(p_Row, p_Col, LambdaAccumulatedIndex) = Me.Lambda(p_Row, p_Col)
        '        LambdaAccumulatedTotal = 0
        '        Exit For
        '    Else
        '        'Ta reda på positionen för det lägsta värdet
        '        If _LambdaAccumulated(p_Row, p_Col, LambdaAccumulatedIndex) < _LambdaAccumulated(p_Row, p_Col, positionForLowestValue) Then
        '            positionForLowestValue = LambdaAccumulatedIndex
        '        End If
        '        'Ta reda på positionen för det högsta värdet
        '        If _LambdaAccumulated(p_Row, p_Col, LambdaAccumulatedIndex) > _LambdaAccumulated(p_Row, p_Col, positionForHighestValue) Then
        '            positionForHighestValue = LambdaAccumulatedIndex
        '        End If
        '        LambdaAccumulatedTotal += _LambdaAccumulated(p_Row, p_Col, LambdaAccumulatedIndex)
        '    End If
        'Next
        'If LambdaAccumulatedTotal <> 0 Then
        'Snittvärdet för alla lambdavärden i denna position
        'LambdaAccumulatedAverage = LambdaAccumulatedTotal / (_LambdaAccumulated.GetUpperBound(2) + 1)

        'Om värdet vi kommer in med är större än medelvärdet och mindre än maxvärdet i arrayen så byter vi ut värdet på maxpositionen
        'På detta sättet kommer snittvärdet i denna punkten att självjustera sig över tiden
        'Vi gör också samma sak med minvärdet
        'If Me.Lambda(p_Row, p_Col) > LambdaAccumulatedAverage And Me.Lambda(p_Row, p_Col) < _LambdaAccumulated(p_Row, p_Col, positionForHighestValue) Then
        '    _LambdaAccumulated(p_Row, p_Col, positionForHighestValue) = Me.Lambda(p_Row, p_Col)
        'ElseIf Me.Lambda(p_Row, p_Col) < LambdaAccumulatedAverage And Me.Lambda(p_Row, p_Col) > _LambdaAccumulated(p_Row, p_Col, positionForLowestValue) Then
        '    _LambdaAccumulated(p_Row, p_Col, positionForLowestValue) = Me.Lambda(p_Row, p_Col)
        'End If

        'Return LambdaAccumulatedAverage

        'Else

        'Return Me.Lambda(p_Row, p_Col)

        'End If

    End Function

End Class
