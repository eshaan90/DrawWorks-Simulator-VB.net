Public Class Alarms_Page

    'HOME BUTTON CLICKED
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Home.Show()
        Me.Hide()
    End Sub

    'DRAWWORKS BUTTON CLICKED
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DW.Show()
        Me.Hide()
    End Sub


    Private access As New dbcontrol
    Dim i As Integer = 0
    Public max As Integer = 0

    'ERROR HANDLING BLOCK
    Public Function NoErrors(Optional report As Boolean = False)
        If Not String.IsNullOrEmpty(access.Exception) Then
            If report = True Then MsgBox(access.Exception) 'Report Errors
            Return False
        Else
            Return True
        End If
    End Function

    'This block runs when the Alarms_Page. It deletes the alarms from the database that were in the acknowledged state.
    Public Sub Alarms_Page_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        RefreshGrid()
        Try
            For Each R As DataRow In access.DBDT.Rows
                If R("Alarm_Status") = "Acknowledged" Then
                    i = R("Alarms_ID")
                    delete_alarm()
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    'THIS BLOCK REFRESHES THE CONTENT IN THE DATAGRIDVIEW OF THE ALARMS_PAGE
    Public Sub RefreshGrid()
        'RUN QUERY
        access.ExecQuery("SELECT * FROM alarms")
        If NoErrors(True) = False Then Exit Sub

        'FILL DATAGRID
        DataGridView1.DataSource = access.DBDT

    End Sub

    'THIS BLOCK FINDS THE UNIQUE ALARM ID
    Public Sub find_ID()
        Try
            For Each R As DataRow In access.DBDT.Rows
                If R("Alarms_ID") > max Then
                    max = R("Alarms_ID")
                End If
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


    'THIS BLOCK INSERTS RECORDS INTO THE ALARMS DATABASE
    Public Sub insert_alarms(x As Integer, str1 As String, str2 As String, str3 As String)

        access.AddParam("@alarm_id", x)
        access.AddParam("@alarm_date", Date.Today)
        access.AddParam("@alarm_time", TimeOfDay)
        access.AddParam("@alarm_desc", str1)
        access.AddParam("@unit_name", str2)
        access.AddParam("@alarm_status", str3)

        'EXECUTE INSERT COMMAND
        access.ExecQuery("INSERT INTO alarms(Alarms_ID,Alarms_Date,Alarms_Time,Alarms_Description,Unit_Name,Alarm_Status) " &
                     "VALUES (@alarm_id,@alarm_date,@alarm_time,@alarm_desc,@unit_name,@alarm_status); ")

        'REPORT AND ABORT ON ERRORS
        If NoErrors(True) = False Then Exit Sub

        'CLEAN UP AND REFRESH OUR DATAGRID VIEW
        RefreshGrid()
    End Sub


    'THIS BLOCK UPDATES THE ALARMS DATABASE
    Public Sub update_alarm(i As Integer, str1 As String, str2 As String, str3 As String)
        access.AddParam("@alarm_id", i)
        access.AddParam("@alarm_date", Date.Today)
        access.AddParam("@alarm_time", TimeOfDay)
        access.AddParam("@alarm_desc", str1)
        access.AddParam("@unit_name", str2)
        access.AddParam("@alarm_status", str3)
        access.ExecQuery("UPDATE alarms SET Alarms_ID=@alarm_ID,Alarms_Date=@alarm_date,Alarms_Time=@alarm_time,Alarms_Description=@alarm_desc,Unit_Name=@unit_name,Alarm_Status=@alarm_status  WHERE Alarms_ID=@alarm_id")

        'REPORT AND ABORT ON ERRORS
        If NoErrors(True) = False Then Exit Sub

        'CLEAN UP AND REFRESH OUR DATAGRID VIEW
        RefreshGrid()
    End Sub

    'THIS BLOCK DELETES RECORDS FROM THE ALARMS DATABASE
    Public Sub delete_alarm()
        access.AddParam("@alarm_id", i)
        access.ExecQuery("DELETE FROM alarms WHERE Alarms_ID = @alarm_id;")

        'REPORT AND ABORT ON ERRORS
        If NoErrors(True) = False Then Exit Sub

        'CLEAN UP AND REFRESH OUR DATAGRID VIEW
        RefreshGrid()
    End Sub

    'ACK ONE BUTTON
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.Item(5, DataGridView1.CurrentRow.Index).Value = "Resolved" Then
            i = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
            delete_alarm()
        ElseIf DataGridView1.Item(5, DataGridView1.CurrentRow.Index).Value = "Active" Then
            'Update the value to acknowledged
            i = DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value
            update_alarm(i, DataGridView1.Item(3, DataGridView1.CurrentRow.Index).Value, DataGridView1.Item(4, DataGridView1.CurrentRow.Index).Value, "Acknowledged")
        End If
    End Sub

    'THIS BLOCK ACTIVATES THE ACK ONE BUTTON
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        Button1.Enabled = True

    End Sub
End Class