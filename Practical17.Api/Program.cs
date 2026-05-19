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
}); builder.Services.AddInfrastructureServices(builder.Configuration);
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


/*
Student Model
User Model wit FirstName, LastName, Email, Password, MobileNumber for Authentication
Admin and Normal User Role for Authorization
Use Identity Library and JWT for Authentication and Authorization
EF Core for Database Operations
Repo pattern for CRUD with Database
*/

/*
Student Model
User Model wit FirstName, LastName, Email, Password, MobileNumber for Authentication
Admin and Normal User Role for Authorization
EF Core for Database Operations
Repo pattern for CRUD with Database and unit of work pattern with lazy loading.
need to do this with identity library and jwt token.
proper architecture with domain, infrastructure, application and api layer assemblies.
auditing with isp interfaces of ientity, icreatable, iupdatable, isoftdeletable, and iconcurrency with the audit done by a user model which is identity library one extended, also the authorization to be done.
add global exception and result pattern and use it too.
use ef core.
validations not using data annotations and not model builder. use separate classes to do for each entity for srp.
use services for each entity for dto records resolve with automapper and other validations with global exc and result pattern.
use inbuilt logger too
override save changes with following srp in different class to do auditing of all audits 
use proper endpoint folder in api layer and then do endpoints following srp. 
use reflecation of di in separate class to follow srp and try to explore al di life cycles where required.
try to follow all solid and oops everywhere required
*/