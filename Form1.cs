using Microsoft.VisualBasic.ApplicationServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.ComponentModel;
using System.Numerics;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
namespace HaloRuns
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            //sets the path you save your file to to the path in path.txt
            PathLabel.Text = path;
            //centers the path text shown in the settings tab
            PathLabel.Location = new Point(this.Width / 2 - PathLabel.Width / 2, 150);
        }

        //sets the base difficulty just in case an option is not selected
        string difficulty = "_campaign_difficulty_level_easy";

        //the basic text at the start of a playlist file
        string output =
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

        //a bool array to keep track of which missions are selected
        bool[] Missions = new bool[65];

        //this array is used for the mombasa streets insertion points
        bool[] hasInsert = new bool[65];
        //and this is the number that tells which insertion point to add
        int[] instert = new int[65];

        //reads the path from the file "path.txt"
        string path = File.ReadAllText("resources/path.txt");

        //arrays used to track which missions will be added top the file in the randomizer
        //this one is used to keep track of missions in the randomizer
        string[] curMissions = new string[0];
        //this one is used to make sure the same level isnt used twice
        string[] usedMissions = new string[0];
        //this one checks for insertion points
        int[] curInstert = new int[0];

        //bools for which type of randomization is applied to the difficulty
        bool fullRand;
        bool semiRand;

        //this var is used for a while loop later
        bool finding = true;

        //the map names of ever mission in the game
        string[] Names = new string[65] {
            //ce
            "Map id ='_map_id_halo1_pillar_of_autumn'",
            "Map id ='_map_id_halo1_halo'",
            "Map id ='_map_id_halo1_truth_and_reconciliation'",
            "Map id ='_map_id_halo1_silent_cartographer'",
            "Map id ='_map_id_halo1_assault_on_the_control_room'",
            "Map id ='_map_id_halo1_343_guilty_spark'",
            "Map id ='_map_id_halo1_the_library'",
            "Map id ='_map_id_halo1_two_betrayals'",
            "Map id ='_map_id_halo1_keyes'",
            "Map id ='_map_id_halo1_the_maw'",
            //h2
            "Map id ='_map_id_halo2_the_armory'",
            "Map id ='_map_id_halo2_cairo_station'",
            "Map id ='_map_id_halo2_outskirts'",
            "Map id ='_map_id_halo2_metropolis'",
            "Map id ='_map_id_halo2_the_arbiter'",
            "Map id ='_map_id_halo2_the_oracle'",
            "Map id ='_map_id_halo2_delta_halo'",
            "Map id ='_map_id_halo2_regret'",
            "Map id ='_map_id_halo2_sacred_icon'",
            "Map id ='_map_id_halo2_quarantine_zone'",
            "Map id ='_map_id_halo2_gravemind'",
            "Map id ='_map_id_halo2_uprising'",
            "Map id ='_map_id_halo2_high_charity'",
            "Map id ='_map_id_halo2_the_great_journey'",
            //h3
            "Map id ='_map_id_halo3_sierra_117'",
            "Map id ='_map_id_halo3_crows_nest'",
            "Map id ='_map_id_halo3_tsavo_highway'",
            "Map id ='_map_id_halo3_the_storm'",
            "Map id ='_map_id_halo3_floodgate'",
            "Map id ='_map_id_halo3_the_ark'",
            "Map id ='_map_id_halo3_the_covenant'",
            "Map id ='_map_id_halo3_cortana'",
            "Map id ='_map_id_halo3_halo'",
            //odst
            "Map id='_map_id_halo3odst_mombasa_streets'",
            "Map id='_map_id_halo3odst_tayari_plaza'",
            "Map id='_map_id_halo3odst_mombasa_streets'",
            "Map id='_map_id_halo3odst_uplift_reserve'",
            "Map id='_map_id_halo3odst_mombasa_streets'",
            "Map id='_map_id_halo3odst_kizingo_boulevard'",
            "Map id='_map_id_halo3odst_mombasa_streets'",
            "Map id='_map_id_halo3odst_oni_alpha_site'",
            "Map id='_map_id_halo3odst_mombasa_streets'",
            "Map id='_map_id_halo3odst_nmpd_hq'",
            "Map id='_map_id_halo3odst_mombasa_streets'",
            "Map id='_map_id_halo3odst_kikowani_station'",
            "Map id='_map_id_halo3odst_mombasa_streets'",
            "Map id='_map_id_halo3odst_data_hive'",
            "Map id='_map_id_halo3odst_coastal_highway'",
            // reach
            "Map id ='_map_id_haloreach_winter_contingency'",
            "Map id='_map_id_haloreach_oni_sword_base'",
            "Map id='_map_id_haloreach_nightfall'",
            "Map id='_map_id_haloreach_tip_of_the_spear'",
            "Map id='_map_id_haloreach_long_night_of_solace'",
            "Map id='_map_id_haloreach_exodus'",
            "Map id='_map_id_haloreach_new_alexandria'",
            "Map id='_map_id_haloreach_the_package'",
            "Map id='_map_id_haloreach_the_pillar_of_autumn'",
            "Map id='_map_id_halo4_dawn'",
            "Map id='_map_id_halo4_requiem'",
            "Map id='_map_id_halo4_forerunner'",
            "Map id='_map_id_halo4_infinity'",
            "Map id='_map_id_halo4_reclaimer'",
            "Map id='_map_id_halo4_shutdown'",
            "Map id='_map_id_halo4_composer'",
            "Map id='_map_id_halo4_midnight'"
                };

        #region levelChecks
        //these change the state of the bools in "missions" whenever you mess with the corresponding checkbox
        //CE
        private void PoAce_CheckedChanged(object sender, EventArgs e)
        {
            Missions[0] = !Missions[0];
        }

        private void Haloce_CheckedChanged(object sender, EventArgs e)
        {
            Missions[1] = !Missions[1];
        }

        private void TnR_CheckedChanged(object sender, EventArgs e)
        {
            Missions[2] = !Missions[2];
        }

        private void SC_CheckedChanged(object sender, EventArgs e)
        {
            Missions[3] = !Missions[3];
        }

        private void AotCR_CheckedChanged(object sender, EventArgs e)
        {
            Missions[4] = !Missions[4];
        }

        private void GS_CheckedChanged(object sender, EventArgs e)
        {
            Missions[5] = !Missions[5];
        }

        private void Library_CheckedChanged(object sender, EventArgs e)
        {
            Missions[6] = !Missions[6];
        }

        private void TB_CheckedChanged(object sender, EventArgs e)
        {
            Missions[7] = !Missions[7];
        }

        private void Keys_CheckedChanged(object sender, EventArgs e)
        {
            Missions[8] = !Missions[8];
        }

        private void Maw_CheckedChanged(object sender, EventArgs e)
        {
            Missions[9] = !Missions[9];
        }

        //H2
        private void Armory_CheckedChanged(object sender, EventArgs e)
        {
            Missions[10] = !Missions[10];
        }

        private void Cairo_CheckedChanged(object sender, EventArgs e)
        {
            Missions[11] = !Missions[11];
        }

        private void Outskirts_CheckedChanged(object sender, EventArgs e)
        {
            Missions[12] = !Missions[12];
        }

        private void Metropolis_CheckedChanged(object sender, EventArgs e)
        {
            Missions[13] = !Missions[13];
        }

        private void Arbiter_CheckedChanged(object sender, EventArgs e)
        {
            Missions[14] = !Missions[14];
        }

        private void Oracle_CheckedChanged(object sender, EventArgs e)
        {
            Missions[15] = !Missions[15];
        }

        private void DeltaHalo_CheckedChanged(object sender, EventArgs e)
        {
            Missions[16] = !Missions[16];
        }

        private void Regret_CheckedChanged(object sender, EventArgs e)
        {
            Missions[17] = !Missions[17];
        }

        private void SacredIcon_CheckedChanged(object sender, EventArgs e)
        {
            Missions[18] = !Missions[18];
        }

        private void qtZone_CheckedChanged(object sender, EventArgs e)
        {
            Missions[19] = !Missions[19];
        }

        private void Gravemind_CheckedChanged(object sender, EventArgs e)
        {
            Missions[20] = !Missions[20];
        }

        private void Uprising_CheckedChanged(object sender, EventArgs e)
        {
            Missions[21] = !Missions[21];
        }

        private void HighCharity_CheckedChanged(object sender, EventArgs e)
        {
            Missions[22] = !Missions[22];
        }

        private void GreatJorney_CheckedChanged(object sender, EventArgs e)
        {
            Missions[23] = !Missions[23];
        }

        //h3
        private void Sierra117_CheckedChanged(object sender, EventArgs e)
        {
            Missions[24] = !Missions[24];
        }

        private void CrowsNest_CheckedChanged(object sender, EventArgs e)
        {
            Missions[25] = !Missions[25];
        }

        private void Tsavo_CheckedChanged(object sender, EventArgs e)
        {
            Missions[26] = !Missions[26];
        }

        private void Storm_CheckedChanged(object sender, EventArgs e)
        {
            Missions[27] = !Missions[27];
        }

        private void FloodGate_CheckedChanged(object sender, EventArgs e)
        {
            Missions[28] = !Missions[28];
        }

        private void Ark_CheckedChanged(object sender, EventArgs e)
        {
            Missions[29] = !Missions[29];
        }

        private void Covenatnt_CheckedChanged(object sender, EventArgs e)
        {
            Missions[30] = !Missions[30];
        }

        private void Cortana_CheckedChanged(object sender, EventArgs e)
        {
            Missions[31] = !Missions[31];
        }

        private void Haloh3_CheckedChanged(object sender, EventArgs e)
        {
            Missions[32] = !Missions[32];
        }

        //odst
        private void Mombasa1_CheckedChanged(object sender, EventArgs e)
        {
            Missions[33] = !Missions[33];
        }

        private void TayariPlaza_CheckedChanged(object sender, EventArgs e)
        {
            Missions[34] = !Missions[34];
        }

        private void Mombasa2_CheckedChanged(object sender, EventArgs e)
        {
            Missions[35] = !Missions[35];
        }

        private void UpliftReserve_CheckedChanged(object sender, EventArgs e)
        {
            Missions[36] = !Missions[36];
        }

        private void Mombasa3_CheckedChanged(object sender, EventArgs e)
        {
            Missions[37] = !Missions[37];
        }

        private void kizingoBoulevard_CheckedChanged(object sender, EventArgs e)
        {
            Missions[38] = !Missions[38];
        }

        private void Mombasa4_CheckedChanged(object sender, EventArgs e)
        {
            Missions[39] = !Missions[39];
        }

        private void OniAlphaSite_CheckedChanged(object sender, EventArgs e)
        {
            Missions[40] = !Missions[40];
        }

        private void Mombasa5_CheckedChanged(object sender, EventArgs e)
        {
            Missions[41] = !Missions[41];
        }

        private void NMPDHQ_CheckedChanged(object sender, EventArgs e)
        {
            Missions[42] = !Missions[42];
        }

        private void Mombasa6_CheckedChanged(object sender, EventArgs e)
        {
            Missions[43] = !Missions[43];
        }

        private void KikowaniStation_CheckedChanged(object sender, EventArgs e)
        {
            Missions[44] = !Missions[44];
        }

        private void Mombasa7_CheckedChanged(object sender, EventArgs e)
        {
            Missions[45] = !Missions[45];
        }

        private void DataHive_CheckedChanged(object sender, EventArgs e)
        {
            Missions[46] = !Missions[46];
        }

        private void CoastalHighway_CheckedChanged(object sender, EventArgs e)
        {
            Missions[47] = !Missions[47];
        }

        //reach
        private void WinterContingency_CheckedChanged(object sender, EventArgs e)
        {
            Missions[48] = !Missions[48];
        }

        private void oniSwordBase_CheckedChanged(object sender, EventArgs e)
        {
            Missions[49] = !Missions[49];
        }

        private void nightfall_CheckedChanged(object sender, EventArgs e)
        {
            Missions[50] = !Missions[50];
        }

        private void tots_CheckedChanged(object sender, EventArgs e)
        {
            Missions[51] = !Missions[51];
        }

        private void lnos_CheckedChanged(object sender, EventArgs e)
        {
            Missions[52] = !Missions[52];
        }

        private void Exodus_CheckedChanged(object sender, EventArgs e)
        {
            Missions[53] = !Missions[53];
        }

        private void newAlexandria_CheckedChanged(object sender, EventArgs e)
        {
            Missions[54] = !Missions[54];
        }

        private void package_CheckedChanged(object sender, EventArgs e)
        {
            Missions[55] = !Missions[55];
        }

        private void PoAReach_CheckedChanged(object sender, EventArgs e)
        {
            Missions[56] = !Missions[56];
        }

        //h4
        private void Dawn_CheckedChanged(object sender, EventArgs e)
        {
            Missions[57] = !Missions[57];
        }

        private void Requiem_CheckedChanged(object sender, EventArgs e)
        {
            Missions[58] = !Missions[58];
        }

        private void Forerunner_CheckedChanged(object sender, EventArgs e)
        {
            Missions[59] = !Missions[59];
        }

        private void Infinity_CheckedChanged(object sender, EventArgs e)
        {
            Missions[60] = !Missions[60];
        }

        private void Reclaimer_CheckedChanged(object sender, EventArgs e)
        {
            Missions[61] = !Missions[61];
        }

        private void Shutdown_CheckedChanged(object sender, EventArgs e)
        {
            Missions[62] = !Missions[62];
        }

        private void Composer_CheckedChanged(object sender, EventArgs e)
        {
            Missions[63] = !Missions[63];
        }

        private void Midnight_CheckedChanged(object sender, EventArgs e)
        {
            Missions[64] = !Missions[64];
        }
        #endregion

        private void reset()
        {
            //sets the output back to the default whenever a button is pressed. if you dont do this if you make the file twice it will paste in the text twice.
            output = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <!--Please increment version number inside the MissionPlaylists tag if the list is updated.  The game uses version number to ensure coop players have same data and also save games will be based on this (The version inside XML tag is not used.)-->
            <MissionPlaylists version=""4"">
	            <Halo1 />
	            <Halo2 />
	            <Halo3 />
	            <Halo3ODST />
	            <HaloReach />
	            <Halo4 />
	            <CrossTitle>
		            <Playlist id=""hydraulic"" name=""Randomizer Playlist"" desc=""H4sIAAAAAAAAAzNkqGHIYKhkSGEoYkhkKGXIYchkSAaKGQCxIZSGsEGkH5ilx2DEYApmYUIDukIAM+Jwa74AAAA="" image=""CT_Setlist_Preview_01"" highestDiffID=""_campaign_difficulty_level_easy"" hasRallyPoints=""false"">
			            <MapList>
                            ";

            //resizes arrays and deletes the elements in them for the randomizer
            Array.Clear(curMissions, 0, curMissions.Length);
            Array.Resize(ref curMissions, 0);

            Array.Clear(usedMissions, 0, usedMissions.Length);
            Array.Resize(ref usedMissions, 0);

            Array.Clear(curInstert, 0, curInstert.Length);
            Array.Resize(ref curInstert, 0);

            //sets the mombasa streets levels insertion points
            hasInsert[35] = true; instert[35] = 1;
            hasInsert[37] = true; instert[37] = 2;
            hasInsert[39] = true; instert[39] = 3;
            hasInsert[41] = true; instert[41] = 4;
            hasInsert[43] = true; instert[43] = 5;
            hasInsert[45] = true; instert[45] = 6;

        }

        private void Make_Click(object sender, EventArgs e)
        {
            reset();

            //this loop goes through every bool in "Missions" and if it is true adds the value from "Names" to "Output"
            //there is also code to check for insertion points and if it has them tags it at the end of the string were adding
            for (int i = 0; i < 65; i++)
            {
                if (Missions[i] == true && hasInsert[i] == true)
                {
                    output = output + ("<" + Names[i] + " diffID='" + difficulty + "' insertionpoint='" + instert[i] + "'  />");
                }
                else
                {
                    if (Missions[i] == true)
                    {
                        output = output + ("<" + Names[i] + " diffID='" + difficulty + "'  />");
                    }
                }
            }
    
            //adds the code the close out the file to "Output"
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

        private void restore_Click(object sender, EventArgs e)
        {
            //places the contents of the original xml file to the file path
            File.Copy("resources/missionplaylistdb.xml", path, true);
        }

        //these two functions change the difficulty and modify a couple checkboxes to resemble the state of the difficuty
        private void Easy_CheckedChanged(object sender, EventArgs e)
        {
            difficulty = "_campaign_difficulty_level_easy";
            Legendary.Checked = false;

            SemiRandom.Checked = false;
            FullRand.Checked = false;
            RandLeg.Checked = false;

            fullRand = false;
            semiRand = false;
        }

        private void Legendary_CheckedChanged(object sender, EventArgs e)
        {
            difficulty = "_campaign_difficulty_level_impossible";
            Easy.Checked = false;

            SemiRandom.Checked = false;
            FullRand.Checked = false;
            RandEasy.Checked = false;

            fullRand = false;
            semiRand = false;
        }

        private void Randomize_Click(object sender, EventArgs e)
        {
            reset();
            
            //generates a random number with the seed you put between the range of 0 and 64
            int Seed = int.Parse(seed.Text);
            Random num = new Random(Seed);
            int numIndex = num.Next(0, 64);

            //generates a random number for the difficulty
            Random diff = new Random(Seed);
            int diffIndex = diff.Next(0, 2);

            //loops as many times as the number entered into the "number of levels" field in the randomize tab
            for (int i = 0; i < int.Parse(numLevels.Text); i++)
            {
                finding = true;
                //makes the length of "curMissions" = to the number of levels entered
                Array.Resize(ref curMissions, int.Parse(numLevels.Text));

                //this is where the finding variable comes into play
                //its a bool that remains true if the program selects a level thats already been used
                //once it finds a new level it sets to false ending the loop and starting the next iteration of the for loop
                while (finding)
                {
                    //pick a random number
                    numIndex = num.Next(0, 64);

                    //check if the level has been used already
                    if (!Array.Exists(curMissions, element => element == Names[numIndex]))
                    {
                        //if it hasnt stop the while loop and add the mission name to an array
                        finding = false;
                        curMissions[i] = Names[numIndex];
                        if (hasInsert[numIndex] == true)
                        {
                            curInstert[i] = instert[numIndex];
                        }
                    }
                }
            }

            //if the selected option was "random per playlist" it uses the random number generated earlier to determine the difficulty
            if (semiRand)
            {
                if (diffIndex == 0)
                {
                    difficulty = "_campaign_difficulty_level_impossible";
                }
                else
                {
                    difficulty = "_campaign_difficulty_level_easy";
                }
            }

            for (int i = 0; i < curMissions.Length; i++)
            {
                //this one uses the same variable as the semirand but changes the value to be random for every mission
                if (fullRand)
                {
                    diffIndex = diff.Next(0, 2);
                    if (diffIndex == 0)
                    {
                        difficulty = "_campaign_difficulty_level_impossible";
                    }
                    else
                    {
                        difficulty = "_campaign_difficulty_level_easy";
                    }
                }

                //checks for the mombasa streets insertion points
                if (curInstert[i] != 0)
                {
                    //if the level has insertion points it will add them to the end
                    output = output + ("<" + curMissions[i] + " diffID='" + difficulty + "' insertionpoint='" + instert[i] + "'  />");
                }
                else
                {
                    //if not it will add the standard mission code
                    output = output + ("<" + curMissions[i] + " diffID='" + difficulty + "'  />");
                }
            }

            //adds the code the close out the file to "Output"
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

        private void ChangePath_Click(object sender, EventArgs e)
        {
            //opens the menu to select a folder
            folderBrowserDialog1.ShowDialog();

            //checks that the user didnt select nothing as their path
            if (folderBrowserDialog1 != null)
            {
                //adds the location to the file we want to the end of the folder you select
                path = folderBrowserDialog1.SelectedPath + "\\data\\careerdb\\missionplaylistdb.xml";
                //writes the path into path.txt. it almost acts as a save file for your selected path
                File.WriteAllText("resources/path.txt", path);
                //updates the path sshown in the window
                PathLabel.Text = path;
                PathLabel.Location = new Point(this.Width / 2 - PathLabel.Width / 2, 150);
            }
        }

        //changes variables based on which random difficulty option you selected
        private void FullRand_CheckedChanged(object sender, EventArgs e)
        {
            SemiRandom.Checked = false;
            RandEasy.Checked = false;
            RandLeg.Checked = false;

            fullRand = true;
            semiRand = false;

        }

        private void SemiRandom_CheckedChanged(object sender, EventArgs e)
        {
            FullRand.Checked = false;
            RandEasy.Checked = false;
            RandLeg.Checked = false;

            fullRand = false;
            semiRand = true;
        }
    }
}