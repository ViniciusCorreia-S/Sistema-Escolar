using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Spectre.Console;

public static class EstatisticaService
{
	//===================== ESTATISTICAS GERAIS ======================================================
	public static void ExibirEstatisticas()
	{

		Console.Clear();

		var regra = new Rule("[bold red]ESTATÍSTICAS GERAIS[/]");
		regra.RuleStyle("grey");
		AnsiConsole.Write(regra);

		var grid = new Grid().AddColumns(2);

		var table = new Table().Border(TableBorder.Rounded).Expand();
		table.AddColumn("Tipo").AddColumn("Total");
		table.AddRow("Turmas", $"[blue]{TurmaService.turmas.Count}[/]");
		table.AddRow("Alunos", $"[blue]{AlunoService.alunos.Count}[/]");
		table.AddRow("Professores", $"[green]{ProfessorService.professores.Count}[/]");

		if (AlunoService.alunos.Count > 0)
		{
			double media = AlunoService.alunos.Average(a => a.CalcularMedia());
			grid.AddRow(table, new Panel($"[bold]Média Acadêmica:[/] {media:F2}").Expand());
		}
		else
		{
			grid.AddRow(table, new Panel("[yellow]Sem dados de média[/]"));
		}

		//grid.AddRow(new Panel($"[bold]Ultima Atualização:[/] {DateTime.Now:dd/MM/yyyy HH:mm}").Expand());
		AnsiConsole.Write(grid);
	}
}
