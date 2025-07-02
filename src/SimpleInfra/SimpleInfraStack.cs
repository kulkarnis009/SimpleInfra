using Amazon.CDK;
using Constructs;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.EC2;

namespace SimpleInfra
{
    public class SimpleInfraStack : Stack
    {
        public SimpleInfraStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // creating VPC
            var vpc = new Vpc(this, "SimpleVpc", new VpcProps
            {
                MaxAzs = 2
            });

            // creating Ec2
            var instance1 = new Instance_(this, "Ec2Instance1", new InstanceProps
            {
                Vpc = vpc,
                InstanceType = InstanceType.Of(InstanceClass.BURSTABLE3, InstanceSize.MICRO),
                MachineImage = MachineImage.LatestAmazonLinux2023()
            });

            // creating Ec2
            var instance2 = new Instance_(this, "Ec2Instance2", new InstanceProps
            {
                Vpc = vpc,
                InstanceType = InstanceType.Of(InstanceClass.BURSTABLE3, InstanceSize.MICRO),
                MachineImage = MachineImage.LatestAmazonLinux2023()
            });

            var bucket1 = new Bucket(this, "bucket1", new BucketProps
            {
                RemovalPolicy = RemovalPolicy.DESTROY,
                AutoDeleteObjects = true
            });

            var bucket2 = new Bucket(this, "bucket2", new BucketProps
            {
                RemovalPolicy = RemovalPolicy.DESTROY,
                AutoDeleteObjects = true
            });

            new CfnOutput(this, "vpcId", new CfnOutputProps { Value = vpc.VpcId });
            new CfnOutput(this, "Instance1Id", new CfnOutputProps { Value = instance1.InstanceId });
            new CfnOutput(this, "instance2Id", new CfnOutputProps { Value = instance2.InstanceId });
            new CfnOutput(this, "Bucket1Name", new CfnOutputProps { Value = bucket1.BucketName });
            new CfnOutput(this, "Bucket2Name", new CfnOutputProps { Value = bucket2.BucketName });
        }
    }
}
