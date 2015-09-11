$(document).ready(function () {

  
    
    
    $('#btnModalSoicitud').click(function () {
        ObtenerSolicitud();
        $('#myModal').modal('show');
    });
    $('#btnModalZona').click(function () {
        $('#ModalZona').modal('show');

    });

    $('#btnaddPredio').click(function () {
        ObtenerUnLote();
        ObtenerUnaManzana();
        ObtenerUnaZona();
        $('#MOdalPredio').modal('show');

    });

    $('#btnmanzana1').click(function () {
        $('#modalManzana').modal('show');

    });

    $('#Button1').click(function () {
        $('#modalLote').modal('show');

    });



    $("#btnaceptarSolicitud").click(function () {
        SeleccionarSolicitud();
        ObtenerZona();
    });
    $("#btnAgregarDetalleZona").click(function () {
        AgregarDetalleZona('Zona');
    });
    $("#btnAgregarDetalleManzana").click(function () {
        console.log("agregar detale");
        AgregarDetalleZona('Manzana');
    })
    $("#btnAgregarDetalleLote").click(function () {
        AgregarDetalleZona('Lote');
    })

    $("#btnEliminarZona").click(function () {
        
        $("#TituloModalConfir").html("Eliminar Zona");
        $("#p1ModalConfir").html("Desea Eliminar la Zona?");
        $("#p2ModalConfir").html("Se Eliminara todos los datos de geolocalización");
        $("#btnModalConfir").html("Eliminar Zona");
        
        $('#ModalConfir').modal('show');
            
    });
    $("#Button4").click(function () {

        $("#TituloModalConfir").html("Eliminar Manzana");
        $("#p1ModalConfir").html("Desea Eliminar la Manzana?");
        $("#p2ModalConfir").html("Se Eliminara todos los datos de geolocalización");
        $("#btnModalConfir").html("Eliminar Manzana");

        $('#ModalConfir').modal('show');

    });

    $("#Button9").click(function () {

        $("#TituloModalConfir").html("Eliminar Lote");
        $("#p1ModalConfir").html("Desea Eliminar la Lote?");
        $("#p2ModalConfir").html("Se Eliminara todos los datos de geolocalización");
        $("#btnModalConfir").html("Eliminar Lote");

        $('#ModalConfir').modal('show');

    });
    $("#Button2").click(function () {

        $("#TituloModalConfir").html("Eliminar Predio");
        $("#p1ModalConfir").html("Desea Eliminar la Predio?");
        $("#p2ModalConfir").html("");
        $("#btnModalConfir").html("Eliminar Predio");

        $('#ModalConfir').modal('show');

    });

    $("#btnGrabarPredio").click(function () {
        var messajeError = ""
        if ($("#txtPreCodigoZona").val() == '') {
            messajeError = messajeError + "Debe Ingresar Codigo Zona  \n";
            console.log('ValidaCodigo');
        }
        if ($("#txtPreNombreZona").val() == '') {
            messajeError = messajeError + "Debe Ingresar Nombre Zona  \n";
        }
        if ($("#txtPreNombreMz").val() == '') {
            messajeError = messajeError + "Debe Ingresar Nombre de la Mz  \n";
        }
        if ($("#txtPreCodigoLote").val() == '') {
            messajeError = messajeError + "Debe Ingresar Codigo Lote  \n";
        }

        if ($("#txtPreNombreLote").val() == '') {
            messajeError = messajeError + "Debe Ingresar Nombre de Lote  \n";
        }

        if ($("#txtPreDirección").val() == '') {
            messajeError = messajeError + "Debe Ingresar Direccion del Predio \n";
        }

        if ($("#txtPreCodigo").val() == '') {
            messajeError = messajeError + "Debe Ingresar Codigo de Predio  \n";
        }


        if ($("#txtPreAltura").val() == '') {
            messajeError = messajeError + "Debe Ingresar  Altura de Predio \n";
        }


        if ($("#txtPreAreaConstruida").val() == '') {
            messajeError = messajeError + "Debe Ingresar Area construida \n";
        }

        if ($("#txtPreObservaciones").val() == '') {
            messajeError = messajeError + "Debe Ingresar la Observacion  \n";
        }

        if (messajeError.length > 0) {
            alert(messajeError);
            return;
        }


        var Predio;
        Predio = {
            "chr_CodigoPredio": $("#txtPreCodigo").val(),
            "var_Nombre": $("#txtNombrePredio").val(),
            
            "var_Ubicacion": $("#txtPreDirección").val(),
            "dcm_AreaConstruida": $("#txtPreAreaConstruida").val(),


            "var_Observaciones": $("#txtPreObservaciones").val(),
            "int_Altura": $("#txtPreAltura").val(),
            "int_IdLote": $("#int_IdLote").val()
        };

        $.ajax({
            type: "post",
            url: "/Predio/InsertarPredio",
            error: function (jqXHR, textStatus, errorThrown) {
                return console.log(jqXHR, errorThrown);
            },
            data: Predio,
            dataType: "JSON"
        }).success(function (json) {
            $('#MOdalPredio').modal('hide');
            ObtenerPredios();
            // ObtenerZona();
            //$('#int_IdZona').val(json.int_IdZona);
            //ConfirmarDetalleZona();
        });



    });


    $("#btnAddZonaNew").click(function () {


        var messajeError = ""
        if ($("#txtCodigoZona").val() == '') {
            messajeError = messajeError + "Debe Ingresar el Codigo de Zona  \n";
            console.log('ValidaCodigo');
        }
        if ($("#txtNombreZona").val() == '') {
            messajeError = messajeError + "Debe Ingresar Nombre de la Zona  \n";
        }
        if ($("#txtAreaZona").val() == '') {
            messajeError = messajeError + "Debe Ingresar el area de la Zona  \n";
        }
        if ($('#tbldetalleZona tbody tr').length == 1) {
            messajeError = messajeError + "Debe Ingresar detalle de Zona \n";
        }

        if (messajeError.length > 0) {
            alert(messajeError);
            return;
        }


        var zona;
        zona = {
            "int_IdSolicitud": $("#int_IdSolicitud").val(),
            "var_CodigoZona": $("#txtCodigoZona").val(),
            "var_Nombre": $("#txtNombreZona").val(),
            "dbl_Area": $("#txtAreaZona").val()
        };

        $.ajax({
            type: "post",
            url: "/zona/InsertarZona",
            error: function (jqXHR, textStatus, errorThrown) {
                return console.log(jqXHR, errorThrown);
            },
            data: zona,
            dataType: "JSON"
        }).success(function (json) {

            // ObtenerZona();
            $('#int_IdZona').val(json.int_IdZona);
            ConfirmarDetalleZona("Zona");
        });
        //ConfirmarDetalleZona();
    });


    $("#btnAgregarManzanaNew").click(function () {


        var messajeError = ""
        if ($("#txtxCodigoManzana").val() == '') {
            messajeError = messajeError + "Debe Ingresar el Codigo de Manzana  \n";

        }
        if ($("#txtNombreManzana").val() == '') {
            messajeError = messajeError + "Debe Ingresar Nombre de la Manzana  \n";
        }
        if ($("#txtManzanaArea").val() == '') {
            messajeError = messajeError + "Debe Ingresar el area de la Manzana \n";
        }
        if ($('#tbldetallemanzana tbody tr').length == 1) {
            messajeError = messajeError + "Debe Ingresar detalle de la Manzana \n";
        }

        if (messajeError.length > 0) {
            alert(messajeError);
            return;
        }


        var manzana;
        manzana = {
            "int_IdZona": $("#int_IdZona").val(),
            "var_CodigoManzana": $("#txtxCodigoManzana").val(),
            "var_Nombre": $("#txtNombreManzana").val(),
            "dbl_Area": $("#txtManzanaArea").val()
        };

        $.ajax({
            type: "post",
            url: "/Manzana/InsertarManzana",
            error: function (jqXHR, textStatus, errorThrown) {
                return console.log(jqXHR, errorThrown);
            },
            data: manzana,
            dataType: "JSON"
        }).success(function (json) {

            // ObtenerZona();
            $('#int_IdManzana').val(json.int_IdManzana);
            ConfirmarDetalleZona("Manzana");
        });
        //ConfirmarDetalleZona();
    });





    $("#btnGrabarLote").click(function () {


        var messajeError = ""
        if ($("#txtxCodigoLote").val() == '') {
            messajeError = messajeError + "Debe Ingresar el Codigo de Lote  \n";

        }
        if ($("#txtNombreLote").val() == '') {
            messajeError = messajeError + "Debe Ingresar Nombre de la Lote  \n";
        }
        if ($("#txtLoteArea").val() == '') {
            messajeError = messajeError + "Debe Ingresar el area de la Lote \n";
        }
        if ($('#tbldetalleLote tbody tr').length == 1) {
            messajeError = messajeError + "Debe Ingresar detalle de la Lote \n";
        }

        if (messajeError.length > 0) {
            alert(messajeError);
            return;
        }


        var manzana;
        manzana = {
            "int_IdManzana": $("#int_IdManzana").val(),
            "var_CodigoLote": $("#txtxCodigoLote").val(),
            "var_Nombre": $("#txtNombreLote").val(),
            "dbl_Area": $("#txtLoteArea").val()
        };

        $.ajax({
            type: "post",
            url: "/Lote/InsertarLote",
            error: function (jqXHR, textStatus, errorThrown) {
                return console.log(jqXHR, errorThrown);
            },
            data: manzana,
            dataType: "JSON"
        }).success(function (json) {

            // ObtenerZona();
            $('#int_IdLote').val(json.int_IdLote);
            ConfirmarDetalleZona("Lote");
        });
        //ConfirmarDetalleZona();
    });



});

