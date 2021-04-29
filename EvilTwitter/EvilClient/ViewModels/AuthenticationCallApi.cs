using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Minitwit.Entities;
using Newtonsoft.Json;
using static System.Net.HttpStatusCode;

namespace EvilClient.ViewModels
{
    public class AuthenticationCallApi : IAuthenticationCallApi
    {
        private readonly IUtilViewModel _util;
        private readonly IUserState _userState;
        private readonly HttpClient _httpClient;

        public AuthenticationCallApi(IUtilViewModel utilViewModel, IUserState userState, HttpClient httpClient)
        {
            _util = utilViewModel;
            _userState = userState;
            _httpClient = httpClient;
        }

        public async Task<User> GetUserFromUsername(string username)
        {
            var response = await _httpClient.GetAsync(_util.APIURL + "user/" + username);
            var content = await response.Content.ReadAsStringAsync();

            var user = System.Text.Json.JsonSerializer.Deserialize<User>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return user;
        }

        public async Task<bool> SignIn(string username)
        {
            var response = await _httpClient.GetAsync(_util.APIURL + "user/" + username);

            if (response.StatusCode != OK)
            {
                return false;
            }

            var content = await response.Content.ReadAsStringAsync();

            var user = System.Text.Json.JsonSerializer.Deserialize<User>
            (
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            _userState.User = user;
            return true;
        }

        public async Task<HttpStatusCode> SignUp(string username, string email, string password)
        {
            var isEmailValid = ValidateEmail(email);
            var isPasswordValid = ValidatePassword(password);
            if (!isEmailValid || !isPasswordValid)
            {
                return BadRequest;//UnprocessableEntity;
            } 

            var userToDB = new User
            {
                username = username,
                email = email,
                pwd = _util.CurrentPasswordHasher(password)
            };
            
            var json = JsonConvert.SerializeObject(userToDB);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_util.APIURL + "register/", data);

            if (response.StatusCode == NoContent)
            {
                return OK;
            }

            return BadRequest;
        }

        public bool ValidateEmail(string email)
        {
            //Got from stackoverflow: https://stackoverflow.com/questions/201323/how-to-validate-an-email-address-using-a-regular-expression
            var pattern = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*)@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
            var reg = new Regex(pattern);
            
            return reg.IsMatch(email); 
        }

        public bool ValidatePassword(string password) 
        {
            var containsDigit = password.Any(char.IsDigit);
            var containsUppercase = password.Any(char.IsUpper);
            var containsLowercase = password.Any(char.IsLower);
            var longerThanSevenChars = password.Length >= 8; 

            return containsLowercase && containsUppercase && longerThanSevenChars && containsDigit;
        }

        public string GeneratePasswardValidationErrorMessage(string password) 
        {
            var errorMessage = "Password must: ";
            if (!password.Any(char.IsDigit))
                errorMessage += "|contain a digit| ";
            if (!password.Any(char.IsUpper))
                errorMessage += "|contain an uppercase letter| ";
            if (!password.Any(char.IsLower))
                errorMessage += "|contain a lowercase letter| ";
            if (password.Length < 8)
                errorMessage += "|be longer than 7 characters|";

            return errorMessage;
        }
    }
}