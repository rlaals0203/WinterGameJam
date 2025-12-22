using System.Collections.Generic;
using Code.Core;
using UnityEngine;

namespace Code.GameFlow
{
    struct InkData
    {
        public InkType InkType;
        public int Change;

        public InkData(InkType inkType, int change)
        {
            InkType = inkType;
            Change = change;
        }
    }

    public static class InkTable
    {
        private static List<InkData[]> Stage1Data = new List<InkData[]>
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

        private static List<InkData[]> Stage2Data = new List<InkData[]>
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

        private static List<InkData[]> Stage3Data = new List<InkData[]>
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

        private static List<InkData[]> Stage4Data = new List<InkData[]>
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