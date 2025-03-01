namespace TownOfUsFusion;

public class Asset
{
    [JsonPropertyName("id")]
    public string ID { get; set; }
}
public abstract class CustomCosmetic : Asset
{
    [JsonPropertyName("artist")]
    public string Artist { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("condition")]
    public string Condition { get; set; }

    [JsonPropertyName("stream")]
    public bool StreamOnly { get; set; }

    [JsonPropertyName("test")]
    public bool TestOnly { get; set; }
}

public abstract class CosmeticExtension
{
    public string Artist { get; set; }
    public string Condition { get; set; }
    public bool StreamOnly { get; set; }
    public bool TestOnly { get; set; }
}

public class CustomHat : CustomCosmetic
{
    [JsonPropertyName("flipid")]
    public string FlipID { get; set; }

    [JsonPropertyName("backid")]
    public string BackID { get; set; }

    [JsonPropertyName("backflipid")]
    public string BackFlipID { get; set; }

    [JsonPropertyName("climbid")]
    public string ClimbID { get; set; }

    [JsonPropertyName("climbflipid")]
    public string ClimbFlipID { get; set; }

    [JsonPropertyName("floorid")]
    public string FloorID { get; set; }

    [JsonPropertyName("floorflipid")]
    public string FloorFlipID { get; set; }

    [JsonPropertyName("nobounce")]
    public bool NoBounce { get; set; }

    [JsonPropertyName("adaptive")]
    public bool Adaptive { get; set; }

    [JsonPropertyName("behind")]
    public bool Behind { get; set; }
}

public class HatExtension : CosmeticExtension
{
    public Sprite FlipImage { get; set; }
    public Sprite BackFlipImage { get; set; }
}

public class CustomVisor : CustomCosmetic
{
    [JsonPropertyName("flipid")]
    public string FlipID { get; set; }

    [JsonPropertyName("floorid")]
    public string FloorID { get; set; }

    [JsonPropertyName("climbid")]
    public string ClimbID { get; set; }

    [JsonPropertyName("adaptive")]
    public bool Adaptive { get; set; }

    [JsonPropertyName("infront")]
    public bool InFront { get; set; }
}

public class VisorExtension : CosmeticExtension
{
    public Sprite ClimbImage { get; set; }
    public Sprite FloorImage { get; set; }
}

public class CustomNameplate : CustomCosmetic;

public class NameplateExtension : CosmeticExtension;
