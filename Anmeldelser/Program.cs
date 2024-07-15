using Anmeldelser;
using Google.Cloud.Language.V1;

LanguageServiceClient client = LanguageServiceClient.Create();

List<String> reviews = new Repository().GetReviews();

int id = 0;
foreach (String review in reviews)
{
    Console.WriteLine($"Anmeldelse {++id}:");
    Console.WriteLine("=============");
    Document document = Document.FromPlainText(review);

    ClassifyText(document);
    AnalyzeSentiment(document);
    ModerationCategories(document);

    Console.WriteLine("=============");
    Console.WriteLine();
}

void AnalyzeSentiment(Document document)
{
    AnalyzeSentimentResponse response = client.AnalyzeSentiment(document);
    Sentiment sentiment = response.DocumentSentiment;

    Console.WriteLine($"Negativ - Positiv [-1 til 1]: {sentiment.Score}");
    Console.WriteLine($"Mængde af følelse: {sentiment.Magnitude}");
    Console.WriteLine();
}

void ClassifyText(Document document)
{
    ClassifyTextResponse response = client.ClassifyText(document);
    Console.WriteLine("Kategorier:");
    foreach (ClassificationCategory category in response.Categories)
    {
        Console.WriteLine($"- {category.Name} (sikkerhed {category.Confidence:P2})");
    }
    Console.WriteLine();
}

void ModerationCategories(Document document)
{
    ModerateTextResponse response = client.ModerateText(document);
    Console.WriteLine("Moderationskategorier:");
    foreach (ClassificationCategory category in response.ModerationCategories)
    {
        Console.WriteLine($"- {category.Name} (sikkerhed {category.Confidence:P2})");

    }
    Console.WriteLine();
}