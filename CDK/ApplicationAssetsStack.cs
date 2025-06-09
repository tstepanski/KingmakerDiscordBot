using Amazon.CDK;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.S3.Deployment;

namespace KingmakerDiscordBot.CDK;

internal sealed class ApplicationAssetsStack : Stack
{
    public ApplicationAssetsStack(App application) : base(application, nameof(ApplicationAssetsStack))
    {
        Bucket = CreateBucket(this);
        //Deployment = CreateDeployment(this, Bucket);
    }
    
    public Bucket Bucket { get; }
    
    //public BucketDeployment Deployment { get; }

    private static Bucket CreateBucket(ApplicationAssetsStack stack)
    {
        var properties = new BucketProps
        {
            AccessControl = BucketAccessControl.PRIVATE,
            BlockPublicAccess = BlockPublicAccess.BLOCK_ALL,
            BucketName = Constants.BotName,
            EnforceSSL = true,
            PublicReadAccess = false,
            RemovalPolicy = RemovalPolicy.DESTROY
        };
        
        return new Bucket(stack, nameof(Bucket), properties);
    }

    private static BucketDeployment CreateDeployment(ApplicationAssetsStack stack, Bucket bucket)
    {
        var artifactPath = stack.GetContextOrThrow(Constants.ArtifactPathKey);
        var applicationAsset = Source.Asset(artifactPath);
        
        var deploymentProperties = new BucketDeploymentProps
        {
            DestinationBucket = bucket,
            Prune = true,
            Sources = [
                applicationAsset
            ]
        };
        
        return new BucketDeployment(stack, nameof(BucketDeployment), deploymentProperties);
    }
}