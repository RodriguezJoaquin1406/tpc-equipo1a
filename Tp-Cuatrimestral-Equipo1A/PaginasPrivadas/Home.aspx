<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPrivadas.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Mi Perfil - Boutique Mi Sueño</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <h2 class="mb-4">Bienvenido, <asp:Label ID="lblNombreUsuario" runat="server" /></h2>

        <div class="card p-4">
            <h4 class="mb-3">Datos del perfil</h4>
            <p><strong>Nombre completo:</strong> <asp:Label ID="lblNombre" runat="server" /></p>
            <p><strong>Email:</strong> <asp:Label ID="lblEmail" runat="server" /></p>
            <p><strong>Teléfono:</strong> <asp:Label ID="lblTelefono" runat="server" /></p>
            <p><strong>Rol:</strong> <asp:Label ID="lblRol" runat="server" /></p>
            <asp:Label ID="lblBienvenida" runat="server" CssClass="alert alert-success" />

            <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar sesión" CssClass="btn btn-outline-danger mt-3" OnClick="btnCerrarSesion_Click" />
        </div>
    </div>
</asp:Content>