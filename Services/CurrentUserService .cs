//using System.IdentityModel.Tokens.Jwt;
//using TaskManagement.Interfaces;

//namespace TaskManagement.Services
//{
//    public class CurrentUserService : ICurrentUserService
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public CurrentUserService(IHttpContextAccessor accessor)
//        {
//            _httpContextAccessor = accessor;
//        }

//        public int? UserId
//        {
//            get
//            {
//                var httpContext = _httpContextAccessor.HttpContext;
//                var user = httpContext?.User;

//                if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
//                {
//                    Console.WriteLine("CurrentUserService: user not authenticated or HttpContext/User is null");
//                    return null;
//                }

//                // Debug: log all claims once
//                Console.WriteLine("=== Claims in token ===");
//                foreach (var claim in user.Claims)
//                {
//                    Console.WriteLine($"{claim.Type}: {claim.Value}");
//                }
//                Console.WriteLine("=== End claims ===");

//                // Prefer JwtRegisteredClaimNames.Sub for user id
//                var userIdClaim =
//                    user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value
//                    ?? user.FindFirst("sub")?.Value
//                    ?? user.FindFirst("uid")?.Value
//                    ?? user.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value
//                    ?? user.FindFirst("unique_name")?.Value;

//                Console.WriteLine($"CurrentUserService: userIdClaim = {userIdClaim}");

//                return int.TryParse(userIdClaim, out var id) ? id : (int?)null;
//            }
//        }
//    }
//}
