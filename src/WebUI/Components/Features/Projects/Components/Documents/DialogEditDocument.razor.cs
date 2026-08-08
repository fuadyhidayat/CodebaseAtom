using Vioren.CodebaseAtom.WebUI.Logics.Documents.UpdateDocument;

namespace Vioren.CodebaseAtom.WebUI.Components.Features.Projects.Components.Documents;

public partial class DialogEditDocument
{
    [Inject]
    public required UpdateDocumentLogic UpdateDocumentLogic { get; init; }

    [Parameter]
    public required EditDocumentModel Model { get; set; }

    private async Task OnValidSubmitAsync()
    {
        try
        {
            IsLoadingBase = true;

            var input = new UpdateDocumentInput
            {
                DocumentId = Model.DocumentId,
                Title = Model.Title,
                FileNameWithoutExtension = Model.FileNameWithoutExtension
            };

            await UpdateDocumentLogic.Handle(input);

            Snackbar.AddSuccess($"The {DomainDisplayTextFor.Document} has been updated successfully.");

            Dialog.Close();
        }
        catch (Exception exception)
        {
            ExceptionBase = exception;
        }
        finally
        {
            IsLoadingBase = false;
        }
    }
}

public sealed record EditDocumentModel
{
    public Guid DocumentId { get; init; }
    public required string Title { get; set; }
    public required string FileNameWithoutExtension { get; set; }
    public required string FileExtension { get; init; }
}
