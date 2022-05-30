namespace Blog.ViewModel
{
    public class ResultViewModel<T>
    {
       

        //Método para receber a data, e uma lista de erros
        public ResultViewModel(T data, List<string> erros)
        {
            Data = data;
            Erros = erros;
        }
        //Método para receber os dados se derem certo
        public ResultViewModel(T data)
        {
            Data=data;
        }
        //Metodo para retornar so os erros
        public  ResultViewModel(List<string> erros)
        {
            Erros = erros;  
        }

        //Método quando eu só tiver um erro
        public ResultViewModel(string erro)
        {
            Erros.Add(erro);
        }
             
        //Não quero que ninguem passe nenhum valor para essa propriedade por isso coloquei o set
        public T Data { get; private set; }
        public List<string> Erros { get; private set; } = new List<string>();

    }
}
