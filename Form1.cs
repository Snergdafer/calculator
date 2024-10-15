using System.Text.RegularExpressions;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool txtDisplayError = false;
        Regex equationPattern = new Regex(@"[-+*/]|([0-9]*[.])?[0-9]+");
        Regex floatPattern = new Regex(@"[0-9|.]+");
        float? operandOne = null;
        float? operandTwo = null;
        string? operater = null;

        private void handleUpdateDisplayText(string text)
        {
            // Check for an error or for the Clear button
            if (txtDisplayError || String.IsNullOrEmpty(text))
            {
                operandOne = null;
                operandTwo = null;
                operater = null;
                txtDisplayError = false;
                txtDisplay.Clear();
            // Check for a new round of calculating (not using the last answer that was calculated)
            } else if (operandOne.HasValue && floatPattern.Count(text) > 0)
            {
                operandOne = null;
                txtDisplay.Clear();
            }
            operandOne = null;
            txtDisplay.Text += text;
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            try
            {
                // Chops up the string into Floats and Operators (still as strings though)
                foreach (Match match in equationPattern.Matches(txtDisplay.Text))
                {
                    // Calculates the equation one operation at a time (non scientific)
                    if (!operandOne.HasValue)
                    {
                        operandOne = float.Parse(match.Value);
                    }
                    else if (String.IsNullOrEmpty(operater))
                    {
                        operater = match.Value;
                    }
                    else if (!operandTwo.HasValue)
                    {
                        operandTwo = float.Parse(match.Value);

                        // Once there are two Operands captured we calculate based on the Operator
                        if (operater.Equals("+"))
                        {
                            // Reset for the next calculation
                            operandOne += operandTwo;
                            operater = null;
                            operandTwo = null;
                        }

                        else if (operater.Equals("-"))
                        {
                            operandOne -= operandTwo;
                            operater = null;
                            operandTwo = null;
                        }

                        else if (operater.Equals("*"))
                        {
                            operandOne = operandOne * operandTwo;
                            operater = null;
                            operandTwo = null;
                        }

                        else if (operater.Equals("/"))
                        {
                            operandOne = operandOne / operandTwo;
                            operater = null;
                            operandTwo = null;
                        }
                    }
                }

                // Then update the display with the answer but don't clear out the first Operator in case of further calculations
                operater = null;
                operandTwo = null;
                txtDisplay.Text = Convert.ToString(operandOne);
            }
            catch (Exception ex)
            {
                // If anthing breaks above show the classic calculator Error
                txtDisplayError = true;
                operandOne = null;
                operandTwo = null;
                operater = null;
                txtDisplay.Text = "Error";
            }
        }

        // **************************** Ugly button binding ****************************

        private void buttonClear_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("");
        }

        private void button0_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("0");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("9");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("+");
        }

        private void buttonSubtract_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("-");
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("*");
        }
        private void buttonDivide_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText("/");
        }
        private void buttonPeriod_Click(object sender, EventArgs e)
        {
            handleUpdateDisplayText(".");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
