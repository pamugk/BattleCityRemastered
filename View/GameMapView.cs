using GamePoint = Model.Basics.Point;
using Model.Entities.Surfaces;
using Model.Map;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Size = System.Drawing.Size;
using System.Windows.Media.Imaging;
using Model.Entities;

namespace View
{
    public static class GameMapView
    {
        private static Dictionary<Surface.Kinds, int> SurfaceTacts = new Dictionary<Surface.Kinds, int>
        {
            { Surface.Kinds.Stone, 1 },
            { Surface.Kinds.Water, 3 },
            { Surface.Kinds.Ice, 1 },
            { Surface.Kinds.Sand, 1 },
            { Surface.Kinds.Lava, 3 }
        };

        private static readonly Size frameSize = new Size(208, 208);

        private static Bitmap GetEntityImage(this Entity entity) =>
            (Bitmap)Properties.Resources.ResourceManager.GetObject(entity.GetDescription());

        private static Point Convert(this GamePoint point) => new Point(point.X, point.Y);

        public static BitmapSource DrawMap(Map gameMap)
        {
            var frame = new Bitmap(frameSize.Width, frameSize.Height);
            var frameGraphics = Graphics.FromImage(frame);

            for (int i = 0; i < gameMap.SurfaceLayer.GetLength(0); i++)
                for (int j = 0; j < gameMap.SurfaceLayer.GetLength(0); j++)
                    frameGraphics.DrawImage(gameMap.SurfaceLayer[i, j].GetEntityImage(), 
                                                gameMap.SurfaceLayer[i, j].Coordinates.Convert());

            frameGraphics.DrawImage(gameMap.hq.GetEntityImage(), gameMap.hq.Coordinates.Convert());

            return frame.ToImageSource();
        }

        public static Dictionary<Surface.Kinds, List<BitmapSource>> EnumerateSurfaceAnimations() =>
            Enum.GetNames(typeof(Surface.Kinds))
                .Select(surfaceName => (Surface.Kinds)Enum.Parse(typeof(Surface.Kinds), surfaceName))
                .ToDictionary(
                    surface => surface,
                    surface =>
                        Enumerable.Range(0, SurfaceTacts[surface])
                            .Select(tact => ((Bitmap)Properties.Resources.ResourceManager.GetObject($"Big{surface}_{tact}")).ToImageSource())
                            .ToList());
    }
}
