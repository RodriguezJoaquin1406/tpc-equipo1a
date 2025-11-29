<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministradorPedidos.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasAdministrador.AdministradorPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server" />

    <h2>Administrador de Pedidos</h2>

    <asp:UpdatePanel ID="upPedidos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="mb-3 d-flex justify-content-between align-items-center">
                <div>
                    <asp:DropDownList ID="ddlFiltroEstado" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged">
                        <asp:ListItem Value="">Todos los estados</asp:ListItem>
                        <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                        <asp:ListItem Value="Pagado">Pagado</asp:ListItem>
                        <asp:ListItem Value="Enviado">Enviado</asp:ListItem>
                        <asp:ListItem Value="Cancelado">Cancelado</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div>
                <asp:GridView runat="server" ID="dgvPedidos" AutoGenerateColumns="false" CssClass="table table-dark">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="Usuario.NombreUsuario" HeaderText="Cliente" />
                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class="badge <%# GetEstadoBadgeClass(Eval("Estado").ToString()) %>">
                                    <%# Eval("Estado") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />

                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button Text="Ver Detalles" runat="server" CssClass="btn btn-info btn-sm me-2"
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnClick="btnVerDetalles_Click" />

                                <asp:Button Text="Cambiar Estado" runat="server" CssClass="btn btn-warning btn-sm me-2"
                                    CommandArgument='<%# Eval("Id") %>'
                                    Visible='<%# Eval("Estado").ToString().Equals("Pendiente") || Eval("Estado").ToString().Equals("Pagado") %>'
                                    OnClick="btnCambiarEstado_Click" />

                                <asp:Button Text="Cancelar" runat="server" CssClass="btn btn-danger btn-sm"
                                    CommandArgument='<%# Eval("Id") %>'
                                    OnClick="btnCancelar_Click"
                                    Visible='<%# Eval("Estado").ToString().Equals("Pendiente") %>'
                                    CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Modal de Detalles del Pedido -->
            <div class="modal fade" id="modalDetalles" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header bg-info text-white">
                            <h5 class="modal-title">Detalles del Pedido #<asp:Label ID="lblIdPedidoDetalles" runat="server" /></h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <h6>Información del Cliente</h6>
                                    <p>
                                        <strong>Usuario:</strong>
                                        <asp:Label ID="lblUsuario" runat="server" />
                                    </p>
                                    <p>
                                        <strong>Fecha:</strong>
                                        <asp:Label ID="lblFechaPedido" runat="server" />
                                    </p>
                                    <p>
                                        <strong>Estado:</strong>
                                        <asp:Label ID="lblEstadoPedido" runat="server" />
                                    </p>
                                </div>
                                <div class="col-md-6">
                                    <h6>Información de Entrega</h6>
                                    <p>
                                        <strong>Dirección:</strong>
                                        <asp:Label ID="lblDireccion" runat="server" />
                                    </p>
                                    <p>
                                        <strong>Método de Pago:</strong>
                                        <asp:Label ID="lblMetodoPago" runat="server" />
                                    </p>
                                    <p>
                                        <strong>Total:</strong>
                                        <asp:Label ID="lblTotalPedido" runat="server" />
                                    </p>
                                </div>
                            </div>

                            <hr />

                            <h6>Productos del Pedido</h6>
                            <asp:GridView ID="dgvDetallesPedido" runat="server" AutoGenerateColumns="false" CssClass="table table-sm">
                                <Columns>
                                    <asp:BoundField DataField="Producto.Nombre" HeaderText="Producto" />
                                    <asp:BoundField DataField="Talle" HeaderText="Talle" />
                                    <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio Unit." DataFormatString="{0:C}" />
                                    <asp:TemplateField HeaderText="Subtotal">
                                        <ItemTemplate>
                                            <%# ((decimal)Eval("PrecioUnitario") * (int)Eval("Cantidad")).ToString("C") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Modal de Cambio de Estado -->
            <div class="modal fade" id="modalCambiarEstado" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-warning text-white">
                            <h5 class="modal-title">Cambiar Estado del Pedido</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="mb-3">
                                <label class="form-label">Nuevo Estado</label>
                                <asp:DropDownList ID="ddlNuevoEstado" runat="server" CssClass="form-select">
                                    <asp:ListItem Value="Pendiente">Pendiente</asp:ListItem>
                                    <asp:ListItem Value="Pagado">Pagado</asp:ListItem>
                                    <asp:ListItem Value="Enviado">Enviado</asp:ListItem>
                                    <asp:ListItem ID="liCancelado" Value="Cancelado">Cancelado</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblErrorEstado" runat="server" CssClass="text-danger" />
                            </div>
                            <asp:HiddenField ID="hfIdPedidoEstado" runat="server" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <asp:Button ID="btnGuardarEstado" runat="server" Text="Cambiar Estado"
                                CssClass="btn btn-warning" OnClick="btnGuardarEstado_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <!-- Modal de Confirmación de Cancelación -->
            <div class="modal fade" id="modalConfirmaCancelar" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header bg-danger text-white">
                            <h5 class="modal-title">Confirmar Cancelación</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblMensajeCancelar" runat="server" Text="¿Estás seguro de cancelar este pedido?" />
                            <asp:HiddenField ID="hfIdPedidoCancelar" runat="server" />
                            <asp:Label ID="lblErrorCancelar" runat="server" CssClass="text-danger" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">No</button>
                            <asp:Button ID="btnConfirmaCancelar" runat="server" Text="Sí, Cancelar"
                                CssClass="btn btn-danger" OnClick="btnConfirmaCancelar_Click"
                                CausesValidation="false" />
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
