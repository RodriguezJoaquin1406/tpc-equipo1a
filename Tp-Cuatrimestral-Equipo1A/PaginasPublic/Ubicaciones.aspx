<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Ubicaciones.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.Ubicaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #mapa-personalizado {
            height: 450px;
            width: 100%;
            border-radius: 12px;
            background-color: #f0f0f0; 
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center">
        <h1 class="display-4 fw-bold mb-3">Donde podés encontrarnos</h1>
        <p class="lead text-muted mb-5">¡Te esperamos!</p>
    </div>
    
    <div id="mapa-personalizado"></div>

    <script>

        function initMap() {
            // Punto medio entre las dos ubicaciones para centrar el mapa
            const puntoMedio = { lat: -34.439254, lng: -58.612688 };

            // Ubicacion del local de Puerto de Frutos
            const ubiLocalPuerto = { lat: -34.418833, lng: -58.572472 };

            // Ubicacion del local de Pacheco
            const ubiLocalPacheco = { lat: -34.456768, lng: -58.641937 };

            const map = new google.maps.Map(document.getElementById("mapa-personalizado"), {
                zoom: 13, 
                center: puntoMedio,
                mapId: "DEMO_MAP_ID"
            });

            // Marcador Puerto 
            const markerPuerto = new google.maps.Marker({
                position: ubiLocalPuerto,
                map: map,
                title: "Boutique Mi Sueño"
            });

            // Marcador Pacheco
            const markerPacheco = new google.maps.Marker({
                position: ubiLocalPacheco,
                map: map,
                title: "Mi Rincón"
            });
        }
    </script>

    
    <script async defer
        src="https://maps.googleapis.com/maps/api/js?key=<%= System.Configuration.ConfigurationManager.AppSettings["Maps Platform API Key"] %>&callback=initMap">
    </script>
</asp:Content>