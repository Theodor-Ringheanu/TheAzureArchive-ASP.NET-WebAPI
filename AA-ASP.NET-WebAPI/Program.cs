using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using TheAzureArchiveAPI.DataContext;
using TheAzureArchiveAPI.Repositories;
using TheAzureArchiveAPI.Services;
using System.Globalization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
            options.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString("2022-01-01")
            })
);

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

        builder.Services.AddTransient<IEmailsSubscribedRepository, EmailsSubscribedRepository>();
        builder.Services.AddTransient<IEmailsSubscribedService, EmailsSubscribedService>();

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

    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private const string Format = "yyyy-MM-dd";

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }
    }
}