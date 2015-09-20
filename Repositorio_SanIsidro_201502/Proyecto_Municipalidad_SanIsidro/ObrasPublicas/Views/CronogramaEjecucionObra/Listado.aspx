<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Intranet.Master" Inherits="System.Web.Mvc.ViewPage<ObrasPublicas.Models.CronogramaEjecucionObra.ListadoCronogramaEjecucionObraModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Municipalidad de San Isidro - Entrega de Materiales
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .field-validation-error
    {
        color: #ff0000;
    }
    .validation-summary-errors
    {
    }
</style>
    <%
        List<ObrasPublicas.Entities.ActividadCronogramaOP> lstActividades = ViewBag.ListadoActividades;
        String strTipo = Request.QueryString["v"];
%>
    <section class="content-header" style="padding-bottom:5px">
        <h1>Actualizar Cronograma de Ejecución de Obra</h1>
        <div>&nbsp;</div>
        
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                
                <%if (strTipo != "1") { 
                %>
                    <button id="btnIconCrear" type="button" class="btn btn-default" onclick="document.location.href='/CronogramaEjecucionObra/createactividad?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>&c=<%:Request.QueryString["c"]%>'">
                        <span class="fa fa-file" aria-hidden="true"></span> Nueva actividad
                    </button>
                <% }%>
                <button id="btnBuscar" type="button" class="btn btn-default" onclick="document.location.href='/CronogramaEjecucionObra/search/<%:strTipo %>'">
                    <span class="fa fa-arrow-left" aria-hidden="true"></span> Volver a buscar proyecto
                </button>
            </div>
        </div>
    </section>
    <section class="content">
    <div id="divPanelCrear" class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Cronograma de Ejecución de Obra</h3>
        </div>
        <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Proyecto:</label>
                    <div class="col-sm-9">
                        <strong><%: Html.DisplayFor(m => m.IdProyecto) %> - <%: Html.DisplayFor(m => m.NomProyecto) %></strong>
                        <%: Html.HiddenFor(m => m.IdProyecto, new { Value = Request.QueryString["p"] })%>
                        <%: Html.HiddenFor(m => m.IdExpediente, new { Value = Request.QueryString["e"] })%>
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
                        <strong>Valor referencial: S/. <%: Html.DisplayFor(m => m.ValorRefExpediente) %></strong>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label"></label>
                    <div class="col-sm-9">
                        <strong>Costo del proyecto: S/. <%: Html.DisplayFor(m => m.CostoProyecto) %></strong>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label"></label>
                    <div class="col-sm-9">
                        <strong>Plazo ejecuci&oacute;n: <%: Html.DisplayFor(m => m.PlazoEjecucion) %> d&iacute;as</strong>
                    </div>
                    </div>
                </div>
            <%if (ViewBag.OKEliminar == "1")
              {%>
                <div id="divMensajeOK"  class="alert alert-success" role="alert">
                    <%:ViewBag.MsgEliminar %>
                </div>
                <%}
              else if (ViewBag.OKEliminar == "0") { 
              %>
                <div class="alert alert-error" style="text-align:left">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <%:ViewBag.MsgEliminar %>
                </div>
                  <%
              }%>

            <div class="form-group">
                &nbsp;
            </div>

            <%if (lstActividades == null || lstActividades.Count() == 0)
              { 
            %>
                <div>
                    No se encontraron entregas registradas
                </div>
            <%
              }
              else{
              %>
            <table class="table table-condensed">
                <thead>
                    <tr>
                        <%if (strTipo != "1") { 
                          %>
                        <th></th>
                        <th></th>
                        <%
                          } %>
                        <th>Actividad</th>
                        <th>Fecha Ini. Prog.</th>
                        <th>Fecha Fin Prog.</th>
                        <th>Fecha Ini. Ejec.</th>
                        <th>Fecha Fin Ejec.</th>
                        <th>Responsable</th>
                        <th>Cantidad RRHH</th>
                        <th>Costo</th>
                    </tr>
                </thead>
                <tbody>
                    <%
                        int intSecuencia=1;
                        foreach (ObrasPublicas.Entities.ActividadCronogramaOP objActividadCronogramaOP in lstActividades)
                        {
                      %>
                        <tr>
                            <%if (strTipo != "1") { 
                            %>
                            <td><a href="/CronogramaEjecucionObra/EditActividad?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>&c=<%:Request.QueryString["c"]%>&a=<%:objActividadCronogramaOP.IdActividad%>">Modificar</a></td>
                            <td><a href="/CronogramaEjecucionObra/DeleteActividad?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>&c=<%:Request.QueryString["c"]%>&a=<%:objActividadCronogramaOP.IdActividad%>">Eliminar</a></td>
                            <%} %>
                            <td><%:objActividadCronogramaOP.Nombre %></td>
                            <td><%:objActividadCronogramaOP.FechaIniProg.ToString("dd/MM/yyyy") %></td>
                            <td><%:objActividadCronogramaOP.FechaFinProg.ToString("dd/MM/yyyy") %></td>
                            <td>
                                <%if (objActividadCronogramaOP.FechaIniEjec.HasValue) { 
                                  %>
                                <%:objActividadCronogramaOP.FechaIniEjec.Value.ToString("dd/MM/yyyy") %>
                                <%
                                  }
                                  else{
                                  %>
                                    -
                                <%
                                  } %>
                            </td>
                            <td>
                                <%if (objActividadCronogramaOP.FechaFinEjec.HasValue)
                                  { 
                                  %>
                                <%:objActividadCronogramaOP.FechaFinEjec.Value.ToString("dd/MM/yyyy") %>
                                <%
                                  }
                                  else{
                                  %>
                                    -
                                <%
                                  } %>
                            </td>
                            <%
                            if (objActividadCronogramaOP.IdTipoResponsable == "P")
                            {
                                %>
                            <td><%:objActividadCronogramaOP.ResponsableNom %> <%:objActividadCronogramaOP.ResponsableApe %></td>
                            <%
                            }
                            else { 
                                %>
                            <td><%:objActividadCronogramaOP.ResponsableRazSoc %></td>
                            <%
                            }
                            %>
                            <td><%:objActividadCronogramaOP.CantidadRRHH %></td>
                            <td><%:objActividadCronogramaOP.Costo %></td>
                        </tr>
                    <%
                            intSecuencia++;
                      } %>
                </tbody>
            </table>
            <%
              } %>
        </div>
    </div>
    </section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jquery") %>
    <%: Scripts.Render("~/Scripts/bootstrap.min.js") %>
    <%: Scripts.Render("~/Scripts/jquery-ui-1.8.20.js") %>
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