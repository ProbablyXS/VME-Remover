Public Class Form1

    Dim p() As Process
    Dim result As String
    Dim memory As New Memory.Mem

    Public Sub GETMODULEBASADDRESS()

        Dim myProcess As New Process()

        Dim myProcessStartInfo As New ProcessStartInfo("voicemeeter8.exe")

        myProcess.StartInfo = myProcessStartInfo

        myProcess.Start()
        System.Threading.Thread.Sleep(1000)
        Dim myProcessModule As ProcessModule

        Dim myProcessModuleCollection As ProcessModuleCollection = myProcess.Modules


        myProcessModule = myProcessModuleCollection.Item(0)

        result = myProcessModule.BaseAddress.ToString

    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GETMODULEBASADDRESS()

        TextBox1.Text = TextBox1.Text & "Program detection launch in progress." & vbCrLf

        p = Process.GetProcessesByName("voicemeeter8")
        If p.Count > 0 Then

            TextBox1.Text = TextBox1.Text & "Program open." & vbCrLf

        Else

            TextBox1.Text = TextBox1.Text & "Program close. Thank to retry again." & vbCrLf

            TextBox1.Text = TextBox1.Text & "Closing program running." & vbCrLf


            Await Task.Delay(5000)

        End If

        TextBox1.Text = TextBox1.Text & "Injection in progress" & vbCrLf



        Try

            memory.OpenProcess("voicemeeter8")
            memory.writeMemory("voicemeeter8.exe+102A4C", "int", "0")
            Timer1.Start()

        Catch ex As Exception

        End Try


        TextBox1.Text = TextBox1.Text & "Injection complete" & vbCrLf
        Await Task.Delay(1000)
        Me.Hide()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        WriteNOPs("voicemeeter8", result + &H25D324, "90")


    End Sub

    Private Sub ShowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowToolStripMenuItem.Click

        Me.Show()

    End Sub

    Private Sub HideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideToolStripMenuItem.Click

        Me.Hide()

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        Application.Exit()

    End Sub
End Class
