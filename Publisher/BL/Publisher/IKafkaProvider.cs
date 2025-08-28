namespace KafkaLab.Publisher.BL.Publisher;

public interface IKafkaProvider
{
    Task CreateTopic(string topicName);
}
