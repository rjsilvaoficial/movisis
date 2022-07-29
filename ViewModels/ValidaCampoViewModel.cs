using MovisisCadastro.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MovisisCadastro.ViewModels
{
    public class ValidaCampoViewModel
    {
        public ValidaCampoViewModel(bool sucesso, string mensagem,IEnumerable<string> erros)
        {
            Sucesso = Convert.ToInt32(sucesso);
            Mensagem = mensagem;
            Data = erros;
        }

        public int Sucesso { get; }
        public string Mensagem { get; }
        public IEnumerable<string> Data { get; private set; }


    }
}
