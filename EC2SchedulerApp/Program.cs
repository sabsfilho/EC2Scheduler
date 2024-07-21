﻿// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using EC2SchedulerLib;

Console.WriteLine("running...");

SchedulerRequest r = new SchedulerRequest(){
    Action = ActionEnum.UNDEFINED,
    User = "test"
};

/*
r.Action = ActionEnum.UpdateScheduledInstance;
r.ParamA = "i-02e0b8a91c28d8147";
r.ParamB = "SchedulerStart";
r.ParamC = "08:00";
r.Action = ActionEnum.UpdateScheduledInstance;
r.ParamA = "i-02e0b8a91c28d8147";
r.ParamB = "SchedulerFinish";
r.ParamC = "18:00";

r.Action = ActionEnum.StartScheduledInstances;
r.Action = ActionEnum.FinishScheduledInstances;

r.Action = ActionEnum.ListScheduledInstances;
r.Action = ActionEnum.ListImages;

r.Action = ActionEnum.RemoveScheduledTag;
r.ParamA = "i-02e0b8a91c28d8147";
*/

r.Action = ActionEnum.ListImages;


string input = JsonSerializer.Serialize(r);

Console.WriteLine(input);

string txt = Control.Run(input);

Console.WriteLine(txt);

Console.WriteLine("finished");
