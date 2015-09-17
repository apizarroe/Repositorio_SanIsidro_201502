

$(document).ready(function () {

    fn_CargaTipoServicio();
    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });

    fn_CargarBuscarServicio();
});

function fn_CargaTipoServicio() {
    var lst = '';
    $.get(path + "GSMmaestro/ListaTipoServicio", function (data) {
        console.log(data);
        var lstEnt = data;
        var strItems = '';
        $('#cboSearch').find('option').remove();
        var strItems = '<option value="0">Seleccione</option>';
        if (lstEnt.length) {
            for (var i = 0; i < lstEnt.length; i++) {
                //{ID: 8, N: "ESTILOS (LIMA)"}
                strItems += '<option value=' + lstEnt[i].value + '>' + lstEnt[i].text + '</option>';
            }
        }
        $('#cboSearch').append(strItems);
    });
}

/***************************************************************/

function fn_AceptarAlert() { //$('#btnAceptarModalConfirm').click(function () {
    $('#ModalConfirm').modal('toggle');
    var tipo = $('#hidTypeButton').val();

    if (tipo == 1) {//Registrar
        RegistrarServicio();
    } else if (tipo == 2) {//Actualizar
        ActualizarServicio();
    } else if (tipo == 3) {//Cancelar
        AnularServicio();
    }
}

function RegistrarServicio() {
    var InformeServicio = {};
    var Servicio = {};

    Servicio.IdServicio = $('#hidServicio').val();
    InformeServicio.oServicio = Servicio;
    InformeServicio.Contenido = $('#txtContenido').val();
    InformeServicio.Observacion = $('#txtObs').val();
    InformeServicio.Fecha = $('#txtFecha').val();

    /*************************************************/
    InformeServicio.lstInforme = new Array();

    $('#tblInspeccion tbody tr').each(function () {
        //falta 666
        console.log($(this).children().next().html());

        var check = $(this).children().children();
        if (check.is(":checked")) {
            var InformeInspeccion = {};
            InformeInspeccion.IdInforme = $(this).children().next().html();
            InformeServicio.lstInforme.push(InformeInspeccion);  
        }  
    });
     
    /*************************************************/

    var cadJson = JSON.stringify({ oInforme: InformeServicio });

    $.ajax({//((IV.ID > 0) ?: "JuntaVecinal/SaveRequerimiento"
        type: "POST",
        url: path + "GSMServicio/SaveInfoServicio",
        data: cadJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            var nro = response.ID.substring(0, 1);
            var msj = response.ID.substring(1, response.ID.length);

            console.log(nro);
            console.log(msj);
            if (nro == 1) {
                strmsg = 'Mensaje : ' + msj;

                LlamarAlert('Registro con éxito', strmsg, function () {
                    var strUrl = path + 'GSMServicio/InfoIndex';
                    window.location = strUrl;
                });
            } else {
                strmsg = msj;
                //alert(strmsg);
                LlamarAlert('Registro con éxito', strmsg, function () { });
            }
        },
        failure: function (response) {
        },
        error: function (data) {
        }
    });

}

function ActualizarServicio() {
    var InformeServicio = {};
    var Servicio = {};

    InformeServicio.IdInforme = $('#hidId').val();
    Servicio.IdServicio = $('#hidServicio').val();
    InformeServicio.oServicio = Servicio;
    InformeServicio.Contenido = $('#txtContenido').val();
    InformeServicio.Observacion = $('#txtObs').val();
    InformeServicio.Fecha = $('#txtFecha').val();


    var cadJson = JSON.stringify({ oInforme: InformeServicio });

    $.ajax({
        type: "POST",
        url: path + "GSMServicio/UpdateInfoServicio",
        data: cadJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);

            var nro = response.ID.substring(0, 1);
            var msj = response.ID.substring(1, response.ID.length);

            console.log(nro);
            console.log(msj);
            if (nro == 1) {
                strmsg = 'Mensaje : ' + msj;

                LlamarAlert('Actualizado con éxito', strmsg, function () {
                    var strUrl = path + 'GSMServicio/InfoIndex';
                    window.location = strUrl;
                });
            } else {
                strmsg = msj;
                //alert(strmsg);
                LlamarAlert('Actualizado con éxito', strmsg, function () { });
            }
        },
        failure: function (response) {
        },
        error: function (data) {
        }
    });

}

