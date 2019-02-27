public class InfinityApp : IContentAppFactory
{
    private const int Weight = +10; // Info is at +100, we want this to show first.

    private ContentApp _contentApp;
    private ContentApp _mediaContentApp;

    public InfinityApp()
    {
    }

    public ContentApp GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
    {


        return null;
    }
}
