using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Point = System.Drawing.Point;
using Random = System.Random;
using Size = System.Drawing.Size;

namespace Location
{
    public interface IMap
    {
        int Width { get; }
        int Height { get; }
        Point[][] Paths { get; }
        Color32 GetCellColor(int x, int y);
    }

    public class Map : IMap
    {
        private readonly Color32[,] _cells;

        public int Width { get; }
        public int Height { get; }
        public Point[][] Paths { get; }

        public Map(Color32[,] cells, Point[][] paths)
        {
            Width = cells.GetLength(0);
            Height = cells.GetLength(1);
            _cells = cells;
            Paths = paths;
        }

        public Color32 GetCellColor(int x, int y)
        {
            return _cells[x, y];
        }
    }

    public class MapBuilder
    {
        private const int MIN_DIMENSION = 3;
        private const int MAX_DIMENSION = 30;
        private readonly List<Color32> _pallette = new List<Color32>();
        private readonly Size _size;

        private int _seed;

        public MapBuilder(int width, int height)
        {
            _size = new Size(width, height);
        }

        public MapBuilder AddColors(params Color32[] colors)
        {
            _pallette.AddRange(colors.Where(c => !_pallette.Contains(c)));
            return this;
        }

        public MapBuilder SetSeed(int seed)
        {
            _seed = seed;
            return this;
        }

        public IMap AttemptBuild()
        {
            var rnd = new System.Random(_seed);

            var width = Math.Max(MIN_DIMENSION, Math.Min(MAX_DIMENSION, _size.Width));
            var height = Math.Max(MIN_DIMENSION, Math.Min(MAX_DIMENSION, _size.Height));
            var cells = new Color32[width, height];
            // todo generate colors

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    cells[i, j] = Color.green;
                }
            }

            var paths = new List<Point[]>
            {
                CreatePath(rnd, width, height)
            };

            return new Map(cells, paths.ToArray());
        }

        private static Point[] CreatePath(Random rnd, int width, int height)
        {
            var (start, end) = CreateStartAndEnd(rnd, width, height);

            // todo add curves
            var result = new List<Point>
            {
                start
            };

            if (start.X != end.X && start.Y != end.Y)
            {
                if (rnd.NextDouble() > 0.5)
                {
                    var midX = (start.X + end.X) / 2;
                    result.Add(new Point(midX, start.Y));
                    result.Add(new Point(midX, end.Y));
                }
                else
                {
                    var midY = (start.Y + end.Y) / 2;
                    result.Add(new Point(start.X, midY));
                    result.Add(new Point(end.X, midY));
                }
            }

            result.Add(end);
            return result.ToArray();
        }

        private static (Point start, Point end) CreateStartAndEnd(Random rnd, int width, int height)
        {
            var h2 = height / 2;
            var w2 = width / 2;
            var h = height - 1;
            var w = width - 1;
            var startValues = new[]
            {
                // start variants: north, west, center
                new Point(w2, 0), new Point(0, h2), new Point(w2, h2)
            };

            var endValues = new[]
            {
                // end variants: south, west, east, center
                new Point(w2, h), new Point(0, h2), new Point(w, h2), new Point(w2, h2)
            };


            var startProbabilities = new[] 
            {
                // start variants: north, west, center
                0.6, 0.3, 0.1
            };

            var endProbabilities = new[]
            {
                // if start: north
                // probability for end: south, west, east, center
                new[] { 0.50, 0.10, 0.20, 0.20 },

                // if start: west
                // probability for end: south, west, east, center
                new[] { 0.30, 0, 0.60, 0.10 },

                // if start: center
                // probability for end: south, west, east, center
                new[] { 0.50, 0.10, 0.40, 0 }
            };

            var rndStart = rnd.NextDouble();
            var rndEnd = rnd.NextDouble();

            var startIndex = 0;
            for (int i = 0; i < startProbabilities.Length; i++)
            {
                if (rndStart < startProbabilities[i])
                {
                    startIndex = i;
                    break;
                }

                rndStart -= startProbabilities[i];
            }

            var endIndex = 0;
            for (int i = 0; i < endProbabilities[startIndex].Length; i++)
            {
                if (rndEnd < endProbabilities[startIndex][i])
                {
                    endIndex = i;
                    break;
                }

                rndEnd -= endProbabilities[startIndex][i];
            }

            return (startValues[startIndex], endValues[endIndex]);
        }
    }
}

