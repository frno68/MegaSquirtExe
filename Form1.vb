Imports System.Drawing
Imports System.Text
Imports System.Xml
Public Class Form1

    Dim _RANDOM As New Random

    Dim RealTimeValues() As Byte = Nothing

    'Visningsklasser
    Dim Grid As New clsGrid
    Dim Current As New clsCurrent
    Dim Vacuum_Axis As New clsVacuum_axis
    Dim Rpm_Axis As New clsRpm_axis
    'Kommunikationsklass
    Dim MSCommunicator As New clsMSCommunicator

    'Håller kommandobeskrivningarna bara
    Dim MSCommand As New clsMSCommand

    'Räknare för att avgöra hur länge ctrl-knappen varit nertryckt för att kunna dubbla hastigheten
    Dim myCtrlButtonCounter As Integer
#Region "Form Events"

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        myCtrlButtonCounter = 0
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim myNewValue As Integer
        Dim myCellPostion As New clsCellPosition

        If e.Control Then
            myCtrlButtonCounter = myCtrlButtonCounter + 1
        End If
        TextBox1.Text = myCtrlButtonCounter

        'Detta är för att möjliggöra snabbare ändringar om man håller nere CTRL-tangenten
        Dim myCounterIncrease As Integer
        If myCtrlButtonCounter > 10 Then myCounterIncrease = 3 Else myCounterIncrease = 1

        myCellPostion = Grid.SelectedCell
        Select Case e.KeyValue
            Case 37
                'Move to the left
                myNewValue = myCellPostion.Col - 1
                myCellPostion.Col = IIf(myCellPostion.Col <= 0, 0, myNewValue)
            Case 38
                If e.Control Then
                    'Increase value
                    myNewValue = Grid.Map(Current.SelectedCell.Row, Current.SelectedCell.Col) + myCounterIncrease
                    Grid.Map(Current.SelectedCell.Row, Current.SelectedCell.Col) = IIf(myNewValue > 254, 255, myNewValue)
                Else
                    'Move up
                    myNewValue = myCellPostion.Row + 1
                    myCellPostion.Row = IIf(myCellPostion.Row >= MapSize - 1, MapSize - 1, myNewValue)
                End If
            Case 39
                'Move to the right
                myNewValue = myCellPostion.Col + 1
                myCellPostion.Col = IIf(myCellPostion.Col >= MapSize - 1, MapSize - 1, myNewValue)
            Case 40
                If e.Control Then
                    'Decrease value
                    myNewValue = Grid.Map(Current.SelectedCell.Row, Current.SelectedCell.Col) - myCounterIncrease
                    Grid.Map(Current.SelectedCell.Row, Current.SelectedCell.Col) = IIf(myNewValue < 2, 1, myNewValue)
                Else
                    'Move down
                    myNewValue = myCellPostion.Row - 1
                    myCellPostion.Row = IIf(myCellPostion.Row <= 0, 0, myNewValue)
                End If
        End Select
        Grid.SelectedCell = myCellPostion
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Grid.Panel = pnl_Map_and_Lambda 'Knyt griddens displaypanel 
        Current.Panel = pnl_Map_and_Lambda_detail 'Knyt stapelkontrollens displaypanel 
        Vacuum_Axis.Panel = pnl_VACUUM_Axis 'Knyt vacuumaxelns displaypanel 
        Rpm_Axis.Panel = pnl_RPM_Axis 'Knyt varvtalsaxelns displaypanel 

        Dim myCellPostion As New clsCellPosition
        myCellPostion.Row = 0
        myCellPostion.Col = 0
        Grid.SelectedCell = myCellPostion
        Current.SelectedCell = myCellPostion

        'Koppla händelsen till då dataåterkommer från MS
        AddHandler MSCommunicator.DataAvailable, AddressOf mySerialPort_DataAvailable

        AddHandler LambdaToolStripMenuItem.Click, AddressOf MapLambaMenu_Click
        AddHandler MapToolStripMenuItem.Click, AddressOf MapLambaMenu_Click

        'Timer som bara körs under uppstarten av formuläret
        Timer_StartUp.Enabled = True

        'Dra igång hämtning av livedata
        Timer_GetRealTimeValues.Enabled = True

        'Dra igång uppdatering av formuläret
        Timer_FormUpdate.Enabled = True

    End Sub

