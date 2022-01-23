using MediatR;
using Microsoft.EntityFrameworkCore;
using Tributacao_Singular.Infra.Contexto;
using Tributacao_Singular.Servico.Configuracoes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddAutoMapperSetup();
builder.Services.AddSwaggerSetup();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddCors();

builder.Services.ResolveDependencies();

builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<MeuDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseSwaggerSetup();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(options =>
             options
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader()
             );

app.UseHttpsRedirection();
app.UseAuthConfiguration();
app.UseAuthorization();
app.MapControllers();

app.Run();