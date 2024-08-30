using BidCalculationTool.Core.Calculation;
using BidCalculationTool.Core.Domain.Interfaces;
using BidCalculationTool.Core.Factories;
using BidCalculationTool.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IBidCalculationService, BidCalculationService>();
builder.Services.AddScoped<FeeStrategyFactory>();
builder.Services.AddScoped<IBidCalculationService>(provider =>
{
    var feeStrategyFactory = provider.GetRequiredService<FeeStrategyFactory>();
    return new BidCalculationService(feeStrategyFactory);
});
builder.Services.AddScoped<IBidCalculationAppService, BidCalculationAppService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
