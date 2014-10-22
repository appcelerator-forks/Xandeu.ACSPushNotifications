ACSPushNotifications
====================

Basic .NET functionality for calling ACS Rest API for Push

Usage in VB.NET:

    Dim push As New ACSPushNotifications("<ApplicationKey>", "<ACSUserID>", "<ACSPassword")
    Dim n As New Notification With {.Channel = "<ChannelName>", _
                                    .IDList = "<Comma Delimited List of UserIDs>", _
                                    .NotificationPayload = New NotificationPayload With {.alert = "<Push Alert>", _
                                                                                         .title = "<Push Title>"}
                                    }
    responseBox.Text = push.Notify(n)

Usage in C#:
    Coming Soon.
