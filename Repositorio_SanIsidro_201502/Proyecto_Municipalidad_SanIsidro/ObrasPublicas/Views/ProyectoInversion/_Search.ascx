<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ObrasPublicas.Models.ProyectoInversion.SearchProyectoInversionModel>" %>

    <%
        ObrasPublicas.DAL.ProyectoInversion_DAL objProyectoInversion_DAL = new ObrasPublicas.DAL.ProyectoInversion_DAL();

        String strTipoFiltro = null;
        bool bolMostrarOpcTodosEstado = true;

        if (Model.Tipo == "EU" || Model.Tipo == "EC")
        {
            strTipoFiltro = "EXP";
            bolMostrarOpcTodosEstado = false;
        }
        else if (Model.Tipo == "CU" || Model.Tipo == "CC")
        {
            strTipoFiltro = "CRO";
            bolMostrarOpcTodosEstado = false;
        }
        else if (Model.Tipo == "CV")
        {
            strTipoFiltro = "CROCONSULTA";
            bolMostrarOpcTodosEstado = true;
        }
        else if (Model.Tipo == "EMC" || Model.Tipo == "EMU")
        {
            strTipoFiltro = "ENTMAT";
            bolMostrarOpcTodosEstado = false;
        }
        else if (Model.Tipo == "CIO" || Model.Tipo == "UIO")
        {
            strTipoFiltro = "INFO";
            bolMostrarOpcTodosEstado = false;
        }

        var lstEstadoSearch = objProyectoInversion_DAL.ObtieneEstados(strTipoFiltro).Select(x =>
                                                                                  new SelectListItem
                                                                                  {
                                                                                      Value = x.Id,
                                                                                      Text = x.Nombre
                                                                                  }).OrderBy(x => x.Text);
    %>
 <% using (Html.BeginForm("Search2", "ProyectoInversion"))
       { %>
        <%: Html.AntiForgeryToken() %>
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
                        <%if (bolMostrarOpcTodosEstado) { 
                          %>
                        <%: Html.DropDownListFor(m => m.SearchIdEstado, lstEstadoSearch, "(Todos)", new { @class = "form-control" })%>
                    <% } 
                       else { %>                        
                        <%: Html.DropDownListFor(m => m.SearchIdEstado, lstEstadoSearch, new { @class = "form-control" })%>
                        <%
                          } %>
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
                                    <% if (Model.Tipo == "CU" || Model.Tipo == "CV")
                                       { 
                                    %>
                                    <th></th>
                                    <th></th>
                                    <th>C&oacute;digo</th>
                                    <th>Nombre</th>
                                    <th>Fecha emisión</th>
                                    <th>Estado</th>
                                <% }%>
                                 <% 
                                   else{
                                %>
                                    <th></th>
                                    <th></th>
                                    <th>C&oacute;digo</th>
                                    <th>Nombre</th>
                                    <th>Ubicación</th>
                                    <th>Cod. SNIP</th>
                                    <th>Estado</th>
                                <%
                                 }%>
                                  </tr>
                                </thead>
                                <tbody>
                                <%
                                foreach (ObrasPublicas.Entities.ProyectoInversion objProyecto in lstProyectos)
                                { 
                                %>
                                    <tr>
                                        <td>
                                            <%if (Model.Tipo == "EC")
                                              { 
                                             %> 
                                            <a href="/expedientetecnicoop/create?id=<%:objProyecto.IdProyecto %>">Crear</a>                                            
                                            <%  }
                                              else if (Model.Tipo == "EU")
                                              {
                                             %> 
                                            <a href="/expedientetecnicoop/edit?id=<%:objProyecto.IdExpediente %>">Modificar</a>
                                            <%  }
                                              else if (Model.Tipo == "CC")
                                              {
                                             %> 
                                            <a href="/cronogramaejecucionobra/createactividad?p=<%:objProyecto.IdProyecto %>&e=<%:objProyecto.IdExpediente %>&c=0">Crear</a>
                                            <%  }
                                              else if (Model.Tipo == "CU")
                                              {
                                             %> 
                                            <a href="/cronogramaejecucionobra/listado?p=<%:objProyecto.IdProyecto %>&e=<%:objProyecto.IdExpediente %>&c=<%:objProyecto.IdCronograma %>">Modificar</a>
                                            <%  }
                                              else if (Model.Tipo == "CV")
                                              {
                                             %> 
                                            <a href="/cronogramaejecucionobra/listado?p=<%:objProyecto.IdProyecto %>&e=<%:objProyecto.IdExpediente %>&c=<%:objProyecto.IdCronograma %>&v=1">Ver cronograma</a>
                                              <% 
                                                }
                                              else if (Model.Tipo == "EMC" || Model.Tipo == "EMU")
                                              {
                                             %> 
                                            <a href="/entregamaterialop/listado?p=<%:objProyecto.IdProyecto %>">Seleccionar</a>
                                              <%  
                                                } 
                                              else if (Model.Tipo == "CIO")
                                              {
                                             %> 
                                            <a href="/informeobra/create?p=<%:objProyecto.IdProyecto %>&e=<%:objProyecto.IdExpediente %>">Crear</a>
                                               <%  
                                                }
                                              else if (Model.Tipo == "CIU" || Model.Tipo == "UIO")
                                              {
                                             %> 
                                            <a href="/informeobra/listado?p=<%:objProyecto.IdProyecto %>&e=<%:objProyecto.IdExpediente %>">Seleccionar</a>
                                              <%  
                                                }
                                                else{
                                                 %>
                                                <%if (objProyecto.IdEstado == ObrasPublicas.Entities.ProyectoInversion.STR_ID_ESTADO_EN_CONSULTA)
                                                  { 
                                                  %>
                                                <a href="/proyectoinversion/edit?id=<%:objProyecto.IdProyecto %>">Modificar</a>
                                                <%
                                                  }%>

                                              <%  
                                              }
                                                 %>
                                        </td>
                                        <td>
                                            
                                        </td>
                                        <% if (Model.Tipo == "CU") { 
                                        %>
                                            <td><%:objProyecto.IdProyecto %></td>
                                            <td><%:objProyecto.Nombre %></td>
                                            <td><%:objProyecto.FechaEmisionCrono %></td>
                                            <td><%:objProyecto.PlazoEjecucionCrono %></td>
                                            <td><%:objProyecto.NomEstado %></td>
                                        <%                                           
                                           } else{
                                           %>
                                        <td><%:objProyecto.IdProyecto %></td>
                                        <td><%:objProyecto.Nombre %></td>
                                        <td><%:objProyecto.TipoVia %> <%:objProyecto.NomVia %> <%:objProyecto.Ubicacion %></td>
                                        <td><%:objProyecto.CodSNIP %></td>
                                        <td><%:objProyecto.NomEstado %></td>
                                        <%
                                           } %>
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
            <%: Html.HiddenFor(m => m.Tipo)%>
    <% } %>