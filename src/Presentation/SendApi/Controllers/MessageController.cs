using Domain.Entities; // Add this using directive
using Infrastructure.Kafka;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly KafkaProducer _kafkaProducer;

        public MessageController(KafkaProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] Message message) // Change parameter type
        {
            await _kafkaProducer.SendMessageAsync("test-topic", message.Content); // Send message content
            return Ok();
        }
    }
}
