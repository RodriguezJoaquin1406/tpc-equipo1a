<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Estilos específicos para la página de detalle -->
    <style>
        /* Galería de imágenes */
        .product-main-image {
            width: 100%;
            aspect-ratio: 1;
            background-size: cover;
            background-position: center;
            border-radius: 12px;
            cursor: zoom-in;
            background-color: #f0f0f0;
        }

        .product-thumb {
            width: 100%;
            aspect-ratio: 1;
            background-size: cover;
            background-position: center;
            border-radius: 8px;
            cursor: pointer;
            opacity: 0.7;
            transition: opacity 0.3s, border 0.3s;
            border: 2px solid transparent;
            background-color: #f0f0f0;
        }

            .product-thumb:hover {
                opacity: 1;
            }

            .product-thumb.active {
                opacity: 1;
                border-color: #ee9d2b;
            }

        /* Botón primario personalizado */
        .btn-primary-custom {
            background-color: #ee9d2b;
            border-color: #ee9d2b;
            color: white;
            font-weight: 700;
        }

            .btn-primary-custom:hover {
                background-color: #d88d23;
                border-color: #d88d23;
            }

            .btn-primary-custom:disabled {
                background-color: #ccc;
                border-color: #ccc;
            }

        /* Acordeón/Details */
        .accordion-button:not(.collapsed) {
            background-color: transparent;
            color: inherit;
        }

        .accordion-button:focus {
            box-shadow: none;
        }

        /* Cards de productos relacionados */
        .related-product-card {
            transition: transform 0.3s;
            text-decoration: none;
            color: inherit;
            display: block;
        }

            .related-product-card:hover {
                transform: translateY(-4px);
            }

                .related-product-card:hover .product-title {
                    color: #ee9d2b;
                }

        .related-product-image {
            width: 100%;
            aspect-ratio: 3/4;
            background-size: cover;
            background-position: center;
            border-radius: 12px;
            background-color: #f0f0f0;
        }

        .alert-custom {
            border-radius: 8px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container my-5">
        <!-- Mensaje de error/éxito -->
        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="mb-4">
            <div class="alert alert-custom" role="alert">
                <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlProducto" runat="server">
            <div class="row g-5">

                <div class="col-lg-6">
                    <div id="productCarousel" class="carousel slide" data-bs-ride="carousel">
                        <div class="carousel-inner">
                            <asp:Repeater ID="RepeaterImagenes" runat="server">
                                <ItemTemplate>
                                    <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                        <img src="<%# Container.DataItem %>" class="d-block w-100" alt="Imagen del producto" style="aspect-ratio: 1; object-fit: cover; border-radius: 12px;" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <button class="carousel-control-prev" type="button" data-bs-target="#productCarousel" data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </button>
                        <button class="carousel-control-next" type="button" data-bs-target="#productCarousel" data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </button>
                    </div>
                </div>


                <!-- Columna derecha: Información del producto -->
                <div class="col-lg-6">
                    <h1 class="display-5 fw-bold mb-3">
                        <asp:Label ID="lblNombreProducto" runat="server"></asp:Label>
                    </h1>

                    <p class="text-muted mb-2">
                        <asp:Label ID="lblCategoria" runat="server"></asp:Label>
                    </p>

                    <p class="h2 fw-bold mb-4" style="color: #ee9d2b;">
                        <asp:Label ID="lblPrecio" runat="server"></asp:Label>
                    </p>

                    <!-- Stock disponible -->
                    <p class="mb-3">
                        <asp:Label ID="lblStock" runat="server" CssClass="text-muted"></asp:Label>
                    </p>

                    <!-- Selectores de talle y color -->
                    <div class="row g-3 mb-4">
                        <div class="col-md-6">
                            <asp:Label ID="lblTalleLabel" runat="server" AssociatedControlID="ddlTalle" CssClass="form-label fw-medium">Talla</asp:Label>
                            <asp:DropDownList ID="ddlTalle" runat="server" CssClass="form-select">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="lblColorLabel" runat="server" AssociatedControlID="ddlColor" CssClass="form-label fw-medium">Color</asp:Label>
                            <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-select">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Cantidad -->
                    <div class="mb-4">
                        <asp:Label ID="lblCantidadLabel" runat="server" AssociatedControlID="txtCantidad" CssClass="form-label fw-medium">Cantidad</asp:Label>
                        <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" CssClass="form-control" Text="1" min="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCantidad" runat="server" ControlToValidate="txtCantidad"
                            ErrorMessage="La cantidad es requerida" CssClass="text-danger small" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="rvCantidad" runat="server" ControlToValidate="txtCantidad"
                            MinimumValue="1" MaximumValue="999" Type="Integer"
                            ErrorMessage="Ingrese una cantidad válida" CssClass="text-danger small" Display="Dynamic">
                        </asp:RangeValidator>
                    </div>

                    <!-- Botón añadir al carrito -->
                    <div class="d-grid mb-4">
                        <asp:Button ID="btnAgregarCarrito" runat="server" Text="Añadir al carrito"
                            CssClass="btn btn-primary-custom btn-lg" OnClick="btnAgregarCarrito_Click" />
                    </div>

                    <!-- Descripción expandible -->
                    <div class="border-top pt-4">
                        <div class="accordion accordion-flush" id="accordionProductDetails">
                            <div class="accordion-item bg-transparent border-0">
                                <h2 class="accordion-header" id="headingDescription">
                                    <button class="accordion-button bg-transparent fw-bold ps-0" type="button"
                                        data-bs-toggle="collapse" data-bs-target="#collapseDescription"
                                        aria-expanded="true" aria-controls="collapseDescription">
                                        Descripción
                                    </button>
                                </h2>
                                <div id="collapseDescription" class="accordion-collapse collapse show"
                                    aria-labelledby="headingDescription" data-bs-parent="#accordionProductDetails">
                                    <div class="accordion-body ps-0 text-muted">
                                        <asp:Label ID="lblDescripcion" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="accordion-item bg-transparent border-0 border-top mt-3">
                                <h2 class="accordion-header" id="headingDetails">
                                    <button class="accordion-button collapsed bg-transparent fw-bold ps-0" type="button"
                                        data-bs-toggle="collapse" data-bs-target="#collapseDetails"
                                        aria-expanded="false" aria-controls="collapseDetails">
                                        Detalles del Producto
                                    </button>
                                </h2>
                                <div id="collapseDetails" class="accordion-collapse collapse"
                                    aria-labelledby="headingDetails" data-bs-parent="#accordionProductDetails">
                                    <div class="accordion-body ps-0">
                                        <ul class="list-unstyled text-muted">
                                            <li><strong>Material:</strong>
                                                <asp:Label ID="lblMaterial" runat="server"></asp:Label></li>
                                            <li><strong>Color:</strong>
                                                <asp:Label ID="lblColorDisponible" runat="server"></asp:Label></li>
                                            <li><strong>Talla:</strong>
                                                <asp:Label ID="lblTalleDisponible" runat="server"></asp:Label></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </asp:Panel>
    </div>
</asp:Content>