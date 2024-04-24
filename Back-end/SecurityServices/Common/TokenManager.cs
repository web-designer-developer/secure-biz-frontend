using SecurityServices.Models;
using System.Text;

namespace SecurityServices.Common
{
    public  class TokenManager
    {
        private serviceContext _context;
        public TokenManager(serviceContext context)
        {
            _context = context;
        }
        public TokenManager()
        {
            _context = new serviceContext();

        }
        public  string GenerateToken(string sessionId, string userId )
        {
            DateTime generatedAt = DateTime.UtcNow;
            DateTime expiresAt = DateTime.UtcNow.AddDays(1);
            string input = sessionId + "_" + userId + "_" + generatedAt.ToLongDateString() +"_" +expiresAt.ToLongDateString();
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            string token = Convert.ToBase64String(bytes);
            return token;
        }
        public bool ValidateToken(string token, string userName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(userName) || token.ToLower() == "default" || userName.ToLower() == "default")
                {
                    return false;
                }
                byte[] bytes = Convert.FromBase64String(token);
                string result = Encoding.UTF8.GetString(bytes);
                var isTokenValid = false;
                var sessionid = result.Split('_').First();
                var user = _context.Users.Where(x => x.Username == userName).FirstOrDefault();
                if (user != null)
                {
                    isTokenValid = (from ut in _context.Usertokens
                                    where ut.Sessionid == sessionid && ut.Userid == user.Id && ut.Expiresat >= DateTime.UtcNow
                                    select ut.Id).Any();
                }
                return isTokenValid;
            }
            catch
            {
                return false;
            }
        }

        public bool ExpireToken(string token, string userName) 
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(token);
                string result = Encoding.UTF8.GetString(bytes);

                var sessionid = result.Split('_').First();
                var userId = _context.Users.Where(x => x.Username == userName).FirstOrDefault().Id;
                var userToken = (from ut in _context.Usertokens
                                 where ut.Sessionid == sessionid && ut.Userid == userId && ut.Expiresat >= DateTime.UtcNow
                                 select ut).FirstOrDefault();

                if (userToken == null)
                {
                    return false;
                }
                else
                {
                    userToken.Expiresat = DateTime.UtcNow;
                    _context.Update(userToken);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {

                return false;
            }
        }


    }
}
