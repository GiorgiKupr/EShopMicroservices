var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
var random = new Random();

app.MapPost("/api/payments", () =>
{
    var outcome = random.Next(0, 2); // 0 or 1

    if (outcome == 0)
    {
        return Results.Ok(new { message = "Payment succeeded" });
    }
    else
    {
        return Results.Problem("Payment processing failed due to gateway error.", statusCode: 500);
    }
});

app.Run();
