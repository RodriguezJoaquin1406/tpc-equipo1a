<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Iniciar sesión - Boutique Mi Sueño</title>

    <style>
        .login-card {
            width: 100%;
            border-radius: 12px;
            box-shadow: 2px 2px 8px 2px rgba(0, 0, 0, 0.5);
            background-color: #fff;
        }

            .login-card .card-body {
                padding: 28px;
            }

        .brand-title {
            font-family: "Epilogue", system-ui, -apple-system, "Segoe UI", Roboto, "Helvetica Neue", Arial;
            font-weight: 800;
            letter-spacing: -0.02em;
            color: rgba(0,0,0,0.8);
        }

        .btn-primary-custom {
            background-color: #D3C5AA;
            border-color: #D3C5AA;
            color: #2b2315;
            margin-top: 10px;
            margin-bottom: 10px;
        }

            .btn-primary-custom:hover {
                background-color: #c1b092;
                border-color: #c1b092;
            }

        .muted-accent {
            color: #7d715d;
        }

        .divider {
            height: 1px;
            background-color: rgba(191,174,152,0.25);
        }
    </style>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex align-items-center justify-content-center min-vh-100 py-5">
        <div class="w-100" style="max-width: 960px;">
            <div class="row justify-content-center">



                <div class="col-12 col-md-8 col-lg-6">

                    <%--// Cartel alerta de bootstrap "escondido" para usuario que intente cargar productos a su carrito sin haber iniciado sesion.--%>
                    <asp:Panel ID="pnlAlerta" runat="server" Visible="false" CssClass="alert alert-warning alert-dismissible fade show" role="alert">
                        <p><strong>Tenes que iniciar sesion</strong>  para cargar productos al carrito. </p>
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </asp:Panel>

                    <div class="card login-card mx-auto">
                        <div class="card-body">
                            <div class="text-center mb-4">
                                <h1 class="h3 brand-title mb-0">Bienvenido</h1>
                            </div>

                            <div class="mb-3">
                                <asp:Label ID="lblEmail" runat="server" AssociatedControlID="txtEmail" CssClass="form-label mb-1">Correo Electrónico</asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" ForeColor="Red" CssClass="form-control" placeholder="tu.correo@ejemplo.com" TextMode="SingleLine"></asp:TextBox>
                            </div>

                            <div class="mb-3">
                                <asp:Label ID="lblPassword" runat="server" AssociatedControlID="txtPassword" CssClass="form-label mb-1">Contraseña</asp:Label>
                                <div class="input-group">
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Introduce tu contraseña"></asp:TextBox>
                                </div>
                            </div>

                            <div class="d-grid gap-2 mb-2">
                                <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-primary-custom btn-lg" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
                            </div>

                            <div class="text-center mb-3">
                                <asp:HyperLink ID="hlForgot" NavigateUrl="#" runat="server" CssClass="text-muted-accent small muted-accent">Olvidé mi contraseña</asp:HyperLink>
                            </div>

                            <div class="d-flex align-items-center mb-3">
                                <div class="flex-grow-1 divider"></div>
                                <div class="px-3 small muted-accent">o</div>
                                <div class="flex-grow-1 divider"></div>
                            </div>

                            <div class="text-center">
                                <p class="mb-0 small" style="color: rgba(0,0,0,0.8);">¿Aún no tienes una cuenta?</p>
                                <p class="mb-0 small">
                                    <asp:HyperLink ID="hlRegister" NavigateUrl="~/PaginasPublic/Registro.aspx" runat="server" CssClass="fw-medium text-decoration-underline muted-accent">Crear una</asp:HyperLink>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



