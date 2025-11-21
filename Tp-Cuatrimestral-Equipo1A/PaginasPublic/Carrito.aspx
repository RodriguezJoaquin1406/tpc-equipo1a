<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.Carrito" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container py-5"> 
    
    <div class="row g-4">

        <div class="col-md-8">
            
            <div class="card shadow-sm border-0"> 
                <div class="card-header bg-white border-bottom py-3">
                    <h4 class="mb-0">Productos</h4>
                </div>
                <div class="card-body">
                    
                    <div class="d-flex align-items-center mb-3">
                        <img src="https://via.placeholder.com/80" class="img-fluid rounded me-3" alt="Producto">
                        <div>
                            <h5 class="mb-1">titulo producto</h5>
                            <p class="text-muted mb-0 small">Talle</p>
                            <a href="#" class="btn btn-danger" style="--bs-btn-padding-y: .20rem; --bs-btn-padding-x: .5rem; --bs-btn-font-size: .70rem;">Eliminar</a>
                        </div>
                        <div class="ms-auto fw-bold">
                            $$precio
                        </div>
                    </div>
                    
                    <hr class="text-muted" />

                    

                </div>
            </div>
        </div>

        <div class="col-md-4">
            
            <div class="card shadow-sm border-0">
                <div class="card-body">
                    <h5 class="card-title mb-4">Resumen de compra</h5>
                    
                    <div class="d-flex justify-content-between mb-2">
                        <span class="text-muted">Productos (2)</span>
                        <span>$60.000</span>
                    </div>
                    <div class="d-flex justify-content-between mb-4">
                        <span class="text-muted">Envío</span>
                        <span class="text-success">$$$$</span>
                    </div>

                    <hr />

                    <div class="d-flex justify-content-between mb-4">
                        <span class="h5">Total</span>
                        <span class="h5">$60.000</span>
                    </div>

                    <div class="d-grid gap-2">
                        <asp:Button ID="btnComprar" runat="server" Text="Comprar" CssClass="btn btn-warning btn-lg" />
                    </div>
                </div>
            </div>
            

        </div>

    </div>
</div>

</asp:Content>
