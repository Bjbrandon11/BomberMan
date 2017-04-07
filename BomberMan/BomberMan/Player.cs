using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BomberMan
{
    class Player
    {
        public PlayerIndex PlayerNum;//This is which gamePad the player is using
        GamePadState oldGPState;//This is the state of the gamePad for the previous frame.
        Vector2 position;

        public Player(PlayerIndex index, Vector2 position)
        {
            PlayerNum = index;
            GamePadState = GamePad.GetState(PlayerNum);
            this.position = position;
        }

    }
}
