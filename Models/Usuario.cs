using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CenterPsy_Backend.Models;

public class Usuario
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

    public string Senha { get; set; } = "";
    public string Tipo { get; set; } = "";
}