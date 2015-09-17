<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Municipalidad de San Isidro
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Ingreso al sistema</h2>

    <fieldset>
        <legend></legend>
        <table style="width:40%;">
            <tr>
                <td class="tdForm">
                    <p class="col-sm-3 control-label">Usuario:</p> 
                </td>
                <td class="tdForm">
                    <input type="text" name="mypass" id="txtUsuario" class="form-control"> 
                </td>
                <td colspan="2">
                    
                </td>
            </tr>
            <tr>
                <td>
                    <p class="col-sm-3 control-label">Contraseña:</p> 
                </td>
                <td>
                    <input type="password" name="mypass" id="txtPassword" class="form-control"> 
                </td>
                <td colspan="2">
                    
                </td>
            </tr>
            <tr>
                <td><br /></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:Center;">
                    <% using (Html.BeginForm("Index2", "Home")) %>
                    <%  {%>
                    <input type="submit" id="btnIngresar" title="Ingresar" value="Ingresar" class="btn btn-default"/>
                    <% }%>
                </td>
                <td></td>
                <td></td>
            </tr>
        </table>
    </fieldset>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
</asp:Content>
