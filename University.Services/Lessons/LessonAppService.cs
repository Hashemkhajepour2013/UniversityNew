using University.Entities;
using University.Services.Lessons.Contracts;
using University.Services.Lessons.Contracts.Dtos;
using University.Services.Lessons.Extensions;

namespace University.Services.Lessons;

public sealed class LessonAppService : LessonService
{
    private readonly LessonRepository _repository;
    private readonly UnitOfWork _unitOfWork;
    public LessonAppService(
        LessonRepository repository,
        UnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> Add(AddLessonDto dto)
    {
        var lesson = PrototypingOfLesson(dto);
        
        await _repository.Add(lesson);
        
        await _unitOfWork.Complete();
        
        return lesson.Id;
    }

    public async Task Update(UpdateLessonDto dto, int id)
    {
        var lesson = await StopIfLessonNotFound(id);
        
        EditLesson(dto, lesson);
        
        await _unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        var lesson = await StopIfLessonNotFound(id);
        
        _repository.Delete(lesson);
        
        await _unitOfWork.Complete();
    }
    
    private async Task<Lesson> StopIfLessonNotFound(int id)
    {
        var lesson = await _repository.FindById(id);
        if (lesson == null)
        {
            throw new LessonNotFoundException();
        }

        return lesson;
    }
    
    private static Lesson PrototypingOfLesson(AddLessonDto dto)
    {
        var lesson = new Lesson
        {
            Title = dto.Title,
            Coefficient = dto.Coefficient
        };
        return lesson;
    }
    
    private static void EditLesson(UpdateLessonDto dto, Lesson lesson)
    {
        lesson.Title = dto.Title;
        lesson.Coefficient = dto.Coefficient;
    }
}