﻿using System.ComponentModel.DataAnnotations;

namespace UPTEAM.Models
{
    public class EquipeModel
    {
        public int IdEquipe { get; set; }
        public string NomeEquipe { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}
