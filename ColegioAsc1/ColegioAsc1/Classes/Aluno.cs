using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colegio.Helper;

namespace Colegio.Classes
{
    public class Aluno 
    {
        private string nome;
        private List<float> nota = new List<float>(); //index 0 = primeira prova, index 1 = primeira prova,  index 2 = primeira prova, index 3 = prova final, index 4 = primeira prova,  
        private float mediaFinal; 
        private int status;       //status == 3 (Aprovado), status == 2 (Em prova final), status == 1 (Reprovado) e status == 0 (Aprovação ainda não definida) 
        private bool pf;
        private string nomeTurma;
        private float mediaCompeticao;
        private bool competidor;
        
        public float MediaFinal

        { 
            get { return mediaFinal; }
        }

        

        public string Nome

        {
            get { return nome; }
        }

        public string NomeTurma

        {
            get { return nomeTurma; }
        }

        public float MediaCompeticao

        {
            get { return mediaCompeticao; }
        }

        public void insereNota(float i) 
        {
            nota.Add(i); 
        }


        public List<float> Nota

        {
            get { return nota; }
        }

        public bool Pf

        {
            get { return pf; }
        }

        public int Status

        {
            get { return status; }
        }

        public bool Competidor

        {
            get { return competidor; }
        }


        
    

    public Aluno(string nome, string nomeTurma)                      //construtor
        {
            this.nome = nome;
            status = 0;                                //definido para 0 no começo do período letivo
            pf = false;
            this.nomeTurma = nomeTurma;
            mediaCompeticao = 0;
            competidor = false;
        }

       


        public void adicionaNota() //gera uma prova com nota aleatoria
        {
            Random rnd = new Random();
            float prova = rnd.Next(1, 100); 

            prova = prova/10;

            nota.Add(prova);
        }

        public void calculaMedia()
        {
            float media;

            if(status == 0)          //calcula media final sem prova final
            {
                media = (nota.ElementAt(0) * 10) + (nota.ElementAt(1) * 12) + (nota.ElementAt(2) * 14);
                media = media / 36;

                mediaFinal = media; 
            }
            else if(status == 2)     //calcula media final com prova final
            {
                media = mediaFinal + nota.ElementAt(3);
                media = media / 2;

                mediaFinal = media;
            }





        }

     

    public void calculaStatus()  
        {
            if (status == 0)                          //se o aluno somente completou as 3 avaliações
            {
                if (mediaFinal > 6)
                {
                    status = 3;                 //aprovado
                }
                else if (mediaFinal < 4)
                {
                    status = 1;                 //reprovado
                }
                else
                {
                    status = 2;                 //em prova final
                    pf = true;
                }
            }
            else if (status == 2)                      //se o aluno já fez as avaliações e ficou de prova final.
            {
                pf = true;
                if (mediaFinal >= 5)
                    status = 3;
                else
                    status = 1;
            }

            
        }

        //setters

        public void setMediaCompeticao(float i)
        {
            mediaCompeticao = i;
        }

        public void setMediaFinal(float i)
        {
            mediaFinal = i;
        }

        public void setNome(string i)
        {
            nome = i;
        }

        public void setPf(bool i)
        {
            pf = i;
        }

        public void setNomeTurma(string i)
        {
            nomeTurma = i;
        }

        public void setStatus(int i)
        {
            status = i;
        }

        public void setCompetidor(bool ver)
        {
            competidor = ver;
        }

        public void realizaProva()  //Realiza as provas e calcula as medias. Também realiza prova final, caso o aluno esteja em prova final. 
        {
            if(status == 0)
            {
                adicionaNota();
                adicionaNota();
                adicionaNota();
                calculaMedia();
                calculaStatus();
            }

            if (status == 2)
                adicionaNota();
                calculaMedia();
                calculaStatus();
        }

        public static IComparer sortAluno()    //comparador sort para organizar o array de competidores
        {
            return (IComparer)new AlunoHelper();
        }

        public static IComparer sortAlunoC() //outro comparador sort, especifico para a classe Competicao, para organizar o array de competidores
        {
            return (IComparer)new CompetidorHelper();
        }



        public static Aluno[] criaAlunos(int colegio)  //gera os alunos, suas respectivas provas e medias.
        {
            int i = 0;
            Aluno[] alunos = new Aluno[colegio];
            
            while (i < colegio) { 


               string nome = AlunoHelper.geraNome(i);

                if (i <= 19)
                {
                    Aluno aluno = new Aluno(nome, "A");
                    alunos[i] = aluno;
                    aluno.realizaProva();
                }
                else if (i <= 39)
                {
                    Aluno aluno = new Aluno(nome, "B");
                    alunos[i] = aluno;
                    aluno.realizaProva();
                }
                else if (i <= 59)
                {
                    Aluno aluno = new Aluno(nome, "C");
                    alunos[i] = aluno;
                    aluno.realizaProva();
                }
                i++;
            }

            return alunos;
        }

    }
}