function ObtenerSolicitud() {
    $.get(
         '../../solicitudcatastro/getsolicitudes', //Action method del controller Expediente
         function (jsonResult) {
             $('#tblSolicitudPendiente > tbody').children('tr:not(:first)').remove();
             //$("#tblSolicitudPendiente > tbody").append("<tr><th>Nro Solicitud</th><th>Fecha Emisión</th><th>Tipo Solicitud</th><th>Descripción</th> <th></th></tr> ");
             $.each(jsonResult, function () {
                 var currentTime = new Date(parseInt(this.dtm_FechaEmision.substr(6)));

                 var month = pad(currentTime.getMonth() + 1, 2);
                 var day = pad(currentTime.getDate(), 2); currentTime.getDate();

                 var year = currentTime.getFullYear();
                 var date = day + "/" + month + "/" + year;
                 $("#tblSolicitudPendiente > tbody").append("<tr><td>" + this.var_NroSolicitud + "</td><td>" + date + "</td><td>" + this.var_TipoSolicitud + "</td><td>" + this.var_Descripcion + "</td> <td><input type='radio' name='mygroup' class='seleccionarradio' id='" + this.var_NroSolicitud + "@" + this.var_TipoSolicitud + "@" + this.int_IdSolicitud + "@" + this.var_NroPropuesta + "' /></td></tr>");
             });
         }
     );

}
function EliminarZona() {

    $("#tblZonas input:radio").each(function () {

        var $this = $(this);

        if (this.checked) {
            var src = $this.attr('id');
            var campos = src.split("@");

            $.ajax({
                type: "post",
                url: "/Zona/EliminarZona",
                error: function (jqXHR, textStatus, errorThrown) {
                    return console.log(jqXHR, errorThrown);
                },
                data: { id: src },       //Parametro para el action method,
                dataType: "JSON"
            }).success(function (json) {
                if (json.success == "1")
                {
                    $("#TituloModalError").html("Eliminar Zona");
                    $("#p1ModalError").html("La Zona no puede ser eliminada");
                    $("#p2ModalError").html("No se puede Eliminar la Zona porque tiene Manzanas Asignadas");
                    $('#ModalError').removeClass('modal-danger');
                    $('#ModalError').removeClass('modal-success');
                    $('#ModalError').addClass('modal-danger');
                    $('#ModalError').modal('show');
                    
                }
                if (json.success == "2") {
                    $("#TituloModalError").html("Eliminar Zona");
                    $("#p1ModalError").html("La Zona fue eliminada satisfactoriamente!");
                    $("#p2ModalError").html("");
                    $('#ModalError').removeClass('modal-danger');
                    $('#ModalError').removeClass('modal-success');
                    $('#ModalError').addClass('modal-success');
                    $('#ModalError').modal('show');
                }
                ObtenerZona();
                
            });


        }

    });


}

