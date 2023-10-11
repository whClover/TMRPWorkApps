Imports System.Web.DynamicData
Imports TMRPWorkApps.SQLFunction
Imports TMRPWorkApps.GlobalString
Imports TMRPWorkApps.Utility

Public Class RevisedUsername
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub bfix_Click(sender As Object, e As EventArgs)
        Dim system_username As String = eusername()
        Dim db_username As String
        Dim dt As New DataTable
        Dim ejdeno As String = tjdeno.Text
        If ejdeno = String.Empty Then
            genSwalAlert("warning", "Please fill JDE No", Me)
            Exit Sub
        End If

        Dim query As String = "select * from tbl_user where userid=" & evar(ejdeno, 1)
        dt = GetDataTable(query)
        If dt.Rows.Count = 0 Then
            genSwalAlert("warning", "JDE Number is not registered, Please contact Admin", Me)
            Exit Sub
        End If

        db_username = GetDataFromColumn(dt, "Username")
        If db_username <> system_username Then
            Dim qryupdate As String = "update tbl_user set username=" & evar(system_username, 1) & " where userid=" & evar(ejdeno, 1)
            executeQuery(qryupdate)
        End If
        Response.Redirect(urlTMRP_TimesheetForm)
    End Sub

End Class