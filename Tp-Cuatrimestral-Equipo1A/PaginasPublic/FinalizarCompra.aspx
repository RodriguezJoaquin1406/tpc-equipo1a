<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinalizarCompra.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.FinalizarCompra" MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div class="container my-5">
        <h2 class="mb-4">Finalizar compra</h2>

        <!-- Datos del usuario -->
        <h5>Datos del usuario</h5>
        <ul class="list-group mb-4">
            <li class="list-group-item">Nombre: <asp:Label ID="lblNombre" runat="server" /></li>
            <li class="list-group-item">Usuario: <asp:Label ID="lblNombreUsuario" runat="server" /></li>
            <li class="list-group-item">Email: <asp:Label ID="lblEmail" runat="server" /></li>
            <li class="list-group-item">Teléfono: <asp:Label ID="lblTelefono" runat="server" /></li>
        </ul>

      <!-- Selección de dirección -->
<div class="mb-3">
    <label for="ddlDireccion" class="form-label">Dirección de envío</label>
    <asp:DropDownList ID="ddlDireccion" runat="server" CssClass="form-select" />
    
    <asp:Label ID="lblSinDireccion" runat="server" 
        Text="No tenés direcciones registradas." 
        CssClass="text-danger fw-bold mt-2 d-block" 
        Visible="false" />
    
    <!-- Botón que abre el modal directamente -->
    <asp:Button ID="btnAgregarDireccion" runat="server" 
        Text="Agregar dirección" 
        CssClass="btn btn-outline-primary mt-2" 
        OnClientClick="$('#modalDireccion').modal('show'); return false;" 
        Visible="false" />
</div>


        <!-- Selección de método de pago -->
        <div class="mb-3">
            <label for="ddlMetodoPago" class="form-label">Método de pago</label>
            <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select" />
        </div>

        <!-- Resumen de compra -->
        <div class="mb-3">
            <asp:Repeater ID="rptResumen" runat="server">
                <HeaderTemplate>
                    <h4>Resumen de compra</h4>
                    <ul class="list-group">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="list-group-item d-flex justify-content-between align-items-center">
                        <%# Eval("Nombre") %> (Talle: <%# Eval("Talle") %>)
                        <span>Cantidad: <%# Eval("Cantidad") %> - Subtotal: <%# Eval("Subtotal", "{0:C2}") %></span>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
            <p class="fw-bold mt-3">Total: <asp:Literal ID="litTotal" runat="server" /></p>
        </div>

        <!-- Botón confirmar -->
        <asp:Button ID="btnConfirmarCompra" runat="server" Text="Confirmar compra" CssClass="btn btn-success" OnClick="btnConfirmarCompra_Click" />
    </div>

    <!-- Modal para agregar dirección -->
   <div class="modal fade" id="modalDireccion" tabindex="-1" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header bg-primary text-white">
                <h5 class="modal-title">Agregar dirección</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
            </div>
            <div class="modal-body">
                <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control mb-2" placeholder="Calle" />
                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control mb-2" placeholder="Número" />
                <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control mb-2" placeholder="Ciudad" />
                <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control mb-2" placeholder="Código Postal" />
                <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control mb-2" placeholder="Provincia" />
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnConfirmarDireccion" runat="server" Text="Guardar dirección" CssClass="btn btn-primary" OnClick="btnConfirmarDireccion_Click" />
            </div>
        </div>
    </div>
</div>

</asp:Content>