﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace TestAspCoreTuto.Bootstrapping.Authorizations.Conventions
{
    public class AddAuthorizeFiltersControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            //TODO +WORK
            //controller.Filters.Add(new AuthorizeFilter("defaultPolicy"));
            //TODO +WORK
        }
    }
}
