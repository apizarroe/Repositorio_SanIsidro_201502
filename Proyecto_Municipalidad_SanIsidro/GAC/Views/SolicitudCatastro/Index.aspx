<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Catastro.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Dominio.Core.Entities.ModeloGestionCatastral.CT_SOLICITUD>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Solicitud Catastral
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <h1>Lista de Solicitud Catastral
            
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Lista de Solicitud Catastral</li>

        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class='col-md-12'>
                <div class='box box-info'>
                    <div class='box-header'>
                        <%: Html.ActionLink("Crear Nueva Solicitud", "Create",null,new {@class="btn btn-primary"}) %>
                    </div>
                    <div class='box-body pad'>

                        <table class="table table-hover">
                            <tr>
                                <th>
                                    Nro. Solicitud
                                </th>
                                <th>
                                    Fecha Emisión
                                </th>
                                <th>
                                    Tipo Solicitud
                                </th>
                                <th>
                                    Descripción
                                </th>
                                <th></th>
                            </tr>

                            <% if(Model !=null){ foreach (var item in Model) { %>
                            <tr>
                                <td>
                                    <%: Html.DisplayFor(modelItem => item.var_NroSolicitud) %>
                                </td>
                                <td>
                                    <%: Html.DisplayFor(modelItem => item.dtm_FechaEmision) %>
                                </td>
                                <td>

                                  <%: Html.DisplayFor(modelItem => item.CT_TIPOSOLICITUD.var_TipoSolicitud) %>

                                    
                                </td>
                                <td>
                                    <%: Html.DisplayFor(modelItem => item.var_Descripcion) %>
                                </td>
                                <td>
                                    <%: Html.ActionLink("Modificar", "Edit", new { id=item.int_IdSolicitud }) %> 
                                </td>
                            </tr>
                            <% }
                               } %>
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
