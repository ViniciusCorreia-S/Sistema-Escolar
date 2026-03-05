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
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(AlunoService.Alunos, options);
        File.WriteAllText(nomeArquivoAluno, json);
    }

    public static List<Aluno> CarregarAlunos()
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
}
