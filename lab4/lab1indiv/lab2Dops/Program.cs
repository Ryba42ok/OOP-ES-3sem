using System;
using dop1;
using dop2;


namespace dop1
{
    // Роли экипажа
    enum CrewRole
    {
        Captain,
        Pilot,
        Engineer,
        Doctor,
        FlightEngineer
    }

    // Состояние здоровья
    enum HealthStatus
    {
        Healthy,
        Injured,
        SeriouslyInjured,
        Unconscious
    }

    // Состояние систем корабля
    enum ShipStatus
    {
        AllSystemsWorking,
        MinorDamage,
        SeriousDamage,
        CriticalCondition
    }

    // Член экипажа
    struct CrewMember
    {
        public string name;
        public CrewRole role;
        public int skillLevel;   
        public HealthStatus health;

        public CrewMember(string name, CrewRole role, int skillLevel, HealthStatus health)
        {
            this.name = name;
            this.role = role;
            this.skillLevel = skillLevel;
            this.health = health;
        }

        // Просто выводим информацию о члене экипажа
        public void PrintInfo()
        {
            Console.WriteLine("  Имя: " + name);
            Console.WriteLine("  Должность: " + role);
            Console.WriteLine("  Навык: " + skillLevel + "/10");
            Console.WriteLine("  Здоровье: " + health);
            Console.WriteLine();
        }
    }

    // Корабль
    struct Spaceship
    {
        public string name;
        public int speed;              // условные единицы (например, км/с)
        public int fuel;               // запас топлива
        public CrewMember[] crew;      // массив членов экипажа
        public ShipStatus status;

        public Spaceship(string name, int speed, int fuel, CrewMember[] crew)
        {
            this.name = name;
            this.speed = speed;
            this.fuel = fuel;
            this.crew = crew;
            this.status = ShipStatus.AllSystemsWorking;
        }

        // Дальность полёта — простая формула: скорость * топливо
        public int CalculateRange()
        {
            int range = speed * fuel;

            // Если системы повреждены — дальность меньше
            if (status == ShipStatus.MinorDamage)
            {
                range = range - range / 10;      // минус 10%
            }
            else if (status == ShipStatus.SeriousDamage)
            {
                range = range - range / 3;       // минус треть
            }
            else if (status == ShipStatus.CriticalCondition)
            {
                range = range / 2;               // половина
            }

            return range;
        }

