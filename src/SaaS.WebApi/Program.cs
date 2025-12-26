using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SaaS.Application.Common.Behaviors;
using SaaS.Application.Interfaces;
using SaaS.Infrastructure.Persistence;
using SaaS.Infrastructure.Persistence.Repositories;
using SaaS.WebApi.Filters;
using SaaS.WebApi.Middleware;
using SaaS.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SaaS.Application.Interfaces.IGenericRepository<>).Assembly));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddValidatorsFromAssembly(typeof(SaaS.Application.Interfaces.IGenericRepository<>).Assembly);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantService, TenantService>();

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<TenantHeaderFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
