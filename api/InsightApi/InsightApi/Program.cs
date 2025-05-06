using InsightApi.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend",
		policy => policy.WithOrigins("http://localhost:3000", "http://localhost:5173")
						.AllowAnyMethod()
						.AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Bind API settings from appsettings.json.
builder.Services.Configure<InsightApiOptions>(builder.Configuration.GetSection(key: "InsightApiOptions"));

// Register HttpClient for WebjetMoviesApi.
builder.Services.AddHttpClient<InsightAccountsApi>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
