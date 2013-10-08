﻿using System;
using System.Collections.Generic;
using System.Linq;
using RedditSharp;
using System.Security.Authentication;

namespace TestRedditSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new Reddit();
            while (reddit.User == null)
            {
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                var password = ReadPassword();
                try
                {
                    Console.WriteLine("Logging in...");
                    reddit.LogIn(username, password);
                }
                catch (AuthenticationException)
                {
                    Console.WriteLine("Incorrect login.");
                }
            }
            var subreddit = reddit.GetSubreddit("askreddit");
            var posts = subreddit.GetPosts();
            var post = posts.Last();
            Console.WriteLine("sucess");
            while (true)
            {
                post.Update();
                System.Threading.Thread.Sleep(5000);
                Console.WriteLine(post.Upvotes);
            }
        }
        

        public static string ReadPassword()
        {
            var passbits = new Stack<string>();
            //keep reading
            for (ConsoleKeyInfo cki = Console.ReadKey(true); cki.Key != ConsoleKey.Enter; cki = Console.ReadKey(true))
            {
                if (cki.Key == ConsoleKey.Backspace)
                {
                    //rollback the cursor and write a space so it looks backspaced to the user
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    passbits.Pop();
                }
                else
                {
                    Console.Write("*");
                    passbits.Push(cki.KeyChar.ToString());
                }
            }
            string[] pass = passbits.ToArray();
            Array.Reverse(pass);
            Console.Write(Environment.NewLine);
            return string.Join(string.Empty, pass);
        }
    }
}
