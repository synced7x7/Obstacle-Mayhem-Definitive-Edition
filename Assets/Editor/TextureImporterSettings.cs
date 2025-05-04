using UnityEditor;
using UnityEngine;

public class TextureImporterSettings : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        TextureImporter importer = assetImporter as TextureImporter;

        if (importer != null)
        {
            importer.textureType = TextureImporterType.Sprite; // Set texture type to Sprite
            importer.spriteImportMode = SpriteImportMode.Single; // Set Sprite Mode to Single
            importer.mipmapEnabled = false; // Disable mipmaps for 2D sprites
            importer.filterMode = FilterMode.Point; // Example: Set filter mode to Point for pixel art
            importer.textureCompression = TextureImporterCompression.Uncompressed; // No compression
        }
    }
}
