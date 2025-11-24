<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ModificarUsuario.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasAdministrador.ModificarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Modificar Usuario</h2>
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
    <asp:Panel ID="pnlFormulario" runat="server">
        <div class="mb-3">
            <label class="form-label">Nombre de Usuario</label>
            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label class="form-label">Nombre</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label class="form-label">Correo Electrónico</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label class="form-label">Teléfono</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <label class="form-label">Tipo de Usuario</label>
            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select">
                
            </asp:DropDownList>
        </div>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
    </asp:Panel>
</asp:Content>