function EliminarManzana() {

    $("#tblManzanas input:radio").each(function () {

        var $this = $(this);

        if (this.checked) {
            var src = $this.attr('id');
            var campos = src.split("@");

            $.ajax({
                type: "post",
                url: "/Manzana/EliminarManzana",
                error: function (jqXHR, textStatus, errorThrown) {
                    return console.log(jqXHR, errorThrown);
                },
                data: { id: src },       //Parametro para el action method,
                dataType: "JSON"
            }).success(function (json) {
                if (json.success == "1") {
                    $("#TituloModalError").html("Eliminar Manzana");
                    $("#p1ModalError").html("La Manzana no puede ser eliminada");
                    $("#p2ModalError").html("No se puede Eliminar la Mazana porque tiene Lotes Asignadas");
                    $('#ModalError').removeClass('modal-danger');
                    $('#ModalError').removeClass('modal-success');
                    $('#ModalError').addClass('modal-danger');
                    $('#ModalError').modal('show');

                }
                if (json.success == "2") {
                    $("#TituloModalError").html("Eliminar Manzana");
                    $("#p1ModalError").html("La Manzana fue eliminada satisfactoriamente!");
                    $("#p2ModalError").html("");
                    $('#ModalError').removeClass('modal-danger');
                    $('#ModalError').removeClass('modal-success');
                    $('#ModalError').addClass('modal-success');
                    $('#ModalError').modal('show');
                }
                ObtenerManzana();

            });


        }

    });


}



