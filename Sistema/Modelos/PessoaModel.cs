using System;

public abstract class Pessoa
{
	public string Nome { get; private set; } = string.Empty;
	public int Idade { get; private set; }
	public string CPF { get; private set; } = string.Empty;

	public Pessoa() { }

	public Pessoa(string nome, int idade, string cpf)
	{
		Nome = nome;
		Idade = idade;
		CPF = cpf;
	}

	public void AlterarNome(string nome)
	{
		Nome = nome;
	}

	public void AlterarIdade(int idade)
	{
		Idade = idade;
	}

	public void AlterarCPF(string cpf)
	{
		CPF = cpf;
	}
}