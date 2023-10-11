Imports TMRPWorkApps.SQLFunction
Imports TMRPWorkApps.Utility
Imports TMRPWorkApps.GlobalString

Public Class TimesheetModule
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            gvTS.DataSource = New List(Of String)
            gvTS.DataBind()

            bindingheader()
            BindingData()
        End If
    End Sub

    Sub bindingheader()
        Dim euname As String = eusername()
        Dim dt As New DataTable
        Dim query As String = "select * from vw_TCRPUserInformation where username=" & evar(eusername, 1)
        dt = GetDataTable(query)
        If dt.Rows.Count = 0 Then
            Response.Redirect(urlTMRP_Revisuname)
            Exit Sub
        Else
            hwelcome.InnerText = "JDE No. " & GetDataFromColumn(dt, "UserID")
            sJDENo.InnerText = GetDataFromColumn(dt, "UserID")
            sUsername.InnerText = GetDataFromColumn(dt, "Username")
            ssFullName.InnerText = GetDataFromColumn(dt, "FullName")
            sSupvName.InnerText = GetDataFromColumn(dt, "SpvFullName")
            tCrew.InnerText = GetDataFromColumn(dt, "GroupName")
            tJobCost.InnerText = GetDataFromColumn(dt, "JobCost")

            sDate.InnerText = Now.ToString("dd MMMM yyyy")

        End If
    End Sub

    Sub BindingData()
        Dim dt As New DataTable
        Dim query As String = "select Job,CONVERT(varchar(5), StartTime, 108) as StartTime, CONVERT(varchar(5), StopTime, 108) as StopTime
            from v_TCRPDailyTimeSheet where username=" & evar(eusername, 1) & " and DailyDate >= convert(varchar,getdate(),23) order by starttime desc"
        dt = GetDataTable(query)
        gvTS.DataSource = dt
        gvTS.DataBind()
    End Sub

    Protected Sub btoolbox_Click(sender As Object, e As EventArgs)
        checkusername(sJDENo.InnerText)

        Dim eshift As String = ddshift.Text
        Dim ejobno As String = tJobCost.InnerText
        Dim ejdeno As String = sJDENo.InnerText

        ' Memanggil fungsi StartWorkNoWO atau tugas lain yang diperlukan
        StartWorkNoWO(2, eshift, ejdeno, ejobno)
        genSwalAlert("success", "Clockin Toolbox Success", Me)

        BindingData()
    End Sub

    Protected Sub boffschedule_Click(sender As Object, e As EventArgs)
        checkusername(sJDENo.InnerText)

        Dim eshift As String = ddshift.Text
        Dim ejobno As String = tJobCost.InnerText
        Dim ejdeno As String = sJDENo.InnerText

        ' Memanggil fungsi StartWorkNoWO atau tugas lain yang diperlukan
        StartWorkNoWO(15, eshift, ejdeno, ejobno)
        genSwalAlert("success", "Clockin Off Schedule Success", Me)

        BindingData()
    End Sub

    Sub checkusername(ByVal euserid As String)
        Dim euname_sys As String = eusername()
        Dim dt As New DataTable
        Dim query As String = "select * from tbl_user where userid=" & evar(euserid, 1)
        dt = GetDataTable(query)

        Dim euname_db As String = GetDataFromColumn(dt, "username")
        If euname_sys <> euname_db Then
            Response.Redirect(urlTMRP_Revisuname)
            Exit Sub
        End If
    End Sub
End Class