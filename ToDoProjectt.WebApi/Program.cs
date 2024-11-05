using Core.Tokens.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoProject.Model.Users.Entity;
using ToDoProject.Repository.Contexts;
using ToDoProject.Repository.Extensions;
using ToDoProject.Service.Extensions;
using ToDoProject.Service.Filters;
using ToDoProject.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add custom repository and service configurations
builder.Services.AddRepositories(builder.Configuration)
                .AddServices(builder.Configuration);

// Configure custom token options
builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOption"));

// Configure Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<BaseDbContext>();

// Add custom exception handlers
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure global exception handling middleware
app.UseExceptionHandler(x => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
