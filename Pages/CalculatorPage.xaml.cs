namespace _253505_Azarov_Lab1.Pages;
public partial class CalculatorPage: ContentPage
{
	private string currentEntry {get;set;} = "0";
	private bool isFloat{get;set;} = false;
	private bool afterOperation {get;set;} = false;
	private double buff = 0;
	private bool afterDoubleOperation {get;set;} = false;	
	private bool preCalc {get;set;} = false;
	private string mathOperation {get;set;} = "";
	private double memory{get;set;} = 0;
	private bool memLock{get;set;} = true;
	public CalculatorPage()
	{
		InitializeComponent();
	}
	private void OnSingleActionClicked(object sender, EventArgs eventArgs)
	{

		Button btn = (Button)sender;
		double val = Double.Parse(currentEntry);
		if(btn.Text == "2^x")
		{
			val = Math.Pow(2, val);
			CurrentCalculation.Text = $"2^{val}";
			afterOperation = true;
		}
		else if(btn.Text == "%")
		{
			val /= 100;						
			CurrentCalculation.Text = $"%{val}";
			afterOperation = true;
		}
		else if(btn.Text == "1/x")
		{
			val = 1/val;
			CurrentCalculation.Text = $"1/{val}";
			afterOperation = true;
		}
		else if(btn.Text == "x²")
		{
			val = val*val;
			CurrentCalculation.Text = $"{val}²";
			afterOperation = true;
		}
		else if(btn.Text == "√x")
		{
			val = Math.Sqrt(val);
			CurrentCalculation.Text = $"sqrt({val})";
			afterOperation = true;
		}
		else if(btn.Text == "+/-")
		{
			CurrentCalculation.Text = $"neg({val})";
			val *= -1;
		}
		currentEntry = val.ToString();
		ResultText.Text = currentEntry;
	}
	
	private void OnMemoryActionClicked(object sender, EventArgs eventArgs)
	{
		Button btn = (Button)sender;
		if(btn.Text == "MC")
		{
			memLock = true;
			memory = 0;
		}
		else if(btn.Text == "MR" && !memLock)
		{
			currentEntry = Convert.ToString(memory);
			ResultText.Text = currentEntry;	
			if(afterDoubleOperation)
			{
				preCalc = true;
			}
			afterOperation = true;
		}
		else if(btn.Text == "M+")
		{
			memLock = false;
			memory +=  Double.Parse(currentEntry);
		}
		else if(btn.Text == "M-")
		{

			memLock = false;
			memory -=  Double.Parse(currentEntry);
		}
		else if(btn.Text == "MS")
		{
			
			memLock = false;
			memory =  Double.Parse(currentEntry);
		}
	}
	
	private void OnDeleteClicked(object sender, EventArgs eventArgs)
	{
		if(afterOperation)
			return;
		if(currentEntry.Length > 0)	
		{
			if(currentEntry[currentEntry.Length - 1] == '.')
				isFloat = false;
			currentEntry = currentEntry.Remove(currentEntry.Length - 1, 1);	
		}	
		if(currentEntry.Length == 0)
			currentEntry = "0";
		ResultText.Text = currentEntry;
	}

	private void OnDoubleActionClicked(object sender, EventArgs eventArgs)
	{
		Button btn = (Button)sender;		
		double val = Double.Parse(currentEntry);
		if(!afterDoubleOperation)
		{
			buff = Double.Parse(currentEntry);
			afterDoubleOperation = true;
			CurrentCalculation.Text = $"{currentEntry}{btn.Text}";
			mathOperation = btn.Text;
			return;	
		}
		if(afterDoubleOperation && !preCalc)
		{
			CurrentCalculation.Text = CurrentCalculation.
				Text.Remove(CurrentCalculation.Text.Length - 1);
			mathOperation = btn.Text;
			CurrentCalculation.Text += btn.Text;
			return;
		}
		if(mathOperation == "+")
		{
			buff += val;
		}
		else if(mathOperation == "-")
		{
			buff -= val;
		}
		else if(mathOperation == "*")
		{
			buff *= val;
		}
		else if(mathOperation == "/")
		{
			buff /= val;		
		}
		currentEntry = Convert.ToString(buff);
		ResultText.Text = currentEntry;
		CurrentCalculation.Text += $"{currentEntry}{btn.Text}"; 
		afterOperation = true;
		mathOperation = btn.Text;
		preCalc = false;
	}
	
	private void OnNumberClicked(object sender, EventArgs eventArgs)
	{

		Button btn = (Button)sender;
		if(afterDoubleOperation)
		{
			preCalc = true;
		}
		if(afterOperation || afterDoubleOperation)
		{
			currentEntry = "";
			afterOperation = false;
			// afterDoubleOperation = false;
		}
		if(currentEntry.Length < 14)
		{
			if(currentEntry == "0")
				currentEntry = Convert.ToString(btn.Text);
			else
				currentEntry += btn.Text;
			ResultText.Text = currentEntry;
		}
		
	}
	
	private void OnCommaClicked(object sender, EventArgs eventArgs)
	{
		if(afterDoubleOperation || afterOperation)
		{
			// afterDoubleOperation = false;
			afterOperation = false;
			currentEntry = "0";
		}
		if(!isFloat)
		{
			currentEntry+=",";
			isFloat = true;
			ResultText.Text = currentEntry;
			if(afterDoubleOperation)
			{
				preCalc = true;
			}
		}
	}
	
	private void OnEqualClicked(object sender, EventArgs eventArgs)
	{
		double val = Double.Parse(currentEntry);
		Button btn = (Button)sender;
		if(mathOperation == "+")
		{
			buff += val;
		}
		else if(mathOperation == "-")
		{
			buff -= val;
		}
		else if(mathOperation == "*")
		{
			buff *= val;
		}
		else if(mathOperation == "/")
		{
			buff /= val;		
		}
		else if(mathOperation == "")
		{
			buff = val;
			CurrentCalculation.Text = "";
		}
		CurrentCalculation.Text += $"{currentEntry}="; 
		currentEntry = Convert.ToString(buff);
		ResultText.Text = currentEntry;
		afterDoubleOperation = false;
		afterOperation = true;
		mathOperation = "";
		buff = 0;
		preCalc = false;
	}
	private void OnClearActionClicked(object sender, EventArgs eventArgs)	
	{
		Button btn = (Button)sender;
		currentEntry = "0";
		isFloat = false;
		ResultText.Text = currentEntry;
		afterDoubleOperation = false;
		afterDoubleOperation = false;
		preCalc = false;
		mathOperation = "";
		if(btn.Text == "C")
		{
			CurrentCalculation.Text = "";
		}
	}
}

