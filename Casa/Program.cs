//using System;

//namespace SistemaEscolar
//{
//	class Program
//	{
//		static void Main()
//		{
//			bool continuar = true;

//			Console.WriteLine("╔═════════════════════════════════════╗");
//			Console.WriteLine("║     SISTEMA DE GESTÃO ESCOLAR       ║");
//			Console.WriteLine("╚═════════════════════════════════════╝");

//			while (continuar)
//			{

//				ExibirMenuPrincipal();
//				Console.WriteLine("\nDigite o número de uma opção: ");
//				string? opcao = Console.ReadLine();

//				switch (opcao)
//				{
//					case "1":
//						MenuAlunos();
//						break;

//					case "2":
//						MenuProfessores();
//						break;

//					case "3":
//						Console.WriteLine("3");
//						break;

//					case "0":
//						continuar = false;
//						Console.WriteLine("\n Encerrando o sistema...");
//						Console.WriteLine("Obrigado por usar o Sistema de Gestão Escolar!");
//						break;

//					default:
//						DestacarMensagem("\n   Opção inválida! Tente novamente.", ConsoleColor.Red);
//						break;
//				}

//				if (continuar && opcao != "0")
//				{
//					Console.WriteLine("\nPressione qualquer tecla para continuar...");
//					Console.ReadKey();
//					Console.Clear();
//				}
//			}
//		}

//		static void DestacarMensagem(string mensagem, ConsoleColor cor)
//		{
//			Console.ForegroundColor = cor;
//			Console.WriteLine(mensagem);
//			Console.ResetColor();
//		}

//		static void ExibirMenuPrincipal()
//		{
//			Console.WriteLine("\n┌────────────────────────────────────────┐");
//			Console.WriteLine("│          MENU PRINCIPAL                │");
//			Console.WriteLine("├────────────────────────────────────────┤");
//			Console.WriteLine("│ 1 - Gestão de Alunos                   │");
//			Console.WriteLine("│ 2 - Gestão de Professores              │");
//			Console.WriteLine("│ 3 - Estatísticas Gerais                │");
//			Console.WriteLine("│ 0 - Sair                               │");
//			Console.WriteLine("└────────────────────────────────────────┘");
//		}

//		static void MenuAlunos()
//		{
//			Console.Clear();
//			ExibirMenuAlunos();
//			bool voltarMenu = false;

//			while (!voltarMenu)
//			{
//				Console.WriteLine("\nDigite o número de uma opção: ");
//				string? opcao = Console.ReadLine();

//				switch (opcao)
//				{
//					case "1":
//						CadastrarAluno();
//						break;

//					case "2":
//						ListarAluno();
//						break;

//					case "3":
//						ExibirDetalheAluno();
//						break;

//					case "4":
//						AdicionarNota();
//						break;

//					case "0":
//						voltarMenu = true;
//						Console.Clear();
//						break;

//					default:
//						DestacarMensagem("\n   Opção inválida! Tente novamente.", ConsoleColor.Red);
//						break;
//				}

//				if (!voltarMenu && opcao != "0")
//				{
//					Console.WriteLine("\nPressione qualquer tecla para continuar...");
//					Console.ReadKey();
//					Console.Clear();
//				}
//			}
//		}

//		static void ExibirMenuAlunos()
//		{
//			Console.WriteLine("\n┌────────────────────────────────────────┐");
//			Console.WriteLine("│        GESTÃO DE ALUNOS                │");
//			Console.WriteLine("├────────────────────────────────────────┤");
//			Console.WriteLine("│ 1 - Cadastrar Aluno                    │");
//			Console.WriteLine("│ 2 - Listar Alunos                      │");
//			Console.WriteLine("│ 3 - Exibir Detalhes de Aluno           │");
//			Console.WriteLine("│ 4 - Adicionar Nota                     │");
//			Console.WriteLine("│ 0 - Voltar ao Menu Principal           │");
//			Console.WriteLine("└────────────────────────────────────────┘");
//		}

//		static void CadastrarAluno()
//		{
//			Console.Clear();
//			Console.WriteLine("╔═════════════════════════════════════╗");
//			Console.WriteLine("║         CADASTRO DE ALUNO           ║");
//			Console.WriteLine("╚═════════════════════════════════════╝");

//			Aluno novoAluno = new Aluno();

//			Console.Write("\nDigite o nome do aluno: ");
//			novoAluno.nome = Console.ReadLine();

//			Console.Write("Digite a idade do aluno: ");
//			novoAluno.idade = Convert.ToInt32(Console.ReadLine());

//			Console.Write("Digite a turma do aluno: ");
//			novoAluno.turma = Console.ReadLine();

//			var listaDadosAluno = $"{novoAluno.nome}, {novoAluno.idade} anos, Turma: {novoAluno.turma}";
//			DestacarMensagem($"\nAluno {novoAluno.nome} cadastrado com sucesso!", ConsoleColor.Green);
//		}

//		static void ListarAluno()
//		{
//			Console.Clear();
//			Console.WriteLine("╔═════════════════════════════════════╗");
//			Console.WriteLine("║           LISTA DE ALUNOS           ║");
//			Console.WriteLine("╚═════════════════════════════════════╝");

//			//var alunos = ; 
		
