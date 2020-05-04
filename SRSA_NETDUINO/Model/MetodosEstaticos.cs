using System;
using Microsoft.SPOT;

namespace Model
{
    public static class MetodosEstaticos
    {
        public static string Replace(string stringToSearch, char charToFind, char charToSubstitute)
        {
            // Surely there must be nicer way than this?
            char[] chars = stringToSearch.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
                if (chars[i] == charToFind) chars[i] = charToSubstitute;

            return new string(chars);
        }
    }
}
