using System;

public class Aluno : Pessoa
{
	public string? nome { get; set; }
	public int idade { get; set; }
	public string? turma { get; set; }
	public List<double> notas { get; set; }
	public float media { get; set; }
}

static void DestacarMensagem(string mensagem, ConsoleColor cor)
{
	Console.ForegroundColor = cor;
	Console.WriteLine(mensagem);
	Console.ResetColor();
}

public void AdicionarNota(double nota)
{
	if (nota >= 0 && nota <= 10)
	{
		Aluno.notas.Add(nota);
		DestacarMensagem($"Nota {nota} adicionada com sucesso!", ConsoleColor.Green);
	}
	else
	{
		throw new ArgumentException("A nota deve estar entre 0 e 10.");
	}
}

public double CalcularMedia()
{
	if (Aluno.notas.Count == 0)
		return 0;
	return Aluno.notas.Average();
}

public string ObterEstatisticas()
{
	if (Notas.Count == 0)
		return "Nenhuma nota cadastrada.";

	double media = CalcularMedia();
	return $"Média: {media:F2}\n";

}