using Teachable_Machine_Model_Handler_with_Redis.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("corspolicy", build =>
    {
        build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddTransient<RedisListener>(options =>
{
    return new RedisListener("127.0.0.1");
});

builder.Services.AddTransient<ResizeImageByte>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://localhost:24112");
