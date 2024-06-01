namespace finebe.webapi;

public static class EnvVariableHelper
{
    public static string GetByKey(string key) {
        var value = Environment.GetEnvironmentVariable(key);
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidOperationException($"{nameof(key)} [{key}] is not set in the environment variables.");
        }

        return value;
    }

}
