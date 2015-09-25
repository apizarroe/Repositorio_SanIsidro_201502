<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Catastro.master" Inherits="System.Web.Mvc.ViewPage<Dominio.Core.Entities.ModeloGestionCatastral.CT_PROPUESTAINSPECCION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <h1>Asignar Técnico
            
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active"><a href="#"><i class="fa fa-dashboard"></i>Asignar Técnico</a></li>
            

        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class='col-md-12'>
                <div class='box box-primary'>
                    <div class='box-header'>
                        <h3 class="box-title">Información Solicitud</h3>
                    </div>
                    <div class="box-body">
                        <div class="bootstrap-timepicker">
                            <div class="form-group">
                                <label>Nro. Solicitud:</label>

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
                                <label for="exampleInputEmail1">Nro. Propuesta</label>
                                <input type="email" class="form-control" id="txtnroPropuesta" disabled>
                            </div>

                        </div>

                        <br />

                    </div>
                </div>
            </div>
        </div>
    </section>

    <input type="hidden" id="int_IdSolicitud" />
    <input type="hidden" id="int_IdZona" />
    <input type="hidden" id="int_IdManzana" />
    <input type="hidden" id="int_IdLote" />


    <section class="content" id="sectionAsignartecnico" style="display:none">
        <div class="row">
            <div class='col-md-12'>
                <!-- Form Element sizes -->
                <div class="box box-success">
                    <div class="box-header">
                        <h3 class="box-title">Asignar Técnico - Zona</h3>
                    </div>
                    <div class="box-body">




                        <div class="form-group">
                            <label>Tiempo de Inspección:</label>
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" id="reservation">
                            </div>
                            <!-- /.input group -->
                        </div>
                        <input type="hidden" id="hffechainicio" />
                        <input type="hidden" id="hffechafin" />
                        <label>
                            Técnico
                        </label>
                        <div class="editor-field">
                            
                            <select id="Select1" class="form-control select2">
                             </select>
                        </div>
                        


                        <!-- /.form group -->
                        <p>

                            <input type="button" value="Agregar Técnico" class="btn btn-success" id="btnAsignartecnico" />
                            
                        </p>
                        <br />
                        <table class="table table-hover" id="tblDetalleAsignar">
                            <tr>
                                <th>
                                </th>
                                <th>Técnico
                                </th>
                                


                                <th></th>
                            </tr>
                        </table>
                        <br />
                            <input type="button" value="Asignar Técnicos" class="btn btn-success" id="btnasignarTecnicosconfir" style="display:none" />
                            

                    </div>
                    <!-- /.box-body  -->
                </div>
            </div>
        </div>
    </section>










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
                            <th>Nro. Solicitud
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


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.10.2/moment.min.js"></script>
    <%: Scripts.Render("~/Scripts/jstemplate/InformacionCatastral.js") %>
    <%: Styles.Render("~/Scripts/jstemplate/select2/select2.min.css") %>
    <%: Scripts.Render("~/Scripts/jstemplate/select2/select2.full.min.js") %>
    <%: Styles.Render("~/Scripts/jstemplate/daterangepicker/daterangepicker-bs3.css") %>
    <%: Scripts.Render("~/Scripts/jstemplate/daterangepicker/daterangepicker.js") %>



    <script type="text/javascript">
        $(document).ready(function () {
            
            $('#reservation').daterangepicker({
                "autoApply": true,
            }, function (start, end, label) {
                    $('#hffechainicio').val(start.format('DD/MM/YYYY'));
                    $('#hffechafin').val(end.format('DD/MM/YYYY'));
                    ObtenerTecnicos();
            });
            
            $('#btnEmitirPropuesta').click(function () {
                var menssaje = "";
                if ($('#int_CantResponsable').val().length == 0) {
                    menssaje += "Debe Ingresar la Cantidad \n"
                }
                if ($('#var_Descripcion').val().length == 0) {
                    menssaje += "Debe Ingresar Descripcion \n"
                }
                if ($('#int_IdSolicitud').val().length == 0) {
                    menssaje += "Debe Seleccionar Una Solicitud \n"
                }
                if (menssaje.length > 0) {
                    alert(menssaje);
                    return
                }
                else {
                    $('#formularioinspeccion').submit();
                }
            });
        });

    </script>
</asp:Content>
