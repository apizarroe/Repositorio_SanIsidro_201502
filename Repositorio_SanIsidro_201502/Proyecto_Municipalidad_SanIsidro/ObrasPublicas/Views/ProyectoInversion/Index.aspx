<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Intranet.Master" Inherits="System.Web.Mvc.ViewPage<ObrasPublicas.Models.ProyectoInversion.CreateProyectoInversionModel>" %>

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
    %>
    <section class="content-header" style="padding-bottom:5px">
        <h1>Registrar Proyectos de Inversión</h1>
        <div>&nbsp;</div>
        
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button id="btnIconCrear" type="button" class="btn btn-primary" onclick="document.location.href='/proyectoinversion/index'">
                    <span class="fa fa-file" aria-hidden="true"></span> Crear
                </button>
                <button id="btnIconModificar" type="button" class="btn btn-default" onclick="document.location.href='/proyectoinversion/update'">
                    <span class="fa fa-pencil" aria-hidden="true"></span> Modificar
                </button>
            </div>
        </div>
    </section>
    <section class="content">
    <% using (Html.BeginForm("Create", "ProyectoInversion", FormMethod.Post, new { @name = "frmCreate", @id = "frmCreate" }))
       { %>
        <%: Html.AntiForgeryToken() %>
    <div id="divPanelCrear" class="panel panel-primary">
            <div class="panel-heading">
              <h3 class="panel-title">Crear Proyecto de Inversión</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal" role="form">
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
                            <%: Html.HiddenFor(m => m.NomViaReal) %>
                            <%: Html.HiddenFor(m => m.IdVia) %>
                            <%: Html.ValidationMessageFor(m => m.IdVia) %>
                        </div>
                        <div class="col-sm-4">
                            <%: Html.TextBoxFor(m => m.Ubicacion, new { @class = "form-control", maxlength = "120", @PlaceHolder="ubicación adicional"}) %>
                            <%: Html.ValidationMessageFor(m => m.Ubicacion) %>
                        </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* # de beneficiarios:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.Beneficiarios, new { @class = "form-control numbersOnly", maxlength = "6"}) %>
                        <%: Html.ValidationMessageFor(m => m.Beneficiarios) %>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Valor referencial (S/.):</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.ValorReferencial, new { @class = "form-control decimalsOnly", maxlength = "9"}) %>
                        <%: Html.ValidationMessageFor(m => m.ValorReferencial) %>
                        <p></p>
                    </div>
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
                        <div id="divMensajeOK"  class="alert alert-success" role="alert">
                            <%:ViewBag.MsgSuccess %>
                        </div>
                        <%} %>
                    </div>
                    <div class="col-sm-12 text-center">
                    <button id="btnGrabar" class="btn btn-primary" type="submit">Grabar</button>
                    <button id="btnLimpiar" class="btn btn-primary" type="button" onclick="document.location.href='/proyectoinversion/index'">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <% } %>
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

            $("#NomVia").keyup(function () {
                if ($("#IdVia").val() != "") {
                    if ($("#NomVia").val() != $("#NomViaReal").val()) {
                        $("#NomVia").val("");
                        $("#NomViaReal").val("");
                        $("#IdVia").val("");
                    }
                }
            });
            $("#NomVia").blur(function () {
                if ($("#IdVia").val() == "") {
                    $("#NomVia").val("");
                    $("#NomViaReal").val("");
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
            $('#TipoVia').change(function () {
                $('#IdVia').empty();

                var selectedValue = $(this).val();
                if (selectedValue != "") {
                    var dataToSend = {
                        pStrTipoVia: selectedValue
                    };
                    $.ajax({
                        url: "/ProyectoInversion/Lista_NombreVias",
                        data: dataToSend,
                        success: function (result) {
                            $('#IdVia').append(
                                $('<option/>', {
                                    value: "",
                                    text: "(Seleccione)"
                                })
                            );
                            $.each(result, function (index, item) {
                                $('#IdVia').append(
                                    $('<option/>', {
                                        value: item.Value,
                                        text: item.Text
                                    })
                                );
                            });
                        }
                    });
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
                    //fill selected customer details on form
                    $("#NomVia").val(ui.item.label);
                    $("#NomViaReal").val(ui.item.label);
                    $("#IdVia").val(ui.item.value);
                    //alert(ui.item.label);
                    //alert(ui.item.value);
                },
                focus: function (event, ui) {
                    $("#NomVia").val(ui.item.label);
                    $("#NomViaReal").val(ui.item.label);
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
                    if ($("#frmCreate").valid()) {
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