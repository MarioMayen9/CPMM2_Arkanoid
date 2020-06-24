namespace ProyectoFinal_Arkanoid
{
    public class DatosJuego
    {
        public static bool GameStarted = false;
        public static double ticksCount = 0;
        public static int dirX = 7, dirY = -dirX, lifes = 3, score = 0;

        public static void InitializeGame()
        {
            GameStarted = false;
            ticksCount = 1;
            lifes = 3;
            score = 0;
        }
    }
}
