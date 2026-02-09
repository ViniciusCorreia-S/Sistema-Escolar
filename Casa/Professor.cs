using System;
using System.Collections.Generic;
using System.Text;

class Professor
{
    private string Nome { get; set; } = string.Empty;
    private int Idade { get; set; }
    private string Disciplina { get; set; } = string.Empty;
    private List<decimal> Salarios { get; set; } = new List<decimal>();
    private List<string> Turmas { get; set; } = new List<string>();

    public string GetNome()
    {
        return Nome;
    }
    public int GetIdade()
    {
        return Idade;
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
