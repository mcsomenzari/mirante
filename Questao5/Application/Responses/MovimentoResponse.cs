namespace Questao5.Aplication.Response
{
    public class MovimentoResponse
    {
        public MovimentoResponse(string idMovimento)
        {
            IdMovimento = idMovimento;
        }

        public string IdMovimento { get; set; }
    }
}
