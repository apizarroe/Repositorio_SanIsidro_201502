<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Intranet.Master" Inherits="System.Web.Mvc.ViewPage<ObrasPublicas.Models.EntregaMaterialOP.ListadoEntregaMaterialModel>" %>

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
        List<ObrasPublicas.Entities.EntregaMaterialOP> lstEntregas = ViewBag.ListadoEntregas;
%>
    <section class="content-header" style="padding-bottom:5px">
        <h1>Actualizar Calendario de Entrega de Materiales</h1>
        <div>&nbsp;</div>
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button id="btnIconCrear" type="button" class="btn btn-default" onclick="document.location.href='/entregamaterialop/create?p=<%:Request.QueryString["p"]%>'">
                    <span class="fa fa-file" aria-hidden="true"></span> Nueva Entrega
                </button>
                <button id="btnBuscar" type="button" class="btn btn-default" onclick="document.location.href='/entregamaterialop/index'">
                    <span class="fa fa-arrow-left" aria-hidden="true"></span> Volver a buscar proyecto
                </button>
            </div>
        </div>
    </section>
    <section class="content">
    <div id="divPanelCrear" class="panel panel-primary">
        <div class="panel-heading">
            <h3 class="panel-title">Calendario de Entrega de Material</h3>
        </div>
        <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Proyecto:</label>
                    <div class="col-sm-9">
                        <strong><%: Html.DisplayFor(m => m.IdProyecto) %> - <%: Html.DisplayFor(m => m.NomProyecto) %></strong>
                        <%: Html.HiddenFor(m => m.IdProyecto, new { Value = Model.IdProyecto })%>
                        <%: Html.HiddenFor(m => m.NomProyecto, new { Value = Model.NomProyecto })%>
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
                </div>
            <div>&nbsp;</div>
            <%if (lstEntregas == null || lstEntregas.Count() == 0)
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
                        <td>Item</td>
                        <td>Material</td>
                        <td>Cantidad</td>
                        <td>Fec. entrega prog.</td>
                        <td>Fec. efectiva</td>
                        <td>Proveedor</td>
                        <td>Tipo Entrega</td>
                    </tr>
                </thead>
                    <%
                        int intIdEntrega=1;
                        foreach (ObrasPublicas.Entities.EntregaMaterialOP objEntregaMaterialOP in lstEntregas) { 
                      %>
                        <tr>
                            <td><a href="/entregamaterialop/Edit?p=<%:objEntregaMaterialOP.Proyecto.IdProyecto %>&ent=<%:objEntregaMaterialOP.IdEntrega %>">Modificar</a></td>
                            <td><%:intIdEntrega %></td>
                            <td><%:objEntregaMaterialOP.Material.Nombre %></td>
                            <td><%:objEntregaMaterialOP.Cantidad %></td>
                            <td><%:objEntregaMaterialOP.FecEntregaProg.ToString("dd/MM/yyyy") %></td>
                            <td><%:objEntregaMaterialOP.FecEntregaEfec.ToString("dd/MM/yyyy") %></td>
                            <td><%:objEntregaMaterialOP.Proveedor.RazonSocial %></td>
                            <td><%:objEntregaMaterialOP.TipoEntrega %></td>
                        </tr>
                    <%
                            intIdEntrega++;
                      } %>
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