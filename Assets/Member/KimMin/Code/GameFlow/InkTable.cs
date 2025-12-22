using System.Collections.Generic;
using Code.Core;
using UnityEngine;

namespace Code.GameFlow
{ 
    public struct InkData
    {
        public InkType InkType;
        public int Chance;

        public InkData(InkType inkType, int chance)
        {
            InkType = inkType;
            Chance = chance;
        }
    }

    public class InkTable
    {
        public static List<InkData[]>[] StageDatas = new []
        {
            Stage1Data,
            Stage2Data,
            Stage3Data
        };
        
        public static InkType GetRandomInk(int stage, int area)
        {
            if (stage < 0 || stage >= StageDatas.Length)
                return InkType.None;

            var stageData = StageDatas[stage];

            if (area < 1 || area > stageData.Count)
                return InkType.None;

            var datas = stageData[area - 1];

            int total = 0;
            for (int i = 0; i < datas.Length; i++)
                total += datas[i].Chance;

            int rand = Random.Range(0, total);
            int acc = 0;

            for (int i = 0; i < datas.Length; i++)
            {
                acc += datas[i].Chance;
                if (rand < acc)
                    return datas[i].InkType;
            }

            return InkType.None;
        }
        
        public static List<InkData[]> Stage1Data = new List<InkData[]>
        {
            new InkData[]
            {
                new InkData(InkType.Green, 20),
                new InkData(InkType.White, 60),
                new InkData(InkType.Yellow, 20),
            },
            new InkData[]
            {
                new InkData(InkType.Green, 10),
                new InkData(InkType.White, 50),
                new InkData(InkType.Yellow, 40),
            },
            new InkData[]
            {
                new InkData(InkType.Green, 10),
                new InkData(InkType.White, 60),
                new InkData(InkType.Yellow, 30),
            },
            new InkData[]
            {
                new InkData(InkType.Green, 60),
                new InkData(InkType.Yellow, 40),
            },
            new InkData[]
            {
                new InkData(InkType.Green, 20),
                new InkData(InkType.White, 50),
                new InkData(InkType.Red, 30),
            },
            new InkData[]
            {
                new InkData(InkType.Green, 30),
                new InkData(InkType.White, 30),
                new InkData(InkType.Yellow, 40),
            },
            new InkData[]
            {
                new InkData(InkType.Green, 30),
                new InkData(InkType.Yellow, 70),
            },
            new InkData[]
            {
                new InkData(InkType.White, 10),
                new InkData(InkType.Red, 30),
                new InkData(InkType.Yellow, 60),
            },
            new InkData[]
            {
                new InkData(InkType.Green, 90),
                new InkData(InkType.Yellow, 10),
            }
        };

        public static List<InkData[]> Stage2Data = new List<InkData[]>
        {
            new InkData[]
            {
                new InkData(InkType.Blue, 30),
                new InkData(InkType.Yellow, 20),
                new InkData(InkType.Black, 30),
                new InkData(InkType.White, 20),
            },

            new InkData[]
            {
                new InkData(InkType.Blue, 10),
                new InkData(InkType.Yellow, 10),
                new InkData(InkType.White, 80),
            },

            new InkData[]
            {
                new InkData(InkType.Blue, 30),
                new InkData(InkType.Yellow, 50),
                new InkData(InkType.Green, 20),
            },

            new InkData[]
            {
                new InkData(InkType.Blue, 20),
                new InkData(InkType.Yellow, 20),
                new InkData(InkType.Black, 40),
                new InkData(InkType.White, 20),
            },

            new InkData[]
            {
                new InkData(InkType.Blue, 20),
                new InkData(InkType.Yellow, 20),
                new InkData(InkType.White, 60),
            },

            new InkData[]
            {
                new InkData(InkType.Blue, 20),
                new InkData(InkType.Green, 60),
                new InkData(InkType.Black, 20),
            },

            new InkData[]
            {
                new InkData(InkType.Blue, 50),
                new InkData(InkType.Black, 50),
            },

            new InkData[]
            {
                new InkData(InkType.Blue, 10),
                new InkData(InkType.Green, 30),
                new InkData(InkType.Black, 40),
                new InkData(InkType.White, 20),
            },

            new InkData[]
            {
                new InkData(InkType.Blue, 20),
                new InkData(InkType.Green, 80),
            },
        };

        public static List<InkData[]> Stage3Data = new List<InkData[]>
        {
            new InkData[]
            {
                new InkData(InkType.Black, 80),
                new InkData(InkType.Blue, 10),
                new InkData(InkType.White, 10),
            },
            new InkData[]
            {
                new InkData(InkType.Black, 30),
                new InkData(InkType.Blue, 60),
                new InkData(InkType.Yellow, 10),
            },
            new InkData[]
            {
                new InkData(InkType.Black, 30),
                new InkData(InkType.Blue, 40),
                new InkData(InkType.Yellow, 30),
            },
            new InkData[]
            {
                new InkData(InkType.Black, 70),
                new InkData(InkType.White, 30),
            },
            new InkData[]
            {
                new InkData(InkType.Yellow, 20),
                new InkData(InkType.White, 80),
            },
            new InkData[]
            {
                new InkData(InkType.Blue, 30),
                new InkData(InkType.Yellow, 50),
                new InkData(InkType.White, 20),
            },
            new InkData[]
            {
                new InkData(InkType.Black, 90),
                new InkData(InkType.Yellow, 10),
            },
            new InkData[]
            {
                new InkData(InkType.Black, 10),
                new InkData(InkType.Yellow, 90),
            },
            new InkData[]
            {
                new InkData(InkType.Yellow, 80),
                new InkData(InkType.White, 20),
            }
        };

        public static List<InkData[]> Stage4Data = new List<InkData[]>
        {
            new InkData[]
            {
                new InkData(InkType.Red, 25),
                new InkData(InkType.Blue, 25),
                new InkData(InkType.Yellow, 25),
                new InkData(InkType.Black, 25),
            },
            new InkData[]
            {
                new InkData(InkType.Red, 25),
                new InkData(InkType.Blue, 25),
                new InkData(InkType.Yellow, 25),
                new InkData(InkType.Black, 25),
            },
            new InkData[]
            {
                new InkData(InkType.Red, 20),
                new InkData(InkType.Blue, 20),
                new InkData(InkType.Yellow, 20),
                new InkData(InkType.Black, 40),
            },
            new InkData[]
            {
                new InkData(InkType.Blue, 30),
                new InkData(InkType.Yellow, 40),
                new InkData(InkType.Black, 30),
            },
            new InkData[]
            {
                new InkData(InkType.Blue, 20),
                new InkData(InkType.Green, 40),
                new InkData(InkType.Yellow, 40),
            },
            new InkData[]
            {
                new InkData(InkType.Blue, 60),
                new InkData(InkType.Black, 40),
            },
            new InkData[]
            {
                new InkData(InkType.Red, 60),
                new InkData(InkType.Yellow, 20),
                new InkData(InkType.Black, 20),
            },
            new InkData[]
            {
                new InkData(InkType.Blue, 20),
                new InkData(InkType.Green, 10),
                new InkData(InkType.Black, 70),
            },
            new InkData[]
            {
                new InkData(InkType.Red, 30),
                new InkData(InkType.Green, 30),
                new InkData(InkType.Yellow, 40),
            }
        };
    }
}