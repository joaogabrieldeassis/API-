using System.ComponentModel.DataAnnotations;
namespace shop.Model
{
    public class Category
    {
        [Key]
        public int Id{ get; set; }

        [Required(ErrorMessage ="Esse Campo é obrigatorio")]
        [MaxLength(70,ErrorMessage ="Esse campo pode ter no máximo 70 caracters")]
        [MinLength(4,ErrorMessage ="Esse campo deve conter no minimo 4 caracters")]
        public string Title{ get; set; }
    }
}
