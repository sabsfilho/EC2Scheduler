// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using EC2SchedulerLib;

Console.WriteLine("running...");

const string INSTANCE_ID = "i-02e0b8a91c28d8147";

const string AMI_ID = "ami-0e2c584bc104bd107";

SchedulerRequest r = new SchedulerRequest(){
    Action = ActionEnum.UNDEFINED,
    User = "test"
};

// SetUseCaseParams(r, 9);
 
string input = JsonSerializer.Serialize(r);
Console.WriteLine(input);
string txt = Control.Run(input);
Console.WriteLine(txt);

Console.WriteLine("finished");


static void SetUseCaseParams(SchedulerRequest r, int useCaseId)
{
    switch(useCaseId)
    {
        case 1:
            r.Action = ActionEnum.UpdateScheduledInstance;
            r.ParamA = INSTANCE_ID;
            r.ParamB = "SchedulerStart";
            r.ParamC = "08:00";
            break;
        case 2:
            r.Action = ActionEnum.UpdateScheduledInstance;
            r.ParamA = INSTANCE_ID;
            r.ParamB = "SchedulerFinish";
            r.ParamC = "18:00";
            break;
        case 3:
            r.Action = ActionEnum.StartScheduledInstances;
            break;
        case 4:
            r.Action = ActionEnum.FinishScheduledInstances;
            break;
        case 5:
            r.Action = ActionEnum.ListScheduledInstances;
            break;
        case 6:
            r.Action = ActionEnum.RemoveScheduledTag;
            r.ParamA = INSTANCE_ID;
            break;
        case 7:
            r.Action = ActionEnum.CreateImage;
            r.ParamA = INSTANCE_ID;
            r.ParamB = "DEV";
            break;
        case 8:
            r.Action = ActionEnum.ListImages;
            break;
        case 9:
            r.Action = ActionEnum.DeleteImage;
            r.ParamA = AMI_ID;
            break;            
    }

}