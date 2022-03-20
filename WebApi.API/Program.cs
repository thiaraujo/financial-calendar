using Infra.IoC;
using Microsoft.OpenApi.Models;
using WebApi.API.Filters;
using WebApi.API.InitSettings;

var builder = WebApplication.CreateBuilder(args);

// Localization
builder.Services.AddLocalization();

builder.Services.AddControllers(options => options.Filters.Add<NotificationFilter>());
builder.Services.AddRouting(x => x.LowercaseUrls = true);

// Bearer
JwtSettings.JwtInit(builder.Services);

// Register Container
builder.Services.RegisterContainer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Financial Calendar", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme." +
                      "\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below." +
                      "\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1); //Removes Schema section
    });
}

app.UseHttpsRedirection();

// Globalization
var supportedCultures = new[] { "pt-BR", "en-US" };
var localiztionOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures);

app.UseRequestLocalization(localiztionOptions);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
