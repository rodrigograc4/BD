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
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projeto
{
    public partial class Form2 : Form
    {
        private SqlConnection cn;
        private int currentCiv;
        private bool adding;

        public Form2()
        {
            InitializeComponent();
            verifySGBDConnection();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox_Type.Items.Add("-Seleciona-");
            comboBox_Type.Items.Add("Grande Civilização");
            comboBox_Type.Items.Add("Pequena Civilização");
            SearchBox.Items.Add("Todas as Civilizações");
            SearchBox.Items.Add("Grande Civilização");
            SearchBox.Items.Add("Pequena Civilização");
            comboBox_Type.SelectedIndex = 0;
            SearchBox.SelectedIndex = 0;
            LockControls();
        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("data source = tcp:mednat.ieeta.pt\\SQLSERVER, 8101; Initial Catalog = p4g6; uid = p4g6; password = RR2022!");

        }


        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                currentCiv = listBox1.SelectedIndex;
                ShowCiv();
            }
        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (!verifySGBDConnection())
                return;

            String sqlGrande = "SELECT ID, Nome, Lider, Capital FROM Empires.Todas_Civilizacao WHERE Lider IS NOT NULL";

            String sqlPequena = "SELECT ID, Nome, Limite_Tropas FROM Empires.Todas_Civilizacao WHERE Limite_Tropas IS NOT NULL";

            String sqlAll = "SELECT * FROM Empires.Todas_Civilizacao";


            SqlCommand cmd;
            cmd = new SqlCommand(sqlAll, cn);

            if (SearchBox.SelectedItem != null)
            {
                if (SearchBox.SelectedItem.ToString() == "Grande Civilização")
                {
                    cmd = new SqlCommand(sqlGrande, cn);
                }
                else if (SearchBox.SelectedItem.ToString() == "Pequena Civilização")
                {
                    cmd = new SqlCommand(sqlPequena, cn);
                }
                else if (SearchBox.SelectedItem.ToString() == "Todas as Civilizações")
                {
                    cmd = new SqlCommand(sqlAll, cn);
                }
            }

            SqlDataReader reader = cmd.ExecuteReader();
            listBox1.Items.Clear();

            while (reader.Read())
            {
                Civilizacao C;
                if (cmd.CommandText == sqlGrande)
                {
                    C = new Grande_Civilizacao();
                    ((Grande_Civilizacao)C).Lider = reader["Lider"].ToString();
                    ((Grande_Civilizacao)C).Capital = reader["Capital"].ToString();
                }
                else if (cmd.CommandText == sqlPequena)
                {
                    C = new Pequena_Civilizacao();
                    ((Pequena_Civilizacao)C).LimiteTropas = reader["Limite_Tropas"].ToString();
                }
                else
                {
                    C = new Civilizacao();
                    if (!reader.IsDBNull(reader.GetOrdinal("Lider")))
                    {
                        C = new Grande_Civilizacao();
                        ((Grande_Civilizacao)C).Lider = reader["Lider"].ToString();
                        ((Grande_Civilizacao)C).Capital = reader["Capital"].ToString();
                    }
                    else if (!reader.IsDBNull(reader.GetOrdinal("Limite_Tropas")))
                    {
                        C = new Pequena_Civilizacao();
                        ((Pequena_Civilizacao)C).LimiteTropas = reader["Limite_Tropas"].ToString();
                    }
                }

                C.CivID = reader["ID"].ToString();
                C.Nome = reader["Nome"].ToString();
                listBox1.Items.Add(C);
            }

            cn.Close();
        }


        private void SubmitCivilizacao(Civilizacao C)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;

            // Insert into Civilizacao table
            cmd.CommandText = "EXEC Empires.InsertCivilizacao  @Nome = @nome, @FK_grande_id = @id;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", C.CivID);
            cmd.Parameters.AddWithValue("@nome", C.Nome);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to submit Civilizacao in the database. \nERROR MESSAGE:\n" + ex.Message);
            }
            if (comboBox_Type.SelectedItem.ToString() != "-Seleciona-")
            {
                if (comboBox_Type.SelectedItem.ToString() == "Grande Civilização")
                {

                    cmd.CommandText = "INSERT INTO Empires.Grande_Civilizacao (FK_civ_id, lider, capital) VALUES (@FK_civ_id, @lider, @capital)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@FK_civ_id", txtID.Text);
                    cmd.Parameters.AddWithValue("@lider", textBoxGC.Text);
                    cmd.Parameters.AddWithValue("@capital", textBoxGC2.Text);
                }
                else if (comboBox_Type.SelectedItem.ToString() == "Pequena Civilização")
                {

                    cmd.CommandText = "INSERT INTO Empires.Pequena_Civilizacao (FK_civ_id, limite_tropas) VALUES (@FK_civ_id, @limite_tropas)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@FK_civ_id", txtID.Text);
                    cmd.Parameters.AddWithValue("@limite_tropas", textBoxPC.Text);
                }
            }
            else
                throw new Exception("Invalid Civilizacao Type. Please select a valid Civilizacao type.");

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to submit Civilizacao in the database. \nERROR MESSAGE:\n" + ex.Message);
            }

            cn.Close();
        }




        private void UpdateCivilizacao(Civilizacao C)
        {
            int rows = 0;

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Empires.Civilizacao SET nome = @novo_nome WHERE civ_ID = @civ_id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@novo_nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@civ_id", txtID.Text);
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            if (comboBox_Type.SelectedItem.ToString() != "-Seleciona-")
            {
                if (comboBox_Type.SelectedItem.ToString() == "Grande Civilização")
                {

                    cmd.CommandText = "UPDATE Empires.Grande_Civilizacao SET lider =@novo_lider, capital = @novo_capital WHERE FK_civ_ID = @FK_civ_id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("FK_civ_id", txtID.Text);
                    cmd.Parameters.AddWithValue("@novo_lider", textBoxGC.Text);
                    cmd.Parameters.AddWithValue("@novo_capital", textBoxGC2.Text);
                }
                else if (comboBox_Type.SelectedItem.ToString() == "Pequena Civilização")
                {

                    cmd.CommandText = "UPDATE Empires.Pequena_Civilizacao SET limite_tropas =@novo_limite_tropas WHERE FK_civ_ID = @FK_civ_id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@FK_civ_id", txtID.Text);
                    cmd.Parameters.AddWithValue("@novo_limite_tropas", textBoxPC.Text);
                }
            }
            else
                throw new Exception("Invalid Civilizacao Type. Please select a valid Civilizacao type.");

            cmd.Connection = cn;

            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update Civilizacao in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                if (rows == 1)
                    MessageBox.Show("Update OK");
                else
                    MessageBox.Show("Update NOT OK");

                cn.Close();
            }
        }


        private void RemoveCivilizacao(string civ_id)
        {
            if (!verifySGBDConnection())
                return;

            try
            {
                // Delete the record in Civilizacao table
                SqlCommand deleteCmd = new SqlCommand();
                deleteCmd.CommandText = "DELETE FROM Empires.Civilizacao WHERE civ_id = @civ_id";
                deleteCmd.Parameters.AddWithValue("@civ_id", civ_id);
                deleteCmd.Connection = cn;
                deleteCmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete Civilizacao in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
        }




        // Helper routines
        public void LockControls()
        {
            txtID.ReadOnly = true;
            txtNome.ReadOnly = true;
        }

        public void UnlockControls()
        {
            txtID.ReadOnly = false;
            txtNome.ReadOnly = false;
        }

        public void ClearFields()
        {
            txtID.Text = "";
            txtNome.Text = "";
            comboBox_Type.SelectedIndex = 0;
            SearchBox.SelectedIndex = 0;
            listBox1.Items.Clear();
        }

        public void ShowButtons()
        {
            LockControls();
            button4.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            button8.Visible = false;
            button9.Visible = false;
        }

        public void HideButtons()
        {
            UnlockControls();
            button4.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button8.Visible = true;
            button9.Visible = true;
        }

        public void ShowCiv()
        {
            if (listBox1.Items.Count == 0 || currentCiv < 0)
                return;

            Civilizacao Civ = (Civilizacao)listBox1.Items[currentCiv];
            txtID.Text = Civ.CivID;
            txtNome.Text = Civ.Nome;

            if (Civ is Pequena_Civilizacao)
            {
                comboBox_Type.SelectedItem = "Pequena Civilização";
                textBoxPC.Text = ((Pequena_Civilizacao)Civ).LimiteTropas;
            }
            else if (Civ is Grande_Civilizacao)
            {
                comboBox_Type.SelectedItem = "Grande Civilização";
                textBoxGC.Text = ((Grande_Civilizacao)Civ).Lider;
                textBoxGC2.Text = ((Grande_Civilizacao)Civ).Capital;
            }
            else
                comboBox_Type.SelectedItem = 0;
        }



        private bool SaveCivilizacao()
        {
            Civilizacao Civ = new Civilizacao();
            try
            {
                Civ.CivID = txtID.Text;
                Civ.Nome = txtNome.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            if (adding)
            {
                SubmitCivilizacao(Civ);
                listBox1.Items.Add(Civ);
            }
            else
            {
                UpdateCivilizacao(Civ);
                listBox1.Items[currentCiv] = Civ;
            }
            return true;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowCiv();
        }

        private void comboBox_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Type.SelectedItem.ToString() == "Grande Civilização")
            {
                // Mostrar adicional
                showBigInfo();
            }
            else if (comboBox_Type.SelectedItem.ToString() == "Pequena Civilização")
            {
                // Ocultar adicional
                showSmallInfo();
            }
            else
            {
                // Ocultar adicional
                showNoInfo();
            }
        }

        private void showBigInfo()
        {
            textBoxGC.Visible = true;
            textBoxGC2.Visible = true;
            labelGC.Visible = true;
            labelGC2.Visible = true;
            labelPC.Visible = false;
            textBoxPC.Visible = false;
            comboBox_Type.SelectedIndex = 1;
        }

        private void showSmallInfo()
        {
            textBoxGC.Visible = false;
            textBoxGC2.Visible = false;
            labelGC.Visible = false;
            labelGC2.Visible = false;
            labelPC.Visible = true;
            textBoxPC.Visible = true;
            comboBox_Type.SelectedIndex = 2;
        }

        private void showNoInfo()
        {
            textBoxGC.Visible = false;
            textBoxGC2.Visible = false;
            labelGC.Visible = false;
            labelGC2.Visible = false;
            labelPC.Visible = false;
            textBoxPC.Visible = false;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            adding = true;
            ClearFields();
            HideButtons();
            listBox1.Enabled = false;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                try
                {
                    RemoveCivilizacao(((Civilizacao)listBox1.SelectedItem).CivID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                if (currentCiv == listBox1.Items.Count)
                    currentCiv = listBox1.Items.Count - 1;
                if (currentCiv == -1)
                {
                    ClearFields();
                    MessageBox.Show("There are no more Civilizacoes");
                }
                else
                {
                    ShowCiv();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 HomePage = new Form1();
            HomePage.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCivilizacao();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            listBox1.Enabled = true;
            int idx = listBox1.FindString(txtID.Text);
            listBox1.SelectedIndex = idx;
            ShowButtons();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listBox1.Enabled = true;
            if (listBox1.Items.Count > 0)
            {
                currentCiv = listBox1.SelectedIndex;
                if (currentCiv < 0)
                    currentCiv = 0;
                ShowCiv();
            }
            else
            {
                ClearFields();
                LockControls();
            }
            ShowButtons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentCiv = listBox1.SelectedIndex;
            if (currentCiv < 0)
            {
                MessageBox.Show("Please select a Civilizacao to edit");
                return;
            }
            adding = false;
            HideButtons();
            listBox1.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                currentCiv = listBox1.SelectedIndex;
                String msg = "Displaying information for:\n\t";
                msg += listBox1.SelectedItem.ToString();
                msg += "\n\nN. Cards: ";

                msg += getMoreInfo();

                MessageBox.Show (msg , "Civ Info");
            }
        }

        private String getMoreInfo()
        {
            if (!verifySGBDConnection())
                return "";

            String info = "";

            Civilizacao Civ = (Civilizacao)listBox1.Items[currentCiv];

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Empires.GetTotalCardsPerCivilizacao(@civ_id) AS NumberCards";
            cmd.Parameters.AddWithValue("@civ_id", Civ.CivID);
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                info += reader["NumberCards"].ToString();
            }

            cn.Close();

            return info;
        }
    }
}
