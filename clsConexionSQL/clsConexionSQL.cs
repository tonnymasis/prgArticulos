using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;//Retornar de system de windows lo que es el nombre de la maquina
using System.Data.SqlClient; // Acceso a la base de datos (IMEC)

namespace Modelo
{
    public class clsConexionSQL
    {
        //Area de la declaracion de las variables
        #region Atributos
        private string codigo;
        private string clave;
        private string perfil;
        private string baseDatos;

        private SqlConnection conexion; //Guarda la cadena de conexion del usuario cn la BD
        private SqlCommand comando; //Permite ejecutar los IMEC
        #endregion

        //Establecemos el metodo inicial
        #region Constructor
        public clsConexionSQL()
        {
            this.codigo = "";
            this.clave = "";
            this.perfil = "";
            this.baseDatos = "BDEstudiantes";
        }
        #endregion

        //Propiuedades de lectura y escritura
        #region GetySet
        public void setCodigo(String codigo) {
            this.codigo = codigo.Trim();
        }
        public String getCodigo() {
            return this.codigo;
        }

        public void setClave(String clave)
        {
            this.clave = clave.Trim();
        }
        public String getClave()
        {
            return this.clave;
        }

        public void setPerfil(String perfil)
        {
            this.perfil = perfil.Trim();
        }
        public String getPerfil()
        {
            return this.perfil;
        }
        #endregion

        //Metodos para la conexion de la bd
        #region Metodos

        //Este metodo permite ejecutar los select 
        public SqlDataReader mSeleccionar(string strSentencia, clsConexionSQL cone)
        {
            try
            {
                if (mConectar(cone))
                {
                    comando = new SqlCommand(strSentencia, conexion);
                    comando.CommandType = System.Data.CommandType.Text;
                    return comando.ExecuteReader();//El executeReader ejecuta solo el select
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        //Este metodo permitira ejecutr los insert, update y delete 
        public Boolean mEjecutar(string strSentencia, clsConexionSQL cone)
        {
            try
            {
                if (mConectar(cone))
                {
                    comando = new SqlCommand(strSentencia, conexion);
                    comando.ExecuteNonQuery();
                    return true;
                }else
                    return false;
            }catch
            {
                return false;
            }
        }

        //Este metodo nos permite abrir y conectarnos con la base de datos
        public Boolean mConectar(clsConexionSQL cone)
        {
            try
            {
                conexion = new SqlConnection();
                conexion.ConnectionString = "user id='" + cone.getCodigo() + "'; password='" + cone.getClave() + "'; Data Source='" + mNomServidor() + "'; Initial Catalog='" + this.baseDatos + "'";
                conexion.Open();
                return true;
            }catch
            {
                return false;
            }
        }

        //Este metodo obtiene el nombre de la maquina de w7
        public string mNomServidor()
        {
            return Dns.GetHostName();
        }


        #endregion

    }
}
