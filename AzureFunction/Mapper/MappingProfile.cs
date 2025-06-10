using AutoMapper;
using HellowWord.DTO;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourNamespace;

namespace HellowWord.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDTO>().ForMember(s => s.PrimaryContactId, opt => opt.MapFrom<LookUpEntityReferenceDTO>(src => new LookUpEntityReferenceDTO
            {
                EntityLogicalName = src.PrimaryContactId.LogicalName,
                ID = src.PrimaryContactId.Id,
                Name = src.PrimaryContactId.Name,

            }));
            CreateMap<AccountDTO, Account>().ForMember(s => s.PrimaryContactId,
                opt =>
                {
                    opt.PreCondition(src => src.PrimaryContactId != null);
                    opt.MapFrom<EntityReference>(src => new EntityReference(src.PrimaryContactId.EntityLogicalName, src.PrimaryContactId.ID));
                    });

        }
    }
}
