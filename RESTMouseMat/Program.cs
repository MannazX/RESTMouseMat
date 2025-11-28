using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using MouseMatLibrary;
using RESTMouseMat;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
const bool useDB = true;
IMouseMatRepository _repo;
if (useDB)
{
	var optionsBuilder = new DbContextOptionsBuilder<MannazRestAppDbContext>();
	optionsBuilder.UseSqlServer(SecretDB.ConnectionStringSimply);
	MannazRestAppDbContext _dbContext = new(optionsBuilder.Options);
	_dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Pencils");
	_repo = new MouseMatRepositoryDB(_dbContext);
}
else
{
	_repo = new MouseMatRepository();
}

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
