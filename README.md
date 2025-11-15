# WinUI3 RisohEditor

A modern Windows resource editor built with WinUI 3, inspired by [RisohEditor](https://github.com/katahiromz/RisohEditor) by katahiromz.

## Overview

RisohEditor is a free resource editor for Win32 development that allows you to edit resources in:
- Resource Script files (`.rc`)
- Compiled Resource files (`.res`) 
- Executable files (`.exe`)
- Dynamic Link Libraries (`.dll`)

This WinUI3 version brings a modern interface while maintaining compatibility with standard Windows resource formats.

## Features

### Implemented
- ✅ Modern WinUI 3 interface with Mica backdrop
- ✅ Menu-driven file operations (New, Open, Save, Save As)
- ✅ Resource tree view organized by type
- ✅ Basic resource text editor
- ✅ RC file parsing and generation
- ✅ PE file format detection (.exe/.dll)
- ✅ Support for multiple resource types:
  - DIALOG resources
  - MENU resources
  - STRING resources
  - ICON/BITMAP references
  - ACCELERATOR tables
  - VERSIONINFO
  - Custom resources

### In Development
- 🚧 Full PE resource extraction
- 🚧 Binary .RES file support
- 🚧 Visual dialog editor
- 🚧 Icon/Bitmap viewer and editor
- 🚧 String table grid editor
- 🚧 Resource import/export
- 🚧 Undo/Redo functionality
- 🚧 Multi-language support

## Resource Types

The editor supports the following standard Windows resource types:

| Type | Description | Status |
|------|-------------|--------|
| DIALOG | Dialog box templates | Text editor |
| MENU | Menu definitions | Text editor |
| STRINGTABLE | String resources | Text editor |
| ICON | Icon resources | Parser only |
| BITMAP | Bitmap resources | Parser only |
| CURSOR | Cursor resources | Parser only |
| ACCELERATORS | Keyboard accelerators | Text editor |
| VERSIONINFO | Version information | Text editor |
| RCDATA | Raw data resources | Text editor |
| MESSAGETABLE | Message tables | Planned |

## System Requirements

- Windows 10 version 1809 (build 17763) or later
- Windows 11 (recommended)
- .NET 8.0 SDK
- Windows App SDK 1.8 or later

## Building

### Prerequisites
1. Install Visual Studio 2022 with:
   - .NET desktop development workload
   - Universal Windows Platform development workload
   - Windows App SDK C# Templates

2. Clone the repository:
```bash
git clone https://github.com/JimmyKodu/WinUI3RisohEditor.git
cd WinUI3RisohEditor
```

### Build Instructions

#### Using Visual Studio
1. Open `WinUI3RisohEditor.slnx` in Visual Studio 2022
2. Select the appropriate platform (x86, x64, or ARM64)
3. Build the solution (F7 or Ctrl+Shift+B)
4. Run the application (F5)

#### Using Command Line
```bash
dotnet build WinUI3RisohEditor/WinUI3RisohEditor.csproj
```

Note: Building WinUI3 applications requires Windows as the target platform.

## Usage

### Opening Resource Files

1. **File → Open** or press `Ctrl+O`
2. Select a resource file:
   - `.rc` - Resource Script (text format)
   - `.res` - Compiled Resource (binary format)
   - `.exe` - Executable with embedded resources
   - `.dll` - Dynamic Link Library with resources

### Editing Resources

1. Select a resource from the tree view
2. Edit the resource content in the text editor
3. Click **Save Changes** to apply
4. **File → Save** to write changes to disk

### Creating New Resources

1. **Edit → Add Resource**
2. Choose the resource type
3. Enter a resource name/ID
4. Edit the resource content

### Saving Files

- **File → Save** - Save to current file
- **File → Save As** - Save to new location
- Currently supports `.rc` format for output

## Architecture

### Project Structure
```
WinUI3RisohEditor/
├── MainWindow.xaml          # Main UI definition
├── MainWindow.xaml.cs       # Main window code-behind
├── ResourceParsers/
│   ├── RCParser.cs          # Resource Script parser
│   └── PEParser.cs          # PE file parser
├── Assets/                  # Application icons and images
└── Package.appxmanifest     # App manifest
```

### Key Components

**RCParser**: Parses and generates Resource Script files
- Extracts resources using regex patterns
- Supports nested structures (menus, dialogs)
- Generates formatted RC output

**PEParser**: Reads resources from PE files
- Parses PE headers (DOS, COFF, Optional)
- Locates resource section
- Extracts resource directory tree

**ResourceNode**: Data model for resources
- Type (DIALOG, MENU, etc.)
- Name/ID
- Content (text or binary)

## Comparison with Original RisohEditor

### Advantages
- Modern WinUI 3 interface
- Native Windows 11 styling (Mica)
- Better high-DPI support
- Async file operations
- Extensible architecture

### Original Features Not Yet Implemented
- Visual dialog designer with drag-and-drop
- Icon/Bitmap editor
- Message table compiler (mcdx)
- Resource.h management
- Multilanguage UI
- Portable version
- Plugin support

## Contributing

Contributions are welcome! Areas that need help:

1. **PE Resource Extraction**: Complete implementation of binary resource reading
2. **Visual Editors**: Dialog designer, icon editor, bitmap editor
3. **File Formats**: Support for `.res` binary format
4. **Localization**: Multi-language support
5. **Testing**: Test with real-world resource files

## License

This project is open source. Please refer to the LICENSE file for details.

The original RisohEditor by katahiromz is licensed under GPLv3.

## Acknowledgments

- **katahiromz** - Creator of the original [RisohEditor](https://github.com/katahiromz/RisohEditor)
- **Microsoft** - WinUI 3 framework and Windows App SDK

## Links

- Original RisohEditor: https://github.com/katahiromz/RisohEditor
- WinUI 3 Documentation: https://docs.microsoft.com/windows/apps/winui/
- Windows App SDK: https://docs.microsoft.com/windows/apps/windows-app-sdk/

## Screenshots

_Screenshots will be added once the application is built and tested on Windows._

## FAQ

### Q: Why recreate RisohEditor in WinUI3?
**A:** To provide a modern interface using the latest Windows UI technology while learning about resource file formats.

### Q: Will this replace the original RisohEditor?
**A:** No, this is an educational/experimental project. The original RisohEditor is more feature-complete and battle-tested.

### Q: Can I use this in production?
**A:** This is an early version. Use at your own risk and always backup your resource files.

### Q: Does it support Unicode?
**A:** Yes, WinUI3 fully supports Unicode strings, and the RC parser handles UTF-8/UTF-16.

### Q: Can I edit EXE files directly?
**A:** Currently, you can view resources from EXE/DLL files but saving back to PE format is not yet implemented.

## Roadmap

### Version 1.0 (Current)
- [x] Basic UI framework
- [x] RC file parsing
- [x] Resource tree view
- [x] Text editing

### Version 1.1 (Planned)
- [ ] Complete PE resource extraction
- [ ] Binary RES file support
- [ ] Icon/Bitmap viewers
- [ ] String table grid editor

### Version 2.0 (Future)
- [ ] Visual dialog editor
- [ ] Icon/Bitmap editor
- [ ] Resource import/export
- [ ] Undo/Redo
- [ ] Find/Replace in resources

### Version 3.0 (Future)
- [ ] Multi-language support
- [ ] Plugin system
- [ ] Theme customization
- [ ] Resource comparison

## Support

For issues, questions, or suggestions:
- Open an issue on GitHub
- Check the original RisohEditor documentation for resource format details

---

**Note**: This is a recreation/reimplementation project for educational purposes. For production use, consider the original [RisohEditor](https://github.com/katahiromz/RisohEditor) which is more mature and feature-complete.
