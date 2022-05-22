var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Recebendo informação da URL
app.MapGet("/", () => "Hello World!");
app.MapGet("/{nome}", (string nome) =>
 {
     return $"João Gabriel {nome} ";
 });

// Recebendo essa informação do corpo da requisição
app.MapPost("/", (Mariana mariana) =>
{
    return Results.Ok(mariana);
}
);
app.MapPost("/gabriel", (Loja loja) =>
{
    return Results.Ok(loja);
}
);


app.Run();
public class Mariana
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Idade { get; set; }
};
public class Loja
{
    public int numberFuncionarios { get; set; }
    public int QuantidadeDeRoupas { get; set; }
    public string nomeDaLoja { get; set; }
}