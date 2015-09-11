<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ObrasPublicas.Models.CronogramaEjecucionObra.ActividadEjecucionObraModel>" %>
<%@ Import Namespace="GAC.Helpers"%>

<%
    Infraestructura.Data.SQL.Empleado_DAL objEmpleado_DAL = new Infraestructura.Data.SQL.Empleado_DAL();

    var lstEmpleado = objEmpleado_DAL.ObtieneXArea(1).Select(x =>
                                                                new SelectListItem
                                                                {
                                                                    Value = x.IdEmpleado.ToString(),
                                                                    Text = x.Apellido + " " + x.Nombre
                                                                }).OrderBy(x => x.Text);
     %>

<div id="editRow" class="editRow row">
    <% 
         using (Html.BeginAjaxContentValidation("form0"))
    {
        using (Html.BeginCollectionItem("ListaActividades"))
       { %>
    <div class="col-md-3">Actividad<%= Html.TextBoxFor(x => x.Nombre, new { size = 12, @class = "form-control input-sm", maxlength="45"}) %>
        <%: Html.ValidationMessageFor(m => m.Nombre) %>
    </div>
    <div class="col-md-1">
        Inicio Prog.
        <%= Html.TextBoxFor(x => x.FechaIniProg, new { size = 12, @class = "form-control input-sm", @style="font-size: 10px;", @placeholder="dd/mm/yyyy", maxlength="10" }) %>
        <%: Html.ValidationMessageFor(m => m.FechaIniProg) %>
    </div>
    <div class="col-md-1">Fin Prog.<%= Html.TextBoxFor(x => x.FechaFinProg, new { size = 12, @class = "form-control input-sm", @style="font-size: 10px;", @placeholder="dd/mm/yyyy", maxlength="10"  }) %>
                <%: Html.ValidationMessageFor(m => m.FechaFinProg) %>
    </div>
    <div class="col-md-1">Inicio Ejec.<%= Html.TextBoxFor(x => x.FechaIniEjec, new { size = 12, @class = "form-control input-sm", @style="font-size: 10px;", @placeholder="dd/mm/yyyy", maxlength="10"  }) %>
        <%: Html.ValidationMessageFor(m => m.FechaIniEjec) %>
    </div>
    <div class="col-md-1">Fin Ejec.<%= Html.TextBoxFor(x => x.FechaFinEjec, new { size = 12, @class = "form-control input-sm", @style="font-size: 10px;", @placeholder="dd/mm/yyyy", maxlength="10"  }) %>
                <%: Html.ValidationMessageFor(m => m.FechaFinEjec) %>
    </div>
    <div class="col-md-2">Responsable 
                        <%: Html.DropDownListFor(m => m.IdEmpleado, lstEmpleado, "(Seleccione)", new { @class = "form-control input-sm" })%>
                <%: Html.ValidationMessageFor(m => m.IdEmpleado) %>
        <%: Html.HiddenFor(m => m.IdEmpleado)%>
    </div>
    <div class="col-md-1">Costo <%= Html.TextBoxFor(x => x.Costo, new { size = 6, @class = "form-control input-sm", maxlength="7" }) %>
                        <%: Html.ValidationMessageFor(m => m.Costo) %>
    </div>
    <div class="col-md-1">Cantidad <%= Html.TextBoxFor(x => x.CantidadRRHH, new { size = 6, @class = "form-control input-sm", maxlength="6" }) %>
    <%: Html.ValidationMessageFor(m => m.CantidadRRHH) %>
    </div>
    <div class="col-md-1"><br /><a href="#" class="deleteRow">Eliminar</a></div>
    <% }
    } %>
</div>