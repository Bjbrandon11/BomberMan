using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BomberMan
{
    public class Tile
    {
        public static Dictionary<String,Texture2D> TextureList;
        public static void LoadContent()
        {
            TextureList = new Dictionary<String, Texture2D>();
            ContentManager Content =GameHolder.game.Content;
            /*string[] textNames = Directory.GetFiles(@Content.RootDirectory+"/Textures/Tiles");
            foreach (String name in textNames)
                TextureList.Add(name,Content.Load<Texture2D>(name));*/
            TextureList.Add("Block_Invin", Content.Load<Texture2D>(@"Textures/Tiles/Block_Invin"));
            TextureList.Add("break", Content.Load<Texture2D>(@"Textures/Tiles/Block_Break"));
            TextureList.Add("Explosion-sheet", Content.Load<Texture2D>(@"Textures/Sprites/Bombs/Explosion-sheet"));
            TextureList.Add("Man", Content.Load<Texture2D>(@"Textures/Sprites/Man/BomberMan"));
            TextureList.Add("EXP_END_1", Content.Load<Texture2D>(@"Textures/Tiles/Explosions/Exp1"));
            TextureList.Add("EXP_END_2", Content.Load<Texture2D>(@"Textures/Tiles/Explosions/Exp3"));
            TextureList.Add("EXP_END_3", Content.Load<Texture2D>(@"Textures/Tiles/Explosions/Exp5"));
            TextureList.Add("EXP_END_4", Content.Load<Texture2D>(@"Textures/Tiles/Explosions/Exp6"));
            TextureList.Add("EXP_CONNECT_1", Content.Load<Texture2D>(@"Textures/Tiles/Explosions/Exp2"));
            TextureList.Add("EXP_CONNECT_2", Content.Load<Texture2D>(@"Textures/Tiles/Explosions/Exp4"));
            TextureList.Add("EXP_CENTER", Content.Load<Texture2D>(@"Textures/Tiles/Explosions/Exp7"));

            TextureList.Add("Walk_Down", Content.Load<Texture2D>(@"Textures/Sprites/Man/Walk_Down"));
            TextureList.Add("Walk_Side", Content.Load<Texture2D>(@"Textures/Sprites/Man/Walk_Side"));
            TextureList.Add("Walk_Up", Content.Load<Texture2D>(@"Textures/Sprites/Man/Walk_Up"));

        }
        public readonly Rectangle sourceRec;
        public readonly Texture2D texture;
        public Tile(int x,int y,int width,int height,Texture2D text)
        {
            sourceRec = new Rectangle(x, y, width, height);
            texture = text;
        }
        public bool Equals(Tile other) { return texture.Equals(other.texture); }
        public void Draw(Rectangle rect){this.Draw(rect,Color.White,false);}
        public void Draw(Rectangle rect,Color color) { this.Draw(rect, color, false); }
        public void Draw(Rectangle rect,Color color,bool Flipped)
        {
            if(!Flipped)
                GameHolder.spritebatch.Draw(texture, rect,sourceRec, color);
            else
                GameHolder.spritebatch.Draw(texture, rect, sourceRec, color, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
        }
    }
}
