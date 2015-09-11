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
%>
    <section class="content-header" style="padding-bottom:5px">
        <h1>Actualizar Cronograma de Ejecución de Obra</h1>
        <div>&nbsp;</div>
        
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button id="btnIconCrear" type="button" class="btn btn-default" onclick="document.location.href='/CronogramaEjecucionObra/createactividad?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>&c=<%:Request.QueryString["c"]%>'">
                    <span class="fa fa-file" aria-hidden="true"></span> Nueva actividad
                </button>
                <button id="btnBuscar" type="button" class="btn btn-default" onclick="document.location.href='/CronogramaEjecucionObra/search'">
                    <span class="fa fa-pencil" aria-hidden="true"></span> Volver a buscar proyecto
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
            <div class="form-group">
            <label class="col-sm-3 control-label">Proyecto:</label>
            <div class="col-sm-9">
                <strong><%: Html.DisplayFor(m => m.IdProyecto) %> - <%: Html.DisplayFor(m => m.NomProyecto) %></strong>
                <%: Html.HiddenFor(m => m.IdProyecto, new { Value = Request.QueryString["p"] })%>
                <%: Html.HiddenFor(m => m.IdExpediente, new { Value = Request.QueryString["e"] })%>
            </div>
            </div>
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
                        <td></td>
                        <td>Actividad</td>
                        <td>Fecha Ini. Prog.</td>
                        <td>Fecha Fin Prog.</td>
                        <td>Fecha Ini. Ejec.</td>
                        <td>Fecha Fin Ejec.</td>
                        <td>Responsable</td>
                        <td>Cantidad RRHH</td>
                        <td>Costo</td>
                    </tr>
                    <%
                        int intSecuencia=1;
                        foreach (ObrasPublicas.Entities.ActividadCronogramaOP objActividadCronogramaOP in lstActividades)
                        {
                      %>
                        <tr>
                            <td><a href="/CronogramaEjecucionObra/EditActividad?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>&c=<%:Request.QueryString["c"]%>&a=<%:objActividadCronogramaOP.IdActividad%>">Modificar</a></td>
                            <td><%:objActividadCronogramaOP.Nombre %></td>
                            <td><%:objActividadCronogramaOP.FechaIniProg.ToString("dd/MM/yyyy") %></td>
                            <td><%:objActividadCronogramaOP.FechaFinProg.ToString("dd/MM/yyyy") %></td>
                            <td><%:objActividadCronogramaOP.FechaIniEjec.ToString("dd/MM/yyyy") %></td>
                            <td><%:objActividadCronogramaOP.FechaFinEjec.ToString("dd/MM/yyyy") %></td>
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
                </thead>
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