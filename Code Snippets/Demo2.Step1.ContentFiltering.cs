                if (content.Properties.Any(p => p.PropertyType.PropertyEditorAlias == "UmbracoForms.FormPicker"))
                {

                    return _formsContentApp ?? (_formsContentApp = new ContentApp
                    {
                        Alias = "mcForms",
                        Name = "Form Management",
                        Icon = "icon-bulleted-list color-blue",
                        View = "/App_Plugins/MegaContent/apps/content/formscontentapp.html",
                        Weight = Weight
                    });
                }
