Public Class lube_oil

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
