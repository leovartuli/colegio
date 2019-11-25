using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Colegio.Classes;

namespace Colegio.Helper
{
    public class SQL
    {
        private static readonly string stringConexao = "datasource = 127.0.0.1; port=3306;username=root;password=;database=colegioBanco;";
      

        public static int insereAluno(int i, string nome, float mediafinal, string nometurma, float mediacompeticao, bool pf, int status, string nomeTurma)
        {
            using (MySqlConnection con = new MySqlConnection(stringConexao))
            {

                try
                {
                    con.Open();

                    //faz o insert de aluno no banco

                    MySqlCommand comando = new MySqlCommand("INSERT INTO aluno (Id_Aluno, Nome, Media_Final, Media_Competicao, Pf, Condicao, NomeTurma) VALUES (@idaluno, @nome, @mediafinal, @mediacompeticao, @pf, @condicao, @turma) ", con);
                    comando.Parameters.AddWithValue("@idaluno", i);
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@mediafinal", mediafinal);
                    comando.Parameters.AddWithValue("@mediacompeticao", mediacompeticao);
                    comando.Parameters.AddWithValue("@turma", nomeTurma);

                    if (pf)
                    {
                        comando.Parameters.AddWithValue("@pf", 1);
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@pf", 0);
                    }

                    if (status == 3)
                    {
                        comando.Parameters.AddWithValue("@condicao", 1);
                    }
                    else 
                    {
                        comando.Parameters.AddWithValue("@condicao", 0);
                    }

                    return comando.ExecuteNonQuery();
                   
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

            }

        }

        public static int insereParticipa(int indexAluno)
        {
            using (MySqlConnection con = new MySqlConnection(stringConexao))
            {

                try
                {
                    con.Open();

                    //faz o insert da tabela participa, que contém os alunos que vão fazer a competição.

                    MySqlCommand comando = new MySqlCommand("INSERT INTO participa (fk_Aluno_Id_Aluno) VALUES (@indexaluno) ", con);
                    comando.Parameters.AddWithValue("@indexaluno", indexAluno);

                    return comando.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

            }

        }

        public static int insereNota(List<float> nota, int i, int tipo)
        {
            using (MySqlConnection con = new MySqlConnection(stringConexao))
            {

                try
                {
                    con.Open();
                    //faz o insert de nota no banco

                    MySqlCommand comando = new MySqlCommand("INSERT INTO nota(Nota, Tipo_Prova, fk_Aluno_Id_Aluno) VALUES(@nota, @tipo, @fk) ", con);
                    if (tipo == 4)
                        comando.Parameters.AddWithValue("@nota", nota.Last());
                    else
                        comando.Parameters.AddWithValue("@nota", nota.ElementAt(tipo));
                    
                    comando.Parameters.AddWithValue("@tipo", tipo);
                    comando.Parameters.AddWithValue("@fk", i);

                    return comando.ExecuteNonQuery(); 

                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

            }

        }

        public static int insereCompeticao(int i,string ano, string vencedor, float mediacompeticao)
        {
            using (MySqlConnection con = new MySqlConnection(stringConexao))
            {
                try  //faz a inserção da competição no banco
                {
                    con.Open();

                    MySqlCommand comando = new MySqlCommand("INSERT INTO competicao (Id_Competicao, Ano, Vencedor, Desempenho) VALUES(@idcompeticao, @ano, @vencedor, @mediacompeticao) ", con);
                    comando.Parameters.AddWithValue("@idcompeticao", i);
                    comando.Parameters.AddWithValue("@ano", ano);
                    comando.Parameters.AddWithValue("@vencedor", vencedor);
                    comando.Parameters.AddWithValue("@mediacompeticao", mediacompeticao);

                    return comando.ExecuteNonQuery(); 

                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

            }

        }


        public static List<Aluno> listaAluno()
        {
            using (MySqlConnection con = new MySqlConnection(stringConexao))
            {

                try  //lista todos os 60 alunos do banco
                {
                    con.Open();
                    List<Aluno> alunos = new List<Aluno>(); //array de alunos que irá ser enviado.

                    
                    for (int i = 1; i <= 60; i++)
                    {
                        
                        MySqlCommand comando = new MySqlCommand("SELECT Nome, Media_Final, Media_Competicao, Pf, NomeTurma FROM aluno WHERE Id_Aluno = @Id_Aluno", con);
                        comando.Parameters.AddWithValue("@Id_Aluno", i);

                        MySqlDataReader reader = comando.ExecuteReader();
                                                                           
                        Aluno aluno = new Aluno("a", "X");
                        while (reader.Read())
                        { 
                                int index = 0;
                                aluno.setNome(reader.GetString(index++));

                                aluno.setMediaFinal(reader.GetFloat(index++));
                                aluno.setMediaCompeticao(reader.GetFloat(index++));
                                if (reader.GetInt32(index) == 1)
                                  aluno.setPf(true);
                                else
                                   aluno.setPf(false);
                                index++;
                                aluno.setNomeTurma(reader.GetString(index++));

                                alunos.Add(aluno);
                        }
                            reader.Close();
                        
                    }

                    return alunos; 

                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

            }

        }

        public static List<Aluno> listaAlunoNotaFinal()
        {
            using (MySqlConnection con = new MySqlConnection(stringConexao))
            {

                try 
                {
                    con.Open();

                    List<Aluno> alunos = new List<Aluno>();
                    for (int i = 1; i <= 60; i++) 
                    {

                        //pega do banco as informações dos alunos
                        MySqlCommand comando = new MySqlCommand("SELECT Nome, Media_Final, Media_Competicao, Pf, NomeTurma FROM aluno WHERE Id_Aluno = @Id_Aluno", con);
                        comando.Parameters.AddWithValue("@Id_Aluno", i);
                        MySqlDataReader reader = comando.ExecuteReader();


                        Aluno aluno = new Aluno("a", "X");

                        while (reader.Read())
                        {
                            int index = 0;

                            aluno.setNome(reader.GetString(index++));
                            aluno.setMediaFinal(reader.GetFloat(index++));
                            aluno.setMediaCompeticao(reader.GetFloat(index++));
                            int g = reader.GetInt32(index++);
                            if (g == 1)
                                aluno.setPf(true);
                            else
                                aluno.setPf(false);

                            aluno.setNomeTurma(reader.GetString(index++));

                            aluno.calculaStatus();
                            alunos.Add(aluno);

                        }
                        reader.Close();

                    }

                    //pega suas respectivas notas de provas do banco

                    for (int j = 1; j <= 60; j++)
                    {
                        MySqlCommand comandoA = new MySqlCommand("SELECT Nota FROM nota WHERE fk_Aluno_Id_Aluno = @Id_Aluno ORDER BY Tipo_Prova", con);
                        comandoA.Parameters.AddWithValue("@Id_Aluno", j);

                        MySqlDataReader readerA = comandoA.ExecuteReader();
                        while (readerA.Read())
                        {
                            int indexA = 0;

                            alunos.ElementAt(j - 1).insereNota(readerA.GetFloat(indexA));

                        }
                        readerA.Close();

                    }
                    return alunos;         //retorna os alunos e suas notas e medias
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

            }

        }

        public static List<Competidores> competidores()
        {
            using (MySqlConnection con = new MySqlConnection(stringConexao))
            {

                try 
                {
                    con.Open();
                    
                    List<int> id = new List<int>();
                    List<Competidores> competidores = new List<Competidores>();

                    for (int i = 1; i <= 5; i++) { //seleciona os 5 competidores do banco

                        MySqlCommand comando = new MySqlCommand("SELECT fk_Aluno_Id_Aluno FROM participa WHERE Id_Participa = @id", con);
                        comando.Parameters.AddWithValue("@id", i);
                        MySqlDataReader reader = comando.ExecuteReader();

                        while (reader.Read())
                        {
                            int index = 0;

                            id.Add(reader.GetInt32(index++));


                        }
                        reader.Close();

                    }


                    for (int j = 0; j < 5; j++) //pega suas notas de provas do banco
                    {
                        Competidores competidor = new Competidores();
                        MySqlCommand comandoA = new MySqlCommand("SELECT Nota FROM nota WHERE fk_Aluno_Id_Aluno = @Id_Aluno ORDER BY Tipo_Prova DESC LIMIT 1", con);
                        comandoA.Parameters.AddWithValue("@Id_Aluno", id.ElementAt(j));
                        MySqlDataReader readerA = comandoA.ExecuteReader();

                        while (readerA.Read())
                        {
                            int indexA = 0;

                            competidor.setNotaProva(readerA.GetFloat(indexA));

                            competidores.Add(competidor);
                        }
                        readerA.Close();

                    }

                    for (int j = 1; j <= 5; j++)
                    {

                        //pega seus respectivos nome e media da competicao
                        MySqlCommand comandoB = new MySqlCommand("SELECT Nome, Media_Competicao FROM aluno WHERE Id_Aluno = @Id_Aluno ", con);
                        comandoB.Parameters.AddWithValue("@Id_Aluno", id.ElementAt(j-1));
                        MySqlDataReader readerB = comandoB.ExecuteReader();

                        while (readerB.Read())
                        {
                            int indexA = 0;

                            competidores.ElementAt(j - 1).setNome(readerB.GetString(indexA++));
                            competidores.ElementAt(j - 1).setMediaCompeticao(readerB.GetFloat(indexA));

                        }
                        readerB.Close();


                    }
                    //envia os competidores
                    return competidores;
                        
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

            }

        }

        public static Competidores vencedor()
        {
            using (MySqlConnection con = new MySqlConnection(stringConexao))
            {

                try
                {
                    con.Open();

                    string nome = "";
                    int id = -1;
                    Competidores competidor = new Competidores();

                    //seleciona o aluno vencedor inserido na tabela competicao do banco
                    MySqlCommand comando = new MySqlCommand("SELECT Vencedor, Desempenho FROM competicao", con);
                    MySqlDataReader reader = comando.ExecuteReader();

                    while (reader.Read())
                    {
                        int indexA = 0;

                        nome = reader.GetString(indexA);
                        competidor.setNome(reader.GetString(indexA++));
                        competidor.setMediaCompeticao(reader.GetFloat(indexA));

                    }
                    reader.Close();

                    //pega o id do vencedor
                    MySqlCommand comandoB = new MySqlCommand("SELECT Id_Aluno FROM aluno WHERE Nome = @nome ", con);
                    comandoB.Parameters.AddWithValue("@nome", nome);

                    MySqlDataReader readerB = comandoB.ExecuteReader();
                    while (readerB.Read())
                    {
                        int indexA = 0;

                        id = readerB.GetInt32(indexA++);

                    }
                    readerB.Close();


                    //pega sua nota na prova especial 
                    MySqlCommand comandoA = new MySqlCommand("SELECT Nota FROM nota WHERE fk_Aluno_Id_Aluno = @Id_Aluno ORDER BY Tipo_Prova DESC LIMIT 1", con);
                    comandoA.Parameters.AddWithValue("@Id_Aluno", id);

                    MySqlDataReader readerA = comandoA.ExecuteReader();
                    while (readerA.Read())
                    {
                        int indexA = 0;

                        competidor.setNotaProva(readerA.GetFloat(indexA));

                    }
                    readerA.Close();

                    //envia o vencedor, com sua media da competicao e nota da prova
                    return competidor;

                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    con.Close();
                }

            }

        }

    }
}
