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
using Umbraco.Core.Services;
using Umbraco.Forms.Core.Models;
using Umbraco.Forms.Data.Storage;
using Umbraco.Web;

namespace MegaContentApp.Core.ContentApps
{

    public class FormsAppComponent : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.ContentApps().Append<FormsApp>();
        }
    }

    public class FormsApp : IContentAppFactory
    {
        private const int Weight = -10; // Info is at +100, we want this to show first.

        private readonly ILogger _logger;
        private readonly IUserSecurityStorage _userSecurityStorage;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        private ContentApp _formsContentApp;

        public FormsApp(ILogger logger,
                        IUserSecurityStorage userSecurityStorage,
                        IUmbracoContextAccessor umbracoContextAccessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userSecurityStorage = userSecurityStorage ?? throw new ArgumentNullException(nameof(userSecurityStorage));
            _umbracoContextAccessor = umbracoContextAccessor ?? throw new ArgumentNullException(nameof(umbracoContextAccessor));
        }

        public UmbracoContext UmbracoContext
        {
            get
            {
                return _umbracoContextAccessor.UmbracoContext;
            }
        }

        public ContentApp GetContentAppFor(object source, IEnumerable<IReadOnlyUserGroup> userGroups)
        {
            // Check if we have a content item that has a form picker on it.
            if (source is IContent content) {
                _logger.Debug<FormsApp>($"Content type is {content.ContentType.Alias}");

                if (content.Properties.Any(p => p.PropertyType.PropertyEditorAlias == "UmbracoForms.FormPicker"))
                {
                    
                    // We also want to check that the user has access to forms.
                    if (!userGroups.Any(g => g.AllowedSections.Any(s => s == "forms")))
                    {
                        return null;
                    }

                    if (!CanCurrentUserManageForms())
                    {
                        return null;
                    }

                    return _formsContentApp ?? (_formsContentApp = new ContentApp
                    {
                        Alias = "mcForms",
                        Name = "Form Management",
                        Icon = "icon-bulleted-list color-blue",
                        View = "/App_Plugins/MegaContent/apps/content/formscontentapp.html",
                        Weight = Weight
                    });
                }
            }

            return null;
        }

        private bool CanCurrentUserManageForms()
        {
            // We can check the Forms UserSecurity database table for manage forms permissions.
            IUser currentUser = UmbracoContext.Security.CurrentUser;
            UserSecurity userSecurity = _userSecurityStorage.GetUserSecurity(currentUser.Id.ToString());
            return userSecurity != null ? userSecurity.ManageForms : currentUser.IsAdmin();
        }

    }
}
