// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using EC2SchedulerLib;

Console.WriteLine("running...");

SchedulerRequest r = new SchedulerRequest(){
    Action = ActionEnum.UNDEFINED,
    User = "test"
};

r.Action = ActionEnum.UpdateScheduledInstance;
r.ParamA = "i-02e0b8a91c28d8147";


string input = JsonSerializer.Serialize(r);

Console.WriteLine(input);

string txt = Control.Run(input);

Console.WriteLine(txt);

Console.WriteLine("finished");
