using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace KafkaLab.Publisher.BL.Publisher;

public class PublisherService : IPublisherService
{
    private const string TopicName = "my-topic";
    private const string KafkaHost = "localhost:9092";

    public PublisherService()
    {
        CreateTopic();
    }

    public async Task PublishEvent(string eventName)
    {
        var config = new ProducerConfig { BootstrapServers = KafkaHost };

        using var producer = new ProducerBuilder<Guid, string>(config).Build();

        var message = new Message<Guid, string> { Key = Guid.NewGuid(), Value = eventName };
        var deliveryResult = await producer.ProduceAsync(TopicName, message);

        Console.WriteLine($"Message delivered to {deliveryResult.TopicPartitionOffset}");
    }

    public void CreateTopic()
    {
        var config = new AdminClientConfig { BootstrapServers = KafkaHost };
        using var adminClient = new AdminClientBuilder(config).Build();
        var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(10));

        if (!metadata.Topics.Any(x => x.Topic == TopicName))
        {
            var topic = new TopicSpecification
            {
                Name = TopicName,
                NumPartitions = 1,
                ReplicationFactor = 1
            };

            var result = async () => await adminClient.CreateTopicsAsync([topic]);

            Console.WriteLine($"Topic {TopicName} created...");
        }
    }
}
