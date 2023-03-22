using Microsoft.EntityFrameworkCore;
using TheAzureArchiveAPI.DataContext;
using TheAzureArchiveAPI.Repositories;
using TheAzureArchiveAPI.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options => 
        {
            options.AddPolicy("ReactDomain",
            policy => policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
             );
        });

        builder.Services.AddDbContext<TheAzureArchiveDataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        
        builder.Services.AddTransient<IStoriesRepository, StoriesRepository>();
        builder.Services.AddTransient<IStoriesService, StoriesService>();

        builder.Services.AddTransient<IArticlesRepository, ArticlesRepository>();
        builder.Services.AddTransient<IArticlesService, ArticlesService>();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors("ReactDomain");

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}