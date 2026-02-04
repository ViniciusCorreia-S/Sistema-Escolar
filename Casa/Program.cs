
namespace SistemaEscolar
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continuar = true;

            Console.WriteLine("╔═════════════════════════════════════╗");
            Console.WriteLine("║     SISTEMA DE GESTÃO ESCOLAR       ║");
            Console.WriteLine("╚═════════════════════════════════════╝");

            while (continuar)
            {
                ExibirMenuPrincipal();
                Console.WriteLine("\nDigite o número de uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine("1");
                        break;

                    case "2":
                        Console.WriteLine("2");
                        break;

                    case "3":
                        Console.WriteLine("3");
                        break;

                    case "0":
                        continuar = false;
                        Console.WriteLine("\n Encerrando o sistema...");
                        Console.WriteLine("Obrigado por usar o Sistema de Gestão Escolar!");
                        break;

                    default:
                        DestacarMensagem("\n   Opção inválida! Tente novamente.", ConsoleColor.Red);
                        break;
                }

				if (continuar && opcao != "0")
				{
					Console.WriteLine("\nPressione qualquer tecla para continuar...");
					//Console.ReadKey();
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
            ExibirMenuAlunos();
            bool voltarMenu = false;

            while (!voltarMenu)
            {
				Console.WriteLine("\nDigite o número de uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine("cadastro Aluno");
                        break;

                    case "2":
                        Console.WriteLine("listar Aluno");
                        break;

                    case "3":
                        Console.WriteLine("exibir detalhe Aluno");
                        break;

					case "4":
						Console.WriteLine("adicionar nota Aluno");
						break;

					case "0":
						voltarMenu = true;
						Console.Clear();
						break;

					default:
						DestacarMensagem("\n   Opção inválida! Tente novamente.", ConsoleColor.Red);
						break;
				}

				if (!voltarMenu && opcao != "0")
				{
					Console.WriteLine("\nPressione qualquer tecla para continuar...");
					//Console.ReadKey();
					Console.Clear();
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

        static void MenuProfessores()
        {
			bool voltarMenu = false;

            while (!voltarMenu)
			{
                //ExibirMenuProfessores();
				Console.WriteLine("\nDigite o número de uma opção: ");
                string opcao = Console.ReadLine();


				switch (opcao) 
				{

					case "1":
						//CadastrarProfessor();
						break;

					case "2":
						//ListarProfessores();
						break;

					case "3":
						//ExibirDetalhesProfessor();
						break;

					case "4":
						//AdicionarTurmaProfessor();
						break;

					case "0":
						voltarMenu = true;
						Console.Clear();
						break;

					default:
						DestacarMensagem("\n   Opção inválida! Tente novamente.", ConsoleColor.Red);
						break;

				}

			}
		}
	}
}