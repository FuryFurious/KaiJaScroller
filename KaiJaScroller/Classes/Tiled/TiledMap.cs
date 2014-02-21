using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TiledMap
{
    public class TiledMapInfo
    {
        String path;
        String tileSetName;

        int tileWidth;
        int tileHeight;

        int numTilesX;
        int numTilesY;


        int[, ,] tileIds;

        List<String> layers = new List<String>();
        public List<TiledRectangle> rectangles = new List<TiledRectangle>();
        public List<TiledPicture> pictures = new List<TiledPicture>();

        Stack<String> lastElement = new Stack<String>();
        int numLayers = -1;

        //TODO: add possibility to read objects such as images

        private TiledMapInfo()
        {
        }
        /// <summary>
        /// Creates a TiledMap data from tmx data given by the path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>The return has still some rawData!</returns>
        public static TiledMapInfo getMap(String path)
        {
            TiledMapInfo map = new TiledMapInfo();
            XmlReader reader = XmlReader.Create(path);

            map.path = path;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                        break;

                    case XmlNodeType.CDATA:
                        break;

                    case XmlNodeType.Whitespace:
                        break;

                    case XmlNodeType.Comment:
                        break;


                    case XmlNodeType.Element:
                        #region EmptyElement
                        if (reader.IsEmptyElement) //rectangles here:
                        {
                            if (reader.HasAttributes)
                            {
                                while (reader.MoveToNextAttribute())
                                {
                                    if (reader.Value.Equals("Collision"))
                                    {
                                        TiledRectangle currentRect = new TiledRectangle();
                                        currentRect.type = reader.Value;

                                        reader.MoveToNextAttribute();
                                        currentRect.x = float.Parse(reader.Value);

                                        reader.MoveToNextAttribute();
                                        currentRect.y = float.Parse(reader.Value);

                                        reader.MoveToNextAttribute();
                                        currentRect.width = float.Parse(reader.Value);

                                        reader.MoveToNextAttribute();
                                        currentRect.height = float.Parse(reader.Value);

                                        map.rectangles.Add(currentRect);
                                    }

                                    else if (reader.Value.Equals("Event"))
                                    {
                                        TiledRectangle currentRect = new TiledRectangle();
                                        currentRect.type = reader.Value;

                                        reader.MoveToNextAttribute();
                                        currentRect.x = float.Parse(reader.Value);

                                        reader.MoveToNextAttribute();
                                        currentRect.y = float.Parse(reader.Value);

                                        reader.MoveToNextAttribute();
                                        currentRect.width = float.Parse(reader.Value);

                                        reader.MoveToNextAttribute();
                                        currentRect.height = float.Parse(reader.Value);

                                        map.rectangles.Add(currentRect);
                                    }

                                    else if (reader.Value.Equals("Picture"))
                                    {
                                        TiledPicture currentPic = new TiledPicture();
                                        currentPic.type = reader.Value;

                                        reader.MoveToNextAttribute();
                                        currentPic.id = int.Parse(reader.Value);

                                        reader.MoveToNextAttribute();
                                        currentPic.x = float.Parse(reader.Value);

                                        reader.MoveToNextAttribute();
                                        currentPic.y = float.Parse(reader.Value);

                                       



                                        map.pictures.Add(currentPic);
                                    }

                                    //else if() Other objects here!
                                }
                            }

                        }
                        #endregion

                        #region NotEmptyElement
                        else //everything else here
                        {
                            map.lastElement.Push(reader.Name);

                            // prüfen, ob der Knoten Attribute hat
                            if (reader.HasAttributes)
                            {
                                // Durch die Attribute navigieren
                                while (reader.MoveToNextAttribute())
                                {
                                    if (map.lastElement.Peek().Equals("map"))
                                    {
                                        if (reader.Name.Equals("width"))
                                        {
                                            map.numTilesX = int.Parse(reader.Value);
                                        }

                                        else if (reader.Name.Equals("height"))
                                        {
                                            map.numTilesY = int.Parse(reader.Value);
                                        }

                                        else if (reader.Name.Equals("tilewidth"))
                                        {
                                            map.tileWidth = int.Parse(reader.Value);
                                        }

                                        else if (reader.Name.Equals("tileheight"))
                                        {
                                            map.tileHeight = int.Parse(reader.Value);
                                        }
                                    }

                                    else if (map.lastElement.Peek().Equals("tileset"))
                                    {
                                        if (reader.Name.Equals("name"))
                                        {
                                            map.tileSetName = reader.Value;
                                        }

                                    }

                                    else if (map.lastElement.Peek().Equals("layer"))
                                    {
                                        if (reader.Name.Equals("name"))
                                        {
                                            map.numLayers++;

                                            map.layers.Add("");
                                        }

                                    }

                                }
                            }
                        }
                        #endregion
                        break;


                    case XmlNodeType.EndElement:
                        map.lastElement.Pop();
                        break;

                    case XmlNodeType.Text:

                        map.layers[map.numLayers] = reader.Value;

                        break;


                }
            }

        //    map.convertTilesToIntArray();
            map.tileIds = map.convertTilesToIntArray();

            foreach (TiledPicture pic in map.pictures)
                Console.WriteLine(pic.ToString());

            return map;
        }

        public void printAllData()
        {
            Console.WriteLine(path);
            Console.WriteLine();

            Console.WriteLine("TileSet: " + tileSetName);
            Console.WriteLine();

            Console.WriteLine("TileWidth: " + this.tileWidth);
            Console.WriteLine("TileHeight: " + this.tileHeight);

            Console.WriteLine("NumTilesX: " + this.numTilesX);
            Console.WriteLine("NumTilesY: " + this.numTilesY);

            Console.WriteLine();

            Console.WriteLine("NumLayers: " + (numLayers + 1));

            foreach (String l in layers)
            {
                Console.WriteLine(l);
                Console.WriteLine();
            }

            foreach (TiledRectangle frect in rectangles)
            {
                Console.WriteLine(frect.ToString());
            }
            
        }

        private int[,,] convertTilesToIntArray()
        {
            String[, ,] tileIdS = new String[numTilesY, numTilesX, numLayers + 1];

            for(int z = 0; z < tileIdS.GetLength(2); z++)
            {
                String layer = layers.ElementAt(z);
                layer = layer.TrimStart('\n');
                layer = layer.TrimEnd('\n');

                String[] rows = layer.Split('\n');

                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i] = rows[i].TrimEnd(',');
                    String[] cols = rows[i].Split(',');

                    for(int x = 0;  x < cols.Length; x++)
                        tileIdS[i,x,z] = cols[x];
                }
            }

            int[,,] intIds = new int[tileIdS.GetLength(0), tileIdS.GetLength(1), tileIdS.GetLength(2)];

            for (int zi = 0; zi < tileIdS.GetLength(2); zi++)
                for (int xi = 0; xi < tileIdS.GetLength(0); xi++)
                    for (int yi = 0; yi < tileIdS.GetLength(1); yi++)
                        intIds[xi, yi, zi] = int.Parse(tileIdS[xi,yi,zi]);


            return intIds;
        }

        public String getTileSetName()
        {
            return tileSetName;
        }

        public int getTileHeight()
        {
            return tileHeight;
        }

        public int getTileWidth()
        {
            return tileWidth;
        }

        public int[,,] getTileIds()
        {
            return tileIds;
        }


    }
}