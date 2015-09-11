<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Intranet.Master" Inherits="System.Web.Mvc.ViewPage<ObrasPublicas.Models.CronogramaEjecucionObra.UpdateCronogramaEjecucionObraModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Municipalidad de San Isidro - Cronograma de Ejecución de Obra
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<style>
    .field-validation-error
    {
        color: #ff0000;
        font-size: 10px;
    }
    .validation-summary-errors
    {
    }
</style>
    <section class="content-header" style="padding-bottom:5px">
        <h1>Actualizar Cronograma de Ejecución de Obra</h1>
        <div>&nbsp;</div>
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button id="btnIconCrear" type="button" class="btn btn-default" onclick="document.location.href='/CronogramaEjecucionObra/create?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>'">
                    <span class="fa fa-file" aria-hidden="true"></span> Crear
                </button>
                <button id="btnIconCrearActividad" type="button" class="btn btn-default" onclick="document.location.href='/CronogramaEjecucionObra/listado?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>&c=<%:Request.QueryString["c"]%>'">
                    <span class="fa fa-file" aria-hidden="true"></span> Modificar Actividades
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
              <h3 class="panel-title">Modificar Datos Generales</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <% using (Html.BeginForm("Update", "CronogramaEjecucionObra", FormMethod.Post, new { @name = "frmUpdate", @id = "frmUpdate" }))
                       { %>
                        <%: Html.AntiForgeryToken() %>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Proyecto:</label>
                    <div class="col-sm-9">
                        <strong><%: Html.DisplayFor(m => m.IdProyecto) %> - <%: Html.DisplayFor(m => m.NomProyecto) %></strong>
                        <%: Html.HiddenFor(m => m.IdExpediente, new { Value = Request.QueryString["e"] })%>
                        <%: Html.HiddenFor(m => m.IdCronograma, new { Value = Request.QueryString["c"] })%>
                        <%: Html.HiddenFor(m => m.IdProyecto, new { Value = Request.QueryString["p"] })%>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Plazo ejecución:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.PlazoEjecucion, new { @class = "form-control", maxlength = "4"}) %>
                        <%: Html.ValidationMessageFor(m => m.PlazoEjecucion) %>
                        <div id="Err_PlazoEjecucion" class="field-validation-error"></div>
                    </div>
                    </div>
                    <div class="col-sm-12 text-center">
                        &nbsp;
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12 text-center">
                        <div id="Err_General" class="alert alert-error" style="text-align:left; display:none">
                            <button type="button" class="close" data-dismiss="alert">×</button>
                        </div>
                            <div id="divMensajeOK" class="alert alert-success" role="alert" style="display:none"></div>
                        </div>
                        <div class="col-sm-12 text-center">
                        <button id="btnGrabar" class="btn btn-primary" type="button">Grabar</button>
                        </div>
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
    <script>
        $(document).ready(function () {
            $('.numbersOnly').keyup(function () {
                this.value = this.value.replace(/[^0-9\.]/g, '');
            });

            $(".decimalsOnly").keyup(function () {
                var $this = $(this);
                $this.val($this.val().replace(/[^\d.]/g, ''));
            });
        });

        $("#btnAgregarActividad").click(function () {
            $.ajax({
                url: "/CronogramaEjecucionObra/BlankEditorRow",
                cache: false,
                success: function (html) { $("#editorRow").append(html); }
            });
            return false;
        });

        $("#addItem").click(function () {
            $.ajax({
                url: this.href,
                cache: false,
                success: function (html) { $("#editorRow").append(html); }
            });
            return false;
        });

        $(document).on('click', 'a.deleteRow', function () {
            $(this).parents("div.editRow:first").remove();
            return false;
        });

        $("#btnGrabar").click(function () {
            //$("#frmUpdate").submit();
            var infoForm = $("#frmUpdate");

            //waitingDialog.show("Procesando..");
            $.ajax({
                url: "/CronogramaEjecucionObra/Save_Update",
                type: "POST",
                data: infoForm.serialize(),
                dataType: "json",
                success: function (data) {
                    if (data.Valid) {
                        //$("#divStudent").html(data.StudentsPartial);
                        //$("input").val("");
                        $("#divMensajeOK").html("Se realizó la operación satisfactoriamente.");
                        $("#divMensajeOK").show();
                        //$(':input').val('');
                        $(".field-validation-error").html('');
                        $("#Err_General").hide();
                    }
                    $.each(data.Errors, function (key, value) {
                        $(".field-validation-error").html('');
                        $("#Err_General").hide();
                        if (value != null) {
                            $("#Err_" + key).html(value[value.length - 1].ErrorMessage);
                            if (key == "General") {
                                $("#Err_General").show();
                            }
                        }
                    });
                },
                error: function (jqXHR, textStatus, errorThrown) {
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