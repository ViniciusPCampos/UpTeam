﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPTEAM.Domain.Entities;
using UPTEAM.Models;

namespace UPTEAM.AutoMapper.MappingProfile
{
    public class TbEquipeMappingProfile : Profile
    {
        public TbEquipeMappingProfile()
        {
            CreateMap<tb_equipe, EquipeModel>()
                .ForMember(x => x.NomeEquipe, x => x.MapFrom(y => y.nme_equipe))
                .ForMember(x => x.Descricao, x => x.MapFrom(y => y.dsc_equipe));
        }
    }
}
