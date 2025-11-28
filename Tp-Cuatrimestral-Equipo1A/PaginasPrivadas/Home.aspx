<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPrivadas.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Mi Perfil - Boutique Mi Sueño</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <h2 class="mb-4">Bienvenido, <asp:Label ID="lblNombreUsuario" runat="server" /></h2>

        <div class="card p-4">
            <h4 class="mb-3">Datos del perfil</h4>

            <p><strong>Nombre completo:</strong>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </p>
            <p><strong>Email:</strong>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
            </p>
            <p><strong>Teléfono:</strong>
                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
            </p>
            <p><strong>Rol:</strong>
                <asp:Label ID="lblRol" runat="server" />
            </p>

            <h4 class="mt-4 mb-3">Direcciones registradas</h4>

            <asp:DropDownList ID="ddlDirecciones" runat="server" CssClass="form-select"
                AutoPostBack="true" OnSelectedIndexChanged="ddlDirecciones_SelectedIndexChanged">
            </asp:DropDownList>

            <p><strong>Calle:</strong>
    <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" />
</p>
<p><strong>Número:</strong>
    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" />
</p>
<p><strong>Ciudad:</strong>
    <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" />
</p>
<p><strong>Código Postal:</strong>
    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" />
</p>
<p><strong>Provincia:</strong>
    <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" />
</p>

            <asp:Label ID="lblBienvenida" runat="server" CssClass="alert alert-success" />
            <asp:Label ID="lblResultado" runat="server" CssClass="text-success mb-2 d-block" />

            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar datos" CssClass="btn btn-primary me-2" OnClick="btnActualizar_Click" />
            <asp:Button ID="btnEliminarCuenta" runat="server" Text="Eliminar mi cuenta" CssClass="btn btn-danger me-2" OnClick="btnEliminarCuenta_Click" />
            <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar sesión" CssClass="btn btn-outline-secondary mt-3" OnClick="btnCerrarSesion_Click" />
        </div>
    </div>
</asp:Content>