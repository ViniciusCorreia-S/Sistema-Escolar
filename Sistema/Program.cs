//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Text.RegularExpressions;
//using Spectre.Console;

////MELHORIAS FUTURAS:
//// - Adicionar funcionalidade de turmas (vincular alunos a turmas e professores a turmas)

//class Program
//{

//    //LISTAS DE PESSOAS
//    static List<Aluno> alunos = new List<Aluno>();
//    static List<Professor> professores = new List<Professor>();


//    //===================== MENU PRINCIPAL ======================================================

//    //CHECKMARK
//    static char check = '\u2714';

//    static void Main()
//    {

//        bool continuar = true;

//        while (continuar)
//        {
//            Console.Clear();

//            var header = new Panel(new Text("SISTEMA DE GESTÃO ESCOLAR", new Style(Color.Green)))
//                .Border(BoxBorder.Double)
//                .Expand();

//            AnsiConsole.Write(header);
//            AnsiConsole.Write(new Rule("[yellow]Menu Principal[/]"));
//            AnsiConsole.WriteLine();

//            var opcao = AnsiConsole.Prompt(
//                new SelectionPrompt<string>()
//                    .Title("Selecione uma [green]opção[/]:")
//                    .AddChoices(new[] {
//                    "1. Gestão de Alunos",
//                    "2. Gestão de Professores",
//                    "3. Estatísticas Gerais",
//                    "0. Sair"
//                }));

//            if (opcao.StartsWith("0")) break;

//            switch (opcao[0])
//            {
//                case '1': MenuAlunos(); break;
//                case '2': MenuProfessores(); break;
//                case '3': ExibirEstatisticas(); break;
//                case '0': EncerrarSistema(); continuar = false; break;
//            }
//            // switch (opcao)
//            // {
//            // 	case "1. Gestão de Alunos":
//            // 		MenuAlunos();
//            // 		break;

//            // 	case "2. Gestão de Professores":
//            // 		MenuProfessores();
//            // 		break;

//            // 	case "3. Estatísticas Gerais":
//            // 		ExibirEstatisticas();
//            // 		break;

//            // 	case "0. Sair":
//            // 		continuar = false;
//            // 		EncerrarSistema();
//            // 		break;
//            // }

//            AnsiConsole.MarkupLine("\n[italic grey]Pressione qualquer tecla para retornar ao menu...[/]");
//            Console.ReadKey();
//        }
//    }

//    static void EncerrarSistema()
//    {
//        AnsiConsole.Status().Start("Encerrando o sistema...", ctx => {
//            Thread.Sleep(1500);
//        });
//        AnsiConsole.Write(new FigletText("[blue]Ate Logo![/]"));
//    }

//    //static void DestacarMensagem(string mensagem, ConsoleColor cor)
//    //{
//    //	Console.ForegroundColor = cor;
//    //	Console.WriteLine(mensagem);
//    //	Console.ResetColor();
//    //}



//    //===================== ALUNOS ======================================================

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


//    //===================== PROFESSORES ======================================================

//    static void MenuProfessores()
//    {
//        while (true)
//        {
//            Console.Clear();
//            AnsiConsole.Write(new Rule("[bold green]GESTÃO DE PROFESSORES[/]").RuleStyle("grey"));

//            var opcao = AnsiConsole.Prompt(
//                new SelectionPrompt<string>()
//                    .Title("Selecione uma ação:")
//                    .PageSize(10)
//                    .AddChoices(new[] {
//                    "1. Cadastrar Professor",
//                    "2. Listar Professores",
//                    "3. Atualizar Salário",
//                    "4. Remover Professor",
//                    "0. Voltar"
//                    }));

//            if (opcao.StartsWith("0")) break;

//            switch (opcao[0])
//            {
//                case '1': CadastrarProfessor(); break;
//                case '2': ListarProfessores(); break;
//                case '3': SalarioProfessor(); break;
//                case '4': RemoverProfessor(); break;
//            }

//            AnsiConsole.MarkupLine("\n[grey]Pressione qualquer tecla para continuar...[/]");
//            Console.ReadKey(true);
//        }
//    }

//    static void CadastrarProfessor()
//    {

//        var opcao = AnsiConsole.Prompt(
//                        new SelectionPrompt<string>()
//                            .Title("Deseja cadastrar um professor?")
//                            .AddChoices(new[] {
//                                 "1. Sim",
//                                 "0. Não"
//                            })
//                        );

//        if (opcao.StartsWith("0"))
//            return;

//        // AnsiConsole.Write(new Rule("[bold blue]NOVO REGISTRO DE PROFESSOR[/]"));

//        // var nome = AnsiConsole.Ask<string>("Nome completo:");
//        var nome = AnsiConsole.Prompt(new TextPrompt<string>("Nome do Professor:")
//            .ValidationErrorMessage("[red]Nome inválido (digite apenas letras)[/]"));
//        // var cpf = AnsiConsole.Ask<string>("CPF (somente números):");
//        var cpf = AnsiConsole.Prompt(new TextPrompt<string>("CPF do Professor:")
//            .ValidationErrorMessage("[red]CPF inválido (digite apenas números)[/]")
//            .Validate(cpf =>
//            {
//                if (cpf.Length != 11)
//                    return ValidationResult.Error("[red]CPF deve conter exatamente 11 números[/]");

