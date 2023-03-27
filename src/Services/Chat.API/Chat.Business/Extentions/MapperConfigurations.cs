using AutoMapper;
using Chat.Business.Models.AccountModels;
using Chat.Entities.DatabaseEntities.GPTUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Business.Extentions
{
    public class MapperConfigurations : Profile
    {
        public MapperConfigurations()
        {
            CreateMap<GPTUser, RegisterDto>().ReverseMap();
        }
    }
}
