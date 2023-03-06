namespace ProjetoEscola_API.Models
{
public class Professor
{
    public int id { get; set; }
    public string? nome { get; set; }
    public string? email { get; set; }
    public string? telefone { get; set; }
    public DateTime? data_nascimento { get; set; }
    public string? disciplina { get; set; }
}
}