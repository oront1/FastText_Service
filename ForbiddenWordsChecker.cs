using FastText.NetWrapper;
using FastTextService.Constants; // As previously used in your semantic vibe checker code.

public class ForbiddenWordsChecker
{
    private static readonly string[] TargetWords = FastTextConstants.BlacklistedWords;
    private readonly Dictionary<string, float[]> _targetEmbeddings = new Dictionary<string, float[]>();
    private readonly FastTextWrapper _model;
    private readonly float _similarityThreshold;

    public ForbiddenWordsChecker(string modelPath, float similarityThreshold = FastTextConstants.SimilarityThreshold)
    {
        _similarityThreshold = similarityThreshold;
        _model = new FastTextWrapper();
        _model.LoadModel(modelPath);
        foreach (var word in TargetWords)
        {
            _targetEmbeddings[word] = _model.GetWordVector(word);
        }
    }

    public string ContainsSimilarWords(string text)
    {
        string blacklistedWords = "";
        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var w in words)
        {
            float[] wVec = _model.GetWordVector(w);
            foreach (var kvp in _targetEmbeddings)
            {
                float similarity = CosineSimilarity(wVec, kvp.Value);
                if (similarity >= _similarityThreshold)
                {
                    blacklistedWords += w + ", ";
                }
            }
        }
        if (blacklistedWords.EndsWith(", "))
        {
            blacklistedWords = blacklistedWords.Substring(0, blacklistedWords.Length - 2);
        }

        return blacklistedWords;
    }

    private float CosineSimilarity(float[] vecA, float[] vecB)
    {
        if (vecA == null || vecB == null || vecA.Length != vecB.Length)
            return 0f;

        float dot = 0f;
        float normA = 0f;
        float normB = 0f;

        for (int i = 0; i < vecA.Length; i++)
        {
            dot += vecA[i] * vecB[i];
            normA += vecA[i] * vecA[i];
            normB += vecB[i] * vecB[i];
        }

        float denominator = (float)(Math.Sqrt(normA) * Math.Sqrt(normB));
        if (denominator == 0f) return 0f;

        return dot / denominator;
    }
}
