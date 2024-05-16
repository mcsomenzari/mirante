using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using Questao5.Models;

namespace Questao5.Repository
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly DatabaseConfig dbConfig;
        readonly SqliteConnection connection;

        public MovimentoRepository(DatabaseConfig dbConfig)
        {
            this.dbConfig = dbConfig;
            connection = new SqliteConnection(dbConfig.Name);
            connection.Open();
        }

        public async Task<string> CreateMovimento(string idContaCorrente, string tipoMovimento, decimal valor)
        {
            try
            {
                var insertSql = @"INSERT INTO movimento
                                (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) 
                                VALUES (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor);";

                Movimento movimento = new Movimento(idContaCorrente, tipoMovimento, valor);

                await connection.ExecuteAsync(insertSql, movimento);

                return movimento.IdMovimento;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);

            }
            finally
            {
                connection.Close();
            }

        }
    }
}
