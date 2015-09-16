<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Intranet.Master" Inherits="System.Web.Mvc.ViewPage<ObrasPublicas.Models.EntregaMaterialOP.UpdateEntregaMaterialOP>" %>

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
        Infraestructura.Data.SQL.Proveedor_DAL objProveedor_DAL = new Infraestructura.Data.SQL.Proveedor_DAL();
        var lstProveedores = objProveedor_DAL.ObtieneProveedores().Select(x =>
                                                                                  new SelectListItem
                                                                                  {
                                                                                      Value = x.IdProveedor.ToString(),
                                                                                      Text = x.RazonSocial
                                                                                  }).OrderBy(x => x.Text); ;

        ObrasPublicas.DAL.Material_DAL objMaterial_DAL = new ObrasPublicas.DAL.Material_DAL();

        var lstMateriales = objMaterial_DAL.ObtieneMateriales().Select(x =>
                                                                        new SelectListItem
                                                                        {
                                                                            Value = x.IdMaterial.ToString(),
                                                                            Text = x.Nombre + " (en " + x.UnidadMedida + ")"
                                                                        }).OrderBy(x => x.Text);

        ObrasPublicas.DAL.EntregaMaterial_DAL objEntregaMaterial_DAL = new ObrasPublicas.DAL.EntregaMaterial_DAL();
        var lstTiposEntrega = objEntregaMaterial_DAL.ObtieneTiposEntrega().Select(x =>
                                                                                  new SelectListItem
                                                                                  {
                                                                                      Value = x.Id.ToString(),
                                                                                      Text = x.Nombre
                                                                                  }).OrderBy(x => x.Text);
    %>
    <section class="content-header" style="padding-bottom:5px">
        <h1>Actualizar Calendario de Entrega de Materiales</h1>
        <div>&nbsp;</div>
        
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button id="btnIconCrear" type="button" class="btn btn-default" onclick="document.location.href='/entregamaterialop/create?p=<%:Model.IdProyecto%>'">
                    <span class="fa fa-file" aria-hidden="true"></span> Nueva Entrega
                </button>
                <button id="btnListado" type="button" class="btn btn-default" onclick="document.location.href='/entregamaterialop/listado?p=<%:Model.IdProyecto %>'">
                    <span class="fa fa-search" aria-hidden="true"></span> Ver Entregas de Material
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
            <h3 class="panel-title">Modificar Entrega de Material</h3>
        </div>
        <div class="panel-body">
            <div class="form-horizontal" role="form">
                <% using (Html.BeginForm("Save_Update", "EntregaMaterialOP", FormMethod.Post, new { @name = "frmUpdate", @id = "frmUpdate" }))
                { %>
                <%: Html.AntiForgeryToken() %>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Proyecto:</label>
                    <div class="col-sm-9">
                        <strong><%: Html.DisplayFor(m => m.IdProyecto) %> - <%: Html.DisplayFor(m => m.NomProyecto) %></strong>
                        <%: Html.HiddenFor(m => m.IdProyecto, new { Value = Model.IdProyecto })%>
                        <%: Html.HiddenFor(m => m.IdEntrega, new { Value = Model.IdEntrega })%>
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
                    <label class="col-sm-3 control-label">* Fecha de entrega programada:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.FecEntregaProg, new { @class = "form-control", maxlength = "10", @placeholder="dd/mm/yyyy", @onkeydown="return f_OnKeyDown_fecha(this,event);", @onkeypress="return f_solo_numeros_fecha(event);", @onkeyup="return f_OnKeyUp_fecha(this,event);" }) %>
                        <%: Html.ValidationMessageFor(m => m.FecEntregaProg) %>
                        <div id="Err_FecEntregaProg" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Fecha de entrega efectiva:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.FecEntregaEfec, new { @class = "form-control", maxlength = "10", @placeholder="dd/mm/yyyy", @onkeydown="return f_OnKeyDown_fecha(this,event);", @onkeypress="return f_solo_numeros_fecha(event);", @onkeyup="return f_OnKeyUp_fecha(this,event);" }) %>
                        <%: Html.ValidationMessageFor(m => m.FecEntregaEfec) %>
                        <div id="Err_FecEntregaEfec" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Observaciones:</label>
                    <div class="col-sm-9">
                            <%: Html.TextAreaFor(m => m.Observaciones, new { @class = "form-control", maxlength = "500"}) %>
                        <%: Html.ValidationMessageFor(m => m.Observaciones) %>
                        <div id="Err_Observaciones" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Tipo de entrega:</label>
                    <div class="col-sm-9">
                        <%: Html.DropDownListFor(m => m.TipoEntrega, lstTiposEntrega, "----", new { @class = "form-control" })%>
                        <%: Html.ValidationMessageFor(m => m.TipoEntrega) %>
                        <div id="Err_TipoEntrega" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Proveedor:</label>
                    <div class="col-sm-9">
                        <%: Html.DropDownListFor(m => m.IdProveedor, lstProveedores, "----", new { @class = "form-control" })%>
                        <%: Html.ValidationMessageFor(m => m.IdProveedor) %>
                        <div id="Err_IdProveedor" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Material:</label>
                    <div class="col-sm-9">
                        <%: Html.DropDownListFor(m => m.IdMaterial, lstMateriales, "----", new { @class = "form-control" })%>
                        <%: Html.ValidationMessageFor(m => m.IdMaterial) %>
                        <div id="Err_Material" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Cantidad:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.Cantidad, new { @class = "form-control numbersOnly", maxlength = "7"}) %>
                        <%: Html.ValidationMessageFor(m => m.Cantidad) %>
                        <div id="Err_Cantidad" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                
                    <div class="col-sm-12 text-center">
                        &nbsp;
                    </div>
                
                    <div class="form-group">
                    <div class="col-sm-12 text-center">
                        <%if (ViewData.ModelState[""] != null && ViewData.ModelState[""].Errors.Count > 0)
                        { %>
                            <div class="alert alert-error" style="text-align:left">
                                <button type="button" class="close" data-dismiss="alert">×</button>
                                <%:Html.ValidationSummary(true, "Error: ") %>
                            </div>
                        <%}%>
                        <%if(ViewBag.MsgSuccess!=null) {%>
                        <div id="div1"  class="alert alert-success" role="alert">
                            <%:ViewBag.MsgSuccess %>
                        </div>
                        <%} %>
                    </div>
                    <div class="col-sm-12 text-center">
                        <button id="btnGrabar" class="btn btn-primary" type="submit">Grabar</button>
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