Imports System.Data.OleDb

Public Class DW

    Private gui_d As New drawing_gui
    Private access As New dbcontrol
    Private airpress As New air_press
    Private l_oil As New lube_oil
    Private emergency_stop As New estop
    Private hpu_obj As New hpu
    Public clutch_obj As New clutch

    Public f As PaintEventArgs
    Dim g As System.Drawing.Graphics
    Public low_clutch As Integer = 0
    Public high_clutch As Integer = 0
    Public lube_oil As Integer = 0
    Public hpu_a As Integer = 0
    Public hpu_b As Integer = 0
    Public dw_on As Integer = 0
    Public cal12 As Integer = 1
    Public cal56 As Integer = 1
    Public cal3 As Integer = 1
    Public cal4 As Integer = 1
    Public e_stop As Integer = 0
    Public cal_sum As Integer = cal12 + cal3 + cal4 + cal56
    Const floor_saver As Single = 4.57
    Const roof_saver As Single = 117.16
    Public auto_mode As Integer = 0
    Public manual_mode As Integer = 0


    'Variables to keep track of the Alarms 
    Public low_air_press As Integer = 0
    Public low_air_press_id As Integer = 0
    Public low_hpu_press As Integer = 0
    Public low_low_hpu_press As Integer = 0
    Public low_hpu_press_id As Integer = 0
    Public brakes_applied_id As Integer = 0
    Public low_lube_oil_press As Integer = 0
    Public low_lube_oil_press_id As Integer = 0
    Public estop_press As Integer = 0
    Public estop_id As Integer = 0
    Public roof_saver_active As Integer = 0
    Public roof_saver_active_id As Integer = 0
    Public floor_saver_active As Integer = 0
    Public floor_saver_active_id As Integer = 0

    'ALARMS BUTTON PRESSED
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Alarms_Page.Show()
        Me.Hide()
    End Sub

    'DOCUMENTATION BUTTON PRESSED
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dw_documentation.Show()
        Me.Hide()

    End Sub

    'HOME BUTTON PRESSED
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Home.Show()
        Me.Hide()
    End Sub

    Private Sub DW_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        g = Me.CreateGraphics()
        Dim brush3 As Brush
        brush3 = New Drawing.SolidBrush(Color.LawnGreen)
        GroupBox1.Enabled = False
        TextBox10.Text += 141.1
        GroupBox4.Hide()
    End Sub

    'Graphic Design for the DW GUI
    Private Sub DW_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Try
            gui_d.gui_draw(e)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'Lube Oil Pump Start Button
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Delay(1)
        Using brush3 As New SolidBrush(Color.LawnGreen)
            g.FillEllipse(brush3, 740, 800, 40, 40) 'Lube Oil Pump
        End Using
        Button5.Enabled = False
        Button5.Visible = False
        Button14.Enabled = True
        Button14.Visible = True
        While TextBox9.Text < 88
            TextBox9.Text += 11
        End While
        lube_oil = 1
    End Sub

    'Lube Oil Pump Stop Button
    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        If dw_on = 0 Then
            Delay(1)
            Using brush2 As New SolidBrush(Color.White)
                g.FillEllipse(brush2, 740, 800, 40, 40) 'Lube Oil Pump
            End Using
            Button5.Enabled = True
            Button5.Visible = True
            Button14.Enabled = False
            Button14.Visible = False
            While TextBox9.Text <> 0
                TextBox9.Text -= 11
            End While
            If low_lube_oil_press = 1 Then
                l_oil.low_lube(88.0)
            End If
            lube_oil = 0
        End If
    End Sub

    'Manual Mode Radio Button
    Private Sub RadioButton1_CheckedChanged_1(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        GroupBox1.Show()
        GroupBox4.Hide()
        GroupBox5.Hide()
        GroupBox1.Enabled = True
        GroupBox4.Enabled = False
        GroupBox5.Enabled = False
        If low_clutch = 1 Then
            Button36.BackColor = Color.Green
        ElseIf high_clutch = 1 Then
            Button35.BackColor = Color.Green
        End If

    End Sub

    'HPU Pump A Start Button
    Public Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            hpu_obj.hpu_a_start()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Using brush3 As New SolidBrush(Color.LawnGreen)
            g.FillEllipse(brush3, 110, 680, 40, 40) 'HPU Pump A
        End Using
    End Sub

    'HPU Pump A Stop Button
    Public Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        Try
            If hpu_a = 1 And (dw_on = 0 Or cal_sum > 2) Then
                Delay(1)
                Using brush3 As New SolidBrush(Color.White)
                    g.FillEllipse(brush3, 110, 680, 40, 40) 'HPU Pump A
                End Using
                hpu_obj.hpu_a_stop()
                If low_hpu_press = 1 Or low_low_hpu_press = 1 Then
                    Alarms_Page.update_alarm(low_hpu_press_id, "Low HPU Pressure", "DW_HPU", "Resolved")
                    Alarms_Page.update_alarm(brakes_applied_id, "DW_Brakes Applied(Low HPU Pressure)", "DW_HPU", "Resolved")
                    low_hpu_press = 0
                    hpu_obj.hpu_press_normal(0)
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'HPU Pump B Start Button
    Public Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            hpu_obj.hpu_b_start()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Using brush3 As New SolidBrush(Color.LawnGreen)
            g.FillEllipse(brush3, 410, 680, 40, 40) 'HPU Pump B
        End Using
    End Sub

    'HPU Pump B Stop Button
    Public Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Try
            If hpu_b = 1 And (dw_on = 0 Or cal_sum > 2) Then
                Delay(1)
                Using brush3 As New SolidBrush(Color.White)
                    g.FillEllipse(brush3, 410, 680, 40, 40) 'HPU Pump B
                End Using
                hpu_obj.hpu_b_stop()
                If low_hpu_press = 1 Or low_low_hpu_press = 1 Then
                    Alarms_Page.update_alarm(low_hpu_press_id, "Low HPU Pressure", "DW_HPU", "Resolved")
                    Alarms_Page.update_alarm(brakes_applied_id, "DW_Brakes Applied(Low HPU Pressure)", "DW_HPU", "Resolved")
                    low_hpu_press = 0
                    hpu_obj.hpu_press_normal(0)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'Low Clutch Activation Button
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        clutch_obj.low_clutch()
    End Sub

    'High Clutch Activation Button
    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        clutch_obj.high_clutch()
    End Sub

    'DW On Button
    Public Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        'If lube_oil = 1 Then
        'If hpu_a = 1 Or hpu_b = 1 Then
        If dw_on = 0 Then
            Delay(2)

            Using brush3 As New SolidBrush(Color.LawnGreen)
                g.FillEllipse(brush3, 700, 160, 60, 60) 'DW A Motor
                g.FillEllipse(brush3, 700, 270, 60, 60) 'DW B Motor
                g.FillEllipse(brush3, 700, 370, 60, 60) 'DW C Motor
                g.FillEllipse(brush3, 600, 160, 60, 60) 'DW A Blower Motor
                g.FillEllipse(brush3, 600, 270, 60, 60) 'DW B Blower Motor
                g.FillEllipse(brush3, 600, 370, 60, 60) 'DW C Blower Motor

            End Using
            Dim myFont As Font
            Dim myBrush As Brush
            myBrush = New Drawing.SolidBrush(Color.Black)
            myFont = New System.Drawing.Font("Times New Roman", 24)
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 173) 'DW A 
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 283) 'DW B
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 382) 'DW C
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 170) 'DW A Blower
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 283) 'DW B Blower
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 381) 'DW C Blower
            dw_on = 1
            Button10.Enabled = False
            Button10.Visible = False
            Button20.Enabled = True
            Button20.Visible = True
        End If

    End Sub

    'DW Off Button
    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        cal_sum = cal12 + cal3 + cal4 + cal56
        If dw_on = 1 And cal_sum > 2 Then
            Delay(2)
            Using brush3 As New SolidBrush(Color.White)
                g.FillEllipse(brush3, 700, 160, 60, 60) 'DW A Motor
                g.FillEllipse(brush3, 700, 270, 60, 60) 'DW B Motor
                g.FillEllipse(brush3, 700, 370, 60, 60) 'DW C Motor
                g.FillEllipse(brush3, 600, 160, 60, 60) 'DW A Blower Motor
                g.FillEllipse(brush3, 600, 270, 60, 60) 'DW B Blower Motor
                g.FillEllipse(brush3, 600, 370, 60, 60) 'DW C Blower Motor

            End Using
            Dim myFont As Font
            Dim myBrush As Brush
            myBrush = New Drawing.SolidBrush(Color.Black)
            myFont = New System.Drawing.Font("Times New Roman", 24)
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 173) 'DW A 
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 283) 'DW B
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 382) 'DW C
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 170) 'DW A Blower
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 283) 'DW B Blower
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 381) 'DW C Blower
            dw_on = 0
            Button20.Enabled = False
            Button20.Visible = False
            Button10.Enabled = True
            Button10.Visible = True
        End If

    End Sub

    'DW Brake Calipers 1,2 Release Button
    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        If hpu_a = 1 Or hpu_b = 1 Then
            If (dw_on = 0 And cal_sum > 2) Or (dw_on = 1 And (low_clutch = 1 Or high_clutch = 1)) Then
                Delay(1)
                Using brush4 As New SolidBrush(Color.White)
                    g.FillRectangle(brush4, 388, 432, 7, 25) 'DW Brakes 1,2
                    g.FillRectangle(brush4, 367, 432, 7, 25) 'DW Brakes 1,2
                End Using
                Button16.Enabled = False
                Button16.Visible = False
                Button21.Enabled = True
                Button21.Visible = True

                TextBox5.Text = TextBox7.Text - 4
                cal12 = 0
                cal_sum = cal12 + cal3 + cal4 + cal56
                If cal_sum = 0 Then
                    Button37.Enabled = True
                    Button37.Visible = True
                    Button38.Enabled = False
                    Button38.Visible = False
                End If
            End If
        End If
    End Sub

    'DW Brake Calipers 1,2 Apply Button
    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        If cal12 = 0 Then
            Delay(1)
            Using brush4 As New SolidBrush(Color.LawnGreen)
                g.FillRectangle(brush4, 388, 432, 7, 25) 'DW Brakes 1,2
                g.FillRectangle(brush4, 367, 432, 7, 25) 'DW Brakes 1,2
            End Using
            cal12 = 1
            Button21.Enabled = False
            Button21.Visible = False
            Button16.Enabled = True
            Button16.Visible = True

            TextBox5.Text = 0.0
            cal_sum = cal12 + cal3 + cal4 + cal56
            If cal_sum = 4 Then
                Button38.Enabled = True
                Button38.Visible = True
                Button37.Enabled = False
                Button37.Visible = False
            End If
        End If

    End Sub

    'DW Brake Calipers 3 Release Button
    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        If hpu_a = 1 Or hpu_b = 1 Then
            If (dw_on = 0 And cal_sum > 2) Or (dw_on = 1 And (low_clutch = 1 Or high_clutch = 1)) Then
                Delay(1)
                Using brush4 As New SolidBrush(Color.White)
                    g.FillRectangle(brush4, 388, 359, 7, 25) 'DW Brakes 3
                End Using
                Button15.Enabled = False
                Button15.Visible = False
                Button22.Enabled = True
                Button22.Visible = True

                TextBox4.Text = TextBox7.Text - 4
                cal3 = 0
                cal_sum = cal12 + cal3 + cal4 + cal56
                If cal_sum = 0 Then
                    Button37.Enabled = True
                    Button37.Visible = True
                    Button38.Enabled = False
                    Button38.Visible = False
                End If
            End If
        End If
    End Sub

    'DW Brake Calipers 3 Apply Button
    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        If cal3 = 0 Then
            Delay(1)
            Using brush4 As New SolidBrush(Color.LawnGreen)
                g.FillRectangle(brush4, 388, 359, 7, 25) 'DW Brakes 3
            End Using
            cal3 = 1
            Button22.Enabled = False
            Button22.Visible = False
            Button15.Enabled = True
            Button15.Visible = True

            TextBox4.Text = 0.0
            cal_sum = cal12 + cal3 + cal4 + cal56
            If cal_sum = 4 Then
                Button38.Enabled = True
                Button38.Visible = True
                Button37.Enabled = False
                Button37.Visible = False
            End If
        End If
    End Sub

    'DW Brake Calipers 4 Release Button
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If hpu_a = 1 Or hpu_b = 1 Then
            If (dw_on = 0 And cal_sum > 2) Or (dw_on = 1 And (low_clutch = 1 Or high_clutch = 1)) Then
                Delay(1)
                Using brush4 As New SolidBrush(Color.White)
                    g.FillRectangle(brush4, 227, 359, 7, 25) 'DW Brakes 4
                End Using
                Button3.Enabled = False
                Button3.Visible = False
                Button23.Enabled = True
                Button23.Visible = True

                TextBox3.Text = TextBox7.Text - 4
                cal4 = 0
                cal_sum = cal12 + cal3 + cal4 + cal56
                If cal_sum = 0 Then
                    Button37.Enabled = True
                    Button37.Visible = True
                    Button38.Enabled = False
                    Button38.Visible = False
                End If
            End If
        End If
    End Sub

    'DW Brake Calipers 4 Apply Button
    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        If cal4 = 0 Then
            Delay(1)
            Using brush4 As New SolidBrush(Color.LawnGreen)
                g.FillRectangle(brush4, 227, 359, 7, 25) 'DW Brakes 4
            End Using
            cal4 = 1
            Button23.Enabled = False
            Button23.Visible = False
            Button3.Enabled = True
            Button3.Visible = True

            TextBox3.Text = 0.0
            cal_sum = cal12 + cal3 + cal4 + cal56
            If cal_sum = 4 Then
                Button38.Enabled = True
                Button38.Visible = True
                Button37.Enabled = False
                Button37.Visible = False
            End If
        End If
    End Sub

    'DW Brake Calipers 5,6 Release Button
    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        If hpu_a = 1 Or hpu_b = 1 Then
            If (dw_on = 0 And cal_sum > 2) Or (dw_on = 1 And (low_clutch = 1 Or high_clutch = 1)) Then
                Delay(1)
                Using brush4 As New SolidBrush(Color.White)
                    g.FillRectangle(brush4, 227, 432, 7, 25) 'DW Brakes 5,6
                    g.FillRectangle(brush4, 248, 432, 7, 25) 'DW Brakes 5,6
                End Using
                Button19.Enabled = False
                Button19.Visible = False
                Button24.Enabled = True
                Button24.Visible = True

                TextBox6.Text = TextBox7.Text - 4
                cal56 = 0
                cal_sum = cal12 + cal3 + cal4 + cal56
                If cal_sum = 0 Then
                    Button37.Enabled = True
                    Button37.Visible = True
                    Button38.Enabled = False
                    Button38.Visible = False
                End If
            End If
        End If
    End Sub

    'DW Brake Calipers 5,6 Apply Button
    Private Sub Button24_Click(sender As Object, e As EventArgs) Handles Button24.Click
        If cal56 = 0 Then
            Delay(1)
            Using brush4 As New SolidBrush(Color.LawnGreen)
                g.FillRectangle(brush4, 227, 432, 7, 25) 'DW Brakes 5,6
                g.FillRectangle(brush4, 248, 432, 7, 25) 'DW Brakes 5,6
            End Using
            cal56 = 1
            Button24.Enabled = False
            Button24.Visible = False
            Button19.Enabled = True
            Button19.Visible = True

            TextBox6.Text = 0.0
            cal_sum = cal12 + cal3 + cal4 + cal56
            If cal_sum = 4 Then
                Button38.Enabled = True
                Button38.Visible = True
                Button37.Enabled = False
                Button37.Visible = False
            End If
        End If
    End Sub

    'EMERGENCY STOP BUTTON IS PRESSED -MANUAL MODE
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Try
            emergency_stop.e_stop_active()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Using brush4 As New SolidBrush(Color.LawnGreen)
            g.FillRectangle(brush4, 227, 432, 7, 25) 'DW Brakes 5,6
            g.FillRectangle(brush4, 248, 432, 7, 25) 'DW Brakes 5,6
            g.FillRectangle(brush4, 227, 359, 7, 25) 'DW Brakes 4
            g.FillRectangle(brush4, 388, 359, 7, 25) 'DW Brakes 3
            g.FillRectangle(brush4, 388, 432, 7, 25) 'DW Brakes 1,2
            g.FillRectangle(brush4, 367, 432, 7, 25) 'DW Brakes 1,2
        End Using

        Using brush3 As New SolidBrush(Color.White)
            g.FillEllipse(brush3, 700, 160, 60, 60) 'DW A Motor
            g.FillEllipse(brush3, 700, 270, 60, 60) 'DW B Motor
            g.FillEllipse(brush3, 700, 370, 60, 60) 'DW C Motor
            g.FillEllipse(brush3, 600, 160, 60, 60) 'DW A Blower Motor
            g.FillEllipse(brush3, 600, 270, 60, 60) 'DW B Blower Motor
            g.FillEllipse(brush3, 600, 370, 60, 60) 'DW C Blower Motor
            g.FillEllipse(brush3, 110, 680, 40, 40) 'HPU Pump A
            g.FillEllipse(brush3, 410, 680, 40, 40) 'HPU Pump B
            g.FillEllipse(brush3, 740, 800, 40, 40) 'Lube Oil Pump
        End Using

        Dim myFont As Font
        Dim myBrush As Brush
        myBrush = New Drawing.SolidBrush(Color.Black)
        myFont = New System.Drawing.Font("Times New Roman", 24)
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 173) 'DW A 
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 283) 'DW B
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 382) 'DW C
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 170) 'DW A Blower
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 283) 'DW B Blower
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 381) 'DW C Blower

    End Sub

    'EMERGENCY STOP RELEASE BUTTON IS PRESSED -MANUAL MODE
    Private Sub Button25_Click(sender As Object, e As EventArgs) Handles Button25.Click
        Try
            emergency_stop.e_stop_release()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'Manual Mode UP Button
    Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
        If dw_on = 1 And lube_oil = 1 And cal_sum = 0 Then
            If TextBox8.Text < roof_saver - 5 Then
                If low_clutch = 1 Then
                    TextBox8.Text += 2.8
                ElseIf high_clutch = 1 Then
                    TextBox8.Text += 5
                End If
            End If
        End If

    End Sub

    'Manual Mode Down Button
    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        If dw_on = 1 And lube_oil = 1 And cal_sum = 0 Then
            If TextBox8.Text > floor_saver + 5 Then
                If low_clutch = 1 Then
                    TextBox8.Text -= 2.8
                ElseIf high_clutch = 1 Then
                    TextBox8.Text -= 5
                End If
            End If
        End If
    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label16.Text = TimeOfDay
    End Sub



    'Brake Calipers Release Button
    Private Sub Button38_Click(sender As Object, e As EventArgs) Handles Button38.Click
        If hpu_a = 1 Or hpu_b = 1 Then
            If dw_on = 1 And (low_clutch = 1 Or high_clutch = 1) And cal_sum <> 0 Then
                Delay(1)
                Using brush4 As New SolidBrush(Color.White)
                    g.FillRectangle(brush4, 388, 432, 7, 25) 'DW Brakes 1,2
                    g.FillRectangle(brush4, 367, 432, 7, 25) 'DW Brakes 1,2
                    g.FillRectangle(brush4, 388, 359, 7, 25) 'DW Brakes 3
                    g.FillRectangle(brush4, 227, 359, 7, 25) 'DW Brakes 4
                    g.FillRectangle(brush4, 227, 432, 7, 25) 'DW Brakes 5,6
                    g.FillRectangle(brush4, 248, 432, 7, 25) 'DW Brakes 5,6
                End Using
                Button16.Enabled = False
                Button16.Visible = False
                Button21.Enabled = True
                Button21.Visible = True

                Button15.Enabled = False
                Button15.Visible = False
                Button22.Enabled = True
                Button22.Visible = True

                Button3.Enabled = False
                Button3.Visible = False
                Button23.Enabled = True
                Button23.Visible = True

                Button19.Enabled = False
                Button19.Visible = False
                Button24.Enabled = True
                Button24.Visible = True

                TextBox5.Text = TextBox7.Text - 4
                TextBox4.Text = TextBox7.Text - 4
                TextBox3.Text = TextBox7.Text - 4
                TextBox6.Text = TextBox7.Text - 4



                cal56 = 0
                cal4 = 0
                cal3 = 0
                cal12 = 0
                cal_sum = cal12 + cal3 + cal4 + cal56

                Button38.Enabled = False
                Button38.Visible = False
                Button37.Enabled = True
                Button37.Visible = True
            End If
        End If
    End Sub

    'Brake Calipers Apply Button
    Private Sub Button37_Click(sender As Object, e As EventArgs) Handles Button37.Click
        cal_sum = cal12 + cal3 + cal4 + cal56
        If cal_sum <> 4 Then
            Delay(1)
            Using brush4 As New SolidBrush(Color.LawnGreen)
                g.FillRectangle(brush4, 388, 432, 7, 25) 'DW Brakes 1,2
                g.FillRectangle(brush4, 367, 432, 7, 25) 'DW Brakes 1,2
                g.FillRectangle(brush4, 388, 359, 7, 25) 'DW Brakes 3
                g.FillRectangle(brush4, 227, 359, 7, 25) 'DW Brakes 4
                g.FillRectangle(brush4, 227, 432, 7, 25) 'DW Brakes 5,6
                g.FillRectangle(brush4, 248, 432, 7, 25) 'DW Brakes 5,6

            End Using
            cal12 = 1
            cal3 = 1
            cal4 = 1
            cal56 = 1
            Button21.Enabled = False
            Button21.Visible = False
            Button16.Enabled = True
            Button16.Visible = True

            Button22.Enabled = False
            Button22.Visible = False
            Button15.Enabled = True
            Button15.Visible = True

            Button23.Enabled = False
            Button23.Visible = False
            Button3.Enabled = True
            Button3.Visible = True

            Button24.Enabled = False
            Button24.Visible = False
            Button19.Enabled = True
            Button19.Visible = True


            TextBox5.Text = 0.0
            TextBox4.Text = 0.0
            TextBox3.Text = 0.0
            TextBox6.Text = 0.0

            cal_sum = cal12 + cal3 + cal4 + cal56
            Button37.Enabled = False
            Button37.Visible = False
            Button38.Enabled = True
            Button38.Visible = True
        End If
    End Sub




    'DW Auto Mode 

    'Auto Mode Selected
    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        GroupBox4.Show()
        GroupBox1.Hide()
        GroupBox5.Hide()
        GroupBox1.Enabled = False
        GroupBox5.Enabled = False
        GroupBox4.Enabled = True
        If low_clutch = 1 Then
            Button36.BackColor = Color.Green
        ElseIf high_clutch = 1 Then
            Button35.BackColor = Color.Green
        End If
    End Sub


    'DW ON button- Auto Mode
    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        If dw_on = 0 And e_stop = 0 Then

            Try
                hpu_obj.hpu_a_start()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            Using brush3 As New SolidBrush(Color.LawnGreen)
                g.FillEllipse(brush3, 110, 680, 40, 40) 'HPU Pump A
            End Using

            If lube_oil = 0 Then
                Delay(0.5)
                Using brush3 As New SolidBrush(Color.LawnGreen)
                    g.FillEllipse(brush3, 740, 800, 40, 40) 'Lube Oil Pump
                End Using
                Button5.Enabled = False
                Button5.Visible = False
                Button14.Enabled = True
                Button14.Visible = True
                While TextBox9.Text < 88
                    TextBox9.Text += 11
                End While
                lube_oil = 1
            End If

        End If

        If dw_on = 0 And lube_oil = 1 And (hpu_a = 1 Or hpu_b = 1) Then
            Delay(0.5)
            Using brush3 As New SolidBrush(Color.LawnGreen)
                g.FillEllipse(brush3, 700, 160, 60, 60) 'DW A Motor
                g.FillEllipse(brush3, 700, 270, 60, 60) 'DW B Motor
                g.FillEllipse(brush3, 700, 370, 60, 60) 'DW C Motor
                g.FillEllipse(brush3, 600, 160, 60, 60) 'DW A Blower Motor
                g.FillEllipse(brush3, 600, 270, 60, 60) 'DW B Blower Motor
                g.FillEllipse(brush3, 600, 370, 60, 60) 'DW C Blower Motor

            End Using
            Dim myFont As Font
            Dim myBrush As Brush
            myBrush = New Drawing.SolidBrush(Color.Black)
            myFont = New System.Drawing.Font("Times New Roman", 24)
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 173) 'DW A 
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 283) 'DW B
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 382) 'DW C
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 170) 'DW A Blower
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 283) 'DW B Blower
            Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 381) 'DW C Blower
            dw_on = 1
            Button34.Enabled = False
            Button34.Visible = False
            Button31.Enabled = True
            Button31.Visible = True


        End If
    End Sub

    'DW Off button- Auto Mode
    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        If dw_on = 1 Then
            If cal_sum > 1 Then
                Delay(1)
                Using brush3 As New SolidBrush(Color.White)
                    g.FillEllipse(brush3, 700, 160, 60, 60) 'DW A Motor
                    g.FillEllipse(brush3, 700, 270, 60, 60) 'DW B Motor
                    g.FillEllipse(brush3, 700, 370, 60, 60) 'DW C Motor
                    g.FillEllipse(brush3, 600, 160, 60, 60) 'DW A Blower Motor
                    g.FillEllipse(brush3, 600, 270, 60, 60) 'DW B Blower Motor
                    g.FillEllipse(brush3, 600, 370, 60, 60) 'DW C Blower Motor

                End Using
                Dim myFont As Font
                Dim myBrush As Brush
                myBrush = New Drawing.SolidBrush(Color.Black)
                myFont = New System.Drawing.Font("Times New Roman", 24)
                Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 173) 'DW A 
                Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 283) 'DW B
                Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 382) 'DW C
                Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 170) 'DW A Blower
                Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 283) 'DW B Blower
                Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 381) 'DW C Blower
                dw_on = 0
                Button31.Enabled = False
                Button31.Visible = False
                Button34.Enabled = True
                Button34.Visible = True
            End If

        End If

        If dw_on = 0 Then
            Delay(1)
            Using brush2 As New SolidBrush(Color.White)
                g.FillEllipse(brush2, 740, 800, 40, 40) 'Lube Oil Pump
            End Using
            Button5.Enabled = True
            Button5.Visible = True
            Button14.Enabled = False
            Button14.Visible = False
            While TextBox9.Text <> 0
                TextBox9.Text -= 11
            End While
            If low_lube_oil_press = 1 Then
                l_oil.low_lube(88.0)
            End If
            lube_oil = 0

            If TextBox2.Text > 130 Or TextBox1.Text > 130 Then



                high_clutch = 0
                low_clutch = 0
                TextBox2.Text = 0.0
                TextBox1.Text = 0.0
                Button36.BackColor = Color.Gainsboro
                Button35.BackColor = Color.Gainsboro
            End If
        End If
    End Sub

    'E-stop button- Auto Mode
    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        Try
            emergency_stop.e_stop_active()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Using brush4 As New SolidBrush(Color.LawnGreen)
            g.FillRectangle(brush4, 227, 432, 7, 25) 'DW Brakes 5,6
            g.FillRectangle(brush4, 248, 432, 7, 25) 'DW Brakes 5,6
            g.FillRectangle(brush4, 227, 359, 7, 25) 'DW Brakes 4
            g.FillRectangle(brush4, 388, 359, 7, 25) 'DW Brakes 3
            g.FillRectangle(brush4, 388, 432, 7, 25) 'DW Brakes 1,2
            g.FillRectangle(brush4, 367, 432, 7, 25) 'DW Brakes 1,2
        End Using
        Using brush3 As New SolidBrush(Color.White)
            g.FillEllipse(brush3, 700, 160, 60, 60) 'DW A Motor
            g.FillEllipse(brush3, 700, 270, 60, 60) 'DW B Motor
            g.FillEllipse(brush3, 700, 370, 60, 60) 'DW C Motor
            g.FillEllipse(brush3, 600, 160, 60, 60) 'DW A Blower Motor
            g.FillEllipse(brush3, 600, 270, 60, 60) 'DW B Blower Motor
            g.FillEllipse(brush3, 600, 370, 60, 60) 'DW C Blower Motor
            g.FillEllipse(brush3, 110, 680, 40, 40) 'HPU Pump A
            g.FillEllipse(brush3, 410, 680, 40, 40) 'HPU Pump B
            g.FillEllipse(brush3, 740, 800, 40, 40) 'Lube Oil Pump
        End Using
        Dim myFont As Font
        Dim myBrush As Brush
        myBrush = New Drawing.SolidBrush(Color.Black)
        myFont = New System.Drawing.Font("Times New Roman", 24)
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 173) 'DW A 
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 283) 'DW B
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 712, 382) 'DW C
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 170) 'DW A Blower
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 283) 'DW B Blower
        Me.CreateGraphics.DrawString("M", myFont, myBrush, 612, 381) 'DW C Blower
    End Sub

    'E-stop Release button- Auto Mode
    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        Try
            emergency_stop.e_stop_release()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'DW Brakes On Button -Auto Mode
    Public Sub Button40_Click(sender As Object, e As EventArgs) Handles Button40.Click
        cal_sum = cal12 + cal3 + cal4 + cal56
        If cal_sum <> 4 Then
            Delay(1)
            Using brush4 As New SolidBrush(Color.LawnGreen)
                g.FillRectangle(brush4, 388, 432, 7, 25) 'DW Brakes 1,2
                g.FillRectangle(brush4, 367, 432, 7, 25) 'DW Brakes 1,2
                g.FillRectangle(brush4, 388, 359, 7, 25) 'DW Brakes 3
                g.FillRectangle(brush4, 227, 359, 7, 25) 'DW Brakes 4
                g.FillRectangle(brush4, 227, 432, 7, 25) 'DW Brakes 5,6
                g.FillRectangle(brush4, 248, 432, 7, 25) 'DW Brakes 5,6

            End Using
            cal12 = 1
            cal3 = 1
            cal4 = 1
            cal56 = 1
            Button21.Enabled = False
            Button21.Visible = False
            Button16.Enabled = True
            Button16.Visible = True

            Button22.Enabled = False
            Button22.Visible = False
            Button15.Enabled = True
            Button15.Visible = True

            Button23.Enabled = False
            Button23.Visible = False
            Button3.Enabled = True
            Button3.Visible = True

            Button24.Enabled = False
            Button24.Visible = False
            Button19.Enabled = True
            Button19.Visible = True


            TextBox5.Text = 0.0
            TextBox4.Text = 0.0
            TextBox3.Text = 0.0
            TextBox6.Text = 0.0

            cal_sum = cal12 + cal3 + cal4 + cal56
            Button37.Enabled = False
            Button37.Visible = False
            Button38.Enabled = True
            Button38.Visible = True

            Button40.Enabled = False
            Button40.Visible = False
            Button39.Enabled = True
            Button39.Visible = True
        End If
    End Sub

    'DW Brakes Off Button -Auto Mode
    Private Sub Button39_Click(sender As Object, e As EventArgs) Handles Button39.Click
        If hpu_a = 1 Or hpu_b = 1 Then
            If dw_on = 1 And (low_clutch = 1 Or high_clutch = 1) And cal_sum <> 0 Then
                Delay(1)
                Using brush4 As New SolidBrush(Color.White)
                    g.FillRectangle(brush4, 388, 432, 7, 25) 'DW Brakes 1,2
                    g.FillRectangle(brush4, 367, 432, 7, 25) 'DW Brakes 1,2
                    g.FillRectangle(brush4, 388, 359, 7, 25) 'DW Brakes 3
                    g.FillRectangle(brush4, 227, 359, 7, 25) 'DW Brakes 4
                    g.FillRectangle(brush4, 227, 432, 7, 25) 'DW Brakes 5,6
                    g.FillRectangle(brush4, 248, 432, 7, 25) 'DW Brakes 5,6
                End Using
                Button16.Enabled = False
                Button16.Visible = False
                Button21.Enabled = True
                Button21.Visible = True

                Button15.Enabled = False
                Button15.Visible = False
                Button22.Enabled = True
                Button22.Visible = True

                Button3.Enabled = False
                Button3.Visible = False
                Button23.Enabled = True
                Button23.Visible = True

                Button19.Enabled = False
                Button19.Visible = False
                Button24.Enabled = True
                Button24.Visible = True

                TextBox5.Text = TextBox7.Text - 4
                TextBox4.Text = TextBox7.Text - 4
                TextBox3.Text = TextBox7.Text - 4
                TextBox6.Text = TextBox7.Text - 4



                cal56 = 0
                cal4 = 0
                cal3 = 0
                cal12 = 0
                cal_sum = cal12 + cal3 + cal4 + cal56

                Button38.Enabled = False
                Button38.Visible = False
                Button37.Enabled = True
                Button37.Visible = True

                Button39.Enabled = False
                Button39.Visible = False
                Button40.Enabled = True
                Button40.Visible = True
            End If
        End If
    End Sub

    'DW Low Clutch Activation Button -Auto Mode
    Private Sub Button36_Click(sender As Object, e As EventArgs) Handles Button36.Click
        clutch_obj.low_clutch()
    End Sub

    'DW High Clutch Activation Button -Auto Mode
    Private Sub Button35_Click(sender As Object, e As EventArgs) Handles Button35.Click
        clutch_obj.high_clutch()
    End Sub

    'Auto Mode UP Button
    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        If dw_on = 1 And lube_oil = 1 And cal_sum = 0 Then
            If TextBox8.Text > roof_saver - 17 Then
                If TextBox8.Text < roof_saver - 4 Then
                    TextBox8.Text += 0.3
                    roof_saver_active = 1
                    Alarms_Page.find_ID()
                    roof_saver_active_id = Alarms_Page.max + 1
                    Alarms_Page.insert_alarms(roof_saver_active_id, "Roof Saver Active", "DW", "Active")
                End If
            ElseIf TextBox8.Text < floor_saver + 15 Then
                If TextBox8.Text > floor_saver + 5 Then
                    TextBox8.Text -= 0.3
                    If floor_saver_active = 0 Then
                        floor_saver_active = 1
                        Alarms_Page.find_ID()
                        floor_saver_active_id = Alarms_Page.max + 1
                        Alarms_Page.insert_alarms(floor_saver_active_id, "Floor Saver Active", "DW", "Active")
                    End If
                End If
            Else
                If low_clutch = 1 Then
                    TextBox8.Text += 2.8
                ElseIf high_clutch = 1 Then
                    TextBox8.Text += 5
                End If
                If floor_saver_active = 1 Then
                    Alarms_Page.update_alarm(floor_saver_active_id, "Floor Saver Active", "DW", "Resolved")
                End If
            End If

        End If
    End Sub

    'Auto Mode Down Button
    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        If dw_on = 1 And lube_oil = 1 And cal_sum = 0 Then
            If TextBox8.Text > roof_saver - 17 Then
                If TextBox8.Text < roof_saver - 4 Then
                    TextBox8.Text -= 0.3
                    If roof_saver_active = 0 Then
                        roof_saver_active = 1
                        Alarms_Page.find_ID()
                        roof_saver_active_id = Alarms_Page.max + 1
                        Alarms_Page.insert_alarms(roof_saver_active_id, "Roof Saver Active", "DW", "Active")
                    End If
                End If

            ElseIf TextBox8.Text < floor_saver + 15 Then
                If TextBox8.Text > floor_saver + 5 Then
                    TextBox8.Text -= 0.3
                    floor_saver_active = 1
                    Alarms_Page.find_ID()
                    floor_saver_active_id = Alarms_Page.max + 1
                    Alarms_Page.insert_alarms(floor_saver_active_id, "Floor Saver Active", "DW", "Active")
                End If
            Else
                If low_clutch = 1 Then
                    TextBox8.Text += 2.8
                ElseIf high_clutch = 1 Then
                    TextBox8.Text += 5
                End If
                If roof_saver_active = 1 Then
                    Alarms_Page.update_alarm(low_hpu_press_id, "Roof Saver Active", "DW", "Resolved")
                End If
            End If

        End If
    End Sub

    'HPU Pump A Start Button -Auto Mode
    Private Sub Button44_Click(sender As Object, e As EventArgs) Handles Button44.Click
        Try
            hpu_obj.hpu_a_start()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Using brush3 As New SolidBrush(Color.LawnGreen)
            g.FillEllipse(brush3, 110, 680, 40, 40) 'HPU Pump A
        End Using
    End Sub

    'HPU Pump A Stop Button -Auto Mode
    Private Sub Button42_Click(sender As Object, e As EventArgs) Handles Button42.Click
        Try
            If hpu_a = 1 And (dw_on = 0 Or cal_sum > 2) Then
                Delay(1)
                Using brush3 As New SolidBrush(Color.White)
                    g.FillEllipse(brush3, 110, 680, 40, 40) 'HPU Pump A
                End Using
                hpu_obj.hpu_a_stop()
                If low_hpu_press = 1 Or low_low_hpu_press = 1 Then
                    Alarms_Page.update_alarm(low_hpu_press_id, "Low HPU Pressure", "DW_HPU", "Resolved")
                    Alarms_Page.update_alarm(brakes_applied_id, "DW_Brakes Applied(Low HPU Pressure)", "DW_HPU", "Resolved")
                    low_hpu_press = 0
                    low_low_hpu_press = 0
                    hpu_obj.hpu_press_normal(0)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'HPU Pump B Start Button -Auto Mode
    Private Sub Button43_Click(sender As Object, e As EventArgs) Handles Button43.Click
        Try
            hpu_obj.hpu_b_start()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Using brush3 As New SolidBrush(Color.LawnGreen)
            g.FillEllipse(brush3, 410, 680, 40, 40) 'HPU Pump B
        End Using
    End Sub

    'HPU Pump B Stop Button -Auto Mode
    Private Sub Button41_Click(sender As Object, e As EventArgs) Handles Button41.Click
        Try
            If hpu_b = 1 And (dw_on = 0 Or cal_sum > 2) Then
                Delay(1)
                Using brush3 As New SolidBrush(Color.White)
                    g.FillEllipse(brush3, 410, 680, 40, 40) 'HPU Pump B
                End Using
                hpu_obj.hpu_b_stop()
                If low_hpu_press = 1 Or low_low_hpu_press = 1 Then
                    Alarms_Page.update_alarm(low_hpu_press_id, "Low HPU Pressure", "DW_HPU", "Resolved")
                    Alarms_Page.update_alarm(brakes_applied_id, "DW_Brakes Applied(Low HPU Pressure)", "DW_HPU", "Resolved")
                    low_hpu_press = 0
                    low_low_hpu_press = 0
                    hpu_obj.hpu_press_normal(0)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    'FAULT SIMULATION MODE SELECTED
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        GroupBox5.Show()
        GroupBox1.Hide()
        GroupBox4.Hide()
        GroupBox5.Enabled = True
        GroupBox4.Enabled = False
        GroupBox1.Enabled = False
    End Sub


    'LOW AIR PRESSURE BUTTON
    Private Sub Button59_Click(sender As Object, e As EventArgs) Handles Button59.Click
        TextBox10.Text = CInt(Math.Ceiling(Rnd() * 120)) + 1

        If low_air_press = 0 Then
            Delay(0.5)
            Alarms_Page.find_ID()
            low_air_press_id = Alarms_Page.max + 1
            Alarms_Page.insert_alarms(low_air_press_id, "Low Air Pressure", "DW", "Active")
            low_air_press = 1
            Button59.Enabled = False
            Button59.Visible = False
            Button46.Enabled = True
            Button46.Visible = True
        End If
    End Sub

    'AIR PRESSURE NORMAL BUTTON 
    Private Sub Button46_Click(sender As Object, e As EventArgs) Handles Button46.Click
        airpress.air_press_normal()
    End Sub

    'LOW LUBE OIL PRESSURE BUTTON
    Private Sub Button45_Click(sender As Object, e As EventArgs) Handles Button45.Click
        If lube_oil = 1 And low_lube_oil_press = 0 Then
            TextBox9.Text = CInt(Math.Ceiling(Rnd() * 70)) + 1
            Delay(0.5)
            low_lube_oil_press_id = Alarms_Page.DataGridView1.RowCount
            Alarms_Page.insert_alarms(low_lube_oil_press_id, "Low Lube Oil Pressure", "DW_LUBE", "Active")
            low_lube_oil_press = 1
            Button45.Enabled = False
            Button45.Visible = False
            Button28.Enabled = True
            Button28.Visible = True
        End If
    End Sub

    'LUBE OIL PRESSURE NORMAL BUTTON
    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        l_oil.low_lube(88.0)
    End Sub

    Private Sub Button51_Click(sender As Object, e As EventArgs) Handles Button51.Click
        Try
            If (hpu_a = 1 Or hpu_b = 1) And low_hpu_press = 0 Then
                TextBox7.Text = CInt(Math.Ceiling(Rnd() * 150)) + 1

                If cal_sum <> 4 And TextBox7.Text < 120 Then
                    Delay(1)
                    Using brush4 As New SolidBrush(Color.LawnGreen)
                        g.FillRectangle(brush4, 388, 432, 7, 25) 'DW Brakes 1,2
                        g.FillRectangle(brush4, 367, 432, 7, 25) 'DW Brakes 1,2
                        g.FillRectangle(brush4, 388, 359, 7, 25) 'DW Brakes 3
                        g.FillRectangle(brush4, 227, 359, 7, 25) 'DW Brakes 4
                        g.FillRectangle(brush4, 227, 432, 7, 25) 'DW Brakes 5,6
                        g.FillRectangle(brush4, 248, 432, 7, 25) 'DW Brakes 5,6

                    End Using
                    hpu_obj.hpu_low_low_press()
                    Alarms_Page.find_ID()
                    brakes_applied_id = Alarms_Page.max + 1
                    Alarms_Page.insert_alarms(brakes_applied_id, "DW_Brakes Applied(Low Low HPU Pressure)", "DW_HPU", "Active")
                    low_low_hpu_press = 1
                End If
                hpu_obj.hpu_low_press()
                low_hpu_press = 1
                Alarms_Page.find_ID()
                low_hpu_press_id = Alarms_Page.max + 1
                Alarms_Page.insert_alarms(low_hpu_press_id, " Low HPU Pressure", "DW_HPU", "Active")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Button49_Click(sender As Object, e As EventArgs) Handles Button49.Click
        Try
            If low_hpu_press = 1 Or low_low_hpu_press = 1 Then
                Delay(1)
                Alarms_Page.update_alarm(low_hpu_press_id, "Low HPU Pressure", "DW_HPU", "Resolved")
                Alarms_Page.update_alarm(brakes_applied_id, "DW_Brakes Applied(Low HPU Pressure)", "DW_HPU", "Resolved")
                low_hpu_press = 0
                low_low_hpu_press = 0
                hpu_obj.hpu_press_normal(165)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class