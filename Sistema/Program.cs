using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Spectre.Console;
using System.Text.Json;

//MELHORIAS FUTURAS:
// - opcao de voltar em salario
// - opção de fechar turma (remover da lista de turmas abertas)
// - opção de remover nota
// - corrigir erro na permanencia de todos os dados (alunos, turmas e professores)

class Program
{

    //===================== ARQUIVO DE DADOS =====================
    static string nomeArquivoTurma = "turmas.json";
    static string nomeArquivoAluno = "alunos.json";
    static string nomeArquivoProf = "professores.json";

    //===================== LISTAS DE DADOS =====================
    static List<Turma> turmas = CarregarTurmas();
    static List<Aluno> alunos = CarregarAlunos();
    static List<Professor> professores = CarregarProfessores();

    //===================== PERSISTÊNCIA ==========================================

    //===================== TURMAS =====================
    static void SalvarTurmas()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(turmas, options);
        File.WriteAllText(nomeArquivoTurma, json);
    }

    static List<Turma> CarregarTurmas()
    {
        if (!File.Exists(nomeArquivoTurma))
            return new List<Turma>();

        string json = File.ReadAllText(nomeArquivoTurma);

        try
        {
            return JsonSerializer.Deserialize<List<Turma>>(json) ?? new List<Turma>();
        }
        catch
        {
            return new List<Turma>();
        }
    }

    //===================== ALUNO =====================
    static void SalvarAlunos()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(alunos, options);
        File.WriteAllText(nomeArquivoAluno, json);
    }

    static List<Aluno> CarregarAlunos()
    {
        if (!File.Exists(nomeArquivoAluno))
            return new List<Aluno>();

        string json = File.ReadAllText(nomeArquivoAluno);

        try
        {
            return JsonSerializer.Deserialize<List<Aluno>>(json) ?? new List<Aluno>();
        }
        catch
        {
            return new List<Aluno>();
        }
    }

    //===================== PROFESSORES =====================
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

    //CHECKMARK
    static char check = '\u2714';

    //===================== MENU PRINCIPAL ======================================================
    static void Main()
    {

        bool continuar = true;

        while (continuar)
        {
            Console.Clear();

            var header = new Panel(new Align(new Markup($"[bold green]SISTEMA DE GETÃO ESCOLAR[/]"), HorizontalAlignment.Center))
                .Border(BoxBorder.Double)
                .Expand();

            var data = DateTime.Now.ToString("Iniciado: dd/MM/yyyy HH:mm:ss");
            var relogio = new Panel( new Align( new Markup($"[yellow]{data}[/]"), HorizontalAlignment.Right));

            AnsiConsole.Write(header);
            AnsiConsole.Write(relogio);
            AnsiConsole.Write(new Rule("[yellow]Menu Principal[/]"));
            AnsiConsole.WriteLine();

            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Selecione uma [green]opção[/]:")
                    .AddChoices(new[] {
                    "1. Gestão de Turmas",
                    "2. Gestão de Alunos",
                    "3. Gestão de Professores",
                    "4. Estatísticas Gerais",
                    "0. Sair"
                }));

            if (opcao.StartsWith("0")) break;

            switch (opcao[0])
            {
                case '1': MenuTurmas(); break;
                case '2': AlunoService.MenuAlunos(); break;
                case '3': MenuProfessores(); break;
                case '4': ExibirEstatisticas(); break;
                case '0': EncerrarSistema(); continuar = false; break;
            }

            AnsiConsole.MarkupLine("\n[italic grey]Pressione qualquer tecla para retornar ao menu...[/]");
            Console.ReadKey();
        }
    }

    //===================== ENCERRAR SISTEMA ==========================================
    static void EncerrarSistema()
    {
        AnsiConsole.Status().Start("Encerrando o sistema...", ctx => {
            Thread.Sleep(1500);
        });
    }

    ////===================== MENU DE ALUNOS ======================================================
    //static void MenuAlunos()
    //{
    //    while (true)
    //    {
    //        Console.Clear();
    //        AnsiConsole.Write(new Rule("[bold green]GESTÃO DE ALUNOS[/]"));

    //        var opcao = AnsiConsole.Prompt(
    //            new SelectionPrompt<string>()
    //                .Title("Selecione uma ação:")
    //                .AddChoices(new[] {
    //                "1. Cadastrar Aluno",
    //                "2. Listar Alunos",
    //                "3. Detalhes do Aluno",
    //                "4. Adicionar Nota",
    //                "5. Remover Aluno",
    //                "0. Voltar"
    //            }));

    //        if (opcao.StartsWith("0")) break;

    //        switch (opcao[0])
    //        {
    //            case '1': CadastrarAluno(); break;
    //            case '2': ListarAlunos(); break;
    //            case '3': ExibirDetalheAluno(); break;
    //            case '4': AdicionarNota(); break;
    //            case '5': RemoverAluno(); break;
    //        }

    //        AnsiConsole.MarkupLine("\n[italic grey]Pressione qualquer tecla para retornar ao menu...[/]");
    //        Console.ReadKey();
    //    }
    //}

    ////===================== CADASTRO DE ALUNO ==========================================
    //static void CadastrarAluno()
    //{

    //    if (turmas.Count == 0)
    //    {
    //        AnsiConsole.MarkupLine("\n[yellow]! Nenhuma turma foi aberta até o momento.[/]");
    //        return;
    //    }

    //    var nome = AnsiConsole.Prompt(new TextPrompt<string>("Nome do Aluno:")
    //        .ValidationErrorMessage("[red]Nome inválido (digite apenas letras)[/]"));
    //    var cpf = AnsiConsole.Prompt(new TextPrompt<string>("CPF do Aluno:")
    //        .ValidationErrorMessage("[red]CPF inválido (digite apenas números)[/]")
    //        .Validate(cpf =>
    //        {
    //            if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
    //                return ValidationResult.Error("[red]CPF deve conter exatamente 11 números[/]");

    //            //if (alunos.Any(a => a.CPF == cpf))
    //            //    AnsiConsole.MarkupLine("[red]CPF já cadastrado![/]");
    //            //    return;

    //            return ValidationResult.Success();
    //        })
    //    );
    //    var idade = AnsiConsole.Prompt(new TextPrompt<int>("Idade do Aluno:")
    //        .ValidationErrorMessage("[red]Idade inválida[/]"));

    //    var turmaSelecionada = AnsiConsole.Prompt(
    //        new SelectionPrompt<Turma>()
    //            .Title("Selecione a turma do [blue]aluno[/]:")
    //            .AddChoices(turmas)
    //            .UseConverter(t => $"Turma: {t.GetNomeTurma()}")
    //    );

    //    var novoAluno = new Aluno( nome, idade, cpf, turmaSelecionada.GetNomeTurma().ToString(), new List<double>());

    //    alunos.Add(novoAluno);

    //    turmaSelecionada.AdicionarAluno(novoAluno);

    //    SalvarAlunos();
    //    SalvarTurmas();

    //    AnsiConsole.MarkupLine($"\n {check} [green] Aluno [bold]{nome}[/] cadastrado com sucesso![/]");
    //}

    ////===================== DETALHES DE ALUNO ==========================================
    //static void ExibirDetalheAluno()
    //{

    //    if (alunos.Count == 0)
    //    {
    //        AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
    //        return;
    //    }

    //    var alunoSelecionado = AnsiConsole.Prompt(
    //        new SelectionPrompt<Aluno>()
    //            .Title("Selecione o [blue]aluno[/] para ver detalhes:")
    //            .AddChoices(alunos)
    //            .UseConverter(a => $"{a.GetNome()} (Turma: {a.GetTurma()})")
    //    );

    //    string cpf = alunoSelecionado.GetCPF();
    //    string cpfFormatado = string.Format(@"{0:000\.000\.000\-00}", Convert.ToUInt64(cpf));

    //    var card = new Panel(new Markup(
    //        $"[bold]CPF:[/] {cpfFormatado}\n" +
    //        $"[bold]Idade:[/] {alunoSelecionado.GetIdade()} anos\n" +
    //        $"[bold]Média Acadêmica:[/] [yellow]{alunoSelecionado.CalcularMedia():F2}[/]\n" +
    //        $"[bold]Notas:[/] {(alunoSelecionado.GetNotas().Count > 0
    //            ? string.Join(" | ", alunoSelecionado.GetNotas().Select(n => n.ToString("F2")))
    //            : "[red]N/D[/]")}"
    //    ));

    //    card.Header($"Ficha Cadastral: [bold]{alunoSelecionado.GetNome()}[/]");
    //    card.BorderColor(Color.Blue);

    //    AnsiConsole.Write(card);
    //}

    ////===================== LISTA DE ALUNO ==========================================
    //static void ListarAlunos()
    //{

    //    if (alunos.Count == 0)
    //    {
    //        AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
    //        return;
    //    }

    //    var tabela = new Table().Border(TableBorder.Rounded).Expand();
    //    tabela.AddColumn("[blue]ID[/]");
    //    tabela.AddColumn("[blue]Nome[/]");
    //    tabela.AddColumn("[blue]Turma[/]");
    //    tabela.AddColumn("[blue]Média[/]");

    //    for (int i = 0; i < alunos.Count; i++)
    //    {
    //        tabela.AddRow(
    //            (i + 1).ToString(),
    //            alunos[i].GetNome() ?? "[red]N/A[/]",
    //            alunos[i].GetTurma() ?? "[red]N/A[/]",
    //            $"[green]{alunos[i].CalcularMedia():F2}[/]"
    //        );
    //    }

    //    AnsiConsole.Write(tabela);
    //}

    ////===================== ADICIONAR NOTA ==========================================
    //static void AdicionarNota()
    //{

    //    if (alunos.Count == 0)
    //    {
    //        AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
    //        return;
    //    }

    //    ListarAlunos();

    //    var alunoSelecionado = AnsiConsole.Prompt(
    //        new SelectionPrompt<Aluno>()
    //            .Title("Selecione o [blue]aluno[/] que receberar a nota:")
    //            .AddChoices(alunos)
    //            .UseConverter(a => $"{a.GetNome()} (Turma: {a.GetTurma()})")
    //    );

    //    double nota = AnsiConsole
    //                    .Prompt(new TextPrompt<double>("Digite a [blue]nota[/] a ser adicionada (0 a 10): ")
    //                        .ValidationErrorMessage("[red]Nota inválida (digite um número de 0 a 10)[/]")
    //                        .Validate(notaNova => {
    //                            if (notaNova >= 0 && notaNova <= 10)
    //                                return ValidationResult.Success();

    //                            return ValidationResult.Error("[red]A nota deve ser um número de 0 a 10[/]");
    //                        })
    //                    );

    //    alunoSelecionado.GetNotas().Add(nota);

    //    SalvarAlunos();

    //    AnsiConsole.MarkupLine($"\n {check} [green] Nota de {alunoSelecionado.GetNome()} adicionada com sucesso ![/]");
    //}

    ////===================== REMOVER ==========================================
    //static void RemoverAluno()
    //{
    //    if (alunos.Count == 0)
    //    {
    //        AnsiConsole.MarkupLine("\n[yellow]! Nenhum aluno cadastrado.[/]");
    //        return;
    //    }

    //    var alunoSelecionado = AnsiConsole.Prompt(
    //        new SelectionPrompt<Aluno>()
    //            .Title("Selecione o [blue]aluno[/] que sera removido:")
    //            .AddChoices(alunos)
    //            .UseConverter(a => $"{a.GetNome()} | CPF: {a.GetCPF()} | Turma: {a.GetTurma()}")
    //    );

    //    var turmaDoAluno = turmas.FirstOrDefault(t => t.GetNomeTurma().ToString() == alunoSelecionado.GetTurma());

    //    if (turmaDoAluno != null)
    //    {
    //        turmaDoAluno.RemoverAluno(alunoSelecionado);
    //    }

    //    var confirmar = AnsiConsole.Prompt(
    //                    new SelectionPrompt<string>()
    //                        .Title("Tem certeza que deseja remover este aluno?")
    //                        .AddChoices(new[] {
    //                             "1. Sim",
    //                             "0. Não"
    //                        })
    //                    );

    //    if (confirmar.StartsWith("0"))
    //        return;

    //    alunos.Remove(alunoSelecionado);

    //    SalvarAlunos();
    //    SalvarTurmas();

    //    AnsiConsole.MarkupLine($"\n {check} [green] Aluno [bold]{alunoSelecionado.GetNome()}[/] removido com sucesso![/]");
    //}

    ////===================== MENU DE PROFESSORES ======================================================
    //static void MenuProfessores()
    //{
    //    while (true)
    //    {
    //        Console.Clear();
    //        AnsiConsole.Write(new Rule("[bold green]GESTÃO DE PROFESSORES[/]").RuleStyle("grey"));

    //        var opcao = AnsiConsole.Prompt(
    //            new SelectionPrompt<string>()
    //                .Title("Selecione uma ação:")
    //                .PageSize(10)
    //                .AddChoices(new[] {
    //                "1. Cadastrar Professor",
    //                "2. Listar Professores",
    //                "3. Atualizar Salário",
    //                "4. Remover Professor",
    //                "0. Voltar"
    //                }));

    //        if (opcao.StartsWith("0")) break;

    //        switch (opcao[0])
    //        {
    //            case '1': CadastrarProfessor(); break;
    //            case '2': ListarProfessores(); break;
    //            case '3': SalarioProfessor(); break;
    //            case '4': RemoverProfessor(); break;
    //        }

    //        AnsiConsole.MarkupLine("\n[grey]Pressione qualquer tecla para continuar...[/]");
    //        Console.ReadKey(true);
    //    }
    //}

    ////===================== CADASTRO PROFESSORES ==========================================
    //static void CadastrarProfessor()
    //{

    //    var nome = AnsiConsole.Prompt(new TextPrompt<string>("Nome do Professor:")
    //        .ValidationErrorMessage("[red]Nome inválido (digite apenas letras)[/]"));

    //    var cpf = AnsiConsole.Prompt(new TextPrompt<string>("CPF do Professor:")
    //        .ValidationErrorMessage("[red]CPF inválido (digite apenas números)[/]")
    //        .Validate(cpf =>
    //        {
    //            if (cpf.Length != 11)
    //                return ValidationResult.Error("[red]CPF deve conter exatamente 11 números[/]");

    //            if (!Regex.IsMatch(cpf, @"^\d{11}$"))
    //                return ValidationResult.Error("[red]CPF deve conter exatamente 11 números[/]");

    //            return ValidationResult.Success();
    //        })
    //    );
    //    var idade = AnsiConsole.Prompt(
    //        new TextPrompt<int>("Idade:")
    //            .ValidationErrorMessage("[red]Por favor, insira uma idade válida.[/]")
    //            .Validate(age => age >= 18 ? ValidationResult.Success() : ValidationResult.Error("[red]O professor deve ser maior de idade.[/]"))
    //    );
    //    var disciplina = AnsiConsole.Ask<string>("Disciplina/Matéria:");

    //    Professor novoProfessor = new(nome, idade, cpf, disciplina, new List<decimal>(), new List<string>());
    //    professores.Add(novoProfessor);

    //    SalvarProfessores();

    //    AnsiConsole.MarkupLine($"\n {check} [green] Professor [bold]{nome}[/] cadastrado com sucesso![/]");
    //}

    ////===================== LISTA PROFESSORES ==========================================
    //static void ListarProfessores()
    //{
    //    if (professores.Count == 0)
    //    {
    //        AnsiConsole.MarkupLine("\n[yellow]! Nenhum professor cadastrado no sistema.[/]");
    //        return;
    //    }

    //    var tabela = new Table().Border(TableBorder.Rounded).Expand();
    //    tabela.AddColumn("[blue]ID[/]");
    //    tabela.AddColumn("[blue]Nome[/]");
    //    tabela.AddColumn("[blue]CPF[/]");
    //    tabela.AddColumn("[blue]Disciplina[/]");
    //    tabela.AddColumn("[blue]Último Salário[/]");

    //    for (int i = 0; i < professores.Count; i++)
    //    {
    //        var p = professores[i];
    //        decimal ultimoSalario = p.GetSalarios().Count > 0 ? p.GetSalarios()[^1] : 0;

    //        string cpf = p.GetCPF();
    //        string cpfFormatado = string.Format(@"{0:000\.000\.000\-00}", Convert.ToUInt64(cpf));

    //        tabela.AddRow(
    //            (i + 1).ToString(),
    //            p.GetNome(),
    //            cpfFormatado,
    //            $"[italic]{p.GetDisciplina()}[/]",
    //            $"[green]{ultimoSalario}[/]"
    //        );
    //        Console.WriteLine(ultimoSalario);
    //    }

    //    AnsiConsole.Write(tabela);
    //}

    ////===================== SALARIO ==========================================
    //static void SalarioProfessor()
    //{
    //    if (professores.Count == 0)
    //    {
    //        AnsiConsole.MarkupLine("\n[yellow]! Nenhum professor cadastrado.[/]");
    //        return;
    //    }

    //    var prof = AnsiConsole.Prompt(
    //        new SelectionPrompt<Professor>()
    //            .Title("Atualizar salário de qual professor?")
    //            .AddChoices(professores)
    //            .UseConverter(p => p.GetNome())
    //    );

    //    var novoSalario = AnsiConsole.Prompt(new TextPrompt<decimal>($"Novo salário para [green]{prof.GetNome()}[/] (digite apenas números):")
    //                        .ValidationErrorMessage("[red]Por favor, insira um salario válido.[/]")
    //                        .Validate(salario =>
    //                        {
    //                            //var salarioString = salario.ToString();
    //                            if (salario < 0)
    //                            {
    //                                return ValidationResult.Error("[Red]Por favor, adicione um salario positivo[/]");
    //                            }

    //                            return ValidationResult.Success();
    //                        })
    //                    );

    //    AnsiConsole.Status()
    //        .Start("Atualizando folha de pagamento...", ctx => {
    //            Thread.Sleep(800);
    //            prof.GetSalarios().Add(novoSalario);
    //            SalvarProfessores();
    //        });

    //    AnsiConsole.MarkupLine($"\n {check} [green] Salário atualizado com sucesso![/]");
    //}

    ////===================== REMOVER PROFESSORES ==========================================
    //static void RemoverProfessor()
    //{
    //    if (professores.Count == 0)
    //    {
    //        AnsiConsole.MarkupLine("\n[yellow]! Nenhum professor cadastrado.[/]");
    //        return;
    //    }

    //    var profSelecionado = AnsiConsole.Prompt(
    //        new SelectionPrompt<Professor>()
    //            .Title("Selecione o [blue]professor[/] que sera removido:")
    //            .AddChoices(professores)
    //            .UseConverter(p => $"{p.GetNome()} | CPF: {p.GetCPF()} | Disciplina: {p.GetDisciplina()}")
    //    );

    //    var confirmar = AnsiConsole.Prompt(
    //        new SelectionPrompt<string>()
    //            .Title("Tem certeza que deseja remover este aluno?")
    //            .AddChoices(new[] {
    //                    "1. Sim",
    //                    "0. Não"
    //            })
    //    );

    //    if (confirmar.StartsWith("0"))
    //        return;

    //    professores.Remove(profSelecionado);

    //    SalvarProfessores();

    //    AnsiConsole.MarkupLine($"\n {check} [green] Professor [bold]{profSelecionado.GetNome()}[/] removido com sucesso![/]");
    //}

    //===================== MENU DE TURMAS ======================================================
    static void MenuTurmas()
    {
        while (true)
        {
            Console.Clear();

            var opcao = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Selecione uma ação:")
                    .PageSize(10)
                    .AddChoices(new[] {
                    "1. Abrir Nova Turma",
                    "2. Turmas Abertas",
					//"3. Fechar Turma",
					"0. Voltar"
                    }));

            if (opcao.StartsWith("0")) break;

            switch (opcao[0])
            {
                case '1': AbrirNovaTurma(); break;
                case '2': ListarTurmas(); break;
                    //case '3': ListarTurmas(); break;
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

        bool turmaExiste = turmas.Any(t => t.GetNomeTurma() == nomeTurma);

        if (turmaExiste)
        {
            AnsiConsole.MarkupLine($"\n[red]Já existe uma turma com o nome [bold]{nomeTurma}[/]![/]");
            return;
        }

        turmas.Add(new Turma(nomeTurma));

        SalvarTurmas();

        AnsiConsole.MarkupLine($"\n {check} [green] Turma [bold]{nomeTurma}[/] aberta com sucesso![/]");
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
            var alunosDaTurma = turma.GetAlunos();

            string listaAlunos = alunosDaTurma.Count > 0
                ? string.Join(", ", alunosDaTurma.Select(a => a.GetNome()))
                : "[grey]Nenhum aluno[/]";

            table.AddRow(
                $"[bold]{turma.GetNomeTurma()}[/]",
                alunosDaTurma.Count.ToString(),
                listaAlunos
            );
        }
        AnsiConsole.Write(table);
    }

    //===================== ESTATISTICAS GERAIS ======================================================
    static void ExibirEstatisticas()
    {

        Console.Clear();

        var regra = new Rule("[bold red]ESTATÍSTICAS GERAIS[/]");
        regra.RuleStyle("grey");
        AnsiConsole.Write(regra);

        var grid = new Grid().AddColumns(2);

        var table = new Table().Border(TableBorder.Rounded).Expand();
        table.AddColumn("Tipo").AddColumn("Total");
        table.AddRow("Turmas", $"[blue]{turmas.Count}[/]");
        table.AddRow("Alunos", $"[blue]{alunos.Count}[/]");
        table.AddRow("Professores", $"[green]{professores.Count}[/]");

        if (alunos.Count > 0)
        {
            double media = alunos.Average(a => a.CalcularMedia());
            grid.AddRow(table, new Panel($"[bold]Média Acadêmica:[/] {media:F2}").Expand());
        }
        else
        {
            grid.AddRow(table, new Panel("[yellow]Sem dados de média[/]"));
        }

        //grid.AddRow(new Panel($"[bold]Ultima Atualização:[/] {DateTime.Now:dd/MM/yyyy HH:mm}").Expand());
        AnsiConsole.Write(grid);
    }
}