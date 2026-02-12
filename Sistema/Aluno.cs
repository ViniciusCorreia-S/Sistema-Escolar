using System;
using System.Collections.Generic;
using System.Text;

class Aluno : Pessoa
{
    private string Turma { get; set; } = string.Empty;
    private List<double> Notas { get; set; } = new List<double>();

    public Aluno(string nome, int idade, long cpf, string turma,
        List<double> notas) 
        : base(nome, idade, cpf)
    {
        Turma = turma;
        Notas = notas;
    }

    public string GetTurma()
    {
        return Turma;
    }
    public List<double> GetNotas()
    {
        return Notas;
    }

    public double CalcularMedia()
    {
        if (Notas.Count == 0) return 0;
        double soma = 0;
        foreach (var nota in Notas)
            soma += nota;
        return soma / Notas.Count;
    }
}