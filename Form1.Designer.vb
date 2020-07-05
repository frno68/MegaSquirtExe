<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.txtRow = New System.Windows.Forms.TextBox()
        Me.txtCol = New System.Windows.Forms.TextBox()
        Me.Timer_GetRealTimeValues = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_FormUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.txtMapValue = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Timer_StartUp = New System.Windows.Forms.Timer(Me.components)
        Me.pnl_RPM_Axis = New System.Windows.Forms.Panel()
        Me.pnl_VACUUM_Axis = New System.Windows.Forms.Panel()
        Me.pnl_Map_and_Lambda_detail = New System.Windows.Forms.Panel()
        Me.pnl_Map_and_Lambda = New System.Windows.Forms.Panel()
        Me.updwn_Timer1_Ratio = New System.Windows.Forms.NumericUpDown()
        Me.txtRawData = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MapToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LambdaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.txtLambdaValue = New System.Windows.Forms.TextBox()
        Me.LambdaGauge = New Aguage.AGauge()
        CType(Me.updwn_Timer1_Ratio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtRow
        '
        Me.txtRow.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRow.Location = New System.Drawing.Point(721, 37)
        Me.txtRow.Name = "txtRow"
        Me.txtRow.ReadOnly = True
        Me.txtRow.Size = New System.Drawing.Size(46, 26)
        Me.txtRow.TabIndex = 19
        '
        'txtCol
        '
        Me.txtCol.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCol.Location = New System.Drawing.Point(774, 37)
        Me.txtCol.Name = "txtCol"
        Me.txtCol.ReadOnly = True
        Me.txtCol.Size = New System.Drawing.Size(47, 26)
        Me.txtCol.TabIndex = 20
        '
        'Timer_GetRealTimeValues
        '
        Me.Timer_GetRealTimeValues.Interval = 10
        '
        'Timer_FormUpdate
        '
        '
        'txtMapValue
        '
        Me.txtMapValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMapValue.Location = New System.Drawing.Point(721, 69)
        Me.txtMapValue.Name = "txtMapValue"
        Me.txtMapValue.ReadOnly = True
        Me.txtMapValue.Size = New System.Drawing.Size(100, 26)
        Me.txtMapValue.TabIndex = 25
        '
        'TextBox1
        '
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(721, 150)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(100, 26)
        Me.TextBox1.TabIndex = 26
        '
        'Timer_StartUp
        '
        Me.Timer_StartUp.Interval = 65
        '
        'pnl_RPM_Axis
        '
        Me.pnl_RPM_Axis.Location = New System.Drawing.Point(42, 576)
        Me.pnl_RPM_Axis.Name = "pnl_RPM_Axis"
        Me.pnl_RPM_Axis.Size = New System.Drawing.Size(541, 30)
        Me.pnl_RPM_Axis.TabIndex = 30
        '
        'pnl_VACUUM_Axis
        '
        Me.pnl_VACUUM_Axis.Location = New System.Drawing.Point(11, 34)
        Me.pnl_VACUUM_Axis.Name = "pnl_VACUUM_Axis"
        Me.pnl_VACUUM_Axis.Size = New System.Drawing.Size(30, 541)
        Me.pnl_VACUUM_Axis.TabIndex = 29
        '
        'pnl_Map_and_Lambda_detail
        '
        Me.pnl_Map_and_Lambda_detail.Location = New System.Drawing.Point(585, 34)
        Me.pnl_Map_and_Lambda_detail.Name = "pnl_Map_and_Lambda_detail"
        Me.pnl_Map_and_Lambda_detail.Size = New System.Drawing.Size(42, 541)
        Me.pnl_Map_and_Lambda_detail.TabIndex = 28
        '
        'pnl_Map_and_Lambda
        '
        Me.pnl_Map_and_Lambda.Location = New System.Drawing.Point(42, 34)
        Me.pnl_Map_and_Lambda.Name = "pnl_Map_and_Lambda"
        Me.pnl_Map_and_Lambda.Size = New System.Drawing.Size(540, 540)
        Me.pnl_Map_and_Lambda.TabIndex = 27
        '
        'updwn_Timer1_Ratio
        '
        Me.updwn_Timer1_Ratio.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.updwn_Timer1_Ratio.Location = New System.Drawing.Point(721, 586)
        Me.updwn_Timer1_Ratio.Maximum = New Decimal(New Integer() {4000, 0, 0, 0})
        Me.updwn_Timer1_Ratio.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.updwn_Timer1_Ratio.Name = "updwn_Timer1_Ratio"
        Me.updwn_Timer1_Ratio.Size = New System.Drawing.Size(100, 20)
        Me.updwn_Timer1_Ratio.TabIndex = 59
        Me.updwn_Timer1_Ratio.Value = New Decimal(New Integer() {100, 0, 0, 0})
        '
        'txtRawData
        '
        Me.txtRawData.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRawData.Location = New System.Drawing.Point(827, 34)
        Me.txtRawData.Multiline = True
        Me.txtRawData.Name = "txtRawData"
        Me.txtRawData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRawData.Size = New System.Drawing.Size(145, 169)
        Me.txtRawData.TabIndex = 58
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.ViewToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(984, 24)
        Me.MenuStrip1.TabIndex = 60
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenToolStripMenuItem, Me.SaveToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.OpenToolStripMenuItem.Text = "Open"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MapToolStripMenuItem, Me.LambdaToolStripMenuItem})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.ViewToolStripMenuItem.Text = "View"
        '
        'MapToolStripMenuItem
        '
        Me.MapToolStripMenuItem.Checked = True
        Me.MapToolStripMenuItem.CheckOnClick = True
        Me.MapToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.MapToolStripMenuItem.Name = "MapToolStripMenuItem"
        Me.MapToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.MapToolStripMenuItem.Text = "Map"
        '
        'LambdaToolStripMenuItem
        '
        Me.LambdaToolStripMenuItem.Checked = True
        Me.LambdaToolStripMenuItem.CheckOnClick = True
        Me.LambdaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.LambdaToolStripMenuItem.Name = "LambdaToolStripMenuItem"
        Me.LambdaToolStripMenuItem.Size = New System.Drawing.Size(117, 22)
        Me.LambdaToolStripMenuItem.Text = "Lambda"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Filter = "Megatune files (*.msq)|*.msq|All files (*.*)|*.*"
        '
        'txtLambdaValue
        '
        Me.txtLambdaValue.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLambdaValue.Location = New System.Drawing.Point(721, 101)
        Me.txtLambdaValue.Name = "txtLambdaValue"
        Me.txtLambdaValue.ReadOnly = True
        Me.txtLambdaValue.Size = New System.Drawing.Size(100, 26)
        Me.txtLambdaValue.TabIndex = 61
        '
        'LambdaGauge
        '
        Me.LambdaGauge.BackColor = System.Drawing.SystemColors.Control
        Me.LambdaGauge.BaseArcColor = System.Drawing.Color.Black
        Me.LambdaGauge.BaseArcRadius = 155
        Me.LambdaGauge.BaseArcStart = 135
        Me.LambdaGauge.BaseArcSweep = 270
        Me.LambdaGauge.BaseArcWidth = 4
        Me.LambdaGauge.Cap_Idx = CType(0, Byte)
        Me.LambdaGauge.CapColors = New System.Drawing.Color() {System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black}
        Me.LambdaGauge.CapPosition = New System.Drawing.Point(133, 240)
        Me.LambdaGauge.CapsPosition = New System.Drawing.Point() {New System.Drawing.Point(133, 240), New System.Drawing.Point(10, 10), New System.Drawing.Point(10, 10), New System.Drawing.Point(10, 10), New System.Drawing.Point(10, 10)}
        Me.LambdaGauge.CapsText = New String() {"", "", "", "", ""}
        Me.LambdaGauge.CapText = ""
        Me.LambdaGauge.Center = New System.Drawing.Point(170, 170)
        Me.LambdaGauge.Font = New System.Drawing.Font("Arial Rounded MT Bold", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LambdaGauge.Location = New System.Drawing.Point(632, 281)
        Me.LambdaGauge.MaxValue = 19.4!
        Me.LambdaGauge.MinValue = 10.0!
        Me.LambdaGauge.Name = "LambdaGauge"
        Me.LambdaGauge.NeedleColor1 = Aguage.AGauge.NeedleColorEnum.Green
        Me.LambdaGauge.NeedleColor2 = System.Drawing.Color.Black
        Me.LambdaGauge.NeedleRadius = 140
        Me.LambdaGauge.NeedleType = 0
        Me.LambdaGauge.NeedleWidth = 8
        Me.LambdaGauge.Range_Idx = CType(0, Byte)
        Me.LambdaGauge.RangeColor = System.Drawing.Color.PaleGoldenrod
        Me.LambdaGauge.RangeEnabled = True
        Me.LambdaGauge.RangeEndValue = 19.4!
        Me.LambdaGauge.RangeInnerRadius = 100
        Me.LambdaGauge.RangeOuterRadius = 150
        Me.LambdaGauge.RangesColor = New System.Drawing.Color() {System.Drawing.Color.PaleGoldenrod, System.Drawing.Color.DarkOrange, System.Drawing.Color.YellowGreen, System.Drawing.SystemColors.Control, System.Drawing.SystemColors.Control}
        Me.LambdaGauge.RangesEnabled = New Boolean() {True, True, True, False, False}
        Me.LambdaGauge.RangesEndValue = New Single() {19.4!, 16.2!, 15.2!, 0!, 0!}
        Me.LambdaGauge.RangesInnerRadius = New Integer() {100, 130, 130, 70, 70}
        Me.LambdaGauge.RangesOuterRadius = New Integer() {150, 150, 150, 80, 80}
        Me.LambdaGauge.RangesStartValue = New Single() {10.0!, 12.2!, 13.2!, 0!, 0!}
        Me.LambdaGauge.RangeStartValue = 10.0!
        Me.LambdaGauge.ScaleLinesInterColor = System.Drawing.Color.Black
        Me.LambdaGauge.ScaleLinesInterInnerRadius = 130
        Me.LambdaGauge.ScaleLinesInterOuterRadius = 150
        Me.LambdaGauge.ScaleLinesInterWidth = 2
        Me.LambdaGauge.ScaleLinesMajorColor = System.Drawing.Color.Black
        Me.LambdaGauge.ScaleLinesMajorInnerRadius = 130
        Me.LambdaGauge.ScaleLinesMajorOuterRadius = 150
        Me.LambdaGauge.ScaleLinesMajorStepValue = 10.0!
        Me.LambdaGauge.ScaleLinesMajorWidth = 2
        Me.LambdaGauge.ScaleLinesMinorColor = System.Drawing.Color.Black
        Me.LambdaGauge.ScaleLinesMinorInnerRadius = 150
        Me.LambdaGauge.ScaleLinesMinorNumOf = 9
        Me.LambdaGauge.ScaleLinesMinorOuterRadius = 130
        Me.LambdaGauge.ScaleLinesMinorWidth = 2
        Me.LambdaGauge.ScaleNumbersColor = System.Drawing.Color.Black
        Me.LambdaGauge.ScaleNumbersFormat = "3"
        Me.LambdaGauge.ScaleNumbersRadius = 95
        Me.LambdaGauge.ScaleNumbersRotation = 0
        Me.LambdaGauge.ScaleNumbersStartScaleLine = 2
        Me.LambdaGauge.ScaleNumbersStepScaleLines = 3
        Me.LambdaGauge.Size = New System.Drawing.Size(340, 293)
        Me.LambdaGauge.TabIndex = 64
        Me.LambdaGauge.Text = "AGauge1"
        Me.LambdaGauge.Value = 10.0!
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 612)
        Me.Controls.Add(Me.LambdaGauge)
        Me.Controls.Add(Me.txtLambdaValue)
        Me.Controls.Add(Me.updwn_Timer1_Ratio)
        Me.Controls.Add(Me.txtRawData)
        Me.Controls.Add(Me.pnl_RPM_Axis)
        Me.Controls.Add(Me.pnl_VACUUM_Axis)
        Me.Controls.Add(Me.pnl_Map_and_Lambda_detail)
        Me.Controls.Add(Me.pnl_Map_and_Lambda)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.txtMapValue)
        Me.Controls.Add(Me.txtCol)
        Me.Controls.Add(Me.txtRow)
        Me.Controls.Add(Me.MenuStrip1)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.updwn_Timer1_Ratio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtRow As System.Windows.Forms.TextBox
    Friend WithEvents txtCol As System.Windows.Forms.TextBox
    Friend WithEvents Timer_GetRealTimeValues As System.Windows.Forms.Timer
    Friend WithEvents Timer_FormUpdate As System.Windows.Forms.Timer
    Friend WithEvents txtMapValue As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Timer_StartUp As System.Windows.Forms.Timer
    Friend WithEvents pnl_RPM_Axis As System.Windows.Forms.Panel
    Friend WithEvents pnl_VACUUM_Axis As System.Windows.Forms.Panel
    Friend WithEvents pnl_Map_and_Lambda_detail As System.Windows.Forms.Panel
    Friend WithEvents pnl_Map_and_Lambda As System.Windows.Forms.Panel
    Friend WithEvents updwn_Timer1_Ratio As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtRawData As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtLambdaValue As System.Windows.Forms.TextBox
    Friend WithEvents LambdaGauge As Aguage.AGauge
    Friend WithEvents ViewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MapToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LambdaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
