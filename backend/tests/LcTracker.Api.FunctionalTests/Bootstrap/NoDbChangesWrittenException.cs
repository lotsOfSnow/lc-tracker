namespace LcTracker.Api.FunctionalTests.Bootstrap;

public class NoDbChangesWrittenException()
    : Exception("No entries were written to the database");
