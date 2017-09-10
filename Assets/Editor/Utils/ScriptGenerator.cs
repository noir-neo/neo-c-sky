using System;
using System.IO;
using System.Text;
using UnityEditor;

public static class ScriptGenerator
{
    private static readonly string[] INVALID_CHARS =
    {
        " ", "!", "\"", "#", "$",
        "%", "&", "\'", "(", ")",
        "-", "=", "^",  "~", "\\",
        "|", "[", "{",  "@", "`",
        "]", "}", ":",  "*", ";",
        "+", "/", "?",  ".", ">",
        ",", "<"
    };

    public static void CreateScript(string path, string contents)
    {
        var directoryName = Path.GetDirectoryName(path);
        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        File.WriteAllText(path, contents, Encoding.UTF8);
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }

    public static string RemoveInvalidChars(string str)
    {
        Array.ForEach(INVALID_CHARS, c => str = str.Replace(c, string.Empty));
        return str;
    }
}

