using System;
using Spectre.Console;
using System.Text.Json;

public static class TurmasRepository
{
    //===================== ARQUIVO DE DADOS =====================
    static string nomeArquivoTurma = "turmas.json";

    //===================== PERSISTÊNCIA TURMAS =====================
    public static void SalvarTurmas()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(TurmaService.Turmas, options);
            File.WriteAllText(nomeArquivoTurma, json);
        }

        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Erro ao salvar dados:[/] {ex.Message}");
        }
    }

    public static List<Turma> CarregarTurmas()
    {
        if (!File.Exists(nomeArquivoTurma))
            return new List<Turma>();

        try
        {
            string json = File.ReadAllText(nomeArquivoTurma);

            var lista = JsonSerializer.Deserialize<List<Turma>>(json);

            return lista ?? new List<Turma>();
        }

        catch (JsonException ex)
        {
            AnsiConsole.Write(new Panel($"[red]ERRO DE DADOS:[/] O arquivo '{nomeArquivoTurma}' está corrompido ou em formato inválido.\n[grey]Detalhe: {ex.Message}[/]")
                .Header("[bold red] Falha na Persistência [/]")
                .Border(BoxBorder.Double));

            return new List<Turma>();
        }

        catch (IOException ex)
        {
            AnsiConsole.MarkupLine($"[red]ERRO DE LEITURA:[/] Não foi possível acessar o arquivo. {ex.Message}");
            return new List<Turma>();
        }

        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]ERRO INESPERADO:[/] {ex.Message}");
            return new List<Turma>();
        }
    }
}
