using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam1DZ
{
    class Acccount
    {
        public delegate void AccountHandler(string message);
        private event AccountHandler _notify;
        public event AccountHandler Notify
        {
            add
            {
                _notify += value;
                Console.WriteLine($"{value.Method.Name} добавлен");
            }
            remove
            {
                _notify -= value;
                Console.WriteLine($"{value.Method.Name} удален");
            }
        }
        public Acccount(int sum)
        {
            Sum = sum;
        }
        public int Sum { get; private set; }
        public void Put(int sum)
        {
            Sum += sum;
            _notify?.Invoke($"На счет поступило: {sum}");
        }

        public void Take(int sum)
        {
            if (Sum >= sum)
            {
                Sum -= sum;
                _notify?.Invoke($"Со счета снято: {sum} Текущий баланс: {Sum}");

            }
            else
            {
                _notify?.Invoke($"Недостаточно денег на счете. Текущий баланс: {Sum}"); ;
                Console.Clear();
            }
        }
    }
}
