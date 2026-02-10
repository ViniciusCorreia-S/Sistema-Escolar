using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
	static List<Aluno> alunos = new List<Aluno>();
	static List<Professor> professores = new List<Professor>();

	static void Main()
	{
		bool continuar = true;

		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║      SISTEMA DE GESTÃO ESCOLAR      ║");
		Console.WriteLine("╚═════════════════════════════════════╝");

		while (continuar)
		{
			ExibirMenuPrincipal();
			Console.WriteLine("\nDigite o número de uma opção: ");
			string? opcao = Console.ReadLine();

			switch (opcao)
			{
				case "1":
					MenuAlunos();
					break;

				case "2":
					MenuProfessores();
					break;

				case "3":
					ExibirEstatisticas();
					break;

				case "0":
					continuar = false;
					Console.WriteLine("\nEncerrando o sistema...");
					Console.WriteLine("Obrigado por usar o Sistema de Gestão Escolar!");
					break;

				default:
					DestacarMensagem("\n   Opção inválida! Tente novamente.", ConsoleColor.Red);
					break;
			}

			if (continuar && opcao != "0")
			{
				Console.WriteLine("\nPressione qualquer tecla para continuar...");
				Console.ReadKey();
				Console.Clear();
			}
		}
	}

	static void DestacarMensagem(string mensagem, ConsoleColor cor)
	{
		Console.ForegroundColor = cor;
		Console.WriteLine(mensagem);
		Console.ResetColor();
	}

	static void ExibirMenuPrincipal()
	{
		Console.WriteLine("\n┌────────────────────────────────────────┐");
		Console.WriteLine("│          MENU PRINCIPAL                │");
		Console.WriteLine("├────────────────────────────────────────┤");
		Console.WriteLine("│ 1 - Gestão de Alunos                   │");
		Console.WriteLine("│ 2 - Gestão de Professores              │");
		Console.WriteLine("│ 3 - Estatísticas Gerais                │");
		Console.WriteLine("│ 0 - Sair                               │");
		Console.WriteLine("└────────────────────────────────────────┘");
	}

	static void MenuAlunos()
	{
		bool voltarMenu = false;

		while (!voltarMenu)
		{
			Console.Clear();
			ExibirMenuAlunos();
			Console.WriteLine("\nDigite o número de uma opção: ");
			string? opcao = Console.ReadLine();

			switch (opcao)
			{
				case "1":
					CadastrarAluno();
					break;

				case "2":
					ListarAlunos();
					break;

				case "3":
					ExibirDetalheAluno();
					break;

				case "4":
					AdicionarNota();
					break;

				case "0":
					voltarMenu = true;
					break;

				default:
					DestacarMensagem("\n   Opção inválida! Tente novamente.", ConsoleColor.Red);
					break;
			}

			if (!voltarMenu && opcao != "0")
			{
				Console.WriteLine("\nPressione qualquer tecla para continuar...");
				Console.ReadKey();
			}
		}
	}

	static void ExibirMenuAlunos()
	{
		Console.WriteLine("\n┌────────────────────────────────────────┐");
		Console.WriteLine("│        GESTÃO DE ALUNOS                │");
		Console.WriteLine("├────────────────────────────────────────┤");
		Console.WriteLine("│ 1 - Cadastrar Aluno                    │");
		Console.WriteLine("│ 2 - Listar Alunos                      │");
		Console.WriteLine("│ 3 - Exibir Detalhes de Aluno           │");
		Console.WriteLine("│ 4 - Adicionar Nota                     │");
		Console.WriteLine("│ 0 - Voltar ao Menu Principal           │");
		Console.WriteLine("└────────────────────────────────────────┘");
	}

	static void CadastrarAluno()
	{
		Console.Clear();
		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║         CADASTRO DE ALUNO           ║");
		Console.WriteLine("╚═════════════════════════════════════╝");

		var nome = ReadNonEmptyString("\nDigite o nome do aluno: ");
		long cpf = ReadLongInRange("Digite o CPF do aluno (apenas números): ", 10000000000, 99999999999);
		int idade = ReadIntInRange("Digite a idade do aluno: ", 0, 120);
		var turma = ReadNonEmptyString("Digite a turma do aluno: ");

		Aluno novoAluno = new(nome, idade, cpf, turma, new List<double>());

		alunos.Add(novoAluno);
		int idAluno = alunos.Count;

        DestacarMensagem($"\nAluno {novoAluno.GetNome()} cadastrado com sucesso! Seu número de ID é: {idAluno}", ConsoleColor.Green);
	}

	static void ListarAlunos()
	{
		Console.Clear();
		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║           LISTA DE ALUNOS           ║");
		Console.WriteLine("╚═════════════════════════════════════╝");

		if (alunos.Count == 0)
		{
			DestacarMensagem("\nNenhum aluno cadastrado.", ConsoleColor.Yellow);
			return;
		}

		Console.WriteLine();
		for (int i = 0; i < alunos.Count; i++)
		{
			var nome = string.IsNullOrEmpty(alunos[i].GetNome()) ? "(sem nome)" : alunos[i].GetNome();
			var turma = string.IsNullOrEmpty(alunos[i].GetTurma()) ? "(sem turma)" : alunos[i].GetTurma();
			Console.WriteLine($"ID: {i + 1}. {nome} - Turma: {turma} - Média: {alunos[i].CalcularMedia():F2}");
		}
	}

	static void ExibirDetalheAluno()
	{
		Console.Clear();
		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║          DETALHES DO ALUNO          ║");
		Console.WriteLine("╚═════════════════════════════════════╝");

		if (alunos.Count == 0)
		{
			DestacarMensagem("\nNenhum aluno cadastrado.", ConsoleColor.Yellow);
			return;
		}

		ListarAlunos();
		int indice = ReadIndexChoice("\nDigite o número de ID do aluno: ", alunos.Count);

		Aluno aluno = alunos[indice];
		Console.WriteLine($"\nNome: {aluno.GetNome()}");
		Console.WriteLine($"CPF: {aluno.GetIdade()}");
		Console.WriteLine($"Idade: {aluno.GetIdade()} anos");
		Console.WriteLine($"Turma: {aluno.GetTurma()}");
		Console.WriteLine($"Média: {aluno.CalcularMedia():F2}");
		Console.WriteLine($"Notas: {string.Join(", ", aluno.GetNotas())}");
	}

	static void AdicionarNota()
	{
		Console.Clear();
		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║           ADICIONAR NOTA            ║");
		Console.WriteLine("╚═════════════════════════════════════╝");

		if (alunos.Count == 0)
		{
			DestacarMensagem("\nNenhum aluno cadastrado.", ConsoleColor.Yellow);
			return;
		}

		ListarAlunos();
		int indice = ReadIndexChoice("\nDigite o número do aluno: ", alunos.Count);

		double nota = ReadDoubleInRange("\nDigite a nota a ser adicionada (0 a 10): ", 0, 10);
		alunos[indice].GetNotas().Add(nota);
		DestacarMensagem($"\nNota adicionada com sucesso para {alunos[indice].GetNome()}!", ConsoleColor.Green);
	}

	static void MenuProfessores()
	{
		bool voltarMenu = false;

		while (!voltarMenu)
		{
			Console.Clear();
			ExibirMenuProfessores();
			Console.WriteLine("\nDigite o número de uma opção: ");
			string? opcao = Console.ReadLine();

			switch (opcao)
			{
				case "1":
					CadastrarProfessor();
					break;

				case "2":
					ListarProfessores();
					break;

				case "3":
					DestacarMensagem("\nFuncionalidade em desenvolvimento.", ConsoleColor.Yellow);
					break;

				case "4":
					SalarioProfessor();
					break;

				case "0":
					voltarMenu = true;
					break;

				default:
					DestacarMensagem("\n   Opção inválida! Tente novamente.", ConsoleColor.Red);
					break;
			}

			if (!voltarMenu && opcao != "0")
			{
				Console.WriteLine("\nPressione qualquer tecla para continuar...");
				Console.ReadKey();
			}
		}
	}

	static void ExibirMenuProfessores()
	{
		Console.WriteLine("\n┌────────────────────────────────────────┐");
		Console.WriteLine("│         GESTÃO DE PROFESSORES          │");
		Console.WriteLine("├────────────────────────────────────────┤");
		Console.WriteLine("│ 1 - Cadastrar Professor                │");
		Console.WriteLine("│ 2 - Listar Professores                 │");
		Console.WriteLine("│ 3 - Adicionar Turma                    │");
		Console.WriteLine("│ 4 - Atualizar Salário                  │");
		Console.WriteLine("│ 0 - Voltar ao Menu Principal           │");
		Console.WriteLine("└────────────────────────────────────────┘");
	}

	static void CadastrarProfessor()
	{
		Console.Clear();
		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║       CADASTRO DE PROFESSOR         ║");
		Console.WriteLine("╚═════════════════════════════════════╝");

		var nome = ReadNonEmptyString("\nDigite o nome do professor: ");
		int idade = ReadIntInRange("Digite a idade do professor: ", 18, 120);
        long cpf = ReadLongInRange("Digite o CPF do professor (apenas números): ", 10000000000, 99999999999);
        string disciplina = ReadNonEmptyString("Qual a disciplina desse professor? ");

		Professor novoProfessor = new(nome, idade, cpf, disciplina, new List<decimal>(), new List<string>());

		professores.Add(novoProfessor);
		int idProfessor = professores.Count;

		DestacarMensagem($"\nProfessor {novoProfessor.GetNome()} cadastrado com sucesso! Seu número de ID é: {idProfessor}", ConsoleColor.Green);
	}

	static void ListarProfessores()
	{
		Console.Clear();
		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║        LISTA DE PROFESSORES         ║");
		Console.WriteLine("╚═════════════════════════════════════╝");

		if (professores.Count == 0)
		{
            DestacarMensagem("\nNenhum professor cadastrado.", ConsoleColor.Yellow);
		}

		Console.WriteLine();
		for (int i = 0; i < professores.Count; i++)
		{
			var nome = string.IsNullOrEmpty(professores[i].GetNome()) ? "(sem nome)" : professores[i].GetNome();
			var disciplina = string.IsNullOrEmpty(professores[i].GetDisciplina()) ? "(sem disciplina)" : professores[i].GetDisciplina();
			var salario = professores[i].GetSalarios().Count == 0 ? 0 : professores[i].GetSalarios()[^1];

			Console.WriteLine($"ID: {i + 1}. {nome} - Disciplina: {disciplina} - Salário: {salario}");
		}
	}

	static void AdicionarTurma()
	{
		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║           ADICIONAR TURMA           ║");
		Console.WriteLine("╚═════════════════════════════════════╝");
	}


	static void SalarioProfessor()
	{
		Console.Clear();
		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║          ATUALIZAR SÁLARIO          ║");
		Console.WriteLine("╚═════════════════════════════════════╝");

		if (professores.Count == 0)
		{
            DestacarMensagem("\nNenhum professor cadastrado.", ConsoleColor.Yellow);
            return;
		}

		ListarProfessores();

		int indice = ReadIndexChoice("\nDigite o número de ID do professor: ", professores.Count);

		decimal salario = ReadDecimalInRange("\nDigite o sálario a ser adicionado : ", 0, 1_000_000);
		professores[indice].GetSalarios().Add(salario);
	}

	static void ExibirEstatisticas()
	{
		Console.Clear();
		Console.WriteLine("╔═════════════════════════════════════╗");
		Console.WriteLine("║         ESTATÍSTICAS GERAIS         ║");
		Console.WriteLine("╚═════════════════════════════════════╝");

		Console.WriteLine($"\nTotal de alunos cadastrados: {alunos.Count}");
		Console.WriteLine($"Total de professores cadastrados: {professores.Count}");

		if (alunos.Count > 0)
		{
			double somaMedias = 0;

			foreach (var aluno in alunos)
				somaMedias += aluno.CalcularMedia();
			Console.WriteLine($"Média geral dos alunos: {(somaMedias / alunos.Count):F2}");
		}
	}

	static string ReadNonEmptyString(string prompt)
	{
		while (true)
		{
			Console.Write(prompt);

			var entrada = Console.ReadLine()?.Trim();

			if (!string.IsNullOrEmpty(entrada)) return entrada;
			DestacarMensagem("Entrada vazia. Tente novamente.", ConsoleColor.Yellow);
		}
	}

	static int ReadIntInRange(string prompt, int min, int max)
	{
		while (true)
		{
			Console.Write(prompt);

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
			Console.Write(prompt);

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
			Console.Write(prompt);

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
			Console.Write(prompt);

			var line = Console.ReadLine();
			long value;

			if (long.TryParse(line, out value) && value >= min && value <= max)
				return value;
			//if (double.TryParse(line, NumberStyles.Number, CultureInfo.CurrentCulture, out double value) && value >= min && value <= max)
			//	return value;
			DestacarMensagem($"Entrada inválida. Siga este modelo: 000.000.000-00.", ConsoleColor.Red);
		}
	}

	static decimal ReadDecimalNonNegative(string prompt)
	{
		while (true)
		{
			Console.Write(prompt);

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
			Console.Write(prompt);
			var line = Console.ReadLine();
			if (int.TryParse(line, NumberStyles.Integer, CultureInfo.CurrentCulture, out int value) && value >= 1 && value <= count)
				return value - 1;
			DestacarMensagem($"Escolha inválida. Digite um número entre 1 e {count}.", ConsoleColor.Red);
		}
	}
}
