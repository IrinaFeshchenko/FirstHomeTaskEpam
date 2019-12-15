using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace epam1DZ
{
    abstract class NameGeroi
    {
        public string Name { get; set; }
        public NameGeroi()
        {
            Name = "NONAME";
        }
        public NameGeroi(string name)
        {
            Name = name;
        }
        public virtual void Dysplay()
        {
            Console.Write("Имя: "+ Name+ " ");
        }
        public void dysplay()
        {
            Console.WriteLine(" " + Name + " ");
        }
        public void Nickname()
        {
            Console.Write(Name +": ");
        }
    }
    class Info:NameGeroi
    {
        public string Animal { get; set; }
        public string Color { get; set; }
        public string Personality { get; set; }
        public Info(string name):base(name)
        {
            
        }
        public Info(string name,string animal):base(name)
        {
            Animal = animal;
        }
        public Info(string name,string animal,string color):base(name)
        {
            Animal = animal;
            Color = color;
        }
        public Info(string name,string animal,string color,string personality):base(name)
        {
            Animal = animal;
            Color = color;
            Personality = personality;
        }
        public override void Dysplay()
        {
            base.Dysplay();
            Console.Write("Животное: " + Animal +" ");
            Console.Write("Окрас: " + Color+" ");
            Console.Write("Характер: " + Personality + " ");
        }
    }
}
