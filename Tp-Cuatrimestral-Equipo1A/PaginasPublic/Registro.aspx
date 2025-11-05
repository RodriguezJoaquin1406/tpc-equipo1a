<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="text-center my-4">Crear cuenta</h2>
<div class="container" style="max-width: 500px;">
    <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />
    
    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control mb-3" placeholder="Nombre de usuario" />
    <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control mb-3" TextMode="Password" placeholder="Contraseña" />
    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control mb-3" placeholder="Nombre completo" />
    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-3" placeholder="Email" />
    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control mb-3" placeholder="Teléfono" />

    <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" CssClass="btn btn-dark w-100" OnClick="btnRegistrar_Click" />
</div>

</asp:Content>