#End Region

#Region "Timers and related stuff"

    'Timers används för att formuläret är händelsestyrt och datakommunikationen är asynkron.
    'Dvs vi vet inte när datat är färdighämtat från MS så uppdatering av formuläret och skickande av kommandon sker med timers
    ' för att dessa skall kunna evekvera i egna trådar och man kan jobba med forumläret under tiden

    Private Sub Timer_StartUp_Tick(sender As Object, e As System.EventArgs) Handles Timer_StartUp.Tick

        MSCommunicator.Send(MSCommand.GetStaticValues)

    End Sub

    Private Sub Timer_GetRealTimeValues_Tick(sender As Object, e As System.EventArgs) Handles Timer_GetRealTimeValues.Tick

        MSCommunicator.Send(MSCommand.GetRealTimeValues)

    End Sub

    Private Sub Timer_FormUpdate_Tick(sender As Object, e As System.EventArgs) Handles Timer_FormUpdate.Tick

        Select Case True
            Case MapToolStripMenuItem.Checked And LambdaToolStripMenuItem.Checked
                Grid.WhatToDisplayInGrid = WhatToDisplayInGrid.Map_Lambda
            Case MapToolStripMenuItem.Checked
                Grid.WhatToDisplayInGrid = WhatToDisplayInGrid.Map
            Case LambdaToolStripMenuItem.Checked
                Grid.WhatToDisplayInGrid = WhatToDisplayInGrid.Lambda
            Case Else
                Grid.WhatToDisplayInGrid = WhatToDisplayInGrid.Map_Lambda
        End Select

        Grid.Draw()
        Current.Draw()
        Rpm_Axis.Draw()
        Vacuum_Axis.Draw()
        txtRow.Text = Current.SelectedCell.Row
        txtCol.Text = Current.SelectedCell.Col

    End Sub

