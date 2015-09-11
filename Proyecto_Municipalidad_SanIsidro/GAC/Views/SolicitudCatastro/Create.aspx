<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Catastro.Master" Inherits="System.Web.Mvc.ViewPage<Dominio.Core.Entities.ModeloGestionCatastral.CT_SOLICITUD>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Solicitud Catastral
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <h1>Crear Solicitud Catastral
            
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">Lista de Solicitud Catastral</li>

        </ol>
    </section>


    <section class="content">
        <div class="row">
            <div class='col-md-12'>
                <div class='box box-primary'>
                    <div class="box-header">
                        <h3 class="box-title">Informacion de Catastro</h3>
                    </div>
                    <% using (Html.BeginForm())
                       { %>
                    <%: Html.AntiForgeryToken() %>
                    <%: Html.ValidationSummary(true) %>

                    <div class="box-body">


                        <div class="form-group">
                            <%: Html.LabelFor(model => model.int_IdSolicitud) %>
                            <%: Html.TextBoxFor(model => model.int_IdSolicitud,null, new { @class = "form-control" ,@placeholder="Codigo Solicitud"  })%>
                            <%: Html.ValidationMessageFor(model => model.int_IdSolicitud) %>
                        </div>
                        <div class="form-group">
                            <%: Html.LabelFor(model => model.var_NroSolicitud) %>
                            <%: Html.TextBoxFor(model => model.var_NroSolicitud,null, new { @class = "form-control" ,@placeholder="Nro Solicitud"  })%>
                            <%: Html.ValidationMessageFor(model => model.var_NroSolicitud) %>
                        </div>
                        <div class="form-group">
                            <%: Html.LabelFor(model => model.dtm_FechaEmision) %>

                            <%: Html.TextBoxFor(model => model.dtm_FechaEmision,"{0:dd/MM/yyyy}",new { @class = "form-control" ,@placeholder="Fecha Emision",@disabled="true" }) %>
                            <%: Html.ValidationMessageFor(model => model.dtm_FechaEmision) %>
                        </div>

                        <div class="form-group">
                            <%: Html.LabelFor(model => model.CT_TIPOSOLICITUD.int_IdTipoSolicitud) %>


                            <%:Html.DropDownList("int_IdTipoSolicitud",null, new { @class = "form-control"}) %>

                            <%: Html.ValidationMessageFor(model => model.CT_TIPOSOLICITUD.int_IdTipoSolicitud) %>
                        </div>

                        <div class="form-group">
                            <%: Html.LabelFor(model => model.var_Descripcion) %>

                            <%: Html.TextAreaFor(model => model.var_Descripcion,3,6,new { @class = "form-control" ,@placeholder="Descripcion.." }) %>
                            <%: Html.ValidationMessageFor(model => model.var_Descripcion) %>
                        </div>
                    </div>
                    <div class="box-footer">
                        <button type="submit" class="btn btn-success">Crear Nueva Solicitud</button>
                            <%: Html.ActionLink("Cancelar", "Index",null,new {@class="btn btn-primary"}) %>
                    </div>


                    <% } %>
                </div>
            </div>
        </div>
    </section>
    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>
