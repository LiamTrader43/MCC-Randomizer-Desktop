using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace randomize
{
    internal class Randomize
    {
        public void reset(ref string[] Names, bool[] activeGames, ref int[] insert, ref string[] curMissions, ref int[] curInsert)
        {
            addNames(ref Names, activeGames, ref insert);

            Array.Clear(curMissions, 0, curMissions.Length);
            Array.Resize(ref curMissions, 0);

            Array.Clear(curInsert, 0, curInsert.Length);
            Array.Resize(ref curInsert, 0);

        }

        public void addNames(ref string[] Names, bool[] activeGames, ref int[] insert)
        {
            int index = 0;
            int pastIndex;
            string filename;

            Array.Clear(Names, 0, Names.Length);
            Array.Clear(insert, 0, insert.Length);

            if (activeGames[0])
            {
                pastIndex = index;
                index += 10;
                Array.Resize(ref Names, index);
                Array.Resize(ref insert, index);
                filename = "resources/missions/CE.txt";
                using (var reader = new StreamReader(filename))
                {
                    for (int i = pastIndex; i < index; i++)
                    {
                        Names[i] = reader.ReadLine();
                    }
                }
            }

            if (activeGames[1])
            {
                pastIndex = index;
                index += 14;
                Array.Resize(ref Names, index);
                Array.Resize(ref insert, index);
                filename = "resources/missions/H2.txt";
                using (var reader = new StreamReader(filename))
                {
                    for (int i = pastIndex; i < index; i++)
                    {
                        Names[i] = reader.ReadLine();
                    }
                }
            }

            if (activeGames[2])
            {
                pastIndex = index;
                index += 9;
                Array.Resize(ref Names, index);
                Array.Resize(ref insert, index);
                filename = "resources/missions/H3.txt";
                using (var reader = new StreamReader(filename))
                {
                    for (int i = pastIndex; i < index; i++)
                    {
                        Names[i] = reader.ReadLine();
                    }
                }
            }

            if (activeGames[3])
            {
                pastIndex = index;
                index += 15;
                Array.Resize(ref Names, index);
                Array.Resize(ref insert, index);
                filename = "resources/missions/ODST.txt";
                using (var reader = new StreamReader(filename))
                {
                    for (int i = pastIndex; i < index; i++)
                    {
                        Names[i] = reader.ReadLine();
                    }
                }
                insert[pastIndex + 2] =  1;
                insert[pastIndex + 4] =  2;
                insert[pastIndex + 6] =  3;
                insert[pastIndex + 8] =  4;
                insert[pastIndex + 10] = 5;
                insert[pastIndex + 12] = 6;
            }

            if (activeGames[4])
            {
                pastIndex = index;
                index += 9;
                Array.Resize(ref Names, index);
                Array.Resize(ref insert, index);
                filename = "resources/missions/Reach.txt";
                using (var reader = new StreamReader(filename))
                {
                    for (int i = pastIndex; i < index; i++)
                    {
                        Names[i] = reader.ReadLine();
                    }
                }
            }

            if (activeGames[5])
            {
                pastIndex = index;
                index += 10;
                Array.Resize(ref Names, index);
                Array.Resize(ref insert, index);
                filename = "resources/missions/H4.txt";
                using (var reader = new StreamReader(filename))
                {
                    for (int i = pastIndex; i < index; i++)
                    {
                        Names[i] = reader.ReadLine();
                    }
                }
            }
        }

        public void choose(ref string[] Names, ref string[] curMissions, int[] insert, ref int[] curInsert, TextBox seed, TextBox numLevels)
        {
            Array.Resize(ref curMissions, int.Parse(numLevels.Text));
            Array.Resize(ref curInsert, int.Parse(numLevels.Text));

            bool finding = true;
            int Seed = int.Parse(seed.Text);
            Random num = new Random(Seed);
            int numIndex = num.Next(0, Names.Length);

            for (int i = 0; i < int.Parse(numLevels.Text); i++)
            {
                finding = true;

                while (finding)
                {
                    numIndex = num.Next(0, Names.Length);

                    if (!curMissions.Contains(Names[numIndex]))
                    {
                        finding = false;
                        curMissions[i] = Names[numIndex];
                        if (insert[numIndex] != 0)
                        {
                            curInsert[i] = insert[numIndex];
                        }
                    }
                }
            }
        }

        public void RandDiff(string difficulty, TextBox Seed)
        {
                Random diff = new Random(int.Parse(Seed.Text));
                int diffIndex = diff.Next(0, 4);

                if (diffIndex == 0)
                {
                    difficulty = "_campaign_difficulty_level_impossible";
                }
                else if (diffIndex == 1)
                {
                    difficulty = "_campaign_difficulty_level_easy";
                }
                else if (diffIndex == 2)
                {
                    difficulty = "_campaign_difficulty_level_normal";
                }
                else if (diffIndex == 3)
                {
                    difficulty = "_campaign_difficulty_level_heroic";
                }
        }

        public void create(string[] curMissions, string difficulty, bool fullRand, int[] curInsert, string output, TextBox Seed, string path)
        {
            Random diff = new Random(int.Parse(Seed.Text));
            int diffIndex = diff.Next(0, 4);

            for (int i = 0; i < curMissions.Length; i++)
            {
                //this one uses the same variable as the semirand but changes the value to be random for every mission
                if (fullRand)
                {
                    diffIndex = diff.Next(0, 4);

                    if (diffIndex == 0)
                    {
                        difficulty = "_campaign_difficulty_level_impossible";
                    }
                    else if (diffIndex == 1)
                    {
                        difficulty = "_campaign_difficulty_level_easy";
                    }
                    else if (diffIndex == 2)
                    {
                        difficulty = "_campaign_difficulty_level_normal";
                    }
                    else if (diffIndex == 3)
                    {
                        difficulty = "_campaign_difficulty_level_heroic";
                    }
                }

                //checks for the mombasa streets insertion points
                if (curInsert[i] != 0)
                {
                    //if the level has insertion points it will add them to the end
                    output = output + ("<" + curMissions[i] + " diffID='" + difficulty + "' insertionpoint='" + curInsert[i] + "'  />");
                }
                else
                {
                    //if not it will add the standard mission code
                    output = output + ("<" + curMissions[i] + " diffID='" + difficulty + "'  />");
                }
            }
            
            output = output +
                        $@"</MapList>
		            </Playlist>
	            </CrossTitle>
	            <Extras>
		            <!-- Status: 0 = greyed out, 1 = visible, 2 = hidden, 3 = invalid -->
		            <Halo5Beta status=""0"" />
	            </Extras>
            </MissionPlaylists>";

            //writes the data in output to the path youve selected
            File.WriteAllText(path, output);
        }
    }
}
