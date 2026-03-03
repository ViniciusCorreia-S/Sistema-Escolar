using System;
using System.Collections.Generic;
using System.Text;

public class Pessoa
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public string CPF { get; set; } = string.Empty;

	public Pessoa() { }

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
