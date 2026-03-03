using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Spectre.Console;
using System.Text.Json;


public static class ProfessorService
{

	//===================== ARQUIVO DE DADOS =====================
	static string nomeArquivoProf = "professores.json";

	//===================== LISTA DE DADOS =====================
	public static List<Professor> professores = CarregarProfessores();

	//===================== PERSISTÊNCIA PROFESSORES =====================
	static void SalvarProfessores()
	{
		var options = new JsonSerializerOptions { WriteIndented = true };
		string json = JsonSerializer.Serialize(professores, options);
		File.WriteAllText(nomeArquivoProf, json);
	}

	static List<Professor> CarregarProfessores()
	{
		if (!File.Exists(nomeArquivoProf))
			return new List<Professor>();

		string json = File.ReadAllText(nomeArquivoProf);

		try
		{
			return JsonSerializer.Deserialize<List<Professor>>(json) ?? new List<Professor>();
		}
		catch
		{
			return new List<Professor>();
		}
	}

	//===================== MENU DE PROFESSORES ======================================================
	public static void MenuProfessores()
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
                    "4. Remover Professor",
                    "0. Voltar"
                    }));

            if (opcao.StartsWith("0")) break;

            switch (opcao[0])
            {
                case '1': CadastrarProfessor(); break;
                case '2': ListarProfessores(); break;
                case '3': SalarioProfessor(); break;
                case '4': RemoverProfessor(); break;
            }

            AnsiConsole.MarkupLine("\n[grey]Pressione qualquer tecla para continuar...[/]");
            Console.ReadKey(true);
        }
    }

    //===================== CADASTRO PROFESSORES ==========================================
    static void CadastrarProfessor()
    {

        var nome = AnsiConsole.Prompt(new TextPrompt<string>("Nome do Professor:")
            .ValidationErrorMessage("[red]Nome inválido (digite apenas letras)[/]"));

        var cpf = AnsiConsole.Prompt(new TextPrompt<string>("CPF do Professor:")
            .ValidationErrorMessage("[red]CPF inválido (digite apenas números)[/]")
            .Validate(cpf =>
            {
                if (cpf.Length != 11)
                    return ValidationResult.Error("[red]CPF deve conter exatamente 11 números[/]");

                if (!Regex.IsMatch(cpf, @"^\d{11}$"))
                    return ValidationResult.Error("[red]CPF deve conter exatamente 11 números[/]");

                return ValidationResult.Success();
            })
        );
        var idade = AnsiConsole.Prompt(
            new TextPrompt<int>("Idade:")
                .ValidationErrorMessage("[red]Por favor, insira uma idade válida.[/]")
                .Validate(age => age >= 18 ? ValidationResult.Success() : ValidationResult.Error("[red]O professor deve ser maior de idade.[/]"))
        );
        var disciplina = AnsiConsole.Ask<string>("Disciplina/Matéria:");

        Professor novoProfessor = new(nome, idade, cpf, disciplina, new List<decimal>(), new List<string>());
        professores.Add(novoProfessor);

        SalvarProfessores();

        AnsiConsole.MarkupLine($"\n [green] Professor [bold]{nome}[/] cadastrado com sucesso![/]");
    }

    //===================== LISTA PROFESSORES ==========================================
    static void ListarProfessores()
    {
        if (professores.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]! Nenhum professor cadastrado no sistema.[/]");
            return;
        }

        var tabela = new Table().Border(TableBorder.Rounded).Expand();
        tabela.AddColumn("[blue]ID[/]");
        tabela.AddColumn("[blue]Nome[/]");
        tabela.AddColumn("[blue]CPF[/]");
        tabela.AddColumn("[blue]Disciplina[/]");
        tabela.AddColumn("[blue]Último Salário[/]");

        for (int i = 0; i < professores.Count; i++)
        {
            var p = professores[i];
            decimal ultimoSalario = p.GetSalarios().Count > 0 ? p.GetSalarios()[^1] : 0;

            string cpf = p.GetCPF();
            string cpfFormatado = string.Format(@"{0:000\.000\.000\-00}", Convert.ToUInt64(cpf));

            tabela.AddRow(
                (i + 1).ToString(),
                p.GetNome(),
                cpfFormatado,
                $"[italic]{p.GetDisciplina()}[/]",
                $"[green]{ultimoSalario}[/]"
            );
            Console.WriteLine(ultimoSalario);
        }

        AnsiConsole.Write(tabela);
    }

    //===================== SALARIO ==========================================
    static void SalarioProfessor()
    {
        if (professores.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]! Nenhum professor cadastrado.[/]");
            return;
        }

        var prof = AnsiConsole.Prompt(
            new SelectionPrompt<Professor>()
                .Title("Atualizar salário de qual professor?")
                .AddChoices(professores)
                .UseConverter(p => p.GetNome())
        );

        var novoSalario = AnsiConsole.Prompt(new TextPrompt<decimal>($"Novo salário para [green]{prof.GetNome()}[/] (digite apenas números):")
                            .ValidationErrorMessage("[red]Por favor, insira um salario válido.[/]")
                            .Validate(salario =>
                            {
                                //var salarioString = salario.ToString();
                                if (salario < 0)
                                {
                                    return ValidationResult.Error("[Red]Por favor, adicione um salario positivo[/]");
                                }

                                return ValidationResult.Success();
                            })
                        );

        AnsiConsole.Status()
            .Start("Atualizando folha de pagamento...", ctx => {
                Thread.Sleep(800);
                prof.GetSalarios().Add(novoSalario);
                SalvarProfessores();
            });

        AnsiConsole.MarkupLine($"\n [green] Salário atualizado com sucesso![/]");
    }

    //===================== REMOVER PROFESSORES ==========================================
    static void RemoverProfessor()
    {
        if (professores.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]! Nenhum professor cadastrado.[/]");
            return;
        }

        var profSelecionado = AnsiConsole.Prompt(
            new SelectionPrompt<Professor>()
                .Title("Selecione o [blue]professor[/] que sera removido:")
                .AddChoices(professores)
                .UseConverter(p => $"{p.GetNome()} | CPF: {p.GetCPF()} | Disciplina: {p.GetDisciplina()}")
        );

        var confirmar = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Tem certeza que deseja remover este aluno?")
                .AddChoices(new[] {
                        "1. Sim",
                        "0. Não"
                })
        );

        if (confirmar.StartsWith("0"))
            return;

        professores.Remove(profSelecionado);

        SalvarProfessores();

        AnsiConsole.MarkupLine($"\n [green] Professor [bold]{profSelecionado.GetNome()}[/] removido com sucesso![/]");
    }
}
