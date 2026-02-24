//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Text.RegularExpressions;
//using Spectre.Console;

//public class AlunoService
//{
//    static List<Aluno> alunos = new List<Aluno>();

//    static char check = '\u2714';

//    static void MenuAlunos()
//    {
//        while (true)
//        {
//            Console.Clear();
//            AnsiConsole.Write(new Rule("[bold green]GESTÃO DE ALUNOS[/]"));

//            var opcao = AnsiConsole.Prompt(
//                new SelectionPrompt<string>()
//                    .Title("Selecione uma ação:")
//                    .AddChoices(new[] {
//                    "1. Cadastrar Aluno",
//                    "2. Listar Alunos",
//                    "3. Detalhes do Aluno",
//                    "4. Adicionar Nota",
//                    "5. Remover Aluno",
//                    "0. Voltar"
//                }));

//            if (opcao.StartsWith("0")) break;

//            switch (opcao[0])
//            {
//                case '1': CadastrarAluno(); break;
//                case '2': ListarAlunos(); break;
//                case '3': ExibirDetalheAluno(); break;
//                case '4': AdicionarNota(); break;
//                case '5': RemoverAluno(); break;
//            }

//            AnsiConsole.MarkupLine("\n[italic grey]Pressione qualquer tecla para retornar ao menu...[/]");
//            Console.ReadKey();
//        }
//    }

//    static void CadastrarAluno()
//    {
//        var opcao = AnsiConsole.Prompt(
//                        new SelectionPrompt<string>()
//                            .Title("Deseja cadastrar um aluno?")
//                            .AddChoices(new[] {
//                                 "1. Sim",
//                                 "0. Não"
//                            })
//                        );

//        if (opcao.StartsWith("0"))
//            return;


//        var nome = AnsiConsole.Prompt(new TextPrompt<string>("Nome do Aluno:")
//            .ValidationErrorMessage("[red]Nome inválido (digite apenas letras)[/]"));
//        var cpf = AnsiConsole.Prompt(new TextPrompt<string>("CPF do Aluno:")
//            .ValidationErrorMessage("[red]CPF inválido (digite apenas números)[/]")
//            .Validate(cpf =>
//            {
//                if (cpf.Length != 11 && !Regex.IsMatch(cpf, @"^\d{11}$"))
//                    return ValidationResult.Error("[red]CPF deve conter exatamente 11 números[/]");

//                return ValidationResult.Success();
//            })
//        );
//        var idade = AnsiConsole.Prompt(new TextPrompt<int>("Idade do Aluno:")
//            .ValidationErrorMessage("[red]Idade inválida[/]"));
//        var turma = AnsiConsole.Ask<string>("Turma:");

//        alunos.Add(new Aluno(nome, idade, cpf, turma, new List<double>()));

//        AnsiConsole.MarkupLine($"\n {check} [green]Aluno [bold]{nome}[/] cadastrado com sucesso![/]");
//    }

//    static void ExibirDetalheAluno()
//    {

//        if (alunos.Count == 0)
//        {
//            AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
//            return;
//        }

//        var alunoSelecionado = AnsiConsole.Prompt(
//            new SelectionPrompt<Aluno>()
//                .Title("Selecione o [blue]aluno[/] para ver detalhes:")
//                .AddChoices(alunos)
//                .UseConverter(a => $"{a.GetNome()} (Turma: {a.GetTurma()})")
//        );

//        string cpf = alunoSelecionado.GetCPF();
//        string cpfFormatado = string.Format(@"{0:000\.000\.000\-00}", Convert.ToUInt64(cpf));

//        var card = new Panel(new Markup(
//            $"[bold]CPF:[/] {cpfFormatado}\n" +
//            $"[bold]Idade:[/] {alunoSelecionado.GetIdade()} anos\n" +
//            $"[bold]Média Acadêmica:[/] [yellow]{alunoSelecionado.CalcularMedia():F2}[/]\n" +
//            $"[bold]Notas:[/] {string.Join(" | ", alunoSelecionado.GetNotas()) ?? "[red]N/D[/]":F2}"
//        ));

//        card.Header($"Ficha Cadastral: [bold]{alunoSelecionado.GetNome()}[/]");
//        card.BorderColor(Color.Blue);

//        AnsiConsole.Write(card);
//    }

//    static void ListarAlunos()
//    {

//        if (alunos.Count == 0)
//        {
//            AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
//            return;
//        }

//        var tabela = new Table().Border(TableBorder.Rounded).Expand();
//        tabela.AddColumn("[blue]ID[/]");
//        tabela.AddColumn("[blue]Nome[/]");
//        tabela.AddColumn("[blue]Turma[/]");
//        tabela.AddColumn("[blue]Média[/]");

//        for (int i = 0; i < alunos.Count; i++)
//        {
//            tabela.AddRow(
//                (i + 1).ToString(),
//                alunos[i].GetNome() ?? "[red]N/A[/]",
//                alunos[i].GetTurma() ?? "[red]N/A[/]",
//                $"[green]{alunos[i].CalcularMedia():F2}[/]"
//            );
//        }

//        AnsiConsole.Write(tabela);
//    }

//    static void AdicionarNota()
//    {

//        if (alunos.Count == 0)
//        {
//            AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
//            return;
//        }

//        ListarAlunos();

//        var alunoSelecionado = AnsiConsole.Prompt(
//            new SelectionPrompt<Aluno>()
//                .Title("Selecione o [blue]aluno[/] que receberar a nota:")
//                .AddChoices(alunos)
//                .UseConverter(a => $"{a.GetNome()} (Turma: {a.GetTurma()})")
//        );

//        //if (alunoSelecionado.StartsWith("0"))
//        //	return;

//        // int indice = AnsiConsole.Prompt(new TextPrompt<int>("Indice:").ValidationErrorMessage("[red]Indice inválido[/]"));

//        // double nota = ReadDoubleInRange("\nDigite a [blue]nota[/] a ser adicionada (0 a 10): ", 0, 10);
//        double nota = AnsiConsole
//                        .Prompt(new TextPrompt<double>("Digite a [blue]nota[/] a ser adicionada (0 a 10): ")
//                            .ValidationErrorMessage("[red]Nota inválida (digite um número de 0 a 10)[/]")
//                            .Validate(notaNova => {
//                                if (notaNova >= 0 && notaNova <= 10)
//                                    return ValidationResult.Success();

//                                return ValidationResult.Error("[red]A nota deve ser um número de 0 a 10[/]");
//                            })
//                        );

//        alunoSelecionado.GetNotas().Add(nota);

//        AnsiConsole.MarkupLine($"\n {check} [green]Nota de {alunoSelecionado.GetNome()} adicionada com sucesso ![/]");
//    }

//    static void RemoverAluno()
//    {
//        if (alunos.Count == 0)
//        {
//            AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
//            return;
//        }

//        var alunoSelecionado = AnsiConsole.Prompt(
//            new SelectionPrompt<Aluno>()
//                .Title("Selecione o [blue]aluno[/] que sera removido:")
//                .AddChoices(alunos)
//                .UseConverter(a => $"{a.GetNome()} | CPF: {a.GetCPF()} | Turma: {a.GetTurma()}")
//        );

//        alunos.Remove(alunoSelecionado);

//        AnsiConsole.MarkupLine($"\n {check} [green]Aluno [bold]{alunoSelecionado.GetNome()}[/] removido com sucesso![/]");
//    }
//}

