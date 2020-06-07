using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Account
{
    [Serializable]
    public class PlayerAccount
    {
        [JsonProperty]
        public int AccountId;

        [JsonProperty]
        public string Email;
        [JsonProperty]
        public string Username;
        [JsonProperty]
        public string Password;

        [JsonConstructor]
        public PlayerAccount(int accountId, string email, string userName, string password)
        {
            this.AccountId = accountId;
            this.Email = email;
            this.Username = userName;
            this.Password = password;
        }

        public PlayerAccount(string email, string userName, string password)
        {
            this.Email = email;
            this.Username = userName;
            this.Password = password;
        }
    }
}
