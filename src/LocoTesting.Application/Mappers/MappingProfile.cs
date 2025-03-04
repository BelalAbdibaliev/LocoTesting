using AutoMapper;
using LocoTesting.Application.Dtos;
using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Question;
using LocoTesting.Application.Dtos.Test;
using LocoTesting.Domain.Entities;
using Microsoft.Extensions.Options;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTestDto, Test>();
        CreateMap<CreateQuestionDto, Question>();
        CreateMap<CreateAnswerOptionDto, AnswerOption>();
        CreateMap<Question, QuestionResponseDto>()
            .ForMember(dest => dest.AnswerOptions, opt => opt.MapFrom(src => src.AnswerOptions));
        CreateMap<AnswerOption, AnswerOptionResponseDto>();
        CreateMap<UpdateQuestionDto, Question>();
        CreateMap<CreateAnswerOptionDto, AnswerOption>();
    }
}