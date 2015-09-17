

$(document).ready(function () {

    fn_CargaTipoServicio();
    $("#txtFecha").kendoDatePicker({ format: "dd/MM/yyyy" });

});


function fn_CargaTipoServicio() {
    var lst = '';
    $.get(path + "GSMmaestro/ListaTipoServicio", function (data) {
        console.log(data);
        var lstEnt = data;
        var strItems = '';
        $('#cboTipoServicio').find('option').remove();
        var strItems = '<option value="0">Seleccione</option>';
        if (lstEnt.length) {
            for (var i = 0; i < lstEnt.length; i++) {
                //{ID: 8, N: "ESTILOS (LIMA)"}
                strItems += '<option value=' + lstEnt[i].value + '>' + lstEnt[i].text + '</option>';
            }
        }
        $('#cboTipoServicio').append(strItems);
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
        AnularInspeccion();
    }
}

function RegistrarServicio() {

    var InformeInspeccion = {};
    var Servicio = {};
    Servicio.IdServicio = $('#hidServicio').val();

    InformeInspeccion.oServicio = Servicio;
    InformeInspeccion.IdInspeccion = $('#hidInspeccion').val();
    InformeInspeccion.descripcion = $('#txtContenido').val();
    InformeInspeccion.titulo = $('#txtNomServ').val();
    InformeInspeccion.Fecha = $('#txtFecha').val();

    var cadJson = JSON.stringify({ oInforme: InformeInspeccion });

    $.ajax({//((IV.ID > 0) ?: "JuntaVecinal/SaveRequerimiento"
        type: "POST",
        url: path + "GSMInformeInspeccion/SaveInfoInspeccion",
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
                    var strUrl = path + 'GSMInformeInspeccion/';
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
    var InformeInspeccion = {};
    var Servicio = {};
    Servicio.IdServicio = $('#hidServicio').val();

    InformeInspeccion.IdInforme = $('#hidId').val();
    InformeInspeccion.oServicio = Servicio;
    InformeInspeccion.IdInspeccion = $('#hidInspeccion').val();
    InformeInspeccion.descripcion = $('#txtContenido').val();
    InformeInspeccion.titulo = $('#txtNomServ').val();
    InformeInspeccion.Fecha = $('#txtFecha').val();
    var cadJson = JSON.stringify({ oInforme: InformeInspeccion });

    $.ajax({
        type: "POST",
        url: path + "GSMInformeInspeccion/UpdateInfoInspeccion",
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
                    var strUrl = path + 'GSMInformeInspeccion/';
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

function AnularInspeccion() {

    var InformeInspeccion = {};
    InformeInspeccion.IdInforme = $('#hidId').val(); 
    var cadJson = JSON.stringify({ oInforme: InformeInspeccion });
     
    $.ajax({//((IV.ID > 0) ?: "JuntaVecinal/SaveRequerimiento"
        type: "POST",
        url: path + "GSMInformeInspeccion/CancelInfoInspeccion",
        data: cadJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.ID > 0) {
                strmsg = 'Mensaje :Informe cancelado con éxito. ';

                LlamarAlert('Registro con éxito', strmsg, function () {
                    var strUrl = path + 'GSMInformeInspeccion/';
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

    var idInspeccion = $('#hidInspeccion').val();
    var idServicio = $('#hidServicio').val();
    var contenido = $('#txtContenido').val();
    var fecha = $('#txtFecha').val();

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

    console.log(str);
    if (str != '') {
        LlamarAlert('Validar Campos', str, function () { });
        return false;
    }
    $('#hidTypeButton').val("1");
    LlamarConfirmar('Registro de Informe de Inspección', '¿Seguro que desea registrar el informe de inspección?', function () { });
}

function fnActualizarServicio() {
    $('#hidTypeButton').val("0");

    var idInspeccion = $('#hidInspeccion').val();
    var idServicio = $('#hidServicio').val();
    var contenido = $('#txtContenido').val();
    var fecha = $('#txtFecha').val();

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

    console.log(str);
    if (str != '') {
        LlamarAlert('Validar Campos', str, function () { });
        return false;
    }
    $('#hidTypeButton').val("2");
    LlamarConfirmar('Actualizar informa inspección', '¿Seguro que desea Actualizar el informe inspección?', function () { });
}

function fnCancelar() {
    $('#hidTypeButton').val("3");
    LlamarConfirmar('Cancelar Inspección', '¿Seguro que desea cancelar el informe de inspección?', function () { });

}

function fnRetroceder() {
    var strUrl = path + "GSMInformeInspeccion/";
    window.location = strUrl;
}

/***************************************************************/

function fn_CargaBusquedaInspec(e) {
    e.preventDefault();

    $('#ModalInspeccion').modal('show');
}

function fn_BuscarInspec() {


    var dsRequerimiento = new kendo.data.DataSource({
        serverFiltering: true,
        serverPaging: true,
        type: 'odata',
        serverSorting: true,
        pageSize: 5,
        transport: {
            read: {
                url: path + "GSMInspeccion/ListaInspeccion",
                dataType: "json"
            },
            parameterMap: function (options, type) {
                var Paginacion = options.pageSize;
                var Pagina = options.page;
                var paramMap = kendo.data.transports.odata.parameterMap(options);
                paramMap.Pagina = Pagina;
                paramMap.Paginacion = Paginacion;
                paramMap.fnIni = $('#txtFecProg').val();
                paramMap.fnFin = $('#txtFecProgf').val();
                paramMap.tipo = $('#cboTipoServicio').val();

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
        dataSource: dsRequerimiento,
        pageable: { refresh: true, pageSizes: [5, 10, 20], pageSize: 10, buttonCount: 3 },
        //filterable: { mode: "row" }, 
        filterable: { extra: false },
        editable: false,
        //height: 543,
        columns: [
            { field: "IdRegistro", hidden: true },
            { field: "IdServicio", hidden: true },
            { field: "servicio", title: "Servicio", width: 100, filterable: true },
            { field: "tipoServicio", title: "Tipo servicio", width: 100, filterable: false },
            { field: "fechaProgramada", title: "Fecha Programada", width: 100, filterable: false },
            { field: "personaAsignada", title: "Persona Asignada", width: 100, filterable: false },
            { command: [{ name: "Seleccionar", template: "<div class='k-grid-historico k-button' onclick='fn_SelectIns(this);' ><span class='k-icon k-i-maximize'></span>&nbsp;&nbsp;Seleccionar</div>" }], title: " ", width: 150 },
        ]
    };

    /***********************************************************************************************/
    var grid = $("#gridInspeccion").kendoGrid(_params);
    /***********************************************************************************************/
    //  $("#grid").data("kendoGrid").dataSource.read();

}

function fn_SelectIns(vItem) {

    var id = $(vItem).parent().parent().children().html();
    var idServicio = $(vItem).parent().parent().children().next().html();

    id = parseInt(id);

    $('#hidInspeccion').val(id);
    $('#txtidInspeccion').val(id);
    fn_CargaServicio(id);// se carga por id de inspeccion
    $('#ModalInspeccion').modal('toggle');

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


