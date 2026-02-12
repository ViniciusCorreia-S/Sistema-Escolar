using System;
using System.Collections.Generic;
using System.Text;

abstract class Pessoa
{
    private string Nome { get; set; } = string.Empty;
    private int Idade { get; set; }
    private long CPF { get; set; }

    public Pessoa(string nome, int idade, long cpf)
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
    public long GetCPF()
    {
        return CPF;
    }
}
