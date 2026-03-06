using System;
using Spectre.Console;
using System.Text.Json;

public class ProfessoresRepository
{
    //===================== ARQUIVO DE DADOS =====================
    static string nomeArquivoProf = "professores.json";

    //===================== PERSISTÊNCIA PROFESSORES =====================
    public static void SalvarProfessores()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(ProfessorService.Professores, options);
        File.WriteAllText(nomeArquivoProf, json);
    }

    public static List<Professor> CarregarProfessores()
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
}
