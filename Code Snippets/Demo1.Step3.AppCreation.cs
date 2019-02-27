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
