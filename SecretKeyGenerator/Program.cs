using System;
using System.Security.Cryptography;

public partial class Program
{
    public static void Main(string[] args)
    {
        var secretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
        Console.WriteLine(secretKey);
        GenerateAndStoreSecretKey();
    }

    private static void GenerateAndStoreSecretKey()
    {
        string secretKey = SecretKeyGenerator.GenerateSecretKey();
        Console.WriteLine($"Generated Secret Key: {secretKey}");

        Environment.SetEnvironmentVariable("SECRET_KEY", secretKey, EnvironmentVariableTarget.User);
    }
}

public class SecretKeyGenerator
{
    public static string GenerateSecretKey()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] secretKey = new byte[32];
            rng.GetBytes(secretKey);
            return Convert.ToBase64String(secretKey);
        }
    }
}

