<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Intranet.Master" Inherits="System.Web.Mvc.ViewPage<ObrasPublicas.Models.InformeObra.CreateInformeObraModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Municipalidad de San Isidro - Informe de Obra
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% Html.EnableClientValidation(); %>
<style>
    .field-validation-error
    {
        color: #ff0000;
    }
    .validation-summary-errors
    {
    }
</style>
    <section class="content-header" style="padding-bottom:5px">
        <h1>Actualizar Informe de Obra</h1>
        <div>&nbsp;</div>
        
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button id="btnBuscar" type="button" class="btn btn-default" onclick="document.location.href='/informeobra/index'">
                    <span class="fa fa-arrow-left" aria-hidden="true"></span> Volver a buscar proyecto
                </button>
            </div>
        </div>
    </section>
    <section class="content">
    <div id="divPanelCrear" class="panel panel-primary">
            <div class="panel-heading">
              <h3 class="panel-title">Crear Informe de Obra</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <% using (Html.BeginForm("Create", "InformeObra", FormMethod.Post, new { @name = "frmCreate", @id = "frmCreate" }))
                       { %>
                        <%: Html.AntiForgeryToken() %>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Proyecto:</label>
                    <div class="col-sm-9">
                        <strong><%: Html.DisplayFor(m => m.IdProyecto) %> - <%: Html.DisplayFor(m => m.NomProyecto) %></strong>
                        <%: Html.HiddenFor(m => m.IdProyecto, new { Value = Request.QueryString["p"] })%>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label"></label>
                    <div class="col-sm-9">
                        <strong>Ubicaci&oacute;n: <%: Html.DisplayFor(m => m.UbicacionProyecto) %></strong>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label"></label>
                    <div class="col-sm-9">
                        <strong>Valor referencial: <%: Html.DisplayFor(m => m.ValorRefProyecto) %></strong>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label"></label>
                    <div class="col-sm-9">
                        <strong>Ejecutor: <%: Html.DisplayFor(m => m.NomEjecutor) %></strong>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label"></label>
                    <div class="col-sm-9">
                        <strong>Supervisor: <%: Html.DisplayFor(m => m.NomSupervisor) %></strong>
                    </div>
                    </div>
                    <div>&nbsp;</div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Entregas de Material:</label>
                        <div class="col-sm-9">
                            <%
                               List<ObrasPublicas.Entities.EntregaMaterialOP> lstEntregas = (List<ObrasPublicas.Entities.EntregaMaterialOP>)ViewBag.lstEntregas;
                                 %>
                            <%
                               if (lstEntregas != null && lstEntregas.Count > 0) { 
                               %>
                                <table class="table table-condensed">
                                    <thead>
                                        <tr>
                                            <th>C&oacute;digo</th>
                                            <th>Material</th>
                                            <th>Proveedor</th>
                                            <th>Entrega Prog.</th>
                                            <th>Entrega efectiva</th>
                                            <th>Cantidad</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    <%foreach (var objEntrega in lstEntregas) { 
                                      %>
                                        <tr>
                                            <td><%:objEntrega.IdEntrega %></td>
                                            <td><%:objEntrega.Material.Nombre %></td>
                                            <td><%:objEntrega.Proveedor.RazonSocial %></td>
                                            <td><%:objEntrega.FecEntregaProg.ToString("dd/MM/yyyy") %></td>
                                            <td><%:objEntrega.FecEntregaEfec.ToString("dd/MM/yyyy") %></td>
                                            <td><%:objEntrega.Cantidad %></td>
                                        </tr>
                                    <%
                                      } %>
                                    </tbody>
                                </table>
                            <% 
                               }
                               else { 
                                %>
                                No existen entregas nuevas registradas para el proyecto
                            <%
                               }
                            %>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Actividades:</label>
                        <div class="col-sm-9">
                            <%
                           List<ObrasPublicas.Entities.ActividadCronogramaOP> lstActividades = (List<ObrasPublicas.Entities.ActividadCronogramaOP>)ViewBag.lstActividades;
                                 %>
                            <%
                           if (lstActividades != null && lstActividades.Count > 0)
                           { 
                               %>
                                <table class="table table-condensed">
                                    <thead>
                                        <tr>
                                            <th>Actividad</th>
                                            <th>Fec. Ini. Efec.</th>
                                            <th>Fec. Fin. Efec.</th>
                                            <th>Cantidad</th>
                                            <th>Costo</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    <%foreach (var objActividad in lstActividades)
                                      { 
                                      %>
                                        <tr>
                                            <td><%:objActividad.Nombre %></td>
                                            <td><%:objActividad.FechaIniEjec.ToString("dd/MM/yyyy") %></td>
                                            <td><%:objActividad.FechaFinEjec.ToString("dd/MM/yyyy") %></td>
                                            <td><%:objActividad.CantidadRRHH %></td>
                                            <td><%:objActividad.Costo %></td>
                                        </tr>
                                    <%
                                      } %>
                                        </tbody>
                                </table>
                            <% 
                               }
                               else { 
                                %>
                                No existen actividades nuevas registradas para el proyecto
                            <%
                               }
                            %>
                        </div>
                    </div>

                    <div class="form-group">
                    <label class="col-sm-3 control-label">* T&iacute;tulo:</label>
                    <div class="col-sm-9">
                        <%: Html.TextBoxFor(m => m.Titulo, new { @class = "form-control", maxlength = "500" })%>
                        <%: Html.ValidationMessageFor(m => m.Titulo) %>
                        <div id="Err_Titulo" class="field-validation-error"></div>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Tipo de Informe:</label>
                    <div class="col-sm-9">
                        <%: Html.TextBoxFor(m => m.TipoInforme, new { @class = "form-control", maxlength = "500" })%>
                        <%: Html.ValidationMessageFor(m => m.TipoInforme) %>
                        <div id="Err_TipoInforme" class="field-validation-error"></div>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Conclusi&oacute;n:</label>
                    <div class="col-sm-9">
                        <%: Html.TextAreaFor(m => m.Conclusion, new { @class = "form-control", maxlength = "500" })%>
                        <%: Html.ValidationMessageFor(m => m.Conclusion) %>
                        <div id="Err_Conclusion" class="field-validation-error"></div>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Recomendaci&oacute;n:</label>
                    <div class="col-sm-9">
                        <%: Html.TextAreaFor(m => m.Recomendacion, new { @class = "form-control", maxlength = "500" })%>
                        <%: Html.ValidationMessageFor(m => m.Recomendacion) %>
                        <div id="Err_Recomendacion" class="field-validation-error"></div>
                    </div>
                    </div>

                    <div class="col-sm-12 text-center">
                        &nbsp;
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12 text-center">
                        <%: Html.ValidationMessage("Err_General2") %>
                        <div id="Err_General" class="alert alert-error" style="text-align:left; display:none">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                        </div>
                            <div id="divMensajeOK" class="alert alert-success" role="alert" style="display:none"></div>
                        </div>

                        <% if (ViewBag.NoGrabar != "1")
                           { 
                         %>
                            <div class="col-sm-12 text-center">
                                <button id="btnGrabar" class="btn btn-primary" type="button">Grabar</button>
                                <button id="btnLimpiar" class="btn btn-primary" type="button" onclick="document.location.href='/CronogramaEjecucionObra/index'">Cancelar</button>
                            </div>
                        <% 
                           } %>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jquery") %>
    <%: Scripts.Render("~/Scripts/bootstrap.min.js") %>
    <%: Scripts.Render("~/Scripts/jquery-ui-1.8.20.js") %>
    <%: Scripts.Render("~/Scripts/utils.js") %>
    <script>
        $("#btnGrabar").click(function () {
            //$("#frmCreate").submit();
            var infoForm = $("#frmCreate");
            $(".field-validation-error").html('');
            $("#divMensajeOK").hide();
            $("#Err_General").hide();

            waitingDialog.show('Procesando...');
            $.ajax({
                url: "/informeobra/Create",
                type: "POST",
                data: infoForm.serialize(),
                dataType: "json",
                success: function (data) {
                    waitingDialog.hide();
                    //Sys.Mvc.FormContext._Application_Load();
                    if (data.Valid) {
                        $("#Err_General").hide();
                        $("#divMensajeOK").html("Se realizó la operación satisfactoriamente.");
                        $("#divMensajeOK").show();
                        //$(':input').val('');
                        $(".field-validation-error").html('');
                    }
                    $.each(data.Errors, function (key, value) {
                        if (value != null) {
                            $("#Err_" + key).html(value[value.length - 1].ErrorMessage);
                            $(key).html(value[value.length - 1].ErrorMessage);
                            if (key == "General") {
                                $("#Err_General").show();
                            }
                        }
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    waitingDialog.hide();
                    alert(errorThrown);
                    waitingDialog.hide();
                }
            })
        });

        $("input").keyup(function () {
            var $errorDiv = $("#Err_" + this.id);
            if ($errorDiv.html() != "") {
                $errorDiv.html("");
            }
        });
        $("input").focus(function () {
            var $errorDiv = $("#Err_" + this.id);
            if ($errorDiv.html() != "") {
                $errorDiv.html("");
            }
        });
    </script>
    <script type="text/javascript">
        var waitingDialog = waitingDialog || (function ($) {
            'use strict';

            var $dialog = $(
                '<div class="modal fade" data-backdrop="static" data-keyboard="false" tabindex="-1" role="dialog" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
                '<div class="modal-dialog modal-m">' +
                '<div class="modal-content">' +
                    '<div class="modal-header"><h3 style="margin:0;"></h3></div>' +
                    '<div class="modal-body">' +
                        '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
                    '</div>' +
                '</div></div></div>');

            return {
                show: function (message, options) {
                    // Assigning defaults
                    if (typeof options === 'undefined') {
                        options = {};
                    }
                    if (typeof message === 'undefined') {
                        message = 'Loading';
                    }
                    var settings = $.extend({
                        dialogSize: 'm',
                        progressType: '',
                        onHide: null // This callback runs after the dialog was hidden
                    }, options);

                    // Configuring dialog
                    $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
                    $dialog.find('.progress-bar').attr('class', 'progress-bar');
                    if (settings.progressType) {
                        $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
                    }
                    $dialog.find('h3').text(message);
                    // Adding callbacks
                    if (typeof settings.onHide === 'function') {
                        $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                            settings.onHide.call($dialog);
                        });
                    }
                    // Opening dialog
                    $dialog.modal();
                },
                /**
                 * Closes dialog
                 */
                hide: function () {
                    $dialog.modal('hide');
                }
            };
        })(jQuery);

	</script>
    
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>