        // Проверка: все ли ключевые члены экипажа на борту и в сознании
        public bool CanStartMission()
        {
            bool hasCaptain = false;
            bool hasPilot = false;
            bool hasEngineer = false;

            for (int i = 0; i < crew.Length; i++)
            {
                CrewMember member = crew[i];

                // Если человек без сознания — он не считается
                if (member.health == HealthStatus.Unconscious)
                {
                    continue;
                }

                if (member.role == CrewRole.Captain)
                {
                    hasCaptain = true;
                }
                else if (member.role == CrewRole.Pilot)
                {
                    hasPilot = true;
                }
                else if (member.role == CrewRole.Engineer)
                {
                    hasEngineer = true;
                }
            }

            if (hasCaptain && hasPilot && hasEngineer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Столкновение с метеоритом
        public void HitByMeteorite(int damage)
        {
            Console.WriteLine("!!! Столкновение с метеоритом! Урон: " + damage);

            // Топливо утекает
            fuel = fuel - damage;
            if (fuel < 0)
            {
                fuel = 0;
            }

            // Портится состояние систем
            if (damage < 10)
            {
                if (status == ShipStatus.AllSystemsWorking)
                {
                    status = ShipStatus.MinorDamage;
                }
            }
            else if (damage < 25)
            {
                status = ShipStatus.SeriousDamage;
            }
            else
            {
                status = ShipStatus.CriticalCondition;
            }

            // Случайный член экипажа может пострадать
            if (crew.Length > 0)
            {
                Random rnd = new Random();
                int index = rnd.Next(0, crew.Length);

                if (damage >= 25)
                {
                    crew[index].health = HealthStatus.SeriouslyInjured;
                    Console.WriteLine("  " + crew[index].name + " тяжело ранен!");
                }
                else if (damage >= 10)
                {
                    crew[index].health = HealthStatus.Injured;
                    Console.WriteLine("  " + crew[index].name + " ранен!");
                }
            }

            Console.WriteLine("  Текущий статус систем: " + status);
            Console.WriteLine("  Остаток топлива: " + fuel);
            Console.WriteLine();
        }

        // Ремонт систем
        public void Repair()
        {
            Console.WriteLine("Начинаем ремонт систем...");

            // Ищем инженера с хорошим навыком
            bool repaired = false;

            for (int i = 0; i < crew.Length; i++)
            {
                CrewMember member = crew[i];

                if (member.role == CrewRole.Engineer && member.skillLevel >= 5
                    && member.health != HealthStatus.Unconscious)
                {
                    // Улучшаем состояние систем на одну ступень
                    if (status == ShipStatus.CriticalCondition)
                    {
                        status = ShipStatus.SeriousDamage;
                    }
                    else if (status == ShipStatus.SeriousDamage)
                    {
                        status = ShipStatus.MinorDamage;
                    }
                    else if (status == ShipStatus.MinorDamage)
                    {
                        status = ShipStatus.AllSystemsWorking;
                    }

                    Console.WriteLine("  " + member.name + " отремонтировал системы.");
                    Console.WriteLine("  Новый статус: " + status);
                    repaired = true;
                    break;
                }
            }

            if (repaired == false)
            {
                Console.WriteLine("  Некому ремонтировать (нет здорового инженера).");
            }

            Console.WriteLine();
        }

        // Лечение экипажа
        public void HealCrew()
        {
            Console.WriteLine("Начинаем лечение экипажа...");

            // Ищем врача
            bool hasDoctor = false;

            for (int i = 0; i < crew.Length; i++)
            {
                if (crew[i].role == CrewRole.Doctor
                    && crew[i].health != HealthStatus.Unconscious)
                {
                    hasDoctor = true;
                    break;
                }
            }

            if (hasDoctor == false)
            {
                Console.WriteLine("  На борту нет врача — лечение невозможно.");
                Console.WriteLine();
                return;
            }

            for (int i = 0; i < crew.Length; i++)
            {
                if (crew[i].health == HealthStatus.SeriouslyInjured)
                {
                    crew[i].health = HealthStatus.Injured;
                    Console.WriteLine("  " + crew[i].name + ": тяжело ранен -> ранен");
                }
                else if (crew[i].health == HealthStatus.Injured)
                {
                    crew[i].health = HealthStatus.Healthy;
                    Console.WriteLine("  " + crew[i].name + ": ранен -> здоров");
                }
            }

            Console.WriteLine();
        }

        // Печать состояния корабля
        public void PrintStatus()
        {
            Console.WriteLine("=== Корабль \"" + name + "\" ===");
            Console.WriteLine("Скорость: " + speed);
            Console.WriteLine("Топливо: " + fuel);
            Console.WriteLine("Дальность полёта: " + CalculateRange());
            Console.WriteLine("Состояние систем: " + status);
            Console.WriteLine("Экипаж:");
            for (int i = 0; i < crew.Length; i++)
            {
                crew[i].PrintInfo();
            }
        }
    }
}

namespace dop2
{
    enum DeviceType
    {
        Light,
        Heater,
        AirConditioner,
        Humidifier,
        Ventilation,
        WaterSensor,     // датчик протечки
        SmokeSensor,     // датчик дыма
        TemperatureSensor,
        HumiditySensor
    }

    // Состояние устройства
    enum DeviceState
    {
        Off,
        On,
        Error
    }

    // Условие по времени суток
    enum DayTime
    {
        Any,
        Morning,      // 6..12
        Day,          // 12..18
        Evening,      // 18..24
        Night         // 0..6
    }

    struct Device
    {
        public DeviceType type;
        public DeviceState state;
        public int power;            // потребляемая мощность (Вт)
        public int value;            // "показатель" (яркость, температура и т.п.)

        public Device(DeviceType type, DeviceState state, int power, int value)
        {
            this.type = type;
            this.state = state;
            this.power = power;
            this.value = value;
        }

        public void PrintInfo()
        {
            Console.WriteLine("    Устройство: " + type
                + " | Состояние: " + state
                + " | Мощность: " + power + " Вт"
                + " | Значение: " + value);
        }
    }

    struct Room
    {
        public string name;
        public double temperature;
        public double humidity;
        public Device[] devices;

