using System.ComponentModel.DataAnnotations;

namespace shop.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(60,ErrorMessage ="O campo pode ter no maximo 60 caracters")]
        [MinLength(4, ErrorMessage = "O campo pode ter no minimo 4 caracters")]
        public string Title { get; set; }

        [MaxLength(2000, ErrorMessage = "O campo pode ter no maximo 2000 caracters")]
        public string Description{ get; set; }
        [Required(ErrorMessage ="Este campo é obrigatorio")]
        [Range(1,double.MaxValue,ErrorMessage ="A compra minima é de R$1,00")]
        public double Price{ get; set; }

        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Categoria invalida")]
        public int CategoryId{ get; set; }
        public Category Category { get; set; }

    }
}
