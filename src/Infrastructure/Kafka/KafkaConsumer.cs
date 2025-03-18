using Confluent.Kafka;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Kafka
{
    public class KafkaConsumer
    {
        private readonly IConsumer<Null, string> _consumer;
        private readonly ILogger<KafkaConsumer> _logger;

        public KafkaConsumer(string bootstrapServers, string groupId, ILogger<KafkaConsumer> logger)
        {
            _logger = logger;
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                // Uncomment and configure the lines below if your broker uses SSL
                // SecurityProtocol = SecurityProtocol.Ssl,
                // SslCaLocation = "path/to/ca-cert",
                // SslCertificateLocation = "path/to/client-cert",
                // SslKeyLocation = "path/to/client-key",
                // SslKeyPassword = "your-password"
            };
            _consumer = new ConsumerBuilder<Null, string>(config)
                .SetLogHandler((_, logMessage) => _logger.LogInformation($"Kafka log: {logMessage.Message}"))
                .SetErrorHandler((_, error) => _logger.LogError($"Kafka error: {error.Reason}"))
                .Build();
        }

        public void Consume(string topic, CancellationToken cancellationToken)
        {
            _consumer.Subscribe(topic);
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumeResult = _consumer.Consume(cancellationToken);
                    Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
                }
            }
            catch (OperationCanceledException)
            {
                _consumer.Close();
            }
        }
    }
}
