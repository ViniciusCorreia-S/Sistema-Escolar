using System;
using System.Collections.Generic;
using System.Text;

class Turma
{
    private char NomeTurma;
    private List<Aluno> alunos;

    public Turma(char nomeTurma)
    {
        NomeTurma = nomeTurma;
        alunos = new List<Aluno>();
    }

    public char GetNomeTurma()
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