using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain.Models
{
    [Table("EVENTOS")]
    public class Evento
    {
        [Key]
        public int Id {get; set;}
        public string Local {get; set;}
        public DateTime? DataEvento {get; set;}

        [NotMapped]
        public int ContagemDias { get; set; }

        [Required,
        MaxLength(50)]
        public string Tema {get; set;}
        public int QtdPessoas {get; set;}
        public string ImageURL {get; set;}
        public string Telefone { get; set; }
        public string Email { get; set; }
        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }
    }
}