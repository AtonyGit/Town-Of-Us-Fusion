using System.IO;
using System.Reflection;
using Reactor.Utilities.Extensions;
using TownOfUsFusion.Extensions;
using UnityEngine;

namespace TownOfUsFusion.Cosmetics;

public static class CustomCosmeticsManager
{
    public static Sprite CreateSprite(Texture2D tex, string name, float size = -1f, SpriteMeshType meshType = SpriteMeshType.Tight)
    {
        var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), size > 0f ? size : 100f, 0, meshType);
        sprite.name = name;
        sprite.hideFlags |= HideFlags.DontSaveInEditor;
        return sprite.DontDestroy();
    }
/*
    public static Sprite CreateSprite(string name, float size = -1f, SpriteMeshType meshType = SpriteMeshType.Tight)
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
            var sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, size > 0f ? size : pixelsPerUnit, 0, meshType);
            sprite.DontDestroy();
            return sprite.DontDestroy();
    }*/
    private static Texture2D EmptyTexture() => new(2, 2, TextureFormat.ARGB32, true);
    public static Texture2D LoadTexture(byte[] data, string name)
    {
        var texture = EmptyTexture();

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
        return CreateSprite(texture, path.SanitisePath(), cosmetic switch
        {
            CosmeticTypeEnum.Hat or CosmeticTypeEnum.Visor => 100f,
            _ => texture.width * 0.375f
        });
    }
}