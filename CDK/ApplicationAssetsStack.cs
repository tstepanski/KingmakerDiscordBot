using Amazon.CDK;
using Amazon.CDK.AWS.S3;
using Amazon.CDK.AWS.S3.Deployment;

namespace KingmakerDiscordBot.CDK;

internal sealed class ApplicationAssetsStack : Stack
{
    public ApplicationAssetsStack(App application, Hasher hasher) : base(application, nameof(ApplicationAssetsStack))
    {
        var artifactPath = this.GetContextOrThrow(Constants.ArtifactPathKey);
        
        AddFilesToHasher(hasher, artifactPath);
        
        Bucket = CreateBucket(this);
        Deployment = CreateDeployment(this, Bucket, artifactPath);
    }

    public Bucket Bucket { get; }
    
    public BucketDeployment Deployment { get; }

    private static void AddFilesToHasher(Hasher hasher, string artifactPath)
    {
        foreach (var file in Directory.GetFiles(artifactPath))
        {
            hasher.AddFile(file);
        }
    }

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

    private static BucketDeployment CreateDeployment(ApplicationAssetsStack stack, Bucket bucket, string artifactPath)
    {
        var applicationAsset = Source.Asset(artifactPath);
        
        var deploymentProperties = new BucketDeploymentProps
        {
            DestinationBucket = bucket,
            MemoryLimit = 512,
            Prune = true,
            Sources = [
                applicationAsset
            ]
        };
        
        return new BucketDeployment(stack, nameof(BucketDeployment), deploymentProperties);
    }
}