Public Class lube_oil

    'THIS PROCEDURE STARTS THE LUBE OIL PUMP.
    Public Sub lube_pump_start()
        DW.Button5.Enabled = False
        DW.Button5.Visible = False
        DW.Button14.Enabled = True
        DW.Button14.Visible = True
        DW.TextBox9.Text = 88
        DW.lube_oil = 1
    End Sub

    'THIS PROCEDURE STOPS THE LUBE OIL PUMP.
    Public Sub lube_pump_stop()
        DW.Button5.Enabled = True
        DW.Button5.Visible = True
        DW.Button14.Enabled = False
        DW.Button14.Visible = False
        DW.TextBox9.Text = 0
        If DW.low_lube_oil_press = 1 Then
            low_lube(88.0)
        End If
        DW.lube_oil = 0

    End Sub

    'THIS PROCEDURE SIMULATES THE LOW LUBE OIL FAULT CONDITION
    Public Sub low_lube(x As Integer)

        If DW.low_lube_oil_press = 1 Then
            Delay(0.5)
            DW.TextBox9.Text = x
            Alarms_Page.update_alarm(DW.low_lube_oil_press_id, "Low Lube Oil Pressure", "DW_LUBE", "Resolved")

            DW.low_lube_oil_press = 0
            DW.Button28.Enabled = False
            DW.Button28.Visible = False
            DW.Button45.Enabled = True
            DW.Button45.Visible = True
        End If
    End Sub
End Class
