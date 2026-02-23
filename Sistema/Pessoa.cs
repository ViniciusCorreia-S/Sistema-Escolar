using System;
using System.Collections.Generic;
using System.Text;

abstract class Pessoa
{
    private string Nome { get; set; } = string.Empty;
    private int Idade { get; set; }
    private string CPF { get; set; }

    public Pessoa(string nome, int idade, string cpf)
    {
        Nome = nome;
        Idade = idade;
        CPF = cpf;
    }

    public string GetNome()
    {
        return Nome;
    }
    public int GetIdade()
    {
        return Idade;
    }
    public string GetCPF()
    {
        return CPF;
    }
}
