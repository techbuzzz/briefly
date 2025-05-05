using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Briefly.Core.Domain;
using Briefly.Core.Persistence;
using Briefly.Infrastructure.Persistence;
using FastEndpoints;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Domain;
using Notes.Application.Persistence;

namespace Notes.Application;
public static class NotesModuleInitializer
{
    public static WebApplicationBuilder RegisterNotesServices(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.BindDbContext<NotesDbContext>();
        builder.Services.AddScoped<IDbInitializer, NotesDbInitializer>();
        builder.Services.AddKeyedScoped<IRepository<NoteType>, NotesRepository<NoteType>>("module:notes");
        builder.Services.AddKeyedScoped<IReadRepository<NoteType>, NotesRepository<NoteType>>("module:notes");
        builder.Services.AddKeyedScoped<IRepository<Note>, NotesRepository<Note>>("module:notes");
        builder.Services.AddKeyedScoped<IReadRepository<Note>, NotesRepository<Note>>("module:notes");

        builder.Services.AddFastEndpoints(options =>
        {
            options.Assemblies = [typeof(NotesModuleInitializer).Assembly];
        });

        return builder;
    }
    public static WebApplication UseNotesModule(this WebApplication app)
    {
        return app;
    }
}

internal sealed class NotesRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T>
    where T : class, IAggregateRoot
{
    public NotesRepository(NotesDbContext context)
        : base(context)
    {
    }

    // We override the default behavior when mapping to a dto.
    // We're using Mapster's ProjectToType here to immediately map the result from the database.
    // This is only done when no Selector is defined, so regular specifications with a selector also still work.
    protected override IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification) =>
        specification.Selector is not null
            ? base.ApplySpecification(specification)
            : ApplySpecification(specification, false)
                .ProjectToType<TResult>();
}
