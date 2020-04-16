using System;

namespace Acusoft.Identity.Client.Models
{
    public class PhoneInfo
    {
        public string Number { get; internal set; }
        public DateTimeOffset? ConfirmationTime { get; internal set; }
        public bool IsConfirmed => ConfirmationTime != null;

        public static implicit operator PhoneInfo(string input)
            => new PhoneInfo { Number = input };

        public bool AllPropertiesAreSetToDefaults =>
            Number == null &&
            ConfirmationTime == null;
    }
}
