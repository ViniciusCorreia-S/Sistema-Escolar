using System;
using System.Collections.Generic;
using System.Text;

class Turma
{
	private char NomeTurma { get; set; }
	private List<string> AlunosTurma { get; set; } = new List<string>();

	public Turma() { }

	public Turma(char nometurma, List<string> alunosturma)
	{
		NomeTurma = nometurma;
		AlunosTurma = alunosturma;
	}

	public char GetNomeTurma()
	{
		return NomeTurma;
	}
	public List<string> GetAlunosTurma()
	{
		return AlunosTurma;
	}

}