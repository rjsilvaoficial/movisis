using MovisisCadastro.Models;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MovisisCadastro.ViewModels
{
    public class RespostaClienteViewModel
    {
        public RespostaClienteViewModel(bool sucesso, string mensagem)
        {
            Sucesso = Convert.ToInt32(sucesso);
            Mensagem = mensagem;
        }

        public int Sucesso { get; }
        public string Mensagem { get; }

        public List<Cliente> Data { get; set; } = new List<Cliente>();


    }
}
