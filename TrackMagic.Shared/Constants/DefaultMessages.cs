namespace TrackMagic.Shared.Constants
{
    public static class DefaultMessages
    {
        // Error handling messages.
        public static string ValidationExceptionTitle = "One or more validation errors occurred";
        public static string ValidationExceptionMessage = "The request contains invalid parameters. See errors for more information.";
        public static string NonValidationExceptionTitle = "One or more errors occurred";
        public static string SupportMessage = "If this problem persists, please contact me on GitHub.";

        // Request messages.
        public static string RequestReceivedMessage(string requestType) => $"Request of type {requestType} received.";
    }
}
