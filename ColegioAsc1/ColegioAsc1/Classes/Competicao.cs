using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colegio.Classes;

namespace Colegio.Classes
{
    public class Competicao
    {
        private Aluno[] competidores;
        private Aluno vencedor;
        private string ano;
        private Aluno[] candidatos;

        public Aluno[] Competidores              //getters

        {
            get { return competidores; }
        }

        public Aluno[] Candidatos

        {
            get { return candidatos; }
        }

        public Aluno Vencedor

        {
            get { return vencedor; }
        }

        public string Ano

        {
            get { return ano; }
        }


        public Competicao(string ano, Aluno[] candidatos, int vaga)   //construtor
        {

            this.ano = ano;
            this.candidatos = candidatos;
            competidores = defineCompetidores(vaga);
            this.vencedor = vence();
        }



        private Aluno[] defineCompetidores(int vaga) //define um array com 5 competidores que irão fazer a prova especial.
        {
            
            Aluno[] competidores = new Aluno[vaga];

            int i = 0;
             

            if (candidatos.Length < vaga) {
                return candidatos;
            }

            while (i < candidatos.Length)
            {

                if (i < vaga)
                {
                    competidores[i] = candidatos[i];

                }
                else if (i == vaga)
                {
                   Array.Sort(competidores, Aluno.sortAluno());
                   
                   if (compara(competidores, candidatos[i]))
                   {
                       competidores[vaga - 1] = candidatos[i];
                       
                       Array.Sort(competidores, Aluno.sortAluno());
                   }

                }
                else
                {
                   if (compara(competidores, candidatos[i]))
                   {
                       competidores[vaga - 1] = candidatos[i];
                       
                       Array.Sort(competidores, Aluno.sortAluno());
                   }
                }

                 i++;
            }

            
            return competidores;
        }


        private bool compara(Aluno[] alunos, Aluno aluno) //comparador manual de media de alunos para comparar o array de competidores
        {
            int i = 0;

            while (i < alunos.Length)
            {
                if (alunos[i].MediaFinal < aluno.MediaFinal)
                {
                    return true;
                }
                i++;
            }

            return false;
        }


        public Aluno vence() //gera a prova especial e define o vencedor da competição
        {

            for (int j = 0; j < competidores.Length; j++)
            {
                competidores[j].setCompetidor(true);
            }

            int i = 0;
            while (i < competidores.Length)
            {
                competidores[i].adicionaNota();
                mediaCompeticao(i);

                i++;
            }

            Array.Sort(competidores, Aluno.sortAlunoC());



            return competidores[4];

            
            
        }

        private void mediaCompeticao(int i) //gera a média especial da competição
        {
            float media = 0;
            if (competidores[i].Pf == true)
            {
                media += competidores[i].Nota.ElementAt(0);
                media += competidores[i].Nota.ElementAt(1);
                media += competidores[i].Nota.ElementAt(2);
                media += competidores[i].Nota.ElementAt(3);
                media += competidores[i].Nota.ElementAt(4)*2;
                media = media / 6;

                competidores[i].setMediaCompeticao(media);
            }
            else
            {
                media += competidores[i].Nota.ElementAt(0);
                media += competidores[i].Nota.ElementAt(1);
                media += competidores[i].Nota.ElementAt(2);
                media += competidores[i].Nota.ElementAt(3) * 2;
                media = media / 5;

                competidores[i].setMediaCompeticao(media);
            }

        }

        


    }
}
