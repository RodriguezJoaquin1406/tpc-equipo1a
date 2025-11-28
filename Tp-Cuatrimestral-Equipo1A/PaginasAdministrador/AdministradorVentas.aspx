<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministradorVentas.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasAdministrador.AdministradorVentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager runat="server" />

    <h2>Administrador de Ventas</h2>

    <asp:UpdatePanel ID="upVentas" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="mb-3 d-flex justify-content-between align-items-center">
                <div>
                    <asp:Label Text="Filtrar por fecha:" runat="server" CssClass="form-label me-2" />
                    <asp:TextBox ID="txtFechaDesde" runat="server" TextMode="Date" CssClass="form-control d-inline-block me-2" Style="width: auto;" />
                    <span class="me-2">hasta</span>
                    <asp:TextBox ID="txtFechaHasta" runat="server" TextMode="Date" CssClass="form-control d-inline-block me-2" Style="width: auto;" />
                    <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary btn-sm" OnClick="btnFiltrar_Click" />
                    <asp:Button ID="btnLimpiarFiltro" runat="server" Text="Limpiar" CssClass="btn btn-outline-secondary btn-sm ms-2" OnClick="btnLimpiarFiltro_Click" />
                </div>
            </div>

            <div class="mb-3">
                <asp:Label ID="lblResumen" runat="server" CssClass="text-muted" />
            </div>

            <div>
                <asp:GridView runat="server" ID="dgvVentas" AutoGenerateColumns="false" CssClass="table table-dark">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="NumeroFactura" HeaderText="Nº Factura" />
                        <asp:BoundField DataField="Cliente.NombreUsuario" HeaderText="Cliente" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <%# GetTotal(Container.DataItem).ToString("C") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button Text="Ver Detalles" runat="server" CssClass="btn btn-info btn-sm me-2"
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnClick="btnVerDetalles_Click" />

                                <asp:Button Text="Eliminar" runat="server" CssClass="btn btn-danger btn-sm"
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnClick="btnEliminar_Click"
                                    OnClientClick="return confirm('¿Está seguro de eliminar esta venta?');"
                                    CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Modal de Detalles de la Venta -->
            <div class="modal fade" id="modalDetalles" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header bg-info text-white">
                            <h5 class="modal-title">Detalles de la Venta #<asp:Label ID="lblIdVentaDetalles" runat="server" /></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <h6>Información de la Venta</h6>
                                    <p><strong>Número Factura:</strong>
                                        <asp:Label ID="lblNumeroFactura" runat="server" /></p>
                                    <p><strong>Fecha:</strong>
                                        <asp:Label ID="lblFechaVenta" runat="server" /></p>
                                </div>
                                <div class="col-md-6">
                                    <h6>Información del Cliente</h6>
                                    <p><strong>Cliente:</strong>
                                        <asp:Label ID="lblNombreCliente" runat="server" /></p>
                                    <p><strong>Usuario:</strong>
                                        <asp:Label ID="lblUsuarioCliente" runat="server" /></p>
                                    <p><strong>Email:</strong>
                                        <asp:Label ID="lblEmailCliente" runat="server" /></p>
                                    <p><strong>Teléfono:</strong>
                                        <asp:Label ID="lblTelefonoCliente" runat="server" /></p>
                                </div>
                            </div>

                            <hr />

                            <h6>Productos de la Venta</h6>
                            <asp:GridView ID="dgvDetallesVenta" runat="server" AutoGenerateColumns="false" CssClass="table table-sm">
                                <Columns>
                                    <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unit." DataFormatString="{0:C}" />
                                    <asp:TemplateField HeaderText="Subtotal">
                                        <ItemTemplate>
                                            <%# ((decimal)Eval("PrecioUnitario") * (int)Eval("Cantidad")).ToString("C") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <div class="text-end mt-3">
                                <h5><strong>Total:
                                    <asp:Label ID="lblTotalVenta" runat="server" /></strong></h5>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
