using System;
using System.Linq;
using System.Security.Claims;

namespace Sysplan.Crosscutting.Common.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static Guid GetUserIdFromToken(this ClaimsPrincipal claimsPrincipal)
        {
            var checkClaimsPrincipal = claimsPrincipal.CheckClaimsPrincipal();

            if (checkClaimsPrincipal)
            {
                var claims = claimsPrincipal.FindAll(t => t.Type == "idUser");

                if (!claims.Any()) return Guid.Empty;

                return Guid.Parse(claims.FirstOrDefault()?.Value);
            }

            return Guid.Empty;
        }

        public static string GetUserNameFromToken(this ClaimsPrincipal claimsPrincipal)
        {
            var checkClaimsPrincipal = claimsPrincipal.CheckClaimsPrincipal();

            if (checkClaimsPrincipal)
            {
                var claims = claimsPrincipal.FindAll(t => t.Type == "name");

                if (!claims.Any()) return string.Empty;

                return claims.FirstOrDefault()?.Value.ToString();
            }

            return string.Empty;
        }

        public static string GetUserLoginFromToken(this ClaimsPrincipal claimsPrincipal)
        {
            var checkClaimsPrincipal = claimsPrincipal.CheckClaimsPrincipal();

            if (checkClaimsPrincipal)
            {
                var claims = claimsPrincipal.FindAll(t => t.Type == "username");

                if (!claims.Any()) return string.Empty;

                return claims.FirstOrDefault()?.Value.ToString();
            }

            return string.Empty;
        }

        public static string GetUserEmailFromToken(this ClaimsPrincipal claimsPrincipal)
        {
            var checkClaimsPrincipal = claimsPrincipal.CheckClaimsPrincipal();

            if (checkClaimsPrincipal)
            {
                var claims = claimsPrincipal.FindAll(t => t.Type == "email");

                if (!claims.Any()) return string.Empty;

                return claims.FirstOrDefault()?.Value.ToString();
            }

            return string.Empty;
        }

        private static bool CheckClaimsPrincipal(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                return false;

            return true;
        }

        public static bool GetReadOnlyScopeFromToken(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.CheckClaimsPrincipal())
            {
                var claim = claimsPrincipal.FindFirst(t => t.Type == "scope" && t.Value == "readonly");

                if (claim == null) return false;

                return true;
            }

            return false;
        }
    }
}