<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Intranet.Master" Inherits="System.Web.Mvc.ViewPage<ObrasPublicas.Models.CronogramaEjecucionObra.UpdateActividadCronogramaEjecucionModel>" %>

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
        /*font-size: 10px;*/
    }
    .validation-summary-errors
    {
    }
</style>
<%
    ObrasPublicas.DAL.CronogramaEjecucionObra_DAL objCronogramaEjecucionObra_DAL = new ObrasPublicas.DAL.CronogramaEjecucionObra_DAL();
    var lstAreas = objCronogramaEjecucionObra_DAL.ObtieneAreas().Select(x =>
                                                                        new SelectListItem
                                                                        {
                                                                            Value = x.Id,
                                                                            Text = x.Nombre
                                                                        }).OrderBy(x => x.Text);
%>
    <section class="content-header" style="padding-bottom:5px">
        <h1>Modificar Cronograma de Ejecución de Obra</h1>
        <div>&nbsp;</div>
        
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button id="btnBuscar" type="button" class="btn btn-default" onclick="document.location.href='/CronogramaEjecucionObra/index'">
                    <span class="fa fa-pencil" aria-hidden="true"></span> Volver a buscar proyecto
                </button>
                <button id="btnRegresar" type="button" class="btn btn-default" onclick="document.location.href='/CronogramaEjecucionObra/listado?p=<%:Request.QueryString["p"]%>&e=<%:Request.QueryString["e"]%>&c=<%:Request.QueryString["c"]%>'">
                    <span class="fa fa-pencil" aria-hidden="true"></span> Ver cronograma
                </button>
            </div>
        </div>
    </section>
    <section class="content">
    <div id="divPanelCrear" class="panel panel-primary">
            <div class="panel-heading">
              <h3 class="panel-title">Actualizar Actividad</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <% using (Html.BeginForm("Save_UpdateActividad", "CronogramaEjecucionObra", FormMethod.Post, new { @name = "frmUpdate", @id = "frmUpdate" }))
                       { %>
                        <%: Html.AntiForgeryToken() %>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Proyecto:</label>
                    <div class="col-sm-9">
                        <strong><%: Html.DisplayFor(m => m.IdProyecto) %> - <%: Html.DisplayFor(m => m.NomProyecto) %></strong>
                        <%: Html.HiddenFor(m => m.IdProyecto, new { Value = Request.QueryString["p"] })%>
                        <%: Html.HiddenFor(m => m.IdExpediente, new { Value = Request.QueryString["e"] })%>
                        <%: Html.HiddenFor(m => m.IdCronograma, new { Value = Request.QueryString["c"] })%>
                        <%: Html.HiddenFor(m => m.IdActividad, new { Value = Request.QueryString["a"] })%>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Plazo ejecución:</label>
                    <div class="col-sm-9">
                            <%: Html.DisplayFor(m => m.PlazoEjecucion) %>
                    </div>
                    </div>
                    <!--DIV QUE OCULTE CREAR UNA ACTIVIDAD SI NO HA INGRESADO EL PLAZO Y HA CREADO EL CRONOGRAMA VACIO PRIMERO-->
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Datos de la actividad</label>
                    <div class="col-sm-9">&nbsp;</div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Nombre Actividad:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.NomAct, new { @class = "form-control", maxlength = "50"}) %>
                        <%: Html.ValidationMessageFor(m => m.NomAct) %>
                        <div id="Err_NomAct" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Fecha inicio programada:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.FechaIniProgAct, new { @class = "form-control", maxlength = "10", @placeholder="dd/mm/yyyy"}) %>
                        <%: Html.ValidationMessageFor(m => m.FechaIniProgAct) %>
                        <div id="Err_FechaIniProgAct" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Fecha fin programada:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.FechaFinProgAct, new { @class = "form-control", maxlength = "10", @placeholder="dd/mm/yyyy"}) %>
                        <%: Html.ValidationMessageFor(m => m.FechaFinProgAct) %>
                        <div id="Err_FechaFinProgAct" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Fecha inicio ejecución:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.FechaIniEjecAct, new { @class = "form-control", maxlength = "10", @placeholder="dd/mm/yyyy"}) %>
                        <%: Html.ValidationMessageFor(m => m.FechaIniEjecAct) %>
                        <div id="Err_FechaIniEjecAct" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Fecha fin ejecución:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.FechaFinEjecAct, new { @class = "form-control", maxlength = "10", @placeholder="dd/mm/yyyy"}) %>
                        <%: Html.ValidationMessageFor(m => m.FechaFinEjecAct) %>
                        <div id="Err_FechaFinEjecAct" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Costo:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.CostoAct, new { @class = "form-control decimalsOnly", maxlength = "7"}) %>
                        <%: Html.ValidationMessageFor(m => m.CostoAct) %>
                        <div id="Err_CostoAct" class="field-validation-error"></div>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Cantidad RRHH:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.CantidadRRHHAct, new { @class = "form-control numbersOnly", maxlength = "7"}) %>
                        <%: Html.ValidationMessageFor(m => m.CantidadRRHHAct) %>
                        <div id="Err_CantidadRRHHAct" class="field-validation-error"></div>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Responsable:</label>
                    <div class="col-sm-9">
                        <%:Html.RadioButtonFor(m => m.ResponsableActTipo, "P") %> Persona natural
                        <%:Html.RadioButtonFor(m => m.ResponsableActTipo, "E") %> Empresa
                        <div id="Err_TipoEjecutor" class="field-validation-error"></div>
                    </div>
                    </div>
                    <%
                           String strDisplayReponsablePersona = "none";
                           String strDisplayReponsableEmpresa = "none";

                           if (Model.ResponsableActTipo == "P")
                           {
                               strDisplayReponsablePersona = "block";
                               strDisplayReponsableEmpresa = "none";
                           }
                           else {
                               strDisplayReponsablePersona = "none";
                               strDisplayReponsableEmpresa = "block";
                           }
                         %>
                    <div id="divResponsablePersona" style="display:<%:strDisplayReponsablePersona%>">
                        <%
                           if (ViewBag.lstEmpleadosPersona != null)
                           {
                         %>
                        <div class="form-group">
                        <label class="col-sm-3 control-label">* Area:</label>
                        <div class="col-sm-9">
                            <%: Html.DropDownListFor(m => m.IdAreaResponsable, lstAreas, "----", new { @class = "form-control" })%>
                            <div id="Err_IdAreaResponsable" class="field-validation-error"></div>
                        </div>
                        </div>
                        <%} %>
                        <div class="form-group">
                        <label class="col-sm-3 control-label">* Empleado:</label>
                        <div class="col-sm-9">
                        <%
                           if (ViewBag.lstEmpleadosPersona != null)
                           {
                               var lstEmpleadosPersona = ((List<ObrasPublicas.Entities.ItemCombo>)ViewBag.lstEmpleadosPersona).Select(x =>
                                                                        new SelectListItem
                                                                        {
                                                                            Value = x.Id,
                                                                            Text = x.Nombre
                                                                        }).OrderBy(x => x.Text);
                           %>
                            <%: Html.DropDownListFor(m => m.IdResponsablePersonaNatural, lstEmpleadosPersona,"----",  new { @class = "form-control" })%>
                            <%: Html.ValidationMessageFor(m => m.IdResponsablePersonaNatural) %>
                            <div id="Err_IdResponsablePersonaNatural" class="field-validation-error"></div>
                        <%
                           } else{
                           %>
                            <%: Html.DropDownListFor(m => m.IdResponsablePersonaNatural, Enumerable.Empty<SelectListItem>(), new { @class = "form-control" })%>
                            <%: Html.ValidationMessageFor(m => m.IdResponsablePersonaNatural) %>
                            <div id="Err_IdResponsablePersonaNatural" class="field-validation-error"></div>
                            <%
                           } %>
                        </div>
                        </div>
                    </div>
                    <div id="divResponsableEmpresa" style="display:<%:strDisplayReponsableEmpresa%>">
                        <div class="form-group">
                        <label class="col-sm-3 control-label">* Ra&oacute;n social:</label>
                        <div class="col-sm-9">
                        <%
                           var lstEmpleadosEmpresa = ((List<ObrasPublicas.Entities.ItemCombo>)ViewBag.lstEmpleadosEmpresa).Select(x =>
                                                                        new SelectListItem
                                                                        {
                                                                            Value = x.Id,
                                                                            Text = x.Nombre
                                                                        }).OrderBy(x => x.Text);
                           %>
                            <%: Html.DropDownListFor(m => m.IdResponsablePersonaJuridica, lstEmpleadosEmpresa, "----", new { @class = "form-control" })%>
                            <%: Html.ValidationMessageFor(m => m.IdResponsablePersonaJuridica) %>
                            <div id="Err_IdResponsablePersonaJuridica" class="field-validation-error"></div>
                        </div>
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
            //$("#frmCreate").submit();
            var infoForm = $("#frmUpdate");

            //waitingDialog.show("Procesando..");
            $.ajax({
                url: "/CronogramaEjecucionObra/Save_UpdateActividad",
                type: "POST",
                data: infoForm.serialize(),
                dataType: "json",
                success: function (data) {
                    $(".field-validation-error").html('');
                    $("#Err_General").hide();
                    if (data.Valid) {
                        $("#divMensajeOK").html("Se realizó la operación satisfactoriamente.");
                        $("#divMensajeOK").show();
                    }
                    $.each(data.Errors, function (key, value) {
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

        $('input[type=radio][name=ResponsableActTipo]').change(function () {
            if (this.value == 'P') {
                $("#divResponsablePersona").show();
                $("#divResponsableEmpresa").hide();
                //alert("Allot Thai Gayo Bhai");
            }
            else if (this.value == 'E') {
                $("#divResponsablePersona").hide();
                $("#divResponsableEmpresa").show();
                //alert("Transfer Thai Gayo");
            }
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