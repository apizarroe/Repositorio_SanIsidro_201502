<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Catastro.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Dominio.Core.Entities.ModeloGestionCatastral.CT_PROPUESTAINSPECCION>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <h1>Lista de Propuesta de Inspeccion
            
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Lista de Propuesta de Inspeccion</li>

        </ol>
    </section>



    <section class="content">
        <div class="row">
            <div class='col-md-12'>
                <div class='box box-info'>
                    <div class='box-header'>
                        <%: Html.ActionLink("Crear Nueva Inspección", "Create",null,new {@class="btn btn-primary"}) %>
                    </div>
                    <div class='box-body pad'>

                        <table class="table table-hover">
                            <tr>
                                <th>
                                    Nro.
                                </th>
                                <th>
                                    Nro. Solicitud
                                </th>
                                
                                <th>
                                    Descripción
                                </th>
                                <th>
                                    Fecha de Documento
                                </th>
                                <th>
                                    Fecha de Registro
                                </th>
                                <th>
                                    Estado
                                </th>
                                <th></th>
                            </tr>

                            <% foreach (var item in Model)
                               { %>
                            <tr>
                                <td>
                                    <%: Html.DisplayFor(modelItem => item.int_IdPropuestaInspeccion) %>
                                </td>
                                <td>
                                    <%: Html.DisplayFor(modelItem => item.CT_SOLICITUD.var_NroSolicitud) %>
                                </td>
                                
                                <td>
                                    <%: Html.DisplayFor(modelItem => item.var_Descripcion) %>
                                </td>
                                <td>
                                    <%: Html.DisplayFor(modelItem => item.dtm_FechaDocumento) %>
                                </td>
                                <td>
                                    <%: Html.DisplayFor(modelItem => item.dtm_FechaRegistro) %>
                                </td>
                                                                <td>
                                    <%if(item.int_Estado==1) %>
                                    <% { %>
                                        Pendiente
                                    <% } %>
                                    <%if(item.int_Estado==2) %>
                                    <% { %>
                                        Aprobada
                                    <% } %>
                                    <%if(item.int_Estado==3) %>
                                    <% { %>
                                        Observada
                                    <% } %>
                                </td>
                                <td>
                                    <%if (item.int_Estado == 1 || item.int_Estado == 3) %>
                                    <% { %>
                                    <%: Html.ActionLink("Edit","Create", new {  id=item.int_IdPropuestaInspeccion  }) %> |
                                    
                                        
                                        
                                    <% } %>
                                    <%: Html.ActionLink("Details", "Details", new { id=item.int_IdPropuestaInspeccion }) %> 
                                </td>
                            </tr>
                            <% } %>
                        </table>


                    </div>
                </div>
            </div>
        </div>
    </section>





</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
