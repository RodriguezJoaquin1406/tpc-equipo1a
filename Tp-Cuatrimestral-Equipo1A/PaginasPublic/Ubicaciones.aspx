<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Ubicaciones.aspx.cs" Inherits="Tp_Cuatrimestral_Equipo1A.PaginasPublic.Ubicaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Donde Podes Encontrarnos</h1>
    
    <div id="mapa-personalizado" style="height: 400px; width: 100%;">
        <script src="https://maps.googleapis.com/maps/api/js?key=<%= System.Configuration.ConfigurationManager.AppSettings["GoogleMapsApiKey"] %>"></script>
    </div>

    <script>
        function initMap() {
            
            const miUbicacion = { lat: -34.418833, lng: -58.572472 }; // Coordenadas de ejemplo
            const map = new google.maps.Map(document.getElementById("mapa-personalizado"), {
                zoom: 15,
                center: miUbicacion,
            });
            const marker = new google.maps.Marker({
                position: miUbicacion,
                map: map,
            });
        }
        // Llama a initMap cuando la página cargue (o de la forma que prefieras)
        window.onload = initMap;
    </script>
    <h3> Te Esperamos !</h3>
</asp:Content>
