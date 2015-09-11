<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Catastro.Master" Inherits="System.Web.Mvc.ViewPage<Entidades.CT_SOLICITUD>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Mantener Informacion Catastral
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <h1>Mantener Informacion Catastral
            
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Mantener Informacion catastral</li>

        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class='col-md-12'>
                <div class='box box-primary'>
                    <div class='box-header'>
                        <h3 class="box-title">Informacion Solicitud</h3>
                    </div>
                    <div class="box-body">
                        <div class="bootstrap-timepicker">
                            <div class="form-group">
                                <label>Nro Solicitud:</label>

                                <div class="input-group">
                                    <input type="text" class="form-control" disabled id="txtnroDolicitud">
                                    <span class="input-group-btn">
                                        <button class="btn btn-info btn-flat" type="button" data-toggle="modal" id="btnModalSoicitud"><i class="fa  fa-search"></i></button>
                                    </span>
                                </div>
                                <!-- /input-group -->
                            </div>
                            <div class="form-group">
                                <label for="exampleInputEmail1">Tipo de Solicitud</label>
                                <input type="text" class="form-control" id="txttiposolicitud" disabled>
                            </div>

                            <div class="form-group">
                                <label for="exampleInputEmail1">Nro Propuesta</label>
                                <input type="email" class="form-control" id="txtnroPropuesta" disabled>
                            </div>

                        </div>

                        <br />

                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="content">
        <div class="row">
            <div class='col-md-12'>
                <!-- Form Element sizes -->
                <div class="box box-success">
                    <div class="box-header">
                        <h3 class="box-title">Mantenimiento</h3>
                    </div>
                    <div class="box-body">
                        <!-- Custom Tabs (Pulled to the right) -->
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs pull-right" id="myTab">
                                <li class="active"><a href="#tab_1-1" data-toggle="tab">Zona</a></li>
                                <li><a href="#tab_2-2" data-toggle="tab">Manzana</a></li>
                                <li><a href="#tab_3-2" data-toggle="tab">Lote</a></li>
                                <li><a href="#tab_4-2" data-toggle="tab">Predio</a></li>
                                <li class="pull-left header"><i class="fa fa-th"></i>

                                    <label id="lblTituloTab">Zonas</label>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab_1-1">
                                    <table class="table table-hover" id="tblZonas">
                                        <tr>
                                            <th>Codigo
                                            </th>
                                            <th>Nombre
                                            </th>
                                            <th></th>
                                            <th></th>
                                        </tr>

                                    </table>


                                    <div class="box-footer">
                                        <button type="button" class="btn btn-success" id="btnModalZona">Crear Nueva Zona</button>
                                        <button type="button" class="btn btn-success" id="btnEliminarZona">Eliminar Zona</button>
                                        <button type="button" class="btn btn-success">Cancelar</button>

                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                                <div class="tab-pane" id="tab_2-2">
                                    <div class="tab-pane active" id="Div1">
                                        <div class="form-group">
                                            <label>
                                                Zona
                                            </label>
                                            <select id="ddlZona1" class="ddlZona">
                                                <option>Zona A </option>
                                                <option>Zona B </option>
                                                <option>Zona C </option>
                                                <option>Zona D </option>
                                                <option>Zona N </option>

                                            </select>
                                        </div>
                                        <table class="table table-hover" id="tblManzanas">
                                            <tr>
                                                <th>Codigo
                                                </th>
                                                <th>Nombre
                                                </th>
                                                <th></th>
                                                <th></th>
                                            </tr>

                                        </table>


                                        <div class="box-footer">
                                            <button type="button" class="btn btn-success" id="btnmanzana1">Crear Nueva Manzana</button>
                                            <button type="button" class="btn btn-success" id="Button4">Eliminar Manzana</button>
                                            <button type="button" class="btn btn-success">Cancelar</button>

                                        </div>
                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                                <div class="tab-pane" id="tab_3-2">
                                    <div class="tab-pane active" id="Div2">
                                        <div class="form-group">
                                            <label>
                                                Zona
                                            </label>
                                            <select id="Select3" class="ddlZona">
                                                <option>Zona A </option>
                                                <option>Zona B </option>
                                                <option>Zona C </option>
                                                <option>Zona D </option>
                                                <option>Zona N </option>

                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Manzana
                                            </label>
                                            <select id="Select5" class="ddlManzana">
                                                <option>Zona A </option>
                                                <option>Zona B </option>
                                                <option>Zona C </option>
                                                <option>Zona D </option>
                                                <option>Zona N </option>

                                            </select>
                                        </div>


                                        <table class="table table-hover" id="tblLotes">
                                            <tr>
                                                <th>Codigo
                                                </th>
                                                <th>Nombre
                                                </th>
                                                <th></th>
                                                <th></th>
                                            </tr>

                                        </table>


                                        <div class="box-footer">
                                            <button type="button" class="btn btn-success" id="Button1">Crear Nueva Lote</button>
                                            <button type="button" class="btn btn-success" id="Button9">Eliminar Lote</button>
                                            <button type="button" class="btn btn-success">Cancelar</button>

                                        </div>
                                    </div>
                                </div>
                                <div class="tab-pane" id="tab_4-2">
                                    <div class="form-group">
                                        <label>
                                            Zona
                                        </label>
                                        <select id="ddlZona" class="ddlZona">
                                            <option>Zona A </option>
                                            <option>Zona B </option>
                                            <option>Zona C </option>
                                            <option>Zona D </option>
                                            <option>Zona N </option>

                                        </select>
                                    </div>


                                    <div class="form-group">
                                        <label>
                                            Manzana
                                        </label>
                                        <select id="Select1" class="ddlManzana">
                                            <option>Manzana A </option>
                                            <option>Manzana B </option>
                                            <option>Manzana C </option>
                                            <option>Manzana D </option>
                                            <option>Manzana N </option>

                                        </select>
                                    </div>

                                    <div class="form-group">
                                        <label>
                                            Lote
                                        </label>
                                        <select id="Select2" class="ddlote">
                                            <option>Lote A </option>
                                            <option>Lote B </option>
                                            <option>Lote C </option>
                                            <option>Lote D </option>
                                            <option>Lote N </option>

                                        </select>
                                    </div>



                                    <br />

                                    Lista de Predios
                                    <table class="table table-hover" id="tblPredio">
                                        <tr>
                                            <th>Codigo
                                            </th>
                                            <th>Nombre
                                            </th>
                                            <th></th>
                                            <th></th>
                                        </tr>

                                    </table>
                                    <div class="box-footer">
                                        <button type="button" class="btn btn-success" id="btnaddPredio">Crear Nuevo Predio</button>
                                        <button type="button" class="btn btn-success" id="Button3">Editar Predio</button>
                                        <button type="button" class="btn btn-success" id="Button2">Eliminar Predio</button>
                                        <button type="button" class="btn btn-success">Cancelar</button>

                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                            </div>
                            <!-- /.tab-content -->
                        </div>
                        <!-- nav-tabs-custom -->
                        <!-- Button trigger modal -->
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
        </div>
    </section>
    <!-- /.box -->





    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Buscar Solicitud Catastral</h4>
                </div>
                <div class="modal-body">
                    <table class="table table-hover" id="tblSolicitudPendiente">
                        <tr>
                            <th>Nro Solicitud
                            </th>
                            <th>Fecha Emisión
                            </th>
                            <th>Tipo Solicitud
                            </th>
                            <th>Descripción
                            </th>
                            <th></th>


                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnaceptarSolicitud">Aceptar</button>
                </div>
            </div>
        </div>
    </div>


    <!-- Modal -->
    <div class="modal fade" id="ModalZona" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H1">Crear Zona
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-3">

                            <label for="exampleInputEmail1">Código Zona</label>
                            <input type="text" class="form-control" placeholder="Código Zona" id="txtCodigoZona">
                        </div>
                        <div class="col-xs-4">

                            <label for="exampleInputEmail1">Nombre Zona</label>
                            <input type="text" class="form-control" placeholder="Nombre Zona" id="txtNombreZona">
                        </div>
                        <div class="col-xs-4">

                            <label for="exampleInputEmail1">Area</label>
                            <input type="text" class="form-control" placeholder="Area" id="txtAreaZona">
                        </div>

                    </div>
                    <hr />
                    <h5 class="modal-title" id="H2">Detalle Zona
                    </h5>
                    <div class="row">
                        <div class="col-xs-2">
                            <input type="text" class="form-control" placeholder="Marca" id="txtMarcaZona">
                        </div>
                        <div class="col-xs-3">
                            <input type="text" class="form-control" placeholder="referencia" id="txtreferecniaZona">
                        </div>
                        <div class="col-xs-2">
                            <input type="text" class="form-control" placeholder="Latitud" id="txtLatitudZona">
                        </div>
                        <div class="col-xs-3">
                            <input type="text" class="form-control" placeholder="Longitud" id="txtlongitudZona">
                        </div>
                        <div class="col-xs-2">
                            <button type="button" class="btn btn-primary" id="btnAgregarDetalleZona">Agregar</button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <table class="table table-hover" id="tbldetalleZona">
                            <tr>
                                <th>ID
                                </th>
                                <th>Marca
                                </th>
                                <th>Referencia
                                </th>
                                <th>Latitud
                                </th>
                                <th>Longitud
                                </th>

                                <th></th>
                            </tr>
                        </table>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnAddZonaNew" onclick="return confirm('¿Desea registrar la zona?');">Grabar</button>
                </div>
            </div>
        </div>
    </div>



    <!-- Modal -->
    <div class="modal fade" id="modalManzana" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H5">Crear Manzana
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-3">

                            <label for="exampleInputEmail1">Código Manzana</label>
                            <input type="text" class="form-control" placeholder="Código Manzana" id="txtxCodigoManzana">
                        </div>
                        <div class="col-xs-4">

                            <label for="exampleInputEmail1">Nombre Manzana</label>
                            <input type="text" class="form-control" placeholder="Nombre Manzana" id="txtNombreManzana">
                        </div>
                        <div class="col-xs-4">

                            <label for="exampleInputEmail1">Area</label>
                            <input type="text" class="form-control" placeholder="Area" id="txtManzanaArea">
                        </div>
                    </div>
                    <hr />
                    <h5 class="modal-title" id="H6">Detalle Manzana
                    </h5>
                    <div class="row">
                        <div class="col-xs-2">
                            <input type="text" class="form-control" placeholder="Marca" id="txtMarcaManzana">
                        </div>
                        <div class="col-xs-3">
                            <input type="text" class="form-control" placeholder="referencia" id="txtreferecniaManzana">
                        </div>
                        <div class="col-xs-2">
                            <input type="text" class="form-control" placeholder="Latitud" id="txtLatitudManzana">
                        </div>
                        <div class="col-xs-3">
                            <input type="text" class="form-control" placeholder="Longitud" id="txtlongitudManzana">
                        </div>
                        <div class="col-xs-2">
                            <button type="button" class="btn btn-primary" id="btnAgregarDetalleManzana">Agregar</button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <table class="table table-hover" id="tbldetalleManzana">
                            <tr>
                                <th>ID
                                </th>
                                <th>Marca
                                </th>
                                <th>Referencia
                                </th>
                                <th>Latitud
                                </th>
                                <th>Longitud
                                </th>

                                <th></th>
                            </tr>
                        </table>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnAgregarManzanaNew" onclick="return confirm('¿Desea registrar la manzana?');">Grabar</button>
                </div>
            </div>
        </div>
    </div>




    <div class="modal fade" id="modalLote" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H7">Crear Lote
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-3">

                            <label for="exampleInputEmail1">Código Zona</label>
                            <input type="text" class="form-control" placeholder="Código Zona" id="txtxCodigoLote">
                        </div>
                        <div class="col-xs-4">

                            <label for="exampleInputEmail1">Nombre Zona</label>
                            <input type="text" class="form-control" placeholder="Nombre Zona" id="txtNombreLote">
                        </div>
                        <div class="col-xs-4">

                            <label for="exampleInputEmail1">Area</label>
                            <input type="text" class="form-control" placeholder="Area" id="txtLoteArea">
                        </div>
                    </div>
                    <hr />
                    <h5 class="modal-title" id="H8">Detalle Lote
                    </h5>
                    <div class="row">
                        <div class="col-xs-2">
                            <input type="text" class="form-control" placeholder="Marca" id="txtMarcaLote">
                        </div>
                        <div class="col-xs-3">
                            <input type="text" class="form-control" placeholder="referencia" id="txtreferecniaLote">
                        </div>
                        <div class="col-xs-2">
                            <input type="text" class="form-control" placeholder="Latitud" id="txtLatitudLote">
                        </div>
                        <div class="col-xs-3">
                            <input type="text" class="form-control" placeholder="Longitud" id="txtlongitudLote">
                        </div>
                        <div class="col-xs-2">
                            <button type="button" class="btn btn-primary" id="btnAgregarDetalleLote">Agregar</button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <table class="table table-hover" id="tbldetalleLote">
                            <tr>
                                <th>ID
                                </th>
                                <th>Marca
                                </th>
                                <th>Referencia
                                </th>
                                <th>Latitud
                                </th>
                                <th>Longitud
                                </th>

                                <th></th>
                            </tr>
                        </table>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnGrabarLote" onclick="return confirm('¿Desea registrar el lote?');">Grabar</button>
                </div>
            </div>
        </div>
    </div>





    <div class="modal fade" id="MOdalPredio" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="H3">Crear Predio
                    </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-3">

                            <label for="exampleInputEmail1">Código Zona</label>
                            <input type="text" class="form-control" placeholder="Código Zona" id="txtPreCodigoZona">
                        </div>
                        <div class="col-xs-4">

                            <label for="exampleInputEmail1">Código Manzana</label>
                            <input type="text" class="form-control" placeholder="Codigo Manzana" id="txtCodigoManzana">
                        </div>
                        <div class="col-xs-4">

                            <label for="exampleInputEmail1">Nombre Mz   </label>
                            <input type="text" class="form-control" placeholder="Nombre Mz" id="txtPreNombreMz">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-3">

                            <label for="exampleInputEmail1">Código Lote</label>
                            <input type="text" class="form-control" placeholder="Código Lote" id="txtPreCodigoLote">
                        </div>
                        <div class="col-xs-4">

                            <label for="exampleInputEmail1">Nombre Lote</label>
                            <input type="text" class="form-control" placeholder="Nombre Lote" id="txtPreNombreLote">
                        </div>
                        <div class="col-xs-4">

                            <label for="exampleInputEmail1">Dirección </label>
                            <input type="text" class="form-control" placeholder="Dirección" id="txtPreDirección">
                        </div>
                    </div>

                    <hr />
                    <h5 class="modal-title" id="H4">Detalle Del Predio
                    </h5>
                    <div class="row">
                        <div class="col-xs-4">
                            <label>Codigo Predio</label>
                            <input type="text" class="form-control" placeholder="Codigo Predio" id="txtPreCodigo">
                        </div>

                        <div class="col-xs-4">
                            <label>Nombre Predio</label>
                            <input type="text" class="form-control" placeholder="Nombre Predio" id="txtNombrePredio">
                        </div>

                        <div class="col-xs-4">
                            <label>Altura</label>
                            <input type="text" class="form-control" placeholder="Altura" id="txtPreAltura">
                        </div>
                        <div class="col-xs-4">
                            <label>Area Construida</label>
                            <input type="text" class="form-control" placeholder="Area Construida" id="txtPreAreaConstruida">
                        </div>
                        <div class="col-xs-12">
                            <label>Observaciones</label>
                            <textarea class="form-control" placeholder="Observaciones" id="txtPreObservaciones"></textarea>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-primary" id="btnGrabarPredio" onclick="return confirm('¿Desea registrar el predio?');">Grabar</button>
                </div>
            </div>
        </div>
    </div>



    <input type="hidden" id="int_IdSolicitud" />
    <input type="hidden" id="int_IdZona" />
    <input type="hidden" id="int_IdManzana" />
    <input type="hidden" id="int_IdLote" />




    <div class="modal modal-warning" id="ModalConfir">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="TituloModalConfir"></h4>
                </div>
                <div class="modal-body">
                    <p id="p1ModalConfir"></p>

                    <p id="p2ModalConfir"></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline pull-left" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-outline" id="btnModalConfir" onclick="EliminarGeneral($(this).html())"></button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->



    

    <div class="modal modal-danger" id="ModalError">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="TituloModalError"></h4>
                </div>
                <div class="modal-body">
                    <p id="p1ModalError"></p>

                    <p id="p2ModalError"></p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-outline " data-dismiss="modal">Aceptar</button>
                    
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/Scripts/jstemplate/InformacionCatastral.js") %>
</asp:Content>
