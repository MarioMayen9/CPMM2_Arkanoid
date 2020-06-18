namespace ProyectoFinal_Arkanoid
{
    public class DatosJuego
    {
        public static bool juegoIniciado = false;
        public static double ticksCount = 0;
        public static int dirX = 7, dirY = -dirX, vidas = 3, score = 0;

        public static void inicializarJuego()
        {
            juegoIniciado = false;
            ticksCount = 0;
            vidas = 3;
            score = 0;
        }
    }
}