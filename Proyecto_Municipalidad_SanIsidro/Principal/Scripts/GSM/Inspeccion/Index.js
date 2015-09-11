 

$(document).ready(function () {
    fn_CargaTipoServicio();
    $("#txtFecProg").kendoDatePicker({ format: "dd/MM/yyyy" });
    $("#txtFecProgf").kendoDatePicker({ format: "dd/MM/yyyy" });    
});


function fn_Click(vitem) {
    var id = $(vitem).parent().parent().children().html();
    id = parseInt(id);

    if (id > 0) {
        var strUrl = path + "GSMInspeccion/Inspeccion?Id=" + id;
        window.location = strUrl;
    }
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
            { field: "servicio", title: "Servicio", width: 100, filterable: true },
            { field: "tipoServicio", title: "Tipo servicio", width: 100, filterable: false },
            { field: "fechaProgramada", title: "Fecha Programada", width: 100, filterable: false },
            { field: "personaAsignada", title: "Persona Asignada", width: 100, filterable: false }, 
            { command: [{ name: "Registrar", template: "<div class='k-grid-historico k-button' onclick='fn_Click(this);' ><span class='k-icon k-i-maximize'></span>&nbsp;&nbsp;Ver Detalle</div>" }], title: " ", width: 150 },
        ]
    }; 
   
    /***********************************************************************************************/
    var grid = $("#grid").kendoGrid(_params);
    /***********************************************************************************************/
    //  $("#grid").data("kendoGrid").dataSource.read();

}

function fn_CrearNuevo() {

    //var idIni = $('#hidIni').val();
    var strUrl = path + "GSMInspeccion/Inspeccion?Id=" + 0;
    window.location = strUrl;

}


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





