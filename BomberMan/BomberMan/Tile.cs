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
        public static List<Texture2D> TextureList;
        public static void LoadContent()
        {
            TextureList = new List<Texture2D>();
            ContentManager Content =GameHolder.game.Content;
            String[] textNames = Directory.GetFiles(Content.RootDirectory+"/Textures");
            foreach (String name in textNames)
                TextureList.Add(Content.Load<Texture2D>(name));
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
