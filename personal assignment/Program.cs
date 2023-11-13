
namespace personal_assignment
{
    public class Program
    {
        private static Character Player;
        private static List<Item> inventory;

        static void Main(string[] args)
        {
            GameData();
            start();
        }
        static void GameData()
        {
            Player = new Character("Choonjin", "전사", 1, 10, 5, 100, 1500);
            inventory = new List<Item>
            {
                new Item("검", ItemType.Weapon, "공격력", 5),
                new Item("활", ItemType.Weapon, "공격력", 3),
                new Item("도끼", ItemType.Weapon, "공격력", 7),
                new Item("무쇠갑옷", ItemType.Armor, "방어력", 5),
                new Item("무쇠투구", ItemType.Armor, "방어력", 3),
                new Item("무쇠견갑", ItemType.Armor, "방어력", 1),
                new Item("무쇠하의", ItemType.Armor, "방어력", 4),
                new Item("무쇠장갑", ItemType.Armor, "방어력", 1)
            };

        }

        static void start()
        {

            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int input = CheckValidInput(1, 2);
            switch (input)
            {
                case 1:
                    status();
                    break;
                case 2:
                    inventorydisplay();
                    break;
            }

        }
        static void status()
        {
            Console.Clear();

            Console.WriteLine("상태보기");
            Console.WriteLine("캐릭터의 정보를 표시합니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Player.Level}");
            Console.WriteLine($"{Player.Name}({Player.Job})");
            Console.WriteLine($"공격력 :{Player.Atk}");
            Console.WriteLine($"방어력 : {Player.Def}");
            Console.WriteLine($"체력 : {Player.Hp}");
            Console.WriteLine($"Gold : {Player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 0);
            switch (input)
            {
                case 0:
                    start();
                    break;
            }
        }
        static void inventorydisplay()
        {
            Console.Clear();

            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < inventory.Count; i++)
            {
                string equipStatus = Player.EquippedItems.Contains(inventory[i]) ? " [E]" : ""; // 장착한 아이템인 경우 " [E]" 추가
                Console.WriteLine($"{equipStatus} {inventory[i].Name} ({inventory[i].Type})  {inventory[i].ValueTpye} : {inventory[i].Value}");
            }
            Console.WriteLine();

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            int input = CheckValidInput(0, 1);
            switch (input)
            {
                case 1:
                    EquipManage();
                    break;
                case 0:
                    start();
                    break;
            }
        }
        static void EquipManage()
        {
            Console.Clear();
            Console.WriteLine("장착 관리");
            Console.WriteLine("장착하고자하는 아이템의 번호를 입력하세요.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < inventory.Count; i++)
            {
                string equipStatus = Player.EquippedItems.Contains(inventory[i]) ? " [E]" : ""; // 장착한 아이템인 경우 " [E]" 추가
                Console.WriteLine($"{i + 1}. {equipStatus} {inventory[i].Name} ({inventory[i].Type})  {inventory[i].ValueTpye} : {inventory[i].Value}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int input = CheckValidInput(0, inventory.Count);
            if (input == 0)
            {
                inventorydisplay();
                return;
            }

            Item selectedItem = inventory[input - 1];

            if (Player.EquippedItems.Contains(selectedItem))
            {
                Player.EquippedItems.Remove(selectedItem);
                // 아이템을 해제할 때의 처리
                if (selectedItem.Type == ItemType.Weapon)
                    Player.UpdateStats(-selectedItem.Value, 0);
                else if (selectedItem.Type == ItemType.Armor)
                    Player.UpdateStats(0, -selectedItem.Value);
            }
            else
            {
                Player.EquippedItems.Add(selectedItem);
                // 아이템을 장착할 때의 처리
                if (selectedItem.Type == ItemType.Weapon)
                    Player.UpdateStats(selectedItem.Value, 0);
                else if (selectedItem.Type == ItemType.Armor)
                    Player.UpdateStats(0, selectedItem.Value);
            }


            EquipManage();
        }
        static int CheckValidInput(int min, int max)
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool parseSuccess = int.TryParse(input, out var ret);
                if (parseSuccess)
                {
                    if (ret >= min && ret <= max)
                        return ret;
                }
                Console.WriteLine("잘못된 입력입니다.");
            }
        }


        public class Character
        {
            public string Name { get; }
            public string Job { get; }
            public int Level { get; }
            public int Atk { get; private set; }
            public int Def { get; private set; }
            public int Hp { get; }
            public int Gold { get; }

            public Character(string name, string job, int level, int atk, int def, int hp, int gold)
            {
                Name = name;
                Job = job;
                Level = level;
                Atk = atk;
                Def = def;
                Hp = hp;
                Gold = gold;
                EquippedItems = new List<Item>();
            }
            public List<Item> EquippedItems { get; set; } = new List<Item>();
            public void UpdateStats(int atkDelta, int defDelta)
            {
                Atk += atkDelta;
                Def += defDelta;
            }
        }
        public class Item
        {
            public string Name { get; }
            public ItemType Type { get; }
            public string ValueTpye { get; }
            public int Value { get; }

            public Item(string name, ItemType type, string valuetype, int value)
            {
                Name = name;
                Type = type;
                ValueTpye = valuetype;
                Value = value;
            }
        }
        public enum ItemType
        {
            Weapon,
            Armor
        }
    }
}
