using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ItemDescriptionEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            richTextBox1.ReadOnly = true;
            richTextBox2.ReadOnly = true;

            textBox1.TextChanged += NonNegativeIntegerTextBox_TextChanged;
            textBox2.TextChanged += NonNegativeIntegerTextBox_TextChanged;
            textBox3.TextChanged += NonNegativeIntegerTextBox_TextChanged;
            textBox4.TextChanged += NonNegativeIntegerTextBox_TextChanged;
            textBox5.TextChanged += NonNegativeIntegerTextBox_TextChanged;
            textBox6.TextChanged += NonNegativeIntegerTextBox_TextChanged;
            textBox7.TextChanged += NonNegativeIntegerTextBox_TextChanged;
            textBox8.TextChanged += NonNegativeIntegerTextBox_TextChanged;
            textBox9.TextChanged += NonNegativeIntegerTextBox_TextChanged;
            textBox10.TextChanged += NonNegativeIntegerTextBox_TextChanged;
            textBox11.TextChanged += NonNegativeIntegerTextBox_TextChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void NonNegativeIntegerTextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!int.TryParse(textBox.Text, out int result) || result < 0)
            {
                if (textBox.Text.Length > 1)
                {
                    textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                }
                else
                {
                    textBox.Text = "";
                }

                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void GenerateStatsDescription(string statPrefix, TextBox textBox, StringBuilder result)
        {
            string value = textBox.Text.Trim();

            if (!string.IsNullOrEmpty(value))
            {
                // Split the input into segments with digits and non-digits
                var segments = Regex.Split(value, @"(\d+)");

                for (int i = 0; i < segments.Length; i++)
                {
                    string segment = segments[i].Trim();

                    if (!string.IsNullOrEmpty(segment))
                    {
                        // Append the stat prefix and value with a space between them
                        result.Append($"{statPrefix} {segment}");

                        // Add a comma and space if it's not the last element
                        if (i < segments.Length - 1)
                        {
                            result.Append(", ");
                        }
                    }
                }
            }
        }

        private void GenerateBasicDescriptionandScript(string statName, TextBox textBox, string delimiter, StringBuilder result)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                result.AppendLine($"{statName}{textBox.Text}{delimiter}");
            }
        }

        private void GenerateDescriptionAndScript(bool isChecked, string bonusType, TextBox textBox, StringBuilder result)
        {
            string bonusDescription = $"{bonusType}{(isChecked ? "-" : "")}";
            GenerateBasicDescriptionandScript(bonusDescription, textBox, ";", result);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Clear the RichTextBoxes
            richTextBox1.Clear();
            richTextBox2.Clear();

            StringBuilder result1 = new StringBuilder(); // STATS Description
            StringBuilder result2 = new StringBuilder(); // STATS Script
            StringBuilder result3 = new StringBuilder(); // HPSP Description

            GenerateStatsDescription(checkBox1.Checked ? "STR -" : "STR +", textBox1, result1);
            GenerateStatsDescription(checkBox2.Checked ? "AGI -" : "AGI +", textBox2, result1);
            GenerateStatsDescription(checkBox3.Checked ? "VIT -" : "VIT +", textBox3, result1);
            GenerateStatsDescription(checkBox4.Checked ? "INT -" : "INT +", textBox4, result1);
            GenerateStatsDescription(checkBox5.Checked ? "DEX -" : "DEX +", textBox5, result1);
            GenerateStatsDescription(checkBox6.Checked ? "LUK -" : "LUK +", textBox6, result1);
            GenerateStatsDescription(checkBox7.Checked ? "ALL STATS -" : "ALL STATS +", textBox7, result1);

            GenerateBasicDescriptionandScript("\"HP + ", textBox8, ".\",", result3);
            GenerateBasicDescriptionandScript("\"MAX HP + ", textBox9, "%.\",", result3);
            GenerateBasicDescriptionandScript("\"SP + ", textBox10, ".\",", result3);
            GenerateBasicDescriptionandScript("\"MAX SP + ", textBox11, "%.\",", result3);

            GenerateBasicDescriptionandScript("\"Base attack power + ", textBox12, ".\",", result3); //bonus bBaseAtk,n;
            GenerateBasicDescriptionandScript("\"Increase attack by ", textBox13, ".\",", result3); //bonus bAtk,n; 
            GenerateBasicDescriptionandScript("\"Increate attack2 by ", textBox14, ".\",", result3); //bonus bAtk2,n; 
            GenerateBasicDescriptionandScript("\"Increase attack rate by ", textBox15, "%.\",", result3); //ATK + VALUE% that won't interfere with Damage modifier and SC_EDP (renewal mode only)
            GenerateBasicDescriptionandScript("\"Increase weapon attack rate by ", textBox16, "%.\",", result3); //Weapon ATK + VALUE%
            GenerateBasicDescriptionandScript("\"Magic attack + ", textBox17, ".\",", result3); //Magical attack power + VALUE
            GenerateBasicDescriptionandScript("\"Increase magic attack by ", textBox18, "%.\",", result3); //Magical attack power + VALUE%
            GenerateBasicDescriptionandScript("\"Increase weapon magic attack by ", textBox19, "%.\",", result3); //Weapon Magical ATK + VALUE% (renewal mode only)
            GenerateBasicDescriptionandScript("\"Increase defense by ", textBox20, ".\",", result3); //Equipment DEF + VALUE
            GenerateBasicDescriptionandScript("\"Increase defense by ", textBox21, "%.\",", result3); //Equipment DEF + VALUE%
            GenerateBasicDescriptionandScript("\"Increase defense based on VIT by ", textBox22, ".\",", result3); //VIT based DEF + VALUE
            GenerateBasicDescriptionandScript("\"Increase defense based on VIT by ", textBox23, "%.\",", result3); //VIT based DEF + VALUE%
            GenerateBasicDescriptionandScript("\"MDEF + ", textBox24, ".\",", result3); //Equipment MDEF + VALUE
            GenerateBasicDescriptionandScript("\"MDEF + ", textBox25, "%.\",", result3); //Equipment MDEF + VALUE%
            GenerateBasicDescriptionandScript("\"MDEF based on INT + ", textBox26, ".\",", result3); //INT based MDEF + VALUE
            GenerateBasicDescriptionandScript("\"MDEF based on INT + ", textBox27, "%.\",", result3); //INT based MDEF + VALUE%

            string combinedResultStats = "\"" + result1.ToString().TrimEnd(',', ' ') + ".\",\n";

            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" || textBox6.Text != "" || textBox7.Text != "")
            {
                richTextBox1.AppendText(combinedResultStats);
            }
            richTextBox1.AppendText(result3.ToString());








            //-------------------------------------------- GENERATING ITEM SCRIPTS

            //GenerateBasicDescriptionandScript(checkBox1.Checked ? "      bonus bStr,-" : "      bonus bStr,", textBox1, ";", result2);
            //GenerateBasicDescriptionandScript(checkBox2.Checked ? "      bonus bAgi,-" : "      bonus bAgi,", textBox2, ";", result2);
            //GenerateBasicDescriptionandScript(checkBox3.Checked ? "      bonus bVit,-" : "      bonus bVit,", textBox3, ";", result2);
            //GenerateBasicDescriptionandScript(checkBox4.Checked ? "      bonus bInt,-" : "      bonus bInt,", textBox4, ";", result2);
            //GenerateBasicDescriptionandScript(checkBox5.Checked ? "      bonus bDex,-" : "      bonus bDex,", textBox5, ";", result2);
            //GenerateBasicDescriptionandScript(checkBox6.Checked ? "      bonus bLuk,-" : "      bonus bLuk,", textBox6, ";", result2);
            //GenerateBasicDescriptionandScript(checkBox7.Checked ? "      bonus bAllStats,-" : "      bonus bAllStats,", textBox7, ";", result2);

            GenerateDescriptionAndScript(checkBox1.Checked, "bonus bStr,", textBox1, result2);
            GenerateDescriptionAndScript(checkBox2.Checked, "bonus bAgi,", textBox2, result2);
            GenerateDescriptionAndScript(checkBox3.Checked, "bonus bVit,", textBox3, result2);
            GenerateDescriptionAndScript(checkBox4.Checked, "bonus bInt,", textBox4, result2);
            GenerateDescriptionAndScript(checkBox5.Checked, "bonus bDex,", textBox5, result2);
            GenerateDescriptionAndScript(checkBox6.Checked, "bonus bLuk,", textBox6, result2);
            GenerateDescriptionAndScript(checkBox7.Checked, "bonus bAllStats,", textBox7, result2);


            GenerateBasicDescriptionandScript("      bonus bMaxHP,", textBox8, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMaxHPrate,", textBox9, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMaxSP,", textBox10, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMaxSPrate,", textBox11, ";", result2);

            GenerateBasicDescriptionandScript("      bonus bBaseAtk,", textBox12, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bAtk,", textBox13, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bAtk2,", textBox14, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bAtkRate,", textBox15, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bWeaponAtkRate,", textBox16, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMatk,", textBox17, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMatkRate,", textBox18, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bWeaponMatkRate,", textBox19, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bDef,", textBox20, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bDefRate,", textBox21, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bDef2,", textBox22, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bDef2Rate,", textBox23, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMdef,", textBox24, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMdefRate,", textBox25, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMdef2,", textBox26, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMdef2Rate,", textBox27, ";", result2);

            // Append the result to richTextBox2
            richTextBox2.AppendText(result2.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.Copy();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox2.SelectAll();
            richTextBox2.Copy();
        }
    }
}
