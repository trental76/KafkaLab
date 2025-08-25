using KafkaLab.Consumer;

internal partial class Program
{
    private static void Main()
    {
        var consumer = new Consumer();
        consumer.Start();
    }
}
