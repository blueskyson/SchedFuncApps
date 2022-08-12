using System;

namespace DemoApp.MvcApp.Models;
public class PayloadCache
{
    public List<Payload>? Payloads { get; set; }
    public List<DateTimeOffset> PayloadTimestamps {get; set; }

    public PayloadCache() {
        Payloads = new List<Payload>();
        PayloadTimestamps = new List<DateTimeOffset>();
    }

    public void Add(Payload payload) {
        Payloads!.Add(payload);
        PayloadTimestamps!.Add(DateTimeOffset.UtcNow);
    }

    public void Clear() {
        Payloads!.Clear();
        PayloadTimestamps!.Clear();
    }
}