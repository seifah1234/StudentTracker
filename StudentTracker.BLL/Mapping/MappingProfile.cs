using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTracker.BLL.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<DAL.Entities.Subject, DTOs.SubjectDto>().ReverseMap();
        }
    }
}
