Public Class Home

    'Draworks Home Page Button Activated
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DW.Show()
        Me.Hide()

    End Sub

    'Alarms Page Button Activated
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Alarms_Page.Show()
        Me.Hide()
    End Sub

    'Return to Login Button Activated
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Login_Page.Show()
        Me.Hide()
    End Sub

    'Tutorial Videos Button Activated
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Video_Tutorial.Show()
        Me.Hide()
    End Sub
End Class
