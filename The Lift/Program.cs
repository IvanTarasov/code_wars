using System;
using System.Collections.Generic;

namespace The_Lift
{
    class Program
    {
        public static void Main()
        {
            int[][] queues =
        {
           new int[0], // G
            new int[0], // 1
            new int[0], // 2
            new int[0], // 3
            new int[0], // 4
            new int[]{3,3,3,3,3,3}, // 5
            new int[0], // 6
        };

            var result = TheLift(queues, 5);
            foreach (var item in result)
            {
                Console.Write(item + "; ");
            }
            Console.ReadLine();
        }
        public static int[] TheLift(int[][] queues, int capacity)
        {
            House house = new House(queues);
            Lift lift = new Lift(capacity, house.Floors.Length - 1);

            while (house.CallsNumber > 0 || lift.FreeSpace < lift.Capacity)
            {
                bool canReport = false;

                if (lift.FreeSpace != lift.Capacity)
                {
                    if (lift.UnloadPeoples(lift.CurrentFloor))
                    {
                        Console.WriteLine("floor - " + lift.CurrentFloor + ": UNLOAD");

                        if (lift.NeedToGoBack)
                        {
                            lift.ReturnToPeople();
                            lift.NewReport();

                            continue;
                        }

                        canReport = true;
                    }
                }

                List<int> deletedPeoples = new List<int>();

                bool canUpdate = false;
                foreach (var peopleFloor in house.Floors[lift.CurrentFloor])
                {
                    Console.WriteLine("floor - " + lift.CurrentFloor + ": PEOPLE - " + peopleFloor);
                    if (InTheDirectionOfTheLift(peopleFloor, lift) && lift.FreeSpace > 0)
                    {
                        Console.WriteLine("floor - " + lift.CurrentFloor + ": LOAD - " + peopleFloor);
                        deletedPeoples.Add(peopleFloor);
                        lift.PeoplesInLift.Add(peopleFloor);

                        canReport = true;
                        canUpdate = true;
                    }
                    else if (InTheDirectionOfTheLift(peopleFloor, lift))
                    {
                        lift.NeedToGoBack = true;
                        lift.BackFloor = lift.CurrentFloor;
                    }
                }

                if (canUpdate)
                {
                    house.UpdateFloor(lift.CurrentFloor, deletedPeoples);
                }

                if (canReport)
                {
                    lift.NewReport();
                }
                lift.GoToNextFloor();
            }

            if (lift.VisitList[lift.VisitList.Count-1] != 0)
            {
                lift.VisitList.Add(0);
            }

            return lift.GetReport();
        }

        private static bool InTheDirectionOfTheLift(int peopleFloor, Lift lift)
        {
            return (peopleFloor > lift.CurrentFloor && lift.Direction == Lift.UP) || (peopleFloor < lift.CurrentFloor && lift.Direction == Lift.DOWN);
        }
    }

    class House
    {
        public int[][] Floors { get; private set; }
        public int CallsNumber { get; private set; }
        public House(int[][] queues)
        {
            Floors = queues;
            CallsNumber = CountCalls();
        }

        public void UpdateFloor(int floorIndex, List<int> deletedPeoples)
        {
            int[] floor = Floors[floorIndex];
            int[] newFloor = new int[floor.Length - deletedPeoples.Count];

            int count = 0;
            foreach (var people in floor)
            {
                foreach (var delPeople in deletedPeoples)
                {
                    if (people != delPeople)
                    {
                        newFloor[count] = people;
                        count++;
                    }
                }
            }

            Floors[floorIndex] = newFloor;
            CallsNumber -= deletedPeoples.Count;
        }

        private int CountCalls()
        {
            int calls = 0;
            foreach (var floor in Floors)
            {
                foreach (var people in floor)
                {
                    calls++;
                }
            }
            return calls;
        }
    }

    class Lift
    {
        public int Capacity { get; private set; }

        public List<int> PeoplesInLift { get; private set; }
        public List<int> VisitList = new List<int>() { 0 };

        public int CurrentFloor { get; private set; }
        private int MaxFloor;

        public const int UP = 1;
        public const int DOWN = -1;
        public int Direction { get; private set; }

        public bool NeedToGoBack { get; set; }
        public int BackFloor { get; set; }

        public int FreeSpace
        {
            get => Capacity - PeoplesInLift.Count;
        }

        public Lift(int capacity, int maxFloor)
        {
            PeoplesInLift = new List<int>();
            Capacity = capacity;
            Direction = UP;
            MaxFloor = maxFloor;
            NeedToGoBack = false;
        }

        public void SetPeople(int requiredFloor)
        {
            PeoplesInLift.Add(requiredFloor);
        }

        public bool UnloadPeoples(int floorNum)
        {
            if (PeoplesInLift.RemoveAll(IsRequiredFloor) > 0)
            {
                return true;
            }

            return false;

            bool IsRequiredFloor(int item)
            {
                return item.Equals(floorNum);
            }
        }

        public void ReturnToPeople()
        {
            CurrentFloor = BackFloor;
        }

        public void GoToNextFloor()
        {
            if (CurrentFloor == MaxFloor) TurnDirection();
            CurrentFloor += Direction;
        }

        public void TurnDirection()
        {
            if (Direction == UP) Direction = DOWN;
            else if (Direction == DOWN) Direction = UP;
        }

        public void NewReport()
        {
            VisitList.Add(CurrentFloor);
        }

        public int[] GetReport()
        {
            int[] report = new int[VisitList.Count];
            for (int visitNum = 0; visitNum < report.Length; visitNum++)
            {
                report[visitNum] = VisitList[visitNum];
            }
            return report;
        }
    }
}
