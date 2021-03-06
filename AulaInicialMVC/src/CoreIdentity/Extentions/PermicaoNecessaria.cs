﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Extentions
{
    public class PermissaoNecessaria : IAuthorizationRequirement
    {
        public string Permissao { get; }

        public PermissaoNecessaria(string permissao) => Permissao = permissao;
    }

    public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessaria>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoNecessaria requisto)
        {
            if (context.User.HasClaim(c => c.Type == "Permissao" && c.Value.Contains(requisto.Permissao)))
                context.Succeed(requisto);

            return Task.CompletedTask;
        }
    }
}
