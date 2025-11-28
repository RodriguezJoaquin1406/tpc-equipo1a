<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RecuperarContrasena.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.RecuperarContrasena" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="text-center my-4">Recuperar Contrasena</h2>
    <h3 class="text-center my-4">Ingrese su mail para que podamos resetear su contrasena</h3>
    <div class="container" style="max-width: 500px;">
        <asp:Label ID="lblError" runat="server" CssClass="text-danger" Visible="false" />
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-succes" Visible="false" />

        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control mb-3" placeholder="Email" />
        <asp:RegularExpressionValidator ID="regexEmail" runat="server"
            ControlToValidate="txtEmail"
            ErrorMessage="Formato de email incorrecto"
            ForeColor="Red"
            ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" />


        <asp:Button ID="btnReiniciar" runat="server" Text="Reiniciar Contraseña" CssClass="btn btn-dark w-100" OnClick="btnReiniciar_Click" />
    </div>
</asp:Content>