function EliminarLote() {

    $("#tblLotes input:radio").each(function () {

        var $this = $(this);

        if (this.checked) {
            var src = $this.attr('id');
            var campos = src.split("@");

            $.ajax({
                type: "post",
                url: "/Lote/EliminarLote",
                error: function (jqXHR, textStatus, errorThrown) {
                    return console.log(jqXHR, errorThrown);
                },
                data: { id: src },       //Parametro para el action method,
                dataType: "JSON"
            }).success(function (json) {
                if (json.success == "1") {
                    $("#TituloModalError").html("Eliminar Lote");
                    $("#p1ModalError").html("El Lote no puede ser eliminado");
                    $("#p2ModalError").html("No se puede Eliminar el lote porque tiene predios Asignados");
                    $('#ModalError').removeClass('modal-danger');
                    $('#ModalError').removeClass('modal-success');
                    $('#ModalError').addClass('modal-danger');
                    $('#ModalError').modal('show');

                }
                if (json.success == "2") {
                    $("#TituloModalError").html("Eliminar Lote");
                    $("#p1ModalError").html("El Lote fue eliminado satisfactoriamente!");
                    $("#p2ModalError").html("");
                    $('#ModalError').removeClass('modal-danger');
                    $('#ModalError').removeClass('modal-success');
                    $('#ModalError').addClass('modal-success');
                    $('#ModalError').modal('show');
                }
                ObtenerManzana();

            });


        }

    });


}




