using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Spectre.Console;
using System.Text.Json;

public static class AlunoService
{

    //===================== LISTA DE DADOS =====================
    private static List<Aluno> alunos = AlunosRepository.CarregarAlunos();
    public static IReadOnlyList<Aluno> Alunos => alunos;

	//===================== MENU DE ALUNOS ======================================================
	public static void MenuAlunos()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.Write(new Rule("[bold green]GESTÃO DE ALUNOS[/]"));

            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Selecione uma ação:")
                    .AddChoices(new[] {
                        "1. Cadastrar Aluno",
                        "2. Listar Alunos",
                        "3. Detalhes do Aluno",
                        "4. Atualizar Nota",
                        "5. Remover Aluno",
                        "0. Voltar"
                }));

            if (opcao.StartsWith("0")) break;

            switch (opcao[0])
            {
                case '1': CadastrarAluno(); break;
                case '2': ListarAlunos(); break;
                case '3': ExibirDetalheAluno(); break;
                case '4': AtualizarNota(); break;
                case '5': RemoverAluno(); break;
            }

            AnsiConsole.MarkupLine("\n[italic grey]Pressione qualquer tecla para retornar ao menu...[/]");
            Console.ReadKey();
        }
    }

    //===================== CADASTRO DE ALUNO ==========================================
    static void CadastrarAluno()
    {

        if (TurmaService.turmas.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]! Nenhuma turma foi aberta até o momento.[/]");
            return;
        }

        var nome = AnsiConsole.Prompt(new TextPrompt<string>("Nome do Aluno:")
            .ValidationErrorMessage("[red]Nome inválido (digite apenas letras)[/]"));
        var cpf = AnsiConsole.Prompt(new TextPrompt<string>("CPF do Aluno:")
            .ValidationErrorMessage("[red]CPF inválido (digite apenas números)[/]")
            .Validate(cpf =>
            {
                if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
                    return ValidationResult.Error("[red]CPF deve conter exatamente 11 números[/]");

                //if (alunos.Any(a => a.CPF == cpf))
                //    AnsiConsole.MarkupLine("[red]CPF já cadastrado![/]");
                //    return;

                return ValidationResult.Success();
            })
        );
        var idade = AnsiConsole.Prompt(new TextPrompt<int>("Idade do Aluno:")
            .ValidationErrorMessage("[red]Idade inválida[/]"));

        var turmaSelecionada = AnsiConsole.Prompt(
            new SelectionPrompt<Turma>()
                .Title("Selecione a turma do [blue]aluno[/]:")
                .AddChoices(TurmaService.Turmas)
                .UseConverter(t => $"Turma: {t.GetNomeTurma()}")
        );

        var novoAluno = new Aluno(nome, idade, cpf, turmaSelecionada.GetNomeTurma().ToString());

        alunos.Add(novoAluno);

        turmaSelecionada.AdicionarAluno(novoAluno);

        AlunosRepository.SalvarAlunos();
        TurmasRepository.SalvarTurmas();

        AnsiConsole.MarkupLine($"\n [green] Aluno [bold]{nome}[/] cadastrado com sucesso![/]");
    }

    //===================== DETALHES DE ALUNO ==========================================
    static void ExibirDetalheAluno()
    {

        if (alunos.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
            return;
        }

        var alunoSelecionado = AnsiConsole.Prompt(
            new SelectionPrompt<Aluno>()
                .Title("Selecione o [blue]aluno[/] para ver detalhes:")
                .AddChoices(alunos)
                .UseConverter(a => $"{a.GetNome()} (Turma: {a.GetTurma()})")
        );

        string cpf = alunoSelecionado.GetCPF();
        string cpfFormatado = string.Format(@"{0:000\.000\.000\-00}", Convert.ToUInt64(cpf));

        var card = new Panel(new Markup(
            $"[bold]CPF:[/] {cpfFormatado}\n" +
            $"[bold]Idade:[/] {alunoSelecionado.GetIdade()} anos\n" +
            $"[bold]Média Acadêmica:[/] [yellow]{alunoSelecionado.CalcularMedia():F2}[/]\n" +
            $"[bold]Notas:[/] {(alunoSelecionado.Notas.Count > 0
                ? string.Join(" | ", alunoSelecionado.Notas.Select(n => n.ToString("F2")))
                : "[red]N/D[/]")}"
        ));

        card.Header($"Ficha Cadastral: [bold]{alunoSelecionado.GetNome()}[/]");
        card.BorderColor(Color.Blue);

        AnsiConsole.Write(card);
    }

    //===================== LISTA DE ALUNO ==========================================
    static void ListarAlunos()
    {

        if (alunos.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
            return;
        }

        var tabela = new Table().Border(TableBorder.Rounded).Expand();
        tabela.AddColumn("[blue]ID[/]");
        tabela.AddColumn("[blue]Nome[/]");
        tabela.AddColumn("[blue]Turma[/]");
        tabela.AddColumn("[blue]Média[/]");

        for (int i = 0; i < alunos.Count; i++)
        {
            var CorNota = alunos[i].CalcularMedia() > 6 ? "[green]" : "[red]";

            tabela.AddRow(
                (i + 1).ToString(),
                alunos[i].GetNome() ?? "[red]N/A[/]",
                alunos[i].GetTurma() ?? "[red]N/A[/]",
                $"{CorNota}{alunos[i].CalcularMedia():F2}[/]"
            );
        }

        AnsiConsole.Write(tabela);
    }

    //===================== ADICIONAR NOTA ==========================================
    static void AtualizarNota()
    {
        Console.Clear();

        if (alunos.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
            return;
        }

        while (alunos.Count > 0)
        {

            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Selecione um opção:")
                    .AddChoices(new[] {
                            "1. Adicionar Nota",
                            "2. Remover Nota",
                            "0. Voltar"
                    })
            );

            if (opcao.StartsWith("0")) break;

            switch (opcao[0])
            {
                case '1': AdicionarNota(); break;
                case '2': RemoverNota(); break;
            }
        }
    }

    static void AdicionarNota()
    {

        ListarAlunos();

        var alunoSelecionado = AnsiConsole.Prompt(
            new SelectionPrompt<Aluno>()
                .Title("Selecione o [blue]aluno[/] que receberar a nota:")
                .AddChoices(alunos)
                .UseConverter(a => $"{a.GetNome()} (Turma: {a.GetTurma()})")
        );

        double nota = AnsiConsole
                        .Prompt(new TextPrompt<double>("Digite a [blue]nota[/] a ser adicionada (0 a 10): ")
                            .ValidationErrorMessage("[red]Nota inválida (digite um número de 0 a 10)[/]")
                        .Validate(notaNova =>
                        {
                            if (notaNova >= 0 && notaNova <= 10)
                                return ValidationResult.Success();

                            return ValidationResult.Error("[red]A nota deve ser um número de 0 a 10[/]");
                        })
                        );

        alunoSelecionado.AdicionarNota(nota);

        AlunosRepository.SalvarAlunos();

        AnsiConsole.MarkupLine($"\n [green] Nota de {alunoSelecionado.GetNome()} adicionada com sucesso ![/]");
    }

    static void RemoverNota()
    {
        ListarAlunos();

        //var alunoSelecionado = AnsiConsole.Prompt(
        //    new SelectionPrompt<Aluno>()
        //        .Title("Selecione o [blue]aluno[/] que deseja remover a nota:")
        //        .AddChoices(alunos)
        //        .UseConverter(a => $"{a.GetNome()} (Turma: {a.GetTurma()})")
        //);

        //var NotaSelecionada = AnsiConsole.Prompt(
        //    new SelectionPrompt<alunoSelecionado>()
        //        .Title("Selecione a [blue]nota[/] que sera removida:")
        //        .AddChoices(alunoSelecionado.Notas)
        //);

        //alunoSelecionado.Notas.Remove(NotaSelecionada);

        //SalvarAlunos();

        //AnsiConsole.MarkupLine($"\n [green] Nota de {alunoSelecionado.GetNome()} removida com sucesso ![/]");
    }
    
    //===================== REMOVER ==========================================
    static void RemoverAluno()
    {
        if (alunos.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
            return;
        }

        var alunoSelecionado = AnsiConsole.Prompt(
            new SelectionPrompt<Aluno>()
                .Title("Selecione o [blue]aluno[/] que sera removido:")
                .AddChoices(alunos)
                .UseConverter(a => $"{a.GetNome()} | CPF: {a.GetCPF()} | Turma: {a.GetTurma()}")
        );

        var turmaDoAluno = TurmaService.turmas.FirstOrDefault(t => t.GetNomeTurma().ToString() == alunoSelecionado.GetTurma());

        if (turmaDoAluno != null)
        {
            turmaDoAluno.RemoverAluno(alunoSelecionado);
        }

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

        alunos.Remove(alunoSelecionado);

        AlunosRepository.SalvarAlunos();
        TurmaService.SalvarTurmas();

        AnsiConsole.MarkupLine($"\n [green] Aluno [bold]{alunoSelecionado.GetNome()}[/] removido com sucesso![/]");
    }
}
