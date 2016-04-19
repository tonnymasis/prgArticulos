using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Llamando de las referencias propias del proyecto
using System.Data.SqlClient;
using Modelo;
using Controlador;

namespace prgArticulos
{
    public partial class frmAcceso : Form
    {
        #region Atributos
        clsConexionSQL conexion;
        clsEntidadUsuario pEntidadUsuario;
        clsUsuarios usuario;
        SqlDataReader dtrUsuario; //Es para el retorno de las tuplas
        int intContador = 0;
        #endregion
        //Inicializamos los atributos
        public frmAcceso()
        {
            conexion = new clsConexionSQL();
            pEntidadUsuario = new clsEntidadUsuario();
            usuario = new clsUsuarios();
            InitializeComponent();
        }

        private void frmAcceso_Load(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            this.SetVisibleCore(false);
            MDImenu menu = new MDImenu(conexion);
            menu.Show();        
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtCodigoUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
                //El evento focus permite trasladar el cursor del mouse al siguiente espacio o objeto indicado
                this.txtClave.Focus();
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                if (mValidarDatos() == true)
                {
                    this.btnIngresar.Enabled = true;
                }
            }

        }//Fin de keypress de clave

        #region Metodos
        //Este metdo permite verificar la existencia del usuario
        //segun el codigo y la clave digitada
        private Boolean mValidarDatos()
        {
            if (intContador <= 2)
            {
                //Llenado de los atributos del servidor para conectarme a la base de datos
                conexion.setCodigo("admEstudiante");
                conexion.setClave("123");

                //Lelenado de los atributos de la clase entidadUsuarios
                pEntidadUsuario.setCodigo(this.txtCodigoUsuario.Text.Trim());
                pEntidadUsuario.setClave(this.txtClave.Text.Trim());

                //Consultar su el usuario existe
                dtrUsuario = usuario.mConsultarUsuario(conexion, pEntidadUsuario);

                //Evaluo si retornan tuplas o datos
                if (dtrUsuario != null)
                {
                    if (dtrUsuario.Read())
                    {
                        //Aca obtengo el perfil de sujeto que se va a conectar
                        pEntidadUsuario.setPerfil(dtrUsuario.GetString(2));
                        pEntidadUsuario.setEstado(dtrUsuario.GetInt32(3));
                        if (pEntidadUsuario.getEstado() == 0)
                        {
                            this.btnIngresar.Enabled = true;
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("El usuario esta bloqueado", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }//Fin del pEntidadUsuario
                    }
                    else {
                        MessageBox.Show("El usuario no existe", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.None);
                        return false;
                    }//Fin del if del read
                }
                else {
                    MessageBox.Show("El usuario no existe", "Atencion", MessageBoxButtons.OK,MessageBoxIcon.None);
                    return false;
                }//Fin del if del null
            }
            else {
                MessageBox.Show("Usted ha fallado muchas veces al intentar ingresar","Usuarios Bloqueado",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }//Fin del if del contador
        }//Fin del metodo mValidarDatos
        #endregion

    }//Fin del public
}
