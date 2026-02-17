using System;
using System.Collections.Generic;
using System.Globalization;
using Spectre.Console;

//MELHORIAS FUTURAS:
// - Implementar exclusão de alunos/professores
// - Validacao de dados no cpf e mascara (11 dígitos, apenas números)
// - Validacao no salario de professores (não pode ser negativo e deve ter limite)
// - Adicionar funcionalidade de turmas (vincular alunos a turmas e professores a turmas)

class Program
{
	static List<Aluno> alunos = new List<Aluno>();
	static List<Professor> professores = new List<Professor>();


	//MENU PRINCIAL

	static void Main()
	{

		bool continuar = true;

		while (continuar)
		{
			Console.Clear();

			var header = new Panel(new Text("SISTEMA DE GESTÃO ESCOLAR", new Style(Color.Green, Color.Black, Decoration.Bold)))
				.Border(BoxBorder.Double)
				.Expand();

			AnsiConsole.Write(header);
			AnsiConsole.Write(new Rule("[yellow]Menu Principal[/]"));
			AnsiConsole.WriteLine();

			var opcao = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Selecione uma [green]opção[/]:")
					.PageSize(10)
					.MoreChoicesText("[grey](Mova para cima e para baixo para revelar mais opções)[/]")
					.AddChoices(new[] {
					"1. Gestão de Alunos",
					"2. Gestão de Professores",
					"3. Estatísticas Gerais",
					"0. Sair"
					}));

			switch (opcao)
			{
				case "1. Gestão de Alunos":
					MenuAlunos();
					break;

				case "2. Gestão de Professores":
					MenuProfessores();
					break;

				case "3. Estatísticas Gerais":
					ExibirEstatisticas();
					break;

				case "0. Sair":
					continuar = false;
					EncerrarSistema();
					break;
			}

			if (continuar)
			{
				AnsiConsole.MarkupLine("\n[grey]Pressione qualquer tecla para voltar ao menu...[/]");
				Console.ReadKey(true);
			}
		}
	}

	static void EncerrarSistema()
	{
		AnsiConsole.Status()
			.Start("Encerrando o sistema...", ctx => {
				Thread.Sleep(1000);
			});

		AnsiConsole.Write(new FigletText("Ate Logo!").Color(Color.Blue));
	}

	static void DestacarMensagem(string mensagem, ConsoleColor cor)
	{
		Console.ForegroundColor = cor;
		Console.WriteLine(mensagem);
		Console.ResetColor();
	}



	//ALUNOS

	static void MenuAlunos()
	{
		while (true)
		{
			Console.Clear();
			AnsiConsole.Write(new Rule("[bold green]GESTÃO DE ALUNOS[/]").RuleStyle("grey"));

			var opcao = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Selecione uma ação:")
					.AddChoices(new[] {
					"1. Cadastrar Aluno", "2. Listar Alunos",
					"3. Detalhes do Aluno", "4. Adicionar Nota",
					"0. Voltar"
					}));

			if (opcao.StartsWith("0")) break;

			switch (opcao[0])
			{
				case '1': CadastrarAluno(); break;
				case '2': ListarAlunos(); break;
				case '3': ExibirDetalheAluno(); break;
				case '4': AdicionarNota(); break;
			}

			AnsiConsole.MarkupLine("\n[grey]Pressione qualquer tecla para continuar...[/]");
			Console.ReadKey(true);
		}
	}

	static void CadastrarAluno()
	{
		AnsiConsole.Write(new Rule("[bold blue]CADASTRO DE NOVO ALUNO[/]"));

		var nome = AnsiConsole.Ask<string>("Nome do aluno:");
		var cpf = AnsiConsole.Ask<long>("CPF (somente números):");
		var idade = AnsiConsole.Prompt(new TextPrompt<int>("Idade:").ValidationErrorMessage("[red]Idade inválida[/]"));
		var turma = AnsiConsole.Ask<string>("Turma:");

		alunos.Add(new Aluno(nome, idade, cpf, turma, new List<double>()));

		AnsiConsole.MarkupLine($"\n[green]✔[/] Aluno [bold]{nome}[/] cadastrado com sucesso!");
	}

	static void ExibirDetalheAluno()
	{
		if (alunos.Count == 0) return;

		var alunoSelecionado = AnsiConsole.Prompt(
			new SelectionPrompt<Aluno>()
				.Title("Selecione o aluno para ver detalhes:")
				.AddChoices(alunos)
				.UseConverter(a => $"{a.GetNome()} (Turma: {a.GetTurma()})")
		);

		var card = new Panel(new Markup(
			$"[bold]CPF:[/] {alunoSelecionado.GetCPF()}\n" +
			$"[bold]Idade:[/] {alunoSelecionado.GetIdade()} anos\n" +
			$"[bold]Média Acadêmica:[/] [yellow]{alunoSelecionado.CalcularMedia():F2}[/]\n" +
			$"[bold]Notas:[/] {string.Join(" | ", alunoSelecionado.GetNotas())}"
		));
		card.Header($"Ficha Cadastral: {alunoSelecionado.GetNome()}");
		card.BorderColor(Color.Blue);

		AnsiConsole.Write(card);
	}

	static void ListarAlunos()
	{
		if (alunos.Count == 0)
		{
			AnsiConsole.MarkupLine("[yellow]![/] Nenhum aluno cadastrado.");
			return;
		}

		var tabela = new Table().Border(TableBorder.Rounded);
		tabela.AddColumn("[blue]ID[/]");
		tabela.AddColumn("[blue]Nome[/]");
		tabela.AddColumn("[blue]Turma[/]");
		tabela.AddColumn("[blue]Média[/]");

		for (int i = 0; i < alunos.Count; i++)
		{
			tabela.AddRow(
				(i + 1).ToString(),
				alunos[i].GetNome() ?? "[red]N/A[/]",
				alunos[i].GetTurma() ?? "[red]N/A[/]",
				$"[green]{alunos[i].CalcularMedia():F2}[/]"
			);
		}

		AnsiConsole.Write(tabela);
	}

	static void AdicionarNota()
	{
		Console.Clear();
		Console.WriteLine("╔═════════════════════════════════════╗");
		AnsiConsole.MarkupLine("║           [bold blue]ADICIONAR NOTA[/]            ║");
		Console.WriteLine("╚═════════════════════════════════════╝");

		if (alunos.Count == 0)
		{
			DestacarMensagem("\nNenhum aluno cadastrado.", ConsoleColor.Yellow);
			return;
		}

		ListarAlunos();
		int indice = ReadIndexChoice("\nDigite o [blue]número de ID[/] do aluno: ", alunos.Count);

		double nota = ReadDoubleInRange("\nDigite a [blue]nota[/] a ser adicionada (0 a 10): ", 0, 10);
		alunos[indice].GetNotas().Add(nota);
		DestacarMensagem($"\nNota adicionada com sucesso para {alunos[indice].GetNome()}!", ConsoleColor.Green);
	}


	//PROFESSORES

	static void MenuProfessores()
	{
		while (true)
		{
			Console.Clear();
			AnsiConsole.Write(new Rule("[bold green]GESTÃO DE PROFESSORES[/]").RuleStyle("grey"));

			var opcao = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Selecione uma ação:")
					.PageSize(10)
					.AddChoices(new[] {
					"1. Cadastrar Professor",
					"2. Listar Professores",
					"3. Atualizar Salário",
					"0. Voltar"
					}));

			if (opcao.StartsWith("0")) break;

			switch (opcao[0])
			{
				case '1': CadastrarProfessor(); break;
				case '2': ListarProfessores(); break;
				case '3': SalarioProfessor(); break;
			}

			AnsiConsole.MarkupLine("\n[grey]Pressione qualquer tecla para continuar...[/]");
			Console.ReadKey(true);
		}
	}

	static void CadastrarProfessor()
	{
		AnsiConsole.Write(new Rule("[bold blue]NOVO REGISTRO DE PROFESSOR[/]"));

		var nome = AnsiConsole.Ask<string>("Nome completo:");
		var cpf = AnsiConsole.Ask<long>("CPF (somente números):");
		var idade = AnsiConsole.Prompt(
			new TextPrompt<int>("Idade:")
				.ValidationErrorMessage("[red]Por favor, insira uma idade válida.[/]")
				.Validate(age => age >= 18 ? ValidationResult.Success() : ValidationResult.Error("[red]O professor deve ser maior de idade.[/]"))
		);
		var disciplina = AnsiConsole.Ask<string>("Disciplina/Matéria:");

		Professor novoProfessor = new(nome, idade, cpf, disciplina, new List<decimal>(), new List<string>());
		professores.Add(novoProfessor);

		AnsiConsole.MarkupLine($"\n[green]✔[/] Professor [bold]{nome}[/] cadastrado com ID: [blue]{professores.Count}[/]");
	}

	static void ListarProfessores()
	{
		if (professores.Count == 0)
		{
			AnsiConsole.MarkupLine("[yellow]![/] Nenhum professor cadastrado no sistema.");
			return;
		}

		var tabela = new Table().Border(TableBorder.Rounded).Expand();
		tabela.AddColumn("[blue]ID[/]");
		tabela.AddColumn("[blue]Nome[/]");
		tabela.AddColumn("[blue]Disciplina[/]");
		tabela.AddColumn("[blue]Último Salário[/]");

		for (int i = 0; i < professores.Count; i++)
		{
			var p = professores[i];
			decimal ultimoSalario = p.GetSalarios().Count > 0 ? p.GetSalarios()[^1] : 0;

			tabela.AddRow(
				(i + 1).ToString(),
				p.GetNome(),
				$"[italic]{p.GetDisciplina()}[/]",
				$"[green]{ultimoSalario:C}[/]" 
			);
		}

		AnsiConsole.Write(tabela);
	}
	static void AdicionarTurma()
	{
		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║           ADICIONAR TURMA           ║");
		Console.WriteLine("╚═════════════════════════════════════╝");
	}


	static void SalarioProfessor()
	{
		if (professores.Count == 0)
		{
			AnsiConsole.MarkupLine("[yellow]Nenhum professor cadastrado.[/]");
			return;
		}

		var prof = AnsiConsole.Prompt(
			new SelectionPrompt<Professor>()
				.Title("Atualizar salário de qual professor?")
				.AddChoices(professores)
				.UseConverter(p => p.GetNome())
		);

		var novoSalario = AnsiConsole.Ask<decimal>($"Novo salário para [green]{prof.GetNome()}[/]:");

		AnsiConsole.Status()
			.Start("Atualizando folha de pagamento...", ctx => {
				Thread.Sleep(800);
				prof.GetSalarios().Add(novoSalario);
			});

		AnsiConsole.MarkupLine("[green]✔[/] Salário atualizado com sucesso!");
	}

	//ESTATISTICAS
	static void ExibirEstatisticas()
	{

		Console.Clear();

		var regra = new Rule("[bold red]ESTATÍSTICAS GERAIS[/]");
		regra.RuleStyle("grey");
		AnsiConsole.Write(regra);

		var grid = new Grid().AddColumns(2);

		var table = new Table().Border(TableBorder.Rounded);
		table.AddColumn("Tipo").AddColumn("Total");
		table.AddRow("Alunos", $"[blue]{alunos.Count}[/]");
		table.AddRow("Professores", $"[green]{professores.Count}[/]");

		if (alunos.Count > 0)
		{
			double media = alunos.Average(a => a.CalcularMedia());
			grid.AddRow(table, new Panel($"[bold]Média Acadêmica:[/] {media:F2}").Expand());
		}
		else
		{
			grid.AddRow(table, new Text("Sem dados de média"));
		}

		AnsiConsole.Write(grid);
		AnsiConsole.WriteLine();

		AnsiConsole.MarkupLine("[italic grey]Pressione qualquer tecla para retornar...[/]");
		Console.ReadKey();

	}

	static string ReadNonEmptyString(string prompt)
	{
		while (true)
		{
			AnsiConsole.MarkupLine(prompt);

			var entrada = Console.ReadLine()?.Trim();

			if (!string.IsNullOrEmpty(entrada)) return entrada;
			DestacarMensagem("Entrada vazia. Tente novamente.", ConsoleColor.Yellow);
		}
	}

	static int ReadIntInRange(string prompt, int min, int max)
	{
		while (true)
		{
			AnsiConsole.MarkupLine(prompt);

			var line = Console.ReadLine();
			int value;

			if (int.TryParse(line, out value) && value >= min && value <= max)
				return value;
			DestacarMensagem($"Entrada inválida. Digite um número inteiro maior que {min}.", ConsoleColor.Red);
		}
	}

	static double ReadDoubleInRange(string prompt, double min, double max)
	{
		while (true)
		{
			AnsiConsole.MarkupLine(prompt);

			var line = Console.ReadLine();
			double value;

			if (double.TryParse(line, out value) && value >= min && value <= max)
				return value;
			//if (double.TryParse(line, NumberStyles.Number, CultureInfo.CurrentCulture, out double value) && value >= min && value <= max)
			//	return value;
			DestacarMensagem($"Entrada inválida. Digite um número entre {min} e {max}.", ConsoleColor.Red);
		}
	}

	static decimal ReadDecimalInRange(string prompt, decimal min, decimal max)
	{
		while (true)
		{
			AnsiConsole.MarkupLine(prompt);

			var line = Console.ReadLine();
			decimal value;

			if (decimal.TryParse(line, out value) && value >= min && value <= max)
				return value;
			//if (double.TryParse(line, NumberStyles.Number, CultureInfo.CurrentCulture, out double value) && value >= min && value <= max)
			//	return value;
			DestacarMensagem($"Entrada inválida. Digite um número maior que {min}.", ConsoleColor.Red);
		}
	}

	static long ReadLongInRange(string prompt, long min, long max)
	{
		while (true)
		{
			AnsiConsole.MarkupLine(prompt);

			var line = Console.ReadLine();
			long value;

			if (long.TryParse(line, out value) && value >= min && value <= max)
				return value;
			//if (double.TryParse(line, NumberStyles.Number, CultureInfo.CurrentCulture, out double value) && value >= min && value <= max)
			//	return value;
			DestacarMensagem($"Entrada inválida. Digite apenas números (11 dígitos).", ConsoleColor.Red);
		}
	}

	static decimal ReadDecimalNonNegative(string prompt)
	{
		while (true)
		{
			AnsiConsole.MarkupLine(prompt);

			var line = Console.ReadLine();
			decimal value;

			if (decimal.TryParse(line, out value) && value >= 0)
				return value;
			DestacarMensagem("Entrada inválida. Digite um número decimal não-negativo.", ConsoleColor.Red);
		}
	}

	static int ReadIndexChoice(string prompt, int count)
	{
		while (true)
		{
			AnsiConsole.MarkupLine(prompt);
			var line = Console.ReadLine();
			if (int.TryParse(line, NumberStyles.Integer, CultureInfo.CurrentCulture, out int value) && value >= 1 && value <= count)
				return value - 1;
			DestacarMensagem($"Escolha inválida. Digite um número entre 1 e {count}.", ConsoleColor.Red);
		}
	}
}
