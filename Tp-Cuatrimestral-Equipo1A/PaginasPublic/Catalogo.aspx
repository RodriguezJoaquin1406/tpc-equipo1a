﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="text-center macrame-border">
        <h2 class="display-3 font-display mb-4">Nuestra Colección</h2>
        <p class="lead text-muted mx-auto" style="max-width: 40rem;">
            Prendas unicas de excelente calidad.
        </p>
    </section>

    <section>
        <div class="d-flex flex-column flex-md-row justify-content-between align-items-center mb-4 gap-3">
            <div class="d-flex flex-wrap gap-3">
                <div>
                    <asp:Label For="category" CssClass="form-label small fw-medium" Text="Categoría" runat="server" />
                    <asp:DropDownList ID="category" CssClass="form-select" runat="server">
                        <asp:ListItem>Todos</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Label For="size" CssClass="form-label small fw-medium" Text="Talle" runat="server" />
                    <asp:DropDownList ID="size" CssClass="form-select" runat="server">
                        <asp:ListItem>Todos</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="text-muted small">
                Mostrando 6 de 24 productos
            </div>
        </div>

        <div class="row row-cols-1 row-cols-sm-2 row-cols-lg-3 g-4">
            <asp:Repeater ID="RepeaterProductos" runat="server">
    <ItemTemplate>
        <div class="col">
            <div class="card h-100">
                <div id="carousel<%# Container.ItemIndex %>" class="carousel slide" data-bs-ride="carousel" data-bs-interval="5000" data-bs-pause="false">
                    <div class="carousel-inner">
                        <asp:Repeater ID="RepeaterImagenes" runat="server" DataSource='<%# Eval("Imagenes") %>'>
                            <ItemTemplate>
                                <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                    <img src='<%# Container.DataItem %>' class="d-block w-100" alt="Imagen producto" />
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

                <div class="card-body">
                    <h3 class="h4 font-display card-title"><%# Eval("Nombre") %></h3>
                    <p class="card-text text-muted"><%# Eval("Descripcion") %></p>
                    <p class="card-text text-muted">Precio: $<%# Eval("PrecioBase") %></p>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>


        </div>
    </section>
</asp:Content>