using System.ComponentModel.DataAnnotations;

namespace Projeto.Models
{
    public class RedefinirSenhaModel
    {

        [Required(ErrorMessage = "Digite o Login")]
        public string Login { get; set; }


        [Required(ErrorMessage = "Digite o Email para envio")]
        public string Email { get; set; }
    }
}
