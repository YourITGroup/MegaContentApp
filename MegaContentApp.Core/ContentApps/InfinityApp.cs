using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Composing;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Models.ContentEditing;
using Umbraco.Core.Models.Membership;
using Umbraco.Web;

namespace MegaContentApp.Core.ContentApps
{

    public class InfinityAppComponent : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.ContentApps().Append<InfinityApp>();
        }
    }

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
            switch(source)
            {
                case IContent _:
                    return _contentApp ?? (_contentApp = new ContentApp
                    {
                        Alias = "mcContent",
                        Name = "To Infinity",
                        Icon = "icon-box",
                        View = "/App_Plugins/MegaContent/apps/content/infinitycontentapp.html",
                        Weight = Weight
                    });
                case IMedia _:
                    return _mediaContentApp ?? (_mediaContentApp = new ContentApp
                    {
                        Alias = "mcProducts",
                        Name = "Products",
                        Icon = "icon-box",
                        View = "/App_Plugins/MegaContent/apps/media/mediacontentapp.html",
                        Weight = Weight
                    });
            }

            return null;
        }
    }
}
