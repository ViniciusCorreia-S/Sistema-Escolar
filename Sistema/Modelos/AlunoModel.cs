using System;

public class Aluno : Pessoa
{
    public string Turma { get; private set; } = string.Empty;
    
    private List<double> notas = new();
    public IReadOnlyList<double> Notas => notas;

    public Aluno() { }

    public Aluno(string nome, int idade, string cpf, string turma) 
        : base(nome, idade, cpf)
    {
        Turma = turma;
    }

    public void AdicionarNota(double nota)
    {
        notas.Add(nota);
    }

    public void RemoverNota(double nota)
    {
        notas.Remove(nota);
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