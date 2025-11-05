<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hero {
            background-image: url('https://static.zara.net/photos///2023/V/0/1/p/5032/221/800/2/w/560/5032221800_1_1_1.jpg?ts=1678797999444');
            background-size: cover;
            background-position: center;
            height: 60vh;
            display: flex;
            align-items: center;
            justify-content: center;
            color: white;
            text-shadow: 0 0 10px rgba(0,0,0,0.7);
        }
        .hero h1 {
            font-size: 4rem;
            font-family: "Playfair Display", serif;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <section class="hero">
        <h1>Bienvenidos a Boutique Mi Sueño</h1>
    </section>

    <section class="text-center my-5">
        <h2 class="display-5 font-display mb-4">Descubrí nuestra colección</h2>
        <p class="lead text-muted mx-auto" style="max-width: 40rem;">
            Prendas únicas, elegantes y de excelente calidad. ¡Explorá lo que tenemos para vos!
        </p>

        <div class="d-flex justify-content-center gap-3 mt-4">
            <asp:HyperLink ID="hlCatalogo" NavigateUrl="~/PaginasPublic/Catalogo.aspx" CssClass="btn btn-outline-dark btn-lg" Text="Ver Catálogo" runat="server" />
        </div>
    </section>

    <section class="container my-5">
        <div class="row text-center">
            <div class="col-md-4">
                <span class="material-icons" style="font-size: 48px;">local_mall</span>
                <h4 class="mt-3">Variedad</h4>
                <p>Amplia selección de prendas para todos los estilos.</p>
            </div>
            <div class="col-md-4">
                <span class="material-icons" style="font-size: 48px;">verified</span>
                <h4 class="mt-3">Calidad</h4>
                <p>Materiales premium y confección cuidada.</p>
            </div>
            <div class="col-md-4">
                <span class="material-icons" style="font-size: 48px;">favorite</span>
                <h4 class="mt-3">Estilo</h4>
                <p>Diseños únicos que te hacen destacar.</p>
            </div>
        </div>
    </section>
</asp:Content>