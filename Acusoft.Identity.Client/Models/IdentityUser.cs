using System;
using System.Collections.Generic;
using System.Linq;
using Acusoft.Identity.Protos;

namespace Acusoft.Identity.Client.Models
{
    public class IdentityUser
    {
        #region | Fields

        private readonly List<LoginInfo> _logins;
        private readonly List<TokenInfo> _tokens;
        private readonly List<string> _roles;

        #endregion

        #region | Properties

        public Guid Id { get; internal set; }

        public string UserName { get; set; }
        public string NormalizedUserName { get; internal set; }
        public string NormalizedEmail { get; set; }

        public string Email { get; set; }
        public DateTimeOffset? EmailConfirmationTime { get; set; }

        public string PasswordHash { get; internal set; }
        public bool TwoFactorEnabled { get; internal set; }
        public string SecurityStamp { get; internal set; }

        public LockoutInfo Lockout { get; internal set; }

        public PhoneInfo Phone { get; internal set; }

        public IEnumerable<LoginInfo> Logins
        {
            get => _logins;
            internal set
            {
                if (value != null)
                    _logins.AddRange(value);
            }
        }

        public IEnumerable<TokenInfo> Tokens
        {
            get => _tokens;
            internal set
            {
                if (value != null)
                    _tokens.AddRange(value);
            }
        }

        public IEnumerable<string> Roles
        {
            get => _roles;
            internal set
            {
                if (value != null)
                    _roles.AddRange(value);
            }
        }

        public bool EmailConfirmed => EmailConfirmationTime.HasValue;

        public Protos.IdentityUser ToGrpcUser()
        {
            return new Protos.IdentityUser
            {
                Id = Id.ToString()
            };
        }

        #endregion

        #region | Constructors

        public IdentityUser()
        {
            _logins = new List<LoginInfo>();
            _tokens = new List<TokenInfo>();
            _roles = new List<string>();
        }

        public IdentityUser(Guid id)
            : this()
        {
            Id = id;
        }

        public IdentityUser(Protos.IdentityUser user)
        {
            Id = Guid.Parse(user.Id);
        }

        #endregion

        #region | Internal Methods

        internal void CleanUp()
        {
            if (Lockout != null && Lockout.AllPropertiesAreSetToDefaults)
                Lockout = null;

            if (Phone != null && Phone.AllPropertiesAreSetToDefaults)
                Phone = null;
        }

        internal void AddLogin(LoginInfo login)
        {
            if (login == null)
                throw new ArgumentNullException(nameof(login));

            if (_logins.Any(l => l.LoginProvider == login.LoginProvider && l.ProviderKey == login.ProviderKey))
                throw new InvalidOperationException($"Login with LoginProvider: '{login.LoginProvider}' and ProviderKey: {login.ProviderKey} already exists.");

            _logins.Add(login);
        }

        internal void RemoveLogin(string loginProvider, string providerKey)
        {
            var loginToRemove = _logins.FirstOrDefault(l =>
                l.LoginProvider == loginProvider &&
                l.ProviderKey == providerKey
            );

            if (loginToRemove == null)
                return;

            _logins.Remove(loginToRemove);
        }

        internal void AddToken(TokenInfo token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (_tokens.Any(x => x.LoginProvider == token.LoginProvider && x.Name == token.Name))
                throw new InvalidOperationException($"Token with LoginProvider: '{token.LoginProvider}' and Name: {token.Name} already exists.");

            _tokens.Add(token);
        }

        internal void RemoveToken(string loginProvider, string name)
        {
            var tokenToRemove = _tokens.FirstOrDefault(l =>
                l.LoginProvider == loginProvider &&
                l.Name == name
            );

            if (tokenToRemove == null)
                return;

            _tokens.Remove(tokenToRemove);
        }

        internal void AddRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                throw new ArgumentNullException(nameof(role));

            if (!_roles.Contains(role))
                _roles.Add(role);
        }

        internal void RemoveRole(string role)
        {
            if (string.IsNullOrEmpty(role))
                throw new ArgumentNullException(nameof(role));

            if (_roles.Contains(role))
                _roles.Remove(role);
        }

        #endregion
    }
}
