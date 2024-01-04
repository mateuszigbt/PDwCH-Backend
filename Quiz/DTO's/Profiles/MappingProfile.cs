using AutoMapper;
using Quiz.DTO_s.Profiles;
using Quiz.DTO_s;
using Quiz.Model;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Login))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin))
            .ForMember(dest => dest.QuizProfile, opt => opt.MapFrom(src => src.QuizProfile.Select(a => new QuizProfileDto { Category = a.Category, Rating = a.Rating, Title = a.Title })))
            //.ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points.Select(a => new PointsDto { Score = a.Score })))
            ;
        CreateMap<Quiz.Model.Quiz, QuizDto>()
            .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new CategoryDto { NameCategory = src.Category.NameCategory }))
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers.Select(a => new AnswerDto { AnswerText = a.AnswerText })))
            .ForMember(dest => dest.CorrectAnswers, opt => opt.MapFrom(src => src.CorrectAnswers.Select(a => new CorrectAnswerDto { correctAnswer = a.correctAnswer })))
            .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.Questions.Select(a => new QuestionDto { QuestionText = a.QuestionText })))
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => MapImages(src.Images)))
        ;
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<Answer, AnswerDto>();
        CreateMap<AnswerDto, Answer>();
        CreateMap<CorrectAnswer, CorrectAnswerDto>();
        CreateMap<CorrectAnswerDto, CorrectAnswer>();
        CreateMap<Question, QuestionDto>();
        CreateMap<QuestionDto, Question>();
        CreateMap<Image, ImageDto>();
        CreateMap<ImageDto, Image>();
        CreateMap<QuizProfile, QuizProfileDto>()
            .ForMember(dest => dest.Points, opt => opt.MapFrom(src => src.Points.Select(a=> new PointsDto { Score = a.Score})));
        CreateMap<QuizProfileDto, QuizProfile>();
    }

    private static List<ImageDto> MapImages(List<Image> images)
    {
        return images.Select(a => new ImageDto { ChoosenImage = a.ChoosenImage, ContentType = a.ContentType }).ToList();
    }
}
