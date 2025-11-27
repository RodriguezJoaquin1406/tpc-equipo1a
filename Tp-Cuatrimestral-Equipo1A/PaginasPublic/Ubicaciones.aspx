
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

            const contenidoHtmlPuerto =
                '<div id="content">' +
                '<a href="https://www.instagram.com/boutique.misueno/" target="_blank" style="color:black; text-decoration:none;">' +
                /*'<img src="https://i.pinimg.com/1200x/58/70/c8/5870c8c46e727d99d4fd828beadfe28f.jpg" alt="icon mi sueno" style="width:30%; height:auto;">' +*/
                '<h4>Local Puerto De Frutos</h4>' +
                    '<p>Sarmiento 15, Galeria Eucalipto</p>' +
                '</a>' +
                '</div>';

            const infowindowPuerto = new google.maps.InfoWindow({
                content: contenidoHtmlPuerto,
            });

            markerPuerto.addListener("click", () => {
                infowindowPuerto.open(map, markerPuerto);
            });


            const contenidoHtmlPacheco =
                '<div id="content">' +
                '<a href="https://www.instagram.com/boutique.mi.rincon/" target="_blank" style="color:black; text-decoration:none;">' +
                /*'<img src="https://i.pinimg.com/1200x/58/70/c8/5870c8c46e727d99d4fd828beadfe28f.jpg" alt="icon mi sueno" style="width:30%; height:auto;">' +*/
                '<h4>Local General Pacheco</h4>' +
                '<p>Neuquen 267, General Pacheco</p>' +
                '</a>' +
                '</div>';

            const infowindowPacheco = new google.maps.InfoWindow({
                content: contenidoHtmlPacheco,
            });

            markerPacheco.addListener("click", () => {
                infowindowPacheco.open(map, markerPacheco);
            });
        }
    </script>

    
    <script async defer
        src="https://maps.googleapis.com/maps/api/js?key=<%= System.Configuration.ConfigurationManager.AppSettings["Maps Platform API Key"] %>&callback=initMap">
    </script>
</asp:Content>