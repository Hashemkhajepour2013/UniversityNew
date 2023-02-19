using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using University.Entities;
using University.Persistence.EF.Terms;
using University.Services;
using University.Services.Professors;
using University.Services.Professors.Contracts;
using University.Services.Terms;
using University.Services.Terms.Contracts;

namespace University.Persistence.EF;

public sealed class EFDataContext : DbContext
{
    public EFDataContext(DbContextOptions<EFDataContext> options) : base(options)
    {

    }
    public EFDataContext(string connectionString)
        : this(new DbContextOptionsBuilder<EFDataContext>().UseSqlServer(connectionString).Options)
    {
    }
    
    
    public DbSet<Professor> Professors { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<StudentClassroom> StudentClassrooms { get; set; }
    public DbSet<Term> Terms { get; set; }
}