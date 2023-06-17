using WebAPI.Repository.Data;
using WebAPI.Repository.Implementation;
using WebAPI.Repository.Interface;
using WebAPI.Service.Implementation;
using WebAPI.Service.Interface;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ApiDbContext>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMasterDataService, MasterDataService>();
builder.Services.AddScoped<IMasterDataRepository, MasterDataRepository>();



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();
