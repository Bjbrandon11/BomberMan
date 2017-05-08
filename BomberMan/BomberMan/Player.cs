using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BomberMan
{
    class Player : Entity
    {
        public int lives;
        const int INITIAL_LIVES = 5;
        bool usingKeyboard;
        Rectangle location;//where the player is on the screen
        //readonly is a keyword that allows the variable to be assigned once then not changed
        readonly PlayerIndex playerNum;//the number of which controller the player has
        GamePadState oldGPS;//what the state of the game pad was last frame
        KeyboardState oldKb;//what the state of the keyboard was last frame
        
        Tile text; //will become animation later
        bool bombPlaced;
        const float MAX_SPEED = 35.0f;
        public Player(Point location) : base( location)
        {
            bombPlaced = false;
            usingKeyboard = true;
            text = new Tile(0, 0, 32, 32, Tile.TextureList["Man"]);
            this.location = new Rectangle((int)(location.X-(16)*Game1.scaleFrom32),(int)(location.Y-(16)*Game1.scaleFrom32),(int)(Game1.scaleFrom32*32), (int)(Game1.scaleFrom32 * 32));
            oldGPS = GamePad.GetState(playerNum);
            oldKb = Keyboard.GetState();
            lives = INITIAL_LIVES;
            
        }
        public Player(PlayerIndex number, Point location) : this(location)
        {
            usingKeyboard = false;
            playerNum = number;
            
        }



        public void placeBomb()
        {
            int bottom = this.location.Bottom;
            int center = this.location.Center.X;
            bombPlaced = true;
            //this.gameBoard.receiveBomb(new Bomb(), bottom, center);
            Game1.EntityList.Add(new Bomb(location.Center, new Timer(.75), 2));
        }

        public override void Update()
        {
            GamePadState gps = GamePad.GetState(playerNum);
            KeyboardState kb = Keyboard.GetState();
            //TODO: write update logic
            if(usingKeyboard)
            {
                if (kb.IsKeyDown(Keys.Space) && oldKb.IsKeyUp(Keys.Space))
                    placeBomb();
            }
            else
            {

            }

            //End of update logic
            oldGPS = gps;
            oldKb = kb;
        }
        public bool CheckIfAllDead(Rectangle explodeRect)
        {
            return lives == 0;
        }
        public bool CheckIfDying(Rectangle explodeRect)
        {
            bool result = false;
            if (hitBox.Intersects(explodeRect))
            {
                lives--;
                result = true;
            }
            return result;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            text.Draw(location);
        }

    }
}
