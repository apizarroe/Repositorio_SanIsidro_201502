<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Intranet.Master" Inherits="System.Web.Mvc.ViewPage<ObrasPublicas.Models.ExpedienteTecnicoOP.UpdateExpedienteTecnicoOPModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Municipalidad de San Isidro - Expedientes Técnicos de Proyectos de Inversión
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
        ObrasPublicas.DAL.ExpedienteTecnicoOP_DAL objExpedienteTecnicoOP_DAL = new ObrasPublicas.DAL.ExpedienteTecnicoOP_DAL();
        var lstTiposDoc = objExpedienteTecnicoOP_DAL.ObtieneTiposDocumento(null).Select(x =>
                                                                                  new SelectListItem
                                                                                  {
                                                                                      Value = x.Id,
                                                                                      Text = x.Nombre
                                                                                  }).OrderBy(x => x.Text);
    %>
    <section class="content-header" style="padding-bottom:5px">
        <h1>Actualizar Expedientes Técnicos</h1>
        <div>&nbsp;</div>
        
        <div id="divControlButtons" class="panel panel-default">
            <div class="panel-body">
                <button id="btnIconCrear" type="button" class="btn btn-default" onclick="document.location.href='/expedientetecnicoop/index'">
                    <span class="fa fa-file" aria-hidden="true"></span> Crear
                </button>
                <button id="btnIconModificar" type="button" class="btn btn-primary" onclick="document.location.href='/expedientetecnicoop/search'">
                    <span class="fa fa-pencil" aria-hidden="true"></span> Modificar
                </button>
            </div>
        </div>
    </section>
    <section class="content">
    <div id="divPanelCrear" class="panel panel-primary">
            <div class="panel-heading">
              <h3 class="panel-title">Modificar Expediente Técinco</h3>
            </div>
            <div class="panel-body">
                <div class="form-horizontal" role="form">
                    <% using (Html.BeginForm("Save_Update", "ExpedienteTecnicoOP", FormMethod.Post, new { @name = "frmUpdate", @id = "frmUpdate", enctype = "multipart/form-data"}))
                       { %>
                        <%: Html.AntiForgeryToken() %>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Proyecto:</label>
                    <div class="col-sm-9">
                        <strong><%: Html.DisplayFor(m => m.IdProyecto) %> - <%: Html.DisplayFor(m => m.NomProyecto) %></strong>
                        <%: Html.HiddenFor(m => m.IdProyecto)%>
                        <%: Html.HiddenFor(m => m.NomProyecto)%>
                        <%: Html.HiddenFor(m => m.IdExpediente)%>
                        <%: Html.HiddenFor(m => m.IdEjecutor) %>
                        <%: Html.HiddenFor(m => m.IdContacto) %>
                        <%: Html.HiddenFor(m => m.IdSupervisor) %>
                    </div>
                    </div>

                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Descripción:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.Descripcion, new { @class = "form-control", maxlength = "500"}) %>
                        <%: Html.ValidationMessageFor(m => m.Descripcion) %>
                        <div id="Err_Descripcion" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Especificaciones técnicas:</label>
                    <div class="col-sm-9">
                        <%=Html.TextAreaFor(m => m.Especificaciones, new { @class = "form-control"})%>
                        <%: Html.ValidationMessageFor(m => m.Especificaciones) %>
                        <div id="Err_Especificaciones" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Valor referencial (S/.):</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.ValorReferencial, new { @class = "form-control decimalsOnly", maxlength = "9"}) %>
                        <%: Html.ValidationMessageFor(m => m.ValorReferencial) %>
                        <div id="Err_ValorReferencial" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Datos del Ejecutor</label>
                    <div class="col-sm-9">&nbsp;</div>
                    </div>
                        <%
                           String strDisplayEjecPer = "none";
                           String strDisplayEjecEmp = "none";

                           if (Model != null) {
                               if (Model.TipoEjecutor == "E")
                               {
                                   strDisplayEjecEmp = "block";
                               }
                               else if (Model.TipoEjecutor == "P")
                               {
                                   strDisplayEjecPer = "block";
                               }
                           }
                             %>
                    <div class="form-group">
                    <label class="col-sm-3"></label>
                    <div class="col-sm-9">
                        <%:Html.RadioButtonFor(m => m.TipoEjecutor, "P") %> Persona natural
                        <%:Html.RadioButtonFor(m => m.TipoEjecutor, "E") %> Empresa
                        <%: Html.ValidationMessageFor(m => m.TipoEjecutor) %>
                    </div>
                    </div>
                    <div id="divEjecutorPersona" style="display:<%:strDisplayEjecPer%>">
                        <div class="form-group">
                        <label class="col-sm-3 control-label">* Nombre:</label>
                        <div class="col-sm-9">
                                <%: Html.TextBoxFor(m => m.EjecutorNom, new { @class = "form-control", maxlength = "50"}) %>
                            <%: Html.ValidationMessageFor(m => m.EjecutorNom) %>
                            <div id="Err_EjecutorNom" class="field-validation-error"></div>
                        </div>
                        </div>
                        <div class="form-group">
                        <label class="col-sm-3 control-label">* Apellidos:</label>
                        <div class="col-sm-9">
                                <%: Html.TextBoxFor(m => m.EjecutorApe, new { @class = "form-control ", maxlength = "50"}) %>
                            <%: Html.ValidationMessageFor(m => m.EjecutorApe) %>
                            <div id="Err_EjecutorApe" class="field-validation-error"></div>
                        </div>
                        </div>
                    </div>
                    <div id="divEjecutorEmpresa" style="display:<%:strDisplayEjecEmp%>">
                        <div class="form-group">
                        <label class="col-sm-3 control-label">* Raz&oacute;n social:</label>
                        <div class="col-sm-9">
                                <%: Html.TextBoxFor(m => m.EjecutorRazonSocial, new { @class = "form-control", maxlength = "250"}) %>
                            <%: Html.ValidationMessageFor(m => m.EjecutorRazonSocial) %>
                            <div id="Err_EjecutorRazonSocial" class="field-validation-error"></div>
                        </div>
                        </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Datos de Contacto</label>
                    <div class="col-sm-9">&nbsp;</div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Nombre:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.ContactoNom, new { @class = "form-control", maxlength = "50"}) %>
                        <%: Html.ValidationMessageFor(m => m.ContactoNom) %>
                            <div id="Err_ContactoNom" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Apellidos:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.ContactoApe, new { @class = "form-control", maxlength = "50"}) %>
                        <%: Html.ValidationMessageFor(m => m.ContactoApe) %>
                            <div id="Err_ContactoApe" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Tel&eacute;fono:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.ContactoTelefono, new { @class = "form-control", maxlength = "20"}) %>
                        <%: Html.ValidationMessageFor(m => m.ContactoTelefono) %>
                            <div id="Err_ContactoTelefono" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* E-mail:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.ContactoEmail, new { @class = "form-control", maxlength = "80"}) %>
                        <%: Html.ValidationMessageFor(m => m.ContactoEmail) %>
                            <div id="Err_ContactoEmail" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Direcci&oacute;n:</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.ContactoDireccion, new { @class = "form-control", maxlength = "100"}) %>
                        <%: Html.ValidationMessageFor(m => m.ContactoDireccion) %>
                            <div id="Err_ContactoDireccion" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">Datos del Supervisor</label>
                    <div class="col-sm-9">&nbsp;</div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Nombre</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.SupervisorNom, new { @class = "form-control", maxlength = "50"}) %>
                        <%: Html.ValidationMessageFor(m => m.SupervisorNom) %>
                            <div id="Err_SupervisorNom" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Apellidos</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.SupervisorApe, new { @class = "form-control", maxlength = "50"}) %>
                        <%: Html.ValidationMessageFor(m => m.SupervisorApe) %>
                            <div id="Err_SupervisorApe" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* Tel&eacute;fono</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.SupervisorTelefono, new { @class = "form-control", maxlength = "20"}) %>
                        <%: Html.ValidationMessageFor(m => m.SupervisorTelefono) %>
                            <div id="Err_SupervisorTelefono" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>
                    <div class="form-group">
                    <label class="col-sm-3 control-label">* E-mail</label>
                    <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.SupervisorEmail, new { @class = "form-control", maxlength = "80"}) %>
                        <%: Html.ValidationMessageFor(m => m.SupervisorEmail) %>
                            <div id="Err_SupervisorEmail" class="field-validation-error"></div>
                        <p></p>
                    </div>
                    </div>

                            <div class="form-group">
                            <label class="col-sm-3 control-label">Adjuntar documentos</label>
                            <div class="col-sm-9">&nbsp;</div>
                            </div>
                            <div class="form-group">
                            <label class="col-sm-3 control-label">* Nro documento</label>
                            <div class="col-sm-9">
                                <%: Html.TextBoxFor(m => m.NroDocumentoAdj, new { @class = "form-control", maxlength = "50"}) %>
                                <%: Html.ValidationMessageFor(m => m.NroDocumentoAdj) %>
                                <p></p>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="col-sm-3 control-label">* Fecha Emision</label>
                            <div class="col-sm-9">
                                <%: Html.TextBoxFor(m => m.FechaEmisionDocAdj, new { @class = "form-control", maxlength = "10", @placeholder="dd/mm/yyyy"}) %>
                                <%: Html.ValidationMessageFor(m => m.FechaEmisionDocAdj) %>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="col-sm-3 control-label">* Descripci&oacute;n</label>
                            <div class="col-sm-9">
                                <%: Html.TextBoxFor(m => m.DescripcionDocAdj, new { @class = "form-control", maxlength = "200"}) %>
                                <%: Html.ValidationMessageFor(m => m.DescripcionDocAdj) %>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="col-sm-3 control-label">* Tipo documento</label>
                            <div class="col-sm-9">
                                    <%: Html.DropDownListFor(m => m.TipoDocmentoDocAdj, lstTiposDoc, "----", new { @class = "form-control" })%>
                                    <%: Html.ValidationMessageFor(m => m.TipoDocmentoDocAdj) %>
                            </div>
                            </div>
                            <div class="form-group">
                            <label class="col-sm-3 control-label">* Archivo</label>
                            <div class="col-sm-9">
                                    <input type="file" name="documentoUpload" id="documentoUpload" />
                                    <span class="field-validation-valid" data-valmsg-for="Err_documentoUpload" data-valmsg-replace="true"></span>
                            </div>
                            </div>
                            <div class="form-group">
                            <div class="col-sm-12 text-center">
                                <button id="btnAdjuntar" name="btnAdjuntar" class="btn btn-default" type="button">Adjuntar</button>
                            </div>
                            <div>&nbsp;</div>
                            <div>&nbsp;</div>
                        <%: Html.HiddenFor(m => m.TipoBotonClick)%>
                    <% } %>
                                <%
                           List<ObrasPublicas.Entities.DocumentoExpedienteTecnicoOP> lstDocumentos = (List<ObrasPublicas.Entities.DocumentoExpedienteTecnicoOP>)Session["SES_DOCUMENTOS_EXPEDIENTE_OP"];
                           if (lstDocumentos != null && lstDocumentos.Count > 0)
                           {
                               %>
                                <table class="table table-condensed">
                                    <thead>
                                    <tr>
                                    <th></th>
                                    <th>Nro. Doc.</th>
                                    <th>Fecha</th>
                                    <th>Descripcion</th>
                                    <th>Tipo</th>
                                    <th>Archivo</th>
                                    </tr>
                                    </thead>
                                <%
                               int intSec = 0;
                               foreach (ObrasPublicas.Entities.DocumentoExpedienteTecnicoOP objDoc in lstDocumentos)
                               { 
                               %>
	                                <tr>
	                                <td></td>
	                                <td><%:objDoc.NroDocumento%></td>
	                                <td><%:objDoc.FechaEmision%></td>
	                                <td><%:objDoc.Descripcion%></td>
	                                <td><%:objDoc.NomTipoDocumento%></td>
	                                <td><a href="<%:objDoc.NomArchivo %>" target="_blank"><%:objDoc.NomArchivo%></a></td>
	                                <td>
                                        <a href="javascript:f_eliminar_archivo(<%:intSec%>);">Eliminar</a></td>
	                                </tr>
                                <%
                                   intSec++;
                               }
                               %>
                                    </table>
                                <%
                           }
                         %>
                                
                        <%: Html.HiddenFor(m => m.IdDocumentoEliminar)%>

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
                        <button id="btnGrabar" class="btn btn-primary" type="button">Grabar</button>
                    </div>
                </div>
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
            $('#DocumentoTipoDocmento').empty();

            var dataToSend = {
            }
            $.ajax({
                url: "/ExpedienteTecnicoOP/Lista_TiposDoc",
                data: dataToSend,
                success: function (result) {
                    $('#DocumentoTipoDocmento').append(
                        $('<option/>', {
                            value: "",
                            text: "(Seleccione)"
                        })
                    );
                    $.each(result, function (index, item) {
                        $('#DocumentoTipoDocmento').append(
                            $('<option/>', {
                                value: item.Value,
                                text: item.Text
                            })
                        );
                    });
                }
            });


            $('.numbersOnly').keyup(function () {
                this.value = this.value.replace(/[^0-9\.]/g, '');
            });

            $(".decimalsOnly").keyup(function () {
                var $this = $(this);
                $this.val($this.val().replace(/[^\d.]/g, ''));
            });
        });

        $("#btnAdjuntar").click(function () {
            $('#TipoBotonClick').val("ADJUNTAR");
            $("#frmUpdate").submit();
        });
        $("#btnRemover").click(function () {
            $("#frmRemoverAdjunto").submit();
            $("#IdDocumentoEliminar").val(IdDocumento);
            $('#TipoBotonClick').val("GRABAR");
        });

        $("#btnGrabar").click(function () {
            $('#TipoBotonClick').val("GRABAR");
            $("#frmUpdate").submit();
        });

        function f_eliminar_archivo(IdDocumento) {
            $("#IdDocumentoEliminar").val(IdDocumento);
            $('#TipoBotonClick').val("REMOVER");
            $("#frmUpdate").submit();
        }

        function formatJSONDate(jsonDate) {
            var newDate = new Date(parseInt(jsonDate.substr(6)));
            return newDate.getDate() + "/" + (newDate.getMonth() + 1) + "/" + newDate.getFullYear();
        }

        // Set up the change event to the textboxes, so when user
        // makes changes, clear the error messages associated to the textbox.
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

        $('input[type=radio][name=TipoEjecutor]').change(function () {
            if (this.value == 'P') {
                $("#divEjecutorPersona").show();
                $("#divEjecutorEmpresa").hide();
                //alert("Allot Thai Gayo Bhai");
            }
            else if (this.value == 'E') {
                $("#divEjecutorPersona").hide();
                $("#divEjecutorEmpresa").show();
                //alert("Transfer Thai Gayo");
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