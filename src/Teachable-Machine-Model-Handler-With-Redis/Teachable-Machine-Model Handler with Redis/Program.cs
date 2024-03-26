using Teachable_Machine_Model_Handler_with_Redis;

public class Program
{
    public static void Main(string[] args)
    {
        string modelUrl = "https://teachablemachine.withgoogle.com/models/WHbfbotMb/" + "model.json";
        string metadataUrl = "https://teachablemachine.withgoogle.com/models/WHbfbotMb/" + "metadata.json";
        string redisConnectionString = "127.0.0.1";

        var modelRunner = new TeachableMachineService(modelUrl, metadataUrl);
        var redisListener = new RedisListener(redisConnectionString);
        var getByteImage = redisListener.ListenForData("video");
        var result = modelRunner.Predict(getByteImage);
    }
}