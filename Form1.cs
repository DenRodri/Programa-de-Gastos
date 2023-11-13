using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using DGVPrinterHelper;

namespace Programacion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /*
         Existe un edificio y es la distribucion entre cada persona que vive ahi en los gastos comunes de ese edificio
        gastos comunes: Agua, Luz de espacios comunes (Pasillos, Elevador),
        Por cada factura, hay que distribuirlo
        Hay consumos, energia electrica general, porque la bomba de agua jala mucha luz
        Si hay un salon y una banca, el salon gasta mas. 
          
         
         */
        public static Boolean IsNumeric(string valor)
        {
            int result;
            return int.TryParse(valor, out result);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            ImpCB.DropDownStyle = ComboBoxStyle.DropDownList;
            int mes = DateTime.Today.Month;
            // Guarda el mes actual
            if (mes != Properties.Settings.Default.CurrMonth)
            {
                Properties.Settings.Default.CurrFact++;
                MessageBox.Show("Nueva factura mes");
                MessageBox.Show(Properties.Settings.Default.CurrMonth.ToString());
                MessageBox.Show(mes.ToString());
                MessageBox.Show(Properties.Settings.Default.CurrFact.ToString());
            }
            Properties.Settings.Default.CurrMonth = mes;
            Properties.Settings.Default.Save();
            string mesnom;
            switch (mes)
            {
                case 1:
                    mesnom = "Enero";
                    break;
                case 2:
                    mesnom = "Febrero";
                    break;
                case 3:
                    mesnom = "Marzo";
                    break;
                case 4:
                    mesnom = "Abril";
                    break;
                case 5:
                    mesnom = "Mayo";
                    break;
                case 6:
                    mesnom = "Junio";
                    break;
                case 7:
                    mesnom = "Julio";
                    break;
                case 8:
                    mesnom = "Agosto";
                    break;
                case 9:
                    mesnom = "Septiembre";
                    break;
                case 10:
                    mesnom = "Octubre";
                    break;
                case 11:
                    mesnom = "Noviembre";
                    break;
                case 12:
                    mesnom = "Diciembre";
                    break;
                default:
                    mesnom = "";
                    break;
            }
            meslb.Text = meslb.Text + " " + mesnom;
            // Primera Tabla
            DistribucionDGB.Rows.Clear();
            DistribucionDGB.Columns.Clear();
            DistribucionDGB.Columns.Add("Abon", "Abonados");
            DistribucionDGB.Columns.Add("Porc", "Porcentajes");
            DistribucionDGB.Columns.Add("Dis", "Distribucion");
            DistribucionDGB.Columns.Add("Gen", "General");
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows[0].Cells[0].Value = "Swites Presidenciales";
            DistribucionDGB.Rows[0].Cells[1].Value = "";
            DistribucionDGB.Rows[0].Cells[2].Value = "";
            DistribucionDGB.Rows[0].Cells[3].Value = "";
            DistribucionDGB.Rows[1].Cells[0].Value = "Swites Presidenciales A";
            DistribucionDGB.Rows[1].Cells[1].Value = "";
            DistribucionDGB.Rows[1].Cells[2].Value = "";
            DistribucionDGB.Rows[1].Cells[3].Value = "";
            DistribucionDGB.Rows[2].Cells[0].Value = "Swites Presidenciales B";
            DistribucionDGB.Rows[2].Cells[1].Value = "";
            DistribucionDGB.Rows[2].Cells[2].Value = "";
            DistribucionDGB.Rows[2].Cells[3].Value = "";
            DistribucionDGB.Rows[3].Cells[0].Value = "Swites Presidenciales C";
            DistribucionDGB.Rows[3].Cells[1].Value = "";
            DistribucionDGB.Rows[3].Cells[2].Value = "";
            DistribucionDGB.Rows[3].Cells[3].Value = "";
            DistribucionDGB.Rows[4].Cells[0].Value = "Swites Presidenciales D";
            DistribucionDGB.Rows[4].Cells[1].Value = "";
            DistribucionDGB.Rows[4].Cells[2].Value = "";
            DistribucionDGB.Rows[4].Cells[3].Value = "";
            DistribucionDGB.Rows[5].Cells[0].Value = "Swites Presidenciales E";
            DistribucionDGB.Rows[5].Cells[1].Value = "";
            DistribucionDGB.Rows[5].Cells[2].Value = "";
            DistribucionDGB.Rows[5].Cells[3].Value = "";
            DistribucionDGB.Rows[6].Cells[0].Value = "Total General Swites";
            DistribucionDGB.Rows[6].Cells[1].Value = "";
            DistribucionDGB.Rows[6].Cells[2].Value = "";
            DistribucionDGB.Rows[6].Cells[3].Value = "";
            // Segunda Tabla
            Distribucion2DGB.Rows.Clear();
            Distribucion2DGB.Columns.Clear();
            Distribucion2DGB.Columns.Add("Abon", "Abonados");
            Distribucion2DGB.Columns.Add("Porc", "Porcentajes");
            Distribucion2DGB.Columns.Add("Dis", "Distribucion");
            Distribucion2DGB.Columns.Add("Gen", "General");
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows[0].Cells[0].Value = "Condominios";
            Distribucion2DGB.Rows[0].Cells[1].Value = "";
            Distribucion2DGB.Rows[0].Cells[2].Value = "";
            Distribucion2DGB.Rows[0].Cells[3].Value = "";
            Distribucion2DGB.Rows[1].Cells[0].Value = "Condominios A";
            Distribucion2DGB.Rows[1].Cells[1].Value = "";
            Distribucion2DGB.Rows[1].Cells[2].Value = "";
            Distribucion2DGB.Rows[1].Cells[3].Value = "";
            Distribucion2DGB.Rows[2].Cells[0].Value = "Condominios B";
            Distribucion2DGB.Rows[2].Cells[1].Value = "";
            Distribucion2DGB.Rows[2].Cells[2].Value = "";
            Distribucion2DGB.Rows[2].Cells[3].Value = "";
            Distribucion2DGB.Rows[3].Cells[0].Value = "Condominios C";
            Distribucion2DGB.Rows[3].Cells[1].Value = "";
            Distribucion2DGB.Rows[3].Cells[2].Value = "";
            Distribucion2DGB.Rows[3].Cells[3].Value = "";
            Distribucion2DGB.Rows[4].Cells[0].Value = "Condominios D";
            Distribucion2DGB.Rows[4].Cells[1].Value = "";
            Distribucion2DGB.Rows[4].Cells[2].Value = "";
            Distribucion2DGB.Rows[4].Cells[3].Value = "";
            Distribucion2DGB.Rows[5].Cells[0].Value = "Condominios E";
            Distribucion2DGB.Rows[5].Cells[1].Value = "";
            Distribucion2DGB.Rows[5].Cells[2].Value = "";
            Distribucion2DGB.Rows[5].Cells[3].Value = "";
            Distribucion2DGB.Rows[6].Cells[0].Value = "Total General Condominios";
            Distribucion2DGB.Rows[6].Cells[1].Value = "";
            Distribucion2DGB.Rows[6].Cells[2].Value = "";
            Distribucion2DGB.Rows[6].Cells[3].Value = "";
            //Tercera Tabla
            Distribucion3DGB.Rows.Clear();
            Distribucion3DGB.Columns.Clear();
            Distribucion3DGB.Columns.Add("Abon", "Abonados");
            Distribucion3DGB.Columns.Add("Porc", "Porcentajes");
            Distribucion3DGB.Columns.Add("Dis", "Distribucion");
            Distribucion3DGB.Columns.Add("Gen", "General");
            Distribucion3DGB.Rows.Add();
            Distribucion3DGB.Rows.Add();
            Distribucion3DGB.Rows.Add();
            Distribucion3DGB.Rows.Add();
            Distribucion3DGB.Rows[0].Cells[0].Value = "Banca deportiva";
            Distribucion3DGB.Rows[0].Cells[1].Value = "";
            Distribucion3DGB.Rows[0].Cells[2].Value = "";
            Distribucion3DGB.Rows[0].Cells[3].Value = "";
            Distribucion3DGB.Rows[1].Cells[0].Value = "Salon de Belleza";
            Distribucion3DGB.Rows[1].Cells[1].Value = "";
            Distribucion3DGB.Rows[1].Cells[2].Value = "";
            Distribucion3DGB.Rows[1].Cells[3].Value = "";
            Distribucion3DGB.Rows[2].Cells[0].Value = "Banco de Reservas";
            Distribucion3DGB.Rows[2].Cells[1].Value = "";
            Distribucion3DGB.Rows[2].Cells[2].Value = "";
            Distribucion3DGB.Rows[2].Cells[3].Value = "";
            Distribucion3DGB.Rows[3].Cells[0].Value = "Supermercado Progreso";
            Distribucion3DGB.Rows[3].Cells[1].Value = "";
            Distribucion3DGB.Rows[3].Cells[2].Value = "";
            Distribucion3DGB.Rows[3].Cells[3].Value = "";
            Distribucion3DGB.Rows[4].Cells[0].Value = "Total General";
            Distribucion3DGB.Rows[4].Cells[1].Value = "";
            Distribucion3DGB.Rows[4].Cells[2].Value = "";
            Distribucion3DGB.Rows[4].Cells[3].Value = "";
        }
        private void LimpiarButton_Click(object sender, EventArgs e)
        {
            // Primera Tabla
            DistribucionDGB.Rows.Clear();
            DistribucionDGB.Columns.Clear();
            DistribucionDGB.Columns.Add("Abon", "Abonados");
            DistribucionDGB.Columns.Add("Porc", "Porcentajes");
            DistribucionDGB.Columns.Add("Dis", "Distribucion");
            DistribucionDGB.Columns.Add("Gen", "General");
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows.Add();
            DistribucionDGB.Rows[0].Cells[0].Value = "Swites Presidenciales";
            DistribucionDGB.Rows[0].Cells[1].Value = "";
            DistribucionDGB.Rows[0].Cells[2].Value = "";
            DistribucionDGB.Rows[0].Cells[3].Value = "";
            DistribucionDGB.Rows[1].Cells[0].Value = "Swites Presidenciales A";
            DistribucionDGB.Rows[1].Cells[1].Value = "";
            DistribucionDGB.Rows[1].Cells[2].Value = "";
            DistribucionDGB.Rows[1].Cells[3].Value = "";
            DistribucionDGB.Rows[2].Cells[0].Value = "Swites Presidenciales B";
            DistribucionDGB.Rows[2].Cells[1].Value = "";
            DistribucionDGB.Rows[2].Cells[2].Value = "";
            DistribucionDGB.Rows[2].Cells[3].Value = "";
            DistribucionDGB.Rows[3].Cells[0].Value = "Swites Presidenciales C";
            DistribucionDGB.Rows[3].Cells[1].Value = "";
            DistribucionDGB.Rows[3].Cells[2].Value = "";
            DistribucionDGB.Rows[3].Cells[3].Value = "";
            DistribucionDGB.Rows[4].Cells[0].Value = "Swites Presidenciales D";
            DistribucionDGB.Rows[4].Cells[1].Value = "";
            DistribucionDGB.Rows[4].Cells[2].Value = "";
            DistribucionDGB.Rows[4].Cells[3].Value = "";
            DistribucionDGB.Rows[5].Cells[0].Value = "Swites Presidenciales E";
            DistribucionDGB.Rows[5].Cells[1].Value = "";
            DistribucionDGB.Rows[5].Cells[2].Value = "";
            DistribucionDGB.Rows[5].Cells[3].Value = "";
            DistribucionDGB.Rows[6].Cells[0].Value = "Total General Swites";
            DistribucionDGB.Rows[6].Cells[1].Value = "";
            DistribucionDGB.Rows[6].Cells[2].Value = "";
            DistribucionDGB.Rows[6].Cells[3].Value = "";
            // Segunda Tabla
            Distribucion2DGB.Rows.Clear();
            Distribucion2DGB.Columns.Clear();
            Distribucion2DGB.Columns.Add("Abon", "Abonados");
            Distribucion2DGB.Columns.Add("Porc", "Porcentajes");
            Distribucion2DGB.Columns.Add("Dis", "Distribucion");
            Distribucion2DGB.Columns.Add("Gen", "General");
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows.Add();
            Distribucion2DGB.Rows[0].Cells[0].Value = "Condominios";
            Distribucion2DGB.Rows[0].Cells[1].Value = "";
            Distribucion2DGB.Rows[0].Cells[2].Value = "";
            Distribucion2DGB.Rows[0].Cells[3].Value = "";
            Distribucion2DGB.Rows[1].Cells[0].Value = "Condominios A";
            Distribucion2DGB.Rows[1].Cells[1].Value = "";
            Distribucion2DGB.Rows[1].Cells[2].Value = "";
            Distribucion2DGB.Rows[1].Cells[3].Value = "";
            Distribucion2DGB.Rows[2].Cells[0].Value = "Condominios B";
            Distribucion2DGB.Rows[2].Cells[1].Value = "";
            Distribucion2DGB.Rows[2].Cells[2].Value = "";
            Distribucion2DGB.Rows[2].Cells[3].Value = "";
            Distribucion2DGB.Rows[3].Cells[0].Value = "Condominios C";
            Distribucion2DGB.Rows[3].Cells[1].Value = "";
            Distribucion2DGB.Rows[3].Cells[2].Value = "";
            Distribucion2DGB.Rows[3].Cells[3].Value = "";
            Distribucion2DGB.Rows[4].Cells[0].Value = "Condominios D";
            Distribucion2DGB.Rows[4].Cells[1].Value = "";
            Distribucion2DGB.Rows[4].Cells[2].Value = "";
            Distribucion2DGB.Rows[4].Cells[3].Value = "";
            Distribucion2DGB.Rows[5].Cells[0].Value = "Condominios E";
            Distribucion2DGB.Rows[5].Cells[1].Value = "";
            Distribucion2DGB.Rows[5].Cells[2].Value = "";
            Distribucion2DGB.Rows[5].Cells[3].Value = "";
            Distribucion2DGB.Rows[6].Cells[0].Value = "Total General Condominios";
            Distribucion2DGB.Rows[6].Cells[1].Value = "";
            Distribucion2DGB.Rows[6].Cells[2].Value = "";
            Distribucion2DGB.Rows[6].Cells[3].Value = "";
            //Tercera Tabla
            Distribucion3DGB.Rows.Clear();
            Distribucion3DGB.Columns.Clear();
            Distribucion3DGB.Columns.Add("Abon", "Abonados");
            Distribucion3DGB.Columns.Add("Porc", "Porcentajes");
            Distribucion3DGB.Columns.Add("Dis", "Distribucion");
            Distribucion3DGB.Columns.Add("Gen", "General");
            Distribucion3DGB.Rows.Add();
            Distribucion3DGB.Rows.Add();
            Distribucion3DGB.Rows.Add();
            Distribucion3DGB.Rows.Add();
            Distribucion3DGB.Rows[0].Cells[0].Value = "Banca deportiva";
            Distribucion3DGB.Rows[0].Cells[1].Value = "";
            Distribucion3DGB.Rows[0].Cells[2].Value = "";
            Distribucion3DGB.Rows[0].Cells[3].Value = "";
            Distribucion3DGB.Rows[1].Cells[0].Value = "Salon de Belleza";
            Distribucion3DGB.Rows[1].Cells[1].Value = "";
            Distribucion3DGB.Rows[1].Cells[2].Value = "";
            Distribucion3DGB.Rows[1].Cells[3].Value = "";
            Distribucion3DGB.Rows[2].Cells[0].Value = "Banco de Reservas";
            Distribucion3DGB.Rows[2].Cells[1].Value = "";
            Distribucion3DGB.Rows[2].Cells[2].Value = "";
            Distribucion3DGB.Rows[2].Cells[3].Value = "";
            Distribucion3DGB.Rows[3].Cells[0].Value = "Supermercado Progreso";
            Distribucion3DGB.Rows[3].Cells[1].Value = "";
            Distribucion3DGB.Rows[3].Cells[2].Value = "";
            Distribucion3DGB.Rows[3].Cells[3].Value = "";
            Distribucion3DGB.Rows[4].Cells[0].Value = "Total General";
            Distribucion3DGB.Rows[4].Cells[1].Value = "";
            Distribucion3DGB.Rows[4].Cells[2].Value = "";
            Distribucion3DGB.Rows[4].Cells[3].Value = "";

            //Resto de Textboxes
            CDEETB.Clear();
            BancoParteTB.Clear();
            BancaParteTB.Clear();
            BancaPorcTB.Clear();
            BancoPorcTB.Clear();
            CAASDTB.Clear();
            CondominioParteTB.Clear();
            CondominioPorcTB.Clear();
            DISTRIBUCIONTTB.Clear();
            MANTENIMIENTOTB.Clear();
            PORTERIATB.Clear();
            SalonParteTB.Clear();
            SalonPorcTB.Clear();
            SuperParteTB.Clear();
            SuperPorcTB.Clear();
            SwitesParteTB.Clear();
            SwitesPorcTB.Clear();
            TotalParteTB.Clear();
            TotalPorcTB.Clear();
            VIGILANCIATB.Clear();
        }
        double Con, Swite, Banco, banca, salon, super;

        private void FacturacionButton_Click(object sender, EventArgs e)
        {

            Form2 facturacion = new Form2();//instanciando al Form
            facturacion.Show(); // Mostramos el Form2
        }

        private void ExportarButton_Click(object sender, EventArgs e)
        {
            string connectionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            connectionString = @"Data Source=LAPTOP-D36E25FB\SQLEXPRESS;Initial Catalog=edificio;Integrated Security=True";
            sql = @"INSERT INTO factura VALUES(@fecha, @cdee, @caasd, @vigilancia, @mantenimiento, @porteria);
                INSERT INTO inquilinos VALUES(@num_fac1, @nome_inq1, @porc1, @gast1);
                INSERT INTO inquilinos VALUES(@num_fac2, @nome_inq2, @porc2, @gast2);
                INSERT INTO inquilinos VALUES(@num_fac3, @nome_inq3, @porc3, @gast3);
                INSERT INTO inquilinos VALUES(@num_fac4, @nome_inq4, @porc4, @gast4);
                INSERT INTO inquilinos VALUES(@num_fac5, @nome_inq5, @porc5, @gast5);
                INSERT INTO inquilinos VALUES(@num_fac6, @nome_inq6, @porc6, @gast6);
              ";
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@fecha", DateTime.Now.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@cdee", Convert.ToDouble(CDEETB.Text));
                command.Parameters.AddWithValue("@caasd", Convert.ToDouble(CAASDTB.Text));
                command.Parameters.AddWithValue("@vigilancia", Convert.ToDouble(VIGILANCIATB.Text));
                command.Parameters.AddWithValue("@mantenimiento", Convert.ToDouble(MANTENIMIENTOTB.Text));
                command.Parameters.AddWithValue("@porteria", Convert.ToDouble(PORTERIATB.Text));
                command.Parameters.AddWithValue("@num_fac1", Properties.Settings.Default.CurrFact);
                command.Parameters.AddWithValue("@nome_inq1", DistribucionDGB.Rows[0].Cells[0].Value);
                command.Parameters.AddWithValue("@porc1", SwitePorc);
                command.Parameters.AddWithValue("@gast1", (total * SwitePorc / 100));
                command.Parameters.AddWithValue("@num_fac2", Properties.Settings.Default.CurrFact);
                command.Parameters.AddWithValue("@nome_inq2", Distribucion2DGB.Rows[0].Cells[0].Value);
                command.Parameters.AddWithValue("@porc2", ConPorc);
                command.Parameters.AddWithValue("@gast2", (total * ConPorc / 100));
                command.Parameters.AddWithValue("@num_fac3", Properties.Settings.Default.CurrFact);
                command.Parameters.AddWithValue("@nome_inq3", Distribucion3DGB.Rows[0].Cells[0].Value);
                command.Parameters.AddWithValue("@porc3", bancaporc);
                command.Parameters.AddWithValue("@gast3", (total * bancaporc / 100));
                command.Parameters.AddWithValue("@num_fac4", Properties.Settings.Default.CurrFact);
                command.Parameters.AddWithValue("@nome_inq4", Distribucion3DGB.Rows[1].Cells[0].Value);
                command.Parameters.AddWithValue("@porc4", salonporc);
                command.Parameters.AddWithValue("@gast4", (total * salonporc / 100));
                command.Parameters.AddWithValue("@num_fac5", Properties.Settings.Default.CurrFact);
                command.Parameters.AddWithValue("@nome_inq5", Distribucion3DGB.Rows[2].Cells[0].Value);
                command.Parameters.AddWithValue("@porc5", BancoPorc);
                command.Parameters.AddWithValue("@gast5", (total * BancoPorc / 100));
                command.Parameters.AddWithValue("@num_fac6", Properties.Settings.Default.CurrFact);
                command.Parameters.AddWithValue("@nome_inq6", Distribucion3DGB.Rows[3].Cells[0].Value);
                command.Parameters.AddWithValue("@porc6", superporc);
                command.Parameters.AddWithValue("@gast6", (total * superporc / 100));
                command.ExecuteNonQuery();
                MessageBox.Show("Se han exportado los datos a SQL Server de manera correcta, no exporte por otro mes para que se mueva a la siguiente factura.");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error generado. Detalles: " + ex.ToString());
            }
            finally
            {
                connection.Close();
            }

        }


            private void ImprimirButton_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Customer Report";
            printer.SubTitle = "Your subtitle";
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = false;
            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.Footer = "FoxLearn";
            printer.FooterSpacing = 15;
            printer.PrintDataGridView(DistribucionDGB);
            
        }
        int nop;
        private void ImpCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            nop = ImpCB.SelectedIndex + 1;
            DGVPrinter printer = new DGVPrinter();
                switch (nop)
                {
                    case 1:
                    printer.Title = "Swites";
                    printer.SubTitle = string.Format("Fecha: {0}", DateTime.Now.Date);
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer = "Torres Atiemar";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(DistribucionDGB);
                    break;
                    case 2:
                    printer.Title = "Condominios";
                    printer.SubTitle = string.Format("Fecha: {0}", DateTime.Now.Date);
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer = "Torres Atiemar";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(Distribucion2DGB);
                    break;
                    case 3:
                    printer.Title = "Servicios";
                    printer.SubTitle = string.Format("Fecha: {0}", DateTime.Now.Date);
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer = "Torres Atiemar";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(Distribucion3DGB);
                    break;
                }
        }

        /*DGVPrinter printer = new DGVPrinter();
    printer.Title = "Customer Report";
    printer.SubTitle = "Your subtitle";
    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
    printer.PageNumbers = true;
    printer.PageNumberInHeader = false;
    printer.PorportionalColumns = true;
    printer.HeaderCellAlignment = StringAlignment.Near;
    printer.Footer = "FoxLearn";
    printer.FooterSpacing = 15;
    printer.PrintDataGridView(DistribucionDGB);
*/

        double ConPorc, SwitePorc, BancoPorc, bancaporc, salonporc, superporc;
        double SwiteNPorc, ConNPorc;
        double total, totalparte, totalporc;
        public static string tot;
        private void CalcularButton_Click(object sender, EventArgs e)
        {
            if (!(IsNumeric(CAASDTB.Text)) || !(IsNumeric(CDEETB.Text)) || !(IsNumeric(MANTENIMIENTOTB.Text)) || !(IsNumeric(PORTERIATB.Text)) || !(IsNumeric(VIGILANCIATB.Text)))
            {
                MessageBox.Show("Uno de los textbox ingresados no es un numero", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                // Llena los porcentajes
                total = Convert.ToDouble(CAASDTB.Text) + Convert.ToDouble(CDEETB.Text) + Convert.ToDouble(MANTENIMIENTOTB.Text) + Convert.ToDouble(PORTERIATB.Text) + Convert.ToDouble(VIGILANCIATB.Text);
                tot = total.ToString();
                DISTRIBUCIONTTB.Text = total.ToString("0.##");
                Con = 100;
                Swite = Con + Con * 0.75;
                Banco = Swite - Swite * 0.3;
                banca = Banco - Banco * 0.85;
                salon = banca + banca * 8.50 / 100;
                super = (salon + banca)/2;
                CondominioParteTB.Text = Con.ToString("0.##");
                SwitesParteTB.Text = Swite.ToString("0.##");
                BancoParteTB.Text = Banco.ToString("0.##");
                BancaParteTB.Text = banca.ToString("0.##");
                SalonParteTB.Text = salon.ToString("0.##");
                SuperParteTB.Text = super.ToString("0.##");
                totalparte = (Con + Swite + Banco + banca + salon + super);
                TotalParteTB.Text = totalparte.ToString("0.##");
                ConPorc = (Con / totalparte * 100);
                CondominioPorcTB.Text = ConPorc.ToString("0.##");
                SwitePorc = (Swite / totalparte * 100);
                SwitesPorcTB.Text = SwitePorc.ToString("0.##");
                BancoPorc = (Banco / totalparte * 100);
                BancoPorcTB.Text = BancoPorc.ToString("0.##");
                bancaporc = (banca / totalparte * 100);
                BancaPorcTB.Text = bancaporc.ToString("0.##");
                salonporc = (salon / totalparte * 100);
                SalonPorcTB.Text = salonporc.ToString("0.##");
                superporc = (super / totalparte * 100);
                SuperPorcTB.Text = superporc.ToString("0.##");
                totalporc = BancoPorc + ConPorc + SwitePorc + bancaporc + salonporc + superporc;
                TotalPorcTB.Text = totalporc.ToString("0.##");
                SwiteNPorc = SwitePorc / 5;
                ConNPorc = ConPorc / 5;
                // Llena la tabla 1
                //DistribucionDGB.Rows[0].Cells[0].Value = "Swites Presidenciales";
                DistribucionDGB.Rows[0].Cells[1].Value = SwitePorc.ToString("0.##");
                DistribucionDGB.Rows[0].Cells[2].Value = (total*SwitePorc/100).ToString("0.##");
               // DistribucionDGB.Rows[1].Cells[0].Value = "Swites Presidenciales A";
                DistribucionDGB.Rows[1].Cells[1].Value = SwiteNPorc.ToString("0.##");
                DistribucionDGB.Rows[1].Cells[2].Value = (total*SwiteNPorc/100).ToString("0.##"); ;
               // DistribucionDGB.Rows[2].Cells[0].Value = "Swites Presidenciales B";
                DistribucionDGB.Rows[2].Cells[1].Value = SwiteNPorc.ToString("0.##");
                DistribucionDGB.Rows[2].Cells[2].Value = (total * SwiteNPorc / 100).ToString("0.##") ;
                // DistribucionDGB.Rows[3].Cells[0].Value = "Swites Presidenciales C";
                DistribucionDGB.Rows[3].Cells[1].Value = SwiteNPorc.ToString("0.##");
                DistribucionDGB.Rows[3].Cells[2].Value = (total * SwiteNPorc / 100).ToString("0.##"); ;
                // DistribucionDGB.Rows[4].Cells[0].Value = "Swites Presidenciales D";
                DistribucionDGB.Rows[4].Cells[1].Value = SwiteNPorc.ToString("0.##");
                DistribucionDGB.Rows[4].Cells[2].Value = (total * SwiteNPorc / 100).ToString("0.##"); ;
                //DistribucionDGB.Rows[5].Cells[0].Value = "Swites Presidenciales E";
                DistribucionDGB.Rows[5].Cells[1].Value = SwiteNPorc.ToString("0.##");
                DistribucionDGB.Rows[5].Cells[2].Value = (total * SwiteNPorc / 100).ToString("0.##"); ;
                //DistribucionDGB.Rows[6].Cells[0].Value = "Total General Swites";
                DistribucionDGB.Rows[6].Cells[3].Value = (total * SwiteNPorc / 100 * 5).ToString("0.##");

                //Llena la tabla 2
                //Distribucion2DGB.Rows[0].Cells[0].Value = "Condominios";
                Distribucion2DGB.Rows[0].Cells[1].Value = ConPorc.ToString("0.##");
                Distribucion2DGB.Rows[0].Cells[2].Value = (total*ConPorc/100).ToString("0.##");
                //Distribucion2DGB.Rows[1].Cells[0].Value = "Condominios A";
                Distribucion2DGB.Rows[1].Cells[1].Value = ConNPorc.ToString("0.##");
                Distribucion2DGB.Rows[1].Cells[2].Value = (total*ConNPorc/100).ToString("0.##");
                //Distribucion2DGB.Rows[2].Cells[0].Value = "Condominios B";
                Distribucion2DGB.Rows[2].Cells[1].Value = ConNPorc.ToString("0.##");
                Distribucion2DGB.Rows[2].Cells[2].Value = (total * ConNPorc / 100).ToString("0.##");
                //Distribucion2DGB.Rows[3].Cells[0].Value = "Condominios C";
                Distribucion2DGB.Rows[3].Cells[1].Value = ConNPorc.ToString("0.##");
                Distribucion2DGB.Rows[3].Cells[2].Value = (total * ConNPorc / 100).ToString("0.##");
                //Distribucion2DGB.Rows[4].Cells[0].Value = "Condominios D";
                Distribucion2DGB.Rows[4].Cells[1].Value = ConNPorc.ToString("0.##");
                Distribucion2DGB.Rows[4].Cells[2].Value = (total * ConNPorc / 100).ToString("0.##");
                //Distribucion2DGB.Rows[5].Cells[0].Value = "Condominios E";
                Distribucion2DGB.Rows[5].Cells[1].Value = ConNPorc.ToString("0.##");
                Distribucion2DGB.Rows[5].Cells[2].Value = (total * ConNPorc / 100).ToString("0.##");
                //Distribucion2DGB.Rows[6].Cells[0].Value = "Total General Condominios";
                Distribucion2DGB.Rows[6].Cells[3].Value = (total * ConNPorc / 100 * 5).ToString("0.##");

                //Llena la tabla 3
                //Distribucion3DGB.Rows[0].Cells[0].Value = "Banca deportiva";
                Distribucion3DGB.Rows[0].Cells[1].Value = bancaporc.ToString("0.##");
                Distribucion3DGB.Rows[0].Cells[2].Value = (total*bancaporc/100).ToString("0.##");
                //Distribucion3DGB.Rows[1].Cells[0].Value = "Salon de Belleza";
                Distribucion3DGB.Rows[1].Cells[1].Value = salonporc.ToString("0.##");
                Distribucion3DGB.Rows[1].Cells[2].Value = (total*salonporc/100).ToString("0.##");
                //Distribucion3DGB.Rows[2].Cells[0].Value = "Banco de Reservas";
                Distribucion3DGB.Rows[2].Cells[1].Value = BancoPorc.ToString("0.##");
                Distribucion3DGB.Rows[2].Cells[2].Value = (total*BancoPorc/100).ToString("0.##");
               // Distribucion3DGB.Rows[3].Cells[0].Value = "Supermercado Progreso";
                Distribucion3DGB.Rows[3].Cells[1].Value = superporc.ToString("0.##");
                Distribucion3DGB.Rows[3].Cells[2].Value = (total*superporc/100).ToString("0.##");
                //Distribucion3DGB.Rows[4].Cells[0].Value = "Total General";
                Distribucion3DGB.Rows[4].Cells[3].Value = ((total * bancaporc / 100)+ (total * salonporc / 100)+ (total * BancoPorc / 100)+ (total * superporc / 100)).ToString("0.##");
            }
        }
    }
}
