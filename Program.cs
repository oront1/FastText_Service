using FastTextService.Constants;
var builder = WebApplication.CreateBuilder(args);

// You must specify the path to your Hebrew model file
// and implement ForbiddenWordsChecker similarly to your SemanticVibeChecker.
string modelPath = FastTextConstants.PathToModel;
builder.Services.AddSingleton(new ForbiddenWordsChecker(modelPath, 0.58f));

builder.Services.AddControllers();
var app = builder.Build();

app.MapControllers();
app.Run(FastTextConstants.Port);