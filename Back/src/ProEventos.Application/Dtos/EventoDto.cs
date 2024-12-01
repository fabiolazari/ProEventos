using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio."),
        //MinLength(3, ErrorMessage = "{0} deve ter no m�nimo 3 caracteres."),
        //MaxLength(50, ErrorMessage = "{0} deve ter no m�ximo 50 caracteres.")
        StringLength(50, MinimumLength =3, ErrorMessage = "{0} deve ter no m�nimo 3 e m�ximo 50 caracteres.")]
        public string Tema { get; set; }

        [Display(Name = "Qtd Pessoas"),
        Range(1, 120000, ErrorMessage = "N�o pode ser menor que 1 e maior que 120.000")]
        public int QtdPessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$",
            ErrorMessage = "N�o � uma imagem v�lida. (gif, jpg, jpeg, bmp ou png).")]
        public string ImageURL { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio."),
        Phone(ErrorMessage = "O campo {0} est� com caractere inv�lido.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio."),
        Display(Name = "E-mail"),
        EmailAddress(ErrorMessage = "O campo {0} precisa um e-mail v�lido.")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}