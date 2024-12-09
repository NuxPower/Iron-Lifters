Public Class AutoCloseMessageBox
    Inherits Form

    Private WithEvents closeTimer As Timer

    Public Sub New(message As String, title As String, displayTime As Integer)
        Me.Text = title
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Size = New Size(300, 150)

        Dim label As New Label()
        label.Text = message
        label.Dock = DockStyle.Fill
        label.TextAlign = ContentAlignment.MiddleCenter
        Me.Controls.Add(label)

        closeTimer = New Timer()
        closeTimer.Interval = displayTime
        AddHandler closeTimer.Tick, AddressOf OnTimedEvent
        closeTimer.Start()
    End Sub

    Private Sub OnTimedEvent(source As Object, e As EventArgs)
        closeTimer.Stop()
        Me.Close()
    End Sub
End Class
