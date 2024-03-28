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

            // Attach the event handler to each numeric TextBox
            AttachTextChangedEvent(textBox1, label1, checkBox1);
            AttachTextChangedEvent(textBox2, label2, checkBox2);
            AttachTextChangedEvent(textBox3, label3, checkBox3);
            AttachTextChangedEvent(textBox4, label4, checkBox4);
            AttachTextChangedEvent(textBox5, label5, checkBox5);
            AttachTextChangedEvent(textBox6, label6, checkBox6);
            AttachTextChangedEvent(textBox7, label7, checkBox7);
            AttachTextChangedEvent(textBox8, label8, checkBox8);
            AttachTextChangedEvent(textBox9, label9, checkBox9);
            AttachTextChangedEvent(textBox10, label10, checkBox10);
            AttachTextChangedEvent(textBox11, label11, checkBox11);
            AttachTextChangedEvent(textBox12, label12, checkBox12);
            AttachTextChangedEvent(textBox13, label13, checkBox13);
            AttachTextChangedEvent(textBox14, label14, checkBox14);
            AttachTextChangedEvent(textBox15, label15, checkBox15);
            AttachTextChangedEvent(textBox16, label16, checkBox16);
            AttachTextChangedEvent(textBox17, label17, checkBox17);
            AttachTextChangedEvent(textBox18, label18, checkBox18);
            AttachTextChangedEvent(textBox19, label19, checkBox19);
            AttachTextChangedEvent(textBox20, label20, checkBox20);
            AttachTextChangedEvent(textBox21, label21, checkBox21);
            AttachTextChangedEvent(textBox22, label22, checkBox22);
            AttachTextChangedEvent(textBox23, label23, checkBox23);
            AttachTextChangedEvent(textBox24, label24, checkBox24);
            AttachTextChangedEvent(textBox25, label25, checkBox25);
            AttachTextChangedEvent(textBox26, label26, checkBox26);
            AttachTextChangedEvent(textBox27, label27, checkBox27);
        }

        private void AttachTextChangedEvent(TextBox textBox, Label label, System.Windows.Forms.CheckBox checkBox)
        {
            // Attach the event handler to the TextChanged event of the TextBox
            textBox.TextChanged += (sender, e) => NonNegativeIntegerTextBox_TextChanged(sender, e, label, checkBox);
        }

        private void NonNegativeIntegerTextBox_TextChanged(object sender, EventArgs e, Label associatedLabel, System.Windows.Forms.CheckBox associatedCheckBox)
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

            // Check if the TextBox has a value
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                associatedLabel.ForeColor = Color.Red;
                associatedCheckBox.Enabled = true;
            }
            // No value
            else
            {
                associatedLabel.ForeColor = DefaultForeColor;
                associatedCheckBox.Enabled = false;
                associatedCheckBox.Checked = false;
            }

            //Trigger Generate Every Time Press
            button1_Click(this, EventArgs.Empty);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Exclusive for Stats Description START
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
                        result.Append($"{statPrefix}{segment}");

                        // Add a comma and space if it's not the last element
                        if (i < segments.Length - 1)
                        {
                            result.Append(", ");
                        }
                    }
                }
            }
        }
        // Exclusive for Stats Description END

        private void GenerateBasicDescriptionandScript(string statName, TextBox textBox, string delimiter, StringBuilder result)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                result.AppendLine($"{statName}{textBox.Text}{delimiter}");
            }
        }

        private void GenerateStatsDescriptionAndScript(bool isChecked, string bonusType, TextBox textBox, StringBuilder result)
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

            GenerateBasicDescriptionandScript(checkBox8.Checked ? "\"Base HP -" : "\"Base HP +", textBox8, ".\",", result3);
            GenerateBasicDescriptionandScript(checkBox9.Checked ? "\"Reduce max HP by " : "\"Increase max HP by ", textBox9, "%.\",", result3);
            GenerateBasicDescriptionandScript(checkBox10.Checked ? "\"Base SP -" : "\"Base SP +", textBox10, ".\",", result3);
            GenerateBasicDescriptionandScript(checkBox11.Checked ? "\"Reduce max SP by " : "\"Increase max SP by ", textBox11, "%.\",", result3);
            
            GenerateBasicDescriptionandScript(checkBox12.Checked ? "\"ATK -" : "\"ATK +", textBox12, ".\",", result3);
            GenerateBasicDescriptionandScript(checkBox13.Checked ? "\"Min ATK -" : "\"Min ATK +", textBox13, ".\",", result3); // Mostly this is not using
            GenerateBasicDescriptionandScript(checkBox14.Checked ? "\"Max ATK -" : "\"Max ATK +", textBox14, ".\",", result3); // Mostly this is not using
            GenerateBasicDescriptionandScript(checkBox15.Checked ? "\"ATK -" : "\"ATK +", textBox15, "%.\",", result3);
            GenerateBasicDescriptionandScript(checkBox16.Checked ? "\"Weapon ATK -" : "\"Weapon ATK +", textBox16, "%.\",", result3);
            GenerateBasicDescriptionandScript(checkBox17.Checked ? "\"MATK -" : "\"MATK +", textBox17, ".\",", result3);
            GenerateBasicDescriptionandScript(checkBox18.Checked ? "\"MATK -" : "\"MATK +", textBox18, "%.\",", result3);
            GenerateBasicDescriptionandScript(checkBox19.Checked ? "\"Weapon MATK -" : "\"Weapon MATK +", textBox19, "%.\",", result3);
            GenerateBasicDescriptionandScript(checkBox20.Checked ? "\"DEF -" : "\"DEF +", textBox20, ".\",", result3);
            GenerateBasicDescriptionandScript(checkBox21.Checked ? "\"DEF -" : "\"DEF +", textBox21, "%.\",", result3);
            //GenerateBasicDescriptionandScript(checkBox22.Checked ? "\"Reduce defense by " : "\"Increase based vit defense by ", textBox22, ".\",", result3); // Should be Include in minus def
            //GenerateBasicDescriptionandScript(checkBox23.Checked ? "\"Reduce defense by " : "\"Increase based vit defense by ", textBox23, "%.\",", result3);// Should be Include in minus def rate

            //GenerateBasicDescriptionandScript(checkBox16.Checked ? "\"AAAAAA" : "\"AAAAAA", textBox16, ".\",", result3); // Example

            GenerateBasicDescriptionandScript(checkBox24.Checked ? "\"AAAAAA" : "\"AAAAAA", textBox24, ".\",", result3); // Example
            GenerateBasicDescriptionandScript(checkBox25.Checked ? "\"AAAAAA" : "\"AAAAAA", textBox25, ".\",", result3); // Example
            GenerateBasicDescriptionandScript(checkBox26.Checked ? "\"AAAAAA" : "\"AAAAAA", textBox26, ".\",", result3); // Example
            GenerateBasicDescriptionandScript(checkBox27.Checked ? "\"AAAAAA" : "\"AAAAAA", textBox27, ".\",", result3); // Example

            //GenerateBasicDescriptionandScript("\"Increase defense based on VIT by ", textBox22, ".\",", result3); //VIT based DEF + VALUE
            //GenerateBasicDescriptionandScript("\"Increase defense based on VIT by ", textBox23, "%.\",", result3); //VIT based DEF + VALUE%
            //GenerateBasicDescriptionandScript("\"MDEF + ", textBox24, ".\",", result3); //Equipment MDEF + VALUE
            //GenerateBasicDescriptionandScript("\"MDEF + ", textBox25, "%.\",", result3); //Equipment MDEF + VALUE%
            //GenerateBasicDescriptionandScript("\"MDEF based on INT + ", textBox26, ".\",", result3); //INT based MDEF + VALUE
            //GenerateBasicDescriptionandScript("\"MDEF based on INT + ", textBox27, "%.\",", result3); //INT based MDEF + VALUE%

            string combinedResultStats = "\"" + result1.ToString().TrimEnd(',', ' ') + ".\",\n";

            if (textBox1.Text != "" || textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "" || textBox5.Text != "" || textBox6.Text != "" || textBox7.Text != "")
            {
                richTextBox1.AppendText(combinedResultStats);
            }
            richTextBox1.AppendText(result3.ToString());

            //-------------------------------------------- GENERATING ITEM SCRIPTS START

            GenerateStatsDescriptionAndScript(checkBox1.Checked, "      bonus bStr,", textBox1, result2);
            GenerateStatsDescriptionAndScript(checkBox2.Checked, "      bonus bAgi,", textBox2, result2);
            GenerateStatsDescriptionAndScript(checkBox3.Checked, "      bonus bVit,", textBox3, result2);
            GenerateStatsDescriptionAndScript(checkBox4.Checked, "      bonus bInt,", textBox4, result2);
            GenerateStatsDescriptionAndScript(checkBox5.Checked, "      bonus bDex,", textBox5, result2);
            GenerateStatsDescriptionAndScript(checkBox6.Checked, "      bonus bLuk,", textBox6, result2);
            GenerateStatsDescriptionAndScript(checkBox7.Checked, "      bonus bAllStats,", textBox7, result2);

            GenerateStatsDescriptionAndScript(checkBox8.Checked, "      bonus bMaxHP,", textBox8, result2);
            GenerateStatsDescriptionAndScript(checkBox9.Checked, "      bonus bMaxHPrate,", textBox9, result2);
            GenerateStatsDescriptionAndScript(checkBox10.Checked, "      bonus bMaxSP,", textBox10, result2);
            GenerateStatsDescriptionAndScript(checkBox11.Checked, "      bonus bMaxSPrate,", textBox11, result2);

            GenerateBasicDescriptionandScript("      bonus bBaseAtk,", textBox12, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bAtk,", textBox13, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bAtk2,", textBox14, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bAtkRate,", textBox15, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bWeaponAtkRate,", textBox16, ";", result2);

            GenerateBasicDescriptionandScript("      bonus bMatk,", textBox17, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMatkRate,", textBox18, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bWeaponMatkRate,", textBox19, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bDef,", textBox20, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bDef2,", textBox20, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bDefRate,", textBox21, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bDef2Rate,", textBox21, ";", result2);


            GenerateBasicDescriptionandScript("      bonus bDef2,", textBox22, ";", result2); // included in bDef
            GenerateBasicDescriptionandScript("      bonus bDef2Rate,", textBox23, ";", result2); // included in bDefRate
            GenerateBasicDescriptionandScript("      bonus bMdef,", textBox24, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMdefRate,", textBox25, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMdef2,", textBox26, ";", result2);
            GenerateBasicDescriptionandScript("      bonus bMdef2Rate,", textBox27, ";", result2);


            richTextBox2.AppendText(result2.ToString());

            //-------------------------------------------- GENERATING ITEM SCRIPTS END
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
