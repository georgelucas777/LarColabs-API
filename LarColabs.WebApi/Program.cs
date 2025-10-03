using LarColabs.WebApi.Database;
using Microsoft.EntityFrameworkCore;
using LarColabs.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using LarColabs.WebApi.Enums;
using Microsoft.OpenApi.Any;
using System.Text.Json.Serialization;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSwaggerGen(c =>
{


    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LarColabs.WebApi", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT (apenas o token)."
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
            Array.Empty<string>()
        }
    });
    
    c.MapType<TipoTelefone>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = new List<IOpenApiAny>
        {
            new OpenApiString("movel"),
            new OpenApiString("fixo")
        }
    });

    c.MapType<PatrimonioTelefone>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = new List<IOpenApiAny>
        {
            new OpenApiString("pessoal"),
            new OpenApiString("corporativo")
        }
    });

    c.MapType<StatusTelefone>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = new List<IOpenApiAny>
        {
            new OpenApiString("ativo"),
            new OpenApiString("desativado"),
            new OpenApiString("manutencao")
        }
    });
});

builder.Services.AddDbContext<LarColabsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ColaboradorService>();
builder.Services.AddScoped<TelefoneService>();
builder.Services.AddScoped<ColaboradorTelefoneService>();

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"error\": \"Token expirado\"}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"error\": \"Token inválido\"}");
            }
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            if (string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"error\": \"Token não informado\"}");
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:5173") // porta do Vite
            .AllowAnyHeader()
            .AllowAnyMethod());
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LarColabs.WebApi v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
