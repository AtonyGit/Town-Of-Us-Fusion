namespace TownOfUsFusion;

[RegisterInIl2Cpp]
public class MiraDebugWindow(IntPtr cppPtr) : MonoBehaviour(cppPtr)
{
    [HideFromIl2Cpp]
    public DragWindow DebuggingWindow { get; } = new(
        new Rect(10, 10, 0, 0),
        "MIRA API DEBUGGING",
        () =>
        {
        })
    {
        Enabled = true,
    };

    public void OnGUI()
    {
        DebuggingWindow.OnGUI();
    }
}
