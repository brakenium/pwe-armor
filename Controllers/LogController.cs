using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using watchtower.Models;
using watchtower.Services;

namespace watchtower.Controllers {

    [ApiController]
    [Route("logs/")]
    public class LogController : ControllerBase {

        private readonly IMatchMessageBroadcast _MatchLog;
        private readonly IAdminMessageBroadcast _AdminLog;

        public LogController(IMatchMessageBroadcast matchLog, IAdminMessageBroadcast adminLog) {
            _MatchLog = matchLog;
            _AdminLog = adminLog;
        }

        [HttpGet("match")]
        public FileResult Match() {
            List<Message> messages = _MatchLog.GetMessages();

            string file = $"Match logs - {DateTime.UtcNow}\n";

            foreach (Message msg in messages) {
                file += $"{msg.Timestamp:yyyy-mm-dd HH:mm:ss} {msg.Content}\n";
            }

            byte[] bytes = Encoding.UTF8.GetBytes(file);

            return File(bytes, "text/plain", $"PWE-BAA-MatchLogs-{DateTime.UtcNow:yyyy-mm-dd HH:mm:ss}.txt");
        }
        
        [HttpGet("admin")]
        public FileResult Admin() {
            List<Message> messages = _AdminLog.GetMessages();

            string file = $"Admin logs - {DateTime.UtcNow}\n";

            foreach (Message msg in messages) {
                file += $"{msg.Timestamp:yyyy-mm-dd HH:mm:ss} {msg.Content}\n";
            }

            byte[] bytes = Encoding.UTF8.GetBytes(file);

            return File(bytes, "text/plain", $"PWE-BAA-AdminLogs-{DateTime.UtcNow:yyyy-mm-dd HH:mm:ss}.txt");
        }


    }
}
