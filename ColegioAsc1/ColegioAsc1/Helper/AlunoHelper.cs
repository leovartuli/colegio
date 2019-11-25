using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Colegio.Classes;
using System.Collections;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

using Newtonsoft.Json;

namespace Colegio.Helper
{
    public class AlunoHelper : IComparer 
    {


        int IComparer.Compare(object x, object y) //comparator para media de alunos
        {
            Aluno aluno1 = (Aluno)x;
            Aluno aluno2 = (Aluno)y;
            if (aluno1.MediaFinal > aluno2.MediaFinal)
                return -1;
            if (aluno1.MediaFinal < aluno2.MediaFinal)
                return 1;
            else
                return 0;
        }


        

        public static string geraNome(int i) //gerador de nomes por um txt para criação dos alunos
        {

            int counter = 0;
            string line;

            
            
            System.IO.StreamReader file = new System.IO.StreamReader(@"./nomes.txt");
            
            while ((line = file.ReadLine()) != null)
            {
                if (i == counter)
                {
                    return line;
                }

                counter++;
            }

            file.Close();
            



            return line;
        }

        
    }
}
