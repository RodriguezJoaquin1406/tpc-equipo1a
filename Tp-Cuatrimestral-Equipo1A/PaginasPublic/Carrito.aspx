<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.Carrito" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
        <ContentTemplate>



            <div class="container py-5">

                <div class="row g-4">

                    <div class="col-md-8">

                        <div class="card shadow-sm border-0">
                            <div class="card-header bg-white border-bottom py-3">
                                <h4 class="mb-0">Productos</h4>
                            </div>
                            <div class="card-body">

                                <asp:Repeater runat="server" ID="rptCarrito">
                                    <ItemTemplate>
                                        <div class="d-flex align-items-center mb-3">
                                            <img src="<%# Eval("Imagen") %>" class="img-fluid rounded me-3" alt="Producto" style="height: 15rem; width: 12rem;">
                                            <div>
                                                <h5 class="mb-1"><%# Eval("Nombre") %></h5>
                                                <p class="text-muted mb-0 small">Cantidad :  <%# Eval("Cantidad") %></p>
                                                <%--<a href="#" class="btn btn-danger" style="">Eliminar</a>--%>
                                                <asp:Button Text="Eliminar" runat="server" CssClass="btn btn-danger"
                                                    Style="--bs-btn-padding-y: .20rem; --bs-btn-padding-x: .5rem; --bs-btn-font-size: .70rem;"
                                                    OnClick="btn_EliminarClick" CommandArgument='<%# Eval("IdProducto") + ";" + Eval("Cantidad") %>' />
                                            </div>
                                            <div class="ms-auto fw-bold">
                                                <%# Eval("Precio") %>
                                            </div>
                                        </div>

                                        <hr class="text-muted" />

                                    </ItemTemplate>
                                </asp:Repeater>

                            </div>
                        </div>

                    </div>

                    <div class="col-md-4">

                        <div class="card shadow-sm border-0">
                            <div class="card-body">
                                <h5 class="card-title mb-4">Resumen de compra</h5>
                                <asp:Label Text="text" runat="server" ID="lblMensajeCarrito" />

                                <div class="d-flex justify-content-between mb-2">
                                    <span class="text-muted">Cantidad:
                                <asp:Literal ID="litCantidad" Text="" runat="server" />
                                    </span>

                                </div>
                                <div class="d-flex justify-content-between mb-4">
                                    <span class="text-muted">Envío
                                <asp:Literal ID="litEnvio" Text="" runat="server" />
                                    </span>
                                </div>

                                <hr />

                                <div class="d-flex justify-content-between mb-4">
                                    <span class="h5">Total
                                <asp:Literal ID="litTotal" Text="" runat="server" />
                                    </span>

                                </div>

                                <div class="d-grid gap-2">
                                    <asp:Button ID="btnComprar" runat="server" Text="Comprar" CssClass="btn btn-warning btn-lg" />
                                </div>
                            </div>
                        </div>


                    </div>

                </div>
            </div>





            <!-- Modal de Bootstrap -->
            <div class="modal fade" id="modalEliminar" tabindex="-1" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header bg-danger text-white">
                            <h5 class="modal-title">Eliminar Producto</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">

                            <p>¿Cuántas unidades deseas eliminar de este producto?</p>

                            <!-- DropDownList que llenaremos desde C# -->
                            <div class="mb-3">
                                <label class="form-label">Cantidad a eliminar:</label>
                                <asp:DropDownList ID="ddlCantidadEliminar" runat="server" CssClass="form-select">
                                </asp:DropDownList>
                            </div>

                            <!-- Campo oculto para recordar qué ID estamos borrando -->
                            <asp:HiddenField ID="hfIdProductoAEliminar" runat="server" />
                            <asp:HiddenField ID="hfCantidadProducto" runat="server" />

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                            <!-- Botón que confirma la acción -->
                            <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Confirmar Eliminación"
                                CssClass="btn btn-danger" OnClick="btn_ConfirmarEliminarClick" />
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
