<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Entidades.utb_GCT_Zona>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>utb_GCT_Zona</legend>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.int_IdZona) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.int_IdZona) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.var_CodigoZona) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.var_CodigoZona) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.var_Nombre) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.var_Nombre) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.dbl_Area) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.dbl_Area) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.dtm_FechaCreacion) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.dtm_FechaCreacion) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.dtm_FechaActualizacion) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.dtm_FechaActualizacion) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.dtm_FechaRegistro) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.dtm_FechaRegistro) %>
    </div>
</fieldset>
<p>
    <%: Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
