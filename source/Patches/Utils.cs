namespace TownOfUsFusion
{

    [HarmonyPatch]
    public static class Utils
    {
        private static readonly Dictionary<string, List<UnityEngine.Object>> UnityLoadedObjects = [];
        private static readonly Dictionary<string, List<object>> SystemLoadedObjects = [];
        public static string SanitisePath(this string path)
        {
            path = path.Replace(".png", "");
            path = path.Replace(".raw", "");
            path = path.Replace(".wav", "");
            path = path.Replace(".txt", "");
            path = path.Split('/')[^1];
            path = path.Split('\\')[^1];
            path = path.Split('.')[^1];
            return path;
        }
        
    public static void SaveText(string fileName, string textToSave, string diskLocation) => SaveText(fileName, textToSave, true, diskLocation);

    public static void SaveText(string fileName, string textToSave, bool overrideText = true, string diskLocation = null)
    {
        try
        {
            File.WriteAllText(Path.Combine(diskLocation ?? Application.persistentDataPath, fileName), (overrideText ? "" : ReadDiskText(fileName, diskLocation)) + textToSave);
        }
        catch
        {
            Debug.LogError($"Unable to save text to {fileName}{(diskLocation != null ? $" in {diskLocation}" : "")}");
        }
    }
    public static string ReadDiskText(string fileName, string diskLocation = null)
    {
        try
        {
            return File.ReadAllText(Path.Combine(diskLocation ?? Application.persistentDataPath, fileName));
        }
        catch
        {
            Debug.LogError($"Error reading {fileName}{(diskLocation != null ? $" from {diskLocation}" : "")}");
            return "";
        }
    }
        public static void AddAsset(string name, UnityEngine.Object obj)
        {
            if (!UnityLoadedObjects.TryGetValue(name, out var value))
                UnityLoadedObjects[name] = [ obj ];
            else if (!value.Contains(obj))
                value.Add(obj);
        }

        public static void AddAsset(string name, object obj)
        {
            if (!SystemLoadedObjects.TryGetValue(name, out var value))
                SystemLoadedObjects[name] = [ obj ];
            else if (!value.Contains(obj))
                value.Add(obj);
        }
        public static IEnumerator Wait(float duration)
        {
            while (duration > 0)
            {
                duration -= Time.deltaTime;
                yield return EndFrame();
            }

            yield break;
        }
        public static IEnumerator PerformTimedAction(float duration, Action<float> action)
        {
            for (var t = 0f; t < duration; t += Time.deltaTime)
            {
                action(t / duration);
                yield return EndFrame();
            }

            action(1f);
            yield break;
        }
        public static IEnumerator EndFrame()
        {
            yield return new WaitForEndOfFrame();
        }

        public static bool IsNullEmptyOrWhiteSpace(string text) => text is null or "" || text.All(x => x is ' ' or '\n');

        public static void OverrideOnClickListeners(this PassiveButton passive, Action action, bool enabled = true)
        {
            passive.OnClick?.RemoveAllListeners();
            passive.OnClick = new();
            passive.OnClick.AddListener(action);
            passive.enabled = enabled;
        }

        public static void OverrideOnMouseOverListeners(this PassiveButton passive, Action action, bool enabled = true)
        {
            passive.OnMouseOver?.RemoveAllListeners();
            passive.OnMouseOver = new();
            passive.OnMouseOver.AddListener(action);
            passive.enabled = enabled;
        }

        public static void OverrideOnMouseOutListeners(this PassiveButton passive, Action action, bool enabled = true)
        {
            passive.OnMouseOut?.RemoveAllListeners();
            passive.OnMouseOut = new();
            passive.OnMouseOut.AddListener(action);
            passive.enabled = enabled;
        }
        public static void WipeListeners(this PassiveButton passive)
        {
            passive.OnClick.RemoveAllListeners();
            passive.OnMouseOut.RemoveAllListeners();
            passive.OnMouseOver.RemoveAllListeners();
            passive.OnClick = new();
            passive.OnMouseOut = new();
            passive.OnMouseOver = new();
        }
    }
}