using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Infrastructure.Sqlite;
using Questao5.Models;

namespace Questao5.Repository
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly DatabaseConfig dbConfig;
        readonly SqliteConnection connection;

        public ContaCorrenteRepository()
        {
        }

        public ContaCorrenteRepository(DatabaseConfig dbConfig)
        {
            this.dbConfig = dbConfig;
            connection = new SqliteConnection(dbConfig.Name);
            connection.Open();
        }


        public async Task<ContaCorrente> GetById(string idContaCorrente)
        {
            try
            {
                string query = "SELECT * FROM contacorrente WHERE IdContaCorrente = @IdContaCorrente";

                return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(query, new { IdContaCorrente = idContaCorrente });
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

        public async Task<ContaCorrente> GetContaCorrenteAtiva(string idContaCorrente)
        {
            try
            {
                string query = "SELECT * FROM contacorrente WHERE IdContaCorrente = @IdContaCorrente and ativo = 1";

                return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(query, new { IdContaCorrente = idContaCorrente });
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

        public async Task<SaldoContaCorrente> GetSaldo(string idContaCorrente)
        {
            try
            {
                string query = @"SELECT
                                    CC.numero,
                                    CC.nome,
                                    (SUM(CASE WHEN M.tipomovimento = 'C' THEN M.valor ELSE 0 END)
	                                - SUM(CASE WHEN M.tipomovimento = 'D' THEN M.valor ELSE 0 END)) AS saldo
                                    FROM contacorrente CC
                                    LEFT JOIN movimento M ON CC.idcontacorrente = M.idcontacorrente
                                    WHERE CC.idcontacorrente = @IdContaCorrente
                                    GROUP BY CC.numero, CC.nome";

                return await connection.QueryFirstOrDefaultAsync<SaldoContaCorrente>(query, new { IdContaCorrente = idContaCorrente });

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
