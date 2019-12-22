using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam1DZ
{
    class Program
    {

        private static void DisplayMessage(string message) => Console.WriteLine(message);
        static void Main(string[] args)
        {
            Acccount acc = new Acccount(100);
            Random rand1 = new Random();
            int presence_of_powder;
            int hit;
            string catname = "Кокос";
            string dogname = "Нос";
            string bullname = "Синяк";
            //List.SWAP<string>(ref catname, ref dogname);
            bool repeat = true;
            while (repeat == true)
            {
                Random ran = new Random();
                int swap = ran.Next(1, 3);
                if (swap == 1)
                {
                    List.SWAP<string>(ref catname, ref dogname);
                }
                else if(swap==2)
                {
                    List.SWAP<string>(ref catname, ref bullname);
                }
                else if(swap ==3)
                {
                    List.SWAP<string>(ref dogname, ref bullname);
                }
                int start = 0;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Хотите начать чтение сказки?");
                Console.WriteLine("1 - yes, 2 - no");
                try
                {
                    start = int.Parse(Console.ReadLine());
                }
                catch when (start != 1 || start != 2)
                {
                    repeat = true;
                }
                finally
                {
                    acc.Notify += DisplayMessage;       // добавляем обработчик DisplayMessage
                    acc.Take(20);    // отнимаем от счета 20
                    acc.Notify -= DisplayMessage;
                    
                    
                    if (start == 1)
                    {
                        Console.WriteLine("Кот Кокос и его друзья");
                        Console.WriteLine("Поездка пса Носа");
                        Console.WriteLine("Главные героои: ");
                        Info cat = new Info(catname, "Кот", "белый с пятнами", "обезбашенный делает 'муку' из нюханики");
                        cat.Dysplay();
                        Console.WriteLine();
                        /*description of fairy tale*/
                        Info dog = new Info(dogname, "Пес", "темный с белыми пятнами", "попешаный на 'муке' кота");
                        dog.Dysplay();
                        Info bull = new Info(bullname, "Як", "коричневый бык", "плотно сидящий на 'синей воде' и выращивающий нюханику");
                        Console.WriteLine();
                        bull.Dysplay();
                        Console.WriteLine();
                        Info catName = new Info(catname);
                        Info dogName = new Info(dogname);
                        Info bullName = new Info(bullname);
                        /*end of description*/
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
                        presence_of_powder = rand1.Next(1, 10);
                        if (presence_of_powder >= 1 && presence_of_powder <= 5)
                        {
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
                            hit = rand.Next(0, 10);
                            hit = rand.Next(0, 10);
                            if (hit >= 0 && hit <= 5)
                            {
                                Console.WriteLine(" Бык не попадает в пса. Действие синей воды заканчивается слишком быстро и в это время пес пытается унести ноги. ");
                                Console.WriteLine(" Ягоды не были собраны. Нос бросает использовать волшебную пудру. ");
                            }
                            else if (hit >= 6)
                            {
                                Console.WriteLine("Попал");
                                Console.WriteLine("Пес кричит от боли!В это время кот нападает под видом пугала на быка!" +
                    "Бык не понимает, что это кот и думает, что начались галюны и убегает в страхе.Пес очнувшись тоже пугается пугала, но потом понимает, что это кот.");
                                Console.WriteLine();
                                dogName.Nickname();
                                Console.WriteLine(" Я думал, что пугало ожило!А это ты.Бык повержен - время собирать урожай!");
                                Random rand3 = new Random();
                                int call = rand3.Next(1, 5);
                                if (call >= 1 && call <= 3)
                                {
                                    catName.Nickname();
                                    Console.WriteLine(" Звони по телефону друзьям.");
                                    Console.WriteLine();
                                }
                                else if (call >= 4 && call <= 5)
                                {
                                    catName.Nickname();
                                    Console.WriteLine(" Позвони по старинке.");
                                    Console.WriteLine();
                                }
                            }
                        }
                        else if (presence_of_powder <= 10 && presence_of_powder >= 5)
                        {
                            catName.Nickname();
                            Console.Write(" Три килло.");
                            Console.WriteLine();
                            dogName.Nickname();
                            Console.WriteLine(" Вот и отлично.Я под твоим домом заложил золотые монеты.");
                        }

                        Console.WriteLine();
                    }
                    else if (start == 2)
                    {
                        repeat = false;
                        Console.WriteLine("Press any key to close application");
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
