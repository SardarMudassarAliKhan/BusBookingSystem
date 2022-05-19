using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Common.Helper
{
    public static class TextMessage
    {
        public static bool Send(string toNumber, string messageBody)
        {
            try
            {
                var accountSid = TextMessageConfigurations.AccountSid;
                var authToken = TextMessageConfigurations.AuthToken;
                TwilioClient.Init(accountSid, authToken);

                var messageOptions = new CreateMessageOptions(
                    new PhoneNumber("+"+toNumber));
                messageOptions.From = new PhoneNumber("+"+TextMessageConfigurations.FromNumber);
                messageOptions.Body = messageBody;
                var message = MessageResource.Create(messageOptions);
                return true;
            }
            catch (Exception ex)
            {
                return false ;
            }

        }
    }
}
