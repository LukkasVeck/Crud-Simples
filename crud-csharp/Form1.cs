﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crud_csharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Btnlistar_Click(object sender, EventArgs e)
        {
            {
                var dataTable = new DataTable();

                // Retorna para a variavel a ConnectionString configurada no App.Config

                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

                // Cria uma instancia de conexão com o banco de dados

                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    // Abre a conexão

                    connection.Open();

                    // Cria uma instancia do command

                    using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand())

                    {

                        // Comando SQL que será executado

                        var _sqlQuery = "SELECT * FROM CLIENTES";

                        command.Connection = connection;

                        command.CommandText = _sqlQuery;
                        // Adiciona o resultado em um DataTable

                        using (System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(command))

                        {

                            adapter.Fill(dataTable);

                        }

                    }
                    // Fecha conexão

                    connection.Close();

                }
                // Atribui o resultado ao grid

                gridClientes.DataSource = dataTable;

                // Gera automaticamente as colunas

                gridClientes.AutoGenerateColumns = true;

                // Muda o modo de seleção da grid para linha inteira

                gridClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            }

        }

        private void btnexcluir_Click(object sender, EventArgs e)
        {
            if (gridClientes.SelectedRows.Count > 0)

            {

                // Pega o ID da primeira coluna da linha selecionada e converte para Integer

                int id;

                int.TryParse(gridClientes.SelectedRows[0].Cells[0].Value.ToString(), out id);
                // Retorna para a variavel a ConnectionString configurada no App.Config

                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
                // Cria uma instancia de conexão com o banco de dados

                using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))

                {

                 // Abre a conexão

                 connection.Open();
                 // Cria uma instancia do command

                 using (System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand())

                 {

                 // Comando SQL que será executado

                 var _sqlQuery = "DELETE FROM CLIENTES WHERE ID = @ID";
                 command.Connection = connection;

                 command.CommandText = _sqlQuery;

                 command.Parameters.AddWithValue("id", id);
                 // Executa a query

                 command.ExecuteNonQuery();

                 }
                 // Fecha conexão

                 connection.Close();

                 // Invoca o botão listar

                 Btnlistar_Click(sender, e);

                }
            }
        }

        private void btnnovo_Click(object sender, EventArgs e)
        {
            //Aqui ele executa o formulário
            var form2 = new Form2();
            //Define o título do FORM
            form2.Text = "Novo";
            //Centraliza o form em relação ao forma Principal
            form2.StartPosition = FormStartPosition.CenterParent;
            //Abre o form em modo Dialog
            form2.ShowDialog();
            //Chama o botão de listar após o DIALOG ser fechado
            Btnlistar_Click(sender, e);
        }

        private void btneditar_Click(object sender, EventArgs e)
        {
            if (gridClientes.SelectedRows.Count > 0)
            {
                int id;
                // Pega o ID da primeira coluna da linha selecionada e realiza a conversão para INTEGER
                int.TryParse(gridClientes.SelectedRows[0].Cells[0].Value.ToString(), out id);

                //Executa o formulario passando o ID do registro que será editado
                var form2 = new Form2(id);
                //Define o titulo do formulario
                form2.Text = "Editar";
                //Centraliza o form em relação ao form principal
                form2.StartPosition = FormStartPosition.CenterParent;
                //Abre o form em modo Dialog
                form2.ShowDialog();
                //Chama o botão de listar após o Dialog ser fechado
                Btnlistar_Click(sender, e);
            }
        }
    }
}
