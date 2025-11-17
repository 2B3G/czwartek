using System;
using System.Text;

public static class CipherUtils
{
    //  CAESAR
    public static string CaesarEncrypt(string plain, int shift)
    {
        shift = (shift % 26 + 26) % 26;

        var sb = new StringBuilder();

        foreach (char ch in plain)
            sb.Append(ShiftChar(ch, shift));

        return sb.ToString();
    }

    public static string CaesarDecrypt(string cipher, int shift)
    {
        return CaesarEncrypt(cipher, -shift);
    }

    private static char ShiftChar(char ch, int shift)
    {
        if (char.IsUpper(ch))
            return (char)('A' + (ch - 'A' + shift + 26) % 26);

        if (char.IsLower(ch))
            return (char)('a' + (ch - 'a' + shift + 26) % 26);

        return ch;
    }

    //  COLUMNAR TRANSPOSITION
    public static string ColumnarEncrypt(string plain, int columns)
    {
        if (string.IsNullOrEmpty(plain)) return "";

        int len = plain.Length;
        if (columns < 2) columns = 2;
        if (columns > len) columns = len; // max columns = length

        int rows = (int)Math.Ceiling(len / (double)columns);
        char[,] grid = new char[rows, columns];
        int index = 0;

        // fill grid
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < columns; c++)
                grid[r, c] = index < len ? plain[index++] : 'X'; // fill empty with 'X'

        // read column by column
        var sb = new StringBuilder();
        for (int c = 0; c < columns; c++)
            for (int r = 0; r < rows; r++)
                sb.Append(grid[r, c]);

        return sb.ToString();
    }
    public static string ColumnarDecrypt(string cipher, int columns)
    {
        if (string.IsNullOrEmpty(cipher)) return "";

        int len = cipher.Length;
        if (columns < 2) columns = 2;
        if (columns > len) columns = len;

        int rows = (int)Math.Ceiling(len / (double)columns);
        char[,] grid = new char[rows, columns];

        int index = 0;
        for (int c = 0; c < columns; c++)
            for (int r = 0; r < rows; r++)
                if (index < len)
                    grid[r, c] = cipher[index++];

        // read row by row
        var sb = new StringBuilder();
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < columns; c++)
                sb.Append(grid[r, c]);

        return sb.ToString().TrimEnd('X'); // remove padding
    }


    //  ROT13
    public static string ROT13(string s)
    {
        return CaesarEncrypt(s, 13);
    }

    //  XOR
    public static string XOREncrypt(string input, int key)
    {
        var sb = new StringBuilder();
        foreach (char c in input)
            sb.Append((char)(c ^ key));
        return sb.ToString();
    }

    public static string XORDecrypt(string input, int key)
    {
        return XOREncrypt(input, key);
    }
}
