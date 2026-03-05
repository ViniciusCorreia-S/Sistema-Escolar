using System;

public class Turma
{
    public string NomeTurma { get; private set; }

    private List<Aluno> alunos = new List<Aluno>();
    public IReadOnlyList<Aluno> Alunos => alunos;

    public Turma(string nomeTurma)
    {
        NomeTurma = nomeTurma;
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