function AnularServicio() {

    var InformeServicio = {};
    InformeServicio.IdInforme = $('#hidId').val();
    var cadJson = JSON.stringify({ oInforme: InformeServicio });

    $.ajax({//((IV.ID > 0) ?: "JuntaVecinal/SaveRequerimiento"
        type: "POST",
        url: path + "GSMServicio/CancelInfoServicio",
        data: cadJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.ID > 0) {
                strmsg = 'Mensaje :Informe cancelado con éxito. ';

                LlamarAlert('Registro con éxito', strmsg, function () {
                    var strUrl = path + 'GSMServicio/InfoIndex';
                    window.location = strUrl;
                });
            } else {
                strmsg = 'Inconvenientes para cancelar.';
                //alert(strmsg);
                LlamarAlert('Cancelacion denegada', strmsg, function () { });
            }
        },
        failure: function (response) {
        },
        error: function (data) {
        }
    });
}

/***************************************************************/

function fnGuardarServicio() {
    $('#hidTypeButton').val("0");

    //var idInspeccion = $('#hidId').val();
    var idServicio = $('#hidServicio').val();
    var contenido = $('#txtContenido').val();
    var fecha = $('#txtFecha').val();
    var Obs = $('#txtObs').val();

    var str = '';

    if (fecha == '') {
        str += 'Seleccione una Fecha<br/>';
    }
    if (contenido == '') {
        str += 'Ingrese un contenido<br/>';
    }
    //if (idInspeccion == '' || idInspeccion == '0') {
    //    str += 'Seleccione una inspección<br/>';
    //}
    if (idServicio == '' || idServicio == '0') {
        str += 'Seleccione un servicio<br/>';
    }

    if (Obs == '') {
        str += 'Ingrese una observación<br/>';
    }

    //Validar check : 
    var cont = 0;
    $('#tblInspeccion tbody tr').each(function () {
        //falta 666
        var check = $(this).children().children();
        //if ($("#chkBateria").is(":checked")) {
        if (check.is(":checked")) {
            cont++;
        }
    });

    if (cont == 0) {
        str += 'Seleccione un informe de inspección<br/>';
    }

    console.log(str);
    if (str != '') {
        LlamarAlert('Validar Campos', str, function () { });
        return false;
    }
    $('#hidTypeButton').val("1");
    LlamarConfirmar('Registro de Informe de servicio', '¿Seguro que desea registrar el informe de servicio?', function () { });
}

function fnActualizarServicio() {
    $('#hidTypeButton').val("0");

    var idInspeccion = $('#hidId').val();
    var idServicio = $('#hidServicio').val();
    var contenido = $('#txtContenido').val();
    var fecha = $('#txtFecha').val();
    var Obs = $('#txtObs').val();

    var str = '';

    if (fecha == '') {
        str += 'Seleccione una Fecha<br/>';
    }
    if (contenido == '') {
        str += 'Ingrese un contenido<br/>';
    }
    if (idInspeccion == '' || idInspeccion == '0') {
        str += 'Seleccione una inspección<br/>';
    }
    if (idServicio == '' || idServicio == '0') {
        str += 'Seleccione un servicio<br/>';
    }

    if (Obs == '') {
        str += 'Ingrese una observación<br/>';
    }

    console.log(str);
    if (str != '') {
        LlamarAlert('Validar Campos', str, function () { });
        return false;
    }
    $('#hidTypeButton').val("2");
    LlamarConfirmar('Actualizar informe de servicio', '¿Seguro que desea Actualizar el informe de servicio?', function () { });
}

function fnCancelar() {
    $('#hidTypeButton').val("3");
    LlamarConfirmar('Cancelar Informe de servicio', '¿Seguro que desea cancelar el informe de servicio?', function () { });

}

function fnRetroceder() {
    var strUrl = path + "GSMServicio/InfoIndex";
    window.location = strUrl;
}

/***************************************************************/


function fn_CargaBusquedaServicio(e) {
    e.preventDefault();

    $('#ModalServicio').modal('show');

}

function fn_BuscarServicio() {
    $("#gridServicio").data("kendoGrid").dataSource.read();
}

