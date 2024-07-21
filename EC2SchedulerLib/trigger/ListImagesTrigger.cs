using System.Text.Json;
using Amazon.EC2.Model;

namespace EC2SchedulerLib.trigger;

class ListImagesTrigger : ATrigger
{
    List<ImageDescription> BuildList()
    {
        var imageDescriptions = new List<ImageDescription>();

        var request = new DescribeImagesRequest();
        request.Filters.Add(
            new Filter(
                "tag-key",
                new List<string>(){
                    Control.SCHEDULER_KEY_IMAGE
                }
            )
        );
        var t = EC2Client.DescribeImagesAsync(request);
        t.Wait();
        var response = t.Result;
        var imgs = response.Images;
        foreach(var img in imgs)
        {
            var i = new ImageDescription(){
                ImageId = img.ImageId,
                CreationDate = img.CreationDate,
                Name = img.Name
            };
            imageDescriptions.Add(i);
        }
        return imageDescriptions;
    }
    protected override string RunRequest()
    {
        return
            JsonSerializer.Serialize(BuildList());
    }
}