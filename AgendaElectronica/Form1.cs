using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgendaElectronica
{
    public partial class frmContacto : Form
    {
        string cModo = "Inicio";
        public static string ConectionString = "";
        int nIdContacto = 0;
        SqlConnection conexion;

        public frmContacto()
        {
            InitializeComponent();
        }

        private void frmContacto_Load(object sender, EventArgs e)
        {
            //this.Width = 1296;
            //this.Height = 749;
           

            try
            {
               
                ConectionString = "Server = DESKTOP-44GN9JE; Database = AgendaElectronica; MultipleActiveResultSets = True; user id = LTurbi; pwd = *010405; Connection Timeout = 1000;";
               
            }
            catch (Exception ex)
            {

                string cMensajeError = ex.Message + ", " + ((ex.InnerException != null) ? ex.InnerException.Message.Trim() : "").ToString();
                MessageBox.Show("Ha ocurriodo un error conectandose a la base de datos, Detalle del Error: " + cMensajeError,"", MessageBoxButtons.OK,
                      MessageBoxIcon.Information);
            }
            Inicir();
        }
        private void Inicir()
        {
            cboEstado.SelectedIndex = 0;
            cModo = "Inicio";
            Botones();
            DesHabilitar();
            Limpiar();
            LlenarGrid();



        }
        private void DesHabilitar()
        {
            txtApellido.Enabled = false;
            txtApellido.ReadOnly = true;

            txtCelular.Enabled = false;
            txtCelular.ReadOnly = true;

            txtDireccion.Enabled = false;
            txtDireccion.ReadOnly = true;

            txtEmail.Enabled = false;
            txtEmail.ReadOnly = true;

            txtNombre.Enabled = false;
            txtNombre.ReadOnly = true;

            txtTelefono.Enabled = false;
            txtTelefono.ReadOnly = true;

            rdoF.Enabled = false;
            rdoM.Enabled = false;
            cboEstado.Enabled = false;
        }

        private void Habilitar()
        {
            txtApellido.Enabled = true;
            txtApellido.ReadOnly = false;

            txtCelular.Enabled = true;
            txtCelular.ReadOnly = false;

            txtDireccion.Enabled = true;
            txtDireccion.ReadOnly = false;

            txtEmail.Enabled = true;
            txtEmail.ReadOnly = false;

            txtNombre.Enabled = true;
            txtNombre.ReadOnly = false;

            txtTelefono.Enabled = true;
            txtTelefono.ReadOnly = false;

            rdoF.Enabled = true;
            rdoM.Enabled = true;
            cboEstado.Enabled = true;
        }

        private void Limpiar()
        {
            txtApellido.Text = "";
            txtCelular.Text = "";
            txtDireccion.Text = "";
            txtEmail.Text = "";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            //rdoF.va = true;
            //rdoM.Enabled = true;
            //cboEstado.Enabled = true;
        }

        public void Botones()
        {

            switch (cModo)
            {

                case "Inicio":
                    this.cmdBorrar.Enabled = false;
                    this.cmdGrabar.Enabled = false;
                    this.cmdNuevo.Enabled = true;
                    this.cmdSalir.Enabled = true;
                    this.cmdEditar.Enabled = true;
                    this.cmdCancelar.Enabled = false;
                    break;
                case "Nuevo":
                    this.cmdBorrar.Enabled = false;
                    this.cmdCancelar.Enabled = true;
                    this.cmdGrabar.Enabled = true;
                    this.cmdNuevo.Enabled = false;
                    this.cmdEditar.Enabled = false;
                    this.cmdSalir.Enabled = true;

                    break;

                case "Grabar":
                    this.cmdBorrar.Enabled = false;
                    this.cmdCancelar.Enabled = false;
                    this.cmdGrabar.Enabled = false;
                    this.cmdNuevo.Enabled = true;
                    this.cmdSalir.Enabled = true;
                    this.cmdEditar.Enabled = false;
                    break;

                case "Eliminar":
                    this.cmdBorrar.Enabled = false;
                    this.cmdCancelar.Enabled = false;
                    this.cmdGrabar.Enabled = false;
                    this.cmdNuevo.Enabled = true;
                    this.cmdSalir.Enabled = true;
                    break;

                case "Cancelar":
                    this.cmdBorrar.Enabled = false;
                    this.cmdCancelar.Enabled = false;
                    this.cmdGrabar.Enabled = false;
                    this.cmdNuevo.Enabled = true;
                    this.cmdSalir.Enabled = true;
                    break;

                case "Buscar":
                    this.cmdBorrar.Enabled = true;
                    this.cmdCancelar.Enabled = true;
                    this.cmdGrabar.Enabled = false;
                    this.cmdNuevo.Enabled = false;
                    this.cmdSalir.Enabled = true;
                    this.cmdEditar.Enabled = true;
                    break;

                case "Actualizar":
                    this.cmdBorrar.Enabled = true;
                    this.cmdCancelar.Enabled = true;
                    this.cmdGrabar.Enabled = true;
                    this.cmdNuevo.Enabled = false;
                    this.cmdSalir.Enabled = true;
                    this.cmdEditar.Enabled = false;
                    break;


                default:
                    this.cmdBorrar.Enabled = false;

                    this.cmdCancelar.Enabled = false;
                    this.cmdGrabar.Enabled = false;
                    this.cmdNuevo.Enabled = true;
                    this.cmdSalir.Enabled = true;
                    this.cmdEditar.Enabled = false;
                    break;
            }
        } // fin Botones

        private void cmdNuevo_Click(object sender, EventArgs e)
        {
            cModo = "Nuevo";
            Botones();
            Habilitar();
            Limpiar();
            txtNombre.Focus();
        }
        private void LlenarGrid()
        {
            string cSexo = "";
            string cGenero = "";
            string cEstado = "";
            string cEstadoCivil = "";
            try
            {
                DataTable dtDato = new DataTable();
                conexion = new SqlConnection(ConectionString);
                conexion.Open();

                string cQuery = "select * from Contactos";
                SqlCommand cmd = new SqlCommand(cQuery, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter(cmd);

                adaptador.Fill(dtDato);
                dgvAgenda.Rows.Clear();
                foreach (DataRow contacto in dtDato.Rows)
                {
                    cSexo = Convert.ToString(contacto["Genero"]);
                    switch (cSexo)
                    {
                        case "M":
                            cGenero = "Masculino";
                            break;
                        case "F":
                            cGenero = "Femenino";
                            break;
                        default:
                            break;
                    }
                    dgvAgenda.Rows.Add(Convert.ToString(contacto["Nombre"]), Convert.ToString(contacto["Apellido"]), Convert.ToString(contacto["FechaNac"]), cGenero,
                        Convert.ToString(contacto["Telefono"]), Convert.ToString(contacto["Celular"]), Convert.ToString(contacto["Direccion"]), Convert.ToString(contacto["Email"]),
                        Convert.ToString(contacto["IdContacto"]));
                }
                conexion.Close();
            }
            catch (Exception ex)
            {

                string cMensajeError = ex.Message + ", " + ((ex.InnerException != null) ? ex.InnerException.Message.Trim() : "").ToString();
                MessageBox.Show("Error Llenando Grid: " + cMensajeError, "", MessageBoxButtons.OK,
                      MessageBoxIcon.Information);
                conexion.Close();
            }
        }
        private void BuscarContacto()
        {
            try
            {
                string cSexo = "";               
                conexion = new SqlConnection(ConectionString);
                conexion.Open();
                string cQuery = "select * from Contactos";
                SqlCommand cmd = new SqlCommand(cQuery, conexion);
                SqlDataReader oLector = cmd.ExecuteReader();
                while (oLector.Read())
                {
                    txtNombre.Text = oLector["Nombre"].ToString();
                    txtApellido.Text = oLector["Apellido"].ToString();
                    txtDireccion.Text = oLector["Direccion"].ToString();
                    txtTelefono.Text = oLector["Telefono"].ToString();
                    txtCelular.Text = oLector["Celular"].ToString();
                    txtEmail.Text = oLector["Email"].ToString();
                    cboEstado.SelectedIndex = Convert.ToInt32(oLector["EstadoCivil"]);
                    nIdContacto = Convert.ToInt32(oLector["IdContacto"]);
                    cSexo = oLector["Genero"].ToString();
                    switch (cSexo)
                    {
                        case "M":
                            rdoM.Checked = true;
                            rdoF.Checked = false;
                            break;
                        case "F":
                            rdoF.Checked = true;
                            rdoM.Checked = false;                            
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {

                string cMensajeError = ex.Message + ", " + ((ex.InnerException != null) ? ex.InnerException.Message.Trim() : "").ToString();
                MessageBox.Show("Error Llenando Grid: " + cMensajeError, "", MessageBoxButtons.OK,
                      MessageBoxIcon.Information);
                conexion.Close();
            }
        }

        private void cmdGrabar_Click(object sender, EventArgs e)
        {
            //int nIdContacto = 0;
            string cSexo = "";
            string cNombre = txtNombre.Text;
            string cApellido = txtApellido.Text;
            string cDireccion = txtDireccion.Text;
            string cTelefono = txtTelefono.Text;
            string cCelular = txtCelular.Text;
            string cEmail = txtEmail.Text;
            String Fecha = dtFecha.Value.ToString("yyyy-MM-dd");
            if (rdoF.Checked)
            {
                cSexo = "F";
            }
            if (rdoM.Checked)
            {
                cSexo = "M";
            }
            string cEstadoCivil = cboEstado.SelectedIndex.ToString();
            try
            {
                conexion = new SqlConnection(ConectionString);
                conexion.Open();

                if (cModo == "Nuevo")
                {
                    string cQuery = "Insert Into Contactos(Nombre,Apellido,FechaNac,Direccion,Genero,EstadoCivil,Celular,Telefono,Email) values('" + cNombre + "','" + cApellido + "','" +
                    Convert.ToDateTime(Fecha).ToString("yyyy-MM-dd HH:mm:ss") + "','" + cDireccion + "','" + cSexo + "','" + cEstadoCivil + "','" + cCelular + "','" + cTelefono + "','" + cEmail + "')";
                    SqlCommand cmd = new SqlCommand(cQuery, conexion);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Contacto Insertado Correctamente!!!");
                        LlenarGrid();
                        Limpiar();
                        Inicir();
                    }
                }
                if (cModo == "Actualizar")
                {
                    string cQuery = "Update Contactos set Nombre = '" + cNombre + "',Apellido = '" + cApellido + "',FechaNac = '" + Convert.ToDateTime(Fecha).ToString("yyyy-MM-dd HH:mm:ss") + "',Direccion = '"+
                        cDireccion + "',Genero = '" + cSexo + "',EstadoCivil = '" + cEstadoCivil + "',Celular = '" + cCelular + "',Telefono = '" + cTelefono + "', Email = '" + cEmail + "' where IdContacto = " +
                        nIdContacto;
                    SqlCommand cmd = new SqlCommand(cQuery, conexion);

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Contacto Actualizado Correctamente Correctamente!!!");
                        LlenarGrid();
                        Limpiar();
                        Inicir();
                    }
                }

                
                
                conexion.Close();

            }
            catch (Exception ex)
            {

                string cMensajeError = ex.Message + ", " + ((ex.InnerException != null) ? ex.InnerException.Message.Trim() : "").ToString();
                MessageBox.Show("Error Insertando el Contacto: " + cMensajeError, "", MessageBoxButtons.OK,
                      MessageBoxIcon.Information);
            }

        }

        private void cmdEditar_Click(object sender, EventArgs e)
        {
            DataGridViewRow IdCont = dgvAgenda.CurrentRow;
            int nId = Convert.ToInt32(IdCont.Cells["idcontacto"].Value);
            if (nId > 0)
            {
                cModo = "Actualizar";
                Botones();
                Limpiar();
                BuscarContacto();
                Habilitar();
            }
        }

        private void cmdBorrar_Click(object sender, EventArgs e)
        {

        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            Inicir();
        }

        private void cmdSalir_Click(object sender, EventArgs e)
        {

        }

        private void dgvAgenda_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
