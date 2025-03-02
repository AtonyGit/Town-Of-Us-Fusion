namespace TownOfUsFusion;

public static class ToufAssets
{
    private static DLoadImage _iCallLoadImage;
    public static LoadableResourceAsset ExampleButton { get; } = new("TownOfUsFusion.Resources.ExampleButton.png");

    // Credit to EpicHorrors for the teleport button asset.
    public static LoadableResourceAsset TeleportButton { get; } = new("TownOfUsFusion.Resources.TeleportButton.png");
    public static LoadableResourceAsset Banner { get; } = new("TownOfUsFusion.Resources.FortniteBanner.jpeg");
    
    public static LoadableResourceAsset AutopsyButton { get; } = new("TownOfUsFusion.Resources.AutopsyButton.png");
    public static LoadableResourceAsset ExamineButton { get; } = new("TownOfUsFusion.Resources.ExamineButton.png");

    public static LoadableResourceAsset CampButton { get; } = new("TownOfUsFusion.Resources.CampButton.png");
    public static LoadableResourceAsset ShootButton { get; } = new("TownOfUsFusion.Resources.Shoot.png");

    public static LoadableResourceAsset DouseButton { get; } = new("TownOfUsFusion.Resources.DouseButton.png");
    public static LoadableResourceAsset IgniteButton { get; } = new("TownOfUsFusion.Resources.IgniteButton.png");

    public static Sprite WerewolfKill = CreateSprite("TownOfUsFusion.Resources.WerewolfKill.png");
    public static Sprite WerewolfVent = CreateVentSprite("TownOfUsFusion.Resources.WerewolfVent.png");
    public static LoadableResourceAsset RampageButton { get; } = new("TownOfUsFusion.Resources.RampageButton.png");
    public static Sprite ToUBanner { get; } = CreateSprite("TownOfUsFusion.Resources.TownOfUsFusionBanner.png");
    
        public static Sprite CreateSprite(string name)
        {
            var pixelsPerUnit = 100f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }
        public static Sprite CreateMeetingSprite(string name)
        {
            var pixelsPerUnit = 300f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }
        public static Sprite CreateScaledSprite(string name)
        {
            var pixelsPerUnit = 200f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            // this allows the icons to be any size and still look good
            pixelsPerUnit = tex.width;
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }
        public static Sprite CreateVentSprite(string name)
        {
            var pixelsPerUnit = 200f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            var img = imageStream.ReadFully();
            LoadImage(tex, img, true);
            tex.DontDestroy();
            pixelsPerUnit = (int)(tex.height * 1.5);
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
        }

        public static void LoadImage(Texture2D tex, byte[] data, bool markNonReadable)
        {
            _iCallLoadImage ??= IL2CPP.ResolveICall<DLoadImage>("UnityEngine.ImageConversion::LoadImage");
            var il2CPPArray = (Il2CppStructArray<byte>) data;
            _iCallLoadImage.Invoke(tex.Pointer, il2CPPArray.Pointer, markNonReadable);
        }

        private delegate bool DLoadImage(IntPtr tex, IntPtr data, bool markNonReadable);
}
