using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CenterPsy_Backend.Models;

public class Contato
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Nome { get; set; } = "";

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public string Email { get; set; } = "";

    [Required]
    [Phone]
    [StringLength(40)]
    public string Telefone { get; set; } = "";

    [Required]
    [StringLength(2000)]
    public string Mensagem { get; set; } = "";

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Pendente";

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
