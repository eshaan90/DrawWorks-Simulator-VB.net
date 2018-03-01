Public Class air_press

    Public Sub air_press_normal()

        If DW.low_air_press = 1 Then
            Delay(0.5)
            DW.TextBox10.Text = 141.1
            Alarms_Page.update_alarm(DW.low_air_press_id, "Low Air Pressure", "DW", "Resolved")

            DW.low_air_press = 0
            DW.Button46.Enabled = False
            DW.Button46.Visible = False
            DW.Button59.Enabled = True
            DW.Button59.Visible = True
        End If
    End Sub

End Class
