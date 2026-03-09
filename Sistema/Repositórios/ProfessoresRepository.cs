using System;
using Spectre.Console;
using System.Text.Json;

public class ProfessoresRepository
{
    //===================== ARQUIVO DE DADOS =====================
    static string nomeArquivoProfessor = "professores.json";

    //===================== PERSISTÊNCIA PROFESSORES =====================
    public static void SalvarProfessores()
    {
        try
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(ProfessorService.Professores, options);
            File.WriteAllText(nomeArquivoProfessor, json);
        }

        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]Erro ao salvar dados:[/] {ex.Message}");
        }
    }

    public static List<Professor> CarregarProfessores()
    {
        if (!File.Exists(nomeArquivoProfessor))
            return new List<Professor>();

        try
        {
            string json = File.ReadAllText(nomeArquivoProfessor);

            var lista = JsonSerializer.Deserialize<List<Professor>>(json);

            return lista ?? new List<Professor>();
        }

        catch (JsonException ex)
        {
            AnsiConsole.Write(new Panel($"[red]ERRO DE DADOS:[/] O arquivo '{nomeArquivoProfessor}' está corrompido ou em formato inválido.\n[grey]Detalhe: {ex.Message}[/]")
                .Header("[bold red] Falha na Persistência [/]")
                .Border(BoxBorder.Double));

            return new List<Professor>();
        }

        catch (IOException ex)
        {
            AnsiConsole.MarkupLine($"[red]ERRO DE LEITURA:[/] Não foi possível acessar o arquivo. {ex.Message}");
            return new List<Professor>();
        }

        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]ERRO INESPERADO:[/] {ex.Message}");
            return new List<Professor>();
        }
    }
}
