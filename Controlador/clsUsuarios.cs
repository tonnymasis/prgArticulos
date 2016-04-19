using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;
using System.Data.SqlClient;

namespace Controlador
{
    public class clsUsuarios
    {
        #region Atributos
        //Permite las sentencias del SQL transac
        private string strSentencia;
        //Permite enviar la ejecucion de la sentencia almodelo en la clase conexion
        clsConexionSQL conexion = new clsConexionSQL();
        #endregion

        #region Metodos
        //Metodo para accesar al sistema
        public SqlDataReader mConsultarUsuario(Modelo.clsConexionSQL cone, Modelo.clsEntidadUsuario pEntidadUsuario)
        {
            strSentencia = "Select * from tbUsuarios where codigo='" + pEntidadUsuario.getCodigo() + "' and clave='" + pEntidadUsuario.getClave() + "'";
            return conexion.mSeleccionar(strSentencia, cone);
        }
        #endregion

    }
}
