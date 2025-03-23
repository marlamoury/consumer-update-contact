using Consumer.Update.Contact.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace Consumer.Update.Contact.Infrastructure.Persistence
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly string _connectionString;

        public ContatoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AtualizarContatoAsync(Contato contato)
        {
            // Garante que a data de atualização seja a data atual
            contato.DataHoraRegistro = DateTime.UtcNow;

            const string query = @"
                UPDATE contatos 
                SET nome = @Nome, telefone = @Telefone, email = @Email, ddd = @Ddd, regiao = @Regiao, DataHoraRegistro = @DataHoraRegistro
                WHERE id = @Id;";

            using var connection = new MySqlConnection(_connectionString);

            try
            {
                var parameters = new
                {
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    Email = contato.Email,
                    Ddd = contato.Ddd,
                    Regiao = contato.Regiao,
                    DataHoraRegistro = contato.DataHoraRegistro,
                    Id = contato.Id
                };

                var rowsAffected = await connection.ExecuteAsync(query, parameters);

                if (rowsAffected > 0)
                {
                    Console.WriteLine($"Contato atualizado com sucesso! ID: {contato.Id}");
                }
                else
                {
                    Console.WriteLine($"Nenhum contato encontrado para atualização com o ID: {contato.Id}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar o contato com ID: {contato.Id}. Erro: {ex.Message}");
            }
        }
    }
}
