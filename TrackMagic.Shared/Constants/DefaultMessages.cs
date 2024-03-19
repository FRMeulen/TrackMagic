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
        public static string DateInFutureMessage = "Provided date is in the future.";
        public static string NoTurnZeroMessage = "No cheesy turn 0 wins allowed. Go play cEDH instead.";
        public static string FullDecklistMessage = "Full decklists need 100 cards.";

        public static string NotFoundExceptionMessage(string entityType) => $"No matching {entityType} found with the provided parameters.";
        public static string AlreadyExistsMessage(string entityType, string property, string value) => $"{entityType} with {property} {value} already exists.";
        public static string MustExistMessage(string entityType, string property, string value) => $"{entityType} with {property} {value} does not exist.";
    }
}
