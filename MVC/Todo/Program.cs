using Todo.Data;

var builder = WebApplication.CreateBuilder(args);
//Adicionando os controles
builder.Services.AddControllers();
//Adicionando a conexão com o banco
builder.Services.AddDbContext<AppDbContext>();
var app = builder.Build();
app.MapControllers();
app.Run();
