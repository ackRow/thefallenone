public static class StaticInfo
    // Saving data between scenes
{

    private static string token = "";
    private static string username = "";
    private static int coin;
    private static int level;

    public enum Stat
    {
        win,
        loose,
        kill,
        death,
        level
    }

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

    public static int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }
}