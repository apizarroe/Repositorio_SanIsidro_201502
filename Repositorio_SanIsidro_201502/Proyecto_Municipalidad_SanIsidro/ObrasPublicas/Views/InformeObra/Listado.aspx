<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Intranet.Master" Inherits="System.Web.Mvc.ViewPage<ObrasPublicas.Models.InformeObra.ListadoInformeObraModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Municipalidad de San Isidro - Informes de Obra
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
        List<ObrasPublicas.Entities.InformeObra> lstInformes = ViewBag.ListadoInformes;
%>
    <section class="content-header" style="padding-bottom:5px">
        <h1>Actualizar Informe de Obra</h1>
        <div>&nbsp;</div>
        
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button type="button" class="btn btn-default" onclick="document.location.href='/informeobra/listado?p=<%:Model.IdProyecto %>&e=<%:Model.IdExpediente %>'">
                    <span class="fa fa-list-alt" aria-hidden="true"></span> Volver a listar informes
                </button>
                <button id="btnBuscar" type="button" class="btn btn-default" onclick="document.location.href='/informeobra/search'">
                    <span class="fa fa-arrow-left" aria-hidden="true"></span> Volver a buscar proyecto
                </button>
                
            </div>
        </div>
    </section>
    <section class="content">
    <div id="divPanelCrear" class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Listado de Informes de Obra</h3>
        </div>
        <div class="panel-body">
                <div class="form-horizontal" role="form">
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
                        <strong>Valor referencial: S/. <%: Html.DisplayFor(m => m.ValorRefExpediente) %></strong>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label"></label>
                    <div class="col-sm-9">
                        <strong>Costo del proyecto: S/. <%: Html.DisplayFor(m => m.ValorRefProyecto) %></strong>
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
                </div>
            <%if (ViewBag.OKAnular == "1")
              {%>
                <div id="divMensajeOK"  class="alert alert-success" role="alert">
                    <%:ViewBag.MsgAnular %>
                </div>
                <%}
              else if (ViewBag.OKAnular == "0") { 
              %>
                <div class="alert alert-error" style="text-align:left">
                    <button type="button" class="close" data-dismiss="alert">×</button>
                    <%:ViewBag.MsgAnular %>
                </div>
                  <%
              }%>

            <div class="form-group">
                &nbsp;
            </div>

            <%if (lstInformes == null || lstInformes.Count() == 0)
              { 
            %>
                <div>
                    No se encontraron informes registrados
                </div>
            <%
              }
              else{
              %>
            <table class="table table-condensed">
                <thead>
                    <tr>
                        <th></th>
                        <th></th>
                        <th>C&oacute;digo Informe</th>
                        <th>T&iacute;tulo</th>
                        <th>Fecha emisi&oacute;n</th>
                        <th>Tipo Informe</th>
                        <th>Estado</th>
                    </tr>
                </thead>
                <tbody>
                    <%
                        foreach (ObrasPublicas.Entities.InformeObra objInformeObra in lstInformes)
                        {
                      %>
                        <tr>
                            <td>
                            <% if (objInformeObra.IdEstado == ObrasPublicas.Entities.InformeObra.INT_ID_ESTADO_GENERADO) { 
                                   %>
                                <a href="/informeobra/edit?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>&i=<%:objInformeObra.IdInforme%>">Modificar</a>
                            <%
                               }else{
                            %>
                                <a href="/informeobra/edit?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>&i=<%:objInformeObra.IdInforme%>&consultar=1">Consultar</a>
                                <%                               
                               } %>
                            </td>
                            <td>
                            <% if (objInformeObra.IdEstado == ObrasPublicas.Entities.InformeObra.INT_ID_ESTADO_GENERADO)
                               { 
                                   %>
                                <a href="/informeobra/anular?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>&i=<%:objInformeObra.IdInforme%>">Anular</a>
                            <%
                               } %>
                            </td>
                            <td><%:objInformeObra.IdInforme %></td>
                            <td><%:objInformeObra.Titulo %></td>
                            <td><%:objInformeObra.FechaEmision.ToString("dd/MM/yyyy") %></td>
                            <td><%:objInformeObra.TipoInforme %></td>
                            <td><%:objInformeObra.NomEstado %></td>
                        </tr>
                    <%
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