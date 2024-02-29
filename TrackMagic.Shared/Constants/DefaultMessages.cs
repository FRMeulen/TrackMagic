namespace TrackMagic.Shared.Constants
{
    public static class DefaultMessages
    {
        // Error handling messages.
        public static string ValidationExceptionTitle = "One or more validation errors occurred";
        public static string ValidationExceptionMessage = "The request contains invalid parameters. See errors for more information.";
        public static string NotFoundExceptionTitle = "No results found";
        public static string NonValidationExceptionTitle = "One or more errors occurred";
        public static string SupportMessage = "If this problem persists, please contact me on GitHub.";

        public static string LoggingPipelineMessage(string requestType) => $"Request of type {requestType} passed through validation.";
        public static string NotFoundExceptionMessage(string entityType) => $"No matching {entityType} found with the provided parameters.";
    }
}
