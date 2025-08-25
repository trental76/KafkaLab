using Microsoft.AspNetCore.Mvc;
using KafkaLab.Publisher.BL.Publisher;

namespace KafkaLab.Publisher.Infrastructure.Controllers;

[ApiController]
[Route("publisher")]
public class PublishController : ControllerBase
{
    private readonly IPublisherService _publisherService;

    public PublishController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    [HttpPut]
    public async Task<IActionResult> PublishEvent(string eventName)
    { 
        await _publisherService.PublishEvent(eventName);

        return Ok(new { Message = $"Event {eventName} published successfully." });
    }
}
