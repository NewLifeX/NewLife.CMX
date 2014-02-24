<%@ Application Language="C#" %>
<script RunAt="server">
    void Session_Start(object sender, EventArgs e)
    {
        string SessionId = Session.SessionID;
    }
</script>
