<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GAC.Models.LoginModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Municipalidad de San Isidro
</asp:Content>

<asp:Content ID="indexFeatured" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ingreso al sistema</h2>

    <section id="loginForm" class="content">

        <% using (Html.BeginForm())
           {%>
        <%: Html.AntiForgeryToken() %>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend></legend>
            <div class="row">
                <div class='col-md-12'>
                    <div class="box-body">
                        <div class="form-group">
                            <label>Usuario:</label>
                            <%: Html.TextBoxFor(m => m.UserName,null, new { @class = "form-control" ,@placeholder="Usuario"  }) %>
                            <%: Html.ValidationMessageFor(m => m.UserName) %>
                        </div>
                        <div class="form-group">
                            <label>Contraseña:</label>
                            <%: Html.PasswordFor(m => m.Password, new { @class = "form-control" ,@placeholder="Contraseña"  }) %>
                            <%: Html.ValidationMessageFor(m => m.Password) %>
                        </div>
                        <div class="form-group">
                            <input type="submit" id="btnIngresar" title="Ingresar" value="Ingresar" class="btn btn-success" />
                        </div>
                    </div>
                </div>
            </div>
        </fieldset>

        <% } %>
    </section>


</asp:Content>
