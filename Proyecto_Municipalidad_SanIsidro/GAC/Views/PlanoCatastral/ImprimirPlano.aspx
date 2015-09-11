<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>
    Impresion de Plano
    </title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <link href="<%: Url.Content("~/favicon.ico") %>" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width" />
    <!-- Font Awesome Icons -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Ionicons -->
    <link href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" rel="stylesheet" type="text/css" />

      <style type="text/css">
            html, body, #map-canvas { height: 600px; margin: 0; padding: 0;}
        </style>

    <%: Styles.Render("~/Content/bootstrap.min.css") %>
    <%: Styles.Render("~/Content/css/AdminLTE.min.css") %>
    <%: Styles.Render("~/Content/css/skins/_all-skins.min.css") %>
</head>
<body>
    <div>
            <input type="hidden" id="int_IdSolicitud"  value='<%: ViewBag.int_IdSolicitud %>'/>
        <input type="hidden" id="opt"  value='<%: ViewBag.opt %>'/>
        <h1>Impresion de <label id="lbltipoImpresion"><%: ViewBag.titulo %></label></h1>
        <label>
        Zona: </label><%: ViewBag.nombre %>
        <br />
            <div id="map-canvas"></div>

    </div>
    
        <%: Scripts.Render("~/bundles/jquery") %>
    <%: Scripts.Render("~/Scripts/bootstrap.min.js") %>

    <%: Scripts.Render("~/Scripts/jstemplate/slimScroll/jquery.slimscroll.min.js") %>
    <%: Scripts.Render("~/Scripts/jstemplate/fastclick/fastclick.min.js") %>
    <%: Scripts.Render("~/Scripts/jstemplate/app.min.js") %>
    <%: Scripts.Render("~/Scripts/jstemplate/demo.js") %>
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=true"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            ImpriPlanoZona();
        });
        function ImpriPlanoZona() {

            $.ajax({
                type: "post",
                url: "../../Geolocalizacion/GetGeolocalizacion",
                error: function (jqXHR, textStatus, errorThrown) {
                    return console.log(jqXHR, errorThrown);
                },
                data: { int_IdSolicitud: $('#int_IdSolicitud').val(), opt: $('#opt').val() },       //Parametro para el action method,
                dataType: "JSON"
            }).success(function (jsonResult) {
                console.log(jsonResult);
                google.maps.event.addDomListener(window, 'load', initialize(jsonResult));
            });



        }

        function initialize(jsonResult) {

            var mapOptions = {
                zoom: 16,
                center: new google.maps.LatLng(jsonResult[0].flt_Latitud, jsonResult[0].flt_Longitud)
            };

            var map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);

            var flightPlanCoordinates = [];
            $.each(jsonResult, function () {
                flightPlanCoordinates.push(new google.maps.LatLng(this.flt_Latitud, this.flt_Longitud));
            });
            flightPlanCoordinates.push(new google.maps.LatLng(jsonResult[0].flt_Latitud, jsonResult[0].flt_Longitud));

            var flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                geodesic: true,
                strokeColor: '#FF0000',
                strokeOpacity: 1.0,
                strokeWeight: 2
            });

            flightPath.setMap(map);
        }

        google.maps.event.addDomListener(window, 'load', initialize);
    </script>


</body>
</html>
