using Ostral.API.Extensions;
using Ostral.API.Middlewares;
using Ostral.Core.Utilities;
using Ostral.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(op => op.SerializerSettings.ReferenceLoopHandling
        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAppSettingsConfig(builder.Configuration, builder.Environment);
builder.Services.AddSwaggerExtension();
builder.Services.AddAuthenticationExtension(builder.Configuration, builder.Environment);
builder.Services.AddAuthorizationExtension();
builder.Services.AddDbContextExtension(builder.Environment, builder.Configuration);
builder.Services.AddServicesExtension(builder.Configuration);
builder.Services.AddAutoMapper(typeof(OstralProfile));
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(setupAction => { setupAction.SwaggerEndpoint("/swagger/OstralOpenAPI/swagger.json", "Ostral API"); });
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());


// await OstralDBInitializer.Seed(app);

// await Ostral.Infrastructure.OstralDBInitializer.Seed(app);

app.Run();