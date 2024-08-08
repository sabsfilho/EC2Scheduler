# EC2Scheduler a .NET 8 AWS Lambda Function microservice
![AWSLambdaAndDockerInDocker](https://github.com/user-attachments/assets/28ce3e1a-c849-49fc-80a2-3edb970e34f3)

This is a AWS Lambda Function microservice .NET 8 project.

*This project is a small piece of a broader brownfield project. I am responsible to modernize our .NET Framework ecosystem from a monolithic architecture based in Virtual Machines to a Service Oriented Architecture using AWS technologies.*

The purpose of this microservice is to control **AWS Elastic Compute Cloud EC2** resources, executing Scheduled Tasks or **Cron Jobs**.

It provides a pipeline to allow some infrastructure maintenance procedures to be executed by our internal services.
I tailored this project to be public on my GitHub repository for training my colleagues. *I didn't implement any internal pieces that would expose some complexities of our business logic.*

This project is also used as the Backend module by a Frontend .NET8 project, which is a Web Application written in Javascript and React, and using Tailwind.css. *I am working on a public version to allow me to put it into my open GitHub repository and then release a more complete Full Stack system.*

I wrote an all-in-one guide to help me and my colleagues to create a straightforward walk through to create a boilerplate for AWS Lambda Serverless Function from Zero to Hero. using the cost effective Graviton Arm64 processor from a VS Code Docker In Docker Dev Container running on Linux/Ubuntu EC2 Instance. [Click here and check it out!](https://www.linkedin.com/pulse/publish-net-8-microservice-aws-lambda-function-using-cost-santos-vsiqe)

As this project consumes AWS cloud resources, it is fundamental to install **AWS CLI** and tools. The AWS credentials must also be set. If you need help on this environment configuration, I really recommend reading this all-in-one guide.

