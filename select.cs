using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HaloRuns;

namespace select
{
    internal class Select
    {
        public void reset(ref string[] Names, ref int[] insert, ref string output)
        {
            namesSetup(ref Names);
            insertSetup(ref insert);

            output =
            $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <!--Please increment version number inside the MissionPlaylists tag if the list is updated.  The game uses version number to ensure coop players have same data and also save games will be based on this (The version inside XML tag is not used.)-->
            <MissionPlaylists version=""4"">
	            <Halo1 />
	            <Halo2 />
	            <Halo3 />
	            <Halo3ODST />
	            <HaloReach />
	            <Halo4 />
	            <CrossTitle>
		            <Playlist id=""hydraulic"" name=""Randomizer Playlist"" desc=""H4sIAAAAAAAAAzNkqGHIYKhkSGEoYkhkKGXIYchkSAaKGQCxIZSGsEGkH5ilx2DEYApmYUIDukIAM+Jwa74AAAA="" image=""CT_Setlist_Preview_01"" highestDiffID=""_campaign_difficulty_level_legendary"" hasRallyPoints=""false"">
			            <MapList>
                            ";
        }

        private void add(string filename, ref string[] Names, int start, int end)
        {
            using (var reader = new StreamReader(filename))
            {
                for (start = start; end < 10; start++)
                {
                    Names[start] = reader.ReadLine();
                }
            }
        }

        public void namesSetup(ref string[] Names)
        {
            Array.Clear(Names, 0, Names.Length);
            Array.Resize(ref Names, 65);

            string filename = "resources/missions/CE.txt";
            using (var reader = new StreamReader(filename))
            {
                for (int i = 0; i < 10; i++)
                {
                    Names[i] = reader.ReadLine();
                }
            }

            filename = "resources/missions/H2.txt";
            using (var reader = new StreamReader(filename))
            {
                for (int i = 10; i < 24; i++)
                {
                    Names[i] = reader.ReadLine();
                }
            }

            filename = "resources/missions/H3.txt";
            using (var reader = new StreamReader(filename))
            {
                for (int i = 24; i < 33; i++)
                {
                    Names[i] = reader.ReadLine();
                }
            }

            filename = "resources/missions/ODST.txt";
            using (var reader = new StreamReader(filename))
            {
                for (int i = 33; i < 48; i++)
                {
                    Names[i] = reader.ReadLine();
                }
            }

            filename = "resources/missions/Reach.txt";
            using (var reader = new StreamReader(filename))
            {
                for (int i = 48; i < 57; i++)
                {
                    Names[i] = reader.ReadLine();
                }
            }

            filename = "resources/missions/H4.txt";
            using (var reader = new StreamReader(filename))
            {
                for (int i = 57; i < 65; i++)
                {
                    Names[i] = reader.ReadLine();
                }
            }
        }

        public void insertSetup(ref int[] insert)
        {
            Array.Clear(insert, 0, insert.Length);
            Array.Resize(ref insert, 65);
            
            insert[35] = 1;
            insert[37] = 2;
            insert[39] = 3;
            insert[41] = 4;
            insert[43] = 5;
            insert[45] = 6;
        }

        public void shuffleMissions(bool[] Missions, int[] insert, string[] Names, string difficulty, ref string output)
        {
            bool finding = false;

            int number = 0;
            for (int i = 0; i < 65; i++)
            {
                if (Missions[i]) { number++; }
            }
            string[] shuffledMissions = new string[number];

            for (int i = 0; i < 65; i++)
            {
                Random random = new Random();

                if (Missions[i] == true && insert[i] != 0)
                {
                    finding = true;

                    while (finding)
                    {
                        finding = true;
                        int randIndex = random.Next(0, number);

                        if (shuffledMissions[randIndex] == null)
                        {
                            finding = false;
                            shuffledMissions[randIndex] = ("<" + Names[i] + " diffID='" + difficulty + "' insertionpoint='" + insert[i] + "'  />");
                        }
                    }
                }
                else
                {
                    if (Missions[i] == true)
                    {
                        finding = true;

                        while (finding)
                        {
                            finding = true;
                            int randIndex = random.Next(0, number);

                            if (shuffledMissions[randIndex] == null)
                            {
                                finding = false;
                                shuffledMissions[randIndex] = ("<" + Names[i] + " diffID='" + difficulty + "'  />");
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < number; i++)
            {
                output = output + shuffledMissions[i];
            }
        }

        public void create(bool[] Missions, int[] insert, string[] Names, string difficulty, ref string output, string path, bool shuffle)
        {
            if (!shuffle)
            {
                for (int i = 0; i < 65; i++)
                {
                    if (Missions[i] == true && insert[i] != 0)
                    {
                        output = output + ("<" + Names[i] + " diffID='" + difficulty + "' insertionpoint='" + insert[i] + "'  />");
                    }
                    else
                    {
                        if (Missions[i] == true)
                        {
                            output = output + ("<" + Names[i] + " diffID='" + difficulty + "'  />");
                        }
                    }
                }
            }
            else
            {
                shuffleMissions(Missions, insert, Names, difficulty, ref output);
            }

            output = output +
                        $@"</MapList>
		            </Playlist>
	            </CrossTitle>
	            <Extras>
		            <!-- Status: 0 = greyed out, 1 = visible, 2 = hidden, 3 = invalid -->
		            <Halo5Beta status=""0"" />
	            </Extras>
            </MissionPlaylists>
            ";

            File.WriteAllText(path, output);
        }
    }
}
