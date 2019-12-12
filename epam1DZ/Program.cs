using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam1DZ
{
    class Program
    {
        static void Main(string[] args)
        {
            int hit;
            string catname = "Кокос";
            string dogname = "Нос";
            string bullname = "Синяк";
            Console.WriteLine("Кот Кокос и его друзья");
            Console.WriteLine("Поездка пса Носа");
            Console.WriteLine("Главные героои: ");
            Info cat = new Info(catname,"Кот","белый с пятнами","обезбашенный делает 'муку' из нюханики");
            cat.Dysplay();
            Console.WriteLine();
            Info dog = new Info(dogname,"Пес","темный с белыми пятнами","попешаный на 'муке' кота");
            dog.Dysplay();
            Info bull = new Info(bullname,"Як","коричневый бык", "плотно сидящий на 'синей воде' и выращивающий нюханику");
            Console.WriteLine();
            bull.Dysplay();
            Console.WriteLine();
            //NameGeroi catName = new NameGeroi(catname);
            //catName.dysplay();
            NameGeroi catName = new NameGeroi(catname);
            NameGeroi dogName = new NameGeroi(dogname);
            NameGeroi bullName = new NameGeroi(bullname);
            /*Start of the fairy tale*/
            Console.WriteLine("Однажды к коту Кокосу пришел в гости за его снадобьем пеес Нос.");
            dogName.Nickname();
            Console.Write(" Привет " + catname + " Мы отправляемся на каникулы нa большом корабле." +
                "А нам нужна твоя волшебная пудра, которая избавит нас от всех забот на корабле.");
            Console.WriteLine();
            catName.Nickname();
            Console.Write(" Как жаль, чтоя не смогу с вами поплыть вед коты боятся воды.Но я как раз готовлю новую партию пудру.На днях оборвал кусты нюханики, ягоды уже поспели.");
            Console.WriteLine();
            dogName.Nickname();
            Console.Write(" А сколько пудры там выйде с всех кустиков?");
            Console.WriteLine();
            catName.Nickname();
            Console.Write(" По стандарту киллограм.");
            Console.WriteLine();
            dogName.Nickname();
            Console.Write(" КАК КИЛЛОГРАМ?НАМ НУЖНО МИМНИМУМ 3.ПРЕДСТАВЬ 30 НЕДОВОЛЬНЫХ ПСОВ И ДВЕ НЕДЕЛИ С НИМИ НА КОРАБЛЕ.");
            Console.WriteLine();
            catName.Nickname();
            Console.Write(" Но у меня нет столько пудры.");
            Console.WriteLine();
            dogName.Nickname();
            Console.Write(" Я знаю кто выращивает нюханику!Як-як!Он пьет 'синюю воду' которую делает из колосьев листьев колдер!");
            Console.WriteLine();
            catName.Nickname();
            Console.Write(" Говорят если долго пить 'синюю воду' начинаешь делать странные вещи!");
            Console.WriteLine();
            catName.Nickname();
            Console.Write(" Иди и попроси у него ягод нюханики.Посмотрим на его реакцию!");
            Console.WriteLine();
            bullName.Nickname();
            Console.Write(" КТО ТУТ?");
            Console.WriteLine();
            dogName.Nickname();
            Console.Write(" Это я!Пес нос!");
            Console.WriteLine();
            bullName.Nickname();
            Console.Write(" ЗАЧЕМ ТЫ ЗДЕСЬ?Я НЕ ПОЗВОЛЮ ТРОГАТЬ МОИ ЛИСТЬЯ НЮХАНИКИ!");
            Console.WriteLine();
            Console.WriteLine("*****Бык достает ружье*****");
            Console.WriteLine();
            Random rand = new Random();
            hit = rand.Next(0,10);
            //Console.WriteLine(hit);
                do
                {
                    hit = rand.Next(0, 10);
                    Console.WriteLine("Не попал");
                if(hit >= 6)
                {
                    Console.WriteLine("Попал");
                }
                } while (hit >= 0 && hit <= 5);
            Console.WriteLine("Пес кричит от боли!В это время кот нападает под видом пугала на быка!" +
                "Бык не понимает, что это кот и думает, что начались галюны и убегает в страхе.Пес очнувшись тоже пугается пугала, но потом понимает, что это кот.");
            Console.WriteLine();
            dogName.Nickname();
            Console.WriteLine(" Я думал, что пугало ожило!А это ты.Бык повержен - время собирать урожай!");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
