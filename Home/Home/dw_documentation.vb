Public Class dw_documentation

    'HOME BUTTON PRESSED
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Home.Show()
        Me.Hide()
    End Sub

    'DRAWWORKS BUTTON PRESSED
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DW.Show()
        Me.Hide()
    End Sub
End Class