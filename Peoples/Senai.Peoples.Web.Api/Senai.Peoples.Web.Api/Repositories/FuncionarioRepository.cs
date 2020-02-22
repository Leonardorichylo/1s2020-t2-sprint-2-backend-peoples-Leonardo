using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {

        private string stringConexao = "Data Source=N-1S-DEV-14\\SQLEXPRESS; initial catalog=M_PEOPLES; user Id=sa; pwd=sa@132";


        public void AtualizarIdCorpo(FuncionarioDomain Funcionarios)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

                string queryUpdate = "UPDATE Funcionarios SET Nome, Sobrenome = @Nome, @Sobrenome WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", Funcionarios.IdFuncionario);
                    cmd.Parameters.AddWithValue("@Nome", Funcionarios.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", Funcionarios.Sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }

            }
        }

        public void AtualizarIdUrl(int id, FuncionarioDomain Funcionarios)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {

            string queryUpdate = "UPDATE Funcionarios SET Nome, Sobrenome = @Nome, @SobreNome WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", Funcionarios.Nome);
                    cmd.Parameters.AddWithValue("@Sobrenome", Funcionarios.Sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();

                }
            }
        }

        public FuncionarioDomain BuscarPorId(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectById = "SELECT IdFuncionario, Nome, Sobrenome FROM Funcionarios WHERE IdFuncionario = @ID";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FuncionarioDomain Funcionarios = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr["IdFuncionario"]),
                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };
                        return Funcionarios;
                    }
                    return null;
                }
            }
        }
        //deixar as variaveis (Funcionarios) nos parameters, na query e public void
        public void Cadastrar(FuncionarioDomain Funcionarios)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO Funcionarios(Nome, Sobrenome) VALUES (@Nome, @Sobrenome)";
                SqlCommand cmd = new SqlCommand(queryInsert, con);

                cmd.Parameters.AddWithValue("@Nome", Funcionarios.Nome);
                cmd.Parameters.AddWithValue("@Sobrenome", Funcionarios.Sobrenome);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM Funcionarios WHERE IdFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FuncionarioDomain> Listar()
        {
            List<FuncionarioDomain> Funcionarios = new List<FuncionarioDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT IdFuncionario, Nome, Sobrenome from Funcionarios";


                using (SqlCommand cmd = new SqlCommand(querySelectAll,con))
                {
                    SqlDataReader rdr;

                    con.Open();

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FuncionarioDomain funcionarios = new FuncionarioDomain
                        {
                            IdFuncionario = Convert.ToInt32(rdr[0]),

                            Nome = rdr["Nome"].ToString(),
                            Sobrenome = rdr["Sobrenome"].ToString()
                        };

                        Funcionarios.Add(funcionarios);
                    }
                }
            }
            return Funcionarios;

        }
    }
}
