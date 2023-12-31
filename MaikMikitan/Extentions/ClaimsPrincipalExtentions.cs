﻿using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.API.Extentions
{
    public static class ClaimsPrincipalExtentions
    {
        public static int GetId(this ClaimsPrincipal claimsPrincipal)
        {
            int.TryParse(claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier), out var id);
            return id;
        }
        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return  claimsPrincipal.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
        }

    }
}