#End Region

    Private Sub mySerialPort_DataAvailable(ByVal sender As Object, ByVal e As EventArgs)
        Select Case True
            Case AreEqual(MSCommunicator.CurrentCommand, MSCommand.GetStaticValues)

                Grid.Map = MSCommunicator.Map
                Rpm_Axis.RPM = MSCommunicator.Rpm
                Vacuum_Axis.Vacuum = MSCommunicator.Vacuum

                Timer_StartUp.Enabled = False

            Case AreEqual(MSCommunicator.CurrentCommand, MSCommand.GetRealTimeValues)

                RealTimeValues = MSCommunicator.RealTimeValues

                Current.Map = MSCommunicator.Map
                Current.Lambda = MSCommunicator.Lambda

                Grid.Rpm = MSCommunicator.Rpm
                Grid.Vacuum = MSCommunicator.Vacuum
                Grid.CurrentRpm = MSCommunicator.CurrentRPMValue
                Grid.CurrentVacuum = MSCommunicator.CurrentMapValue
                'Grid.Lambda = MSCommunicator.Lambda
                Grid.Lambda = MSCommunicator.LambdaAverage

                txtMapValue.Text = MSCommunicator.CurrentMapValue
                txtLambdaValue.Text = MSCommunicator.CurrentLambdaValue
                txtRawData.Text = createRawData(RealTimeValues)

                LambdaGauge.Value = (10 + MSCommunicator.CurrentMapValue * 0.039216)
                'LambdaGauge.BackColor = System.Drawing.ColorTranslator.FromOle( _
                'RGB(_Color(MSCommunicator.CurrentMapValue, cnstRED), _Color(MSCommunicator.CurrentMapValue, cnstGreen), _Color(MSCommunicator.CurrentMapValue, cnstBlue)))

        End Select

        MSCommunicator.CommandInProgress = False
    End Sub

    Private Sub Panel1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

        Dim myPanel As Panel = sender
        Dim mySize As Integer = Int(IIf(myPanel.Width < myPanel.Height, myPanel.Width, myPanel.Height) / MapSize)

        Dim myCellPostion As New clsCellPosition
        myCellPostion.Row = MapSize - 1 - Int(e.Y / mySize) '0:an ligger längst upp när man ritar därför omvänd position
        myCellPostion.Col = Int(e.X / mySize)
        myCellPostion.Row = IIf(myCellPostion.Row > 11, 11, myCellPostion.Row) 'To make sure we are not outside the matrix
        myCellPostion.Col = IIf(myCellPostion.Col > 11, 11, myCellPostion.Col) 'To make sure we are not outside the matrix

        Grid.SelectedCell = myCellPostion
        Current.SelectedCell = myCellPostion


    End Sub

    Private Sub updwn_Timer1_Ratio_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim myNumericUpDown As NumericUpDown = sender
        Timer_GetRealTimeValues.Interval = myNumericUpDown.Value

    End Sub

    Private Function createRawData(ByVal myresultArray() As Byte) As String

        Dim strRawData As String = ""
        strRawData = strRawData & "secl:" & myresultArray(0).ToString & vbCrLf
        strRawData = strRawData & "squirt:" & myresultArray(1).ToString & vbCrLf
        strRawData = strRawData & "engine:" & myresultArray(2).ToString & vbCrLf
        strRawData = strRawData & "baroADC:" & myresultArray(3).ToString & vbCrLf
        strRawData = strRawData & "mapADC:" & myresultArray(4).ToString & vbCrLf
        strRawData = strRawData & "matADC:" & myresultArray(5).ToString & vbCrLf
        strRawData = strRawData & "cltADC:" & myresultArray(6).ToString & vbCrLf
        strRawData = strRawData & "tpsADC:" & myresultArray(7).ToString & vbCrLf
        strRawData = strRawData & "batADC:" & myresultArray(8).ToString & vbCrLf
        strRawData = strRawData & "egoADC:" & myresultArray(9).ToString & vbCrLf
        strRawData = strRawData & "egoCorrection:" & myresultArray(10).ToString & vbCrLf
        strRawData = strRawData & "airCorrection:" & myresultArray(11).ToString & vbCrLf
        strRawData = strRawData & "warmupEnrich:" & myresultArray(12).ToString & vbCrLf
        strRawData = strRawData & "rpm100:" & myresultArray(13).ToString & vbCrLf
        strRawData = strRawData & "pulsewidth1:" & myresultArray(14).ToString & vbCrLf
        strRawData = strRawData & "accelEnrich:" & myresultArray(15).ToString & vbCrLf
        strRawData = strRawData & "baroCorrection:" & myresultArray(16).ToString & vbCrLf
        strRawData = strRawData & "gammaEnrich:" & myresultArray(17).ToString & vbCrLf
        strRawData = strRawData & "veCurr1:" & myresultArray(18).ToString & vbCrLf
        strRawData = strRawData & "pulsewidth2:" & myresultArray(19).ToString & vbCrLf
        strRawData = strRawData & "veCurr2:" & myresultArray(20).ToString & vbCrLf
        strRawData = strRawData & "idleDC:" & myresultArray(21).ToString & vbCrLf
        strRawData = strRawData & "iTime* (Middle byte):" & myresultArray(22).ToString & vbCrLf
        strRawData = strRawData & "iTime* (Lower byte):" & myresultArray(23).ToString & vbCrLf
        strRawData = strRawData & "advance:" & myresultArray(24).ToString & vbCrLf
        strRawData = strRawData & "afrTarget:" & myresultArray(25).ToString & vbCrLf
        strRawData = strRawData & "fuelADC:" & myresultArray(26).ToString & vbCrLf
        strRawData = strRawData & "egtADC:" & myresultArray(27).ToString & vbCrLf
        strRawData = strRawData & "CltIatAngle:" & myresultArray(28).ToString & vbCrLf
        strRawData = strRawData & "KnockAngle:" & myresultArray(29).ToString & vbCrLf
        strRawData = strRawData & "egoCorrection2:" & myresultArray(30).ToString & vbCrLf
        strRawData = strRawData & "porta:" & myresultArray(31).ToString & vbCrLf
        strRawData = strRawData & "portb:" & myresultArray(32).ToString & vbCrLf
        strRawData = strRawData & "portc:" & myresultArray(33).ToString & vbCrLf
        strRawData = strRawData & "portd:" & myresultArray(34).ToString & vbCrLf
        strRawData = strRawData & "stackL:" & myresultArray(35).ToString & vbCrLf
        strRawData = strRawData & "tpsLast:" & myresultArray(36).ToString & vbCrLf
        strRawData = strRawData & "iTime* (Highest byte):" & myresultArray(37).ToString & vbCrLf
        strRawData = strRawData & "bcdc:" & myresultArray(38).ToString & vbCrLf
        Return strRawData

    End Function

    Private Sub OpenToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        OpenFileDialog1.ShowDialog(Me)
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk

        Dim m_DataSet As New DataSet()
        m_DataSet.ReadXml(OpenFileDialog1.FileName)

        Dim m_DataRows As DataRow() = m_DataSet.Tables("page").Select("number=0")
        Dim m_DataRows_constant As DataRow() = m_DataRows(0).GetChildRows("page_constant")
        For Each m_DataRow In m_DataRows_constant
            If m_DataRow("name") = "mapBins1" Then
                Dim mapBins1 As String() = m_DataRow.ItemArray(5).ToString.Trim.Replace(vbLf, "").Replace(" ", "").Split(vbCrLf)
                Dim mapBinValue As Integer = Nothing
                For i As Integer = 0 To mapBins1.Length - 1
                    mapBinValue = CInt(mapBins1(i).Replace(".", ","))
                    Vacuum_Axis.Vacuum(i) = mapBinValue
                Next
            End If
            If m_DataRow("name") = "rpmBins1" Then
                Dim rpmBins1 As String() = m_DataRow.ItemArray(5).ToString.Trim.Replace(vbLf, "").Replace(" ", "").Split(vbCrLf)
                Dim rpmBinValue As Integer = Nothing
                For i As Integer = 0 To rpmBins1.Length - 1
                    rpmBinValue = CInt(rpmBins1(i).Replace(".", ","))
                    Rpm_Axis.Rpm(i) = rpmBinValue / 100
                Next
            End If
            If m_DataRow("name") = "veBins1" Then
                Dim veBinsRows1 As String() = m_DataRow.ItemArray(5).ToString.Trim.Replace(vbLf, "").Split(vbCrLf)
                For row As Integer = 0 To veBinsRows1.Length - 1
                    Dim veBinsRow As String = veBinsRows1(row).ToString.Replace("         ", "")
                    If veBinsRow.EndsWith(" ") Then
                        veBinsRow = Microsoft.VisualBasic.Left(veBinsRow, veBinsRow.Length - 1)
                    End If
                    Dim veBinsCols1 As String() = veBinsRow.Split(" ")
                    Dim veBinValue As Integer = Nothing
                    For col As Integer = 0 To veBinsCols1.Length - 1
                        veBinValue = CInt(veBinsCols1(col).Replace(".", ","))
                        Grid.Map(row, col) = veBinValue
                    Next
                Next
            End If
        Next

    End Sub

    Private Sub MapLambaMenu_Click(sender As System.Object, e As System.EventArgs)
        Dim ToolStripMenuItem As ToolStripMenuItem = sender

        Select Case ToolStripMenuItem.Name
            Case "MapToolStripMenuItem"
                If Not LambdaToolStripMenuItem.Checked Then
                    MapToolStripMenuItem.Checked = True
                End If
            Case "LambdaToolStripMenuItem"
                If Not MapToolStripMenuItem.Checked Then
                    LambdaToolStripMenuItem.Checked = True
                End If

        End Select

    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub
End Class
