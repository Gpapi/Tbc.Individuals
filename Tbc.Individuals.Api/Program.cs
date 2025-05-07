using Tbc.Individuals.Api.Exceptions;
using Tbc.Individuals.Api.Localization;
using Tbc.Individuals.Application.Extensions;
using Tbc.Individuals.FileStorage.Local;
using Tbc.Individuals.Persistance.Sql.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddGlobalExceptionHandler();

builder.Services.AddFileStorage(options =>
{
    builder.Configuration.Bind(FileStorageOptions.Name, options);
    options.Folder = Path.Combine(builder.Environment.WebRootPath, options.Folder);
});

builder.Services.AddCustomLocalization(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.UseCustomLocalization();

app.UseAuthorization();

app.MapControllers();

app.Run();
