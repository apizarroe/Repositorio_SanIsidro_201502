<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Entidades.CT_PROPUESTAINSPECCION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>CT_PROPUESTAINSPECCION</legend>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.int_IdPropuestaInspeccion) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.int_IdPropuestaInspeccion) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.CT_SOLICITUD.var_NroSolicitud) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.CT_SOLICITUD.var_NroSolicitud) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.var_NroPropuesta) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.var_NroPropuesta) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.var_Descripcion) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.var_Descripcion) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.dtm_FechaDocumento) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.dtm_FechaDocumento) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.dtm_FechaRegistro) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.dtm_FechaRegistro) %>
    </div>
</fieldset>
<% using (Html.BeginForm()) { %>
    <p>
        <input type="submit" value="Delete" /> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
<% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
