Public Class clutch

    'Low clutch activation procedure
    Public Sub low_clutch()
        If DW.dw_on = 1 Then
            If DW.TextBox10.Text > 140 Then
                Delay(1)
                If DW.high_clutch = 0 Then
                    DW.TextBox2.Text = 135.5
                    DW.low_clutch = 1
                Else
                    DW.high_clutch = 0
                    DW.low_clutch = 1
                    DW.TextBox2.Text = 135.5
                    DW.TextBox1.Text = 0.0
                    DW.Button11.BackColor = Color.Gainsboro
                    DW.Button35.BackColor = Color.Gainsboro
                End If
                DW.Button9.BackColor = Color.Green
                DW.Button36.BackColor = Color.Green
            End If
        End If
    End Sub

    'High clutch activation procedure
    Public Sub high_clutch()
        If DW.TextBox10.Text > 140 And DW.low_clutch = 1 Then
            Delay(1)
            DW.TextBox1.Text = 135.5
            DW.high_clutch = 1
            DW.low_clutch = 0
            DW.TextBox2.Text = 0.0
            DW.Button11.BackColor = Color.Green
            DW.Button9.BackColor = Color.Gainsboro
            DW.Button35.BackColor = Color.Green
            DW.Button36.BackColor = Color.Gainsboro
        End If
    End Sub
End Class
