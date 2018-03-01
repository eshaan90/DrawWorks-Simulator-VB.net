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

    Private Sub dw_documentation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Console.WriteLine(AxAcroPDF1.LoadFile("C:\Users\evkirpal\Downloads\CSC522_HW2\CSC522_HW2"))
    End Sub
End Class