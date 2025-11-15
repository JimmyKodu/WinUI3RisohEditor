# WinUI3 RisohEditor - Architecture Documentation

## Overview

This project is a modern replication of [RisohEditor](https://github.com/katahiromz/RisohEditor) using WinUI3, .NET 8, and C#. It aims to provide a user-friendly, modern interface for editing Windows resources while maintaining compatibility with standard resource file formats.

## Architecture

### Technology Stack

- **UI Framework**: WinUI3 with XAML
- **Platform**: .NET 8.0, Windows 10/11
- **Language**: C# with nullable reference types
- **Design Pattern**: MVVM (Model-View-ViewModel)
- **Windows App SDK**: 1.8 or later

### Project Structure

```
WinUI3RisohEditor/
├── Models/                     # Data models for resources
│   └── ResourceModels.cs      # Resource type definitions
├── Services/                   # Business logic and file operations
│   └── ResourceFileService.cs # Resource file I/O operations
├── Views/                      # UI pages for resource editing
│   ├── DialogEditorPage.*     # Dialog resource editor
│   ├── StringTableEditorPage.*# String table editor
│   └── MenuEditorPage.*       # Menu resource editor
├── App.xaml[.cs]              # Application entry point
├── MainWindow.xaml[.cs]       # Main application window
└── Assets/                     # Application resources (icons, images)
```

## Core Components

### 1. Models (Data Layer)

**Location**: `Models/ResourceModels.cs`

Defines the data structures for all resource types:

- `ResourceItem` - Base class for all resources
- `DialogResource` - Dialog templates
- `MenuResource` - Menu definitions
- `StringTableResource` - String tables
- `IconResource` - Icons
- `BitmapResource` - Bitmaps
- `AcceleratorResource` - Keyboard accelerators
- `VersionInfoResource` - Version information

Each resource type inherits from `ResourceItem` and implements specific properties relevant to that resource type.

### 2. Services (Business Logic Layer)

**Location**: `Services/ResourceFileService.cs`

Handles all file operations:

- **Loading Resources**: Parse RC, RES, EXE, DLL files
- **Saving Resources**: Generate RC, RES files
- **Resource Management**: Add, remove, modify resources

#### Supported File Formats

| Format | Extension | Read | Write | Description |
|--------|-----------|------|-------|-------------|
| Resource Script | .rc | ✅ | ✅ | Text-based resource definition |
| Compiled Resource | .res | 🚧 | 🚧 | Binary compiled resources |
| Executable | .exe | 🚧 | ❌ | Extract resources from executables |
| Dynamic Library | .dll | 🚧 | ❌ | Extract resources from DLLs |

**Legend**: ✅ Implemented, 🚧 Planned, ❌ Not supported

### 3. Views (Presentation Layer)

#### MainWindow

**Location**: `MainWindow.xaml[.cs]`

The main application window featuring:

- **Menu Bar**: File, Edit, View, Help menus
- **Toolbar**: Quick access to common operations
- **Resource Tree**: Hierarchical view of resources by category
- **Editor Area**: Dynamic content area for resource editing
- **Status Bar**: Current file path and status messages

#### Editor Pages

##### DialogEditorPage

**Location**: `Views/DialogEditorPage.xaml[.cs]`

Visual dialog editor with:
- Canvas for WYSIWYG dialog design
- Properties panel for dialog and control settings
- Control toolbar for adding UI elements
- Alignment tools
- Test dialog functionality

##### StringTableEditorPage

**Location**: `Views/StringTableEditorPage.xaml[.cs]`

String table editor featuring:
- List view of all strings with IDs
- Add/Edit/Delete string operations
- Search and filter capabilities
- Bulk operations support

##### MenuEditorPage

**Location**: `Views/MenuEditorPage.xaml[.cs]`

Menu structure editor with:
- Tree view of menu hierarchy
- Add/Edit/Delete menu items
- Submenu support
- Menu item properties (ID, flags, shortcuts)
- Test menu preview

## Resource Type Details

### Dialog Resources

Dialogs contain:
- Position (X, Y) and Size (Width, Height)
- Caption text
- Style flags (caption, system menu, etc.)
- Font information
- Child controls (buttons, text boxes, labels, etc.)

### Menu Resources

Menus consist of:
- Menu items with text and IDs
- Submenus (nested menu items)
- Separators
- Flags (checked, grayed, etc.)

### String Table Resources

String tables store:
- ID-to-string mappings
- Localization strings
- UI text resources

### Icon/Bitmap Resources

Image resources include:
- Dimensions (width, height)
- Color depth
- Raw image data

## File Format Support

### RC (Resource Script) Format

Text-based format defined by Microsoft. Example:

```rc
IDD_ABOUT DIALOG 0, 0, 200, 150
CAPTION "About"
FONT 8, "MS Shell Dlg"
BEGIN
    DEFPUSHBUTTON "OK", IDOK, 70, 120, 50, 14
    LTEXT "Application Name", IDC_STATIC, 10, 10, 180, 8
END

IDR_MAINMENU MENU
BEGIN
    POPUP "&File"
    BEGIN
        MENUITEM "&New\tCtrl+N", IDM_NEW
        MENUITEM "&Open\tCtrl+O", IDM_OPEN
        MENUITEM SEPARATOR
        MENUITEM "E&xit", IDM_EXIT
    END
END
```

### RES (Compiled Resource) Format

Binary format produced by the resource compiler. Contains:
- Resource headers
- Binary data for each resource
- Language and version information

### PE (Portable Executable) Format

EXE and DLL files use the PE format. Resources are stored in a dedicated `.rsrc` section:
- Resource directory hierarchy
- Resource data entries
- Language-specific versions

## Design Patterns

### MVVM (Model-View-ViewModel)

The application follows MVVM principles:

- **Models**: Pure data classes (ResourceModels.cs)
- **Views**: XAML UI definitions (*.xaml files)
- **ViewModels**: Business logic and UI state (code-behind files)

### Service Pattern

The `ResourceFileService` acts as a central service for resource operations, abstracting file format details from the UI layer.

### Observer Pattern

WinUI3's data binding automatically implements the observer pattern:
- `ObservableCollection` for dynamic lists
- Property change notifications for UI updates

## Future Enhancements

### Planned Features

1. **Advanced Resource Types**
   - Custom resources
   - HTML resources
   - Manifest resources
   - Toolbar resources

2. **Editing Features**
   - Undo/Redo support
   - Drag-and-drop for controls
   - Alignment guides and snapping
   - Multi-select operations

3. **Import/Export**
   - Import from existing executables
   - Export individual resources
   - Batch operations

4. **Localization**
   - Multi-language support
   - Translation management
   - Language comparison view

5. **Advanced Tools**
   - Resource ID management
   - Duplicate detection
   - Resource optimization
   - Validation and error checking

## Development Guidelines

### Code Style

- Use C# nullable reference types
- Follow Microsoft C# coding conventions
- Use async/await for I/O operations
- Implement proper error handling

### Adding New Resource Types

1. Create model class in `ResourceModels.cs`
2. Add parsing logic in `ResourceFileService`
3. Create editor page in `Views/`
4. Update `MainWindow` to show new editor
5. Add category to resource tree

### Testing

- Test on Windows 10 and Windows 11
- Verify file format compatibility
- Test with various resource files
- Validate generated RC files with windres/rc.exe

## Contributing

Contributions are welcome! Areas needing attention:

- Resource file parsing (RC, RES formats)
- PE file resource extraction
- Additional resource editors
- UI/UX improvements
- Documentation
- Testing and bug fixes

## References

- [Original RisohEditor](https://github.com/katahiromz/RisohEditor)
- [WinUI3 Documentation](https://docs.microsoft.com/en-us/windows/apps/winui/winui3/)
- [Resource Script Format](https://docs.microsoft.com/en-us/windows/win32/menurc/resource-definition-statements)
- [PE Format Specification](https://docs.microsoft.com/en-us/windows/win32/debug/pe-format)

## License

This project is provided as-is for educational purposes. Please refer to the original RisohEditor for licensing information.
