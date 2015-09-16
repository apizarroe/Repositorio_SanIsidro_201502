<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Intranet.Master" Inherits="System.Web.Mvc.ViewPage<ObrasPublicas.Models.ProyectoInversion.UpdateProyectoInversionModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Municipalidad de San Isidro - Proyectos de Inversión
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
        Infraestructura.Data.SQL.Via_DAL objVia_DAL = new Infraestructura.Data.SQL.Via_DAL();
        var lstTipoVia = objVia_DAL.ObtieneVias(null).Select(v => v.Tipo).Distinct();

        ObrasPublicas.DAL.ProyectoInversion_DAL objProyectoInversion_DAL = new ObrasPublicas.DAL.ProyectoInversion_DAL();
        var lstEstadoSearch = objProyectoInversion_DAL.ObtieneEstados(null).Select(x =>
                                                                                  new SelectListItem
                                                                                  {
                                                                                      Value = x.Id,
                                                                                      Text = x.Nombre
                                                                                  }).OrderBy(x => x.Text);

        var lstEstadoUpdate = objProyectoInversion_DAL.ObtieneEstados(null).Where(x => (x.Id == ObrasPublicas.Entities.ProyectoInversion.STR_ID_ESTADO_VIABLE || x.Id == ObrasPublicas.Entities.ProyectoInversion.STR_ID_ESTADO_INVIABLE || x.Id == ObrasPublicas.Entities.ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA)).Select(x =>
                                                                                  new SelectListItem
                                                                                  {
                                                                                      Value = x.Id,
                                                                                      Text = x.Nombre
                                                                                  }).OrderBy(x => x.Text);
                                                                                  
        int intIdProyecto = 0;

        if (@Model != null) {
            intIdProyecto = @Model.IdProyecto;
        }
    %>

    <section class="content-header" style="padding-bottom:5px">
        <h1>Actualizar Proyectos de Inversión</h1>
        <div>&nbsp;</div>
        
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button id="btnIconCrear" type="button" class="btn btn-default" onclick="document.location.href='/proyectoinversion/index'">
                    <span class="fa fa-file" aria-hidden="true"></span> Crear
                </button>
                <button id="btnIconModificar" type="button" class="btn btn-primary" onclick="document.location.href='/proyectoinversion/update'">
                    <span class="fa fa-pencil" aria-hidden="true"></span> Modificar
                </button>
            </div>
        </div>
    </section>
        
    <section class="content">
        <div id="divPanelModificar" class="panel panel-primary" >
            <div class="panel-heading">
              <h3 class="panel-title">Modificar Proyecto de Inversión</h3>
            </div>
            
    <% using (Html.BeginForm("Search", "ProyectoInversion"))
       { %>
        <%: Html.AntiForgeryToken() %>
            <%
           if (ViewBag.MostrarSearch != "0")
           { 
          %>

            <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Nombre</label>
                        <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.SearchNombre, new { @class = "form-control", maxlength = "100"}) %>
                            <p></p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Ubicación</label>
                        <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.SearchUbicacion, new { @class = "form-control", maxlength = "100"}) %>
                            <p></p>
                        </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Estado:</label>
                    <div class="col-sm-9">
                        <%: Html.DropDownListFor(m => m.SearchIdEstado, lstEstadoSearch, "(Todos)", new { @class = "form-control" })%>
                        <p></p>
                    </div>
                    </div>
                    <div class="col-sm-12 text-center">
                        <button id="btnBuscarProyectos" class="btn btn-primary" type="submit">Buscar</button>
                    </div>

                    <%
               List<ObrasPublicas.Entities.ProyectoInversion> lstProyectos = ViewBag.lstProyectos;
                        if (lstProyectos != null) {
                            if (lstProyectos.Count == 0)
                            {
                            %>
                            <div>No se encontraron proyectos con los campos de búsqueda ingresados.</div>
                            <%
                            }
                            else { 
                            %>
                            <table class="table table-condensed">
                                <thead>
                                  <tr>
                                    <th></th>
                                    <th></th>
                                    <th>C&oacute;digo</th>
                                    <th>Nombre</th>
                                    <th>Ubicación</th>
                                    <th>Cod. SNIP</th>
                                    <th>Estado</th>
                                  </tr>
                                </thead>
                                <tbody>
                                <%
                                foreach (ObrasPublicas.Entities.ProyectoInversion objProyecto in lstProyectos)
                                { 
                                %>
                                    <tr>
                                        <td>
                                            <%if (objProyecto.IdEstado == ObrasPublicas.Entities.ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA)
                                              { 
                                              %>
                                            <a href="/proyectoinversion/edit?id=<%:objProyecto.IdProyecto %>">Modificar</a>
                                            <%
                                              }%>
                                        </td>
                                        <td><a href="/proyectoinversion/detail?id=<%:objProyecto.IdProyecto %>">Detalle</a></td>
                                        <td><%:objProyecto.IdProyecto %></td>
                                        <td><%:objProyecto.Nombre %></td>
                                        <td><%:objProyecto.TipoVia %> <%:objProyecto.NomVia %> <%:objProyecto.Ubicacion %></td>
                                        <td><%:objProyecto.CodSNIP %></td>
                                        <td><%:objProyecto.NomEstado %></td>
                                    </tr>
                                <%
                                }
                                %>
                                </tbody>
                            </table>
                            <%
                            }
                            %>
                    <%
                        }
                    %>
                </div>
            </div>             
            <%
           }
        %>
    <% } %>

    <% using (Html.BeginForm("Save_Update", "ProyectoInversion", FormMethod.Post, new { @name = "frmUpdate", @id = "frmUpdate" }))
       { %>
        <%: Html.AntiForgeryToken() %>
            <% if (ViewBag.MostrarSearch == "0") {
                   var lstNomVias = objVia_DAL.ObtieneVias(Model.TipoVia).Select(x =>
                                                                                  new SelectListItem
                                                                                  {
                                                                                      Value = x.IdVia.ToString(),
                                                                                      Text = x.Nombre
                                                                                  }).OrderBy(x => x.Text);
                   
              %> 
            <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">Código proyecto</label>
                        <div class="col-sm-9">
                            <strong><%: Html.DisplayFor(m => m.IdProyecto) %></strong>
                            <%: Html.HiddenFor(m => m.IdProyecto) %>
                            <p></p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="inputEmail3" class="col-sm-3 control-label">* Código SNIP</label>
                        <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.CodSNIP, new { @class = "form-control", maxlength = "45"}) %>
                        <%: Html.ValidationMessageFor(m => m.CodSNIP) %>
                            <p></p>
                        </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Nombre:</label>
                    <div class="col-sm-9">
                        <%: Html.TextBoxFor(m => m.Nombre, new { @class = "form-control", maxlength = "100"}) %>
                        <%: Html.ValidationMessageFor(m => m.Nombre) %>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Descripcion:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.Descripcion, new { @class = "form-control", maxlength = "300"}) %>
                        <%: Html.ValidationMessageFor(m => m.Descripcion) %>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">* Ubicación:</label>
                        <div class="col-sm-2">
                            <%: Html.DropDownListFor(m => m.TipoVia, new SelectList(lstTipoVia), "----", new { @class = "form-control" })%>
                            <%: Html.ValidationMessageFor(m => m.TipoVia) %>
                        </div>
                        <div class="col-sm-3">
                            <%: Html.TextBoxFor(m => m.NomVia, new { @class = "form-control", maxlength = "100", @PlaceHolder="nombre de la vía"}) %>
                            <%: Html.ValidationMessageFor(m => m.NomVia) %>
                            <%: Html.HiddenFor(m => m.IdVia) %>
                        </div>
                        <div class="col-sm-4">
                            <%: Html.TextBoxFor(m => m.Ubicacion, new { @class = "form-control", maxlength = "120", @PlaceHolder="ubicación adicional"}) %>
                            <%: Html.ValidationMessageFor(m => m.Ubicacion) %>
                        </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* # de beneficiarios:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.Beneficiarios, new { @class = "form-control numbersOnly", maxlength = "7"}) %>
                        <%: Html.ValidationMessageFor(m => m.Beneficiarios) %>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Valor referencial (S/.):</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.ValorReferencial, new { @class = "form-control decimalsOnly", maxlength = "12"}) %>
                        <%: Html.ValidationMessageFor(m => m.ValorReferencial) %>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Estado:</label>
                    <div class="col-sm-9">
                        <%
                   bool bolConError = false;
                            if (ViewData.ModelState[""] != null && ViewData.ModelState[""].Errors != null && ViewData.ModelState[""].Errors.Count > 0) {
                                bolConError = true;
                   }

                            if (!bolConError && Model.IdEstado != ObrasPublicas.Entities.ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA)
                          { 
                          %>
                            <%: Html.DropDownListFor(m => m.IdEstado, lstEstadoUpdate, new { @class = "form-control", disabled="disabled" })%>
                        <%
                          }else{
                          %>
                            <%: Html.DropDownListFor(m => m.IdEstado, lstEstadoUpdate, new { @class = "form-control" })%>
                        <%
                          }%>
                            <%: Html.ValidationMessageFor(m => m.IdEstado) %>
                    </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-12 text-center">
                            <%if (ViewData.ModelState[""] != null && ViewData.ModelState[""].Errors!=null && ViewData.ModelState[""].Errors.Count > 0)
                            { %>
                                <div class="alert alert-error" style="text-align:left">
                                    <button type="button" class="close" data-dismiss="alert">×</button>
                                    <%:Html.ValidationSummary(true, "Error: ") %>
                                </div>
                            <%}%>
                            <%if(ViewBag.MsgSuccess!=null) {%>
                            <div id="divMensajeOK"  class="alert alert-success" role="alert">
                                <%:ViewBag.MsgSuccess %>
                            </div>
                            <%} %>
                        </div>
                        <div class="col-sm-12 text-center">
                        <button id="btnGrabar" class="btn btn-primary" type="submit">Grabar</button>
                        <button id="btnLimpiar" class="btn btn-primary" type="button" onclick="document.location.href='/proyectoinversion/edit?id=<%:intIdProyecto%>'">Limpiar</button>
                        <button id="btnVoverBuscar" class="btn btn-primary" type="button" onclick="document.location.href='/proyectoinversion/update'">Volver a buscar</button>
                        </div>
                    </div>
            </div>
        </div>
            <%
               } %>
    <% } %>
    </div>
    </section>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jquery") %>
    <%: Scripts.Render("~/Scripts/bootstrap.min.js") %>
    <%: Scripts.Render("~/Scripts/jquery-ui-1.11.4.min.js") %>
    <script>
        $(document).ready(function () {
            $('.numbersOnly').keyup(function (e) {
                if (e.keyCode != 37 && e.keyCode != 39 && e.keyCode != 17 && e.keyCode != 16) {
                    var $this = $(this);
                    this.value = this.value.replace(/[^0-9\.]/g, '');
                }
            });

            $(".decimalsOnly").keyup(function (e) {
                if (e.keyCode != 37 && e.keyCode != 39 && e.keyCode != 17 && e.keyCode != 16) {
                    var $this = $(this);
                    $this.val($this.val().replace(/[^\d.]/g, ''));
                }
            });

            //if ($('#TipoVia').val() != "") {
            //    $('#IdVia').empty();
            //    var dataToSend = {
            //        pStrTipoVia: $('#TipoVia').val()
            //    }
            //    $.ajax({
            //        url: "/ProyectoInversion/Lista_NombreVias",
            //        data: dataToSend,
            //        success: function (result) {
            //            $('#IdVia').append(
            //                $('<option/>', {
            //                    value: "",
            //                    text: "(Seleccione)"
            //                })
            //            );
            //            $.each(result, function (index, item) {
            //                $('#IdVia').append(
            //                    $('<option/>', {
            //                        value: item.Value,
            //                        text: item.Text
            //                    })
            //                );
            //            });

            //            $("#IdVia").val($('#IdViaTmp').val());
            //        }
            //    });
            //}
        });

        $(document).ready(function () {
            //$(this).button('reset');
            //$('#TipoVia').change(function () {
            //    $('#IdVia').empty();

            //    var selectedValue = $(this).val();
            //    if (selectedValue != "") {
            //        var dataToSend = {
            //            pStrTipoVia: selectedValue
            //        };
            //        $.ajax({
            //            url: "/ProyectoInversion/Lista_NombreVias",
            //            data: dataToSend,
            //            success: function (result) {
            //                $('#IdVia').append(
            //                    $('<option/>', {
            //                        value: "",
            //                        text: "(Seleccione)"
            //                    })
            //                );
            //                $.each(result, function (index, item) {
            //                    $('#IdVia').append(
            //                        $('<option/>', {
            //                            value: item.Value,
            //                            text: item.Text
            //                        })
            //                    );
            //                });
            //            }
            //        });
            //    }
            //});

            $('#IdEstado').change(function () {
                var selectedValue = $(this).val();
                if (selectedValue == "I") {
                    $("#CodSNIP").prop('disabled', true);
                    $("#CodSNIP").val("-");
                }
                else {
                    $("#CodSNIP").prop('disabled', false);
                    if ($("#CodSNIP").val()=="-") {
                        $("#CodSNIP").val("");
                    }
                }
            });

            $("#NomVia").autocomplete({
                minLength: 3,
                source: function (request, response) {
                    $.ajax({
                        //async: false,
                        //cache: false,
                        type: "POST",
                        dataType: "json",
                        url: "/proyectoinversion/FiltrarVias",
                        data: { "pStrTipoVia": $('#TipoVia').val(), "pStrDesc": request.term },
                        success: function (data) {
                            response($.map(data, function (item) {
                                return { label: item.Nombre, value: item.IdVia };
                            }))
                        }
                    });
                },
                select: function (event, ui) {
                    event.preventDefault();
                    $("#NomVia").val(ui.item.label);
                    $("#IdVia").val(ui.item.value);
                },
                focus: function (event, ui) {
                    $("#NomVia").val(ui.item.label);
                    return false;
                }
            }).data("ui-autocomplete")._renderItem = function (ul, item) {
                return $("<li />")
                    .data("ui-autocomplete", item)
                    .append("<a>" + item.label + "</a>")
                    .appendTo(ul);
            };

            $(function () {
                $("#btnGrabar").click(function () {
                    if ($("#frmUpdate").valid()) {
                        //Ajax call here
                        //$("#loading").fadeIn();
                        waitingDialog.show('Procesando...');
                    }
                });
            });
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
