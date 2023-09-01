using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Office.Interop.Excel;
using System.Net;

namespace TicketManager
{
    public partial class Main : Form
    {
        //establish connection to sql database
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;Initial Catalog=TicketManager;Integrated Security=True");


        public Main()
        {
            InitializeComponent();
        }

        private void btnSaveClient_Click(object sender, EventArgs e)
        {
            //Save new customer to customer db
            //1.open connection to customerDB
            //2.pass values from form to database
            //3.Update datagridview and clear form

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into customerDB(ID, client, contact, address, postcode, phone, email, website) Values(@ID, @client, @contact, @address, @postcode, @phone, @email, @website)";
            cmd.Parameters.AddWithValue("@ID", tbClientID.Text);
            cmd.Parameters.AddWithValue("@client", tbClientName.Text);
            cmd.Parameters.AddWithValue("@contact", tbContact.Text);
            cmd.Parameters.AddWithValue("@address", tbClientAddress.Text);
            cmd.Parameters.AddWithValue("@postcode", tbClientPostcode.Text);
            cmd.Parameters.AddWithValue("@phone", tbClientPhone.Text);
            cmd.Parameters.AddWithValue("@email", tbClientEmail.Text);
            cmd.Parameters.AddWithValue("@website", tbClientWebsite.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            disp_data();
            MessageBox.Show("Record updated");

            tbClientID.Text = "";
            tbClientName.Text = "";
            tbContact.Text = "";
            tbClientAddress.Text = "";
            tbClientPostcode.Text = "";
            tbClientPhone.Text = "";
            tbClientEmail.Text = "";
            tbClientWebsite.Text = "";

        }

        //update datagridview customer database
        public void disp_data()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from customerDB";
            System.Data.DataTable dt = new System.Data.DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dgvCustomerDB.DataSource = dt;

            con.Close();

        }


        private void Main_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'ticketManagerDataSet2.techs' table. You can move, or remove it, as needed.
            this.techsTableAdapter.Fill(this.ticketManagerDataSet2.techs);

            disp_data();
            cbTech.Text = "";

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            //On live tickets, search by customer ID, auto-fill form fields with customer details
            string sql;
            sql = "Select * from customerDB where ID = '" + tbID.Text + "'";
            SqlCommand com = new SqlCommand(sql, con);
            con.Open();
            DataSet data = new DataSet();
            var adapter = new SqlDataAdapter(com);
            adapter.Fill(data);
            int count = data.Tables[0].Rows.Count;
            con.Close();
            if (count > 0)
            {
                tbClient.Text = data.Tables[0].Rows[0]["client"].ToString();
                tbAddress.Text = data.Tables[0].Rows[0]["address"].ToString();
                tbPostcode.Text = data.Tables[0].Rows[0]["postcode"].ToString();
                tbPhoneNo.Text = data.Tables[0].Rows[0]["phone"].ToString();

            }
            else
            {
                MessageBox.Show("Invalid ID", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //add new task to datagridview
            dgvLiveTickets.Rows.Add(tbID.Text, tbClient.Text, tbAddress.Text, tbPostcode.Text, tbPhoneNo.Text, cbTech.Text, rtbDescription.Text, tbEstimate.Text, monthCalendar1.SelectionRange.Start.ToShortDateString());

            tbID.Text = "";
            tbClient.Text = "";
            tbAddress.Text = "";
            tbPostcode.Text = "";
            tbPhoneNo.Text = "";
            rtbDescription.Text = "";
            cbTech.Text = "";
            tbEstimate.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //add completed task to datagridview completed tasks
            foreach (DataGridViewRow selRow in dgvLiveTickets.SelectedRows.OfType<DataGridViewRow>().ToArray())
            {
                dgvLiveTickets.Rows.Remove(selRow);
                dgvClosedTickets.Rows.Add(selRow);

                tbTotal.Text = (from DataGridViewRow row in dgvClosedTickets.Rows
                                where row.Cells[7].FormattedValue.ToString() != string.Empty
                                select Convert.ToDouble(row.Cells[7].FormattedValue)).Sum().ToString();
            }
        }

        private void btnDeleteLive_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selRow in dgvLiveTickets.SelectedRows.OfType<DataGridViewRow>().ToArray())
            {
                dgvLiveTickets.Rows.Remove(selRow);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //open excel and transfer data from datagridview
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet
                worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Exported from Ticket Manager";

            for (int i = 1; i < dgvClosedTickets.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dgvClosedTickets.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvClosedTickets.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dgvClosedTickets.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgvClosedTickets.Rows[i].Cells[j].Value.ToString();
                }
            }

            workbook.SaveAs("c:\\ClosedTickets.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);

        }

        private void btnDeleteClosed_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selRow in dgvClosedTickets.SelectedRows.OfType<DataGridViewRow>().ToArray())
            {
                dgvClosedTickets.Rows.Remove(selRow);


                tbTotal.Text = (from DataGridViewRow row in dgvClosedTickets.Rows
                                where row.Cells[7].FormattedValue.ToString() != string.Empty
                                select Convert.ToDouble(row.Cells[7].FormattedValue)).Sum().ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login loginPg = new Login();
            loginPg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login loginPg = new Login();
            loginPg.Show();
        }

        private void LogOut1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login loginPg = new Login();
            loginPg.Show();
        }
    }
}
