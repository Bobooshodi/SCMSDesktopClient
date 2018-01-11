using System.Collections.Generic;
using System.Text;

namespace SCMSClient.Models
{
    public class BadRequestError
    {
        public string Message { get; set; }
        public string MessageDetail { get; set; }
        public ModelState ModelState { get; set; }
        public string Error { get; set; }
        public string Error_description { get; set; }

        public string GetErrorMessage()
        {
            var errorMsg = new StringBuilder();

            if (Error_description != null && !string.IsNullOrEmpty(Error_description))
            {
                if (!Error_description.Contains("The request is invalid."))
                    errorMsg.AppendLine(Error_description);
            }

            if (Message != null && !string.IsNullOrEmpty(Message))
            {
                if (!Message.Contains("The request is invalid."))
                    errorMsg.AppendLine(Message);
            }

            if (MessageDetail != null && !string.IsNullOrEmpty(MessageDetail))
            {
                if (!MessageDetail.Contains("The request is invalid."))
                    errorMsg.AppendLine(MessageDetail);
            }

            if (ModelState != null && ModelState.ErrorMessage != null && ModelState.ErrorMessage.Count > 0)
            {
                foreach (var err in ModelState.ErrorMessage)
                {
                    if (!err.Contains("The request is invalid."))
                        errorMsg.AppendLine(err);
                }
            }
            return errorMsg.ToString();
        }
    }

    public class ModelState
    {
        public List<string> ErrorMessage { get; set; }
    }
}
