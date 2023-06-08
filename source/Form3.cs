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
    public partial class Form3 : Form
    {
        private SqlConnection cn;
        private int currentJog;
        private bool adding;
        private Dictionary<string, string> dicionarioGrandecivID = new Dictionary<string, string>();
        private Dictionary<string, int> dicionarioEraID = new Dictionary<string, int>();


        public Form3()
        {
            InitializeComponent();
            verifySGBDConnection();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            if (!verifySGBDConnection())
                return;

            // CIVILIZACAO
            SqlCommand cmd = new SqlCommand("SELECT ID, Nome FROM Empires.Todas_Civilizacao WHERE Lider IS NOT NULL;", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            grandecivBOX.Items.Clear();
            grandecivBOX.Items.Add("-Seleciona-");
            dicionarioGrandecivID.Clear(); // Limpar o dicionário antes de preenchê-lo novamente

            while (reader.Read())
            {
                string nome = reader["Nome"].ToString();
                string id = reader["ID"].ToString();

                grandecivBOX.Items.Add(nome);
                dicionarioGrandecivID[nome] = id; // Adicionar a entrada ao dicionário
            }

            grandecivBOX.SelectedIndex = 0;

            reader.Close();

            // EQUIPA
            cmd = new SqlCommand("SELECT equipa_id FROM Empires.Equipa", cn);
            reader = cmd.ExecuteReader();
            comboBox_Equipa.Items.Clear();
            comboBox_Equipa.Items.Add("-Seleciona-");
            while (reader.Read())
            {
                comboBox_Equipa.Items.Add(reader["equipa_id"].ToString());
            }

            comboBox_Equipa.SelectedIndex = 0;

            reader.Close();


            // ERA
            cmd = new SqlCommand("SELECT nome, era_id FROM Empires.Era", cn);
            reader = cmd.ExecuteReader();
            comboBox_Era.Items.Clear();
            comboBox_Era.Items.Add("-Seleciona-");
            dicionarioEraID.Clear(); // Limpar o dicionário antes de preenchê-lo novamente

            while (reader.Read())
            {
                string nome = reader["nome"].ToString();
                int id = Convert.ToInt32(reader["era_id"]);
                string idAsString = id.ToString();

                comboBox_Era.Items.Add(nome);
                dicionarioEraID[nome] = id; // Adicionar a entrada ao dicionário
            }

            comboBox_Era.SelectedIndex = 0;

            reader.Close();

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


        private void button5_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM Empires.Jogador", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBox2.Items.Clear();

            while (reader.Read())
            {
                Jogador J = new Jogador();
                J.JogID = reader["jogador_id"].ToString();
                J.Nome = reader["nome"].ToString();
                J.Clan = reader["clan"].ToString();
                J.Cor = reader["cor"].ToString();
                J.FK_eraID = reader["FK_era_id"].ToString();
                J.FK_grandeID = reader["FK_grande_id"].ToString();
                J.FK_equipaID = reader["FK_equipa_id"].ToString();
                listBox2.Items.Add(J);
            }

            cn.Close();


        }


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                currentJog = listBox2.SelectedIndex;
                ShowJog();
            }
        }

        private void SubmitJogador(Jogador J)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;

            // Insert into Jogador table
            cmd.CommandText = "INSERT INTO Empires.Jogador (jogador_id, nome, clan, cor, FK_grande_id, FK_equipa_id, FK_era_id) VALUES (@jogador_id, @nome, @clan, @cor, @FK_grande_id, @FK_equipa_id, @FK_era_id)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@jogador_id", J.JogID);
            cmd.Parameters.AddWithValue("@nome", J.Nome);
            cmd.Parameters.AddWithValue("@clan", J.Clan);
            cmd.Parameters.AddWithValue("@cor", J.Cor);


            //grande civ
            if (grandecivBOX.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid Grand Civ Type. Please select a valid Grand Civ type.");
            }
            string nomeSelecionado = grandecivBOX.SelectedItem.ToString();
            string idSelecionado = dicionarioGrandecivID[nomeSelecionado]; // Obter o ID correspondente do dicionário

            cmd.Parameters.AddWithValue("@FK_grande_id", idSelecionado);


            //equipa
            if (comboBox_Equipa.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid Equipa Type. Please select a valid Jogador type.");
            }
            cmd.Parameters.AddWithValue("@FK_equipa_id", comboBox_Equipa.SelectedItem.ToString());

            //era
            if (comboBox_Era.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid Era Type. Please select a valid Era type.");
            }
            string nomeSelecionado2 = comboBox_Era.SelectedItem.ToString();
            int idSelecionado2 = dicionarioEraID[nomeSelecionado2]; // Obter o ID correspondente do dicionário

            cmd.Parameters.AddWithValue("@FK_era_id", idSelecionado2);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to submit Jogador in the database. \nERROR MESSAGE:\n" + ex.Message);
            }

            cn.Close();
        }




        private void UpdateJogadores(Jogador J)
        {
            int rows = 0;

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Empires.Jogador SET nome = @novo_nome, clan = @novo_clan, cor = @novo_cor,FK_grande_id = @novo_FK_grande_id, FK_equipa_id = @novo_FK_equipa_id, FK_era_id = @novo_FK_era_id WHERE jogador_ID = @jogador_id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@novo_nome", J.Nome);
            cmd.Parameters.AddWithValue("@jogador_id", J.JogID);
            cmd.Parameters.AddWithValue("@novo_clan", J.Clan);
            cmd.Parameters.AddWithValue("@novo_cor", J.Cor);

            //grande civ
            if (grandecivBOX.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid Grand Civ Type. Please select a valid Grand Civ type.");
            }
            string nomeSelecionado = grandecivBOX.SelectedItem.ToString();
            string idSelecionado = dicionarioGrandecivID[nomeSelecionado]; // Obter o ID correspondente do dicionário

            cmd.Parameters.AddWithValue("@novo_FK_grande_id", idSelecionado);


            //equipa
            if (comboBox_Equipa.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid Equipa Type. Please select a valid Jogador type.");
            }
            cmd.Parameters.AddWithValue("@novo_FK_equipa_id", comboBox_Equipa.SelectedItem.ToString());

            //era
            if (comboBox_Era.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid Era Type. Please select a valid Era type.");
            }
            string nomeSelecionado2 = comboBox_Era.SelectedItem.ToString();
            int idSelecionado2 = dicionarioEraID[nomeSelecionado2]; // Obter o ID correspondente do dicionário

            cmd.Parameters.AddWithValue("@novo_FK_era_id", idSelecionado2);

            cmd.Connection = cn;

            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update Jogador in database. \n ERROR MESSAGE: \n" + ex.Message);
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


        private void RemoveJogadores(string jogador_id)
        {
            if (!verifySGBDConnection())
                return;

            try
            {
                // Delete the record in Jogadores table
                SqlCommand deleteCmd = new SqlCommand();
                deleteCmd.CommandText = "DELETE FROM Empires.Jogador WHERE jogador_id = @jogador_id";
                deleteCmd.Parameters.AddWithValue("@jogador_id", jogador_id);
                deleteCmd.Connection = cn;
                deleteCmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete Jogadores in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
        }


        // Helper routines
        public void LockControls()
        {
            txtID.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtClan.ReadOnly = true;
            txtCor.ReadOnly = true;
        }

        public void UnlockControls()
        {
            txtID.ReadOnly = false;
            txtNome.ReadOnly = false;
            txtClan.ReadOnly = false;
            txtCor.ReadOnly = false;
        }

        public void ClearFields()
        {
            txtID.Text = "";
            txtNome.Text = "";
            txtClan.Text = "";
            txtCor.Text = "";
            grandecivBOX.SelectedIndex = 0;
            comboBox_Era.SelectedIndex = 0;
            comboBox_Equipa.SelectedIndex = 0;
            listBox2.Items.Clear();

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

        public void ShowJog()
        {
            if (listBox2.Items.Count == 0 | currentJog < 0)
                return;
            Jogador Jog = new Jogador();
            Jog = (Jogador)listBox2.Items[currentJog];
            txtID.Text = Jog.JogID;
            txtNome.Text = Jog.Nome;
            txtClan.Text = Jog.Clan;
            txtCor.Text = Jog.Cor;

            // Obter a grande civilização correspondente ao ID armazenado no objeto Jogador
            string grandeCivId = Jog.FK_grandeID;
            string grandeCivNome = dicionarioGrandecivID.FirstOrDefault(x => x.Value == grandeCivId).Key;
            grandecivBOX.SelectedItem = grandeCivNome;


            // Obter a era correspondente ao ID armazenado no objeto Jogador
            int eraId = Convert.ToInt32(Jog.FK_eraID);
            string eraNome = dicionarioEraID.FirstOrDefault(x => x.Value == eraId).Key;
            comboBox_Era.SelectedItem = eraNome;

            // Definir o índice selecionado da combobox 'comboBox_Equipa'
            string equipaId = (Jog.FK_equipaID);
            comboBox_Equipa.SelectedItem = equipaId;

        }

        private bool SaveJogadores()
        {
            Jogador Jog = new Jogador();
            try
            {
                Jog.JogID = txtID.Text;
                Jog.Nome = txtNome.Text;
                Jog.Clan = txtClan.Text;
                Jog.Cor = txtCor.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            if (adding)
            {
                SubmitJogador(Jog);
                listBox2.Items.Add(Jog);
            }
            else
            {
                UpdateJogadores(Jog);
                listBox2.Items[currentJog] = Jog;
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
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
                SaveJogadores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            listBox2.Enabled = true;
            int idx = listBox2.FindString(txtID.Text);
            listBox2.SelectedIndex = idx;
            ShowButtons();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            listBox2.Enabled = true;
            if (listBox2.Items.Count > 0)
            {
                currentJog = listBox2.SelectedIndex;
                if (currentJog < 0)
                    currentJog = 0;
                ShowJog();
            }
            else
            {
                ClearFields();
                LockControls();
            }
            ShowButtons();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            adding = true;
            ClearFields();
            HideButtons();
            listBox2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > -1)
            {
                try
                {
                    RemoveJogadores(((Jogador)listBox2.SelectedItem).JogID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                if (currentJog == listBox2.Items.Count)
                    currentJog = listBox2.Items.Count - 1;
                if (currentJog == -1)
                {
                    ClearFields();
                    MessageBox.Show("There are no more Jogadores");
                }
                else
                {
                    ShowJog();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentJog = listBox2.SelectedIndex;
            if (currentJog < 0)
            {
                MessageBox.Show("Please select a Jogador to edit");
                return;
            }
            adding = false;
            HideButtons();
            listBox2.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                currentJog = listBox2.SelectedIndex;
                String msg = "Displaying information for:\n\t";
                msg += listBox2.SelectedItem.ToString();
                msg += "\n\nN. Cards: ";

                msg += getCardInfo();

                msg += "\n\nN. Objects: ";

                msg += getObjectInfo();

                MessageBox.Show(msg, "Player Info");
            }
        }

        private String getCardInfo()
        {
            if (!verifySGBDConnection())
                return "";

            String info = "";

            Jogador Jog = (Jogador)listBox2.Items[currentJog];

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Empires.GetTotalCardsPerPlayer(@jog_id) AS NumberCards";
            cmd.Parameters.AddWithValue("@jog_id", Jog.JogID);
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

        private String getObjectInfo()
        {
            if (!verifySGBDConnection())
                return "";

            String info = "";

            Jogador Jog = (Jogador)listBox2.Items[currentJog];

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT Empires.GetTotalObjectsPerPlayer(@jog_id) AS NumberObjects";
            cmd.Parameters.AddWithValue("@jog_id", Jog.JogID);
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                info += reader["NumberObjects"].ToString();
            }

            cn.Close();

            return info;
        }
    }
}