function EliminarPredio() {

    $("#tblPredio input:radio").each(function () {

        var $this = $(this);

        if (this.checked) {
            var src = $this.attr('id');
            var campos = src.split("@");

            $.ajax({
                type: "post",
                url: "/Predio/EliminarPredio",
                error: function (jqXHR, textStatus, errorThrown) {
                    return console.log(jqXHR, errorThrown);
                },
                data: { id: src },       //Parametro para el action method,
                dataType: "JSON"
            }).success(function (json) {
                
                if (json.success == "2") {
                    $("#TituloModalError").html("Eliminar Predio");
                    $("#p1ModalError").html("El predio fue eliminado satisfactoriamente!");
                    $("#p2ModalError").html("");
                    $('#ModalError').removeClass('modal-danger');
                    $('#ModalError').removeClass('modal-success');
                    $('#ModalError').addClass('modal-success');
                    $('#ModalError').modal('show');
                }
                ObtenerPredios();

            });


        }

    });


}



function AgregarDetalleZona(tipoadd) {
    //$('#tbldetalleZona > tbody').children('tr:not(:first)').remove();


    var messajeError = ""
    if ($("#txtMarca" + tipoadd).val() == '') {
        messajeError = messajeError + "Debe Ingresar Marca  \n"
        console.log('ValidaCodigo');
    }
    if ($("#txtreferecnia" + tipoadd).val() == '') {
        messajeError = messajeError + "Debe Ingresar Referencia\n"
    }
    if ($("#txtLatitud" + tipoadd).val() == '') {
        messajeError = messajeError + "Debe Ingresar latitud  \n"
    }

    if ($("#txtlongitud" + tipoadd).val() == '') {
        messajeError = messajeError + "Debe Ingresar longitud \n"
    }


    if (messajeError.length > 0) {
        alert(messajeError);
        return;
    }
    console.log('Aca toi');
    $("#tbldetalle" + tipoadd + " > tbody").append("<tr><td>0</td><td>" + $("#txtMarca" + tipoadd).val() + "</td><td>" + $("#txtreferecnia" + tipoadd).val() + "</td><td>" + $("#txtLatitud" + tipoadd).val() + "</td><td>" + $("#txtlongitud" + tipoadd).val() + "</td> <td><a href='javascript:void(0)' onclick='EliminarDetalleZona(this);' >Eliminar</a></td></tr>");
    LimpiarCajasZonaDetalle();
    //EliminarDetalleZona();
}




function EliminarDetalleZona(Element) {
    $(Element).parent().parent().remove();
}

