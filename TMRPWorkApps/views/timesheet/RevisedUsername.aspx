<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RevisedUsername.aspx.vb" Inherits="TMRPWorkApps.RevisedUsername" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Unmatched Username</title>

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
                            <div class="card">
                                <div class="card-header">
                                    <div class="d-flex align-items-center">
                                        <div class="flex-grow-1 overflow-hidden">
                                            <h6>Fix My Username</h6> 
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="mb-3">
                                            <div class="alert alert-warning alert-dismissible fade show" role="alert">
                                                Your username is not match. Please input your JDE Number and click update
                                            </div>
                                            <h6>JDE No</h6>
                                            <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="tjdeno" AutoCompleteType="Disabled"></asp:TextBox>
                                        </div>
                                        <div class="mb-3">
                                            <asp:LinkButton runat="server" CssClass="btn btn-sm btn-soft-primary" ID="bfix" OnClick="bfix_Click">
                                                <i class="fa fa-wrench"></i> Update
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
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
</body>
</html>