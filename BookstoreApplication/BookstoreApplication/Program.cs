using BookstoreApplication.Models;
using BookstoreApplication.Repositories;
using BookstoreApplication.Services;
using BookstoreApplication.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAwardService, AwardService>();
builder.Services.AddScoped<IAuthorRepository, AuthorsRepository>();
builder.Services.AddScoped<IPublisherRepository, PublishersRepository>();
builder.Services.AddScoped<IBookRepository, BooksRepository>();
builder.Services.AddScoped<IAwardRepository, AwardsRepository>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();












