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
    class Tile
    {
        public static Dictionary<String,Texture2D> TextureList;
        public static Tile[] bombAnimation;
        public static void LoadContent()
        {
            TextureList = new Dictionary<String, Texture2D>();
            ContentManager Content =GameHolder.game.Content;
            /*string[] textNames = Directory.GetFiles(@Content.RootDirectory+"/Textures/Tiles");
            foreach (String name in textNames)
                TextureList.Add(name,Content.Load<Texture2D>(name));*/
            TextureList.Add("Tiles/Block_Invin", Content.Load<Texture2D>(@"Textures/Tiles/Block_Invin"));
            TextureList.Add("Sprites/Bombs/Explosion-sheet", Content.Load<Texture2D>(@"Textures/Sprites/Bombs/Explosion-sheet"));
            bombAnimation = new Tile[30];
            for (int i = 0; i < bombAnimation.Length; i++)
                bombAnimation[i] = new Tile(i * 16, 0, 16, 16, TextureList["Sprites/Bombs/Explosion-sheet"]);
        }
        public readonly Rectangle sourceRec;
        public readonly Texture2D texture;
        public Tile(int x,int y,int width,int height,Texture2D text)
        {
            sourceRec = new Rectangle(x, y, width, height);
            texture = text;
        }
        public void Draw(Rectangle rect){this.Draw(rect,Color.White);}
        public void Draw(Rectangle rect,Color color){GameHolder.spritebatch.Draw(texture, rect,sourceRec, color);}
    }
}
