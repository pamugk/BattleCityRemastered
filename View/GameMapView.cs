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
        private static Dictionary<Surfaces, int> SurfaceTacts = new Dictionary<Surfaces, int>
        {
            { Surfaces.Stone, 1 },
            { Surfaces.Water, 3 },
            { Surfaces.Ice, 1 },
            { Surfaces.Sand, 1 },
            { Surfaces.Lava, 3 }
        };

        private static readonly Size frameSize = new Size(208, 208);

        private static Bitmap GetEntityImage(this Entity entity) =>
            (Bitmap)Properties.Resources.ResourceManager.GetObject(entity.GetDescription());

        private static Point Convert(this GamePoint point) => new Point(point.X, point.Y);

        public static BitmapSource DrawMap(Map gameMap)
        {
            var frame = new Bitmap(frameSize.Width, frameSize.Height);
            var frameGraphics = Graphics.FromImage(frame);
            gameMap.GetSurface().ForEach(surface => frameGraphics.DrawImage(surface.GetEntityImage(), surface.Coordinates.Convert()));
            frameGraphics.DrawImage(gameMap.hq.GetEntityImage(), gameMap.hq.Coordinates.Convert());
            return frame.ToImageSource();
        }

        public static Dictionary<Surfaces, List<BitmapSource>> EnumerateSurfaceAnimations() =>
            Enum.GetNames(typeof(Surfaces))
                .Select(surfaceName => (Surfaces)Enum.Parse(typeof(Surfaces), surfaceName))
                .ToDictionary(
                    surface => surface,
                    surface =>
                        Enumerable.Range(0, SurfaceTacts[surface])
                            .Select(tact => ((Bitmap)Properties.Resources.ResourceManager.GetObject($"Big{surface}_{tact}")).ToImageSource())
                            .ToList());
    }
}
