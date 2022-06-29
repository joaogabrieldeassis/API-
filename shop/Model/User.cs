using System.ComponentModel.DataAnnotations;

namespace shop.Model
{
    public class User
    {
        [Key]
        public int Id{ get; set; }

        [Required]
        [MaxLength(255,ErrorMessage ="Este campo suporta no máximo 255 caracters")]
        [MinLength(2,ErrorMessage ="Este campo requer no minimo 2 caracters")]
        public string UserName{ get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Sua senha deve conter no maximo 50 caracters")]
        [MinLength(4, ErrorMessage = "Sua senha deve conter no minimo 4 caracters")]
        public string Password{ get; set; }
        public string Role { get; set; }
    }
}
