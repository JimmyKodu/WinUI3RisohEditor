# WinUI3 RisohEditor - Project Structure Overview

```
┌─────────────────────────────────────────────────────────────────┐
│                    WinUI3 RisohEditor Application                │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                         User Interface Layer                      │
│                              (XAML/WinUI3)                        │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  ┌────────────────────────────────────────────────────────┐    │
│  │              MainWindow.xaml                            │    │
│  │  ┌──────────────────────────────────────────────┐     │    │
│  │  │ Menu Bar: File | Edit | View | Help          │     │    │
│  │  └──────────────────────────────────────────────┘     │    │
│  │  ┌──────────────────────────────────────────────┐     │    │
│  │  │ Toolbar: New | Open | Save | Add | Delete    │     │    │
│  │  └──────────────────────────────────────────────┘     │    │
│  │  ┌──────────┬─┬────────────────────────────────┐     │    │
│  │  │          │ │                                 │     │    │
│  │  │ Resource │ │    Editor Content Area         │     │    │
│  │  │   Tree   │ │                                 │     │    │
│  │  │          │ │  ┌──────────────────────────┐  │     │    │
│  │  │ • Dialogs│ │  │ DialogEditorPage         │  │     │    │
│  │  │ • Menus  │ │  │ - Visual Canvas          │  │     │    │
│  │  │ • Strings│ │  │ - Properties Panel       │  │     │    │
│  │  │ • Icons  │ │  └──────────────────────────┘  │     │    │
│  │  │ • Bitmaps│ │  ┌──────────────────────────┐  │     │    │
│  │  │ • ...    │ │  │ StringTableEditorPage    │  │     │    │
│  │  │          │ │  │ - String List            │  │     │    │
│  │  │          │ │  │ - Add/Edit/Delete        │  │     │    │
│  │  │          │ │  └──────────────────────────┘  │     │    │
│  │  │          │ │  ┌──────────────────────────┐  │     │    │
│  │  │          │ │  │ MenuEditorPage           │  │     │    │
│  │  │          │ │  │ - Menu Tree              │  │     │    │
│  │  │          │ │  │ - Properties Panel       │  │     │    │
│  │  │          │ │  └──────────────────────────┘  │     │    │
│  │  └──────────┴─┴────────────────────────────────┘     │    │
│  │  ┌──────────────────────────────────────────────┐     │    │
│  │  │ Status Bar: Ready | File Path                │     │    │
│  │  └──────────────────────────────────────────────┘     │    │
│  └────────────────────────────────────────────────────────┘    │
│                                                                   │
└─────────────────────────────────────────────────────────────────┘
                              ▲
                              │ Data Binding
                              │ Commands
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│                    ViewModel / Code-Behind Layer                  │
│                              (C# Logic)                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  • MainWindow.xaml.cs                                            │
│    - File operations (Open, Save, New)                          │
│    - Resource tree management                                    │
│    - Editor switching                                            │
│                                                                   │
│  • DialogEditorPage.xaml.cs                                     │
│    - Dialog properties management                                │
│    - Control manipulation                                        │
│                                                                   │
│  • StringTableEditorPage.xaml.cs                                │
│    - String CRUD operations                                      │
│    - ObservableCollection management                             │
│                                                                   │
│  • MenuEditorPage.xaml.cs                                       │
│    - Menu tree operations                                        │
│    - Menu item management                                        │
│                                                                   │
└─────────────────────────────────────────────────────────────────┘
                              ▲
                              │ Service Calls
                              │ Data Access
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│                        Services Layer                             │
│                      (Business Logic)                             │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  ResourceFileService.cs                                          │
│  ┌───────────────────────────────────────────────────────┐     │
│  │ • LoadFileAsync(filePath)                             │     │
│  │   - Detect format (.rc, .res, .exe, .dll)            │     │
│  │   - Route to appropriate parser                        │     │
│  │                                                        │     │
│  │ • SaveFileAsync(filePath)                             │     │
│  │   - Generate output in requested format               │     │
│  │                                                        │     │
│  │ • CreateNew()                                          │     │
│  │   - Initialize empty resource collection              │     │
│  │                                                        │     │
│  │ • AddResource(resource)                               │     │
│  │ • RemoveResource(resource)                            │     │
│  │ • GetResourcesByType(type)                            │     │
│  └───────────────────────────────────────────────────────┘     │
│                                                                   │
└─────────────────────────────────────────────────────────────────┘
                              ▲
                              │ CRUD Operations
                              │ Type Mapping
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│                         Models Layer                              │
│                       (Data Structures)                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  ResourceModels.cs                                               │
│  ┌───────────────────────────────────────────────────────┐     │
│  │                                                        │     │
│  │  ResourceItem (Base Class)                            │     │
│  │  ├── DialogResource                                   │     │
│  │  │   ├── Caption, Position, Size                      │     │
│  │  │   └── List<DialogControl>                          │     │
│  │  │                                                     │     │
│  │  ├── MenuResource                                     │     │
│  │  │   └── List<MenuItem>                               │     │
│  │  │       └── List<MenuItem> (SubItems)                │     │
│  │  │                                                     │     │
│  │  ├── StringTableResource                              │     │
│  │  │   └── Dictionary<int, string>                      │     │
│  │  │                                                     │     │
│  │  ├── IconResource                                     │     │
│  │  │   ├── Width, Height, ColorDepth                    │     │
│  │  │   └── byte[] Data                                  │     │
│  │  │                                                     │     │
│  │  ├── BitmapResource                                   │     │
│  │  ├── AcceleratorResource                              │     │
│  │  └── VersionInfoResource                              │     │
│  │                                                        │     │
│  └───────────────────────────────────────────────────────┘     │
│                                                                   │
└─────────────────────────────────────────────────────────────────┘
                              ▲
                              │ File I/O
                              │ Parsing/Generation
                              ▼
┌─────────────────────────────────────────────────────────────────┐
│                      File System Layer                            │
│                    (Resource Files)                               │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  Supported Formats:                                              │
│  • .rc  - Resource Script (Text-based)                          │
│  • .res - Compiled Resource (Binary)                            │
│  • .exe - Executable with resources                             │
│  • .dll - Dynamic Library with resources                        │
│                                                                   │
└─────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────┐
│                     Documentation Files                           │
├─────────────────────────────────────────────────────────────────┤
│                                                                   │
│  • README.md              - Project overview, build instructions │
│  • ARCHITECTURE.md        - Technical details, design patterns   │
│  • CONTRIBUTING.md        - Contribution guidelines              │
│  • CHANGELOG.md           - Version history                      │
│  • IMPLEMENTATION_SUMMARY.md - What's been done                  │
│                                                                   │
└─────────────────────────────────────────────────────────────────┘

Data Flow:
──────────

1. User opens file → MainWindow → ResourceFileService
2. Service parses file → Creates ResourceItem objects
3. Resources populate tree view → User selects resource
4. MainWindow loads appropriate editor page
5. Editor binds to resource data → User edits
6. Changes update ResourceItem → User saves
7. ResourceFileService generates output → Writes to file

Key Design Patterns:
────────────────────

• MVVM: Separation of UI (XAML), Logic (Code-behind), and Data (Models)
• Service Pattern: ResourceFileService centralizes file operations
• Observer Pattern: ObservableCollection for automatic UI updates
• Factory Pattern: Resource creation based on type
• Strategy Pattern: Different parsers for different file formats
```
