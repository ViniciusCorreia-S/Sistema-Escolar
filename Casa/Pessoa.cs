using System;
using System.Collections.Generic;
using System.Text;

class Pessoa
{
    private string Nome { get; set; } = string.Empty;
    private int Idade { get; set; }
    private long CPF { get; set; }

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
