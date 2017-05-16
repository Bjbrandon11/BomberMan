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
        Timer invul;
        //readonly is a keyword that allows the variable to be assigned once then not changed
        readonly PlayerIndex playerNum;//the number of which controller the player has
        GamePadState oldGPS;//what the state of the game pad was last frame
        KeyboardState oldKb;//what the state of the keyboard was last frame
        const float deadZoneAmount = 0.15f;

        Tile text; //will become animation later
        bool bombPlaced;
        const float MAX_SPEED = 35.0f;
        readonly Color playerColor;

        public Player(Point location) : base( location)
        {
            bombPlaced = false;
            usingKeyboard = true;
            text = new Tile(0, 0, 32, 32, Tile.TextureList["Man"]);
            hitBox = new Rectangle((int)(location.X-(16)*Game1.scaleFrom32),(int)(location.Y-(16)*Game1.scaleFrom32),(int)(Game1.scaleFrom32*32), (int)(Game1.scaleFrom32 * 32));
            oldGPS = GamePad.GetState(playerNum);
            oldKb = Keyboard.GetState();
            lives = INITIAL_LIVES;
            invul = new Timer(1.5);
            invul.Play();
            
        }
        public Player(PlayerIndex number, Point location) : this(location)
        {
            usingKeyboard = false;
            playerNum = number;
            switch (number)
            {
                case PlayerIndex.One:
                    playerColor = Color.HotPink;
                    break;
                case PlayerIndex.Two:
                    playerColor = Color.Yellow;
                    break;
                case PlayerIndex.Three:
                    playerColor = Color.LawnGreen;
                    break;
                case PlayerIndex.Four:
                    playerColor = Color.Aqua;
                    break;
            }
            
        }



        public void placeBomb()
        {
            int bottom = this.hitBox.Bottom;
            int center = this.hitBox.Center.X;
            bombPlaced = true;
            //this.gameBoard.receiveBomb(new Bomb(), bottom, center);
            Game1.EntityList.Add(new Bomb(hitBox.Center, new Timer(.75), 2, playerColor));
        }

        public override void Update()
        {
            GamePadState gps = GamePad.GetState(playerNum);
            KeyboardState kb = Keyboard.GetState();
            invul.Update();
            //TODO: write update logic
            if(usingKeyboard)
            {
                if (kb.IsKeyDown(Keys.Space) && oldKb.IsKeyUp(Keys.Space))
                    placeBomb();
                if (kb.IsKeyDown(Keys.Left))
                    move(-2,0);
                if (kb.IsKeyDown(Keys.Right))
                    move(2, 0);
                if (kb.IsKeyDown(Keys.Up))
                    move(0, -2);
                if (kb.IsKeyDown(Keys.Down))
                    move(0 , 2);
                if (GameHolder.level.Intersects(hitBox))
                    Console.WriteLine("STUCK");
            }
            else
            {
                if (gps.IsButtonDown(Buttons.A) && oldGPS.IsButtonUp(Buttons.A))
                    placeBomb();
                if (Math.Abs(gps.ThumbSticks.Left.X)>deadZoneAmount||Math.Abs(gps.ThumbSticks.Left.Y)>deadZoneAmount)
                {

                    move((int)(gps.ThumbSticks.Left.X * 2), 0);
                    move(0, (int)(gps.ThumbSticks.Left.Y * -2));
                }
                
                                
            }
            if (IsHit() && invul.isDone())
            {
                lives--;
                invul.Reset();
                Console.WriteLine("ouch");
            }
            //End of update logic
            oldGPS = gps;
            oldKb = kb;
        }
        public void move(int x,int y)
        {
           
            Rectangle futureRect = new Rectangle(hitBox.X+x, hitBox.Y+y, hitBox.Width, hitBox.Height);
            while(futureRect.X!=hitBox.X && futureRect.Y!=hitBox.Y)
            {
                if (!GameHolder.level.Intersects(futureRect))
                    break;
                if (futureRect.X != hitBox.X)
                {
                    futureRect = new Rectangle(futureRect.X + (x-(x-1)), hitBox.Y , hitBox.Width, hitBox.Height);
                }
                if (futureRect.X != hitBox.X)
                {
                    futureRect = new Rectangle(futureRect.X , hitBox.Y + (y - (y - 1)), hitBox.Width, hitBox.Height);
                }
                
            }
            if (!GameHolder.level.Intersects(futureRect))
                hitBox = futureRect;

        }
        public bool CheckIfAllDead(Rectangle explodeRect)
        {
            return lives == 0;
        }
        public bool IsHit()
        {
            Block[,] lvl=GameHolder.level.tiles;
            foreach(Block a in lvl)
            {
                if (a.hitBox.Intersects(hitBox) && a.currentState == BlockState.Explosion)
                    return true;
            }
            return false;
        }
        public override void Draw(SpriteBatch spritebatch)
        {
            text.Draw(hitBox,playerColor);
        }

    }
}
