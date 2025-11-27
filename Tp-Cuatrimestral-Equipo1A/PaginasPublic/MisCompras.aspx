<%@ Page Title="Mis Compras" Language="C#" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.MisCompras" MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <h2 class="mb-4">Mis compras</h2>

        <asp:Repeater ID="rptPedidos" runat="server">
            <ItemTemplate>
                <div class="card mb-4">
                    <div class="card-header bg-light">
                        <strong>Pedido #<%# Eval("Id") %></strong> - <%# Eval("Fecha", "{0:dd/MM/yyyy}") %> - Estado: <%# Eval("Estado") %>
                    </div>
                    <div class="card-body">
                        <p><strong>Total:</strong> $ <%# Eval("Total", "{0:N2}") %></p>
                        <ul class="list-group">
                            <asp:Repeater ID="rptDetalles" runat="server" DataSource='<%# Eval("Detalles") %>'>
                                <ItemTemplate>
                                    <li class="list-group-item d-flex justify-content-between align-items-center">
                                        <%# Eval("Producto.Nombre") %> (Talle: <%# Eval("Talle") %>)
                                        <span>Cantidad: <%# Eval("Cantidad") %> - Subtotal: $ <%# Eval("Subtotal", "{0:N2}") %></span>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Label ID="lblSinCompras" runat="server" CssClass="text-danger fw-bold mt-3 d-block" Visible="false" />
    </div>
</asp:Content>