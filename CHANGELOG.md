# Changelog

All notable changes to the WinUI3 RisohEditor project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Initial WinUI3 application structure
- Main window with menu bar, toolbar, and status bar
- Resource tree view with common resource categories
- File open/save dialogs with support for RC, RES, EXE, DLL formats
- Resource model classes for various resource types:
  - Dialog resources
  - Menu resources
  - String table resources
  - Icon resources
  - Bitmap resources
  - Accelerator resources
  - Version info resources
- Resource editor pages:
  - Dialog editor with visual canvas and properties panel
  - String table editor with add/edit/delete functionality
  - Menu editor with tree view and properties
- ResourceFileService for file operations (skeleton)
- Mica material backdrop for modern UI
- About dialog with project information
- README.md with project documentation
- ARCHITECTURE.md with detailed architecture documentation
- CONTRIBUTING.md with contribution guidelines
- Comprehensive .gitignore for Visual Studio projects

### Planned
- Resource file parsing (RC format)
- Resource file generation (RC format)
- Compiled resource (RES) format support
- PE file resource extraction (EXE/DLL)
- Actual resource loading and saving functionality
- Undo/Redo support
- Visual dialog designer with drag-and-drop
- Icon/Bitmap preview and editing
- Accelerator table editor
- Version info editor
- Search and replace functionality
- Multi-language support
- Resource validation
- Settings/Preferences dialog

## [0.1.0] - 2024-11-15

### Added
- Initial project setup
- Basic WinUI3 template application

---

## Version History

### Version 0.1.0 (Initial Release)
- Basic project structure
- Core UI framework
- Foundation for future development

---

## Future Releases

### Version 0.2.0 (Planned)
- Working RC file parser
- Basic resource loading
- Functional dialog editor

### Version 0.3.0 (Planned)
- Resource saving functionality
- More resource editors
- Improved UI/UX

### Version 1.0.0 (Goal)
- Full feature parity with original RisohEditor core features
- Stable RC file support
- Complete resource editing suite
- Professional-grade UI
