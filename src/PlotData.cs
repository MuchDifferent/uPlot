/// <summary>
/// This class stores all of the data required to draw a line chart.
/// </summary>
public class PlotData
{
	#region PUBLIC_VARIABLES

	public string name;
	public double[] xAxisData;
	public double[] yAxisData;
	public UnityEngine.Color lineColor;

	#endregion PUBLIC_VARIABLES

	#region PUBLIC_CONSTRUCTOR

	public PlotData(string name, double[] xAxisData, double[] yAxisData, UnityEngine.Color lineColor)
	{
		this.name = name;
		this.xAxisData = new double[xAxisData.Length];
		xAxisData.CopyTo(this.xAxisData, 0);
		this.yAxisData = new double[yAxisData.Length];
		yAxisData.CopyTo(this.yAxisData, 0);
		this.lineColor = lineColor;
	}

	#endregion PUBLIC_CONSTRUCTOR
}