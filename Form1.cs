using Microsoft.VisualBasic.ApplicationServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.ComponentModel;
using System.Numerics;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using select;
using randomize;
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
        int[] insert = new int[65];

        //reads the path from the file "path.txt"
        string path = File.ReadAllText("resources/path.txt");

        //arrays used to track which missions will be added top the file in the randomizer
        //this one is used to keep track of missions in the randomizer
        string[] curMissions = new string[0];
        //this one checks for insertion points
        int[] curInsert = new int[0];

        //bools for which type of randomization is applied to the difficulty
        bool fullRand;
        bool semiRand;

        //this var is used for a while loop later
        bool finding = true;

        //the map names of ever mission in the game
        string[] Names = new string[0];

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

        //checks for seeing what games will be active in the randomizer
        bool[] activeGames = new bool[6];
        private void CE_CheckedChanged(object sender, EventArgs e)
        {
            activeGames[0] = !activeGames[0];
        }

        private void H2_CheckedChanged(object sender, EventArgs e)
        {
            activeGames[1] = !activeGames[1];
        }

        private void H3_CheckedChanged(object sender, EventArgs e)
        {
            activeGames[2] = !activeGames[2];
        }

        private void ODST_CheckedChanged(object sender, EventArgs e)
        {
            activeGames[3] = !activeGames[3];
        }

        private void Reach_CheckedChanged(object sender, EventArgs e)
        {
            activeGames[4] = !activeGames[4];
        }

        private void H4_CheckedChanged(object sender, EventArgs e)
        {
            activeGames[5] = !activeGames[5];
        }

        private void Make_Click(object sender, EventArgs e)
        {
            Select selectCode = new Select();
            selectCode.reset(ref Names, ref insert);
            selectCode.create(Missions, insert, Names, difficulty, output, path);
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
            Randomize randCode = new Randomize();
            randCode.reset(ref Names, activeGames, ref insert, ref curMissions, ref curInsert);
            randCode.choose(ref Names, ref curMissions, insert, ref curInsert, seed, numLevels);
            randCode.difficulty(semiRand, difficulty, seed);
            randCode.create(curMissions, difficulty, fullRand, curInsert, output, seed, path);
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