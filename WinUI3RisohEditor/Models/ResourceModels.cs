namespace WinUI3RisohEditor.Models
{
    /// <summary>
    /// Represents a resource type in the resource file
    /// </summary>
    public enum ResourceType
    {
        Unknown = 0,
        Accelerator = 1,
        Bitmap = 2,
        Cursor = 3,
        Dialog = 4,
        Icon = 5,
        Menu = 6,
        StringTable = 7,
        VersionInfo = 8,
        Custom = 9,
        Html = 10,
        Manifest = 11,
        Toolbar = 12
    }

    /// <summary>
    /// Base class for all resource items
    /// </summary>
    public abstract class ResourceItem
    {
        public string Name { get; set; } = string.Empty;
        public ResourceType Type { get; set; }
        public int Id { get; set; }
        public ushort Language { get; set; }
        public byte[] Data { get; set; } = Array.Empty<byte>();

        protected ResourceItem(ResourceType type)
        {
            Type = type;
        }

        public abstract string GetDisplayName();
    }

    /// <summary>
    /// Represents a dialog resource
    /// </summary>
    public class DialogResource : ResourceItem
    {
        public string Caption { get; set; } = string.Empty;
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<DialogControl> Controls { get; set; } = new();

        public DialogResource() : base(ResourceType.Dialog) { }

        public override string GetDisplayName() => $"Dialog: {Name}";
    }

    /// <summary>
    /// Represents a control in a dialog
    /// </summary>
    public class DialogControl
    {
        public string Type { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public uint Style { get; set; }
        public uint ExStyle { get; set; }
    }

    /// <summary>
    /// Represents a menu resource
    /// </summary>
    public class MenuResource : ResourceItem
    {
        public List<MenuItem> Items { get; set; } = new();

        public MenuResource() : base(ResourceType.Menu) { }

        public override string GetDisplayName() => $"Menu: {Name}";
    }

    /// <summary>
    /// Represents a menu item
    /// </summary>
    public class MenuItem
    {
        public string Text { get; set; } = string.Empty;
        public int Id { get; set; }
        public uint Flags { get; set; }
        public List<MenuItem> SubItems { get; set; } = new();
    }

    /// <summary>
    /// Represents a string table resource
    /// </summary>
    public class StringTableResource : ResourceItem
    {
        public Dictionary<int, string> Strings { get; set; } = new();

        public StringTableResource() : base(ResourceType.StringTable) { }

        public override string GetDisplayName() => $"String Table: {Name}";
    }

    /// <summary>
    /// Represents an icon resource
    /// </summary>
    public class IconResource : ResourceItem
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ColorDepth { get; set; }

        public IconResource() : base(ResourceType.Icon) { }

        public override string GetDisplayName() => $"Icon: {Name}";
    }

    /// <summary>
    /// Represents a bitmap resource
    /// </summary>
    public class BitmapResource : ResourceItem
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int ColorDepth { get; set; }

        public BitmapResource() : base(ResourceType.Bitmap) { }

        public override string GetDisplayName() => $"Bitmap: {Name}";
    }

    /// <summary>
    /// Represents an accelerator resource
    /// </summary>
    public class AcceleratorResource : ResourceItem
    {
        public List<AcceleratorEntry> Entries { get; set; } = new();

        public AcceleratorResource() : base(ResourceType.Accelerator) { }

        public override string GetDisplayName() => $"Accelerator: {Name}";
    }

    /// <summary>
    /// Represents an accelerator entry
    /// </summary>
    public class AcceleratorEntry
    {
        public int Id { get; set; }
        public int Key { get; set; }
        public byte Flags { get; set; }
    }

    /// <summary>
    /// Represents version information resource
    /// </summary>
    public class VersionInfoResource : ResourceItem
    {
        public Dictionary<string, string> StringInfo { get; set; } = new();
        public int FileVersionMS { get; set; }
        public int FileVersionLS { get; set; }
        public int ProductVersionMS { get; set; }
        public int ProductVersionLS { get; set; }

        public VersionInfoResource() : base(ResourceType.VersionInfo) { }

        public override string GetDisplayName() => "Version Information";
    }
}