//			//if (alunos.Count == 0)
//			//{
//			//	Console.WriteLine("\n Nenhum aluno cadastrado.");
//			//	return;
//			//}

//			//Console.WriteLine("\n=== LISTA DE ALUNOS ===");
//			//for (int i = 0; i < alunos.Count; i++)
//			//{
//			//	Console.WriteLine($"{i + 1}. {alunos[i].Nome} - Matrícula: {alunos[i].Matricula} - Média: {alunos[i].CalcularMedia():F2}");
//			//}
		
//		}

//		static void ExibirDetalheAluno()
//		{
//			Console.Clear();
//			Console.WriteLine("╔═════════════════════════════════════╗");
//			Console.WriteLine("║       DETALHES DO ALUNO             ║");
//			Console.WriteLine("╚═════════════════════════════════════╝");
//			//DestacarMensagem("\nFuncionalidade de exibir detalhes do aluno ainda não implementada.", ConsoleColor.Yellow)
//		}
//		static void AdicionarNota()
//		{
//			Console.Clear();
//			Console.WriteLine("╔═════════════════════════════╗");
//			Console.WriteLine("║       ADICIONAR NOTA        ║");
//			Console.WriteLine("╚═════════════════════════════╝");

//			Console.WriteLine("\nSelecione o aluno para adicionar a nota:");
//			string alunoSelecionado = Console.ReadLine();

//			Console.Write("\nDigite a nota a ser adicionada (0 a 10): ");
//			double nota = Convert.ToDouble(Console.ReadLine());
			
//			Aluno aluno = new Aluno();
//			if (nota >= 0 && nota <= 10)
//			{
//				aluno.notas.Add(nota);
//				DestacarMensagem($"Nota de {alunoSelecionado} adicionada com sucesso!", ConsoleColor.Green);
//			}
//			else
//			{
//				DestacarMensagem("A nota deve estar entre 0 e 10.", ConsoleColor.Red);
//			}
//		}

//		static void MenuProfessores()
//		{
//			bool voltarMenu = false;

//			while (!voltarMenu)
//			{
//				Console.Clear();
//				ExibirMenuProfessores();
//				Console.WriteLine("\nDigite o número de uma opção: ");
//				string? opcao = Console.ReadLine();


//				switch (opcao)
//				{

//					case "1":
//						CadastrarProfessor();
//						break;

//					case "2":
//						//ListarProfessores();
//						break;

//					case "3":
//						//AdicionarTurmaProfessor();
//						break;

//					case "4":
//						//Salario;
//						break;

//					case "0":
//						voltarMenu = true;
//						Console.Clear();
//						break;

//					default:
//						DestacarMensagem("\n   Opção inválida! Tente novamente.", ConsoleColor.Red);
//						break;

//				}

//				if (!voltarMenu && opcao != "0")
//				{
//					Console.WriteLine("\nPressione qualquer tecla para continuar...");
//					Console.ReadKey();
//					Console.Clear();
//				}

//				static void ExibirMenuProfessores()
//				{
//					Console.WriteLine("\n┌────────────────────────────────────────┐");
//					Console.WriteLine("│         GESTÃO DE PROFESSORES          │");
//					Console.WriteLine("├────────────────────────────────────────┤");
//					Console.WriteLine("│ 1 - Cadastrar Professor                │");
//					Console.WriteLine("│ 2 - Listar Professores                 │");
//					Console.WriteLine("│ 3 - Adicionar Turma                    │");
//					Console.WriteLine("│ 4 - Salário                            │");
//					Console.WriteLine("│ 0 - Voltar ao Menu Principal           │");
//					Console.WriteLine("└────────────────────────────────────────┘");
//				}

//				static void CadastrarProfessor()
//				{
//					Console.Clear();
//					Console.WriteLine("╔═════════════════════════════════════╗");
//					Console.WriteLine("║       CADASTRO DE PROFESSOR         ║");
//					Console.WriteLine("╚═════════════════════════════════════╝");

//					Professor novoProfessor = new Professor();

//					Console.Write("\nDigite o nome do professor: ");
//					novoProfessor.nome = Console.ReadLine();

//					Console.Write("Digite a idade do professor: ");
//					novoProfessor.idade = Convert.ToInt32(Console.ReadLine());
//					if (novoProfessor.idade < 18)
//					{
//						DestacarMensagem("\nIdade do professor deve ser maior ou igual a 18 anos. Tente novamente.", ConsoleColor.Red);
//						return;
//					}

//					Console.Write("Digite a disciplina do professor: ");
//					novoProfessor.Turma = Console.ReadLine();
					
//					Console.Write("Digite o salário do professor: ");
//					novoProfessor.salario = float.Parse(Console.ReadLine());
//					if (novoProfessor.salario < 0)
//					{
//						DestacarMensagem("\nSalário não pode ser negativo. Tente novamente.", ConsoleColor.Red);
//						return;
//					}

//					var listaDadosProfessor = $"{novoProfessor.nome}, {novoProfessor.idade} anos, Disciplina: {novoProfessor.Turma}, Salário: {novoProfessor.salario:C}";
//					DestacarMensagem($"\nProfessor {novoProfessor.nome} cadastrado com sucesso!", ConsoleColor.Green);
//				}
//			}
//		}
//	}
//}