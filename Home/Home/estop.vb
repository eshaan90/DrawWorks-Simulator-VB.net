Public Class estop
    Dim g As System.Drawing.Graphics

    Public Sub e_stop_active()

        Dim myGraphics As Graphics = DW.CreateGraphics

        If DW.e_stop = 0 Then 'Or cal_sum <> 4 Or lube_oil = 1 Or dw_on = 1 Or hpu_a = 1 Or hpu_b = 1
            Delay(0.5)

            DW.cal12 = 1
            DW.cal3 = 1
            DW.cal4 = 1
            DW.cal56 = 1
            DW.cal_sum = DW.cal12 + DW.cal3 + DW.cal4 + DW.cal56
            DW.Button24.Enabled = False
            DW.Button24.Visible = False
            DW.Button19.Enabled = False
            DW.Button19.Visible = True
            DW.Button22.Enabled = False
            DW.Button22.Visible = False
            DW.Button15.Enabled = False
            DW.Button15.Visible = True
            DW.Button23.Enabled = False
            DW.Button23.Visible = False
            DW.Button3.Enabled = False
            DW.Button3.Visible = True
            DW.Button21.Enabled = False
            DW.Button21.Visible = False
            DW.Button16.Enabled = False
            DW.Button16.Visible = True
            DW.Button38.Visible = True
            DW.Button38.Enabled = False
            DW.Button37.Visible = False
            DW.Button37.Enabled = False
            DW.Button9.Enabled = False
            DW.Button11.Enabled = False
            DW.TextBox5.Text = 0.0
            DW.TextBox3.Text = 0.0
            DW.TextBox6.Text = 0.0
            DW.TextBox6.Text = 0.0
            DW.TextBox4.Text = 0.0


            DW.dw_on = 0
            DW.Button20.Enabled = False
            DW.Button20.Visible = False
            DW.Button10.Enabled = False
            DW.Button10.Visible = True


            DW.Button12.Enabled = False
            DW.Button12.Visible = False
            DW.Button8.Enabled = False
            DW.Button8.Visible = True
            DW.Button7.Enabled = False
            DW.Button7.Visible = True
            DW.Button13.Enabled = False
            DW.Button13.Visible = False

            DW.TextBox7.Text = 0.0

            DW.hpu_b = 0
            DW.hpu_a = 0


            DW.Button5.Enabled = False
            DW.Button5.Visible = True
            DW.Button14.Enabled = False
            DW.Button14.Visible = False

            DW.TextBox9.Text = 0.0

            DW.lube_oil = 0


            DW.Button17.Enabled = False
            DW.Button18.Enabled = False
        End If

        DW.Button35.Enabled = False
        DW.Button36.Enabled = False

        DW.Button43.Enabled = False
        DW.Button41.Enabled = False
        DW.Button41.Visible = False
        DW.Button43.Visible = True

        DW.Button42.Enabled = False
        DW.Button42.Visible = False
        DW.Button44.Enabled = False
        DW.Button44.Visible = True

        DW.Button39.Enabled = False
        DW.Button39.Visible = True
        DW.Button40.Enabled = False
        DW.Button40.Visible = False

        DW.Button31.Enabled = False
        DW.Button31.Visible = False
        DW.Button34.Enabled = False
        DW.Button34.Visible = True

        DW.Button32.Enabled = False
        DW.Button33.Enabled = False

        DW.Button30.Visible = False
        DW.Button30.Enabled = False
        DW.Button29.Visible = True
        DW.Button29.Enabled = True

        DW.Button6.Visible = False
        DW.Button6.Enabled = False
        DW.Button25.Visible = True
        DW.Button25.Enabled = True


        DW.e_stop = 1

        If DW.estop_press = 0 Then
            Delay(0.5)
            Alarms_Page.find_ID()
            DW.estop_id = Alarms_Page.max + 1
            Alarms_Page.insert_alarms(DW.estop_id, "Emergency Stop Activated", "DW", "Active")
            DW.estop_press = 1
        End If
    End Sub

    Public Sub e_stop_release()
        If DW.e_stop = 1 Then
            Delay(0.5)

            'Manual Mode Controls
            DW.Button5.Enabled = True
            DW.Button7.Enabled = True
            DW.Button8.Enabled = True
            DW.Button10.Enabled = True
            DW.Button19.Enabled = True
            DW.Button15.Enabled = True
            DW.Button3.Enabled = True
            DW.Button16.Enabled = True

            DW.Button25.Visible = False
            DW.Button25.Enabled = False
            DW.Button6.Visible = True
            DW.Button6.Enabled = True
            DW.Button17.Enabled = True
            DW.Button18.Enabled = True


            DW.Button38.Enabled = True
            DW.Button37.Enabled = True
            DW.Button9.Enabled = True
            DW.Button11.Enabled = True

            'Auto Mode Controls
            DW.Button35.Enabled = True
            DW.Button36.Enabled = True
            DW.Button43.Enabled = True
            DW.Button41.Enabled = True
            DW.Button42.Enabled = True
            DW.Button44.Enabled = True
            DW.Button39.Enabled = True
            DW.Button40.Enabled = True
            DW.Button31.Enabled = True
            DW.Button34.Enabled = True
            DW.Button32.Enabled = True
            DW.Button33.Enabled = True

            DW.Button30.Visible = True
            DW.Button30.Enabled = True
            DW.Button29.Visible = False
            DW.Button29.Enabled = False
            DW.e_stop = 0

            If DW.estop_press = 1 Then
                Delay(0.5)
                Alarms_Page.update_alarm(DW.estop_id, "Emergency Stop Activated", "DW", "Resolved")
                DW.estop_press = 0
            End If
        End If
    End Sub

End Class
