using AutoMapper;
using Elearningbackend.DTOs;
using Elearningbackend.Models;

namespace Elearningbackend.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UpdateUserDto, User>();

            // Course mappings
            CreateMap<Course, CourseDto>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator.FullName))
                .ForMember(dest => dest.LessonsCount, opt => opt.MapFrom(src => src.Lessons.Count))
                .ForMember(dest => dest.QuizzesCount, opt => opt.MapFrom(src => src.Quizzes.Count));
            CreateMap<CreateCourseDto, Course>();
            CreateMap<UpdateCourseDto, Course>();

            // Lesson mappings
            CreateMap<Lesson, LessonDto>();
            CreateMap<CreateLessonDto, Lesson>();
            CreateMap<UpdateLessonDto, Lesson>();

            // Quiz mappings
            CreateMap<Quiz, QuizDto>()
                .ForMember(dest => dest.QuestionsCount, opt => opt.MapFrom(src => src.Questions.Count));
            CreateMap<CreateQuizDto, Quiz>();

            // Question mappings
            CreateMap<Question, QuestionDto>();
            CreateMap<CreateQuestionDto, Question>();

            // Result mappings
            CreateMap<Result, QuizResultDto>()
                .ForMember(dest => dest.QuizTitle, opt => opt.MapFrom(src => src.Quiz.Title))
                .ForMember(dest => dest.TotalQuestions, opt => opt.MapFrom(src => src.Quiz.Questions.Count));
        }
    }
}