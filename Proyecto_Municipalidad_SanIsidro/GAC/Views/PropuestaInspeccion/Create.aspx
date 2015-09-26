<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Catastro.master" Inherits="System.Web.Mvc.ViewPage<Dominio.Core.Entities.ModeloGestionCatastral.CT_PROPUESTAINSPECCION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <h1>Emitir Propuesta de Inspección
            
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>

            <li class="active"><a href="#"><i class="fa fa-dashboard"></i>Lista de Propuesta de Inspección</a></li>
            <li class="active">Lista de Solicitud Catastral</li>

            <li class="active"><a href="#"><i class="fa fa-dashboard"></i>Emitir Propuesta de Inspección</a></li>



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



                                    <%: Html.TextBoxFor(model => Model.CT_SOLICITUD.var_NroSolicitud, new { @class = "form-control", @disabled = "true", @id="txtnroDolicitud" }) %>

                                    <%--<input type="text" class="form-control" disabled id="txtnroDolicitud">--%>







                                    <span class="input-group-btn">
                                        <button class="btn btn-info btn-flat" type="button" data-toggle="modal" id="btnModalSoicitud"><i class="fa  fa-search"></i></button>
                                    </span>
                                </div>
                                <!-- /input-group -->
                            </div>
                            <div class="form-group">
                                <label for="exampleInputEmail1">Tipo de Solicitud</label>
                                <%--<input type="text" class="form-control" id="txttiposolicitud" disabled>--%>
                                <%: Html.TextBoxFor(model => Model.CT_SOLICITUD.CT_TIPOSOLICITUD.var_TipoSolicitud, new {@class="form-control",@disabled="true",@id="txttiposolicitud" })%>
                            </div>

                            <div class="form-group">
                                <label for="exampleInputEmail1">Nro. Propuesta</label>
                                <%--<input type="email" class="form-control" id="txtnroPropuesta" disabled>--%>
                                <%: Html.TextBoxFor(model => Model.int_IdPropuestaInspeccion, new {@class="form-control",@disabled="true",@id="txtnroPropuesta" })%>
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

                        <% using (Html.BeginForm(null, null, FormMethod.Post, new { id = "formularioinspeccion" }))
                           { %>
                        <%: Html.ValidationSummary(true) %>




                        <div class="editor-label" style="display: none">
                            <%: Html.LabelFor(model => model.int_IdPropuestaInspeccion) %>
                        </div>
                        <div class="editor-field" style="display: none">
                            <%: Html.EditorFor(model => model.int_IdPropuestaInspeccion) %>
                            <%: Html.ValidationMessageFor(model => model.int_IdPropuestaInspeccion) %>
                        </div>

                        <div class="editor-label" style="display: none">
                            <%: Html.LabelFor(model => model.int_IdSolicitud, "CT_SOLICITUD") %>
                        </div>
                        <div class="editor-field" style="display: none">
                            <%: Html.DropDownList("int_IdSolicitud", String.Empty) %>
                            <%: Html.ValidationMessageFor(model => model.int_IdSolicitud) %>
                        </div>

                        <div class="editor-label" style="display: none">
                            <%: Html.LabelFor(model => model.var_NroPropuesta) %>
                        </div>
                        <div class="editor-field" style="display: none">
                            <%: Html.EditorFor(model => model.var_NroPropuesta) %>
                            <%: Html.ValidationMessageFor(model => model.var_NroPropuesta) %>
                        </div>
                        <div class="form-group">
                            <label>Cantidad Responsable(*)</label>
                            <%: Html.EditorFor(model => model.int_CantResponsable, new { @class = "form-control" ,@placeholder="Descripción de Inspección"  }) %>
                            <%: Html.ValidationMessageFor(model => model.int_CantResponsable) %>
                        </div>
                        <div class="form-group">
                            <label>Descripción(*)</label>

                            <%: Html.TextAreaFor(model => model.var_Descripcion,htmlAttributes: new { @class = "form-control" ,@placeholder="Descripción de Inspección"  })%>
                            <%: Html.ValidationMessageFor(model => model.var_Descripcion) %>
                        </div>

                        <div class="editor-label" style="display: none">
                            <%: Html.LabelFor(model => model.dtm_FechaDocumento) %>
                        </div>
                        <div class="editor-field" style="display: none">
                            <%: Html.EditorFor(model => model.dtm_FechaDocumento) %>
                            <%: Html.ValidationMessageFor(model => model.dtm_FechaDocumento) %>
                        </div>

                        <div class="editor-label" style="display: none">
                            <%: Html.LabelFor(model => model.dtm_FechaRegistro) %>
                        </div>
                        <div class="editor-field" style="display: none">
                            <%: Html.EditorFor(model => model.dtm_FechaRegistro) %>
                            <%: Html.ValidationMessageFor(model => model.dtm_FechaRegistro) %>
                        </div>

                        <p>

                            <input type="button" value="<%:ViewBag.TextBotton%>" class="btn btn-success" id="btnEmitirPropuesta" />
                            <%: Html.ActionLink("Cancelar", "Index","PropuestaInspeccion", new {@class = "btn  btn-default" }) %>
                        </p>

                        <% } %>
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
    <%: Scripts.Render("~/Scripts/jstemplate/InformacionCatastral.js") %>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnEmitirPropuesta').click(function () {
                var menssaje = "";
                if ($('#int_CantResponsable').val().length == 0) {
                    menssaje += "* Debe Ingresar la Cantidad </br>"
                }
                if ($('#var_Descripcion').val().length == 0) {
                    menssaje += "* Debe Ingresar Descripción </br>"
                }
                if ($('#int_IdSolicitud').val().length == 0) {
                    menssaje += "* Debe Seleccionar Una Solicitud </br>"
                }
                if (menssaje.length > 0) {
                    //alert(menssaje);
                    $("#TituloModalConfir").html("Validación de datos");
                    $("#p2ModalConfir").html(menssaje);
                    $('#ModalValidacion').modal('show');
                    return
                }
                else {
                    $('#formularioinspeccion').submit();
                }
            });
        });

    </script>
</asp:Content>
