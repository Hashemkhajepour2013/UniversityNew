using University.Entities;
using University.Services.Classrooms.Contracts;
using University.Services.Classrooms.Contracts.Dtos;
using University.Services.Classrooms.Extensions;
using University.Services.Lessons.Contracts;
using University.Services.Lessons.Extensions;
using University.Services.Professors.Contracts;
using University.Services.Professors.Extensions;
using University.Services.Terms.Contracts;
using University.Services.Terms.Extensions;

namespace University.Services.Classrooms;

public sealed class ClassroomAppService : ClassroomService
{
    private readonly ClassroomRepository _repository;
    private readonly LessonRepository _lessonRepository;
    private readonly TermRepository _termRepository;
    private readonly ProfessorRepository _professorRepository;
    private readonly UnitOfWork _unitOfWork;
    public ClassroomAppService(
        ClassroomRepository repository,
        LessonRepository lessonRepository, 
        TermRepository termRepository,
        ProfessorRepository professorRepository,
        UnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _professorRepository = professorRepository;
        _lessonRepository = lessonRepository;
        _termRepository = termRepository;
    }
    
    
    public async Task<int> Add(AddClassroomDto dto)
    {
        StopIfStartDateExpired(dto.StartDate);
        
        StopIfEndDateExpired(dto.EndDate, dto.StartDate);

        await StopIfTermNotFound(dto.TermId);

        await StopIfProfessorNotFound(dto.ProfessorId);

        await StopIfLessonNotFound(dto.LessonId);

        var classroom = PrototypingClassroom(dto);
        
        await _repository.Add(classroom);
        
        await _unitOfWork.Complete();
        
        return classroom.Id;
    }
  
    public async Task Update(UpdateClassroomDto dto, int id)
    {
        StopIfStartDateExpired(dto.StartDate);
        
        StopIfEndDateExpired(dto.EndDate, dto.StartDate);
        
        await StopIfTermNotFound(dto.TermId);
        
        await StopIfLessonNotFound(dto.LessonId);

        await StopIfProfessorNotFound(dto.ProfessorId);
        
        var classroom = await StopIfClassroomNotFound(id);

        EditClassroom(dto, classroom);
        
        await _unitOfWork.Complete();
    }

    public async Task Delete(int id)
    {
        var classroom = await StopIfClassroomNotFound(id);
        
        _repository.Delete(classroom);
        
        await _unitOfWork.Complete();
    }
    
    private static void StopIfStartDateExpired(DateTime startDate)
    {
        if (startDate < DateTime.Now)
        {
            throw new StartDateExpiredException();
        }
    }

    private static void StopIfEndDateExpired(DateTime endDate, DateTime startDate)
    {
        if (endDate < startDate)
        {
            throw new EndDateExpiredException();
        }
    }
    
    private static Classroom PrototypingClassroom(AddClassroomDto dto)
    {
        var classroom = new Classroom
        {
            TermId = dto.TermId,
            LessonId = dto.LessonId,
            Capacity = dto.Capacity,
            EndDate = dto.EndDate,
            StartDate = dto.StartDate,
            ProfessorId = dto.ProfessorId
        };
        return classroom;
    }
    
    private async Task StopIfLessonNotFound(int lessonId)
    {
        var lesson = await _lessonRepository.IsExistLesson(lessonId);
        if (!lesson)
        {
            throw new LessonNotFoundException();
        }
    }

    private async Task StopIfProfessorNotFound(int professorId)
    {
        var professor = await _professorRepository.IsExistProfessor(professorId);
        if (!professor)
        {
            throw new ProfessorNotFoundException();
        }
    }

    private async Task StopIfTermNotFound(int termId)
    {
        var term = await _termRepository.IsExistTerm(termId);
        if (!term)
        {
            throw new TermNotFoundException();
        }
    }
    
    private async Task<Classroom> StopIfClassroomNotFound(int id)
    {
        var classroom = await _repository.FindById(id);
        if (classroom == null)
        {
            throw new ClassroomNotFoundException();
        }

        return classroom;
    }
    
    private static void EditClassroom(UpdateClassroomDto dto, Classroom classroom)
    {
        classroom.StartDate = dto.StartDate;
        classroom.EndDate = dto.EndDate;
        classroom.Capacity = dto.Capacity;
        classroom.TermId = dto.TermId;
        classroom.LessonId = dto.LessonId;
        classroom.ProfessorId = dto.ProfessorId;
    }
}