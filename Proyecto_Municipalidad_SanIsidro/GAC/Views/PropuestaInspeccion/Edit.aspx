<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<Dominio.Core.Entities.ModeloGestionCatastral.CT_PROPUESTAINSPECCION>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Edit</h2>

<% using (Html.BeginForm()) { %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>CT_PROPUESTAINSPECCION</legend>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.int_IdPropuestaInspeccion) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.int_IdPropuestaInspeccion) %>
            <%: Html.ValidationMessageFor(model => model.int_IdPropuestaInspeccion) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.int_IdSolicitud, "CT_SOLICITUD") %>
        </div>
        <div class="editor-field">
            <%: Html.DropDownList("int_IdSolicitud", String.Empty) %>
            <%: Html.ValidationMessageFor(model => model.int_IdSolicitud) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.var_NroPropuesta) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.var_NroPropuesta) %>
            <%: Html.ValidationMessageFor(model => model.var_NroPropuesta) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.var_Descripcion) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.var_Descripcion) %>
            <%: Html.ValidationMessageFor(model => model.var_Descripcion) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.dtm_FechaDocumento) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.dtm_FechaDocumento) %>
            <%: Html.ValidationMessageFor(model => model.dtm_FechaDocumento) %>
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
