Imports System.Net
Imports System.IO

Public Class ACSPushNotifications
    Private Cookie As CookieContainer
    Private AcsUserID As String
    Private AcsPassword As String
    Private ApplicationKey As String

    Sub New(AppKey As String, AcsUID As String, AcsPwd As String)
        AcsUserID = AcsUID
        AcsPassword = AcsPwd
        ApplicationKey = AppKey

        'Set Cookie
        ACSLogin(AcsUserID, AcsPassword)
    End Sub
    'channel As String, idList As String, payload As String, cookie As CookieContainer
    Public Function Notify(n As Notification) As String
        Dim request As HttpWebRequest = HttpWebRequest.Create(New Uri("https://api.cloud.appcelerator.com/v1/push_notification/notify.json?key=" & ApplicationKey & "&pretty_json=true" & n.Channel & n.IDList & n.Payload()))

        request.Method = "POST"
        request.ContentType = "application/json"
        request.CookieContainer = Cookie

        Dim dataStream As Stream = request.GetRequestStream()

        'Close the Stream object.
        dataStream.Close()

        'Get the response.
        Dim response As WebResponse = request.GetResponse()

        'Get the stream containing content returned by the server.
        dataStream = response.GetResponseStream()

        'Open the stream using a StreamReader for easy access.
        Dim reader As StreamReader = New StreamReader(dataStream)

        'Read the content.
        Dim responseFromServer As String = reader.ReadToEnd()

        'Clean up the streams.
        reader.Close()
        dataStream.Close()
        response.Close()

        Return responseFromServer
    End Function

    Private Sub ACSLogin(acsuid As String, acspwd As String)
        Dim request As HttpWebRequest = HttpWebRequest.Create(New Uri("https://api.cloud.appcelerator.com/v1/users/login.json?key=" & ApplicationKey & "&login=" & acsuid & "&password=" & acspwd & "&pretty_json=true"))
        request.Method = "POST"
        request.Accept = "*/*"
        request.ContentType = "application/json"
        request.CookieContainer = New CookieContainer

        'Get the request stream.
        Dim dataStream As Stream = request.GetRequestStream()

        'Close the Stream object.
        dataStream.Close()

        'Get the response.
        Dim response As WebResponse = request.GetResponse()

        'Get the stream containing content returned by the server.
        dataStream = response.GetResponseStream()

        'Open the stream using a StreamReader for easy access.
        Dim reader As StreamReader = New StreamReader(dataStream)

        'Read the content.
        Dim responseFromServer As String = reader.ReadToEnd()

        'Clean up the streams.
        reader.Close()
        dataStream.Close()
        response.Close()
        Cookie = request.CookieContainer
    End Sub
End Class

Public Structure NotificationPayload
    Dim title As String
    Dim alert As String
End Structure

Public Class Notification
    Sub New()

    End Sub
    Private _channel As String
    Public Property Channel() As String
        Get
            Return "&channel=" & _channel
        End Get
        Set(ByVal Value As String)
            _channel = Value
        End Set
    End Property

    Private _idlist As String
    Public Property IDList() As String
        Get
            Return "&to_ids=" & _idlist
        End Get
        Set(ByVal Value As String)
            _idlist = Value
        End Set
    End Property

    Public Property NotificationPayload As NotificationPayload

    Public Function Payload()
        Return "&payload={'alert':'" & NotificationPayload.alert & "','title':'" & NotificationPayload.title & "','vibrate':true,'sound':'default'}"
    End Function

End Class
