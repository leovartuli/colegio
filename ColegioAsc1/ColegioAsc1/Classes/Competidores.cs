using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Colegio.Classes
{
    public class Competidores  //classe auxiliar para requisição
    {
        string nome;
        float mediaCompeticao;
        float notaProva;

        //setters e getters

        public void setNome(string i)
        {
            nome = i;
        }

        public void setMediaCompeticao(float i)
        {
            mediaCompeticao = i;
        }

      

        public void setNotaProva(float i)
        {
            notaProva = i;
        }

        public float MediaCompeticao

        {
            get { return mediaCompeticao; }
        }

        public string Nome

        {
            get { return nome; }
        }

        public float NotaProva

        {
            get { return notaProva; }
        }




    }
}
