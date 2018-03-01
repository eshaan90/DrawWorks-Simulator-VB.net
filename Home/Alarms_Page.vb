Public Class Alarms_Page

    Public str As String = "0:0"

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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim CurrentDateTime As DateTime
        CurrentDateTime = DateTime.Now
        str = CurrentDateTime.ToString("hh:mm:ss  tt")
    End Sub

    Private access As New dbcontrol
    Dim i As Integer = 0
    Public max As Integer = 0

    'ERROR HANDLING PROCEDURE
    Public Function NoErrors(Optional report As Boolean = False)
        If Not String.IsNullOrEmpty(access.Exception) Then
            If report = True Then MsgBox(access.Exception) 'Report Errors
            Return False
        Else
            Return True
        End If
    End Function

    'THIS PROCEDURE RUNS WHEN THE ALARMS_PAGE IS SHOWN. IT DELETES THE ALARMS FROM THE DATABASE THAT WERE IN THE ACKNOWLEDGED STATE. 
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
        Timer1.Start()
    End Sub


    'THIS PROCEDURE REFRESHES THE CONTENT IN THE DATAGRIDVIEW OF THE ALARMS_PAGE
    Public Sub RefreshGrid()
        'RUN QUERY
        access.ExecQuery("SELECT * FROM alarms")
        If NoErrors(True) = False Then Exit Sub

        'FILL DATAGRID
        DataGridView1.DataSource = access.DBDT

    End Sub

    'THIS PROCEDURE FINDS THE UNIQUE ALARM ID
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


    'THIS PROCEDURE INSERTS RECORDS INTO THE ALARMS DATABASE
    Public Sub insert_alarms(x As Integer, str1 As String, str2 As String, str3 As String)
        'Dim str As String = TimeOfDay
        access.AddParam("@alarm_id", x)
        access.AddParam("@alarm_date", Date.Today)
        access.AddParam("@alarm_time", str)
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


    'THIS PROCEDURE UPDATES THE ALARMS DATABASE
    Public Sub update_alarm(i As Integer, str1 As String, str2 As String, str3 As String)
        'Dim str As String = TimeOfDay
        access.AddParam("@alarm_id", i)
        access.AddParam("@alarm_date", Date.Today)
        access.AddParam("@alarm_time", str)
        access.AddParam("@alarm_desc", str1)
        access.AddParam("@unit_name", str2)
        access.AddParam("@alarm_status", str3)
        access.ExecQuery("UPDATE alarms SET Alarms_ID=@alarm_ID,Alarms_Date=@alarm_date,Alarms_Time=@alarm_time,Alarms_Description=@alarm_desc,Unit_Name=@unit_name,Alarm_Status=@alarm_status  WHERE Alarms_ID=@alarm_id")

        'REPORT AND ABORT ON ERRORS
        If NoErrors(True) = False Then Exit Sub

        'CLEAN UP AND REFRESH OUR DATAGRID VIEW
        RefreshGrid()
    End Sub


    'THIS PROCEDURE DELETES RECORDS FROM THE ALARMS DATABASE
    Public Sub delete_alarm()
        access.AddParam("@alarm_id", i)
        access.ExecQuery("DELETE FROM alarms WHERE Alarms_ID = @alarm_id;")

        'REPORT AND ABORT ON ERRORS
        If NoErrors(True) = False Then Exit Sub

        'CLEAN UP AND REFRESH OUR DATAGRID VIEW
        RefreshGrid()
    End Sub

    'THIS PROCEDURE ACTIVATES THE ACK ONE BUTTON
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex < 0 Or e.ColumnIndex < 0 Then Exit Sub
        Button1.Enabled = True

    End Sub


    'ACK ONE BUTTON
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            acknowledge(DataGridView1.CurrentRow.Index)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    'ACK ALL BUTTON
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            'DataGridView1.Rows.GetFirstRow()
            Dim x As Integer = 0
            'For x = 0 To DataGridView1.RowCount - 1

            'Next

            Do While x < DataGridView1.Rows.Count
                acknowledge(x)
                x += 1
            Loop
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    'THE ACKNOWLEDGE FUNCTION THAT EXECTUTES THE ACKNOWLEDGE COMMAND
    Private Sub acknowledge(x As Integer)

        If DataGridView1.Item(5, x).Value = "Resolved" Then
            i = DataGridView1.Item(0, x).Value
            delete_alarm()
        ElseIf DataGridView1.Item(5, x).Value = "Active" Then
            'Update the value to acknowledged
            i = DataGridView1.Item(0, x).Value
            update_alarm(i, DataGridView1.Item(3, x).Value, DataGridView1.Item(4, x).Value, "Acknowledged")
        End If
    End Sub

    Private Sub Alarms_Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub


End Class