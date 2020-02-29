using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analizador
{
    public partial class Analizador : Form
    {
        //Primero  definimos listas de caracteres
        List<char> _num = new List<char>(new char[] { '0','1','2','3','4','5','6','7','8','9'});
        List<char> _var = new List<char>(new char[] { 'A', 'B', 'C','D','E','F','G','H','I','J','K','L','M','N','Ñ','O','P','Q','R','S','T','U','V','W','X','Y','Z'});
        List<char> _ope = new List<char>(new char[] { '+', '-', '*', '/' });
        List<char> _del = new List<char>(new char[] { '(', ')' });
        DataTable _tblRes = new DataTable();


        private void frmPrincipal_Load(object sender, EventArgs e)
        {   //Crea columnas a las tablas
            _tblRes.Columns.Add("Token", typeof(char));
            _tblRes.Columns.Add("Tipo", typeof(string));
        }      

        private void btnAnalizar_Click(object sender, EventArgs e)
        {
            //Limpia la tabla
            _tblRes.Clear();
            //Crea resultados en la función
            realizarAnalisis();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {   //Limpia la tabla.
            txtExpresion.Clear();
            _tblRes.Clear();
            //Limpia la base de datos.
            dgvResultados.DataSource = null;
            dgvResultados.Refresh();
        }

        public void realizarAnalisis()
        {// Divide cada caracter a elementos, adicionalmente, elimina los espacios
            List<char> _elementos = txtExpresion.Text.Replace(" ", "").ToCharArray().ToList();
            //Verifica que haya al menos un elemento
            if (_elementos.Count > 0)
            {   
                DataRow _fila;
                //Para cada elemento revisa de que se trata
                foreach (char elemento in _elementos)
                {
                    _fila = _tblRes.NewRow();

                    if (_num.Contains(elemento))
                    {
                        _fila["Token"] = elemento;
                        _fila["Tipo"] = "Número";
                    }
                    else if (_var.Contains(elemento.ToString().ToUpper()[0]))
                    {
                        _fila["Token"] = elemento;
                        _fila["Tipo"] = "Variable";
                    }
                    else if (_ope.Contains(elemento))
                    {
                        _fila["Token"] = elemento;
                        _fila["Tipo"] = "Operador";
                    }
                    else if (_del.Contains(elemento))
                    {
                        _fila["Token"] = elemento;
                        _fila["Tipo"] = "Delimitador";
                    }
                    else
                    {
                        _fila["Token"] = elemento;
                        _fila["Tipo"] = "Error";
                    }

                    _tblRes.Rows.Add(_fila);
                }

                dgvResultados.DataSource = _tblRes;
                dgvResultados.Refresh();
            }
            else
            {
                dgvResultados.DataSource = null;
                dgvResultados.Refresh();
            }
        }
        public Analizador()
        {
            InitializeComponent();
        }
    }
}
