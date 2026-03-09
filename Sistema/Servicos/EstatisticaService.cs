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
		table.AddRow("Turmas", $"[blue]{TurmaService.Turmas.Count}[/]");
		table.AddRow("Alunos", $"[blue]{AlunoService.Alunos.Count}[/]");
		table.AddRow("Professores", $"[green]{ProfessorService.Professores.Count}[/]");

		if (AlunoService.Alunos.Count > 0)
		{
			double media = AlunoService.Alunos.Average(a => a.CalcularMedia());
			grid.AddRow(table, new Panel($"[bold]Média Acadêmica:[/] {media:F2}").Expand());
		}
		else
		{
			grid.AddRow(table, new Panel("[yellow]Sem dados de média[/]"));
		}

		AnsiConsole.Write(grid);
	}
}
