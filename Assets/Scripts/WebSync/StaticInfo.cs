public static class StaticInfo
    // Saving data between scenes
{

    private static string token = "";
    private static string username = "";
    private static int coin;

    public static string Token
    {
        get
        {
            return token;
        }
        set
        {
            token = value;
        }
    }

    public static string Username
    {
        get
        {
            return username;
        }
        set
        {
            username = value;
        }
    }

    public static int Coin
    {
        get
        {
            return coin;
        }
        set
        {
            coin = value;
        }
    }
}