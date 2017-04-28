﻿using Microsoft.Xna.Framework;
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
        private const double scale = 1.5;
        private Tile[,] tiles;
        private Dictionary<string, Texture2D> tileSheets;
        public Dictionary<int, Rectangle> TileSourceRecs;
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

        private Random random = new Random(1337);

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

            tileSheets = new Dictionary<string, Texture2D>();
            tileSheets.Add("Block_Invin", Content.Load<Texture2D>("Textures/Tiles/Block_Invin"));

            TileSourceRecs = new Dictionary<int, Rectangle>();
            for (int i = 0; i < TilesPerRow * NumRowsPerSheet; i++)
            {
                Rectangle rectTile = new Rectangle((i % TilesPerRow) * TileWidth, (i / TilesPerRow) * TileHeight, TileWidth, TileHeight);
                TileSourceRecs.Add(i, rectTile);
            }
            LoadTiles(path);
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
            tiles = new Tile[numOfTilesAcross, lines.Count];

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

        private Tile LoadTile(char _tileType, int _x, int _y)
        {
            switch (_tileType)
            {
                case '.':
                    return null;

                case 'B':
                    return new Tile(0, 0, 32, 32, Tile.TextureList["Tiles/Block_Invin"]);
                case 'N':
                    return new Tile(0, 0, 32, 32, Tile.TextureList["Tiles/Block_Invin"]);

                default:
                    throw new NotSupportedException(String.Format("Unsupported tile type character '{0}' at position {1}, {2}.", _tileType, _x, _y));

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
                        Rectangle position = new Rectangle((int)(scale*x * TileWidth),(int)(scale*y * TileHeight), (int)(scale *TileWidth), (int)(scale *TileHeight));
                        tiles[x, y].Draw(position);
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