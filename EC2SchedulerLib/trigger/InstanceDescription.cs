namespace EC2SchedulerLib.trigger;

class InstanceDescription
{
    public required string InstanceId { get; set; }
    public required string ImageId { get; set; }    
    public required string Name { get; set; }
    public required string PrivateIP { get; set; }
    public required string PublicIP { get; set; }
    public required string InstanceType { get; set; }
    public required string Lifecycle { get; set; }
    public required string Status { get; set; }
    // dd/MM/yyyy hh:mm:ss
    public string? Started { get; set; }
    // dd/MM/yyyy hh:mm:ss
    public string? Finished { get; set; }
    // hh:mm 
    public string? Start { get; set; }
    // hh:mm
    public string? Finish { get; set; }
    public string? User { get; set; }
}