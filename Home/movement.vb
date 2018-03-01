Public Class movement

    Const floor_saver As Single = 4.57
    Const roof_saver As Single = 117.16

    'THIS PROCEDURE ALLOWS FOR THE UPWARD MOVEMENT OF THE DRAWWORKS
    Public Sub up_movement()
        If DW.dw_on = 1 And DW.lube_oil = 1 And DW.cal_sum = 0 Then
            If DW.TextBox8.Text > roof_saver - 17 Then
                If DW.TextBox8.Text < roof_saver - 4 Then
                    DW.TextBox8.Text += 0.3
                    DW.roof_saver_active = 1

                    If DW.rf <> 1 And DW.roof_saver_active = 1 Then
                        Alarms_Page.find_ID()
                        DW.roof_saver_active_id = Alarms_Page.max + 1
                        Alarms_Page.insert_alarms(DW.roof_saver_active_id, "Roof Saver Active", "DW", "Active")
                        DW.rf = 1
                    End If
                End If

            ElseIf DW.TextBox8.Text < floor_saver + 15 Then
                If DW.TextBox8.Text > floor_saver Then
                    DW.TextBox8.Text += 0.3
                End If
            Else
                If DW.low_clutch = 1 Then
                    DW.TextBox8.Text += 2.8
                ElseIf DW.high_clutch = 1 Then
                    DW.TextBox8.Text += 5
                End If
                If DW.floor_saver_active = 1 Then
                    DW.floor_saver_active = 0
                    Alarms_Page.update_alarm(DW.floor_saver_active_id, "Floor Saver Active", "DW", "Resolved")
                    DW.flr = 0
                End If
            End If

            End If
    End Sub

    'THIS PROCEDURE ALLOWS FOR THE DOWNWARDS MOVEMENT OF THE DRAWWORKS
    Public Sub down_movement()
        If DW.dw_on = 1 And DW.lube_oil = 1 And DW.cal_sum = 0 Then
            If DW.TextBox8.Text < (floor_saver + 15) Then
                If DW.TextBox8.Text > (floor_saver + 3) Then
                    DW.TextBox8.Text -= 0.3
                    DW.floor_saver_active = 1
                    If DW.flr <> 1 And DW.floor_saver_active = 1 Then
                        Alarms_Page.find_ID()
                        DW.floor_saver_active_id = Alarms_Page.max + 1
                        Alarms_Page.insert_alarms(DW.floor_saver_active_id, "Floor Saver Active", "DW", "Active")
                        DW.flr = 1
                    End If
                End If

            ElseIf DW.TextBox8.Text > roof_saver - 17 Then
                If DW.TextBox8.Text < roof_saver - 2 Then
                    DW.TextBox8.Text -= 0.3
                End If

            Else
                If DW.roof_saver_active = 1 Then
                    DW.roof_saver_active = 0
                    Alarms_Page.update_alarm(DW.roof_saver_active_id, "Roof Saver Active", "DW", "Resolved")
                    DW.rf = 0
                End If
                If DW.low_clutch = 1 Then
                    DW.TextBox8.Text -= 2.8
                ElseIf DW.high_clutch = 1 Then
                    DW.TextBox8.Text -= 5
                End If
            End If

        End If
    End Sub
End Class
