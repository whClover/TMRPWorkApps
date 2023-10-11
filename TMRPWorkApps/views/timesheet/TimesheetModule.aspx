<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TimesheetModule.aspx.vb" Inherits="TMRPWorkApps.TimesheetModule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Timesheet - ClockIn Module</title>

    <link href="~/assets/css/bootstrap.min.css" rel="stylesheet" runat="server" />
    <link href="~/assets/css/icons.min.css" rel="stylesheet" runat="server" />
    <link href="~/assets/css/app.min.css" rel="stylesheet" runat="server" />
    <link href="~/assets/costume/css/mycss.css" rel="stylesheet" runat="server" />
    <link href="~/assets/libs/choices.js/public/assets/styles/choices.min.css" rel="stylesheet" runat="server" />
    <link href="~/assets/libs/flatpickr/flatpickr.min.css" rel="stylesheet" runat="server" />
    <link href="~/assets/costume/alertify/css/alertify.min.css" rel="stylesheet" runat="server" />
    <link href="~/assets/costume/toast/toastr.min.css" rel="stylesheet" runat="server" />
    <link href="~/assets/costume/summernote/summernote-bs4.min.css" rel="stylesheet" runat="server" />
    <link href="~/assets/libs/sweetalert2/sweetalert2.min.css" rel="stylesheet" runat="server" />
    <script src="/assets/costume/colorbox/jquery.colorbox-min.js"></script>
    <script src="/assets/libs/sweetalert2/sweetalert2.min.js"></script>

    <!-- quill css -->
    <link href="~/assets/libs/quill/quill.core.css" rel="stylesheet" type="text/css" runat="server" />
    <link href="~/assets/libs/quill/quill.bubble.css" rel="stylesheet" type="text/css" runat="server" />
    <link href="~/assets/libs/quill/quill.snow.css" rel="stylesheet" type="text/css" runat="server" />


    <asp:PlaceHolder runat="server">
        <%= Scripts.Render("~/buatmodal") %>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server">
        <%= Scripts.Render("~/summernote") %>
    </asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
        <div class="authentication-bg min-vh-100">
            <div class="bg-overlay bg-white"></div>
            <div class="container">
                <div class="d-flex flex-column min-vh-100 px-3 pt-4">
                    <div class="row justify-content-center my-auto">
                        <div class="col-md-10 col-lg-8 col-xl-6">
                            <div class="mb-2">
                                <h4 runat="server" id="hwelcome" class="text-primary fw-bold">JDE No. 663456</h4>
                            </div>
                            <div class="card">
                                <div class="card-body">
                                    <div class="col-md-12">
                                        <div class="alert alert-primary alert-dismissible fade show" role="alert">
                                            <i class="fa fa-user me-2"></i>
                                            Account Information
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <dl class="dl-horizontal">
                                                    <dt>JDE No</dt>
                                                    <dd runat="server" id="sJDENo">#</dd>
                                                    <dt>Username</dt>
                                                    <dd runat="server" id="sUsername">#</dd>
                                                    <dt>Full Name</dt>
                                                    <dd runat="server" id="ssFullName">#</dd>
                                                </dl>
                                            </div>
                                            <div class="col-md-6">
                                                <dl class="dl-horizontal">
                                                    <dt>Supervisor Name</dt>
                                                    <dd runat="server" id="sSupvName">#</dd>
                                                    <dt>Crew Name</dt>
                                                    <dd runat="server" id="tCrew">#</dd>
                                                    <dt>Job Cost</dt>
                                                    <dd runat="server" id="tJobCost">#</dd>
                                                </dl>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-footer">
                                    <div class="row">
                                        <div class="alert alert-primary alert-dismissible fade show" role="alert">
                                            <i class="fa fa-calendar me-2"></i>
                                            Date & Time Information
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <dl class="dl-horizontal">
                                                <dt>Date</dt>
                                                <dd runat="server" id="sDate">12 October 2023</dd>
                                            </dl>
                                        </div>
                                        <div class="col-md-6">
                                            <dl class="dl-horizontal">
                                                <dt>Time</dt>
                                                <dd runat="server" id="sTime">12:30 PM</dd>
                                            </dl>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row">
                                        <dl class="dl-horizontal">
                                            <dt>Shift</dt>
                                            <dd runat="server" id="Dd1">
                                                <asp:DropDownList runat="server" CssClass="form-control form-control-sm" ID="ddshift">
                                                    <asp:ListItem Value="1" Text="Day"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Night"></asp:ListItem>
                                                </asp:DropDownList>
                                            </dd>
                                        </dl>
                                    </div>
                                    <div class="d-flex align-items-lg-center gap-2">
                                        <asp:LinkButton runat="server" CssClass="btn btn-soft-primary flex-fill" ID="btoolbox" OnClick="btoolbox_Click" OnClientClick="return CheckDouble();">
                                            <i class="fa fa-users"></i> Toolbox
                                        </asp:LinkButton>
                                        <asp:LinkButton runat="server" CssClass="btn btn-soft-primary flex-fill" ID="boffschedule" OnClick="boffschedule_Click" OnClientClick="return CheckDouble();">
                                            <i class="fa fa-home"></i> Off Schedule
                                        </asp:LinkButton>
                                    </div>
                                    <hr />
                                    <asp:GridView CssClass="table align-middle table-check table-sm" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Founds..." ID="gvTS" ShowHeaderWhenEmpty="true">
                                        <Columns>
                                            <asp:BoundField HeaderText="Job Name" DataField="Job" HeaderStyle-CssClass="bg-soft-primary" ItemStyle-CssClass="text-primary" />
                                            <asp:BoundField HeaderText="Start Time" DataField="StartTime" HeaderStyle-CssClass="bg-soft-primary" />
                                            <asp:BoundField HeaderText="Stop Time" DataField="StopTime" HeaderStyle-CssClass="bg-soft-primary" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xl-12">
                            <div class="text-center text-muted p-4">
                                <p class="mb-0">&copy; <script>document.write(new Date().getFullYear())</script> Dashonic. Crafted with <i class="mdi mdi-heart text-danger"></i> by Plant System</p>
                            </div>
                        </div><!-- end col -->
                    </div><!-- end row -->
                </div>
            </div>
        </div>
    </form>
    <script src="/assets/libs/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="/assets/libs/metismenujs/metismenujs.min.js"></script>
    <script src="/assets/libs/simplebar/simplebar.min.js"></script>
    <script src="/assets/libs/feather-icons/feather.min.js"></script>
    <script src="/assets/libs/flatpickr/flatpickr.min.js"></script>
    <script src="/assets/libs/choices.js/public/assets/scripts/choices.min.js"></script>
    <script src="/assets/js/pages/form-advanced.init.js"></script>
    <script src="/assets/js/app.js"></script>
    
    <!-- ckeditor -->
    <script src="/assets/libs/@ckeditor/ckeditor5-build-classic/build/ckeditor.js"></script>
    <script src="/assets/libs/quill/quill.min.js"></script>

    <script type="text/javascript">
        function updateTime() {
            var now = new Date();
            var hours = now.getHours().toString().padStart(2, '0');
            var minutes = now.getMinutes().toString().padStart(2, '0');
            var seconds = now.getSeconds().toString().padStart(2, '0');
            var formattedTime = hours + ':' + minutes + ':' + seconds;
            sTime.innerText = formattedTime;
        }

        // Panggil updateTime setiap detik
        setInterval(updateTime, 1000);

    </script>

    <script type="text/javascript">
        var submit = 0;
        function CheckDouble() {
            if (++submit > 1) {
                alert('This sometimes takes a few seconds - please be patient.');
                return false;
            }
        }
    </script>
</body>
</html>