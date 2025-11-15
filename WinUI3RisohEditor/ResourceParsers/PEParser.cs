using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WinUI3RisohEditor.ResourceParsers
{
    /// <summary>
    /// Parser for Portable Executable (PE) files (.exe, .dll)
    /// Extracts resources from the resource section
    /// </summary>
    public class PEParser
    {
        private const ushort IMAGE_DOS_SIGNATURE = 0x5A4D; // "MZ"
        private const uint IMAGE_NT_SIGNATURE = 0x00004550; // "PE\0\0"
        private const int IMAGE_DIRECTORY_ENTRY_RESOURCE = 2;

        public static List<ResourceNode> Parse(string filePath)
        {
            var resources = new List<ResourceNode>();

            try
            {
                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                using var reader = new BinaryReader(stream);

                // Read DOS header
                var dosSignature = reader.ReadUInt16();
                if (dosSignature != IMAGE_DOS_SIGNATURE)
                {
                    throw new InvalidDataException("Invalid DOS signature");
                }

                // Skip to PE header offset
                stream.Seek(0x3C, SeekOrigin.Begin);
                var peHeaderOffset = reader.ReadUInt32();

                // Read PE signature
                stream.Seek(peHeaderOffset, SeekOrigin.Begin);
                var peSignature = reader.ReadUInt32();
                if (peSignature != IMAGE_NT_SIGNATURE)
                {
                    throw new InvalidDataException("Invalid PE signature");
                }

                // Read COFF header
                var machine = reader.ReadUInt16();
                var numberOfSections = reader.ReadUInt16();
                reader.ReadUInt32(); // TimeDateStamp
                reader.ReadUInt32(); // PointerToSymbolTable
                reader.ReadUInt32(); // NumberOfSymbols
                var sizeOfOptionalHeader = reader.ReadUInt16();
                var characteristics = reader.ReadUInt16();

                // Read Optional Header
                var magic = reader.ReadUInt16();
                bool is64Bit = magic == 0x20b;

                // Skip to DataDirectory
                if (is64Bit)
                {
                    stream.Seek(peHeaderOffset + 24 + 112, SeekOrigin.Begin);
                }
                else
                {
                    stream.Seek(peHeaderOffset + 24 + 96, SeekOrigin.Begin);
                }

                // Read Resource Directory entry
                var resourceDirRVA = reader.ReadUInt32();
                var resourceDirSize = reader.ReadUInt32();

                if (resourceDirRVA == 0)
                {
                    // No resources
                    return resources;
                }

                // Find the resource section
                stream.Seek(peHeaderOffset + 24 + sizeOfOptionalHeader, SeekOrigin.Begin);

                uint resourceSectionOffset = 0;
                uint resourceSectionRVA = 0;

                for (int i = 0; i < numberOfSections; i++)
                {
                    var sectionName = Encoding.ASCII.GetString(reader.ReadBytes(8)).TrimEnd('\0');
                    reader.ReadUInt32(); // VirtualSize
                    var virtualAddress = reader.ReadUInt32();
                    var sizeOfRawData = reader.ReadUInt32();
                    var pointerToRawData = reader.ReadUInt32();
                    reader.ReadBytes(16); // Skip rest of section header

                    if (resourceDirRVA >= virtualAddress && resourceDirRVA < virtualAddress + sizeOfRawData)
                    {
                        resourceSectionOffset = pointerToRawData;
                        resourceSectionRVA = virtualAddress;
                        break;
                    }
                }

                if (resourceSectionOffset == 0)
                {
                    throw new InvalidDataException("Resource section not found");
                }

                // Read resource directory
                var resourceOffset = resourceSectionOffset + (resourceDirRVA - resourceSectionRVA);
                stream.Seek(resourceOffset, SeekOrigin.Begin);

                // Parse resource directory tree
                resources = ParseResourceDirectory(reader, resourceOffset, resourceSectionOffset, resourceSectionRVA);
            }
            catch (Exception ex)
            {
                // Add error resource to show what went wrong
                resources.Add(new ResourceNode
                {
                    Type = "ERROR",
                    Name = "Parse Error",
                    Content = $"Failed to parse PE file: {ex.Message}\n\nNote: Full PE parsing is complex. This is a simplified implementation."
                });
            }

            return resources;
        }

        private static List<ResourceNode> ParseResourceDirectory(
            BinaryReader reader,
            long directoryOffset,
            long sectionOffset,
            long sectionRVA)
        {
            var resources = new List<ResourceNode>();

            reader.BaseStream.Seek(directoryOffset, SeekOrigin.Begin);

            // Read IMAGE_RESOURCE_DIRECTORY
            reader.ReadUInt32(); // Characteristics
            reader.ReadUInt32(); // TimeDateStamp
            reader.ReadUInt16(); // MajorVersion
            reader.ReadUInt16(); // MinorVersion
            var numberOfNamedEntries = reader.ReadUInt16();
            var numberOfIdEntries = reader.ReadUInt16();

            var totalEntries = numberOfNamedEntries + numberOfIdEntries;

            // Read directory entries
            for (int i = 0; i < totalEntries; i++)
            {
                var nameOrId = reader.ReadUInt32();
                var offsetToData = reader.ReadUInt32();

                var resourceType = GetResourceTypeName(nameOrId);

                // For simplicity, add a basic resource node
                // A full implementation would recursively parse the tree structure
                resources.Add(new ResourceNode
                {
                    Type = resourceType,
                    Name = $"Resource_{nameOrId}",
                    Content = $"// Resource type: {resourceType}\n// This would contain the binary resource data"
                });
            }

            return resources;
        }

        private static string GetResourceTypeName(uint type)
        {
            // Standard resource types
            return type switch
            {
                1 => "CURSOR",
                2 => "BITMAP",
                3 => "ICON",
                4 => "MENU",
                5 => "DIALOG",
                6 => "STRING",
                7 => "FONTDIR",
                8 => "FONT",
                9 => "ACCELERATOR",
                10 => "RCDATA",
                11 => "MESSAGETABLE",
                12 => "GROUP_CURSOR",
                14 => "GROUP_ICON",
                16 => "VERSION",
                17 => "DLGINCLUDE",
                19 => "PLUGPLAY",
                20 => "VXD",
                21 => "ANICURSOR",
                22 => "ANIICON",
                23 => "HTML",
                24 => "MANIFEST",
                _ => $"CUSTOM_{type}"
            };
        }

        /// <summary>
        /// Extract icon from PE file
        /// </summary>
        public static byte[]? ExtractIcon(string filePath, string iconName)
        {
            // Placeholder for icon extraction
            // Would involve reading the icon group and icon data from resources
            return null;
        }

        /// <summary>
        /// Extract bitmap from PE file
        /// </summary>
        public static byte[]? ExtractBitmap(string filePath, string bitmapName)
        {
            // Placeholder for bitmap extraction
            return null;
        }

        /// <summary>
        /// Extract string table from PE file
        /// </summary>
        public static Dictionary<int, string> ExtractStringTable(string filePath)
        {
            // Placeholder for string table extraction
            return new Dictionary<int, string>();
        }
    }

    /// <summary>
    /// Helper class for reading PE structures
    /// </summary>
    internal static class PEStructures
    {
        public const int SIZEOF_IMAGE_DOS_HEADER = 64;
        public const int SIZEOF_IMAGE_FILE_HEADER = 20;
        public const int SIZEOF_IMAGE_OPTIONAL_HEADER32 = 224;
        public const int SIZEOF_IMAGE_OPTIONAL_HEADER64 = 240;
        public const int SIZEOF_IMAGE_SECTION_HEADER = 40;
        public const int SIZEOF_IMAGE_RESOURCE_DIRECTORY = 16;
        public const int SIZEOF_IMAGE_RESOURCE_DIRECTORY_ENTRY = 8;
    }
}
