# Contributing to WinUI3 RisohEditor

Thank you for your interest in contributing to WinUI3 RisohEditor! This guide will help you get started.

## How to Contribute

### Reporting Bugs

If you find a bug, please create an issue on GitHub with:

- **Title**: Clear, descriptive title
- **Description**: Detailed description of the bug
- **Steps to Reproduce**: Step-by-step instructions
- **Expected Behavior**: What should happen
- **Actual Behavior**: What actually happens
- **Environment**: Windows version, .NET version, etc.
- **Screenshots**: If applicable

### Suggesting Features

Feature suggestions are welcome! Please create an issue with:

- **Title**: Clear feature description
- **Use Case**: Why this feature would be useful
- **Proposed Implementation**: How it might work (optional)
- **Alternatives**: Other approaches you've considered

### Code Contributions

#### Getting Started

1. **Fork the Repository**
   ```bash
   git clone https://github.com/JimmyKodu/WinUI3RisohEditor.git
   cd WinUI3RisohEditor
   ```

2. **Create a Branch**
   ```bash
   git checkout -b feature/your-feature-name
   # or
   git checkout -b fix/your-bug-fix
   ```

3. **Make Your Changes**
   - Follow the coding guidelines below
   - Write clear, concise commit messages
   - Test your changes thoroughly

4. **Submit a Pull Request**
   - Describe your changes clearly
   - Reference any related issues
   - Include screenshots for UI changes

#### Coding Guidelines

##### C# Style

- **Naming Conventions**
  - Classes: `PascalCase` (e.g., `ResourceFileService`)
  - Methods: `PascalCase` (e.g., `LoadFileAsync`)
  - Private fields: `_camelCase` (e.g., `_currentFile`)
  - Properties: `PascalCase` (e.g., `CurrentDialog`)
  - Local variables: `camelCase` (e.g., `fileContent`)

- **Code Organization**
  - One class per file
  - Group related methods together
  - Use regions sparingly (only for large files)

- **Documentation**
  - Use XML comments for public APIs
  - Include summary, param, and returns tags
  - Document complex algorithms

Example:
```csharp
/// <summary>
/// Loads a resource file from the specified path.
/// </summary>
/// <param name="filePath">Path to the resource file</param>
/// <returns>True if successful, false otherwise</returns>
public async Task<bool> LoadFileAsync(string filePath)
{
    // Implementation
}
```

##### XAML Style

- Use proper indentation (4 spaces)
- Group related properties
- Use meaningful x:Name values
- Leverage styles and templates for reusability

Example:
```xaml
<Button 
    x:Name="SaveButton"
    Content="Save"
    Style="{StaticResource AccentButtonStyle}"
    Click="SaveButton_Click"
    Margin="8,0,0,0"/>
```

##### Async Programming

- Use async/await for I/O operations
- Suffix async methods with `Async`
- Avoid async void (except for event handlers)
- Use `ConfigureAwait(false)` where appropriate

```csharp
public async Task<string> ReadFileAsync(string path)
{
    return await File.ReadAllTextAsync(path).ConfigureAwait(false);
}
```

##### Error Handling

- Use try-catch for expected exceptions
- Log errors appropriately
- Show user-friendly error messages
- Don't swallow exceptions silently

```csharp
try
{
    await LoadFileAsync(filePath);
}
catch (FileNotFoundException)
{
    await ShowErrorAsync("File not found", "The specified file does not exist.");
}
catch (Exception ex)
{
    await ShowErrorAsync("Error", $"An unexpected error occurred: {ex.Message}");
}
```

#### Project Structure

When adding new features:

1. **Models**: Add data structures to `Models/ResourceModels.cs`
2. **Services**: Add business logic to appropriate service classes
3. **Views**: Create new pages in `Views/` folder
4. **Resources**: Add strings, styles to appropriate .xaml files

#### Testing

- Test on Windows 10 and Windows 11
- Test with different file formats (RC, RES, EXE, DLL)
- Verify UI responsiveness
- Check accessibility (keyboard navigation, screen readers)
- Test with both light and dark themes

#### Pull Request Process

1. **Before Submitting**
   - Ensure code builds without errors
   - Run all tests (when test suite is available)
   - Update documentation if needed
   - Add yourself to CONTRIBUTORS.md (if not already there)

2. **PR Description**
   - Clear title describing the change
   - Detailed description of what and why
   - Link to related issues
   - Screenshots for UI changes
   - Breaking changes (if any)

3. **Review Process**
   - Maintainers will review your PR
   - Address feedback and make requested changes
   - Once approved, PR will be merged

### Areas Needing Contribution

High-priority areas:

1. **Resource File Parsing**
   - RC file parser implementation
   - RES file parser implementation
   - PE resource extraction

2. **Resource Editors**
   - Icon/Bitmap editor with image preview
   - Accelerator table editor
   - Version info editor
   - Custom resource editor

3. **UI/UX Improvements**
   - Drag-and-drop for dialog controls
   - Visual alignment guides
   - Improved properties panels
   - Keyboard shortcuts

4. **Features**
   - Undo/Redo functionality
   - Search and replace
   - Resource validation
   - Import/Export tools

5. **Documentation**
   - User guide
   - API documentation
   - Tutorial videos
   - Code examples

### Development Setup

#### Prerequisites

- Windows 10 (1809+) or Windows 11
- Visual Studio 2022 (17.8+)
- .NET 8.0 SDK
- Windows App SDK 1.8+

#### Recommended Extensions

- ReSharper or Rider (optional)
- XAML Styler
- GitLens (for VS Code)

#### Building the Project

```bash
# Clone the repository
git clone https://github.com/JimmyKodu/WinUI3RisohEditor.git
cd WinUI3RisohEditor

# Open in Visual Studio
start WinUI3RisohEditor.slnx

# Or build from command line
dotnet build WinUI3RisohEditor/WinUI3RisohEditor.csproj
```

### Communication

- **Issues**: Use GitHub issues for bugs and features
- **Discussions**: Use GitHub discussions for questions
- **Email**: For private matters only

### Code of Conduct

Be respectful and professional:

- Use welcoming and inclusive language
- Be respectful of differing viewpoints
- Accept constructive criticism gracefully
- Focus on what is best for the community
- Show empathy towards other community members

### Recognition

Contributors will be:
- Listed in CONTRIBUTORS.md
- Mentioned in release notes
- Given credit in commit messages

Thank you for contributing to WinUI3 RisohEditor! 🎉
