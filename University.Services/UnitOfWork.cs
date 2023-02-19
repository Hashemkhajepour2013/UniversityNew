namespace University.Services;

public interface UnitOfWork
{
    Task Complete();
}