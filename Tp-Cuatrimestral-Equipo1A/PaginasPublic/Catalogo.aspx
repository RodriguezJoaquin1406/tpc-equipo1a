<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        a {
            color: #000000f2;
            text-decoration: none;
        }

        .btn-cream {
            --cream-bg: #e6a137; /* fondo */
            --cream-border: #C6AC8F; /* borde / acento */
            --cream-foreground: #3C3633; /* texto */
            background-color: var(--cream-bg);
            color: var(--cream-foreground);
            border: 1px solid var(--cream-border);
            font-weight: 600;
            box-shadow: none;
            margin: 15px;
        }

            .btn-cream:hover,
            .btn-cream:focus {
                background-color: #E0D5BF;
                border-color: #BFA782;
                color: var(--cream-foreground);
            }

            .btn-cream:active {
                background-color: #D6C9A9;
                border-color: #A88E66;
            }

            .btn-cream:disabled,
            .btn-cream.disabled {
                background-color: #F3F1EE;
                border-color: #E0D8C9;
                color: #A8A09A;
                opacity: 0.8;
            }

        .btn-cream-outline {
            background-color: transparent;
            color: var(--cream-border);
            border: 1px solid var(--cream-border);
        }

            .btn-cream-outline:hover {
                background-color: var(--cream-bg);
                color: var(--cream-foreground);
            }

        .product-image-container {
            height: 400px;
            overflow: hidden;
            position: relative;
        }

            .product-image-container .carousel-item img {
                width: 100%;
                height: 100%;
                object-fit: cover; /* Crop to fill container while maintaining aspect ratio */
                object-position: center; /* Center the image */
            }

        .product-image-container {
            border-bottom: 1px solid #e9ecef;
        }
    </style>

    <section class="text-center macrame-border">
        <h2 class="display-3 font-display mb-4">Nuestra Colección</h2>
        <p class="lead text-muted mx-auto" style="max-width: 40rem;">
            Prendas unicas de excelente calidad.
        </p>
    </section>

    <% if (Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).Rol == "Administrador")
        { %>
    <button type="button" class="btn btn-cream">
        <a href="../PaginasAdministrador/AdministradorProducto.aspx">Nuevo Producto</a>
    </button>

    <button type="button" class="btn btn-cream">
        <a href="../PaginasAdministrador/AdministradorCategorias.aspx">Categorías</a>
    </button>

    <button type="button" class="btn btn-cream">
        <a href="../PaginasAdministrador/CatalogoAdministrador.aspx">Tabla de productos</a>
    </button>
    <% } %>

    <section>
        <div class="d-flex flex-column flex-md-row justify-content-between align-items-center mb-4 gap-3">
            <div class="d-flex flex-wrap gap-3">
                <div>
                    <asp:Label For="category" CssClass="form-label small fw-medium" Text="Categoría" runat="server" />
                    <asp:DropDownList ID="category" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                        <asp:ListItem>Todos</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Label For="size" CssClass="form-label small fw-medium" Text="Talle" runat="server" />
                    <asp:DropDownList ID="size" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlTalle_SelectedIndexChanged">
                        <asp:ListItem>Todos</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Label For="txtBusqueda" CssClass="form-label small fw-medium" Text="Búsqueda" runat="server" />
                    <asp:TextBox runat="server" ID="txtBusqueda" CssClass="form-control" OnTextChanged="txtBusqueda_TextChanged" AutoPostBack="true"/>
                </div>
            </div>

            <%--<div class="text-muted small">
                Mostrando 6 de 24 productos
            </div>--%>
        </div>

        <div class="row row-cols-1 row-cols-sm-2 row-cols-lg-3 g-4">
            <asp:Repeater ID="RepeaterProductos" runat="server">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100">
                            <div class="product-image-container">
                                <div id="carousel<%# Container.ItemIndex %>" class="carousel slide h-100" data-bs-ride="carousel" data-bs-interval="5000" data-bs-pause="false">
                                    <div class="carousel-inner h-100">
                                        <asp:Repeater ID="RepeaterImagenes" runat="server" DataSource='<%# Eval("Imagenes") %>'>
                                            <ItemTemplate>
                                                <div class="carousel-item h-100 <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                                    <img src='<%# Container.DataItem %>' class="d-block w-100 h-100" alt="Imagen producto" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <button class="carousel-control-prev" type="button" data-bs-target="#carousel<%# Container.ItemIndex %>" data-bs-slide="prev">
                                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                    </button>
                                    <button class="carousel-control-next" type="button" data-bs-target="#carousel<%# Container.ItemIndex %>" data-bs-slide="next">
                                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                    </button>
                                </div>
                            </div>

                            <div class="card-body">
                                <h3 class="h4 font-display card-title">
                                    <a href="DetalleProducto.aspx?id=<%#Eval("Id")%>">
                                        <%# Eval("Nombre") %>
                                    </a>
                                </h3>

                                <p class="card-text text-muted">
                                    <a href="DetalleProducto.aspx?id=<%#Eval("Id")%>">
                                        <%# Eval("Descripcion") %>
                                    </a>
                                </p>

                                <p class="card-text text-muted">Precio: $<%# Eval("PrecioBase") %></p>
                            </div>
                            <%if (Session["usuario"] != null && ((Dominio.Usuario)Session["usuario"]).Rol == "Administrador")
                                { %>
                            <a href="../PaginasAdministrador/AdministradorProducto.aspx?id=<%#Eval("Id")%>">
                                <button type="button" class="btn btn-cream">
                                    <p>Modificar</p>
                                </button>
                            </a>
                            <%} %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</asp:Content>