//                if (!Regex.IsMatch(cpf, @"^\d{11}$"))
//                    return ValidationResult.Error("[red]CPF deve conter exatamente 11 números[/]");

//                return ValidationResult.Success();
//            })
//        );
//        var idade = AnsiConsole.Prompt(
//            new TextPrompt<int>("Idade:")
//                .ValidationErrorMessage("[red]Por favor, insira uma idade válida.[/]")
//                .Validate(age => age >= 18 ? ValidationResult.Success() : ValidationResult.Error("[red]O professor deve ser maior de idade.[/]"))
//        );
//        var disciplina = AnsiConsole.Ask<string>("Disciplina/Matéria:");

//        Professor novoProfessor = new(nome, idade, cpf, disciplina, new List<decimal>(), new List<string>());
//        professores.Add(novoProfessor);

//        AnsiConsole.MarkupLine($"\n {check} [green]Professor [bold]{nome}[/] cadastrado com ID:[/] [blue]{professores.Count}[/]");
//    }

//    static void ListarProfessores()
//    {
//        if (professores.Count == 0)
//        {
//            AnsiConsole.MarkupLine("\n[yellow]! Nenhum professor cadastrado no sistema.[/]");
//            return;
//        }

//        var tabela = new Table().Border(TableBorder.Rounded).Expand();
//        tabela.AddColumn("[blue]ID[/]");
//        tabela.AddColumn("[blue]Nome[/]");
//        tabela.AddColumn("[blue]CPF[/]");
//        tabela.AddColumn("[blue]Disciplina[/]");
//        tabela.AddColumn("[blue]Último Salário[/]");

//        for (int i = 0; i < professores.Count; i++)
//        {
//            var p = professores[i];
//            decimal ultimoSalario = p.GetSalarios().Count > 0 ? p.GetSalarios()[^1] : 0;

//            string cpf = p.GetCPF();
//            string cpfFormatado = string.Format(@"{0:000\.000\.000\-00}", Convert.ToUInt64(cpf));

//            tabela.AddRow(
//                (i + 1).ToString(),
//                p.GetNome(),
//                cpfFormatado,
//                $"[italic]{p.GetDisciplina()}[/]",
//                $"[green]{ultimoSalario:C}[/]"
//            );
//            Console.WriteLine(ultimoSalario);
//        }

//        AnsiConsole.Write(tabela);
//    }

//    static void SalarioProfessor()
//    {
//        if (professores.Count == 0)
//        {
//            AnsiConsole.MarkupLine("\n[yellow]! Nenhum professor cadastrado.[/]");
//            return;
//        }

//        var prof = AnsiConsole.Prompt(
//            new SelectionPrompt<Professor>()
//                .Title("Atualizar salário de qual professor?")
//                .AddChoices(professores)
//                .UseConverter(p => p.GetNome())
//        );

//        // var novoSalario = AnsiConsole.Ask<decimal>($"Novo salário para [green]{prof.GetNome()}[/]:");
//        var novoSalario = AnsiConsole.Prompt(new TextPrompt<decimal>($"Novo salário para [green]{prof.GetNome()}[/] (digite apenas números):")
//                            .ValidationErrorMessage("[red]Por favor, insira um salario válido.[/]")
//                            .Validate(salario =>
//                            {
//                                //var salarioString = salario.ToString();
//                                if (salario < 0)
//                                {
//                                    return ValidationResult.Error("[Red]Por favor, adicione um salario positivo[/]");
//                                }

//                                //if (!Regex.IsMatch(salarioString, @"."))
//                                //{
//                                //	return ValidationResult.Error("[red]Por favor, digite apenas números[/]");
//                                //}

//                                return ValidationResult.Success();
//                            })
//                        );

//        AnsiConsole.Status()
//            .Start("Atualizando folha de pagamento...", ctx => {
//                Thread.Sleep(800);
//                prof.GetSalarios().Add(novoSalario);
//            });

//        AnsiConsole.MarkupLine($"\n {check} [green]Salário atualizado com sucesso![/]");
//    }

//    static void RemoverProfessor()
//    {
//        if (professores.Count == 0)
//        {
//            AnsiConsole.MarkupLine("\n[yellow]! Nenhum professor cadastrado.[/]");
//            return;
//        }

//        var profSelecionado = AnsiConsole.Prompt(
//            new SelectionPrompt<Professor>()
//                .Title("Selecione o [blue]professor[/] que sera removido:")
//                .AddChoices(professores)
//                .UseConverter(p => $"{p.GetNome()} | CPF: {p.GetCPF()} | Disciplina: {p.GetDisciplina()}")
//        );

//        professores.Remove(profSelecionado);

//        AnsiConsole.MarkupLine($"\n {check} [green]Professor [bold]{profSelecionado.GetNome()}[/] removido com sucesso![/]");
//    }

//    //===================== ESTATISTICAS ======================================================

//    static void ExibirEstatisticas()
//    {

