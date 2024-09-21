
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

using Microsoft.IdentityModel.Tokens;

using System.Text;
using Microsoft.OpenApi.Models;
using ArchiveManagement.BLL.Interfaces;
using ArchiveManagement.BLL.Implementations;
using ArchiveManagement.DAL.Context;
using Microsoft.EntityFrameworkCore;
using ArchiveManagement.DAL.Interfaces;
using ArchiveManagement.DAL.Implementations;
using ArchiveManagement.DAL.Interfaces.BusinessDocuments;
using ArchiveManagement.DAL.Implementations.BusinessDocuments;
using ArchiveManagement.BLL.Implementations.BusinessDocuments;
using ArchiveManagement.BLL.Interfaces.BusinessDocuments;


//using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Ajouter la politique CORS pour permettre les requêtes depuis 'http://localhost:3000'
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000",
        builder => builder
            .WithOrigins("http://localhost:3000") // Autoriser seulement localhost:3000
            .AllowAnyMethod() // Autoriser toutes les méthodes HTTP (GET, POST, etc.)
            .AllowAnyHeader() // Autoriser tous les headers
            .AllowCredentials()); // Permettre l'envoi des credentials si nécessaire (facultatif)
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//configur swagger with authentificat
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
//FIN config Ath swagger


//https://www.youtube.com/watch?v=SdtOGowW-Dk
var connetionString = builder.Configuration.GetConnectionString("WebApiDatabase");
//builder.Services.AddDbContext<ArchivesDbContext>(options => options.UseMySQL(connetionString, null));
//FOR ENTITY FRAMEWORK DBCONTEXT
builder.Services.AddDbContext<ArchivesDbContext>(options =>options.UseMySQL(connetionString, null));
//FOR IDENTITY
//TO CHANGE DEFAULT REQUIRED VALUES OF IDENTITY
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 4;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;

}
).AddEntityFrameworkStores<ArchivesDbContext>()
.AddDefaultTokenProviders()
.AddSignInManager<SignInManager<IdentityUser>>()
.AddUserManager<UserManager<IdentityUser>>()
;
//ADDING AUTHENTIFICATION
builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;   
}).AddJwtBearer(options=>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,   
        ValidateIssuerSigningKey = true,    
        ValidIssuer=builder.Configuration.GetSection("jwt:Issuer").Value,
        ValidAudience= builder.Configuration.GetSection("jwt:Audience").Value,
        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwt:key").Value))
    };

});
builder.Services.AddTransient<IAuthServices,AuthServices>();
builder.Services.AddTransient<IFolderServices, FolderServices>();
builder.Services.AddTransient<IFileservices, Fileservices>();
builder.Services.AddTransient<IFolderDal, FolderDal>();
builder.Services.AddTransient<ITypeBusinessDocumentDAL, TypeBusinessDocumentDAL>();
builder.Services.AddTransient<ITypeBusinessDocumentservices, TypeBusinessDocumentservices>();
builder.Services.AddTransient<IDocumentsBusinessDAL, DocumentsBusinessDAL>(); 
builder.Services.AddTransient<IDocumentsBusinessServices, DocumentsBusinessServices>();  
  //builder.Services.AddSingleton
  //repositorie



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
// Utiliser la politique CORS
app.UseCors("AllowLocalhost3000");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
