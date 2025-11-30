<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CatalogoAdministrador.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasAdministrador.CatalogoAdministrador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Productos Administrador</h2>
    <asp:LinkButton Text="Añadir Nuevo" runat="server" href="AdministradorProducto.aspx" CssClass="btn btn-secondary" Style="margin: 20px;" />

    <!-- Filtros -->
    <section>
        <div class="d-flex flex-column flex-md-row justify-content-start align-items-center mb-4 gap-3">
            <div class="d-flex flex-wrap gap-3">
                <div>
                    <asp:Label For="ddlCategoria" CssClass="form-label small fw-medium" Text="Categoría" runat="server" />
                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                        <asp:ListItem>Todos</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Label For="ddlTalle" CssClass="form-label small fw-medium" Text="Talle" runat="server" />
                    <asp:DropDownList ID="ddlTalle" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlTalle_SelectedIndexChanged">
                        <asp:ListItem>Todos</asp:ListItem>
                    </asp:DropDownList>
                </div>


                <div>
                    <asp:Label For="txtBusqueda" CssClass="form-label small fw-medium" Text="Búsqueda" runat="server" />
                    <asp:TextBox runat="server" ID="txtBusqueda" CssClass="form-control" OnTextChanged="txtBusqueda_TextChanged" AutoPostBack="true" />
                </div>

                <div class="align-self-end">
                    <asp:Button ID="btnLimpiarFiltros" runat="server" Text="Limpiar Filtros"
                        CssClass="btn btn-outline-secondary btn-sm" OnClick="btnLimpiarFiltros_Click" />
                </div>

            </div>
        </div>
    </section>

    <div>
        <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="false" CssClass="table table-dark">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" SortExpression="Descripcion" />
                <asp:BoundField DataField="Talle" HeaderText="Talle" SortExpression="Talle" />
                <asp:BoundField DataField="PrecioBase" HeaderText="PrecioBase" SortExpression="PrecioBase" DataFormatString="{0:C}" />
                <asp:BoundField DataField="StockActual" HeaderText="Stock Actual" SortExpression="StockActual" />
                <asp:BoundField DataField="StockMinimo" HeaderText="Stock Mínimo" SortExpression="StockMinimo" />

                <asp:TemplateField HeaderText="Categoria">
                    <ItemTemplate>
                        <%#Eval("Categoria.Nombre") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:HyperLinkField Text="Editar" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="AdministradorProducto.aspx?Id={0}" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
