Public Class hpu


    Public Sub hpu_a_start()
        If DW.hpu_a = 0 And DW.hpu_b = 0 Then
            Delay(1)
            DW.Button44.Enabled = False
            DW.Button44.Visible = False
            DW.Button42.Enabled = True
            DW.Button42.Visible = True
            DW.Button7.Enabled = False
            DW.Button7.Visible = False
            DW.Button13.Enabled = True
            DW.Button13.Visible = True
            DW.TextBox7.Text = 165.5

            DW.hpu_a = 1
        End If
    End Sub

    Public Sub hpu_b_start()

        If DW.hpu_a = 0 And DW.hpu_b = 0 Then
            Delay(1)
            DW.Button8.Enabled = False
            DW.Button8.Visible = False
            DW.Button12.Enabled = True
            DW.Button12.Visible = True
            DW.Button43.Enabled = False
            DW.Button43.Visible = False
            DW.Button41.Enabled = True
            DW.Button41.Visible = True
            DW.TextBox7.Text = 165.5

            DW.hpu_b = 1
        End If
    End Sub

    Public Sub hpu_a_stop()
        DW.Button7.Enabled = True
        DW.Button7.Visible = True
        DW.Button13.Enabled = False
        DW.Button13.Visible = False
        DW.Button44.Enabled = True
        DW.Button44.Visible = True
        DW.Button42.Enabled = False
        DW.Button42.Visible = False

        DW.TextBox7.Text = 0.0

        DW.hpu_a = 0
    End Sub

    Public Sub hpu_b_stop()
        DW.Button41.Enabled = False
        DW.Button41.Visible = False
        DW.Button43.Enabled = True
        DW.Button43.Visible = True
        DW.Button12.Enabled = False
        DW.Button12.Visible = False
        DW.Button8.Enabled = True
        DW.Button8.Visible = True

        DW.TextBox7.Text = 0.0

        DW.hpu_b = 0
    End Sub

    Public Sub hpu_low_low_press()

        DW.cal12 = 1
        DW.cal3 = 1
        DW.cal4 = 1
        DW.cal56 = 1
        DW.Button21.Enabled = False
        DW.Button21.Visible = False
        DW.Button16.Enabled = True
        DW.Button16.Visible = True

        DW.Button22.Enabled = False
        DW.Button22.Visible = False
        DW.Button15.Enabled = True
        DW.Button15.Visible = True

        DW.Button23.Enabled = False
        DW.Button23.Visible = False
        DW.Button3.Enabled = True
        DW.Button3.Visible = True

        DW.Button24.Enabled = False
        DW.Button24.Visible = False
        DW.Button19.Enabled = True
        DW.Button19.Visible = True

        DW.TextBox5.Text = 0.0
        DW.TextBox4.Text = 0.0
        DW.TextBox3.Text = 0.0
        DW.TextBox6.Text = 0.0

        DW.cal_sum = DW.cal12 + DW.cal3 + DW.cal4 + DW.cal56
        DW.Button37.Enabled = False
        DW.Button37.Visible = False
        DW.Button38.Enabled = False
        DW.Button38.Visible = True

        DW.Button40.Enabled = False
        DW.Button40.Visible = False
        DW.Button39.Enabled = False
        DW.Button39.Visible = True

        DW.Button45.Enabled = False
        DW.Button45.Visible = False
        DW.Button49.Enabled = True
        DW.Button49.Visible = True


    End Sub

    Public Sub hpu_low_press()

        DW.Button51.Enabled = False
        DW.Button51.Visible = False
        DW.Button49.Enabled = True
        DW.Button49.Visible = True
    End Sub

    Public Sub hpu_press_normal(x As Integer)
        DW.TextBox7.Text = x
        DW.Button49.Enabled = False
        DW.Button49.Visible = False
        DW.Button51.Enabled = True
        DW.Button51.Visible = True

        DW.Button37.Enabled = False
        DW.Button37.Visible = False
        DW.Button38.Enabled = True
        DW.Button38.Visible = True

        DW.Button40.Enabled = False
        DW.Button40.Visible = False
        DW.Button39.Enabled = True
        DW.Button39.Visible = True
    End Sub
End Class
