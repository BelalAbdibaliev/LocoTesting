using AutoMapper;
using LocoTesting.Application.Dtos.Question;
using LocoTesting.Application.Interfaces;
using LocoTesting.Application.Interfaces.Services;
using LocoTesting.Domain.Entities;

namespace LocoTesting.Application.Services;

public class TestService : ITestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<TDto> GetByIdAsync<T, TDto>(int id)
        where T: class
        where TDto : class
    {
        var entity = await _unitOfWork.GetRepository<TDto>().GetByIdAsync(id);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>()
    where T : class
    {
        return await _unitOfWork.GetRepository<T>().GetAllAsync();
    }

    public async Task CreateAsync<T, TDto>(TDto dto)
        where T : class
        where TDto : class
    {
        T entity = _mapper.Map<T>(dto);
        
        await _unitOfWork.GetRepository<T>().AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync<T>(int id)
    where T : class
    {
        await _unitOfWork.GetRepository<T>().DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateEntityAsync<T, TDto>(TDto dto) 
        where T : class
        where TDto : class
    {
        var idProperty = typeof(TDto).GetProperty("Id");
        if (idProperty == null)
            throw new Exception("DTO должен содержать свойство Id");

        var idValue = idProperty.GetValue(dto);
        if (idValue == null)
            throw new Exception("Id не может быть null");

        var entity = await _unitOfWork
            .GetRepository<T>()
            .GetByIdAsync((int)idValue);
        if (entity == null)
            throw new Exception($"{typeof(T).Name} с таким Id не найден");

        foreach (var dtoProperty in typeof(TDto).GetProperties())
        {
            if (dtoProperty.Name == "Id") 
                continue;

            var newValue = dtoProperty.GetValue(dto);
            if (newValue == null) 
                continue;

            var entityProperty = typeof(T).GetProperty(dtoProperty.Name);
            if (entityProperty != null && entityProperty.CanWrite)
            {
                entityProperty.SetValue(entity, newValue);
            }
        }

        await _unitOfWork.GetRepository<T>().UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<QuestionResponseDto>> GetQuestionsByTestIdAsync(int testId)
    {
        var questions = await _unitOfWork.Questions.GetByTestIdAsync(testId);
        List<QuestionResponseDto> questionDtos = questions
            .Select(x => _mapper.Map<QuestionResponseDto>(x))
            .ToList();
        return questionDtos;
    }
}