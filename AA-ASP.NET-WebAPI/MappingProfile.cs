using AutoMapper;
using TheAzureArchiveAPI.DataTransferObjects;
using TheAzureArchiveAPI.DataTransferObjects.CreateUpdateObjects;

namespace TheAzureArchiveAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<GetArticle, CreateUpdateArticle>().ReverseMap();
        }
    }
}
