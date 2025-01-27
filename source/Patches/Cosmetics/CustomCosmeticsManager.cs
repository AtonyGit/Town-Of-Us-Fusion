using System.IO;
using System.Reflection;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Cosmetics;

public static class CustomCosmeticsManager
{
    public static Sprite CreateSprite(string name, float size = -1f)
    {
            var pixelsPerUnit = 200f;
            var pivot = new Vector2(0.5f, 0.5f);

            var assembly = Assembly.GetExecutingAssembly();
            var tex = AmongUsExtensions.CreateEmptyTexture();
            var imageStream = assembly.GetManifestResourceStream(name);
            Debug.Log("creating sprite from " + name);
            var img = imageStream.ReadFully();
            TownOfUsFusion.LoadImage(tex, img, true);
            tex.DontDestroy();
            pixelsPerUnit = tex.height;
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, size > 0f ? size : pixelsPerUnit);
            sprite.DontDestroy();
            return sprite;
    }
    private static Texture2D EmptyTexture() => new(2, 2, TextureFormat.ARGB32, true);
    public static Texture2D LoadTexture(byte[] data, string name)
    {
        var texture = EmptyTexture();
        Debug.Log("LOADING TEXTURE2D FROM: " + name);

        if (texture.LoadImage(data))
        {
            texture.name = name;
            return texture.DontDestroy();
        }

        return null;
    }

    public static Texture2D LoadDiskTexture(string path) => LoadTexture(File.ReadAllBytes(path), path.SanitisePath());
    public static Sprite CreateCosmeticSprite(string path, CosmeticTypeEnum cosmetic)
    {
        var texture = LoadDiskTexture(path);
        Debug.Log("loading texture from " + path);
        //var texture = Assembly.GetManifestResourceStream(path);
        return CreateSprite(path.SanitisePath(), cosmetic switch
        {
            CosmeticTypeEnum.Hat or CosmeticTypeEnum.Visor => 100f,
            _ => texture.width * 0.375f
        });
    }
}