using LocoTesting.Application.Dtos.Option;
using LocoTesting.Application.Dtos.Question;
using LocoTesting.Application.Dtos.Test;

namespace LocoTesting.Application.Interfaces.Services;

public interface ITestService
{
    Task<TDto> GetByIdAsync<T, TDto>(int id)
        where T: class
        where TDto : class;
    Task<IEnumerable<T>> GetAllAsync<T>()
        where T : class;
    Task CreateAsync<T, TDto>(TDto dto)
        where T : class
        where TDto: class;
    Task DeleteAsync<T>(int id)
        where T : class;
    Task UpdateEntityAsync<T, TDto>(TDto dto)
        where T : class
        where TDto : class;
    Task<List<QuestionResponseDto>> GetQuestionsByTestIdAsync(int testId);
}