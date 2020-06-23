using System;

namespace ProyectoFinal_Arkanoid
{
    public class EmptyNicknameException : Exception
    {
        public EmptyNicknameException(string Message) : base(Message) { }
    }
}