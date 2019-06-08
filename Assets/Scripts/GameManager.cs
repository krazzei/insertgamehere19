public static class GameManager
{
    private static Ship _ship;

    public static void SetShip(Ship ship)
    {
        _ship = ship;
    }

    public static event System.Action OnShipDeath;

    public static void FireShipDeath()
    {
        OnShipDeath?.Invoke();
    }
}
