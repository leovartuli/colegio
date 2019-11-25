using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Colegio.Classes;
using Colegio.Helper;




namespace Colegio.Controllers
{

    [Route("/Colegio")]
    [ApiController]


    public class AlunoControlador : Controller
    {
        

        [Route("/front")]
        [HttpPut]               
        public IActionResult criaColegio()           //Requisição para preencher o banco.  
        {
            int linhasInseridas = 0;


            //Aqui são geradas todas as instâncias dos objetos e inserções no banco
            Aluno[] alunos = Aluno.criaAlunos(60);

                Competicao competicao = new Competicao("2019", alunos, 5);
                
                Aluno[] competidores = competicao.Competidores;
                
                Aluno[] candidatos = competicao.Candidatos;

            //Aqui ocorrem as inserções no banco.

            linhasInseridas += SQL.insereCompeticao(1,competicao.Ano, competicao.Vencedor.Nome, competicao.Vencedor.MediaCompeticao);

                                                    
                for (int i = 0; i < 60; i++) 
                {
                    linhasInseridas += SQL.insereAluno(i+1, candidatos[i].Nome, candidatos[i].MediaFinal, candidatos[i].NomeTurma, candidatos[i].MediaCompeticao, candidatos[i].Pf, candidatos[i].Status, candidatos[i].NomeTurma);
                    
                    if (candidatos[i].Competidor)
                        linhasInseridas += SQL.insereParticipa(i+1);

                    linhasInseridas += SQL.insereNota(candidatos[i].Nota, i+1, 0);
                    linhasInseridas += SQL.insereNota(candidatos[i].Nota, i+1, 1);
                    linhasInseridas += SQL.insereNota(candidatos[i].Nota, i+1, 2);

                    if (candidatos[i].Pf) {       
                        linhasInseridas += SQL.insereNota(candidatos[i].Nota, i+1, 3);

                        if (candidatos[i].Competidor)
                        {
                            linhasInseridas += SQL.insereNota(candidatos[i].Nota, i+1, 4);
                            
                        }
                    }
                    else if (candidatos[i].Competidor)
                    {
                        
                            linhasInseridas += SQL.insereNota(candidatos[i].Nota, i+1, 4);
                        
                    }



                

                }


            if (linhasInseridas > 0)
            {
                return Ok(new { mensagem = "Colegio criado!" });
            }

            return StatusCode(406, new { mensagem = "Erro no envio: Colegio não criado" });

        }

        

        [Route("/aluno")]
        [HttpGet]
        public IActionResult exibeAlunos()
        {

            List<Aluno> alunosA = SQL.listaAluno(); //faz uma consulta para listar o nome de todos os alunos do banco

            return Ok(alunosA);
        }

        [Route("/alunoNotaFinal")]
        [HttpGet]
        public IActionResult exibeAlunosNotaFinal()
        {

            List<Aluno> alunosA = SQL.listaAlunoNotaFinal(); //faz uma consulta para listar o nome, as notas e a media de todos os alunos do banco

            return Ok(alunosA);
        }

        [Route("/competidores")]
        [HttpGet]
        public IActionResult exibeCompetidores()
        {

            List<Competidores> alunosA = SQL.competidores(); //faz uma série de consultas e mostra os 5 competidores

            return Ok(alunosA);
        }

        [Route("/vencedor")]
        [HttpGet]
        public IActionResult exibeVencedor()
        {

            Competidores alunosA = SQL.vencedor(); //faz uma série de consultas e mostra o vencedor da competição

            return Ok(alunosA);
        }


    }
}