        public Room(string name, double temperature, double humidity, Device[] devices)
        {
            this.name = name;
            this.temperature = temperature;
            this.humidity = humidity;
            this.devices = devices;
        }

        // Считаем суммарную мощность всех включённых устройств в комнате
        public int GetPowerUsage()
        {
            int total = 0;
            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i].state == DeviceState.On)
                {
                    total = total + devices[i].power;
                }
            }
            return total;
        }

        public void PrintInfo()
        {
            Console.WriteLine("  Комната: " + name);
            Console.WriteLine("    Температура: " + temperature + " °C");
            Console.WriteLine("    Влажность: " + humidity + " %");
            Console.WriteLine("    Потребление: " + GetPowerUsage() + " Вт");
            Console.WriteLine("    Устройства:");
            for (int i = 0; i < devices.Length; i++)
            {
                devices[i].PrintInfo();
            }
            Console.WriteLine();
        }
    }

   

    struct Scenario
    {
        public string name;
        public DayTime time;             
        public bool needPresence;        
        public double minTemp;           
        public double maxTemp;           
        public double maxHumidity;       
        public int priority;             
        public string action;            

        public Scenario(string name, DayTime time, bool needPresence,
                        double minTemp, double maxTemp, double maxHumidity,
                        int priority, string action)
        {
            this.name = name;
            this.time = time;
            this.needPresence = needPresence;
            this.minTemp = minTemp;
            this.maxTemp = maxTemp;
            this.maxHumidity = maxHumidity;
            this.priority = priority;
            this.action = action;
        }

        public void PrintInfo()
        {
            Console.WriteLine("  Сценарий: " + name);
            Console.WriteLine("    Время: " + time);
            Console.WriteLine("    Нужны жильцы: " + needPresence);
            Console.WriteLine("    Температура: " + minTemp + ".." + maxTemp);
            Console.WriteLine("    Макс. влажность: " + maxHumidity);
            Console.WriteLine("    Приоритет: " + priority);
            Console.WriteLine("    Действие: " + action);
            Console.WriteLine();
        }
    }
    struct SmartHome
    {
        public string name;
        public Room[] rooms;
        public Scenario[] scenarios;
        public bool residentsPresent;   // есть ли кто-то дома
        public int currentHour;         // текущий час 0..23

        public SmartHome(string name, Room[] rooms, Scenario[] scenarios)
        {
            this.name = name;
            this.rooms = rooms;
            this.scenarios = scenarios;
            this.residentsPresent = true;
            this.currentHour = 12;
        }

        // Определяем время суток по часу
        public DayTime GetDayTime()
        {
            if (currentHour >= 0 && currentHour < 6)
            {
                return DayTime.Night;
            }
            else if (currentHour < 12)
            {
                return DayTime.Morning;
            }
            else if (currentHour < 18)
            {
                return DayTime.Day;
            }
            else
            {
                return DayTime.Evening;
            }
        }

        // Проверка: подходит ли сценарий к текущей ситуации
        public bool IsScenarioActive(Scenario s, Room room)
        {
            // 1. Проверяем время
            DayTime now = GetDayTime();
            if (s.time != DayTime.Any && s.time != now)
            {
                return false;
            }

            // 2. Проверяем присутствие жильцов
            if (s.needPresence == true && residentsPresent == false)
            {
                return false;
            }

            // 3. Проверяем температуру
            if (room.temperature < s.minTemp)
            {
                return true;
            }
            if (room.temperature > s.maxTemp)
            {
                return true;
            }

            // 4. Проверяем влажность
            if (room.humidity > s.maxHumidity)
            {
                return true;
            }

            return false;
        }

        // Управление климатом в конкретной комнате
        public void ManageClimate(int roomIndex)
        {
            Room room = rooms[roomIndex];
            Console.WriteLine("Автоматическое управление климатом: " + room.name);

            // Находим все подходящие сценарии
            // (храним индексы — какой сценарий подошёл)
            int bestPriority = -1;
            int bestIndex = -1;

            for (int i = 0; i < scenarios.Length; i++)
            {
                bool active = IsScenarioActive(scenarios[i], room);
                if (active == true)
                {
                    // Приоритезация: берём самый важный сценарий
                    if (scenarios[i].priority > bestPriority)
                    {
                        bestPriority = scenarios[i].priority;
                        bestIndex = i;
                    }
                }
            }

            if (bestIndex == -1)
            {
                Console.WriteLine("  Все показатели в норме — действий не требуется.");
                Console.WriteLine();
                return;
            }

            // Применяем выбранный сценарий
            Scenario chosen = scenarios[bestIndex];
            Console.WriteLine("  Выбран сценарий: " + chosen.name
                + " (приоритет " + chosen.priority + ")");
            Console.WriteLine("  Действие: " + chosen.action);

            // Меняем устройства в комнате согласно сценарию
            for (int i = 0; i < room.devices.Length; i++)
            {
                Device d = room.devices[i];

                if (room.temperature < chosen.minTemp)
                {
                    if (d.type == DeviceType.Heater)
                    {
                        d.state = DeviceState.On;
                        Console.WriteLine("    Включён обогреватель.");
                    }
                    if (d.type == DeviceType.AirConditioner)
                    {
                        d.state = DeviceState.Off;
                    }
                }
                else if (room.temperature > chosen.maxTemp)
                {
                    if (d.type == DeviceType.AirConditioner)
                    {
                        d.state = DeviceState.On;
                        Console.WriteLine("    Включён кондиционер.");
                    }
                    if (d.type == DeviceType.Heater)
                    {
                        d.state = DeviceState.Off;
                    }
                }

                if (room.humidity > chosen.maxHumidity)
                {
                    if (d.type == DeviceType.Ventilation)
                    {
                        d.state = DeviceState.On;
                        Console.WriteLine("    Включена вентиляция.");
                    }
                }

                room.devices[i] = d;
            }

            rooms[roomIndex] = room;
            Console.WriteLine();
        }

        // Обнаружение нештатных ситуаций
        public bool DetectEmergency()
        {
            Console.WriteLine("Проверка нештатных ситуаций...");
            bool found = false;

            for (int i = 0; i < rooms.Length; i++)
            {
                Device[] devices = rooms[i].devices;

                for (int j = 0; j < devices.Length; j++)
                {
                    // Датчик дыма сработал -> пожар
                    if (devices[j].type == DeviceType.SmokeSensor
                        && devices[j].value > 0)
                    {
                        Console.WriteLine("  !!! ПОЖАР в комнате: " + rooms[i].name);
                        found = true;
                    }

                    // Датчик протечки сработал
                    if (devices[j].type == DeviceType.WaterSensor
                        && devices[j].value > 0)
                    {
                        Console.WriteLine("  !!! ПРОТЕЧКА в комнате: " + rooms[i].name);
                        found = true;
                    }

                    // Устройство в состоянии ошибки
                    if (devices[j].state == DeviceState.Error)
                    {
                        Console.WriteLine("  Устройство " + devices[j].type
                            + " в комнате " + rooms[i].name + " неисправно.");
                        found = true;
                    }
                }
            }

            if (found == false)
            {
                Console.WriteLine("  Нештатных ситуаций не обнаружено.");
            }

            Console.WriteLine();
            return found;
        }

        // Оптимизация энергопотребления: выключаем всё, что можно, когда никого нет
        public void OptimizeEnergy()
        {
            Console.WriteLine("Оптимизация энергопотребления...");

            if (residentsPresent == true)
            {
                Console.WriteLine("  Жильцы дома — сильная экономия не нужна.");
            }
            else
            {
                // Никого нет — гасим свет и климат
                for (int i = 0; i < rooms.Length; i++)
                {
                    Room room = rooms[i];
                    for (int j = 0; j < room.devices.Length; j++)
                    {
                        Device d = room.devices[j];

                        if (d.type == DeviceType.Light)
                        {
                            d.state = DeviceState.Off;
                        }
                        if (d.type == DeviceType.AirConditioner
                            || d.type == DeviceType.Heater)
                        {
                            // Оставляем только минимальный обогрев
                            d.state = DeviceState.Off;
                        }

                        room.devices[j] = d;
                    }
                    rooms[i] = room;
                }
                Console.WriteLine("  Свет и климат выключены — никого нет дома.");
            }

            // Общая экономия: считаем суммарное потребление
            int total = GetTotalPower();
            Console.WriteLine("  Текущее суммарное потребление: " + total + " Вт");
            Console.WriteLine();
        }

        // Суммарное потребление всего дома
        public int GetTotalPower()
        {
            int total = 0;
            for (int i = 0; i < rooms.Length; i++)
            {
                total = total + rooms[i].GetPowerUsage();
            }
            return total;
        }

        // Сбор статистики по дому
        public void CollectStatistics()
        {
            Console.WriteLine("=== СТАТИСТИКА ДОМА \"" + name + "\" ===");

            double totalTemp = 0;
            double totalHumidity = 0;
            int deviceCount = 0;
            int onCount = 0;

            for (int i = 0; i < rooms.Length; i++)
            {
                totalTemp = totalTemp + rooms[i].temperature;
                totalHumidity = totalHumidity + rooms[i].humidity;

                for (int j = 0; j < rooms[i].devices.Length; j++)
                {
                    deviceCount = deviceCount + 1;
                    if (rooms[i].devices[j].state == DeviceState.On)
                    {
                        onCount = onCount + 1;
                    }
                }
            }

            double avgTemp = totalTemp / rooms.Length;
            double avgHumidity = totalHumidity / rooms.Length;

            Console.WriteLine("  Комнат: " + rooms.Length);
            Console.WriteLine("  Устройств всего: " + deviceCount);
            Console.WriteLine("  Из них включено: " + onCount);
            Console.WriteLine("  Средняя температура: " + avgTemp + " °C");
            Console.WriteLine("  Средняя влажность: " + avgHumidity + " %");
            Console.WriteLine("  Общее потребление: " + GetTotalPower() + " Вт");
            Console.WriteLine();
        }

        // Полная печать состояния
        public void PrintStatus()
        {
            Console.WriteLine("=== ДОМ \"" + name + "\" ===");
            Console.WriteLine("Время: " + currentHour + ":00 (" + GetDayTime() + ")");
            Console.WriteLine("Жильцы дома: " + residentsPresent);
            Console.WriteLine();

            for (int i = 0; i < rooms.Length; i++)
            {
                rooms[i].PrintInfo();
            }

            Console.WriteLine("Сценарии:");
            for (int i = 0; i < scenarios.Length; i++)
            {
                scenarios[i].PrintInfo();
            }
        }
    }
}
class Program
{
    static void Main()
    {
        // Собираем экипаж
        CrewMember[] crew = new CrewMember[4];
        crew[0] = new CrewMember("Иван", CrewRole.Captain, 9, HealthStatus.Healthy);
        crew[1] = new CrewMember("Кирилл", CrewRole.Pilot, 8, HealthStatus.Healthy);
        crew[2] = new CrewMember("Родион", CrewRole.Engineer, 7, HealthStatus.Healthy);
        crew[3] = new CrewMember("Халал", CrewRole.Doctor, 6, HealthStatus.Healthy);

        // Создаём корабль
        Spaceship ship = new Spaceship("Белка-стрелка", 100, 500, crew);

        // Показываем начальное состояние
        ship.PrintStatus();
        Console.WriteLine();

        // Проверяем, можно ли лететь
        Console.WriteLine("Проверка готовности к миссии...");
        if (ship.CanStartMission() == true)
        {
            Console.WriteLine("Экипаж готов. Можно лететь к Марсу!");
        }
        else
        {
            Console.WriteLine("Экипаж не готов. Полёт отменён.");
        }
        Console.WriteLine();

        // Считаем, хватит ли топлива до Марса (пусть нужно 40000 единиц)
        int distanceToMars = 40000;
        int range = ship.CalculateRange();
        Console.WriteLine("Нужно долететь: " + distanceToMars);
        Console.WriteLine("Можем пролететь: " + range);

        if (range >= distanceToMars)
        {
            Console.WriteLine("Топлива достаточно!");
        }
        else
        {
            Console.WriteLine("Топлива НЕ хватает!");
        }
        Console.WriteLine();

        // По пути сталкиваемся с метеоритом
        ship.HitByMeteorite(12);

        // Пытаемся починить
        ship.Repair();

        // Лечим экипаж
        ship.HealCrew();

        // Ещё одно столкновение — посерьёзнее
        ship.HitByMeteorite(30);

        // Снова ремонт и лечение
        ship.Repair();
        ship.HealCrew();

        // Итоговое состояние
        ship.PrintStatus();








        // -------- Комната 1: Гостиная --------
        Device[] livingDevices = new Device[5];
        livingDevices[0] = new Device(DeviceType.Light, DeviceState.Off, 60, 0);
        livingDevices[1] = new Device(DeviceType.Heater, DeviceState.Off, 1500, 0);
        livingDevices[2] = new Device(DeviceType.AirConditioner, DeviceState.Off, 1200, 0);
        livingDevices[3] = new Device(DeviceType.SmokeSensor, DeviceState.On, 5, 0);
        livingDevices[4] = new Device(DeviceType.TemperatureSensor, DeviceState.On, 3, 0);

        Room living = new Room("Гостиная", 15.0, 45.0, livingDevices);

        // -------- Комната 2: Кухня --------
        Device[] kitchenDevices = new Device[4];
        kitchenDevices[0] = new Device(DeviceType.Light, DeviceState.On, 40, 0);
        kitchenDevices[1] = new Device(DeviceType.WaterSensor, DeviceState.On, 5, 1);
        kitchenDevices[2] = new Device(DeviceType.Ventilation, DeviceState.Off, 200, 0);
        kitchenDevices[3] = new Device(DeviceType.HumiditySensor, DeviceState.On, 3, 0);

        Room kitchen = new Room("Кухня", 22.0, 70.0, kitchenDevices);

        // -------- Комната 3: Спальня --------
        Device[] bedroomDevices = new Device[3];
        bedroomDevices[0] = new Device(DeviceType.Light, DeviceState.Off, 30, 0);
        bedroomDevices[1] = new Device(DeviceType.AirConditioner, DeviceState.Off, 900, 0);
        bedroomDevices[2] = new Device(DeviceType.HumiditySensor, DeviceState.On, 3, 0);

        Room bedroom = new Room("Спальня", 26.0, 50.0, bedroomDevices);

        Room[] rooms = new Room[3];
        rooms[0] = living;
        rooms[1] = kitchen;
        rooms[2] = bedroom;

        // -------- Сценарии --------
        Scenario[] scenarios = new Scenario[4];
        scenarios[0] = new Scenario(
            "Утренний обогрев", DayTime.Morning, true,
            18.0, 24.0, 60.0, 5, "Включить обогрев, если прохладно");

        scenarios[1] = new Scenario(
            "Дневное охлаждение", DayTime.Day, true,
            18.0, 24.0, 60.0, 4, "Включить кондиционер, если жарко");

        scenarios[2] = new Scenario(
            "Ночной режим", DayTime.Night, false,
            16.0, 22.0, 55.0, 3, "Минимум активности, слегка охладить");

        scenarios[3] = new Scenario(
            "Борьба с влажностью", DayTime.Any, false,
            10.0, 30.0, 60.0, 8, "Включить вентиляцию (высокий приоритет)");

        
        SmartHome home = new SmartHome("Дом", rooms, scenarios);

        // -------- Демонстрация --------
        home.PrintStatus();

        Console.WriteLine(">>> Управление климатом в гостиной <<<");
        home.currentHour = 8;                  // утро
        home.ManageClimate(0);

        Console.WriteLine(">>> Управление климатом на кухне <<<");
        home.currentHour = 14;                 // день
        home.ManageClimate(1);

        Console.WriteLine(">>> Управление климатом в спальне <<<");
        home.currentHour = 2;                  // ночь
        home.residentsPresent = false;         // никого нет
        home.ManageClimate(2);

        Console.WriteLine(">>> Обнаружение нештатных ситуаций <<<");
        home.DetectEmergency();

        Console.WriteLine(">>> Оптимизация энергопотребления <<<");
        home.OptimizeEnergy();

        Console.WriteLine(">>> Сбор статистики <<<");
        home.CollectStatistics();

        // -------- Демонстрация приоритезации сценариев --------
        Console.WriteLine(">>> Демонстрация приоритезации <<<");
        Console.WriteLine("Ситуация: утро, жильцы дома, но влажность 70%.");
        Console.WriteLine("Подходят ДВА сценария: 'Утренний обогрев' (5)"
            + " и 'Борьба с влажностью' (8).");
        Console.WriteLine("Побеждает тот, у кого выше приоритет.");

        home.currentHour = 8;
        home.residentsPresent = true;
        home.ManageClimate(1);  // кухня: влажность 70 -> сработает вентиляция

        Console.WriteLine(">>> Итоговое состояние дома <<<");
        home.PrintStatus();
    }
}
    