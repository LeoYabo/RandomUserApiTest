using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//base URL-api address
builder.Services.AddHttpClient("randomUser", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://randomuser.me/api/");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
