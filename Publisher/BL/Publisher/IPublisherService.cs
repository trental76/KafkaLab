namespace KafkaLab.Publisher.BL.Publisher;

public interface IPublisherService
{
    Task PublishEvent(string eventName);
}
