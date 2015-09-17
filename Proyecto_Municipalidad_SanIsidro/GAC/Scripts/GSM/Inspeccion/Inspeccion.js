

$(document).ready(function () {

    fn_CargaTipoServicio();
    $("#txtFeInspeccion").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txthini").kendoMaskedTextBox({ mask: "##:##" });
    $("#txthfin").kendoMaskedTextBox({ mask: "##:##" });

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

    /***********************************************************************************************/
    $("#txtResponsable").kendoAutoComplete({
        template: '<span class="order-id">#= Nombre #</span> --  #= DNI #',
        dataTextField: "Nombre",
        height: 520,
        minLength: 4,
        dataSource: {
            type: "odata",
            transport: {
                read: {
                    url: path + "GSMPersona/ListaBusqueda",
                    dataType: "json"
                },
                parameterMap: function (options, type) {
                    var paramMap = kendo.data.transports.odata.parameterMap(options);
                    paramMap.keyname = $('#txtResponsable').val();
                    delete paramMap.$inlinecount;
                    delete paramMap.$format;
                    delete paramMap.$filter;
                    return paramMap;
                }
            },

            schema: {
                data: "data",
                total: "total"
            },

            pageSize: 80,
            serverPaging: true,
            serverFiltering: true
        },
        select: onSelect,
        change: onChange,
    });


    function onChange() {
        console.log("event :: change");
    }

    function onSelect(e) {
        console.log(e.item.index());
        var dataItem = this.dataItem(e.item.index());
        console.log(dataItem);
        $('#hidResponsable').val(dataItem.Codigo);
    }
    /***********************************************************************************************/
    var grid = $("#gridServicio").kendoGrid(_params);
    /***********************************************************************************************/

});







function fn_CargaBusquedaServicio(e) {
    e.preventDefault();

    $('#ModalServicio').modal('show');

}


function fn_BuscarServicio() {
    $("#gridServicio").data("kendoGrid").dataSource.read();
}

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


function fn_SeleccionarServ(vItem) {
    var Codigo = $(vItem).parent().parent().children().html();
    var nomServ = $(vItem).parent().parent().children().next().html();
    var TipServ = $(vItem).parent().parent().children().next().next().html();

    $('#hidServicio').val(Codigo);
    $('#txtCodServ').val(Codigo);
    $('#txtNomServ').val(nomServ);
    $('#txtTipServ').val(TipServ);


    $('#ModalServicio').modal('toggle');
}

function fnGuardarServicio() {
    $('#hidTypeButton').val("0");

    var obs = $('#txtNomServ').val();
    var ini = $('#txtResponsable').val();
    var fec = $('#txtFeInspeccion').val();
    var hini = $('#txthini').val();
    var hfin = $('#txthfin').val();


    var str = '';

    if (fec == '') {
        str += 'Seleccione una Fecha<br/>';
    }
    if (hini == '') {
        str += 'Seleccione una hora de inicio<br/>';
    }
    if (hfin == '') {
        str += 'Seleccione una hora de fin<br/>';
    } 


    if (ini == '') {
        str += 'Seleccione una Servicio<br/>';
    }
    if (obs == '') {
        str += 'Ingrese un Responsable<br/>';
    }

    console.log(str);
    if (str != '') {
        //alert(str);

        LlamarAlert('Validar Campos', str, function () { });
        return false;
    }
    $('#hidTypeButton').val("1");
    LlamarConfirmar('Registro de Inspeccion', '¿Seguro que desea registrar la inspección?', function () { });


}

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

    console.log('click');

}//);


function fnCancelar() {
    $('#hidTypeButton').val("3");
    LlamarConfirmar('Cancelar Inspeccion', '¿Seguro que desea cancelar la inspección?', function () { });

}


function RegistrarServicio() { 

    var Inspeccion = {};
    Inspeccion.IdPersona = $('#hidResponsable').val();
    Inspeccion.personaAsignada = $('#hidServicio').val();
    Inspeccion.horaInicio = $('#txthini').val();
    Inspeccion.horaFin = $('#txthfin').val();
    Inspeccion.direccion = $('#txtDireccion').val();
    Inspeccion.fechaProgramada = $('#txtFeInspeccion').val();

    var cadJson = JSON.stringify({ oInspeccion: Inspeccion });

    $.ajax({//((IV.ID > 0) ?: "JuntaVecinal/SaveRequerimiento"
        type: "POST",
        url: path + "GSMInspeccion/SaveInspeccion",
        data: cadJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);

            var nro = response.ID.substring(0,1);
            var msj = response.ID.substring(1,response.ID.length);

            console.log(nro);
            console.log(msj);
            if (nro==1) {
                strmsg = 'Mensaje : ' + msj;

                LlamarAlert('Registro con éxito', strmsg, function () {
                    var strUrl = path + 'GSMInspeccion/';
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

function AnularInspeccion() {

    var Inspeccion = {};
    Inspeccion.IdRegistro = $('#hidId').val();

    var cadJson = JSON.stringify({ oInspeccion: Inspeccion });

    $.ajax({//((IV.ID > 0) ?: "JuntaVecinal/SaveRequerimiento"
        type: "POST",
        url: path + "GSMInspeccion/CancelInspeccion",
        data: cadJson,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.ID >0) {
                strmsg = 'Mensaje : ' + "inspección cancelada";

                LlamarAlert('Registro con éxito', strmsg, function () {
                    var strUrl = path + 'GSMInspeccion/';
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

function fnRetroceder() { 
        var strUrl = path + "GSMInspeccion/";
        window.location = strUrl; 
}

function fnActualizarServicio() {
    $('#hidTypeButton').val("0");

    var obs = $('#txtNomServ').val();
 
    var str = '';
 
    if (obs == '') {
        str += 'Ingrese un Responsable<br/>';
    }

    console.log(str);
    if (str != '') { 
        LlamarAlert('Validar Campos', str, function () { });
        return false;
    }
    $('#hidTypeButton').val("2");
    LlamarConfirmar('Actualizar Inspeccion', '¿Seguro que desea Actualizar la inspección?', function () { });

}
 
function ActualizarServicio() {

    var Inspeccion = {};
    Inspeccion.IdPersona = $('#hidResponsable').val();
    Inspeccion.personaAsignada = $('#hidServicio').val();
    Inspeccion.horaInicio = $('#txthini').val();
    Inspeccion.horaFin = $('#txthfin').val();
    Inspeccion.direccion = $('#txtDireccion').val();
    Inspeccion.fechaProgramada = $('#txtFeInspeccion').val();
    Inspeccion.IdRegistro = $('#hidId').val();

    var cadJson = JSON.stringify({ oInspeccion: Inspeccion });

    $.ajax({//((IV.ID > 0) ?: "JuntaVecinal/SaveRequerimiento"
        type: "POST",
        url: path + "GSMInspeccion/UpdateInspeccion",
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
                    var strUrl = path + 'GSMInspeccion/';
                    window.location = strUrl;
                });
            } else {
                strmsg = msj;
                //alert(strmsg);
                LlamarAlert('Actualizado con éxito' , strmsg, function () { });
            }


        },
        failure: function (response) {
        },
        error: function (data) {
        }
    });

}


