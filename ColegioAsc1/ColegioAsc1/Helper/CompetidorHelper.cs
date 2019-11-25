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
    public class CompetidorHelper : IComparer
    {


        int IComparer.Compare(object x, object y)
        {
            Aluno aluno1 = (Aluno)x;
            Aluno aluno2 = (Aluno)y;
            if (aluno1.MediaCompeticao > aluno2.MediaCompeticao)
                return 1;
            if (aluno1.MediaCompeticao < aluno2.MediaCompeticao)
                return -1;
            else
                return 0;
        }
    }
}