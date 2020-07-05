Public Module Module1
    Public Const MapSize = 12

    Public Enum WhatToDisplayInGrid
        Map
        Lambda
        Map_Lambda
    End Enum

    Public Function retrieveColorForPosition(ByVal p_row As Integer, ByVal p_col As Integer, ByVal p_Matrix As Byte(,)) As Color
        Dim m_PositionInColorArray As Integer = CInt(p_Matrix(p_row, p_col))
        If m_PositionInColorArray <= 0 Then
            Return System.Drawing.ColorTranslator.FromOle( _
                RGB(200, 200, 200))
        ElseIf m_PositionInColorArray > 254 Then
            Return System.Drawing.ColorTranslator.FromOle( _
                RGB(255, 255, 255))
        Else
            Return System.Drawing.ColorTranslator.FromOle( _
                RGB(_Color(m_PositionInColorArray, cnstRED), _Color(m_PositionInColorArray, cnstGreen), _Color(m_PositionInColorArray, cnstBlue)))
        End If
    End Function

    Public Sub prefillMatrix(ByVal p_Matrix As Integer(,), ByVal p_VeBins As Byte(,))

        For i As Integer = 0 To p_Matrix.GetLength(0) - 1
            For j As Integer = 0 To p_Matrix.GetLength(1) - 1
                p_Matrix(i, j) = p_VeBins(i, j)
            Next
        Next

    End Sub

    Public Sub prefillVector(ByVal p_Vector As Integer(), ByVal p_Data As Byte())
        For i As Integer = 0 To p_Vector.GetLength(0) - 1
            p_Vector(i) = p_Data(i)
        Next
    End Sub

    Public Function AreEqual(ByVal p_ByteArray1 As Byte(), ByVal p_ByteArray2 As Byte()) As Boolean

        If p_ByteArray1.Length <> p_ByteArray2.Length Then Return False

        For i As Integer = 0 To p_ByteArray1.Length - 1
            If p_ByteArray1(i) <> p_ByteArray2(i) Then Return False
        Next

        Return True

    End Function


End Module