//        Console.Clear();

//        var regra = new Rule("[bold red]ESTATÍSTICAS GERAIS[/]");
//        regra.RuleStyle("grey");
//        AnsiConsole.Write(regra);

//        var grid = new Grid().AddColumns(2);

//        var table = new Table().Border(TableBorder.Rounded);
//        table.AddColumn("Tipo").AddColumn("Total");
//        table.AddRow("Alunos", $"[blue]{alunos.Count}[/]");
//        table.AddRow("Professores", $"[green]{professores.Count}[/]");

//        if (alunos.Count > 0)
//        {
//            double media = alunos.Average(a => a.CalcularMedia());
//            grid.AddRow(table, new Panel($"[bold]Média Acadêmica:[/] {media:F2}").Expand());
//        }
//        else
//        {
//            grid.AddRow(table, new Text("[yellow]Sem dados de média[/]"));
//        }

//        AnsiConsole.Write(grid);
//        AnsiConsole.WriteLine();
//    }

//    // static string ReadNonEmptyString(string prompt)
//    // {
//    // 	while (true)
//    // 	{
//    // 		AnsiConsole.MarkupLine(prompt);

//    // 		var entrada = Console.ReadLine()?.Trim();

//    // 		if (!string.IsNullOrEmpty(entrada)) return entrada;
//    // 		DestacarMensagem("Entrada vazia. Tente novamente.", ConsoleColor.Yellow);
//    // 	}
//    // }

//    // static int ReadIntInRange(string prompt, int min, int max)
//    // {
//    // 	while (true)
//    // 	{
//    // 		AnsiConsole.MarkupLine(prompt);

//    // 		var line = Console.ReadLine();
//    // 		int value;

//    // 		if (int.TryParse(line, out value) && value >= min && value <= max)
//    // 			return value;
//    // 		DestacarMensagem($"Entrada inválida. Digite um número inteiro maior que {min}.", ConsoleColor.Red);
//    // 	}
//    // }

//    // static double ReadDoubleInRange(string prompt, double min, double max)
//    // {
//    // 	while (true)
//    // 	{
//    // 		AnsiConsole.MarkupLine(prompt);

//    // 		var line = Console.ReadLine();
//    // 		double value;

//    // 		if (double.TryParse(line, out value) && value >= min && value <= max)
//    // 			return value;
//    // 		//if (double.TryParse(line, NumberStyles.Number, CultureInfo.CurrentCulture, out double value) && value >= min && value <= max)
//    // 		//	return value;
//    // 		DestacarMensagem($"Entrada inválida. Digite um número entre {min} e {max}.", ConsoleColor.Red);
//    // 	}
//    // }

//    // static decimal ReadDecimalInRange(string prompt, decimal min, decimal max)
//    // {
//    // 	while (true)
//    // 	{
//    // 		AnsiConsole.MarkupLine(prompt);

//    // 		var line = Console.ReadLine();
//    // 		decimal value;

//    // 		if (decimal.TryParse(line, out value) && value >= min && value <= max)
//    // 			return value;
//    // 		//if (double.TryParse(line, NumberStyles.Number, CultureInfo.CurrentCulture, out double value) && value >= min && value <= max)
//    // 		//	return value;
//    // 		DestacarMensagem($"Entrada inválida. Digite um número maior que {min}.", ConsoleColor.Red);
//    // 	}
//    // }

//    // static long ReadLongInRange(string prompt, long min, long max)
//    // {
//    // 	while (true)
//    // 	{
//    // 		AnsiConsole.MarkupLine(prompt);

//    // 		var line = Console.ReadLine();
//    // 		long value;

//    // 		if (long.TryParse(line, out value) && value >= min && value <= max)
//    // 			return value;
//    // 		//if (double.TryParse(line, NumberStyles.Number, CultureInfo.CurrentCulture, out double value) && value >= min && value <= max)
//    // 		//	return value;
//    // 		DestacarMensagem($"Entrada inválida. Digite apenas números (11 dígitos).", ConsoleColor.Red);
//    // 	}
//    // }

//    // static decimal ReadDecimalNonNegative(string prompt)
//    // {
//    // 	while (true)
//    // 	{
//    // 		AnsiConsole.MarkupLine(prompt);

//    // 		var line = Console.ReadLine();
//    // 		decimal value;

//    // 		if (decimal.TryParse(line, out value) && value >= 0)
//    // 			return value;
//    // 		DestacarMensagem("Entrada inválida. Digite um número decimal não-negativo.", ConsoleColor.Red);
//    // 	}
//    // }

//    // static int ReadIndexChoice(string prompt, int count)
//    // {
//    // 	while (true)
//    // 	{
//    // 		AnsiConsole.MarkupLine(prompt);
//    // 		var line = Console.ReadLine();
//    // 		if (int.TryParse(line, NumberStyles.Integer, CultureInfo.CurrentCulture, out int value) && value >= 1 && value <= count)
//    // 			return value - 1;
//    // 		DestacarMensagem($"Escolha inválida. Digite um número entre 1 e {count}.", ConsoleColor.Red);
//    // 	}
//    // }
////}