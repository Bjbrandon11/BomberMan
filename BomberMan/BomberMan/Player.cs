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
        const int INITIAL_LIVES = 7;
        bool usingKeyboard;
        public Timer invul;
        public double bSeconds;
        //readonly is a keyword that allows the variable to be assigned once then not changed
        readonly PlayerIndex playerNum;//the number of which controller the player has
        GamePadState oldGPS;//what the state of the game pad was last frame
        KeyboardState oldKb;//what the state of the keyboard was last frame
        const float deadZoneAmount = 0.15f;
        public enum PlayerAnimation { WalkUp,WalkDown,WalkLeft,WalkRight}
        PlayerAnimation cAnim;
        Animation anim;
        bool bombPlaced;
        int maxBombs;
        int currentBombs;
        Timer bombTimer;
        double size;
        double speed;
        Color playerColor;

        public Player(Point location) : base( location)
        {
            bombPlaced = false;
            playerColor = Color.White;
            usingKeyboard = true;
            cAnim = PlayerAnimation.WalkRight;
            size = .85;
            anim = Animation.Walk_Side;
            hitBox = new Rectangle((int)(location.X-(16)*Game1.scaleFrom32*size),(int)(location.Y-(16)*Game1.scaleFrom32 * size),(int)(Game1.scaleFrom32*20 * size), (int)(Game1.scaleFrom32 * 32 * size));
            oldGPS = GamePad.GetState(playerNum);
            oldKb = Keyboard.GetState();
            lives = INITIAL_LIVES;
            invul = new Timer(1.5);
            bSeconds = 2.75;
            maxBombs = 2;
            currentBombs = 1;
            bombTimer = new Timer(bSeconds);
            speed = 3.8;
            invul.Play();
            bombTimer.Play();
            
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
            if (currentBombs>0)
            {
                bombPlaced = true;
                currentBombs--;
                //this.gameBoard.receiveBomb(new Bomb(), bottom, center);
                Game1.EntityList.Add(new Bomb(hitBox.Center, new Timer(bSeconds), 2, playerColor));
            }
        }

        public override void Update()
        {
            GamePadState gps = GamePad.GetState(playerNum);
            KeyboardState kb = Keyboard.GetState();
            invul.Update();
            bombTimer.Update();
            anim.Update();
            //TODO: write update logic
            if(usingKeyboard)
            {
                if (kb.IsKeyDown(Keys.Space) && oldKb.IsKeyUp(Keys.Space))
                    placeBomb();
                if (kb.IsKeyDown(Keys.Left))
                    move(-speed,0);
                if (kb.IsKeyDown(Keys.Right))
                    move(speed, 0);
                if (kb.IsKeyDown(Keys.Up))
                    move(0, -speed);
                if (kb.IsKeyDown(Keys.Down))
                    move(0 , speed);
                if (GameHolder.level.Intersects(hitBox))
                    Console.WriteLine("STUCK");
            }
            else
            {
                if (gps.IsButtonDown(Buttons.A) && oldGPS.IsButtonUp(Buttons.A))
                    placeBomb();
                if (Math.Abs(gps.ThumbSticks.Left.X)>deadZoneAmount||Math.Abs(gps.ThumbSticks.Left.Y)>deadZoneAmount)
                {
                    double x = (gps.ThumbSticks.Left.X );
                    double y = (gps.ThumbSticks.Left.Y );
                    move((int)(gps.ThumbSticks.Left.X * speed), 0);
                    move(0, (int)(gps.ThumbSticks.Left.Y * -speed));
                    //move((int)x*2,(int)y*-2);
                    anim.pState = Animation.AnimPlayState.Play;
                        
                    if (Math.Abs(x) >= Math.Abs(y))
                    {
                        if (x > 0 && (cAnim != PlayerAnimation.WalkRight))
                        {
                            cAnim = PlayerAnimation.WalkRight;
                            anim = Animation.Walk_Side.Clone();
                        }
                        else if (x < 0 && cAnim != PlayerAnimation.WalkLeft)
                        {
                            cAnim = PlayerAnimation.WalkLeft;
                            anim = Animation.Walk_Side.Clone();
                            anim.Flipped = true;
                        }

                    }
                    else
                    {
                        if (y < 0.0 && cAnim != PlayerAnimation.WalkDown)
                        {
                            cAnim = PlayerAnimation.WalkDown;
                            anim = Animation.Walk_Down.Clone();
                        }
                        else if (y > 0.0 && cAnim != PlayerAnimation.WalkUp)
                        {
                            cAnim = PlayerAnimation.WalkUp;
                            anim = Animation.Walk_Up.Clone();
                        }

                    }
                }
                else
                    anim.PRestart();

            }
            if(bombTimer.isDone() && currentBombs<maxBombs)
            {
                currentBombs++;
                bombTimer.Reset();
            }
            if (IsHit() && invul.isDone())
            {
                lives--;
                invul.Reset();
                Console.WriteLine("ouch");
            }
            if(!invul.isDone())
            {
                if (invul.getPercent() * 100 % 6 > 3)
                    playerColor.A = 125;
                else
                    playerColor.A = 200;
            }
            else if(playerColor.A!=255)
                playerColor.A = 255;
            //End of update logic
            oldGPS = gps;
            oldKb = kb;
        }
        public void move(int x,int y)
        {
            move((double)x,(double)y);
        }
        public void move(double x, double y)
        {
            Rectangle futureRect = new Rectangle((int)Math.Round(hitBox.X + x), (int)Math.Round(hitBox.Y + y), hitBox.Width, hitBox.Height);
            while (futureRect.X != hitBox.X && futureRect.Y != hitBox.Y)
            {
                if (!GameHolder.level.Intersects(futureRect))
                    break;
                if (futureRect.X != hitBox.X)
                {
                    futureRect = new Rectangle((int)Math.Round(futureRect.X + (x - (x - 1))), hitBox.Y, hitBox.Width, hitBox.Height);
                }
                if (futureRect.Y != hitBox.Y)
                {
                    futureRect = new Rectangle(futureRect.X, (int)Math.Round(hitBox.Y + (y - (y - 1))), hitBox.Width, hitBox.Height);
                }

            }
            if (!GameHolder.level.Intersects(futureRect))
                hitBox = futureRect;
        }
        public bool CheckIfAllDead()
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
                anim.Draw(hitBox, playerColor);
        }

    }
}
