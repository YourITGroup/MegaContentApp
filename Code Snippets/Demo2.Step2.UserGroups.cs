                    
                    // We also want to check that the user has access to forms.
                    if (!userGroups.Any(g => g.AllowedSections.Any(s => s == "forms")))
                    {
                        return null;
                    }
