<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.Catalogo" %>

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
            <asp:Repeater runat="server" ID="RepeaterProductos">
                <ItemTemplate>
                    <div class="col">
                        <div class="card h-100">
                            <div class="overflow-hidden">
                                <asp:Image 
                                    ImageUrl='<%# string.IsNullOrEmpty(Eval("UrlImagen") as string) ? ResolveUrl("~/Content/placeholder.png") : (Eval("UrlImagen") as string) %>'
                                    AlternateText='<%# Eval("Nombre") %>'
                                    CssClass="card-img-top" 
                                    runat="server" />
                            </div>
                            <div class="card-body">
                                <h3 class="h4 font-display card-title"><%# Eval("Nombre") %></h3>
                                <p class="card-text text-muted"><%# Eval("Descripcion") %></p>
                                <p class="card-text text-muted"><%# Eval("PrecioBase") %></p>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</asp:Content>