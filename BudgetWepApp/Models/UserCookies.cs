namespace BudgetWepApp.Models
{
    public class UserCookies
    {
        private const string UserKey = "users";
        private IRequestCookieCollection requestCookies { get; set; } = null!;
        private IResponseCookies responseCookies { get; set; } = null!;

        public UserCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }
        public UserCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }
        public void setUser(List<User> users)
        {
            List<string> ids = users.Select(x => x.Id).ToList();
            string idString = string.Join(",", ids);
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(30),
            };
            RemoveUserID();
            responseCookies.Append(UserKey, idString, options);
        }
        public string[] GetUserId()
        {
            string cookie = requestCookies[UserKey] ?? String.Empty;
            if(string.IsNullOrEmpty(cookie))
                return Array.Empty<string>();
            else
                return cookie.Split(',');
        }
        public void RemoveUserID()
        {
            responseCookies.Delete(UserKey);
        }
    }
}
