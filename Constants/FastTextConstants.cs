namespace FastTextService.Constants
{
    public class FastTextConstants
    {
        public const float SimilarityThreshold = 0.58f;
        public static readonly string[] BlacklistedWords = ["סכין", "אקדח", "להב"];
        public const string PathToModel = @"C:\Users\oront\Desktop\FastTextService\Language_Model\cc.he.300.bin";
        public const string Port = "http://localhost:5003";
    }
}