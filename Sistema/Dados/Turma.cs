using System;
using System.Collections.Generic;
using System.Text;

class Turma
{
    public string NomeTurma { get; set; }
    public List<Aluno> alunos { get; set; } = new List<Aluno>();

    public Turma(string nomeTurma)
    {
        NomeTurma = nomeTurma;
        alunos = new List<Aluno>();
    }

    public string GetNomeTurma()
    {
        return NomeTurma;
    }

    public List<Aluno> GetAlunos()
    {
        return alunos;
    }

    public void AdicionarAluno(Aluno aluno)
    {
        alunos.Add(aluno);
    }

    public void RemoverAluno(Aluno aluno)
    {
        alunos.Remove(aluno);
    }
}