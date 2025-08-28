
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace KafkaLab.Publisher.BL.Publisher;

public class KafkaProvider : IKafkaProvider
{
    private const string KafkaHost = "localhost:9092";

    public async Task CreateTopic(string topicName)
    {
        var config = new AdminClientConfig { BootstrapServers = KafkaHost };
        using var adminClient = new AdminClientBuilder(config).Build();
        var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));

        if (!metadata.Topics.Any(x => x.Topic == topicName))
        {
            var topic = new TopicSpecification
            {
                Name = topicName,
                NumPartitions = 1,
                ReplicationFactor = 1
            };

            await adminClient.CreateTopicsAsync([topic]);

            Console.WriteLine($"Topic {topicName} created...");
        }
    }
}
