

namespace Util.GSM
{
    public class Global
    {
        public enum EstadoInspeccion : int
        {
            Programado = 1,
            Inspeccionado = 2,
            Cancelado = 3,
            Informado = 4
        }

        public enum EstadoInformeInspeccion : int
        {
            Activo = 1,
            Inactivo = 2,
            Informado = 3
        }

        public enum EstadoInformeServicio : int
        {
            Pendiente = 1,
            Aprobado = 2,
            Desaprobado = 3,
            Anulado = 4
        }
    }
}
