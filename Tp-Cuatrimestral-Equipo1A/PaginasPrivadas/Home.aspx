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

            <div class="d-flex align-items-center justify-content-between mt-4 mb-3">
    <h4 class="mb-0">Direcciones registradas</h4>
    <div>
        <asp:Button ID="btnAgregarDireccion" runat="server" Text="Agregar dirección"
            CssClass="btn btn-success me-2" OnClientClick="$('#modalAgregarDireccion').modal('show'); return false;" />
        <asp:Button ID="btnEliminarDireccion" runat="server" Text="Eliminar dirección"
            CssClass="btn btn-danger" OnClick="btnEliminarDireccion_Click"
            OnClientClick="return confirm('¿Estás seguro de que querés eliminar esta dirección?');" />

    </div>
<!-- Agregar Dirección -->
<div class="modal fade" id="modalAgregarDireccion" tabindex="-1" role="dialog" aria-labelledby="modalAgregarDireccionLabel" aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="modalAgregarDireccionLabel">Agregar nueva dirección</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
      </div>
      <div class="modal-body">
        <asp:TextBox ID="txtNuevaCalle" runat="server" CssClass="form-control mb-2" placeholder="Calle" />
        <asp:TextBox ID="txtNuevoNumero" runat="server" CssClass="form-control mb-2" placeholder="Número" />
        <asp:TextBox ID="txtNuevaCiudad" runat="server" CssClass="form-control mb-2" placeholder="Ciudad" />
        <asp:TextBox ID="txtNuevoCodigoPostal" runat="server" CssClass="form-control mb-2" placeholder="Código Postal" />
        <asp:TextBox ID="txtNuevaProvincia" runat="server" CssClass="form-control mb-2" placeholder="Provincia" />
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnConfirmarAgregarDireccion" runat="server" Text="Guardar dirección"
            CssClass="btn btn-primary" OnClick="btnConfirmarAgregarDireccion_Click" />
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
      </div>
    </div>
  </div>
</div>
</div>

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