function fn_CargarBuscarServicio() {

    var dsServicio = new kendo.data.DataSource({
        serverFiltering: true,
        serverPaging: true,
        type: 'odata',
        serverSorting: true,
        pageSize: 5,
        transport: {
            read: {
                url: path + "GSMServicio/ListaBusqueda",
                dataType: "json"
            },
            parameterMap: function (options, type) {
                var Paginacion = options.pageSize;
                var Pagina = options.page;
                var paramMap = kendo.data.transports.odata.parameterMap(options);
                paramMap.Pagina = Pagina;
                paramMap.Paginacion = Paginacion;
                paramMap.Cod = $('#txtSearchCod').val();
                paramMap.Nom = $('#txtSearchNom').val();
                paramMap.Tipo = $('#cboSearch').val();


                delete paramMap.$inlinecount;
                delete paramMap.$format;
                delete paramMap.$filter;
                return paramMap;
            }
        },
        schema: {
            data: "data",
            total: "total"
        }
    });
    /***********************************************************************************************/

    var _params = {
        dataSource: dsServicio,
        pageable: { refresh: true, pageSizes: [5, 10, 20], pageSize: 5, buttonCount: 3 },
        //filterable: { mode: "row" }, 
        filterable: { extra: false },
        editable: false,
        //height: 543
        columns: [
           // { field: "ID", hidden: true },
            { field: "IdServicio", title: "Código", width: 30, filterable: true },
            { field: "Nombre", title: "Nombre", width: 120, filterable: false },
            { field: "TipoServicio", title: "Tipo Servicio", width: 70, filterable: false },
            { command: [{ name: "Registrar", template: "<div class='k-grid-historico k-button' onclick='fn_SeleccionarServ(this);' ><span class='k-icon k-i-maximize'></span>&nbsp;&nbsp;Seleccionar</div>" }], title: " ", width: 100 },
            //{ command: [{ name: "Registrar", template: "<div class='k-grid-historico k-button' onclick='fn_Click(this);' ><span class='k-icon k-i-maximize'></span>&nbsp;&nbsp;Ver Detalle</div>" }], title: " ", width: 150 } 

        ]
    };
    var grid = $("#gridServicio").kendoGrid(_params);
}



function fn_SeleccionarServ(vItem) {
    var Codigo = $(vItem).parent().parent().children().html();
    var nomServ = $(vItem).parent().parent().children().next().html();
    var TipServ = $(vItem).parent().parent().children().next().next().html();

    $('#hidServicio').val(Codigo);
    $('#txtCodServ').val(Codigo);
    $('#txtNomServ').val(nomServ);
    $('#txtTipServ').val(TipServ);

    fn_PintarTabla(Codigo);

    $('#ModalServicio').modal('toggle');
}

function fn_PintarTabla(id) {

    if (id > 0) {

        $.ajax({//((IV.ID > 0) ?: "JuntaVecinal/SaveRequerimiento"
            type: "POST",
            url: path + "GSMServicio/GetListaDetalleById?IdServicio=" + id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log(response);
                var strFila = '';

                for (var i = 0; i < response.length; i++) {
                    strFila += fn_CrearFila(response[i], i);
                }

                $('#tblInspeccion tbody').append(strFila);

            },
            failure: function (response) {
            },
            error: function (data) {
            }
        });

    }

}

function fn_CrearFila(item, contador) {
    //    Seleccione	Nro Informe	Fecha de Informe	Tipo Servicio	Servicio

    var strFila = '';
    strFila += '<tr role="row" ' + (contador % 2 == 0 ? "" : 'class="k-alt"') + ' >';
    strFila += '     <td  style="width:10%;">' + '<input type="checkbox" class="chk" />' + '</td>';
    strFila += '     <td  style="width:15%;">' + item.IdInforme + '</td>';
    strFila += '     <td role="gridcell"  style="width:15%;">' + item.Fecha + '</td>';
    strFila += '     <td role="gridcell" style="width:15%;">' + item.oServicio.TipoServicio + '</td>';
    strFila += '     <td role="gridcell">' + item.oServicio.Nombre + '</td>';
    strFila += '</tr>';
    return strFila;
}

function fn_CargaServicio(id) {

    $.ajax({//((IV.ID > 0) ?: "JuntaVecinal/SaveRequerimiento"
        type: "GET",
        url: path + "GSMInspeccion/GetInspeccion?id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);

            $('#hidServicio').val(response.oServicio.IdServicio);
            $('#txtCodServ').val(response.oServicio.IdServicio);
            $('#txtNomServ').val(response.oServicio.Nombre);
            $('#txtTipServ').val(response.oServicio.TipoServicio);
            /*
Filas: 0
IdCodVia: 0
IdPersona: 2
IdRegistro: 3
IdUsuario: 0
direccion: "bfdbsdfbsdf"
fechaProgramada: "09/09/2015"
horaFin: "10:00"
horaInicio: "09:00"
nuevo: false
oServicio: Object
        Filas: 0
        IdServicio: 3
        IdTipoServicio: 1
        Nombre: "Servicio Limpieza"
        TipoServicio: "Salud Humana"
personaAsignada: "CESAR PEREZ DE CUELLAR"
            */


        },
        failure: function (response) {
        },
        error: function (data) {
        }
    });

}

/***************************************************************/


