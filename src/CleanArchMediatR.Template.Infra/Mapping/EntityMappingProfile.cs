using AutoMapper;
using CleanArchMediatR.Template.Domain.Entities;
using CleanArchMediatR.Template.Infra.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMediatR.Template.Infra.Mapping
{
    public class EntityMappingProfile: Profile
    {
        public EntityMappingProfile() { 
            
            CreateMap<UserEntity, User>()
                .ReverseMap();
        }
    }
}
