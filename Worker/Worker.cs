using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Consumer.Update.Contact.Infrastructure.Messaging;

namespace Consumer.Update.Contact.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMQConsumer _consumer;

        public Worker(ILogger<Worker> logger, RabbitMQConsumer consumer)
        {
            _logger = logger;
            _consumer = consumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker iniciado...");
            return _consumer.StartAsync(stoppingToken); // Chama o StartAsync do RabbitMQConsumer
        }
    }
}
