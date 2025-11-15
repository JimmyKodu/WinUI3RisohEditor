# Risoh Editor - WinUI3 Edition

A modern Windows resource editor built with WinUI3 and .NET 8, inspired by the original [RisohEditor](https://github.com/katahiromz/RisohEditor) by katahiromz.

## About

This project is a replication of the RisohEditor resource editor for Win32 development, reimagined with modern WinUI3 technology. It aims to provide a user-friendly interface for editing Windows resources such as dialogs, menus, icons, strings, and more.

## Features (Planned)

- 🎨 Modern WinUI3 interface with Mica material
- 📂 Open and edit resource files (.rc, .res, .exe, .dll)
- 🔧 Support for common resource types:
  - Dialogs
  - Menus
  - Icons and Cursors
  - Bitmaps
  - String Tables
  - Accelerators
  - Version Information
  - Custom Resources
- 💾 Save resources in various formats
- 👀 Visual resource preview
- ✏️ Intuitive resource editing
- 🌐 Multi-language support

## System Requirements

- Windows 10 version 1809 (build 17763) or later
- Windows 11 (recommended)
- .NET 8.0 or later
- Windows App SDK 1.8 or later

## Building the Project

### Prerequisites

- Visual Studio 2022 (version 17.8 or later)
- Windows App SDK 1.8 or later
- .NET 8.0 SDK

### Build Steps

1. Clone the repository:
   ```
   git clone https://github.com/JimmyKodu/WinUI3RisohEditor.git
   ```

2. Open `WinUI3RisohEditor.slnx` in Visual Studio 2022

3. Restore NuGet packages

4. Build the solution (Ctrl+Shift+B)

5. Run the application (F5)

## Project Structure

```
WinUI3RisohEditor/
├── WinUI3RisohEditor/          # Main application project
│   ├── App.xaml                # Application definition
│   ├── App.xaml.cs             # Application logic
│   ├── MainWindow.xaml         # Main window UI
│   ├── MainWindow.xaml.cs      # Main window logic
│   ├── Assets/                 # Application assets
│   └── Properties/             # Project properties
├── WinUI3RisohEditor.slnx      # Solution file
└── README.md                   # This file
```

## Current Status

🚧 **Work in Progress** 🚧

This project is in early development. Currently implemented:

- ✅ Main window with menu bar and toolbar
- ✅ Resource tree view with categories
- ✅ File open/save dialogs
- ✅ Basic application structure
- ⏳ Resource loading (planned)
- ⏳ Resource editing (planned)
- ⏳ Resource preview (planned)

## Original RisohEditor

This project is inspired by and aims to replicate the functionality of [RisohEditor](https://github.com/katahiromz/RisohEditor) by katahiromz, a free resource editor for Win32 development written in C++. The original RisohEditor:

- Supports reading/writing resource data in RC/RES/EXE/DLL files
- Supports UTF-16 resource files
- Works on Windows XP/2003/Vista/7/8.1/10 and ReactOS
- Is licensed under GPL v3

## License

This WinUI3 replication is provided as-is for educational and development purposes. Please refer to the original RisohEditor project for licensing information on resource editing concepts and implementations.

## Acknowledgments

- **katahiromz** - Creator of the original RisohEditor
- The WinUI team at Microsoft for the modern UI framework

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.

## Contact

For questions or feedback about this WinUI3 implementation, please open an issue on the GitHub repository.
