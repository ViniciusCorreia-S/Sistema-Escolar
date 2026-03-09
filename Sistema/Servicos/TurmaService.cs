using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Spectre.Console;
using System.Text.Json;

public static class TurmaService
{

	//===================== LISTA DE DADOS =====================
    private static List<Turma> turmas = TurmasRepository.CarregarTurmas();
    public static IReadOnlyList<Turma> Turmas => turmas;

    //===================== MENU DE TURMAS ======================================================
    public static void MenuTurmas()
	{
		while (true)
		{
			Console.Clear();
            AnsiConsole.Write(new Rule("[bold green]GESTÃO DE TURMAS[/]"));

            var opcao = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Selecione uma ação:")
					.PageSize(10)
					.AddChoices(new[] {
					"1. Abrir Nova Turma",
					"2. Turmas Abertas",
					"3. Fechar Turma",
					"0. Voltar"
					}));

			if (opcao.StartsWith("0")) break;

			switch (opcao[0])
			{
				case '1': AbrirNovaTurma(); break;
				case '2': ListarTurmas(); break;
				case '3': FecharTurmas(); break;
			}

			AnsiConsole.MarkupLine("\n[italic grey]Pressione qualquer tecla para retornar ao menu...[/]");
			Console.ReadKey();
		}
	}

	//===================== ABRIR TURMAS ==========================================
	static void AbrirNovaTurma()
	{
		var nomeTurma = AnsiConsole.Prompt(new TextPrompt<string>("Escolha um nome para a nova turma:")
							.ValidationErrorMessage("[red]Nome inválido.[/]")
						);

		bool turmaExiste = turmas.Any(t => t.NomeTurma == nomeTurma);

		if (turmaExiste)
		{
			AnsiConsole.MarkupLine($"\n[red]Já existe uma turma com o nome [bold]{nomeTurma}[/]![/]");
			return;
		}

		turmas.Add(new Turma(nomeTurma));

		TurmasRepository.SalvarTurmas();

		AnsiConsole.MarkupLine($"\n [green] ☑ Turma [bold]{nomeTurma}[/] aberta com sucesso![/]");
	}

	//===================== LISTA TURMAS ==========================================
	static void ListarTurmas()
	{
		if (turmas.Count == 0)
		{
			AnsiConsole.MarkupLine("\n[yellow]! Nenhuma turma foi aberta até o momento.[/]");
			return;
		}

		var table = new Table();

		table.Border(TableBorder.Rounded).Expand();
		table.AddColumn("[blue]Turma[/]");
		table.AddColumn("[green]Qtd. Alunos[/]");
		table.AddColumn("[yellow]Alunos[/]");

		foreach (var turma in turmas)
		{
			var alunosDaTurma = turma.Alunos;

			string listaAlunos = alunosDaTurma.Count > 0
				? string.Join(", ", alunosDaTurma.Select(a => a.Nome))
				: "[grey]Nenhum aluno[/]";

			table.AddRow(
				$"[bold]{turma.NomeTurma}[/]",
				alunosDaTurma.Count.ToString(),
				listaAlunos
			);
		}
		AnsiConsole.Write(table);
	}

    //===================== LISTA TURMAS ==========================================
    static void FecharTurmas()
	{
		if (turmas.Count == 0)
		{
			AnsiConsole.MarkupLine("\n[yellow]! Nenhuma turma foi aberta até o momento.[/]");
			return;
		}

        var TurmaSelecionada = AnsiConsole.Prompt(
            new SelectionPrompt<Turma>()
                .Title("Selecione a [blue]turma[/] que sera removida:")
                .AddChoices(turmas)
                .UseConverter(t => $"Turma: {t.NomeTurma} | Qtd. Alunos: {t.Alunos.Count.ToString()}")
        );

        var confirmar = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Tem certeza que deseja remover esta turma?")
                .AddChoices(new[] {
                        "1. Sim",
                        "0. Não"
                })
        );

        if (confirmar.StartsWith("0"))
            return;

		AlunoService.RemoverAlunosDaTurma(TurmaSelecionada.NomeTurma);

		turmas.Remove(TurmaSelecionada);

		TurmasRepository.SalvarTurmas();

		AnsiConsole.MarkupLine($"\n [green] ☑ Turma [bold]{TurmaSelecionada.NomeTurma}[/] fechada com sucesso![/]");
    }
}
