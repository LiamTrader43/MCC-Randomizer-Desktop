using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaloRuns;

namespace select
{
    public partial class Select
    {
        public void reset(ref string[] Names, ref int[] insert)
        {
            namesSetup(ref Names);
            insertSetup(ref insert);
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

        public void create(bool[] Missions, int[] insert, string[] Names, string difficulty, string output, string path)
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
