<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site_Catastro.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    PlanoCatastral
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
          <h1>Emitir Plano Catastral
            
        </h1>
        <section class="content">
        <div class="row">
            <div class='col-md-12'>
                <!-- Form Element sizes -->
                <div class="box box-success">
                    <div class="box-header">
                        <h3 class="box-title"></h3>
                    </div>
                    <div class="box-body">
                        <!-- Custom Tabs (Pulled to the right) -->
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs pull-right" id="myTab">
                                <li class="active"><a href="#tab_1-1" data-toggle="tab">Zona</a></li>
                                <li><a href="#tab_2-2" data-toggle="tab">Manzana</a></li>
                                <li><a href="#tab_3-2" data-toggle="tab">Lote</a></li>
                                
                                <li class="pull-left header"><i class="fa fa-th"></i>

                                    <label id="lblTituloTab">Zonas</label>
                                </li>
                            </ul>
                            <div class="tab-content">
                                <div class="tab-pane active" id="tab_1-1">
                                    <table class="table table-hover" id="tblZonas">
                                        <tr>
                                            <th>Código
                                            </th>
                                            <th>Nombre
                                            </th>
                                            <th></th>
                                            <th></th>
                                        </tr>

                                    </table>


                                    <div class="box-footer">
                                        
                                        <button type="button" class="btn btn-success" id="btnimprimirPlanoZona">Imprimir Plano</button>
                                        

                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                                <div class="tab-pane" id="tab_2-2">
                                    <div class="tab-pane active" id="Div1">
                                        <div class="form-group">
                                            <label>
                                                Zona
                                            </label>
                                            <select id="ddlZona1" class="ddlZona">
                                                <option>Zona A </option>
                                                <option>Zona B </option>
                                                <option>Zona C </option>
                                                <option>Zona D </option>
                                                <option>Zona N </option>

                                            </select>
                                        </div>
                                        <table class="table table-hover" id="tblManzanas">
                                            <tr>
                                                <th>Código
                                                </th>
                                                <th>Nombre
                                                </th>
                                                <th></th>
                                                <th></th>
                                            </tr>

                                        </table>


                                        <div class="box-footer">
                                            <button type="button" class="btn btn-success" id="btnImprimirPlanoManzana">Imprimir Plano</button>

                                        </div>
                                    </div>
                                </div>
                                <!-- /.tab-pane -->
                                <div class="tab-pane" id="tab_3-2">
                                    <div class="tab-pane active" id="Div2">
                                        <div class="form-group">
                                            <label>
                                                Zona
                                            </label>
                                            <select id="Select3" class="ddlZona">
                                                <option>Zona A </option>
                                                <option>Zona B </option>
                                                <option>Zona C </option>
                                                <option>Zona D </option>
                                                <option>Zona N </option>

                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label>
                                                Manzana
                                            </label>
                                            <select id="Select5" class="ddlManzana">
                                                <option>Zona A </option>
                                                <option>Zona B </option>
                                                <option>Zona C </option>
                                                <option>Zona D </option>
                                                <option>Zona N </option>

                                            </select>
                                        </div>


                                        <table class="table table-hover" id="tblLotes">
                                            <tr>
                                                <th>Código
                                                </th>
                                                <th>Nombre
                                                </th>
                                                <th></th>
                                                <th></th>
                                            </tr>

                                        </table>


                                        <div class="box-footer">
                                        <button type="button" class="btn btn-success" id="btnImprimirplanoLote">Imprimir Plano</button>

                                        </div>
                                    </div>
                                </div>
                          
                                <!-- /.tab-pane -->
                            </div>
                            <!-- /.tab-content -->
                        </div>
                        <!-- nav-tabs-custom -->
                        <!-- Button trigger modal -->
                    </div>
                    <!-- /.box-body -->
                </div>
            </div>
        </div>
    </section>
    <!-- /.box -->

    <input type="hidden" id="int_IdSolicitud"  value="-1"/>
      <!-- Modal -->
   
    
    <input type="hidden" id="int_IdZona" />
    <input type="hidden" id="int_IdManzana" />
    <input type="hidden" id="int_IdLote" />

   
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ScriptsSection" runat="server">
       
    <%: Scripts.Render("~/Scripts/jstemplate/InformacionCatastral.js") %>
      <script type="text/javascript">
          $(document).ready(function () {
              ObtenerZona();


              $('#btnimprimirPlanoZona').click(function () {
                  ImpriPlanoZona();

              });

              $('#btnImprimirPlanoManzana').click(function () {
                  ImpriPlanoManzana();

              });
              $('#btnImprimirplanoLote').click(function () {
                  ImpriPlanoLote();

              });
          })


          function ImpriPlanoZona() {

              var validar = "";
              $("#tblZonas input:radio").each(function () {
                  var $this = $(this);

                  if (this.checked) {
                      validar = "1";
                      var src = $this.attr('id');
                      var campos = src.split("@");
                      console.log(src);
                      popitup('/PlanoCatastral/ImprimirPlano/?id=' + src + '&opt=1');
                  
                  
                  }
              });
              if (validar=="")
              {
                  alert('Debe Seleccionar una Zona');
              }
          }
          function ImpriPlanoManzana() {
              var validar = "";
              $("#tblManzanas input:radio").each(function () {
                  var $this = $(this);

                  if (this.checked) {
                      validar = "1";
                      var src = $this.attr('id');
                      var campos = src.split("@");
                      console.log(src);
                      popitup('/PlanoCatastral/ImprimirPlano/?id=' + src + '&opt=2');
                  }

              });
              if (validar == "") {
                  alert('Debe Seleccionar una Manzana');
              }
          }

          function ImpriPlanoLote() {
              var validar = "";
              $("#tblLotes input:radio").each(function () {
                  var $this = $(this);

                  if (this.checked) {
                      validar = "1";
                      var src = $this.attr('id');
                      var campos = src.split("@");
                      console.log(src);
                      popitup('/PlanoCatastral/ImprimirPlano/?id=' + src + '&opt=3');
                  }

              });
              if (validar == "") {
                  alert('Debe Seleccionar un Lote');
              }
          }

          function popitup(url) {
              newwindow = window.open(url, 'name', 'height=600,width=800');
              if (window.focus) { newwindow.focus() }
              return false;
          }

      
        </script>

</asp:Content>
