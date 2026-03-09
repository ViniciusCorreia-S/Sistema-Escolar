using System;
using Spectre.Console;
using System.Text.Json;

public static class AlunosRepository
{
    //===================== ARQUIVO DE DADOS =====================
    static string nomeArquivoAluno = "alunos.json";

    //===================== PERSISTÊNCIA ALUNO =====================
    public static void SalvarAlunos()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(AlunoService.Alunos, options);
            File.WriteAllText(nomeArquivoAluno, json);
        }

        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Erro ao salvar dados:[/] {ex.Message}");
        }
    }

    public static List<Aluno> CarregarAlunos()
    {
        if (!File.Exists(nomeArquivoAluno))
            return new List<Aluno>();

        try
        {
            string json = File.ReadAllText(nomeArquivoAluno);

            var lista = JsonSerializer.Deserialize<List<Aluno>>(json);

            return lista ?? new List<Aluno>();
        }

        catch (JsonException ex)
        {
            AnsiConsole.Write(new Panel($"[red]ERRO DE DADOS:[/] O arquivo '{nomeArquivoAluno}' está corrompido ou em formato inválido.\n[grey]Detalhe: {ex.Message}[/]")
                .Header("[bold red] Falha na Persistência [/]")
                .Border(BoxBorder.Double));

            return new List<Aluno>();
        }

        catch (IOException ex)
        {
            AnsiConsole.MarkupLine($"[red]ERRO DE LEITURA:[/] Não foi possível acessar o arquivo. {ex.Message}");
            return new List<Aluno>();
        }

        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]ERRO INESPERADO:[/] {ex.Message}");
            return new List<Aluno>();
        }
    }
}
