using MelonLoader;

namespace BodylogExtender;

public abstract class Preferences
{
    private const int DefaultPresetCount = 4;

    private static readonly MelonPreferences_Category Category;
    private static readonly MelonPreferences_Entry<int> PresetCount;

    static Preferences()
    {
        Category = MelonPreferences.CreateCategory("Bodylog Extender");
        PresetCount = Category.CreateEntry("Preset Count", DefaultPresetCount);
    }

    public static void SetPresetCount(int count)
    {
        PresetCount.Value = count;
    }

    public static int GetPresetCount()
    {
        return PresetCount.Value;
    }
    
    public static void Load()
    {
        Category.LoadFromFile();
    }

    public static void Save()
    {
        Category.SaveToFile();
    }
}