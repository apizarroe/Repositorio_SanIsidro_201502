﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Entidades.CT_SOLICITUD>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>tbSolicitudCatastro</legend>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.int_IdSolicitud) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.int_IdSolicitud) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.dtm_FechaEmision) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.dtm_FechaEmision) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.int_IdTipoSolicitud) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.int_IdTipoSolicitud) %>
    </div>

    <div class="display-label">
        <%: Html.DisplayNameFor(model => model.var_Descripcion) %>
    </div>
    <div class="display-field">
        <%: Html.DisplayFor(model => model.var_Descripcion) %>
    </div>
</fieldset>
<% using (Html.BeginForm()) { %>
    <%: Html.AntiForgeryToken() %>
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
