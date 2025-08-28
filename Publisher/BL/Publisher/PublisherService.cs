using Confluent.Kafka;

namespace KafkaLab.Publisher.BL.Publisher;

public class PublisherService : IPublisherService
{
    private const string TopicName = "my-topic";
    private const string KafkaHost = "localhost:9092";
    private const string routingKey = "my-routingKey";

    IKafkaProvider _kafkaProvider;

    public PublisherService(IKafkaProvider kafkaProvider)
    {
        _kafkaProvider = kafkaProvider;
        _kafkaProvider.CreateTopic(TopicName).GetAwaiter().GetResult();
    }

    public async Task PublishEvent(string eventName)
    {
        var config = new ProducerConfig { BootstrapServers = KafkaHost };

        using var producer = new ProducerBuilder<string, string>(config).Build();

        var message = new Message<string, string> { Key = routingKey, Value = eventName };
        var deliveryResult = await producer.ProduceAsync(TopicName, message);

        Console.WriteLine($"Message delivered to {deliveryResult.TopicPartitionOffset}");
    }
}
