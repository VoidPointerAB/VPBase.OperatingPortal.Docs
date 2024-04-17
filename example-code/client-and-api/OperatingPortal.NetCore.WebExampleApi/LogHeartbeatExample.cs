using VPBase.Auth.Client.Models.Logging;
using VPBase.Client.Code.Log4Net;
using VPBase.Client.Code.Shared.AuthContract;

namespace OperatingPortal.NetCore.WebExampleApi
{
    public class LogHeartbeatExample
    {
        public static void Execute(ILogger logger)
        {
            // Heartbeat example
            HeartbeatClient.DefaultClient.Heartbeat("Startup Heartbeat Thread!");

            var obj = new LogStartupExample() { Name = "Base Test Class Json Example" };
            var authJsonConverter = new AuthContractJsonConverter();
            var jsonData = authJsonConverter.SerializeObject(obj);

            // Scope logging example
            var dictionary = new Dictionary<string, object>()
            {
                { LoggingKeyDefinitions.UserId, "baseUserId" },
                { LoggingKeyDefinitions.UserName, "baseUserName" },

                { LoggingKeyDefinitions.EntityId, "99999" },
                { LoggingKeyDefinitions.EntityType, (int)LoggingMessageEntityType.Customer },
                { LoggingKeyDefinitions.EntityTypeName, LoggingMessageEntityType.Customer.ToString() },
                { LoggingKeyDefinitions.EntityValueName, "Base Customer AB" },

                { LoggingKeyDefinitions.EntityId2, "88888" },
                { LoggingKeyDefinitions.EntityType2, (int)LoggingMessageEntityType.Vendor },
                { LoggingKeyDefinitions.EntityTypeName2, LoggingMessageEntityType.Vendor.ToString() },
                { LoggingKeyDefinitions.EntityValueName2, "Base Vendor AB" },

                { LoggingKeyDefinitions.AbsoluteUri, "https://voidpointer.se/admin" },
                { LoggingKeyDefinitions.RawUrl, "admin" },

                { LoggingKeyDefinitions.AdditionalInformation, "base additional info" },

                { "CustomStringKey", "baseValue" },
                { "CustomIntKey", 33 },
                { "CustomDateTimeKey", new DateTime(2023, 4, 28) },
                { "CustomBoolKey", true },
                { "CustomDecimalKey", 2.3m },
                { "CustomJsonKey", jsonData }
            };

            using (logger.BeginScope(dictionary))
            {
                logger.LogInformation("Test logging from base server startup!");
            }
        }
    }

    public class LogStartupExample
    {
        public string Name { get; set; }
    }
}
