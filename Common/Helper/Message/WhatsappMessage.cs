using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Common.Helper
{
    public static class WhatsappMessage
    {
        public static bool Send(string toNumber, string messageBody)
        {
            try
            {
                string accountSid = MessageConfigurations.AccountSid;
                string authToken = MessageConfigurations.AuthToken;

                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: messageBody,
                    from: new Twilio.Types.PhoneNumber("whatsapp:+"+ MessageConfigurations.FromNumber),
                    to: new Twilio.Types.PhoneNumber("whatsapp:+" + toNumber)
                );
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
