using Microsoft.AspNetCore.Mvc;

using MQTT_Broker_Backend.Services;

namespace MQTT_Broker_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MqttController : ControllerBase
    {
        private readonly MqttService _broker;

        public MqttController(MqttService broker)
        {
            _broker = broker;
        }

        [HttpPost("start")]
        public async Task<IActionResult> Start()
        {
            await _broker.StartAsync();
            return Ok(new { message = "MQTT broker started" });
        }
        [HttpPost("stop")]
        public async Task<IActionResult> Stop()
        {
           // await _broker.StopAsync();
            return Ok(new { message = "MQTT broker stopped" });
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(/*new { running = _broker.IsRunning }*/);
        }
        [HttpGet("Clients")]
        public async Task<IActionResult> GetClientsCount()
        {
            return Ok(new { count = _broker.ClientCount });
        }
    }
    
    
}
