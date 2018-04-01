using MontyHallLibrary;
using System;
using System.Diagnostics;

namespace MontyHallParadox
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Представьте, что вы стали участником игры, в которой вам нужно выбрать одну из трёх дверей.\n" +
                "За одной из дверей находится автомобиль, за двумя другими дверями — козы.\n" +
                "Вы выбираете одну из дверей, например, номер 1, после этого ведущий, который знает, где находится автомобиль, " +
                "а где — козы, открывает одну из оставшихся дверей, например, номер 3, за которой находится коза.\n" +
                "После этого он спрашивает вас — не желаете ли вы изменить свой выбор и выбрать дверь номер 2?\n" +
                "Увеличатся ли ваши шансы выиграть автомобиль, если вы примете предложение ведущего и измените свой выбор?\n");

            while (true) {
                Console.WriteLine("Выберите стратегию (y - менять дверь, n - не менять дверь, q - выйти): ");
                var line = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(line)) {
                    continue;
                }

                char key = line[0];

                if (key == 'q') {
                    break;
                }

                if (key != 'y' && key != 'n') {
                    Console.WriteLine("Неизвестная команда.");
                    continue;
                }

                bool strategy = key == 'y';

                Console.WriteLine($"{Environment.NewLine}Ваша стратегия: {(strategy ? "" : "не ")}менять выбранную дверь.");

                // создаем игрока
                Player player = new Player(strategy);

                int count;
            
                // проверяем корректность ввода
                do {
                    Console.WriteLine("Введите количество игр:");
                    if (Int32.TryParse(Console.ReadLine(), out count)) {
                        // ввели целое число
                        break;
                    }

                    Console.WriteLine("Вы ввели не целое число.");
                } while (true);
                
                var timer = new Stopwatch();

                Console.WriteLine("Старт выполнения:");
                timer.Start();
                int wins = StartPlaying(player, count);
                timer.Stop();

                Console.WriteLine($"Процент побед: {((double)wins / count) * 100} %");
                Console.WriteLine($"Прошедшее время, мс: {timer.ElapsedMilliseconds}{Environment.NewLine}");
                Console.WriteLine(new string('-', 40) + Environment.NewLine);
            }
        }

        /// <summary>
        /// Играем count раз 
        /// </summary>
        /// <param name="player">Игрок, который будет играть</param>
        /// <param name="count">Количество игр</param>
        /// <returns>Количество выйгранных игр</returns>
        static int StartPlaying(Player player, int count)
        {
            int wins = 0;

            for (int i = 0; i < count; i++) {
                if (player.Play())
                    wins++;
            }

            return wins;
        }
    }
}
