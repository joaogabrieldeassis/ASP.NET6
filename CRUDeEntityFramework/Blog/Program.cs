using Blog.DataContext;
var builder = WebApplication.CreateBuilder(args);
//Desabilitar a validação automatica da minha api
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});   ;
builder.Services.AddDbContext<BlogDataContext>();
var app = builder.Build();

app.MapControllers();
app.Run();
