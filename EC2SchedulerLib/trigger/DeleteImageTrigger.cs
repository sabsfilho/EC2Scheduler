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
            string snapshotId = blockMap.DeviceName;
            if (string.IsNullOrEmpty(snapshotId)) continue;
            int i1 = snapshotId.IndexOf('=');
            if (i1 == -1) continue;
            i1++;
            int i2 = snapshotId.IndexOf(':', i1);
            if (i2 == -1) continue;
            snapshotId = snapshotId.Substring(i1, i2 - i1);

            var requestSnapshot = new DeleteSnapshotRequest(){
                SnapshotId = snapshotId
            };
            var dsnap = EC2Client.DeleteSnapshotAsync(requestSnapshot);
            dsnap.Wait();
        }

        return "ok";
    }
}