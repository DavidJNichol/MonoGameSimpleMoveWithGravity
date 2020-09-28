using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGameSimpleMoveWithGravity
{
    public class Character
    {

        public Texture2D texture;

        public Vector2 location;

        public Vector2 direction;

        public float speed;

        public Vector2 gravityDirection;

        public float gravityAccel;

        public Character()
        {

        }


    }
}
