using Microsoft.AspNetCore.Mvc;
using OperatingPortal.NetCore.WebExampleApi;
using VPBase.Auth.Client.Models.Logging;
using VPBase.Client.Code.Log4Net;

namespace VPBase.OperatingPortal.Server.Controllers
{
    [ApiController]
    [Route("monitoring")]
    [ApiExplorerSettings(GroupName = Definitions.ModuleGroupName)]
    public class LoggingApiController : ControllerBase
    {
        private static List<LogMessage> _loggMessages = new List<LogMessage>();
        private static List<HeartbeatMessage> _heartbeatMessages = new List<HeartbeatMessage>();

        ILogger<LoggingApiController> _logger;

        public LoggingApiController(ILogger<LoggingApiController> logger)
        {
            _logger = logger;
        }

        #region Operating Portal Api

        // The methods below are from the operating portal api (not the actual implementation, here we just save the info to a static list).

        [HttpPut, Route("LogMessage")]
        public ActionResult<LogMessage> Log([FromBody] LogMessage logMessage)
        {
            _loggMessages.Add(logMessage);

            return logMessage;
        }

        [HttpPut, Route("LogMessages")]
        public ActionResult<LogMessageList> Logs([FromBody] LogMessageList logMessageList)
        {
            var logMessages = logMessageList.LogMessages;

            _loggMessages.AddRange(logMessages);

            return logMessageList;
        }

        [HttpPut, Route("HeartbeatMessage")]
        public ActionResult<HeartbeatMessage> LogHeartbeat([FromBody] HeartbeatMessage heartbeatMessage)
        {
            _heartbeatMessages.Add(heartbeatMessage);

            return heartbeatMessage;
        }

        #endregion

        #region Testing

        [HttpGet, Route("TestEcho")]
        public string TestEcho()
        {
            return "Woho! Works: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        [HttpPut, Route("TestGenerateLogInfoMessage")]
        public void TestGenerateLogInfoMessage(string message)
        {
            _logger.LogInformation(message);
        }

        [HttpPut, Route("TestGenerateHeartbeat")]
        public void TestGenerateHeartbeat(string message)
        {
            HeartbeatClient.DefaultClient.Heartbeat(message);
        }

        [HttpGet, Route("TestGetLogMessages")]
        public List<LogMessage> TestGetLogMessages()
        {
            return _loggMessages;
        }

        [HttpGet, Route("TestGetHeartbeats")]
        public List<HeartbeatMessage> TestGetHeartbeats()
        {
            return _heartbeatMessages;
        }

        #endregion
    }
}
