using System.Collections.Generic;
using System.Runtime.Serialization;
using TDAmeritradeAPI.Utilities;
using Utf8Json;

namespace TDAmeritradeAPI.Models.Streaming
{
    public class ResponseData
    {
        [DataMember(Name = "response")]
        public List<Response> Response { get; set; }

        [DataMember(Name = "notify")]
        public List<Notify> Notify { get; set; }

        [DataMember(Name = "data")]
        public List<Data> Data { get; set; }

        [DataMember(Name = "snapshot")]
        public List<Snapshot> Snapshot { get; set; }
    }

    public class Response
    {
        [DataMember(Name = "service")]
        public string Service { get; set; }

        [DataMember(Name = "requestid")]
        public string RequestId { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }

        [DataMember(Name = "content")]
        [JsonFormatter(typeof(StringResolver))]
        public string Content { get; set; }

        public override string ToString() => $"Response --> Service: {Service} - Command: {Command} - RequestId: {RequestId} - TimeStamp: {Timestamp}";
    }

    public class Notify
    {
        [DataMember(Name = "service")]
        public string Service { get; set; }

        public override string ToString() => $"Notify --> Service: {Service}";
    }

    public class Data
    {
        [DataMember(Name = "service")]
        public string Service { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }

        [DataMember(Name = "content")]
        [JsonFormatter(typeof(StringResolver))]
        public string Content { get; set; }

        public override string ToString() => $"Data --> Service: {Service} - Command: {Command} - TimeStamp: {Timestamp}";
    }

    public class Snapshot
    {
        [DataMember(Name = "service")]
        public string Service { get; set; }

        [DataMember(Name = "command")]
        public string Command { get; set; }

        [DataMember(Name = "timestamp")]
        public long Timestamp { get; set; }

        [DataMember(Name = "content")]
        [JsonFormatter(typeof(StringResolver))]
        public string Content { get; set; }

        public override string ToString() => $"Snapshot --> Service: {Service} - Command: {Command} - TimeStamp: {Timestamp}";
    }
}
