<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Entidades.utb_GCT_Zona>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Index</h2>

<p>
    <%: Html.ActionLink("Create New", "Create") %>
</p>
<table>
    <tr>
        <th>
            <%: Html.DisplayNameFor(model => model.int_IdZona) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.var_CodigoZona) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.var_Nombre) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.dbl_Area) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.dtm_FechaCreacion) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.dtm_FechaActualizacion) %>
        </th>
        <th>
            <%: Html.DisplayNameFor(model => model.dtm_FechaRegistro) %>
        </th>
        <th></th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.DisplayFor(modelItem => item.int_IdZona) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.var_CodigoZona) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.var_Nombre) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.dbl_Area) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.dtm_FechaCreacion) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.dtm_FechaActualizacion) %>
        </td>
        <td>
            <%: Html.DisplayFor(modelItem => item.dtm_FechaRegistro) %>
        </td>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
            <%: Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }) %> |
            <%: Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ }) %>
        </td>
    </tr>
<% } %>

</table>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
