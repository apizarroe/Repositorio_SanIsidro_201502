﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Catastro.Master" Inherits="System.Web.Mvc.ViewPage<Entidades.CT_SOLICITUD>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

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
<p>
    <%: Html.ActionLink("Edit", "Edit", new { /* id=Model.PrimaryKey */ }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
