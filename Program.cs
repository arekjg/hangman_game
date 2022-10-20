using System;
using System.Collections.Generic;
using static System.Random;
using System.Text;
using System.IO;

namespace hangman_game
{
    class Program
    {
        // Prints the hangman
        private static void printHangman(int wrong)
        {
            if (wrong == 0)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 1)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 2)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine(" |  |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 3)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|  |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 4)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 5)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("/   |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 6)
            {
                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("/ \\ |");
                Console.WriteLine("   ===");
            }
        }

        // Prints the word
        private static int printWord(List<char>guessedLetters, String randomWord)
        {
            int counter = 0;
            int rightLetters = 0;
            Console.Write("\r\n");
            foreach (char c in randomWord)
            {
                if (guessedLetters.Contains(c))
                {
                    Console.Write(c + " ");
                    rightLetters++;
                }
                else
                {
                    Console.Write("  ");
                }
                counter++;
            }
            return rightLetters;
        }

        // Prints lines under the word
        private static void printLines(String randomWord)
        {
            Console.Write("\r");
            foreach (char c in randomWord)
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.Write("\u0305 ");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to hangman :)");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Choose difficulty: ");
            Console.WriteLine("1) Easy (4 letters)");
            Console.WriteLine("2) Medium (5-6 letters)");
            Console.WriteLine("3) Hard (7 letters)");
            char diff = Console.ReadLine()[0];

            // Choose a file with words depending on user's choice
            string path = "";
            switch (diff)
            {
                case '1':
                    path = "../../../words/easy.txt";
                    break;
                case '2':
                    path = "../../../words/medium.txt";
                    break;
                case '3':
                    path = "../../../words/hard.txt";
                    break;
                default:
                    break;
            }

            // Read file and create list with words from the file
            var logFile = File.ReadAllLines(path);
            var wordDictionary = new List<string>(logFile);
            
            // Pick random words from the list
            Random random = new Random();
            int index = random.Next(wordDictionary.Count);
            String randomWord = wordDictionary[index];

            foreach (char x in randomWord)
            {
                Console.Write("_ ");
            }

            int lengthOfWordToGuess = randomWord.Length;
            int amountOfTimesWrong = 0;
            List<char> currentLettersGuessed = new List<char>();
            int currentLettersRight = 0;

            while (amountOfTimesWrong != 6 && currentLettersRight != lengthOfWordToGuess)
            {
                Console.Write("\nLetters guessed so far: ");
                foreach (char letter in currentLettersGuessed)
                {
                    Console.Write(letter + " ");
                }
                // Prompt user for input
                Console.Write("\nGuess a letter: ");
                char letterGuessed = Console.ReadLine()[0];
                // Check if letter has already been guessed
                if (currentLettersGuessed.Contains(letterGuessed))
                {
                    Console.Write("\r\nYou have already guessed this letter.");
                    printHangman(amountOfTimesWrong);
                    currentLettersRight = printWord(currentLettersGuessed, randomWord);
                    Console.Write("\r\n");
                    printLines(randomWord);
                }
                else
                {
                    // Check if letter is in the word
                    bool right = false;
                    for (int i = 0; i < randomWord.Length; i++) { if (letterGuessed == randomWord[i]) { right = true; } }

                    if (right)
                    {
                        printHangman(amountOfTimesWrong);
                        currentLettersGuessed.Add(letterGuessed);
                        currentLettersRight = printWord(currentLettersGuessed, randomWord);
                        Console.Write("\r\n");
                        printLines(randomWord);
                    }
                    else
                    {
                        amountOfTimesWrong++;
                        currentLettersGuessed.Add(letterGuessed);
                        printHangman(amountOfTimesWrong);
                        currentLettersRight = printWord(currentLettersGuessed, randomWord);
                        Console.Write("\r\n");
                        printLines(randomWord);
                    }
                }
            }

            Console.WriteLine("\r\n");
            // Check if user wins or loses
            if (currentLettersRight == lengthOfWordToGuess)
            {
                Console.WriteLine("\r\nGame is over! You WIN! :) :) :)");
                Console.WriteLine("\r\nThank you for playing!");
            }
            else
            {
                Console.WriteLine("\r\nGame is over! You LOSE! :( :( :(");
                Console.WriteLine("\r\nThank you for playing!");
            }
        }
    }
}