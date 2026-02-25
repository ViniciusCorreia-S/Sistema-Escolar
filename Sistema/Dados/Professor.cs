using System;
using System.Collections.Generic;
using System.Text;

class Professor : Pessoa
{
    public string Disciplina { get; set; } = string.Empty;
    public List<decimal> Salarios { get; set; } = new List<decimal>();
    public List<string> Turmas { get; set; } = new List<string>();

    public Professor() { }

    public Professor(string nome, int idade, string cpf, string disciplina,
        List<decimal> salarios, List<string> turmas) 
        : base(nome, idade, cpf)
    {
        Disciplina = disciplina;
        Salarios = salarios;
        Turmas = turmas;
    }

    public string GetDisciplina()
    {
        return Disciplina;
    }
    public List<decimal> GetSalarios()
    {
        return Salarios;
    }
    public List<string> GetTurmas()
    {
        return Turmas;
    }
}