function ConfirmarDetalleZona(tipoadd) {
    $("#tbldetalle" + tipoadd + " > tbody tr:gt(0)").each(function () {
        var this_row = $(this);


        var marca = $.trim(this_row.find('td:eq(1)').html())
        var referencia = $.trim(this_row.find('td:eq(2)').html())
        var latitud = $.trim(this_row.find('td:eq(3)').html())
        var longitud = $.trim(this_row.find('td:eq(4)').html())
        var idZona = $('#int_IdZona').val();
        var int_IdManzana = $("#int_IdManzana").val();
        var int_IdLote = $("#int_IdLote").val();


        var Geolocalizacion;
        Geolocalizacion = {
            "int_IdZona": idZona,
            "int_IdManzana": int_IdManzana,
            "int_IdLote": int_IdLote,
            "var_Marca": marca,
            "var_Referencia": referencia,
            "flt_Latitud": latitud,
            "flt_Longitud": longitud,
        };


        $.ajax({
            type: "post",
            url: "/Geolocalizacion/InsertarGeolocalizacion",
            error: function (jqXHR, textStatus, errorThrown) {
                return console.log(jqXHR, errorThrown);
            },
            data: Geolocalizacion,
            dataType: "JSON"
        }).success(function (json) {
            ObtenerZona();
            $('#ModalZona').modal('hide');

            ObtenerManzana();
            $('#modalManzana').modal('hide');

            ObtenerLote()
            $('#modalLote').modal('hide');


        });

    });
}
function LimpiarCajasZonaDetalle() {
    $("#txtMarcaZona").val('');
    $("#txtreferecniaZona").val('');
    $("#txtLatitudZona").val('');
    $("#txtlongitudZona").val('');
}
function SeleccionarSolicitud() {

    $("#tblSolicitudPendiente input:radio").each(function () {

        var $this = $(this);

        if (this.checked) {
            var src = $this.attr('id');
            var campos = src.split("@");

            $('#txtnroDolicitud').val(campos[0]);
            $('#txttiposolicitud').val(campos[1]);
            $('#int_IdSolicitud').val(campos[2]);
            $('#txtnroPropuesta').val('002');

            $('#myModal').modal('hide');


        }

    });
}
function ObtenerZona() {
    $.get(
         '../../Zona/GetZonas', //Action method del controller Expediente
         { int_IdSolicitud: $('#int_IdSolicitud').val() },       //Parametro para el action method
         function (jsonResult) {
             $('#tblZonas > tbody').children('tr:not(:first)').remove();
             
             var sel = $(".ddlZona");
             var variable = ""
             if ($('#int_IdSolicitud').val() == -1)
                 variable = "Seleccionar Manzana"
             else
                 variable = "Agregar Manzana"

             sel.empty();
             $.each(jsonResult, function () {
                 $("#tblZonas > tbody").append("<tr><td>" + this.int_IdZona + "</td><td>" + this.var_Nombre + "</td><td><a id='" + this.int_IdZona + "'  onclick='SeleccionarManzana(this);' href='javascript:void(0)' >" + variable + "</a></td><td><input type='radio' name='mygroup2' id='" + this.int_IdZona + "' /></td></tr>");
                 sel.append('<option value="' + this.int_IdZona + '">' + this.var_Nombre + '</option>');
             });

         }
     );

}


function ObtenerUnaZona() {
    $.get(
         '../../Zona/GetZona', //Action method del controller Expediente
         { int_IdZona: $('#int_IdZona').val() },       //Parametro para el action method
         function (jsonResult) {
             $('#txtPreCodigoZona').val(jsonResult.var_CodigoZona);

         }
     );

}


function SeleccionarManzana(Element) {
    $('#int_IdZona').val($(Element).attr('id'));
    $('#myTab li:eq(1) a').tab('show');
    $('.ddlZona').val($(Element).attr('id'));
    ObtenerManzana();

}
function ObtenerUnaManzana() {
    $.get(
         '../../Manzana/GetManzana', //Action method del controller Expediente
         { int_IdManzana: $('#int_IdManzana').val() },       //Parametro para el action method
         function (jsonResult) {
             $('#txtCodigoManzana').val(jsonResult.var_CodigoManzana);
             $('#txtPreNombreMz').val(jsonResult.var_Nombre);
         }
     );

}

function ObtenerManzana() {
    $.get(
         '../../Manzana/GetManzanas', //Action method del controller Expediente
         { int_IdZona: $('#int_IdZona').val() },       //Parametro para el action method
         function (jsonResult) {
             $('#tblManzanas > tbody').children('tr:not(:first)').remove();
             var sel = $(".ddlManzana");
             sel.empty();
             if ($('#int_IdSolicitud').val() == -1)
                 variable = "Seleccionar Lote"
             else
                 variable = "Agregar Lote"

             $.each(jsonResult, function () {
                 $("#tblManzanas > tbody").append("<tr><td>" + this.int_IdManzana + "</td><td>" + this.var_Nombre + "</td><td><a id='" + this.int_IdManzana + "' onclick='SeleccionarLote(this);' href='javascript:void(0)' >" + variable + "</a></td><td><input type='radio' name='mygroup2' id='" + this.int_IdManzana + "' /></td></tr>");
                 sel.append('<option value="' + this.int_IdManzana + '">' + this.var_Nombre + '</option>');
             });
             $('#int_IdManzana').val($(".ddlManzana").val());
             ObtenerLote();
         }
         
     );

}
function SeleccionarLote(Element) {
    $('#int_IdManzana').val($(Element).attr('id'));
    $('#myTab li:eq(2) a').tab('show');
    $('.ddlManzana').val($(Element).attr('id'));
    $('.ddlZona').val($('#int_IdZona').val());
    ObtenerLote();

}

