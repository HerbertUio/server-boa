using Application.Services;
using Application.UseCases.TicketCases;
using Application.Validators.TicketValidators;
using Domain.IRepositories;
using FluentValidation;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc;

public static class ApplicationDi
{
    public static IServiceCollection RegisterDatabase(this IServiceCollection collection, IConfiguration configuration)
    {
        string connectionString = configuration["ConnectionStrings:DefaultConnection"];
        collection.AddDbContext<ApplicationContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        return collection;
    }
    
    public static IServiceCollection RegisterServices(this IServiceCollection collection)
    {
        // TICKET SERVICES
        collection.AddScoped<TicketService>();
        collection.AddScoped<CreateTicketCase>();
        collection.AddScoped<UpdateTicketCase>();
        collection.AddScoped<DeleteTicketCase>();
        collection.AddScoped<GetAllTicketsCase>();
        collection.AddScoped<GetByIdTicketCase>();
        collection.AddScoped<ChangePriorityCase>();
        collection.AddScoped<ChangeStatusCase>();
        collection.AddScoped<MergeTicketsCase>();
        collection.AddScoped<UnmergeTicketsCase>();
        collection.AddScoped<AddCommentCase>();
        collection.AddScoped<AssignTicketCase>();
        collection.AddScoped<GetCommentsByTicketIdCase>();
        collection.AddScoped<GetChildTicketsCase>();
        return collection;
    }
    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        collection.AddScoped<ITicketRepository, TicketRepository>();
        return collection;
    }
    public static IServiceCollection RegisterValidators(this IServiceCollection collection)
    {
        // TICKET VALIDATORS
        collection.AddValidatorsFromAssemblyContaining<CreateTicketValidator>();
        collection.AddValidatorsFromAssemblyContaining<UpdateTicketValidator>();
        collection.AddValidatorsFromAssemblyContaining<ChangePriorityValidator>();
        collection.AddValidatorsFromAssemblyContaining<ChangeStatusValidator>();
        collection.AddValidatorsFromAssemblyContaining<MergeTicketsValidator>();
        collection.AddValidatorsFromAssemblyContaining<UnmergeTicketValidator>();
        return collection;
    }
}