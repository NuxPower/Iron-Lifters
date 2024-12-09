Imports System.IO
Imports MySql.Data.MySqlClient

Module GlobalModule
    Property conn As New MySqlConnection("Server=localhost; Database=gym management system; User ID=root; Password=dengue43")
    Property reader As MySqlDataReader
    Property imageList As New ImageList
    Private resizeTimers As New Dictionary(Of ListView, Timer)
    Sub dbConn()
        If conn IsNot Nothing AndAlso conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
    End Sub
    Sub dbDisconn()
        If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then
            conn.Close()
        End If
    End Sub
    Sub SaveCardImage(cardImage As Bitmap, uid As String)
        Dim customPath As String = "E:\Iron Lifters Cards"

        If Not Directory.Exists(customPath) Then
            Directory.CreateDirectory(customPath)
        End If

        Dim fileName As String = Path.Combine(customPath, "Card_" & uid & ".jpeg")
        cardImage.Save(fileName, Imaging.ImageFormat.Jpeg)
    End Sub

    Function CreateCardImage(qrCodeImage As Bitmap, fullName As String, uid As String) As Bitmap
        Dim cardWidth As Integer = 400
        Dim cardHeight As Integer = 200
        Dim cardImage As New Bitmap(cardWidth, cardHeight)

        Using g As Graphics = Graphics.FromImage(cardImage)
            g.Clear(Color.White)

            ' Draw QR code
            Dim qrCodeX As Integer = 10
            Dim qrCodeY As Integer = 15
            Dim qrCodeWidth As Integer = 170
            Dim qrCodeHeight As Integer = 170
            g.DrawImage(qrCodeImage, qrCodeX, qrCodeY, qrCodeWidth, qrCodeHeight)

            ' Draw Full Name
            Dim fullNameFont As New Font("Arial", 12, FontStyle.Bold)
            Dim fullNameX As Integer = 170
            Dim fullNameY As Integer = 40
            g.DrawString(fullName, fullNameFont, Brushes.Black, fullNameX, fullNameY)

            ' Draw UID
            Dim uidFont As New Font("Arial", 10)
            Dim uidX As Integer = 170
            Dim uidY As Integer = 70
            g.DrawString("UID: " & uid, uidFont, Brushes.Black, uidX, uidY)
        End Using

        Return cardImage
    End Function

    Sub ListView1_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs)
        e.DrawDefault = True
    End Sub

    Public Sub ListView1_Resize(sender As Object, e As EventArgs)
        Dim listView As ListView = DirectCast(sender, ListView)
        Dim totalWidth As Integer = listView.ClientSize.Width

        Dim firstColumnWidth As Integer = listView.Columns(0).Width
        Dim otherColumnsWidth As Integer = listView.Columns.Cast(Of ColumnHeader)().Skip(1).Take(listView.Columns.Count - 2).Sum(Function(c) c.Width)

        Dim lastColumnWidth As Integer = Math.Max(100, totalWidth - firstColumnWidth - otherColumnsWidth)

        listView.Columns(listView.Columns.Count - 1).Width = lastColumnWidth
    End Sub

    Sub InitializeImageList(lv As ListView)
        imageList.Images.Add("active", Image.FromFile("C:\Users\Asus\OneDrive\Desktop\eaaaaa\icons\User.png"))
        imageList.Images.Add("disabled", Image.FromFile("C:\Users\Asus\OneDrive\Desktop\eaaaaa\icons\UserDisabled.png"))
        'imageList.Images.Add("selected", Image.FromFile("C:\Users\Asus\OneDrive\Desktop\icons\UserSelected.png"))
        lv.SmallImageList = imageList
    End Sub
End Module
