using System;
using System.Reflection.Metadata;

class Program
{
    const int LETTERS_COUNT = 26;
    const char UNKNOWN_SYMBOL = '_';

    static void Main()
    {
        List<string> rWords = ["World", "University", "Victor", "Love"];
        Shybenytsa(rWords);

    }

    static void Shybenytsa(List<string> rWords)
    {
        string gameWord = GetRElement(rWords);
        char[] quessedWord = new char[gameWord.Length];
        for (int i = 0; i < quessedWord.Length; i++)
            quessedWord[i] = UNKNOWN_SYMBOL;

        int maxErrorCount = GetMaxErrorCount(gameWord.Length);
        int errorCount = 0;
        int quessedLetters = 0;

        Output(quessedWord, errorCount, maxErrorCount);

        while (true)
        {
            System.Console.Write("Enter a letter: ");
            char curLetter = GetChar();
            sbyte isQuessed = -1;

            for (int i = 0; i < gameWord.Length; i++)
            {
                if (gameWord[i] == curLetter)
                {
                    if (quessedWord[i] != UNKNOWN_SYMBOL)
                    {
                        isQuessed = 0;
                        break;
                    }
                    quessedWord[i] = curLetter;
                    quessedLetters++;
                    isQuessed = 1;
                }
            }

            if (isQuessed == 1)
            {
                if (quessedLetters == gameWord.Length)
                {
                    Output(quessedWord, errorCount, maxErrorCount, "WooHoo :)");
                    break;
                }
                Output(quessedWord, errorCount, maxErrorCount);
            }
            else if (isQuessed == -1)
            {
                errorCount++;

                if (errorCount == maxErrorCount)
                {
                    Output(quessedWord, errorCount, maxErrorCount, "You lose ;(");
                    break;
                }
                Output(quessedWord, errorCount, maxErrorCount, "There is no letter \"" + curLetter + "\"");
            }
            else if (isQuessed == 0)
                Output(quessedWord, errorCount, maxErrorCount, "\"" + curLetter + "\" already quessed");
        }
    }

    static T GetRElement<T>(List<T> rWords)
    {
        Random random = new();
        int rIndex = (int)random.NextInt64(0, rWords.Count - 1);
        return rWords[rIndex];
    }

    static int GetMaxErrorCount(int wordLen)
    {
        return (int)Math.Ceiling((LETTERS_COUNT - wordLen) / (wordLen / 2.0));
    }

    static char GetChar()
    {
        bool isInputValid = false;
        char letter = '\0';

        while (!isInputValid)
        {
            try
            {
                letter = Convert.ToChar(Console.ReadLine());
            }
            catch (FormatException)
            {
                System.Console.WriteLine("Wrong input.");
                isInputValid = false;
                continue;
            }

            isInputValid = true;
        }

        return letter;
    }

    static void Output(char[] word, int errorCount, int maxErrorCount, string? message = null)
    {
        Console.Clear();

        System.Console.WriteLine("Shybenytsa. You must quess the word within " + maxErrorCount + " errors.");

        System.Console.WriteLine($"Word: \"{new string(word)}\"");
        System.Console.WriteLine("Errors: " + errorCount);

        if (message != null)
            System.Console.WriteLine(message);
    }

};