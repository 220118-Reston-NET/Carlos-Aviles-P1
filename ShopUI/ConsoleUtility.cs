namespace ShopUI
{

    /*
     * Special utilities used to interact with Microsoft's Console.
     */
    public class ConsoleUtility
    {

        /*
         * Replaces characters with *, can be used for password fields.
         */
        public static string ReadSensitiveLine()
        {
            string line = "";
            ConsoleKeyInfo info = Console.ReadKey(true);

            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    line += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        line = line.Substring(0, line.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            return line;
        }
    }

}