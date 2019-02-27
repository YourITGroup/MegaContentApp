public class FormsApp : IContentAppFactory
{
    private const int Weight = -10; // Info is at +100, we want this to show first.

    private ContentApp _formsContentApp;

    public FormsApp()
    {
    }

    public ContentApp GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
    {


        return null;
    }
}
