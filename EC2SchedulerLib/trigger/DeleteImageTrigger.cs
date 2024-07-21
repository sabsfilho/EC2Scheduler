using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;

class DeleteImageTrigger : ATrigger
{
    protected override string RunRequest()
    {
        string? ids = SchedulerRequest.ParamA;
        if (ids == null){
            throw new Exception("InstanceIds undefined in ParamA");
        }

        var imgr = new DescribeImagesRequest(){
            ImageIds = ids.Split(',').ToList()
        };
        var timgr = EC2Client.DescribeImagesAsync(imgr);
        timgr.Wait();
        var imgs = timgr.Result.Images;
        foreach(var img in imgs){
            var dir = new DeregisterImageRequest(){
                ImageId = img.ImageId
            };
            
            var tdir = EC2Client.DeregisterImageAsync(dir);
            tdir.Wait();
            if (tdir.Result.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                continue;
            }
            var blockMap = img.BlockDeviceMappings.FirstOrDefault();
            if (blockMap == null) continue;

            var requestSnapshot = new DeleteSnapshotRequest(){
                SnapshotId = blockMap.Ebs.SnapshotId
            };
            var dsnap = EC2Client.DeleteSnapshotAsync(requestSnapshot);
            dsnap.Wait();
        }

        return "ok";
    }
}