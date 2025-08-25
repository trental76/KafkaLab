using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace KafkaLab.Consumer;

internal class Consumer
{
    private const string TopicName = "my-topic";
    private const string KafkaHost = "localhost:9092";
    private readonly ConsumerConfig _consumerConfig;

    public Consumer()
    {
        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = KafkaHost,
            GroupId = "my-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

    }

    public void Start()
    {
        using var consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig).Build();
        consumer.Subscribe(TopicName);

        while (true)
        {
            var cr = consumer.Consume();
            Console.WriteLine($"Received: {cr.Message.Value}");
        }
    }
}
