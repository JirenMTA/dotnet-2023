using AutoMapper;
using FluentValidation;
using LibrarySchool.Domain;
using LibrarySchool.Server.Dto.Validator;
using LibrarySchool.Server.Middlewares;
using LibrarySchoolServer;
using LibrarySchoolServer.Dto;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

});

builder.Services.AddScoped<IValidator<StudentPostDto>, StudentPostDtoValidator>();
builder.Services.AddScoped<IValidator<ClassTypePostDto>, ClassTypePostDtoValidator>();
builder.Services.AddScoped<IValidator<SubjectPostDto>, SubjectPostDtoValidator>();
builder.Services.AddScoped<IValidator<MarkPostDto>, MarkPostDtoValidator>();


builder.Services.AddDbContextFactory<LibrarySchoolContext>(optionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString(nameof(LibrarySchool));
    optionsBuilder.UseMySQL(connectionString!);
});

var mapperConfig = new MapperConfiguration(config => config.AddProfile(new MappingProfile()));
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddTransient<GlobalExeptionHandlingMiddleware>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExeptionHandlingMiddleware>();

app.MapControllers();

app.Run();
