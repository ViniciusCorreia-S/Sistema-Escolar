using System;
using System.Collections.Generic;
using System.Text;

class Aluno
{
    private string Nome { get; set; } = string.Empty;
    private int Idade { get; set; }
    private string Turma { get; set; } = string.Empty;
    private List<double> Notas { get; set; } = new List<double>();

    public string GetNome()
    {
        return Nome;
    }
    public int GetIdade()
    {
        return Idade;
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