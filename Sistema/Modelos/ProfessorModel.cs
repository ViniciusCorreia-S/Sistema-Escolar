using System;
using System.Collections.Generic;

public class Professor : Pessoa
{
    public string Disciplina { get; private set; } = string.Empty;

    private List<decimal> salarios = new List<decimal>();
    public IReadOnlyList<decimal> Salarios => salarios;

    private List<string> turmas = new List<string>();
    public IReadOnlyList<string> Turmas => turmas;

    public Professor() { }

    public Professor(string nome, int idade, string cpf, string disciplina)
        : base(nome, idade, cpf)
    {
        Disciplina = disciplina;
    }

    public void AdicionarSalario(decimal salario)
    {
        salarios.Add(salario);
    }

    public void AdicionarTurma(string turma)
    {
        turmas.Add(turma);
    }
}