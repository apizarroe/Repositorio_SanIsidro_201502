
//txtIdInfo
$(document).ready(function () {
    fn_CargaTipoServicio();
    fn_CargaServicio();
    $('#txtFeIni').kendoDatePicker({ format: "dd/MM/yyyy" });
    $('#txtFeFin').kendoDatePicker({ format: "dd/MM/yyyy" });
});


function fn_Click(vitem) {
    var id = $(vitem).parent().parent().children().html();
    id = parseInt(id);

    if (id > 0) {
        var strUrl = path + "GSMServicio/InfoServicio?Id=" + id;
        window.location = strUrl;
    }
}

function fn_AnularClick(vitem) {

    var id = $(vitem).parent().parent().children().html();
    $('#hidId').val(id);
    LlamarConfirmar('Cancelar Informe', '¿Seguro que desea cancelar el informe de Servicio?', function () { });

}


function fn_AceptarAlert() {
    $('#ModalConfirm').modal('toggle');
 
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
                    fn_Buscar();
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

function fn_Buscar() {


    var dsRequerimiento = new kendo.data.DataSource({
        serverFiltering: true,
        serverPaging: true,
        type: 'odata',
        serverSorting: true,
        pageSize: 5,
        transport: {
            read: {
                url: path + "GSMServicio/ListaInfoServicio",
                dataType: "json"
            },
            parameterMap: function (options, type) {
                var Paginacion = options.pageSize;
                var Pagina = options.page;
                var paramMap = kendo.data.transports.odata.parameterMap(options);

                paramMap.id = $('#txtIdInfo').val();
                paramMap.idServicio = $('#cboServicio').val();
                paramMap.tipo = $('#cboTipo').val();
                paramMap.fnIni = $('#txtFeIni').val();
                paramMap.fnFin = $('#txtFeFin').val();
                paramMap.Pagina = Pagina;
                paramMap.Paginacion = Paginacion;



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
            { field: "IdInforme", hidden: true }, 
            { field: "TipoServicio", title: "Tipo de servicio", width: 100, filterable: false },
            { field: "Servicio", title: "Nombre servicio", width: 100, filterable: false },
            { field: "Fecha", title: "Fecha y hora", width: 100, filterable: false },
            { command: [{ name: "Editar", template: "<div class='k-grid-historico k-button' onclick='fn_Click(this);' ><span class='k-icon k-i-maximize'></span>&nbsp;&nbsp;Detalle</div>" }], title: "Editar", width: 150 },
            { command: [{ name: "Anular", template: "<div class='k-grid-historico k-button' onclick='fn_AnularClick(this);' ><span class='k-icon k-i-maximize'></span>&nbsp;&nbsp;Anular</div>" }], title: "Anular", width: 150 }
        ]
    };
      
    /***********************************************************************************************/
    var grid = $("#grid").kendoGrid(_params);
    /***********************************************************************************************/
    //  $("#grid").data("kendoGrid").dataSource.read();

}

function fn_CrearNuevo() {

    //var idIni = $('#hidIni').val();
    var strUrl = path + "GSMServicio/InfoServicio?Id=" + 0;
    window.location = strUrl;

}

function fn_CargaTipoServicio() {
    var lst = '';
    $.get(path + "GSMmaestro/ListaTipoServicio", function (data) {
        console.log(data);
        var lstEnt = data;
        var strItems = '';
        $('#cboTipo').find('option').remove();
        var strItems = '<option value="0">Seleccione</option>';
        if (lstEnt.length) {
            for (var i = 0; i < lstEnt.length; i++) {
                //{ID: 8, N: "ESTILOS (LIMA)"}
                strItems += '<option value=' + lstEnt[i].value + '>' + lstEnt[i].text + '</option>';
            }
        }
        $('#cboTipo').append(strItems);
    });
}

function fn_CargaServicio() {
    var lst = '';
    $.get(path + "GSMmaestro/ListaServicio", function (data) {
        console.log(data);
        var lstEnt = data;
        var strItems = '';
        $('#cboServicio').find('option').remove();
        var strItems = '<option value="0">Seleccione</option>';
        if (lstEnt.length) {
            for (var i = 0; i < lstEnt.length; i++) {
                //{ID: 8, N: "ESTILOS (LIMA)"}
                strItems += '<option value=' + lstEnt[i].value + '>' + lstEnt[i].text + '</option>';
            }
        }
        $('#cboServicio').append(strItems);
    });
}