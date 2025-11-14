<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministradorProducto.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasAdministrador.AdministradorProducto" %>

<%@ Import Namespace="Negocio" %>
<%@ Import Namespace="Dominio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Administrar Producto</title>
    <style>
        .Error {
            color: red;
        }

        .Success {
            color: green;
        }

        .img-input {
            margin-bottom: 8px;
        }

        .hidden {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <h2 class="mb-4">
            <% if (Request.QueryString["Id"] != null)
                { %>
            Modificar Producto
            <% }
                else
                { %>
            Agregar Nuevo Producto
            <% } %>
        </h2>

        <div class="card">
            <div class="card-body">
                <% if (Request.QueryString["Id"] != null)
                    { %>
                <div class="form-group mb-3">
                    <label class="form-label">ID:</label>
                    <asp:TextBox ID="txtId" runat="server" CssClass="form-control" ReadOnly="true" Text=''></asp:TextBox>
                </div>
                <% } %>

                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre del Producto:</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Text='' placeholder="Ingrese el nombre del producto"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label for="txtDesc" class="form-label">Descripción:</label>
                    <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Text='' placeholder="Ingrese la descripción del producto"></asp:TextBox>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <label for="txtPrecio" class="form-label">Precio Base:</label>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" Text='' placeholder="0.00" TextMode="Number" step="0.01" min="0"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="ddlCategoria" class="form-label">Categoría:</label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
                            <asp:ListItem Text="Seleccione una categoría" Value="" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row mb-3">
                    <div class="col-md-6">
                        <label for="txtStock" class="form-label">Stock Actual:</label>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" Text='' placeholder="0" TextMode="Number" min="0"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label for="txtStockMinimo" class="form-label">Stock Mínimo:</label>
                        <asp:TextBox ID="txtStockMinimo" runat="server" CssClass="form-control" Text='' placeholder="0" TextMode="Number" min="0"></asp:TextBox>
                        <div class="form-text">Cantidad mínima de stock antes de alertar por reposición</div>
                    </div>
                </div>

                <div class="mb-3">
                    <label for="ddlTalle" class="form-label">Talle:</label>
                    <asp:DropDownList ID="ddlTalle" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Seleccione un talle" Value="" />
                        <asp:ListItem Text="S" Value="S" />
                        <asp:ListItem Text="M" Value="M" />
                        <asp:ListItem Text="L" Value="L" />
                        <asp:ListItem Text="XL" Value="XL" />
                    </asp:DropDownList>
                </div>

                <div class="mb-3">
                    <label class="form-label">Imágenes del Producto (una URL por campo):</label>
                    <asp:TextBox ID="txtImagenes" runat="server" CssClass="form-control hidden" Text=''></asp:TextBox>

                    <asp:PlaceHolder ID="phImagenes" runat="server" />

                    <asp:Button ID="btnAgregarImagen" runat="server" Text="Agregar imagen" OnClick="btnAgregarImagen_Click" CssClass="btn btn-secondary mt-2" />
                </div>

                <!-- Mensajes de error y éxito -->
                <% if (prodError == true)
                    { %>
                <div class="alert alert-danger" role="alert">
                    <asp:Label ID="lblError" Text="Error" runat="server" />
                </div>
                <% } %>

                <% if (prodSuccess == true)
                    { %>
                <div class="alert alert-success" role="alert">
                    <asp:Label ID="lblSuccess" Text="Success" runat="server" />
                </div>
                <% } %>

                <!-- Botones de acción -->
                <div class="d-flex gap-2 mt-4">
                    <% if (Request.QueryString["Id"] != null)
                        { %>
                    <asp:Button ID="btnAccion" Text="Modificar Producto" runat="server" OnClick="btnAceptar_Click"
                        CssClass="btn btn-primary"
                        OnClientClick="return syncImagenes();" />
                    <asp:Button ID="btnEliminar" Text="Eliminar Producto" runat="server" CssClass="btn btn-danger"
                        OnClick="btnEliminar_Click"
                        OnClientClick="return confirm('¿Está seguro que desea eliminar este producto?');" />
                    <% }
                        else
                        { %>
                    <asp:Button ID="btnAceptar" Text="Guardar Producto" runat="server" OnClick="btnAceptar_Click"
                        CssClass="btn btn-primary"
                        OnClientClick="return syncImagenes();" />
                    <% } %>

                    <asp:Button ID="btnCancelar" Text="Cancelar" runat="server" CssClass="btn btn-secondary"
                        OnClick="btnCancelar_Click" />
                </div>
            </div>
        </div>
    </div>

    <script runat="server">
        protected int ImagenCount
        {
            get { return (int)(ViewState["ImagenCount"] ?? 0); }
            set { ViewState["ImagenCount"] = value; }
        }

        
    </script>

    <script>
        function syncImagenes() {
            var inputs = document.querySelectorAll('.img-url');
            var vals = [];
            for (var i = 0; i < inputs.length; i++) {
                var v = inputs[i].value ? inputs[i].value.trim() : '';
                if (v) vals.push(v);
            }
            document.getElementById('<%= txtImagenes.ClientID %>').value = vals.join('\r\n');
            return true;
        }
    </script>

</asp:Content>