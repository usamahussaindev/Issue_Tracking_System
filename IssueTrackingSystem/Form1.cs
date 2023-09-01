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

namespace IssueTrackingSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'trackingSystemDataSet.users' table. You can move, or remove it, as needed.
            this.usersTableAdapter.Fill(this.trackingSystemDataSet.users);

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            SqlConnection sqlcon = new SqlConnection(@"Data Source=BLINDSPACE\SQLEXPRESS;Initial Catalog=TrackingSystem;Integrated Security=True");
            string query = "select * from users where username = '" + cbUsername.Text.Trim() + "' and password = '" + tbPassword.Text.Trim() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                MessageBox.Show("Greetings! Welcome to Issue Tracking System....  "  );

                Main ObjMain = new Main();
                this.Hide();
                ObjMain.Show();

            }
            else
            {
                MessageBox.Show("Incorrect username or Password! ");
            }
           

        }
    }
}