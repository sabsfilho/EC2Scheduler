# EC2Scheduler a .NET 8 AWS Lambda Function microservice
![AWSLambdaAndDockerInDocker](https://github.com/user-attachments/assets/28ce3e1a-c849-49fc-80a2-3edb970e34f3)

This is a AWS Lambda Function microservice .NET 8 project.

*This project is a small piece of a broader brownfield project. I am responsible to modernize our .NET Framework ecosystem from a monolithic architecture based in Virtual Machines to a Service Oriented Architecture using AWS technologies.*

The purpose of this microservice is to control **AWS Elastic Compute Cloud EC2** resources, executing Scheduled Tasks or **Cron Jobs**.

It provides a pipeline to allow some infrastructure maintenance procedures to be executed by our internal services.
I tailored this project to be public on my GitHub repository for training my colleagues. *I didn't implement any internal pieces that would expose some complexities of our business logic.*

This project is also used as the Backend module by a Frontend .NET8 project, which is a Web Application written in Javascript and React, and using Tailwind.css. *I am working on a public version to allow me to put it into my open GitHub repository and then release a more complete Full Stack system.*

I wrote an all-in-one guide to help me and my colleagues to create a straightforward walk through to create a boilerplate for AWS Lambda Serverless Function from Zero to Hero. using the cost effective Graviton Arm64 processor from a VS Code Docker In Docker Dev Container running on Linux/Ubuntu EC2 Instance. [Click here and check it out!](https://www.linkedin.com/pulse/publish-net-8-microservice-aws-lambda-function-using-cost-santos-vsiqe)

As this project consumes AWS cloud resources, it is fundamental to install **AWS CLI** and related tools. The AWS credentials must also be set. If you need help on this environment configuration, I really recommend reading this all-in-one guide I mentioned before.

__Implemented Services__<br/>
__ListScheduledInstances__: Return a JSON List having the description of EC2 Instances filtered by Tags: Control.SCHEDULER_REQUESTED_BY_USER, Control.SCHEDULER_KEY_START, Control.SCHEDULER_KEY_FINISH<br/>
__UpdateScheduledInstances__: Set/Update Instance Tag from InstanceId in ParamA, TagName in ParamB and TagValue in ParamC<br/>
__StartScheduledInstances__: Start Instances using the time definition from Tag Control.SCHEDULER_KEY_START<br/>
__FinishScheduledInstances__: Finish Instances using the time definition from Tag Control.SCHEDULER_KEY_FINISH<br/>
__ListImages__: Return a JSON List tagged with Control.SCHEDULER_KEY_IMAGE<br/>
__CreateImage__: Create Image from InstanceId in ParamA having its name defined as the Key in ParamB and current DateTime formatted as yyyyMMdd_hhmmss<br/>
__DeleteImage__: Delete Images and related Snapshots from ImageIds in ParamA<br/>
__CreateInstanceFromImage__: Create Instance from ImageID in ParamA, InstanceType in SchedulerRequest.ParamB (Control.INSTANCE_TYPE is default) and using Control.KEY_PAIR_NAME, Control.SCHEDULER_REQUESTED_BY_USER, Control.SCHEDULER_DEFAULT_DURATION_HOURS, Control.SUBNET_ID, Control.SECURITY_GROUP_ID. Return the created InstanceId.<br/>
__TerminateInstance__: Terminate Instance from InstanceIds in ParamA<br/>
__MonitorWebServices__: Monitor services from Control.BUCKET_NAME_PCB_LIB file are running. If not, send alert message to administrators. <br/>
__RunAllServices__: Trigger Cron jobs and maintenance services<br/>

Complimentary Parameters:<br/>
ParamA = auxiliary parameter A<br/>
ParamB = auxiliary parameter B<br/>
ParamC = auxiliary parameter C<br/>

__Project structure__<br/>
__EC2SchedulerApp__: .NET 8 Console application that consumes EC2SchedulerLib. Another convenient way to use the implemented procedures executing Command Line commands.<br/>
__EC2SchedulerLib__: .NET 8 Library having the implemented procedures as described above<br/>
__EC2SchedulerFunction__: .NET 8 AWS Lambda Function that consumes EC2SchedulerLib.<br/>
__EC2SchedulerFunction/src__: .NET 8 AWS Lambda Function implementation to be published as a Docker container to AWS cloud.<br/>
__EC2SchedulerFunction/test__: .NET 8 Unit Tests to be evaluated in the build process.<br/>

**__document under construction__**

