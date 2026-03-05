using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Spectre.Console;
using System.Text.Json;

//MELHORIAS FUTURAS:
// - opcao de voltar em salario
// - opção de remover nota
// - adicionar sistema de boletim

class Program
{

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

            var data = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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
                    })
            );

            if (opcao.StartsWith("0")) break;

            switch (opcao[0])
            {
                case '1': TurmaService.MenuTurmas(); break;
                case '2': AlunoService.MenuAlunos(); break;
                case '3': ProfessorService.MenuProfessores(); break;
                case '4': EstatisticaService.ExibirEstatisticas(); break;
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
}