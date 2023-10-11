Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Security.Policy
Imports TMRPWorkApps.SQLFunction

Public Class Utility
    Public Shared Function evar(ByVal val As Object, ByVal valtype As Integer, Optional vallen As Integer = 255) As String
        Dim eval As String

        val = Replace(val, """", "")
        val = Replace(val, "#N/A", "")
        val = LTrim(val)
        val = RTrim(val)


        If val Is vbNullString Then
            eval = "NULL"
        ElseIf CStr(val) = "" Then
            eval = "NULL"
        ElseIf CStr(val) = " " Then
            eval = "NULL"
        ElseIf CStr(val) = String.Empty Then
            eval = "NULL"
        Else
            val = Trim(val)
            Select Case valtype
                Case 0
                    'eval = val
                    If IsNumeric(val) = True Then
                        eval = val
                    Else
                        eval = "NULL"
                    End If
                Case 2
                    If IsDate(val) = False Then
                        eval = "NULL"
                    Else
                        val = CDate(val)
                        eval = "'" & DatePart("yyyy", val) & "-" & DatePart("m", val) & "-" & DatePart("d", val) & "'"
                    End If
                Case 3
                    If IsDate(val) = False Then
                        eval = "NULL"
                    Else
                        val = CDate(val)
                        eval = "'" & DatePart("yyyy", val) & "-" & DatePart("m", val) & "-" & DatePart("d", val) & " " & DatePart("h", val) & ":" & DatePart("n", val) & ":" & DatePart("s", val) & "'"
                    End If

                Case 4 : eval = "'" & Left(val, 3750) & "'"

                Case 11 : eval = "'%" & Left(val, vallen) & "%'"
                Case 12 : eval = "'" & Replace(val, ",", "','") & "'"
                Case 13 : eval = "'%," & Left(val, vallen) & ",%'"
                Case 14 : eval = "" & val & ""

                Case Else : eval = "'" & Left(val, vallen) & "'"


            End Select


        End If

        Return eval

    End Function

    Public Shared Sub BindDataDropDown(ByVal ObjName As DropDownList, ByVal query As String, ByVal ObjText As String, ByVal ObjVal As String)
        Dim dt As New DataTable
        dt = SQLFunction.GetDataTable(query)
        If dt.Rows.Count = 0 Then
            ObjName.Items.Clear()
            ObjName.Items.Insert(0, New ListItem("No Data Available !"))
        Else
            ObjName.DataSource = dt
            ObjName.DataTextField = ObjText
            ObjName.DataValueField = ObjVal
            ObjName.DataBind()
            ObjName.Items.Insert(0, New ListItem(""))
        End If
    End Sub

    Public Shared Sub BindDataDropDownV2(ByVal ObjName As DropDownList, ByVal dt As DataTable, ByVal ObjText As String, ByVal ObjVal As String)
        If dt.Rows.Count = 0 Then
            ObjName.Items.Clear()
            ObjName.Items.Insert(0, New ListItem("No Data Available !"))
        Else
            ObjName.DataSource = dt
            ObjName.DataTextField = ObjText
            ObjName.DataValueField = ObjVal
            ObjName.DataBind()
            ObjName.Items.Insert(0, New ListItem(""))
        End If
    End Sub

    Public Shared Sub BindDataListBox(ByVal ObjName As ListBox, ByVal query As String, ByVal ObjText As String, ByVal ObjVal As String)
        Dim dt As New DataTable
        dt = SQLFunction.GetDataTable(query)
        ObjName.DataSource = dt
        ObjName.DataTextField = ObjText
        ObjName.DataValueField = ObjVal
        ObjName.DataBind()
    End Sub

    'Private Function ExportData(ByVal dt As DataTable, ByVal filename As String)
    '    Dim workbook As New ClosedXML.Excel.XLWorkbook()
    '    Dim worksheet = workbook.Worksheets.Add("Data")
    '    Dim headerRow = worksheet.Row(1)
    '    headerRow.Style.Fill.BackgroundColor = XLColor.FromHtml("#2596be")
    '    headerRow.Style.Font.FontColor = XLColor.White
    '    headerRow.Style.Font.Bold = True
    '    Dim columnNumber As Integer = 1
    '    For Each column As DataColumn In dt.Columns
    '        worksheet.Cell(1, columnNumber).Value = column.ColumnName
    '        columnNumber += 1
    '    Next
    '    Dim rowNumber As Integer = 2
    '    For Each row As DataRow In dt.Rows
    '        columnNumber = 1
    '        For Each column As DataColumn In dt.Columns
    '            Dim value As Object = row(column.ColumnName)
    '            If column.DataType Is GetType(Integer) Then
    '                If Not IsDBNull(value) Then
    '                    worksheet.Cell(rowNumber, columnNumber).Value = Convert.ToInt32(value)
    '                End If
    '            Else
    '                If Not IsDBNull(value) Then
    '                    worksheet.Cell(rowNumber, columnNumber).Value = value.ToString()
    '                End If
    '            End If
    '            columnNumber += 1
    '        Next
    '        rowNumber += 1
    '    Next
    '    Response.Clear()
    '    Response.Buffer = True
    '    Response.Charset = ""
    '    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    '    Response.AddHeader("content-disposition", "attachment;filename=" & filename & ".xlsx")
    '    Using memoryStream As New System.IO.MemoryStream()
    '        workbook.SaveAs(memoryStream)
    '        memoryStream.WriteTo(Response.OutputStream)
    '        memoryStream.Close()
    '    End Using
    '    Response.Flush()
    '    Response.End()
    'End Function

    Public Shared Function CheckDBNull(ByVal value As Object) As String
        If value Is DBNull.Value OrElse value Is Nothing Then
            Return "-"
        Else
            Return value
        End If
    End Function

    Public Shared Function CheckDBNullv1(ByVal value As Object) As String
        If value Is DBNull.Value OrElse value Is Nothing Then
            Return ""
        Else
            Return value
        End If
    End Function

    Public Shared Function GetCurrentPageName() As String
        Dim currentPage As Page = DirectCast(HttpContext.Current.Handler, Page)
        Return Path.GetFileName(currentPage.AppRelativeVirtualPath)
    End Function

    Public Shared Function GetCurrentMethodName() As String
        Dim st As New System.Diagnostics.StackTrace()
        Dim sf As System.Diagnostics.StackFrame = st.GetFrame(1)
        Return sf.GetMethod().Name
    End Function

    Public Shared Function IsLoggedIn(ByVal session As HttpSessionState, ByVal user As System.Security.Principal.IPrincipal) As Boolean
        'Check if session variable is set
        If session("UserID") IsNot Nothing AndAlso session("UserName") IsNot Nothing Then
            Return True
        End If

        'If session variable is not set, check if user is already authenticated
        If user.Identity.IsAuthenticated Then
            'Set session variables
            session("UserID") = user.Identity.Name
            session("UserName") = user.Identity.Name
            Return True
        End If

        Return False
    End Function

    Public Shared Function varfilter(F As String) As String
        If Len(F) > 0 Then varfilter = " WHERE " & Right(F, Len(F) - 4)
    End Function

    Public Shared Function generateRandom() As String
        Dim random As New Random()
        Dim length As Integer = 10 ' Panjang string yang diinginkan
        Dim sb As New StringBuilder()

        Dim possibleChars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"

        For i As Integer = 0 To length - 1
            ' Mengambil karakter acak dari kumpulan karakter yang mungkin
            Dim randomChar As Char = possibleChars(random.Next(possibleChars.Length))
            sb.Append(randomChar)
        Next

        Dim randomString As String = sb.ToString()

        Return randomString
    End Function

    Public Shared Function genSwalAlert(ByVal type As String, ByVal msg As String, ByVal vpage As Page)
        Dim script As String
        script = "Swal.fire('','" & msg & "','" & type & "')"
        ScriptManager.RegisterStartupScript(vpage, vpage.GetType(), "Swal", script, True)
        Return 0
    End Function

    Public Shared Function StartWorkNoWO(ByVal eWorkStatusID As Integer, ByVal eShift As Integer, ByVal euserid As String, ByVal eJobNo As String)
        Dim dt As New DataTable
        Dim query As String = "Select WorkStatus,ISNULL(CostCode,0) as CostCode from tbl_TCRPWorkStatusSheet Where WOrkStatusID=" & eWorkStatusID
        dt = GetDataTable(query)

        Dim ecostcode As String = GetDataFromColumn(dt, "CostCode")
        Dim eActivityID As String = "NULL"
        Dim eIDWorkOrder As String = "NULL"
        Dim eSubCompID As String = "NULL"
        Dim eSubActivityID As String = "NULL"
        Dim eNote As String = "NULL"
        Dim eCLockType As String = "Web Apps"

        StartWorkAct(eWorkStatusID, euserid, eShift, ecostcode, eIDWorkOrder, eActivityID, eJobNo, eSubActivityID, eNote, eCLockType)
        Return 0
    End Function

    Public Shared Function StartWorkAct(ByVal eWorkStatusID As String, ByVal eUserID As String, ByVal eShift As String, ByVal eCostCode As String, ByVal eIDWorkOrder As String,
                                    ByVal eActivityID As String, ByVal ejobno As String, ByVal eSubActivityID As String, ByVal eNote As String, ByVal eCLockType As String)

        Dim query As String = "exec dbo.ClockInLabour1 " _
                                & evar(eUserID, 1) & "," _
                                & evar(eShift, 0) & "," _
                                & eWorkStatusID _
                                & "," & evar(eCostCode, 1) & "," _
                                    & eIDWorkOrder _
                                    & "," & eActivityID & "," _
                                    & evar(ejobno, 1) _
                                    & "," & eSubActivityID & "," _
                                    & eNote _
                                    & "," & evar(eCLockType, 1) _
                                    & "," & evar(eusername, 1)
        executeQuery(query)
        Return 0

    End Function

    Public Shared Function eusername() As String
        Dim t As String = HttpContext.Current.Request.LogonUserIdentity.Name.ToString()
        Dim n As String = InStr(t, "\")
        t = Right(t, Len(t) - n)

        Return t
    End Function
End Class