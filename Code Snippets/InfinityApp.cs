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

        private readonly ILogger _logger;

        private ContentApp _productsContentApp;
        private ContentApp _homeContentApp;

        private ContentApp _mediaContentApp;

        public InfinityApp(ILogger logger)
        {
            _logger = logger;
        }

        public ContentApp GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            _logger.Debug<InfinityApp>($"Source is {source.GetType()}");
            switch(source)
            {
                case IContent content:
                    _logger.Debug<InfinityApp>($"Content type is {content.ContentType.Alias}");
                    if (content.ContentType.Alias == "products")
                        return _productsContentApp ?? (_productsContentApp = new ContentApp
                        {
                            Alias = "mcProducts",
                            Name = "Products",
                            Icon = "icon-box",
                            View = "/App_Plugins/MegaContent/apps/content/productscontentapp.html",
                            Weight = Weight
                        });

                    if (content.ContentType.Alias == "home")
                        return _homeContentApp ?? (_homeContentApp = new ContentApp
                        {
                            Alias = "mcHome",
                            Name = "Home",
                            Icon = "icon-globe",
                            View = "/App_Plugins/MegaContent/apps/content/homecontentapp.html",
                            Weight = Weight
                        });
                    break;
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
