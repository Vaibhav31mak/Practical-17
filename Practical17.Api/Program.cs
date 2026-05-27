var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    const string schemeId = "bearer";

    // 1. Define the Security Scheme (No 'Reference' property used here)
    options.AddSecurityDefinition(schemeId, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = schemeId,
        BearerFormat = "JWT"
    });

    // 2. Add the Security Requirement using the new delegate pattern
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            // Pass the document context to correctly resolve the reference
            new OpenApiSecuritySchemeReference(schemeId, document),
            [] // Required scopes (empty array for standard JWT)
        }
    });
}); 
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

await IdentitySeeder.SeedAsync(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
