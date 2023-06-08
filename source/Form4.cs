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
    public partial class Form4 : Form
    {
        private SqlConnection cn;
        private int currentObj;
        private bool adding;
        private Dictionary<string, int> dicionarioEraID = new Dictionary<string, int>();
        private Dictionary<string, int> dicionarioJogID = new Dictionary<string, int>();
        private Dictionary<string, int> dicionarioJogEliID = new Dictionary<string, int>();

        public Form4()
        {
            InitializeComponent();
            verifySGBDConnection();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            // JOG TEM
            SqlCommand cmd = new SqlCommand("SELECT nome, jogador_id FROM Empires.Jogador", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            comboBoxJog.Items.Clear();
            comboBoxJog.Items.Add("-Seleciona-");
            dicionarioJogID.Clear(); // Limpar o dicionário antes de preenchê-lo novamente

            while (reader.Read())
            {
                string nome = reader["nome"].ToString();
                int id = Convert.ToInt32(reader["jogador_id"]);

                comboBoxJog.Items.Add(nome);
                dicionarioJogID[nome] = id; // Adicionar a entrada ao dicionário
            }

            comboBoxJog.SelectedIndex = 0;

            reader.Close();

            // JOGELI
            cmd = new SqlCommand("SELECT nome, jogador_id FROM Empires.Jogador", cn);
            reader = cmd.ExecuteReader();
            comboBoxJogEli.Items.Clear();
            comboBoxJogEli.Items.Add("-Seleciona-");
            dicionarioJogEliID.Clear(); // Limpar o dicionário antes de preenchê-lo novamente

            while (reader.Read())
            {
                string nome = reader["nome"].ToString();
                int id = Convert.ToInt32(reader["jogador_id"]);

                comboBoxJogEli.Items.Add(nome);
                dicionarioJogEliID[nome] = id; // Adicionar a entrada ao dicionário
            }

            comboBoxJogEli.SelectedIndex = 0;

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

            SqlCommand cmd = new SqlCommand("SELECT * FROM Empires.Objeto", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBox2.Items.Clear();

            while (reader.Read())
            {
                Objeto O = new Objeto();
                O.ObjID = reader["obj_id"].ToString();
                O.Nome = reader["nome"].ToString();
                O.Localizacao_X = reader["localizacao_x"].ToString();
                O.Localizacao_Y = reader["localizacao_y"].ToString();
                O.FK_eraID = reader["FK_era_id"].ToString();
                O.FK_jogador_ID_tem = reader["FK_jogador_id_tem"].ToString();
                O.FK_jogador_ID_elimina = reader["FK_jogador_id_elimina"].ToString();
                listBox2.Items.Add(O);
            }
            cn.Close();
        }


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                currentObj = listBox2.SelectedIndex;
                ShowObj();
            }
        }

        private void SubmitObjeto(Objeto O)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;

            // Insert into Objeto table
            cmd.CommandText = "INSERT INTO Empires.Objeto (obj_id, nome, localizacao_x, localizacao_y, FK_jogador_id_tem, FK_jogador_id_elimina, FK_era_id) VALUES (@obj_id, @nome, @localizacao_x, @localizacao_y, @FK_jogador_id_tem, @FK_jogador_id_elimina, @FK_era_id)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@obj_id", O.ObjID);
            cmd.Parameters.AddWithValue("@nome", O.Nome);
            cmd.Parameters.AddWithValue("@localizacao_x", O.Localizacao_X);
            cmd.Parameters.AddWithValue("@localizacao_y", O.Localizacao_Y);


            //era
            if (comboBox_Era.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid Era Type. Please select a valid Era type.");
            }
            string nomeSelecionado = comboBox_Era.SelectedItem.ToString();
            int idSelecionado = dicionarioEraID[nomeSelecionado]; // Obter o ID correspondente do dicionário

            cmd.Parameters.AddWithValue("@FK_era_id", idSelecionado);

            //JogTem
            if (comboBox_Era.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid JogTem Type. Please select a valid JogTem type.");
            }
            string nomeSelecionado2 = comboBoxJog.SelectedItem.ToString();
            int idSelecionado2 = dicionarioJogID[nomeSelecionado2]; // Obter o ID correspondente do dicionário

            cmd.Parameters.AddWithValue("@FK_jogador_id_tem", idSelecionado2);

            //JogEli
            int idSelecionado3;
            if (comboBox_Era.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid JogEli Type. Please select a valid JogEli type.");
            }
            else
            {
                string nomeSelecionado3 = comboBoxJogEli.SelectedItem.ToString();
                idSelecionado3 = dicionarioJogEliID[nomeSelecionado3]; // Obter o ID correspondente do dicionário
            }
            cmd.Parameters.AddWithValue("@FK_jogador_id_elimina", idSelecionado3);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to submit Objeto in the database. \nERROR MESSAGE:\n" + ex.Message);
            }

            cn.Close();
        }

        private void UpdateObjetos(Objeto O)
        {
            int rows = 0;

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Empires.Objeto SET nome = @novo_nome, localizacao_x = @novo_localizacao_x, localizacao_y = @novo_localizacao_y, FK_era_id =@novo_FK_era_id, FK_jogador_id_tem = @novo_FK_jogador_id_tem, FK_jogador_id_elimina = @novo_FK_jogador_id_elimina  WHERE obj_ID = @obj_id";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@novo_nome", O.Nome);
            cmd.Parameters.AddWithValue("@obj_id", O.ObjID);
            cmd.Parameters.AddWithValue("@novo_localizacao_x", O.Localizacao_X);
            cmd.Parameters.AddWithValue("@novo_localizacao_y", O.Localizacao_Y);

            //era
            if (comboBox_Era.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid Era Type. Please select a valid Era type.");
            }
            string nomeSelecionado = comboBox_Era.SelectedItem.ToString();
            int idSelecionado = dicionarioEraID[nomeSelecionado]; // Obter o ID correspondente do dicionário

            cmd.Parameters.AddWithValue("@novo_FK_era_id", idSelecionado);

            //JogTem
            if (comboBox_Era.SelectedItem.ToString() == "-Seleciona-")
            {
                throw new Exception("Invalid JogTem Type. Please select a valid JogTem type.");
            }
            string nomeSelecionado2 = comboBoxJog.SelectedItem.ToString();
            int idSelecionado2 = dicionarioJogID[nomeSelecionado2]; // Obter o ID correspondente do dicionário

            cmd.Parameters.AddWithValue("@novo_FK_jogador_id_tem", idSelecionado2);

            //JogEli
            int idSelecionado3;
            string nomeSelecionado3 = comboBoxJogEli.SelectedItem.ToString();
            idSelecionado3 = dicionarioJogEliID[nomeSelecionado3]; // Obter o ID correspondente do dicionário
            cmd.Parameters.AddWithValue("@novo_FK_jogador_id_elimina", idSelecionado3);

            cmd.Connection = cn;

            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update Objeto in database. \n ERROR MESSAGE: \n" + ex.Message);
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

        private void RemoveObjetos(string obj_id)
        {
            if (!verifySGBDConnection())
                return;

            try
            {
                // Delete the record in Objetos table
                SqlCommand deleteCmd = new SqlCommand();
                deleteCmd.CommandText = "DELETE FROM Empires.Objeto WHERE obj_id = @obj_id";
                deleteCmd.Parameters.AddWithValue("@obj_id", obj_id);
                deleteCmd.Connection = cn;
                deleteCmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete Objetos in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
        }


        public void LockControls()
        {
            txtID.ReadOnly = true;
            txtNome.ReadOnly = true;
            txtLX.ReadOnly = true;
            txtLY.ReadOnly = true;
        }

        public void UnlockControls()
        {
            txtID.ReadOnly = false;
            txtNome.ReadOnly = false;
            txtLX.ReadOnly = false;
            txtLY.ReadOnly = false;
        }

        public void ClearFields()
        {
            txtID.Text = "";
            txtNome.Text = "";
            txtLX.Text = "";
            txtLY.Text = "";
            comboBox_Era.SelectedIndex = 0;
            comboBoxJog.SelectedIndex = 0;
            comboBoxJogEli.SelectedIndex = 0;

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

        public void ShowObj()
        {
            if (listBox2.Items.Count == 0 | currentObj < 0)
                return;
            Objeto Obj = new Objeto();
            Obj = (Objeto)listBox2.Items[currentObj];
            txtID.Text = Obj.ObjID;
            txtNome.Text = Obj.Nome;
            txtLX.Text = Obj.Localizacao_X;
            txtLY.Text = Obj.Localizacao_Y;


            // Obter o Jogador ID Tem correspondente ao ID armazenado no Objeto
            string JogTemId = Obj.FK_jogador_ID_tem;
            int JogTemIdInt;
            int.TryParse(JogTemId, out JogTemIdInt);
            string JogTemNome = dicionarioJogID.FirstOrDefault(x => x.Value == JogTemIdInt).Key;
            comboBoxJog.SelectedItem = JogTemNome;



            // Obter o Jogador ID Elimina correspondente ao ID armazenado no Objeto
            string JogEliId = Obj.FK_jogador_ID_elimina;
            int JogEliIdInt;
            int.TryParse(JogEliId, out JogEliIdInt);
            string JogEliNome = dicionarioJogEliID.FirstOrDefault(x => x.Value == JogEliIdInt).Key;
            comboBoxJogEli.SelectedItem = JogEliNome;


            //Obter a era correspondente ao ID armazenado no objeto Jogador
            int eraId = Convert.ToInt32(Obj.FK_eraID);
            string eraNome = dicionarioEraID.FirstOrDefault(x => x.Value == eraId).Key;
            comboBox_Era.SelectedItem = eraNome;



        }

        private bool SaveObjetos()
        {
            Objeto Obj = new Objeto();
            try
            {
                Obj.ObjID = txtID.Text;
                Obj.Nome = txtNome.Text;
                Obj.Localizacao_X = txtLX.Text;
                Obj.Localizacao_Y = txtLY.Text;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            if (adding)
            {
                SubmitObjeto(Obj);
                listBox2.Items.Add(Obj);
            }
            else
            {
                UpdateObjetos(Obj);
                listBox2.Items[currentObj] = Obj;
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
                SaveObjetos();
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
                currentObj = listBox2.SelectedIndex;
                if (currentObj < 0)
                    currentObj = 0;
                ShowObj();
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
                    RemoveObjetos(((Objeto)listBox2.SelectedItem).ObjID);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                if (currentObj == listBox2.Items.Count)
                    currentObj = listBox2.Items.Count - 1;
                if (currentObj == -1)
                {
                    ClearFields();
                    MessageBox.Show("There are no more Objetos");
                }
                else
                {
                    ShowObj();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentObj = listBox2.SelectedIndex;
            if (currentObj < 0)
            {
                MessageBox.Show("Please select an Object to edit");
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

        private void comboBox_Era_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxJog_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}