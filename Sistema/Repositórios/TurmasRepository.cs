using System;
using Spectre.Console;
using System.Text.Json;

public class TurmasRepository
{
    //===================== ARQUIVO DE DADOS =====================
    static string nomeArquivoTurma = "turmas.json";

    //===================== PERSISTÊNCIA TURMAS =====================
    public static void SalvarTurmas()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(TurmaService.Turmas, options);
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
}
