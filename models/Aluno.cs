namespace ProjetoEscola_API.Models
{
public class Aluno
{

    public int id { get; set; }
    public string? nome { get; set; }
    public string? ra { get; set; }
    public string? email { get; set; }
    public string? telefone { get; set; }
    public DateTime? data_nascimento { get; set; }
    public string? curso { get; set; }
}
}