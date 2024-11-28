using Microsoft.AspNetCore.Mvc;
using TestAssignmentApi.Extensions;
using TestAssignmentApi.Filters;

var builder = WebApplication.CreateBuilder(args);

//TODO: Add unit tests, creat postman collection


// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, JasonPatchInputFormatter.GetJsonPatchInputFormatter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwaggerUI();
builder.Services.RegisterDbContext(configuration: builder.Configuration);
builder.Services.RegisterApplicationServices();
builder.Services.Configure<ApiBehaviorOptions>(ApiBehaviorOptions =>
{
    ApiBehaviorOptions.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(opt => { });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

