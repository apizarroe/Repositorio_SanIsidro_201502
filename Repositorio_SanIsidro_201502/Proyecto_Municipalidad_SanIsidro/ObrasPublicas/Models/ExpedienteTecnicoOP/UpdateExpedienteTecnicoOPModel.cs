using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObrasPublicas.Models.ExpedienteTecnicoOP
{
    public class UpdateExpedienteTecnicoOPModel : IValidatableObject
    {
        public int IdProyecto { get; set; }
        public int IdExpediente { get; set; }
        public String NomProyecto { get; set; }
        public String Descripcion { get; set; }
        public String Especificaciones { get; set; }
        public Decimal ValorReferencial { get; set; }

        public String TipoEjecutor { get; set; }

        public String EjecutorNom { get; set; }
        public String EjecutorApe { get; set; }
        public String EjecutorRazonSocial { get; set; }

        public String ContactoNom { get; set; }
        public String ContactoApe { get; set; }
        public String ContactoTelefono { get; set; }
        public String ContactoEmail { get; set; }
        public String ContactoDireccion { get; set; }

        public String SupervisorNom { get; set; }
        public String SupervisorApe { get; set; }
        public String SupervisorTelefono { get; set; }
        public String SupervisorEmail { get; set; }
        public String TipoBotonClick { get; set; }
        public String IdDocumentoEliminar { get; set; }

        public String NroDocumentoAdj { get; set; }
        public String FechaEmisionDocAdj { get; set; }
        public String DescripcionDocAdj { get; set; }
        public String TipoDocmentoDocAdj { get; set; }

        public int IdEjecutor { get; set; }
        public int IdContacto { get; set; }
        public int IdSupervisor { get; set; }

        //[DataType(DataType.Upload)]
        //public HttpPostedFileBase FileUpload { get; set; }

        public List<DocumentoExpTecOPModel> ListaDocumentos { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            List<ValidationResult> lstValidations = new List<ValidationResult>();
            if (this.TipoBotonClick == "ADJUNTAR")
            {
                DateTime datFecTmp;
                if (String.IsNullOrWhiteSpace(this.NroDocumentoAdj))
                {
                    lstValidations.Add(new ValidationResult("El campo Nro. Documento es obligatorio", new[] { "NroDocumentoAdj" }));
                }
                if (String.IsNullOrWhiteSpace(this.FechaEmisionDocAdj))
                {
                    lstValidations.Add(new ValidationResult("El campo Fecha de emisión es obligatorio", new[] { "FechaEmisionDocAdj" }));
                }
                else if (!DateTime.TryParse(this.FechaEmisionDocAdj, out datFecTmp))
                {
                    lstValidations.Add(new ValidationResult("El campo Fecha de emisión es incorrecta", new[] { "FechaEmisionDocAdj" }));
                }
                else if (Convert.ToDateTime(this.FechaEmisionDocAdj) > DateTime.Now)
                {
                    lstValidations.Add(new ValidationResult("El campo Fecha de emisión debe ser menor o igual a la fecha actual", new[] { "FechaEmisionDocAdj" }));
                }
                if (String.IsNullOrWhiteSpace(this.DescripcionDocAdj))
                {
                    lstValidations.Add(new ValidationResult("El campo Descripción es obligatorio", new[] { "DescripcionDocAdj" }));
                }
                if (String.IsNullOrWhiteSpace(this.TipoDocmentoDocAdj))
                {
                    lstValidations.Add(new ValidationResult("El campo Tipo Documento es obligatorio", new[] { "TipoDocmentoDocAdj" }));
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(this.Descripcion))
                {
                    lstValidations.Add(new ValidationResult("El campo Descripcion es obligatorio", new[] { "Descripcion" }));
                }
                if (String.IsNullOrWhiteSpace(this.Especificaciones))
                {
                    lstValidations.Add(new ValidationResult("El campo Especificaciones es obligatorio", new[] { "Especificaciones" }));
                }
                if (this.ValorReferencial == 0)
                {
                    lstValidations.Add(new ValidationResult("El campo Valor Referencial debe ser mayor a cero", new[] { "ValorReferencial" }));
                }
                if (String.IsNullOrWhiteSpace(this.TipoEjecutor))
                {
                    lstValidations.Add(new ValidationResult("El campo Tipo de Ejecutor es obligatorio", new[] { "TipoEjecutor" }));
                }
                if (this.TipoEjecutor == "P")
                {
                    if (String.IsNullOrWhiteSpace(this.EjecutorNom))
                    {
                        lstValidations.Add(new ValidationResult("El campo Nombre de Ejecutor es obligatorio", new[] { "EjecutorNom" }));
                    }
                    if (String.IsNullOrWhiteSpace(this.EjecutorApe))
                    {
                        lstValidations.Add(new ValidationResult("El campo Apellido de Ejecutor es obligatorio", new[] { "EjecutorApe" }));
                    }
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(this.EjecutorRazonSocial))
                    {
                        lstValidations.Add(new ValidationResult("El campo Razón Social de Ejecutor es obligatorio", new[] { "EjecutorRazonSocial" }));
                    }
                }
                if (String.IsNullOrWhiteSpace(this.ContactoNom))
                {
                    lstValidations.Add(new ValidationResult("El campo Nombre de Contacto es obligatorio", new[] { "ContactoNom" }));
                }
                if (String.IsNullOrWhiteSpace(this.ContactoApe))
                {
                    lstValidations.Add(new ValidationResult("El campo Apellido de Contacto es obligatorio", new[] { "ContactoApe" }));
                }
                if (String.IsNullOrWhiteSpace(this.ContactoTelefono))
                {
                    lstValidations.Add(new ValidationResult("El campo Teléfono de Contacto es obligatorio", new[] { "ContactoTelefono" }));
                }
                if (String.IsNullOrWhiteSpace(this.ContactoEmail))
                {
                    lstValidations.Add(new ValidationResult("El campo Email de Contacto es obligatorio", new[] { "ContactoEmail" }));
                }
                else if (!Helpers.Validations.EmailValido(this.ContactoEmail))
                {
                    lstValidations.Add(new ValidationResult("El campo Email de Contacto es incorrecto", new[] { "ContactoEmail" }));
                }
                if (String.IsNullOrWhiteSpace(this.ContactoDireccion))
                {
                    lstValidations.Add(new ValidationResult("El campo Dirección de Contacto es obligatorio", new[] { "ContactoDireccion" }));
                }
                if (String.IsNullOrWhiteSpace(this.SupervisorNom))
                {
                    lstValidations.Add(new ValidationResult("El campo Nombre de Supervisor es obligatorio", new[] { "SupervisorNom" }));
                }
                if (String.IsNullOrWhiteSpace(this.SupervisorApe))
                {
                    lstValidations.Add(new ValidationResult("El campo Apellido de Supervisor es obligatorio", new[] { "SupervisorApe" }));
                }
                if (String.IsNullOrWhiteSpace(this.SupervisorTelefono))
                {
                    lstValidations.Add(new ValidationResult("El campo Teléfono de Supervisor es obligatorio", new[] { "SupervisorTelefono" }));
                }
                if (String.IsNullOrWhiteSpace(this.SupervisorEmail))
                {
                    lstValidations.Add(new ValidationResult("El campo Email de Supervisor es obligatorio", new[] { "SupervisorEmail" }));
                }
                else if (!Helpers.Validations.EmailValido(this.SupervisorEmail))
                {
                    lstValidations.Add(new ValidationResult("El campo Email de Supervisor es incorrecto", new[] { "SupervisorEmail" }));
                }
            }

            return lstValidations;
        }
    }
}