<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministradorUsuarios.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasAdministrador.AdministradorUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Lista Usuarios</h2>

    <div>
        <asp:GridView runat="server" ID="dgvUsuarios" AutoGenerateColumns="false" CssClass="table table-dark">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" />
                <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre de Usuario" />
                <asp:BoundField DataField="Rol" HeaderText="Tipo Usuario" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Email" HeaderText="Correo Electrónico" />
                <asp:BoundField DataField="Telefono" HeaderText="Telefono" />
                <%--<asp:HyperLinkField Text="Editar" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="AdministradorProducto.aspx?Id={0}" />--%>
            </Columns>
        </asp:GridView>
    </div>

    <%--<div>
        <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="false" CssClass="table table-dark">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                <asp:BoundField DataField="Talle" HeaderText="Talle" SortExpression="Talle" />
                <asp:BoundField DataField="PrecioBase" HeaderText="PrecioBase" SortExpression="PrecioBase" DataFormatString="{0:C}" />
                <asp:BoundField DataField="StockActual" HeaderText="Stock" SortExpression="StockActual" />
                <asp:BoundField DataField="StockMinimo" HeaderText="Stock" SortExpression="StockMinimo" />

                <asp:TemplateField HeaderText="Categoria">
                    <ItemTemplate>
                        <%#Eval("Categoria.Nombre") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:HyperLinkField Text="Editar" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="AdministradorProducto.aspx?Id={0}" />
            </Columns>
        </asp:GridView>
    </div>--%>
</asp:Content>
