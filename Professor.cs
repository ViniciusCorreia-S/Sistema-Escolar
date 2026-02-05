using System;

public class Professor : Pessoa
{
	public string? nome { get; set; }
	public int idade { get; set; }
	public List<string> Turma { get; set; }
	public float salario { get; set; }
}

public void AdicionarTurma(string turma)
{
	if (!string.IsNullOrWhiteSpace(turma))
	{
		Turmas.Add(turma);
		Console.WriteLine($"Turma '{turma}' adicionada com sucesso!");
	}
	else
	{
		throw new ArgumentException("Nome da turma não pode ser vazio.");
	}
}