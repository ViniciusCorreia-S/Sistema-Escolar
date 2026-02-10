using System;
using System.Collections.Generic;
using System.Text;

class Professor : Pessoa
{
    private string Disciplina { get; set; } = string.Empty;
    private List<decimal> Salarios { get; set; } = new List<decimal>();
    private List<string> Turmas { get; set; } = new List<string>();

    public Professor(string nome, int idade, long cpf, string disciplina,
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
