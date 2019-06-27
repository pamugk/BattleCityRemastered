using Model.Basics;
using Model.Entities.Buildings;
using Model.Entities.Surfaces;
using System;
using System.Collections.Generic;

namespace Model.Map
{
    [Serializable]
    public sealed class Map
    {
        #region Константы
        public const string EXT = ".brmap";

        private const int Width = 13;
        private const int Height = 13;

        private const int SurfacesPerACellSide = 2;
        private const int WallsPerACellSide = 4;
        private const int RealPointsPerACell = 16;

        private const int HQX = RealPointsPerACell * (Width - 1) / 2;
        private const int HQY = (Height - 1) * RealPointsPerACell;

        public static List<Point> CoordinatesOfEnemySpawns = new List<Point>
        {
            new Point(0, 0),
            new Point(HQX * RealPointsPerACell, 0),
            new Point((Width - 1) * RealPointsPerACell, 0)
        };

        private static List<Point> CoordinatesOfPlayerSpawns = new List<Point>
        {
            new Point(HQX - 2 * RealPointsPerACell, HQY),
            new Point(HQX + 2 * RealPointsPerACell, HQY),
            new Point(0, HQY),
            new Point((Width - 1) * RealPointsPerACell, HQY)
        };
        #endregion

        public HQ hq;
        public Surfaces[,] surfaceLayer;

        public Map()
        {
            hq = new HQ(HQX, HQY);
            surfaceLayer = new Surfaces[Width * SurfacesPerACellSide, Height * SurfacesPerACellSide];
        }

        public List<Surface> GetSurface()
        {
            var result = new List<Surface>();
            for (int i = 0; i < surfaceLayer.GetLength(0); i++)
                for (int j = 0; j < surfaceLayer.GetLength(1); j++)
                    result.Add(new Surface(i * 8, j * 8, surfaceLayer[i, j]));
            return result;
        }
    }
}