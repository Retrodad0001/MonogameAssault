using MonogameAssault;

//TODO Imgui add frame time per update VS drawing
//TODO F12 enable imgui https://github.com/Mezo-hx/MonoGame.ImGuiNet
//TODO Deferred rendering or foreward?

internal static class Program
{
#pragma warning disable IDE0060 // Remove unused parameter
    public static void Main(string[] args)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        using AssaultGame game = new();
        game.Run();
    }
}
