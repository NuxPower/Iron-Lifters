Module AdminLoggedModule
    Public LoggedInFID As Integer
    Public LoginInDateTime As DateTime
    Public LoginOutDateTime As DateTime
    Public LoggedInAccessLevel As Integer
    Public LoggedInFALID As Integer

    ' Method to update logout datetime
    Public Sub SetLogoutTime()
        LoginOutDateTime = DateTime.Now
    End Sub
End Module