function ObtenerLote() {
    $.get(
         '../../Lote/GetLote', //Action method del controller Expediente
         { int_IdManzana: $('#int_IdManzana').val() },       //Parametro para el action method
         function (jsonResult) {
             $('#tblLotes > tbody').children('tr:not(:first)').remove();
             

             var sel = $(".ddlote");
             sel.empty();
             if ($('#int_IdSolicitud').val() == -1)
                 variable = ""
             else
                 variable = "Agregar Predio"
             $.each(jsonResult, function () {
                 $("#tblLotes > tbody").append("<tr><td>" + this.int_IdLote + "</td><td>" + this.var_Nombre + "</td><td><a id='" + this.int_IdLote + "' onclick='SeleccionarPredio(this);' href='javascript:void(0)' >" + variable + "</a></td><td><input type='radio' name='mygroup2' id='" + this.int_IdLote + "' /></td></tr>");
                 sel.append('<option value="' + this.int_IdLote + '">' + this.var_Nombre + '</option>');
             });
             $('#int_IdLote').val($(".ddlote").val());
             ObtenerPredios();
         }
     );

}

function ObtenerUnLote() {
    $.get(
         '../../Lote/GetUnLote', //Action method del controller Expediente
         { int_IdLote: $('#int_IdLote').val() },       //Parametro para el action method
         function (jsonResult) {
             console.log(jsonResult);
             $('#txtPreCodigoLote').val(jsonResult.var_CodigoLote);
             $('#txtPreNombreLote').val(jsonResult.var_Nombre);
             $('#txtPreDirección').val(jsonResult.var_Direccion);
             

         }
     );

}

function SeleccionarPredio(Element) {
    $('#int_IdLote').val($(Element).attr('id'));
    $('#myTab li:eq(3) a').tab('show');
    ObtenerPredios();
}
function ObtenerPredios() {
    $.get(
         '../../Predio/GetPredio', //Action method del controller Expediente
         { int_IdLote: $('#int_IdLote').val() },       //Parametro para el action method
         function (jsonResult) {
             $('#tblPredio > tbody').children('tr:not(:first)').remove();
             //$("#tblSolicitudPendiente > tbody").append("<tr><th>Nro Solicitud</th><th>Fecha Emisión</th><th>Tipo Solicitud</th><th>Descripción</th> <th></th></tr> ");
             $.each(jsonResult, function () {
                 $("#tblPredio > tbody").append("<tr><td>" + this.chr_CodigoPredio + "</td><td>" + this.var_Nombre + "</td><td></td> <td><input type='radio' name='mygrouppredio' class='seleccionarradio' id='" + this.int_IdPredio + "' /></td></tr>");
             });
         }
     );

};
$('.ddlZona').on('change', function () {
    $('#int_IdZona').val(this.value)
    ObtenerManzana();
    
    
    
});
$('.ddlManzana').on('change', function () {
    $('#int_IdManzana').val(this.value)
    ObtenerLote();
});
$('.ddlote').on('change', function () {
    $('#int_IdLote').val(this.value)
    ObtenerPredios();
});
function EliminarGeneral(Condicion) {
    if (Condicion == "Eliminar Zona") {
        EliminarZona();
    }
    if (Condicion == "Eliminar Manzana") {
        EliminarManzana();
    }
    if (Condicion == "Eliminar Lote") {
        EliminarLote();
    }
    if (Condicion == "Eliminar Predio") {
        EliminarPredio();
    }
    
    $('#ModalConfir').modal('hide');
}



function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
}

