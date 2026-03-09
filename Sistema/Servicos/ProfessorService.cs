using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Spectre.Console;
using System.Text.Json;

public static class ProfessorService
{

	//===================== LISTA DE DADOS =====================
    private static List<Professor> professores = ProfessoresRepository.CarregarProfessores();
    public static IReadOnlyList<Professor> Professores => professores;

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
                    "3. Histório Salarial",
                    "4. Atualizar Salário",
                    "5. Remover Professor",
                    "0. Voltar"
                    }));

            if (opcao.StartsWith("0")) break;

            switch (opcao[0])
            {
                case '1': CadastrarProfessor(); break;
                case '2': ListarProfessores(); break;
                case '3': HistoricoSalario(); break;
                case '4': SalarioProfessor(); break;
                case '5': RemoverProfessor(); break;
            }

            AnsiConsole.MarkupLine("\n[grey]Pressione qualquer tecla para continuar...[/]");
            Console.ReadKey();
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

        Professor novoProfessor = new(nome, idade, cpf, disciplina);
        professores.Add(novoProfessor);

        ProfessoresRepository.SalvarProfessores();

        AnsiConsole.MarkupLine($"\n [green] ☑ Professor [bold]{nome}[/] cadastrado com sucesso![/]");
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
            decimal ultimoSalario = p.Salarios.Count > 0 ? p.Salarios[^1] : 0;

            string cpf = p.CPF;
            string cpfFormatado;
            if (!string.IsNullOrWhiteSpace(p.CPF) &&
                p.CPF.Length == 11 &&
                ulong.TryParse(p.CPF, out var cpfNum))
            {
                cpfFormatado = string.Format(@"{0:000\.000\.000\-00}", cpfNum);
            }
            else
            {
                cpfFormatado = "[red]N/D[/]";
            }

            tabela.AddRow(
                (i + 1).ToString(),
                p.Nome,
                cpfFormatado,
                $"[italic]{p.Disciplina}[/]",
                $"[green]{ultimoSalario:C}[/]"
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
                .UseConverter(p => $"{p.Nome} (CPF: {p.CPF})")
        );

        var salarioAtual = prof.Salarios.Count > 0 ? prof.Salarios[^1] : 0;

        var grid = new Grid().AddColumns(2);
        grid.AddRow(new Panel($"Salário atual: [green]{salarioAtual}[/]"));
        AnsiConsole.Write(grid);

        var novoSalario = AnsiConsole.Prompt(
            new TextPrompt<decimal>($"\n Novo salário para [green]{prof.Nome}[/]:")
            .ValidationErrorMessage("[red]Por favor, insira um salário válido.[/]")
            .Validate(s => s >= 0
                ? ValidationResult.Success()
                : ValidationResult.Error("[red]Salário deve ser positivo[/]"))
        );

        var confirmar = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Tem certeza que deseja modificar o salário?")
                .AddChoices(new[] {
                        "1. Sim",
                        "0. Não"
                })
        );

        if (confirmar.StartsWith("0"))
            return;

        AnsiConsole.Status()
            .Start("Atualizando folha de pagamento...", ctx =>
            {
                Thread.Sleep(800);
                prof.AdicionarSalario(novoSalario);
                ProfessoresRepository.SalvarProfessores();
            });

        AnsiConsole.MarkupLine("\n[green] ☑ Salário atualizado com sucesso![/]");
    }

    //===================== HISTORICO SALARIAL ==========================================
    static void HistoricoSalario()
    {
		if (professores.Count == 0)
		{
			AnsiConsole.MarkupLine("\n[yellow]! Nenhum professor cadastrado.[/]");
			return;
		}

		var prof = AnsiConsole.Prompt(
            new SelectionPrompt<Professor>()
            .Title("Escolha um professor:")
            .AddChoices(professores)
            .UseConverter(p => p.Nome)
        );

        if (prof.Salarios.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]Professor ainda não possui salários registrados.[/]");
            return;
        }

        var tabela = new Table()
            .AddColumn("Registro")
            .AddColumn("Salário");

        for (int i = 0; i < prof.Salarios.Count; i++)
        {
            tabela.AddRow(
                (i + 1).ToString(),
                $"[green]{prof.Salarios[i]:C}[/]"
            );
        }

        AnsiConsole.Write(tabela);
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
                .UseConverter(p => $"{p.Nome} | CPF: {p.CPF} | Disciplina: {p.Disciplina}")
        );

        var confirmar = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Tem certeza que deseja remover este professor?")
                .AddChoices(new[] {
                        "1. Sim",
                        "0. Não"
                })
        );

        if (confirmar.StartsWith("0"))
            return;

        professores.Remove(profSelecionado);

        ProfessoresRepository.SalvarProfessores();

        AnsiConsole.MarkupLine($"\n [green] ☑ Professor [bold]{profSelecionado.Nome}[/] removido com sucesso![/]");
    }
}
