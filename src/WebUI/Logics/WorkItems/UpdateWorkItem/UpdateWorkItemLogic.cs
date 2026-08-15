namespace Vioren.CodebaseAtom.WebUI.Logics.WorkItems.UpdateWorkItem;

public sealed class UpdateWorkItemLogic(
    IDbContextFactory<DatabaseContext> databaseContextFactory,
    IValidator<UpdateWorkItemInput> validator,
    TimeProvider timeProvider)
{
    public async Task Handle(UpdateWorkItemInput input, CancellationToken cancellationToken = default)
    {
        await validator.ValidateInputAsync(input, cancellationToken);

        await using var databaseContext = await databaseContextFactory.CreateDbContextAsync(cancellationToken);

        var workItem = await databaseContext.WorkItems
            .Where(workItem => workItem.Id == input.WorkItemId)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException(DomainDisplayTextFor.WorkItem, DomainDisplayTextFor.Id, input.WorkItemId);

        var today = timeProvider.GetLocalNow().Date.ToDateOnly();

        if (input.Deadline != workItem.Deadline && input.Deadline < today)
        {
            var errorMessage = $"{DomainDisplayTextFor.WorkItem} {DomainDisplayTextFor.Deadline} cannot be in the past.";

            throw new ModelValidationException(errorMessage);
        }

        workItem.Title = input.Title;
        workItem.Description = input.Description;
        workItem.Deadline = input.Deadline;
        workItem.Status = input.Status;

        _ = await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
