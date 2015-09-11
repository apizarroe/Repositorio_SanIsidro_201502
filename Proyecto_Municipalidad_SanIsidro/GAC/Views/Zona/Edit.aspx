<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Entidades.utb_GCT_Zona>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Edit</h2>

<% using (Html.BeginForm()) { %>
    <%: Html.AntiForgeryToken() %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>utb_GCT_Zona</legend>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.int_IdZona) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.int_IdZona) %>
            <%: Html.ValidationMessageFor(model => model.int_IdZona) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.var_CodigoZona) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.var_CodigoZona) %>
            <%: Html.ValidationMessageFor(model => model.var_CodigoZona) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.var_Nombre) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.var_Nombre) %>
            <%: Html.ValidationMessageFor(model => model.var_Nombre) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.dbl_Area) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.dbl_Area) %>
            <%: Html.ValidationMessageFor(model => model.dbl_Area) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.dtm_FechaCreacion) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.dtm_FechaCreacion) %>
            <%: Html.ValidationMessageFor(model => model.dtm_FechaCreacion) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.dtm_FechaActualizacion) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.dtm_FechaActualizacion) %>
            <%: Html.ValidationMessageFor(model => model.dtm_FechaActualizacion) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.dtm_FechaRegistro) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.dtm_FechaRegistro) %>
            <%: Html.ValidationMessageFor(model => model.dtm_FechaRegistro) %>
        </div>

        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
<% } %>

<div>
    <%: Html.ActionLink("Back to List", "Index") %>
</div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
    <%: Scripts.Render("~/bundles/jqueryval") %>
</asp:Content>
