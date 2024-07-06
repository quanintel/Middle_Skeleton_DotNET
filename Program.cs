using Skeleton_DotNET.Helpers;
using Skeleton_DotNET.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.AddCustomSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddCustomMemoryCache();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddDbContext(builder.Configuration);

builder.Services.RegisterRepository();
builder.Services.RegisterService();

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherForecast v1"); });
    app.MapControllers();
}
else
{
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers().RequireAuthorization();
}

app.Run();