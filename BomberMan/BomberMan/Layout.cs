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
    class Layout : IDisposable
    {
        public Block[,] tiles;
        private Block[,] locations;
        private Dictionary<string, Texture2D> tileSheets;
        public Dictionary<int, Rectangle> TileSourceRecs;
        public Dictionary<Block, Timer> Timers;
        Block[] spawns = new Block[4];
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        //private const int TileWidth = 64;
        //private const int TileHeight = 64;
        private const int TileWidth = 32;
        private const int TileHeight = 32;
        private const int TilesPerRow = 5;
        private const int NumRowsPerSheet = 5;

        private Random random = new Random();

        public int Width
        {
            get { return tiles.GetLength(0); }
        }
        public int Height
        {
            get { return tiles.GetLength(1); }
        }

        public Layout(IServiceProvider _serviceProvider, string path)
        {
            content = new ContentManager(_serviceProvider, "Content");
            Timers = new Dictionary<Block, Timer>();
            tileSheets = new Dictionary<string, Texture2D>();
            tileSheets.Add("Block_Invin", Content.Load<Texture2D>("Textures/Tiles/Block_Invin"));
            tileSheets.Add("break", Content.Load<Texture2D>("Textures/Tiles/break"));
            TileSourceRecs = new Dictionary<int, Rectangle>();
            for (int i = 0; i < TilesPerRow * NumRowsPerSheet; i++)
            {
                Rectangle rectTile = new Rectangle((i % TilesPerRow) * TileWidth, (i / TilesPerRow) * TileHeight, TileWidth, TileHeight);
                TileSourceRecs.Add(i, rectTile);
            }
            LoadTiles(path);
        }
        public bool Intersects(Rectangle rect)
        {
            foreach(Block b in tiles)
            {
                if (b!=null && (b.currentState == BlockState.Breakable || b.currentState == BlockState.Impassable))
                    if (b.hitBox.Intersects(rect))
                        return true;
            }
            return false;
        }
        private void LoadTiles(string path)
        {
            int numOfTilesAcross = 0;
            List<string> lines = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line = reader.ReadLine();
                    numOfTilesAcross = line.Length;
                    while (line != null)
                    {
                        lines.Add(line);
                        int nextLineWidth = line.Length;
                        if (nextLineWidth != numOfTilesAcross)
                            throw new Exception(String.Format("The length of line {0} is different from all preceeding lines.", lines.Count));
                        line = reader.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: ");
                Console.WriteLine(e.Message);
            }
            tiles = new Block[numOfTilesAcross, lines.Count];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; ++x)
                {
                    string currentRow = lines[y];
                    char tileType = currentRow[x];
                    tiles[x, y] = LoadTile(tileType, x, y);
                }
            }
        }
        public Block[] getSpawnBlocks() { return spawns; }
        private Block LoadTile(char _tileType, int _x, int _y)
        {
            double scale = Game1.scaleFrom32;
            switch (_tileType)
            {
                case '.':
                    return new Block((int)Math.Ceiling(_x * 32 * scale), (int)Math.Ceiling(_y * 32 * scale), BlockState.Passable); 

                case 'B':
                    return new Block((int)Math.Ceiling(_x*32*scale), (int)Math.Ceiling(_y * 32 * scale),BlockState.Impassable);
                case 'N':
                    return new Block((int)Math.Ceiling(_x * 32 * scale), (int)Math.Ceiling(_y * 32 * scale), BlockState.Breakable);
                case '1':
                    spawns[0]= new Block((int)Math.Ceiling(_x * 32 * scale), (int)Math.Ceiling(_y * 32 * scale), BlockState.Spawn);
                    return spawns[0];
                case '2':
                    spawns[1] = new Block((int)Math.Ceiling(_x * 32 * scale), (int)Math.Ceiling(_y * 32 * scale), BlockState.Spawn);
                    return spawns[1];
                case '3':
                    spawns[2] = new Block((int)Math.Ceiling(_x * 32 * scale), (int)Math.Ceiling(_y * 32 * scale), BlockState.Spawn);
                    return spawns[2];
                case '4':
                    spawns[3] = new Block((int)Math.Ceiling(_x * 32 * scale), (int)Math.Ceiling(_y * 32 * scale), BlockState.Spawn);
                    return spawns[3];
                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", _tileType, _x, _y));

            }
        }
        public void Update()
        {
            UpdateExplosions();
        }
        private void UpdateExplosions()
        {
            foreach (Block b in tiles)
            {
                if (b.currentState == BlockState.Explosion)
                    if (!Timers.ContainsKey(b))
                    {
                        Timers.Add(b, new Timer(.6));
                        Timers[b].Play();
                    }
                    else
                    {
                        Timers[b].Update();
                        if (Timers[b].isDone())
                        {
                            b.newState(new Tile(0, 0, 32, 32, Tile.TextureList["EXP_END_2"]), BlockState.Passable);
                            Timers.Remove(b);
                        }
                    }
            }
        }
        //private Tile LoadVarietyTile(string _tileSheetName, int _colorRow, int _variationCount)
        //{
        //    int index = random.Next(_variationCount);
        //    int tileSheetIndex = _colorRow + index;
        //    return new Tile(_tileSheetName, tileSheetIndex);
        //}

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawTiles(spriteBatch);
        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {
            
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    if (tiles[x, y] != null)
                    {
                        
                        tiles[x, y].Draw();
                    }
                    
                }
            }

        }
        public void Dispose()
        {
            Content.Unload();
        }
    }
}
