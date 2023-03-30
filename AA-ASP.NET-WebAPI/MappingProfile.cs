using AutoMapper;
using TheAzureArchiveAPI.DataTransferObjects.GetObjects;
using TheAzureArchiveAPI.DataTransferObjects.UpdateObjects;

namespace TheAzureArchiveAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Story, UpdateStory>().ReverseMap();
            CreateMap<Article, UpdateArticle>().ReverseMap();
            CreateMap<EmailSubscribed, UpdateEmailSubscribed>().ReverseMap();
            CreateMap<News, UpdateNews>().ReverseMap();
        }
    }
}
