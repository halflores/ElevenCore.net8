using B1SLayer;
using Eleven.Data.Entidad;
using Eleven.Data.Repositorio.Implementacion;
using Eleven.Data.Repositorio.Interfaz;
using Eleven.Service.Repositorio.Implementacion;
using Eleven.Service.Repositorio.Interfaz;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SAPBo.Data.Repositorio.Implementacion;
using SAPBo.Data.Repositorio.Interfaz;
using Serilog;
using System.Text;
using AutoMapper.Extensions.ExpressionMapping;

var builder = WebApplication.CreateBuilder(args);

// LOGGER CONFIGURATION
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
//builder.Host.UseSerilog();
//*********************
logger.Information("iniciando ....");
// Add services to the container.
builder.Services.AddControllers();

// Adding Jwt Bearer
//Jwt configuration starts here
var jwtAudience = builder.Configuration.GetSection("Jwt:ValidAudience").Get<string>();
var jwtIssuer = builder.Configuration.GetSection("Jwt:ValidIssuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Secret").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtAudience,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });
//Jwt configuration ends here

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(serviceLayer => new Dictionary<string, SLConnection>() {
    {"T001", new SLConnection(
        builder.Configuration.GetSection("ServiceLayerSBOT001:Url").Value,
        builder.Configuration.GetSection("ServiceLayerSBOT001:Catalog").Value,
        builder.Configuration.GetSection("ServiceLayerSBOT001:User").Value,
        builder.Configuration.GetSection("ServiceLayerSBOT001:Password").Value)
    },
    {"T002", new SLConnection(
        builder.Configuration.GetSection("ServiceLayerSBOT002:Url").Value,
        builder.Configuration.GetSection("ServiceLayerSBOT002:Catalog").Value,
        builder.Configuration.GetSection("ServiceLayerSBOT002:User").Value,
        builder.Configuration.GetSection("ServiceLayerSBOT002:Password").Value)
    }
});

//-----SAP INJECTION
builder.Services.AddTransient<IBoItemRepository, BoItemRepository>();
//builder.Services.AddTransient<IBoCategoriaRepository, BoCategoriaRepository>();
builder.Services.AddTransient<IBoDocumentRepository, BoDocumentRepository>();
builder.Services.AddTransient<IBoEmployeeRepository, BoEmployeeRepository>();
builder.Services.AddTransient<IBoBusinessPartnersRepository, BoBusinessPartnersRepository>();
builder.Services.AddTransient<IBoPaymentRepository, BoPaymentRepository>();


//-----SAP - ACE HARDWARE INJECTION
builder.Services.AddTransient<IItemService, ItemService>();
//builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IDocumentService, DocumentService>();
builder.Services.AddTransient<IBusinessPartnersService, BusinessPartnersService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();

//-----ONLY ACE HARDWARE DATABASE SQL SERVER
builder.Services.AddSqlServer<RepositoryPatternContext>(builder.Configuration.GetSection("ElevenCoreDB:ConnectionString").Value);

//-----ONLY ACE HARDWARE INJECTION REPOSITORY
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository >();
builder.Services.AddTransient<IAuthenticateRepository, AuthenticateRepository>();
builder.Services.AddTransient<ITipoDocumentoRepository, TipoDocumentoRepository>();


//-----ONLY ACE HARDWARE INJECTION SERVICE
builder.Services.AddTransient(typeof(IServiceAsync<,>), typeof(ServiceAsync<,>));
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IAuthenticateService, AuthenticateService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<ITipoDocumentoService, TipoDocumentoService>();


builder.Services.AddAutoMapper(cfg => { cfg.AddExpressionMapping(); },AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddControllersWithViews(options =>
//{
//    options.Filters.Add(new Microsoft.AspNetCore.Mvc.ValidateAntiForgeryTokenAttribute());
//});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => 
{
    options.Cookie.IsEssential = true;
    //options.IdleTimeout = TimeSpan.FromMilliseconds(10);
});
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSession();
app.UseCors(options => options.WithOrigins("*")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true));
// Configure the HTTP request pipeline.
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}

app.UseSwagger();
app.UseSwaggerUI();

AppDomain.CurrentDomain.SetData("ContentRootPath", app.Environment.ContentRootPath);
AppDomain.CurrentDomain.SetData("WebRootPath", app.Environment.WebRootPath);
AppDomain.CurrentDomain.SetData("SBOCatalog", builder.Configuration.GetSection("ConexionSBO:Catalog").Value);
AppDomain.CurrentDomain.SetData("SBOConnectionString", builder.Configuration.GetSection("ConexionSBO:ConnectionString").Value);

////app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();
//app.MapControllers();
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.MapRazorPages();
app.MapControllers();

app.Run();
