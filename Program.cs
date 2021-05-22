using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_7
{
    class Program
    {
        static void Main()
        {

            //checking 1

            Vector2 first_vector = new Vector2(5, 4);
            Vector2 second_vector = new Vector2(1, 9);
            //+
            Vector2 result_vector1 = first_vector + second_vector;
            Console.WriteLine($"{result_vector1.x}, {result_vector1.y}"); // 6,13
            //-
            Vector2 result_vector2 = first_vector - second_vector;
            Console.WriteLine($"{result_vector2.x}, {result_vector2.y}");//4,-5
            //++
            first_vector++;
            Console.WriteLine($"{first_vector.x}, {first_vector.y}"); //6,5
            //--
            result_vector1--;
            Console.WriteLine($"{result_vector1.x}, {result_vector1.y}");//5,12
            //==
            Vector2 fourth_vector = new Vector2(4, 4);
            Vector2 fifth_vector = new Vector2(4, 4);
            Console.WriteLine(fifth_vector == fourth_vector); //true
            Console.WriteLine(first_vector == second_vector); //false

            //!=
            Console.WriteLine(fifth_vector != fourth_vector);//false
            Console.WriteLine(first_vector != second_vector);//true

            //hash
            Console.WriteLine(result_vector1.GetHashCode()); //17

            //equals
            Console.WriteLine(fourth_vector.Equals(fifth_vector)); //true

            //tostring
            Console.WriteLine(first_vector.ToString());

            Console.ReadKey();

            //checking2

            List<Skeleton> Skeleton_list = new List<Skeleton>();
            int quantity_skeleton = int.Parse(Console.ReadLine());

            for (int i = 0; i < quantity_skeleton; i++)
            {
                Skeleton_list[i].OnDeath += Viewer.AddlListener;
            }

            for (int i = 0; i < Skeleton_list.Count; i++)
            {
                Skeleton_list[i].Kill();
            }

            //checking3

            Application application = new Application();
            Admin admin = new Admin();
            List<User> users_list = new List<User>();
            for (int i = 0; i< 4; i++ ) 
            {
                users_list.Add(new User());
                users_list[i].ListenUpdates(application);
            }
            application.CreateUpdate(admin.AccountType, "New Version", "admin");
        }
    }

    //1
    /* 1 
          + ♦ Создать класс или структуру Vector2

           + • Объявить поля/свойства для хранения координат X, Y
           + • Создать конструктор с параметрами
           + • Перегрузить операторы +, -, ++, --, ==, !=
           + • Переопределить методы GetHashCode, Equals, ToString
          + • Создать 2 открытых статических поля, характеризующих нулевой (0, 0) и единичный (0, 1) векторы
   */
    struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x + vector2.x, vector1.y + vector2.y);
        }

        public static Vector2 operator -(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.x - vector2.x, vector1.y - vector2.y);
        }

        public static Vector2 operator ++(Vector2 vector)
        {
            vector.x++;
            vector.y++;

            return vector;
        }

        public static Vector2 operator --(Vector2 vector)
        {
            vector.x--;
            vector.y--;

            return vector;
        }

        public static bool operator ==(Vector2 vector1, Vector2 vector2)
        {

            return vector1.x == vector2.y && vector1.y == vector2.y;
        }
        public static bool operator !=(Vector2 vector1, Vector2 vector2)
        {

            return vector1.x != vector2.y && vector1.y != vector2.y;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() + this.y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2 objectType)
            {
                return this.x == objectType.x && this.y == objectType.y;
            }

            return base.Equals(obj);
        }

        public override string ToString()
        {
            return "x:" + this.x.ToString() + " y:" + this.y.ToString();
        }

        public static Vector2 zero = new Vector2(0, 0);
        public static Vector2 first = new Vector2(0, 1);
    }

    //2
    /* 2
        ♦ Наблюдатель

        Создать класс Скелет
        + • Создать статическое целочисленное поле lastID
        + • Создать поле id
        + • Создать событие OnDeath, реализовать через делегат Action<int>
        + • Создать конструктор по умолчанию, которые присваивает полю id уникальное значение (в качестве счетчика использовать статическое поле lastID)
        + • Создать метод Kill, который убивает скелета и вызывает событие OnDeath (в качесиве параметра передавать id скелета)

        + Создать статический класс Наблюдатель
        + • Создать в нём статический метод void AddListener(int) который выводит принимаемое число

       + Объявить List с произвольным количеством скелетов
        + Подписаться классом Наблюдатель на события всех скелетов из списка
        + Вызвать метод Kill у всех скелетов
    */
    class Skeleton
    {
        public static int lastID;
        public int id;
        public event Action<int> OnDeath;
        public Skeleton()
        {
            this.id = lastID++;
        }

        public void Kill() 
        {
            OnDeath?.Invoke(this.id);
        }
    }

    static class Viewer
    {
        public static void AddlListener(int id)
        {
            Console.WriteLine($"You killed {id} skeleton");
        }
    }
    /* 3
      ♦ Реализовать систему рассылки оповещений пользователям о выходе нового обновления

        Создать класс Application со следующими полями и методами:
       + • Открытый делегат с сигнатурой void MessageDelegate(string, string)
       + • Открытый событие event MessageDelegate OnUpdateRelease
       + • Открытый метод CreateUpdate(AccountType accountType, string updateName, string updateDescription)
            в котором при условии, что accountType соответствует администратору, вызывать событие (с передачей в него updateName и updateDescription)

       + Создать перечисление AccountType с двумя полями Admin и User

       + Создать абстрактный класс AbstractUser и объявить в нем:
       + • Защищенным поле accountType типа AccountType
       + • Публичное свойство AccountType типа AccountType, обуспечив через него достук к полю accountType только для чтения

      +  Создать класс User и наследовать его от AbstractUser
      + • Конструктор по умолчанию, инициализирующий поле accountType значением AccountType.User
      +  • Закрытый метод с сигнатурой void ShowMessage(string, string), который выводит информацию об обновлении (название обновления и его описание)
      +  • Метод ListenUpdates, который принимает в качестве параметра объект типа Application

      +  Создать класс Admin и наследовать его от AbstractUser
      +  • Создать конструктор по умолчанию, инициализирующий поле accountType значением AccountType.Admin

      +  Создать объект application класса Application
      +  Создать объект admin класса Admin
      +  Создать произвольное количество объектов класса User и подписаться ими на обновления приложения методом ListenUpdates
      +  Создать новое обновление приложения c помощью метода CreateUpdate

*/

    //3
    class Application 
    {
        public delegate void MessageDelete(string string1, string string2);

        public event MessageDelete OnUpdateRelease;

        public void CreateUpdate(AccountType accountType, string updateName, string updateDescription)
        {
            switch (accountType)
            {
                case AccountType.Admin:
                    OnUpdateRelease?.Invoke(updateName, updateDescription);
                    break;
            }
        }
    }
    enum AccountType 
    {
    User,
    Admin
    }

    abstract class AbstrastUser
    {
        private readonly AccountType accountType;
        abstract public AccountType AccountType { get;}


    }

    class User : AbstrastUser
    {
        private readonly AccountType accountType;
        public override AccountType AccountType
        {
            get 
            {
                return accountType;
            }
        }
        public User()
        {
            this.accountType = AccountType.User;
        }

        public void ShowMessage(string updateName, string updateDescription)
        {
            Console.WriteLine($"Update: { updateName}, Descriprion: {updateDescription}");
        }
        public void ListenUpdates(Application obj)
        {
            obj.OnUpdateRelease += ShowMessage;
        }
    
    }

    class Admin : AbstrastUser
    {
        private readonly AccountType accountType;
        public override AccountType AccountType
        {
            get 
            {
                return accountType;
            }
        }

        public Admin()
        {
            this.accountType = AccountType.Admin;
        }
    }

}









